using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models.Dtos;
using VentaBoletosApp.Services;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

/// <summary>Consulta de facturas por folio de compra y exportacion a PDF (solo administrador).</summary>
public partial class FrmFacturacion : Form
{
    private readonly IFacturaService _facturas;

    private FacturaCompleta? _factura;

    public FrmFacturacion(IFacturaService facturas)
    {
        _facturas = facturas;

        InitializeComponent();
        Ui.Estilizar(dgvDetalle);
    }

    private void BtnBuscar_Click(object? sender, EventArgs e)
    {
        _factura = null;
        btnPdf.Enabled = false;
        dgvDetalle.DataSource = null;
        lblInfo.Text = string.Empty;

        FacturaCompleta? factura = null;

        if (!Ui.Ejecutar(this, () => factura = _facturas.ObtenerPorFolioVenta(txtFolio.Text)))
        {
            return;
        }

        if (factura is null)
        {
            Ui.Aviso(this, "No se encontró una factura para ese folio de compra.");
            return;
        }

        _factura = factura;
        dgvDetalle.DataSource = factura.Detalles;

        lblInfo.Text =
            $"Factura: {factura.Factura.Folio}   Estado: {factura.Factura.EstadoFactura}   Fecha: {Formato.FechaHora(factura.Factura.Fecha)}\n" +
            $"Emisor: {factura.Emisor.RazonSocial} ({factura.Emisor.RFC})\n" +
            $"Receptor: {factura.Receptor.NombreCompleto} ({factura.Receptor.RFC})\n" +
            $"Partido: {factura.Venta.EquipoLocal} vs {factura.Venta.EquipoVisitante} - {Formato.FechaHora(factura.Venta.FechaPartido)}\n" +
            $"Subtotal: {Formato.Moneda(factura.Venta.Subtotal)}   IVA: {Formato.Moneda(factura.Venta.IVA)}   Total: {Formato.Moneda(factura.Venta.Total)}";

        btnPdf.Enabled = true;
    }

    private void BtnPdf_Click(object? sender, EventArgs e)
    {
        if (_factura is null)
        {
            return;
        }

        using SaveFileDialog dialogo = new()
        {
            Title = "Guardar factura",
            Filter = "Archivo PDF|*.pdf",
            FileName = $"Factura_{_factura.Factura.Folio}.pdf"
        };

        if (dialogo.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        FacturaCompleta factura = _factura;

        if (Ui.Ejecutar(this, () => _facturas.ExportarPdf(factura, dialogo.FileName)))
        {
            Ui.Aviso(this, "Factura exportada correctamente.");
        }
    }
}
