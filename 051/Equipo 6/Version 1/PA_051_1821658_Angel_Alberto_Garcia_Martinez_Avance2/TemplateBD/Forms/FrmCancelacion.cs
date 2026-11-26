using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Cancelación de una venta por su folio: libera los boletos y cancela la factura
public partial class FrmCancelacion : Form
{
    private readonly ICancelacionService _cancelaciones;

    // Venta que se encontró con el folio escrito
    private ResumenVenta? _venta;

    public FrmCancelacion(ICancelacionService cancelaciones)
    {
        InitializeComponent();
        _cancelaciones = cancelaciones;
        txtMotivo.MaxLength = _cancelaciones.LongitudMaximaMotivo;
        Mostrar(null);
    }

    private void Buscar(object? sender, EventArgs e)
    {
        try
        {
            ResumenVenta? venta = _cancelaciones.BuscarVenta(txtFolio.Text);
            Mostrar(venta);

            if (venta is null)
            {
                Mensajes.Aviso("No se encontró la venta. Revise el folio e intente de nuevo.");
            }
            else if (venta.Cancelada)
            {
                Mensajes.Informacion("Esa venta ya está cancelada.");
            }
        }
        catch (Exception ex)
        {
            Mostrar(null);
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Mostrar(ResumenVenta? venta)
    {
        _venta = venta;

        if (venta is null)
        {
            lblCliente.Text = "Cliente: -";
            lblPartido.Text = "Partido: -";
            lblBoletos.Text = "Boletos: -";
            btnCancelarVenta.Enabled = false;
            return;
        }

        lblCliente.Text = $"Cliente: {venta.Cliente}    Factura: {venta.FolioFactura} ({venta.Estado})";
        lblPartido.Text = $"Partido: {venta.Partido} - {venta.Estadio} - {Formato.FechaHora(venta.FechaPartido)}";
        lblBoletos.Text = $"Boletos: {venta.CantidadBoletos} ({venta.Zonas})    Total: {Formato.Moneda(venta.Total)}";
        btnCancelarVenta.Enabled = !venta.Cancelada;
    }

    // Si cambia el folio después de buscar, se borra la venta mostrada y hay que buscar de nuevo
    private void txtFolio_TextChanged(object? sender, EventArgs e)
    {
        if (_venta != null && !(Validador.FolioValido(txtFolio.Text, out Guid folio) && folio == _venta.Folio))
        {
            Mostrar(null);
        }
    }

    private void Cancelar(object? sender, EventArgs e)
    {
        if (_venta is null || !Validador.FolioValido(txtFolio.Text, out Guid folio) || folio != _venta.Folio)
        {
            Mostrar(null);
            Mensajes.Aviso("Primero busque la venta por su folio.");
            return;
        }

        if (Validador.Vacio(txtMotivo.Text))
        {
            Mensajes.Aviso("Debe escribir el motivo de la cancelación.");
            return;
        }

        if (!Mensajes.Confirmar($"¿Confirma la cancelación de la venta con folio {_venta.Folio}?\n\n" +
            $"Cliente: {_venta.Cliente}\nPartido: {_venta.Partido} ({Formato.Fecha(_venta.FechaPartido)})\n" +
            $"Factura: {_venta.FolioFactura}    Total: {Formato.Moneda(_venta.Total)}\n\n" +
            "Los boletos quedarán libres y la factura cancelada."))
        {
            return;
        }

        try
        {
            _cancelaciones.CancelarVenta(folio, txtMotivo.Text);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
            return;
        }

        Mensajes.Informacion("Venta cancelada. Los boletos quedaron libres y la factura quedó cancelada.");
        txtFolio.Clear();
        txtMotivo.Clear();
        Mostrar(null);
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
