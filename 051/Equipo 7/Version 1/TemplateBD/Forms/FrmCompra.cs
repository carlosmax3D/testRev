using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmCompra : Form
{
    private readonly IVentaService _ventaService;
    private readonly IClienteService _clienteService;
    private readonly IServiceProvider _serviceProvider;
    private int _idPartidoSeleccionado = 0;

    public FrmCompra(
        IVentaService ventaService,
        IClienteService clienteService,
        IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _ventaService = ventaService;
        _clienteService = clienteService;
        _serviceProvider = serviceProvider;
    }

    private void FrmCompra_Load(object sender, EventArgs e)
    {
        cmbMetodoPago.Items.Clear();
        cmbMetodoPago.Items.AddRange(["Efectivo", "Tarjeta de Crédito", "Tarjeta de Débito"]);
        cmbMetodoPago.SelectedIndex = 0;

        CargarClientes();
        CargarPartidos();
    }

    private void CargarClientes(string? filtro = null)
    {
        List<Cliente> clientes = _clienteService.ObtenerClientes(filtro);
        cmbCliente.DataSource = clientes;
        cmbCliente.DisplayMember = "NombreCompleto";
        cmbCliente.ValueMember = "IdCliente";
    }

    private void CargarPartidos(string? filtro = null)
    {
        try
        {
            dgvPartidos.DataSource = null;
            dgvPartidos.DataSource = _ventaService.ObtenerPartidosDisponibles(filtro);

            string[] ocultas = ["Fecha", "IdEstadio", "IdEquipoLocal", "IdEquipoVisitante", "UsuarioAuditoria"];
            foreach (string col in ocultas)
            {
                if (dgvPartidos.Columns[col] != null)
                    dgvPartidos.Columns[col].Visible = false;
            }

            if (dgvPartidos.Columns["FechaFormateada"] != null)
                dgvPartidos.Columns["FechaFormateada"].HeaderText = "Fecha";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar partidos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnBuscarCliente_Click(object sender, EventArgs e)
    {
        CargarClientes(txtBuscarCliente.Text.Trim());
    }

    private void btnNuevoCliente_Click(object sender, EventArgs e)
    {
        var frmClientes = _serviceProvider.GetRequiredService<FrmClientes>();
        frmClientes.ShowDialog();
        CargarClientes();
    }

    private void btnBuscarPartido_Click(object sender, EventArgs e)
    {
        CargarPartidos(txtBuscarPartido.Text.Trim());
    }

    private void dgvPartidos_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvPartidos.CurrentRow?.DataBoundItem is Partido partido)
        {
            _idPartidoSeleccionado = partido.IdPartido;
            CargarZonasPartido(_idPartidoSeleccionado);
        }
    }

    private void CargarZonasPartido(int idPartido)
    {
        List<ZonaDisponible> zonas = _ventaService.ObtenerZonasDisponibles(idPartido);
        cmbZona.DataSource = zonas;
        cmbZona.DisplayMember = "DescripcionCombo";
        cmbZona.ValueMember = "IdZona";
        ActualizarResumenTotales();
    }

    private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActualizarResumenTotales();
    }

    private void nudCantidad_ValueChanged(object sender, EventArgs e)
    {
        ActualizarResumenTotales();
    }

    private void ActualizarResumenTotales()
    {
        if (cmbZona.SelectedItem is ZonaDisponible zona)
        {
            int cantidad = (int)nudCantidad.Value;
            var (subtotal, iva, total) = _ventaService.CalcularTotales(zona.PrecioBase, cantidad);

            lblSubtotal.Text = subtotal.ToString("$#,##0.00");
            lblIVA.Text = iva.ToString("$#,##0.00");
            lblTotal.Text = total.ToString("$#,##0.00");
        }
        else
        {
            lblSubtotal.Text = "$0.00";
            lblIVA.Text = "$0.00";
            lblTotal.Text = "$0.00";
        }
    }

    private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool esTarjeta = cmbMetodoPago.SelectedItem?.ToString() != "Efectivo";
        txtTitular.Enabled = esTarjeta;
        txtNumeroTarjeta.Enabled = esTarjeta;
        txtExpiracion.Enabled = esTarjeta;
        txtCCV.Enabled = esTarjeta;

        if (!esTarjeta)
        {
            txtTitular.Clear();
            txtNumeroTarjeta.Clear();
            txtExpiracion.Clear();
            txtCCV.Clear();
        }
    }

    private void btnConfirmarCompra_Click(object sender, EventArgs e)
    {
        try
        {
            if (cmbCliente.SelectedValue is not int idCliente)
            {
                MessageBox.Show("Seleccione o registre un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbZona.SelectedItem is not ZonaDisponible zonaSeleccionada)
            {
                MessageBox.Show("Seleccione una zona disponible para el partido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SolicitudVenta solicitud = new()
            {
                IdCliente = idCliente,
                IdPartido = _idPartidoSeleccionado,
                IdZona = zonaSeleccionada.IdZona,
                Cantidad = (int)nudCantidad.Value,
                MetodoPago = cmbMetodoPago.SelectedItem?.ToString() ?? "Efectivo",
                NombreTitular = txtTitular.Text.Trim(),
                NumeroTarjeta = txtNumeroTarjeta.Text.Trim(),
                FechaExpiracion = txtExpiracion.Text.Trim(),
                CCV = txtCCV.Text.Trim()
            };

            ResultadoVenta resultado = _ventaService.ProcesarVenta(solicitud, zonaSeleccionada.PrecioBase);

            MessageBox.Show(
                $"¡Compra realizada con éxito!\n\n" +
                $"Folio de Compra (GUID):\n{resultado.FolioCompra}\n\n" +
                $"Asientos asignados:\n{resultado.AsientosAsignados}\n\n" +
                $"Total Pagado: {resultado.Total:$#,##0.00}\n" +
                $"La factura ha sido generada automáticamente.",
                "Venta Confirmada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Refrescar disponibilidad de zonas y limpiar pago
            CargarZonasPartido(_idPartidoSeleccionado);
            nudCantidad.Value = 1;
            cmbMetodoPago.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación de Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void label2_Click(object sender, EventArgs e) { }
    private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
    private void textBox1_TextChanged(object sender, EventArgs e) { }
}
