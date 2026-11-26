using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Factura de una venta, buscada por el folio (GUID) de la compra
public partial class FrmFactura : Form
{
    private readonly IFacturaService _facturas;
    private Factura? _factura;

    public FrmFactura(IFacturaService facturas)
    {
        InitializeComponent();
        _facturas = facturas;
        dgvDetalle.AutoGenerateColumns = false;
        colPrecioUnitario.DefaultCellStyle.FormatProvider = Formato.Cultura;
        colImporte.DefaultCellStyle.FormatProvider = Formato.Cultura;
        Limpiar();
    }

    public void CargarFactura(string folio)
    {
        txtFolio.Text = folio.Trim();
        try
        {
            Factura? factura = _facturas.ObtenerFactura(folio);
            if (factura is null)
            {
                Limpiar();
                Mensajes.Aviso("No se encontró la venta. Revise el folio e intente de nuevo.");
                return;
            }

            Mostrar(factura);
        }
        catch (Exception ex)
        {
            Limpiar();
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Mostrar(Factura factura)
    {
        _factura = factura;
        lblEmisor.Text = $"{factura.EmisorRazonSocial}    RFC: {factura.EmisorRFC}{Environment.NewLine}{factura.EmisorDomicilio}";
        lblReceptor.Text = $"{factura.ReceptorNombre}    RFC: {factura.ReceptorRFC}{Environment.NewLine}{factura.ReceptorDomicilio}";
        lblComprobante.Text = $"Factura {factura.FolioFactura}    Emisión: {Formato.FechaHora(factura.FechaHoraEmision)}    Pago: {factura.MetodoPago}    Estado: {factura.Estado}";
        dgvDetalle.DataSource = factura.Detalle;
        lblIvaTexto.Text = $"IVA ({factura.TasaIva * 100:0.##}%):";
        lblSubtotal.Text = Formato.Moneda(factura.Subtotal);
        lblIva.Text = Formato.Moneda(factura.Iva);
        lblTotal.Text = Formato.Moneda(factura.Total);
    }

    private void Limpiar()
    {
        _factura = null;
        lblEmisor.Text = string.Empty;
        lblReceptor.Text = string.Empty;
        lblComprobante.Text = string.Empty;
        dgvDetalle.DataSource = null;
        lblSubtotal.Text = string.Empty;
        lblIva.Text = string.Empty;
        lblTotal.Text = string.Empty;
    }

    private void btnBuscar_Click(object? sender, EventArgs e)
    {
        CargarFactura(txtFolio.Text);
    }

    // Si cambia el folio después de buscar, se borra la factura mostrada para no exportar otra
    private void txtFolio_TextChanged(object? sender, EventArgs e)
    {
        if (_factura != null && !(Validador.FolioValido(txtFolio.Text, out Guid folio) && folio == _factura.FolioVenta))
        {
            Limpiar();
        }
    }

    private void btnExportarPdf_Click(object? sender, EventArgs e)
    {
        if (_factura is null)
        {
            Mensajes.Aviso("Primero busque la factura con el folio de la venta.");
            return;
        }

        using SaveFileDialog dialogo = new()
        {
            Filter = "Documento PDF|*.pdf",
            FileName = $"Factura_{_factura.FolioFactura}.pdf"
        };
        if (dialogo.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            _facturas.ExportarPdf(_factura, dialogo.FileName);
            Mensajes.Informacion("Factura exportada a PDF.");
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
