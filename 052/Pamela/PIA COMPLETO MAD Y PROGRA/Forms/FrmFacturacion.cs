using System.Data;
using System.Diagnostics;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>
    /// Facturación: consulta la factura que se generó al realizar una compra
    /// (emisor, receptor, comprobante y detalle) y la exporta a PDF.
    /// </summary>
    public partial class FrmFacturacion : Form
    {
        private readonly IVentaService _ventaService;
        private DataRow? _encabezado;
        private DataTable? _detalle;

        public FrmFacturacion(IVentaService ventaService)
        {
            InitializeComponent();

            _ventaService = ventaService;

            Text = "Facturación";
            txtBuscFlo.Text = "Folio de compra:";
            tbBuscFlo.MaxLength = 50;
            tbBuscFlo.PlaceholderText = "Código de compra (GUID) o folio de factura (FAC-...)";
            AcceptButton = botonBuscFlo;

            UiHelper.ConfigurarGrid(
                dataGridView1,
                true,
                ("Zona", "Zona", TipoColumna.Texto),
                ("Asiento", "Asiento", TipoColumna.Texto),
                ("PrecioUnitario", "Precio unitario", TipoColumna.Moneda),
                ("Cantidad", "Cantidad", TipoColumna.Entero),
                ("Importe", "Importe", TipoColumna.Moneda));

            Limpiar();

            botonBuscFlo.Click += botonBuscFlo_Click;
            botonExpPDF.Click += botonExpPDF_Click;
            botonRegFact.Click += botonRegFact_Click;
        }

        /// <summary>Abre la pantalla mostrando directamente la factura de una compra.</summary>
        public void CargarFolio(string folioCompra)
        {
            tbBuscFlo.Text = folioCompra;
            Buscar();
        }

        private void Limpiar()
        {
            _encabezado = null;
            _detalle = null;

            valNomEmp.Text = "-";
            valRFCEmp.Text = "-";
            valDomEmp.Text = "-";
            valNomCte.Text = "-";
            valRCFCte.Text = "-";
            valDomCte.Text = "-";
            valFloComp.Text = "-";
            valFechaHoraComp.Text = "-";
            valMetPagCom.Text = "-";
            valParDet.Text = "-";
            valEstCompra.Text = "-";
            valSubtotalFact.Text = Formato.Moneda(0);
            valIVAFact.Text = Formato.Moneda(0);
            valTotalFact.Text = Formato.Moneda(0);
            valFloComp.ForeColor = SystemColors.ControlText;
            dataGridView1.DataSource = null;
            botonExpPDF.Enabled = false;
        }

        private void botonBuscFlo_Click(object? sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            string folio = tbBuscFlo.Text.Trim();

            if (folio.Length == 0)
            {
                UiHelper.Advertir("Escribe el folio de la compra o el folio de la factura.", "Facturación");
                tbBuscFlo.Focus();
                return;
            }

            try
            {
                Limpiar();

                DataSet datos = _ventaService.ConsultarFactura(folio);

                if (datos.Tables.Count < 2 || datos.Tables[0].Rows.Count == 0)
                {
                    UiHelper.Informar("No se encontró ninguna factura con ese folio.", "Sin resultados");
                    return;
                }

                _encabezado = datos.Tables[0].Rows[0];
                _detalle = datos.Tables[1];

                bool cancelada = DatosHelper.Texto(_encabezado, "Estado") == "Cancelada";

                // Emisor
                valNomEmp.Text = DatosHelper.Texto(_encabezado, "NombreEmpresa");
                valRFCEmp.Text = DatosHelper.Texto(_encabezado, "RFCEmpresa");
                valDomEmp.Text = DatosHelper.Texto(_encabezado, "DomicilioEmpresa");

                // Receptor
                valNomCte.Text = DatosHelper.Texto(_encabezado, "NombreReceptor");
                valRCFCte.Text = DatosHelper.Texto(_encabezado, "RFCReceptor");
                valDomCte.Text = DatosHelper.Texto(_encabezado, "DomicilioReceptor");

                // Comprobante
                valFloComp.Text = DatosHelper.Texto(_encabezado, "FolioFactura") + (cancelada ? "   (FACTURA CANCELADA)" : "   (Vigente)");
                valFloComp.ForeColor = cancelada ? Color.Firebrick : SystemColors.ControlText;
                valFechaHoraComp.Text = Formato.FechaHora(DatosHelper.Fecha(_encabezado, "FechaHoraEmision"));
                valMetPagCom.Text = DatosHelper.Texto(_encabezado, "MetodoPago");

                // Detalle
                valParDet.Text = DatosHelper.Texto(_encabezado, "Partido") + "  -  " +
                                 Formato.FechaHora(DatosHelper.Fecha(_encabezado, "FechaPartido"));
                valEstCompra.Text = DatosHelper.Texto(_encabezado, "Estadio");
                dataGridView1.DataSource = _detalle;

                valSubtotalFact.Text = Formato.Moneda(DatosHelper.Numero(_encabezado, "Subtotal"));
                valIVAFact.Text = Formato.Moneda(DatosHelper.Numero(_encabezado, "IVA"));
                valTotalFact.Text = Formato.Moneda(DatosHelper.Numero(_encabezado, "Total"));

                botonExpPDF.Enabled = true;
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al consultar la factura");
            }
        }

        private void botonLimpiarFact_Click(object? sender, EventArgs e)
        {
            tbBuscFlo.Clear();
            Limpiar();
            tbBuscFlo.Focus();
        }

        private void botonExpPDF_Click(object? sender, EventArgs e)
        {
            if (_encabezado == null || _detalle == null)
            {
                UiHelper.Advertir("Primero busca la factura que quieres exportar.", "Exportar PDF");
                return;
            }

            using SaveFileDialog dialogo = new SaveFileDialog();

            dialogo.Title = "Guardar factura como PDF";
            dialogo.Filter = "Documento PDF (*.pdf)|*.pdf";
            dialogo.DefaultExt = "pdf";
            dialogo.AddExtension = true;
            dialogo.OverwritePrompt = true;
            dialogo.FileName = "Factura_" + DatosHelper.Texto(_encabezado, "FolioFactura") + ".pdf";

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                FacturaPdf.Generar(dialogo.FileName, _encabezado, _detalle);
            }
            catch (IOException)
            {
                UiHelper.Advertir(
                    "No se pudo guardar el archivo. Si ya existe y está abierto en otro programa, ciérralo e inténtalo de nuevo.",
                    "Exportar PDF");
                return;
            }
            catch (UnauthorizedAccessException)
            {
                UiHelper.Advertir("No tienes permiso para guardar en esa carpeta. Elige otra ubicación.", "Exportar PDF");
                return;
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo exportar la factura");
                return;
            }

            if (UiHelper.Confirmar(
                    "La factura se exportó correctamente en:\n" + dialogo.FileName + "\n\n¿Deseas abrir el archivo?",
                    "Factura exportada"))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(dialogo.FileName) { UseShellExecute = true });
                }
                catch (Exception)
                {
                    UiHelper.Advertir("El archivo se guardó, pero no hay un programa instalado para abrir archivos PDF.", "Exportar PDF");
                }
            }
        }

        private void botonRegFact_Click(object? sender, EventArgs e)
        {
            Close();
        }

        // Eventos vacíos que el diseñador dejó enlazados
        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void txtDomEmp_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
        }
    }
}
