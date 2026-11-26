using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Historial de compras de un cliente buscado por apellidos
public partial class FrmHistorialCliente : Form
{
    private readonly IClienteService _clienteService;
    private readonly IReporteService _reportes;

    // Evita consultar las compras varias veces mientras se llena la tabla de clientes
    private bool _cargando;

    public FrmHistorialCliente(IClienteService clienteService, IReporteService reportes)
    {
        InitializeComponent();
        _clienteService = clienteService;
        _reportes = reportes;
        dgvClientes.AutoGenerateColumns = false;
        dgvCompras.AutoGenerateColumns = false;
        colSubtotal.DefaultCellStyle.FormatProvider = Formato.Cultura;
        colTotal.DefaultCellStyle.FormatProvider = Formato.Cultura;
        Portapapeles.HabilitarCopiaFolio(dgvCompras, colFolio);
    }

    private void BuscarClientes(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtApellidos.Text))
        {
            Mensajes.Aviso("Escriba los apellidos del cliente.");
            return;
        }

        _cargando = true;
        try
        {
            // También aparecen los clientes dados de baja: sus compras siguen guardadas
            List<Cliente> clientes = _clienteService.ObtenerClientesConInactivos(txtApellidos.Text);
            dgvClientes.DataSource = clientes;
            if (clientes.Count == 0)
            {
                Mensajes.Informacion("No se encontraron clientes con esos apellidos.");
            }
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible buscar a los clientes.\n\n{Errores.Mensaje(ex)}");
        }
        finally
        {
            _cargando = false;
        }

        CargarCompras();
    }

    private void CargarCompras()
    {
        dgvCompras.DataSource = null;
        if (dgvClientes.CurrentRow?.DataBoundItem is not Cliente cliente)
        {
            lblCliente.Text = "Cliente seleccionado: -";
            return;
        }

        try
        {
            List<ResumenVenta> compras = _reportes.ObtenerHistorialCliente(cliente.IdCliente, rdoPartido.Checked);
            dgvCompras.DataSource = compras;
            string baja = cliente.Activo ? string.Empty : " (dado de baja)";
            lblCliente.Text = $"Cliente seleccionado: {cliente.NombreParaMostrar}{baja}   RFC: {cliente.RFC}   Compras: {compras.Count}";
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void dgvClientes_SelectionChanged(object? sender, EventArgs e)
    {
        if (!_cargando)
        {
            CargarCompras();
        }
    }

    // Basta con este evento: al elegir cualquiera de las dos opciones, rdoCompra cambia
    private void rdoCompra_CheckedChanged(object? sender, EventArgs e)
    {
        CargarCompras();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
