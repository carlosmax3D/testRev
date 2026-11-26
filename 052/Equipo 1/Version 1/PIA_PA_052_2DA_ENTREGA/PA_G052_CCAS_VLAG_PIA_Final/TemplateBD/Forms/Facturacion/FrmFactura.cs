using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;
using TemplateCSharp.Utilities;
using System.Collections.Generic;
using System.Linq;


namespace TemplateCSharp.Forms.Facturacion
{
    public partial class FrmFactura : Form
    {
        private readonly IFacturaService _facturaService;
        private readonly IVentaBoletoService _ventaService;
        private readonly IEmisorService _emisorService;

        private Factura? _facturaActual;

        public FrmFactura(IFacturaService facturaService, IVentaBoletoService ventaService, IEmisorService emisorService)
        {
            InitializeComponent();
            _facturaService = facturaService;
            _ventaService = ventaService;
            _emisorService = emisorService;
        }

        private void FrmFactura_Load(object sender, EventArgs e)
        {
            if (VentaContext.UltimoFolioVenta == Guid.Empty)
            {
                MessageBox.Show("No hay una venta reciente para facturar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            try
            {
                //Cargar datos de la mochila (venta context)
                _facturaActual = _facturaService.ObtenerFacturaPorVenta(VentaContext.UltimoFolioVenta);
                var venta = _ventaService.ObtenerVenta(VentaContext.UltimoFolioVenta);
                var cliente = VentaContext.ClienteSeleccionado;
                var emisor = _emisorService.ObtenerEmisores().FirstOrDefault(); // <-- Usamos el servicio


                if (_facturaActual == null || venta == null || cliente == null || emisor == null) return;


                //lenar datos del comprobante
                label1.Text = $"Folio Factura: {_facturaActual.FolioFactura}";
                label2.Text = $"Fecha: {_facturaActual.FechaEmision:dd/MMM/yyyy}";
                label3.Text = $"Hora: {_facturaActual.HoraEmision}";
                label16.Text = $"Estado: {_facturaActual.EstadoFactura}";
                lblMetodoPago.Text = $"Método de Pago: {venta.MetodoPago}";

                //llenar datos del Emisor ohh sii
                label5.Text = $"Empresa: {emisor.RazonSocial}";
                label6.Text = $"RFC: {emisor.EmisorRfc}";
                label7.Text = $"Domicilio: {emisor.DomicilioCompleto}";

                //Llenar datos del Receptor (el cliente papu)
                label9.Text = $"Nombre: {cliente.Nombre} {cliente.ApellidoPa} {cliente.ApellidoMa}";
                label10.Text = $"RFC: {cliente.RFC}";
                label11.Text = $"Domicilio: {cliente.Calle} #{cliente.Numero}, {cliente.Colonia}, CP {cliente.CP}";

                //Llenar la tabla de boletos comprados
                var detalles = _ventaService.ObtenerDetalles(VentaContext.UltimoFolioVenta);
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = detalles;

                if (dataGridView1.Columns.Count >= 10)
                {
                    dataGridView1.Columns[0].DataPropertyName = "NombrePartido";
                    dataGridView1.Columns[1].DataPropertyName = "NombreEstadio";
                    dataGridView1.Columns[2].DataPropertyName = "NombreZona";
                    dataGridView1.Columns[3].DataPropertyName = "Fila";
                    dataGridView1.Columns[4].DataPropertyName = "NumeroAsiento";
                    dataGridView1.Columns[5].DataPropertyName = "PrecioUnitario";
                    dataGridView1.Columns[6].DataPropertyName = "Cantidad";
                    dataGridView1.Columns[7].DataPropertyName = "Subtotal";
                    dataGridView1.Columns[8].DataPropertyName = "Iva";
                    dataGridView1.Columns[9].DataPropertyName = "Total";

                    dataGridView1.Columns[5].DefaultCellStyle.Format = "C2";
                    dataGridView1.Columns[7].DefaultCellStyle.Format = "C2";
                    dataGridView1.Columns[8].DefaultCellStyle.Format = "C2";
                    dataGridView1.Columns[9].DefaultCellStyle.Format = "C2";
                }

                //Llenar Totales
                label12.Text = $"Subtotal: {venta.Subtotal:C2}";
                label13.Text = $"IVA (16%): {venta.Iva:C2}";
                label14.Text = $"TOTAL: {venta.Total:C2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (_facturaActual == null) return;

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Documento PDF (*.pdf)|*.pdf",
                FileName = $"Factura_{_facturaActual.FolioFactura}.pdf",
                Title = "Guardar Factura"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                //obtener datos frescos (no depender solo de los labels)
                var venta = _ventaService.ObtenerVenta(VentaContext.UltimoFolioVenta);
                var cliente = VentaContext.ClienteSeleccionado;
                var emisor = _emisorService.ObtenerEmisores().FirstOrDefault();
                var detalles = _ventaService.ObtenerDetalles(VentaContext.UltimoFolioVenta);

                //líneas de la factura
                var lineas = new List<string>();
                lineas.Add("==========================================================");
                lineas.Add("                         FACTURA                          ");
                lineas.Add("==========================================================");
                lineas.Add($"Folio:          {_facturaActual.FolioFactura}");
                lineas.Add($"Fecha:          {_facturaActual.FechaEmision:dd/MMM/yyyy}");
                lineas.Add($"Hora:           {_facturaActual.HoraEmision:hh\\:mm}");
                lineas.Add($"Estado:         {_facturaActual.EstadoFactura}");
                lineas.Add($"Metodo de Pago: {venta?.MetodoPago}");
                lineas.Add("----------------------------------------------------------");
                lineas.Add("EMISOR");
                lineas.Add($"Empresa:   {emisor?.RazonSocial}");
                lineas.Add($"RFC:       {emisor?.EmisorRfc}");
                lineas.Add($"Domicilio: {emisor?.DomicilioCompleto}");
                lineas.Add("----------------------------------------------------------");
                lineas.Add("RECEPTOR");
                lineas.Add($"Nombre:    {cliente?.Nombre} {cliente?.ApellidoPa} {cliente?.ApellidoMa}");
                lineas.Add($"RFC:       {cliente?.RFC}");
                lineas.Add($"Domicilio: {cliente?.Calle} #{cliente?.Numero}, {cliente?.Colonia}, CP {cliente?.CP}");
                lineas.Add("----------------------------------------------------------");
                lineas.Add("DETALLE");
                lineas.Add("Partido | Estadio | Zona | Asiento | Precio | Cant | Subtotal | IVA | Total");
                lineas.Add("----------------------------------------------------------");
                foreach (var d in detalles)
                {
                    lineas.Add($"{d.NombrePartido} | {d.NombreEstadio} | {d.NombreZona} | {d.Fila}-{d.NumeroAsiento} | {d.PrecioUnitario:C2} | {d.Cantidad} | {d.Subtotal:C2} | {d.Iva:C2} | {d.Total:C2}");
                }
                lineas.Add("----------------------------------------------------------");
                lineas.Add($"Subtotal: {venta?.Subtotal:C2}");
                lineas.Add($"IVA (16%): {venta?.Iva:C2}");
                lineas.Add($"TOTAL:    {venta?.Total:C2}");
                lineas.Add("==========================================================");

                //generar el PDF con generador
                PdfGenerator.GenerarPdf(sfd.FileName, lineas);

                MessageBox.Show("Factura exportada correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar la factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
