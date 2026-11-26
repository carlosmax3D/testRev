using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Services;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

/// <summary>Historial de compras de un cliente (solo administrador).</summary>
public partial class FrmHistorialCliente : Form
{
    private readonly IReporteService _reportes;
    private readonly IClienteService _clientes;

    public FrmHistorialCliente(IReporteService reportes, IClienteService clientes)
    {
        _reportes = reportes;
        _clientes = clientes;

        InitializeComponent();
        Ui.Estilizar(dgvHistorial);

        cmbOrden.SelectedIndex = 0;
        cmbCliente.Format += (_, e) => e.Value = e.ListItem is Cliente c ? Descripciones.DeCliente(c) : string.Empty;

        dgvHistorial.CellFormatting += (_, e) =>
        {
            if (e.RowIndex >= 0 && dgvHistorial.Rows[e.RowIndex].DataBoundItem is HistorialClienteItem h
                && h.EstadoVenta.Equals("Cancelada", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle!.ForeColor = Ui.Peligro;
            }
        };

        cmbCliente.SelectedIndexChanged += (_, _) => MostrarDatos();
        txtBuscar.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnBuscar_Click(null, EventArgs.Empty);
            }
        };
    }

    private void BtnBuscar_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtBuscar.Text))
        {
            Ui.Aviso(this, "Escriba el apellido o el correo del cliente.");
            return;
        }

        List<Cliente> encontrados = [];

        if (!Ui.Ejecutar(this, () => encontrados = _clientes.BuscarClientes(txtBuscar.Text.Trim(), false)))
        {
            return;
        }

        cmbCliente.DataSource = encontrados;
        dgvHistorial.DataSource = null;
        MostrarDatos();

        if (encontrados.Count == 0)
        {
            Ui.Aviso(this, "No se encontraron clientes con ese apellido o correo.");
        }
    }

    private void MostrarDatos()
    {
        lblDatos.Text = cmbCliente.SelectedItem is Cliente c
            ? $"RFC: {c.RFC}   |   {c.NombreCompleto}   |   Nacimiento: {Formato.Fecha(c.FechaNacimiento)}\n" +
              $"Domicilio: {c.DomicilioCompleto}   |   Correo: {c.Correo}   |   Tel. casa: {c.TelefonoCasa}   |   Celular: {c.TelefonoCelular}"
            : string.Empty;
    }

    private void BtnConsultar_Click(object? sender, EventArgs e)
    {
        if (cmbCliente.SelectedItem is not Cliente cliente)
        {
            Ui.Aviso(this, "Seleccione un cliente.");
            return;
        }

        OrdenHistorial orden = cmbOrden.SelectedIndex == 1 ? OrdenHistorial.FechaPartido : OrdenHistorial.FechaCompra;
        List<HistorialClienteItem> lista = [];

        if (!Ui.Ejecutar(this, () => lista = _reportes.HistorialCliente(cliente.RFC, orden)))
        {
            return;
        }

        dgvHistorial.DataSource = lista;

        if (lista.Count == 0)
        {
            Ui.Aviso(this, "El cliente no tiene compras registradas.");
        }
    }
}
