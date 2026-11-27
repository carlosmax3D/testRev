using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Printing;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmFacturacion : Form
{
    private readonly IFacturaService _facturaService;
    private FacturaCompleta? _facturaActual;

    public FrmFacturacion(IFacturaService facturaService)
    {
        InitializeComponent();
        _facturaService = facturaService;
    }

    private void FrmFacturacion_Load(object sender, EventArgs e)
    {
        CargarCompras();
    }

    private void CargarCompras()
    {
        try
        {
            dgvCompras.DataSource = null;
            dgvCompras.DataSource = _facturaService.ListarCompras();

            if (dgvCompras.Columns["Total"] != null)
                dgvCompras.Columns["Total"].DefaultCellStyle.Format = "$#,##0.00";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar historial de facturas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvCompras_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        if (dgvCompras.Rows[e.RowIndex].DataBoundItem is CompraResumen item)
        {
            txtFolioCompra.Text = item.FolioCompra;
            MostrarFactura(item.FolioCompra);
        }
    }

    private void btnBuscar_Click(object sender, EventArgs e)
    {
        MostrarFactura(txtFolioCompra.Text.Trim());
    }

    private void MostrarFactura(string folio)
    {
        try
        {
            _facturaActual = _facturaService.ConsultarFactura(folio);
            txtVistaPrevia.Text = GenerarTextoComprobante(_facturaActual);
        }
        catch (Exception ex)
        {
            _facturaActual = null;
            txtVistaPrevia.Clear();
            MessageBox.Show(ex.Message, "Consulta de Factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static string GenerarTextoComprobante(FacturaCompleta f)
    {
        StringBuilder sb = new();
        sb.AppendLine("======================================================================");
        sb.AppendLine("                     COMPROBANTE FISCAL / FACTURA                     ");
        sb.AppendLine("======================================================================");
        sb.AppendLine($"EMISOR: {f.EmisorNombre}");
        sb.AppendLine($"RFC Emisor: {f.EmisorRFC} | Domicilio: {f.EmisorDomicilio}");
        sb.AppendLine("----------------------------------------------------------------------");
        sb.AppendLine($"Folio Factura : {f.FolioFactura}");
        sb.AppendLine($"Folio Compra  : {f.FolioCompra}");
        sb.AppendLine($"Fecha Emisión : {f.FechaFactura:dd-MMM-yyyy HH:mm}");
        sb.AppendLine($"Método Pago   : {f.MetodoPago} | Estado Factura: {f.EstadoFactura.ToUpper()}");
        sb.AppendLine("----------------------------------------------------------------------");
        sb.AppendLine($"RECEPTOR      : {f.ReceptorNombre}");
        sb.AppendLine($"RFC Receptor  : {f.ReceptorRFC}");
        sb.AppendLine($"Domicilio     : {f.ReceptorDomicilio}");
        sb.AppendLine("----------------------------------------------------------------------");
        sb.AppendLine($"EVENTO        : {f.Partido}");
        sb.AppendLine($"ESTADIO       : {f.Estadio} | Fecha Partido: {f.FechaPartido}");
        sb.AppendLine("----------------------------------------------------------------------");
        sb.AppendLine("DETALLE DE BOLETOS:");

        foreach (var item in f.Detalles)
        {
            sb.AppendLine($"  • Zona: {item.Zona} | {item.Asiento} | Cant: {item.Cantidad} | P.U.: {item.PrecioUnitario:$#,##0.00} | Sub: {item.Subtotal:$#,##0.00}");
        }

        sb.AppendLine("----------------------------------------------------------------------");
        sb.AppendLine($"SUBTOTAL      : {f.Subtotal:$#,##0.00}");
        sb.AppendLine($"IVA (16%)     : {f.IVA:$#,##0.00}");
        sb.AppendLine($"TOTAL PAGADO  : {f.Total:$#,##0.00}");
        sb.AppendLine("======================================================================");
        return sb.ToString();
    }

    private void btnExportarPDF_Click(object sender, EventArgs e)
    {
        if (_facturaActual is null)
        {
            MessageBox.Show("Primero busque o seleccione una factura de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using SaveFileDialog saveDialog = new()
        {
            Title = "Guardar Factura en PDF",
            Filter = "Archivo PDF|*.pdf",
            FileName = $"Factura_{_facturaActual.FolioFactura[..8]}.pdf"
        };

        if (saveDialog.ShowDialog() != DialogResult.OK) return;

        try
        {
            using PrintDocument pd = new();
            pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            if (!pd.PrinterSettings.IsValid)
            {
                MessageBox.Show("La impresora 'Microsoft Print to PDF' no está instalada en Windows.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pd.PrinterSettings.PrintToFile = true;
            pd.PrinterSettings.PrintFileName = saveDialog.FileName;
            pd.PrintController = new StandardPrintController();
            pd.PrintPage += Pd_PrintPage;
            pd.Print();

            MessageBox.Show("Factura exportada a PDF correctamente.", "PDF Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(new ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo generar el PDF: {ex.Message}", "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Pd_PrintPage(object sender, PrintPageEventArgs e)
    {
        if (_facturaActual is null || e.Graphics is null) return;

        string contenido = GenerarTextoComprobante(_facturaActual);
        using Font fuente = new("Consolas", 9.5f);
        e.Graphics.DrawString(contenido, fuente, Brushes.Black, new RectangleF(40, 40, e.PageBounds.Width - 80, e.PageBounds.Height - 80));
    }
}