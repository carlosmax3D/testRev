
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

// Bibliotecas para generar documentos PDF.
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Pantalla_ComprobanteFactura_PIA_PA
{
    // Define la ventana encargada de mostrar los comprobantes de factura.
    public partial class pantalla_comprobanteFactura : Form
    {
        // Guarda el número de folio que se desea consultar.
        private int Folio { get; set; }

        // Conserva la información de la factura consultada.
        private DataTable tablaFacturaActual = new DataTable();

        // Constructor sin parámetros para mantener compatibilidad
        // con el diseñador de Windows Forms.
        public pantalla_comprobanteFactura()
        {
            InitializeComponent();
            Folio = 0;
        }

        // Constructor que recibe el folio desde la pantalla anterior.
        public pantalla_comprobanteFactura(int folio) : this()
        {
            Folio = folio;
        }

        // CARGAR FACTURA AL ABRIR LA PANTALLA.
        private void pantalla_comprobanteFactura_Load(
            object sender, EventArgs e)
        {
            if (Folio <= 0)
            {
                MessageBox.Show(
                    "No se recibió un número de folio válido.",
                    "Folio no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Consulta únicamente la factura del folio recibido.
            CargarFacturas(Folio);
        }

        // CONSULTAR FACTURA EN SQL SERVER.
        private void CargarFacturas(int folio)
        {
            try
            {
                // Obtener la información del comprobante desde la base de datos.
                tablaFacturaActual = Vistas.ObtenerComprobanteFactura(folio);

                // Verificar que se hayan obtenido registros.
                if (tablaFacturaActual == null || tablaFacturaActual.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontró información del comprobante para el folio de venta "
                        + folio + ".\n\nVerifica que exista una factura registrada para esa venta.",
                        "Factura no encontrada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    datagridview1_comprobanteFactura.DataSource = null;
                    return;
                }

                // Mostrar la información obtenida en el DataGridView.
                datagridview1_comprobanteFactura.DataSource = tablaFacturaActual;

                // Configurar el DataGridView.
                datagridview1_comprobanteFactura.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                datagridview1_comprobanteFactura.ReadOnly = true;

                datagridview1_comprobanteFactura.AllowUserToAddRows = false;

                datagridview1_comprobanteFactura.AllowUserToDeleteRows = false;

                datagridview1_comprobanteFactura.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                datagridview1_comprobanteFactura.MultiSelect = false;

                datagridview1_comprobanteFactura.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el comprobante de factura:\n\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta cuando se hace clic en el contenido de una celda.
        // Actualmente no realiza ninguna acción.
        private void datagridview1_comprobanteFactura_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        // CERRAR Y VOLVER AL MENÚ FACTURA.
        private void atras_ComprobanteFactura_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // EVENTO PARA GENERAR EL COMPROBANTE EN PDF.
        private void generarPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerarPDF();
        }

        // MÉTODO PARA EXPORTAR LA FACTURA A PDF.
        private void GenerarPDF()
        {
            try
            {
                // Comprueba que exista información para exportar.
                if (tablaFacturaActual == null ||
                    tablaFacturaActual.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay información de una factura para exportar.",
                        "Sin información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Obtiene la primera fila para los datos generales.
                DataRow factura = tablaFacturaActual.Rows[0];

                // Permite elegir dónde guardar el documento.
                using (SaveFileDialog guardar = new SaveFileDialog())
                {
                    guardar.Title = "Guardar comprobante de factura";
                    guardar.Filter = "Documento PDF (*.pdf)|*.pdf";
                    guardar.FileName = "Comprobante_Folio_" +
                                       Folio + ".pdf";

                    guardar.DefaultExt = "pdf";
                    guardar.AddExtension = true;

                    if (guardar.ShowDialog() != DialogResult.OK)
                        return;

                    // Configura las fuentes del documento.
                    PdfFont fuenteNormal =
                        PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    PdfFont fuenteNegrita =
                        PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    CultureInfo cultura =
                        CultureInfo.GetCultureInfo("es-MX");

                    // Configura el documento PDF.
                    using (PdfWriter escritor =
                           new PdfWriter(guardar.FileName))
                    using (PdfDocument pdf =
                           new PdfDocument(escritor))
                    using (Document documento =
                           new Document(pdf))
                    {
                        documento.SetMargins(40, 40, 40, 40);

                        // Título del comprobante.
                        documento.Add(
                            new Paragraph("COMPROBANTE DE VENTA")
                                .SetFont(fuenteNegrita)
                                .SetFontSize(20)
                                .SetTextAlignment(TextAlignment.CENTER));

                        documento.Add(
                            new Paragraph("EstadioPass")
                                .SetFont(fuenteNormal)
                                .SetFontSize(12)
                                .SetTextAlignment(TextAlignment.CENTER));

                        documento.Add(
                            new Paragraph(
                                "Folio de venta: " +
                                ObtenerTexto(factura, "Folio_Venta"))
                                .SetFont(fuenteNegrita)
                                .SetFontSize(14)
                                .SetTextAlignment(TextAlignment.CENTER));

                        documento.Add(new Paragraph("\n"));

                        // Tabla con la información general del comprobante.
                        Table tabla = new Table(
                            UnitValue.CreatePercentArray(
                                new float[] { 40, 60 }))
                            .UseAllAvailableWidth();

                        tabla.AddHeaderCell(
                            new Cell()
                                .Add(new Paragraph("CONCEPTO")
                                    .SetFont(fuenteNegrita))
                                .SetBackgroundColor(ColorConstants.LIGHT_GRAY));

                        tabla.AddHeaderCell(
                            new Cell()
                                .Add(new Paragraph("INFORMACIÓN")
                                    .SetFont(fuenteNegrita))
                                .SetBackgroundColor(ColorConstants.LIGHT_GRAY));

                        // Datos del emisor.
                        AgregarFila(tabla, "Nombre del emisor",
                            ObtenerTexto(factura, "Nombre_Emisor"),
                            fuenteNormal);

                        AgregarFila(tabla, "RFC del emisor",
                            ObtenerTexto(factura, "RFC_Emisor"),
                            fuenteNormal);

                        AgregarFila(tabla, "Domicilio del emisor",
                            ObtenerTexto(factura, "Calle_Emisor") + " " +
                            ObtenerTexto(factura, "Numero_Emisor") + ", " +
                            ObtenerTexto(factura, "Colonia_Emisor") + ", " +
                            ObtenerTexto(factura, "Municipio_Emisor"),
                            fuenteNormal);

                        // Datos del cliente.
                        AgregarFila(tabla, "Nombre del cliente",
                            ObtenerTexto(factura, "Nombre_Cliente"),
                            fuenteNormal);

                        AgregarFila(tabla, "RFC del cliente",
                            ObtenerTexto(factura, "RFC_Cliente"),
                            fuenteNormal);

                        AgregarFila(tabla, "Domicilio del cliente",
                            ObtenerTexto(factura, "Calle_Cliente") + " " +
                            ObtenerTexto(factura, "Numero_Cliente") + ", " +
                            ObtenerTexto(factura, "Colonia_Cliente") + ", " +
                            ObtenerTexto(factura, "Ciudad_Cliente") + ", " +
                            ObtenerTexto(factura, "Municipio_Cliente"),
                            fuenteNormal);

                        // Datos del comprobante.
                        AgregarFila(tabla, "Folio de factura",
                            ObtenerTexto(factura, "Folio_Factura"),
                            fuenteNormal);

                        AgregarFila(tabla, "Folio de venta",
                            ObtenerTexto(factura, "Folio_Venta"),
                            fuenteNormal);

                        // Se obtiene únicamente la fecha, sin incluir la hora.
                        AgregarFila(tabla, "Fecha de emisión",
                            ObtenerFecha(factura, "Fecha_Emision"),
                            fuenteNormal);

                        AgregarFila(tabla, "Hora de emisión",
                            ObtenerTexto(factura, "Hora_Emision"),
                            fuenteNormal);

                        AgregarFila(tabla, "Método de pago",
                            ObtenerTexto(factura, "Metodo_de_Pago"),
                            fuenteNormal);

                        AgregarFila(tabla, "Total de boletos",
                            ObtenerTexto(factura, "Total_de_Boletos"),
                            fuenteNormal);

                        // Datos del partido y estadio.
                        AgregarFila(tabla, "Partido",
                            ObtenerTexto(factura, "Equipo_Local") +
                            " vs " +
                            ObtenerTexto(factura, "Equipo_Visitante"),
                            fuenteNormal);

                        AgregarFila(tabla, "Estadio",
                            ObtenerTexto(factura, "Nombre_Estadio"),
                            fuenteNormal);

                        AgregarFila(tabla, "Zona",
                            ObtenerTexto(factura, "Nombre_Zona"),
                            fuenteNormal);

                        documento.Add(tabla);

                        documento.Add(new Paragraph("\n"));

                        // Tabla con los asientos correspondientes a la venta.
                        documento.Add(
                            new Paragraph("DETALLE DE BOLETOS")
                                .SetFont(fuenteNegrita)
                                .SetFontSize(13));

                        Table tablaBoletos = new Table(
                            UnitValue.CreatePercentArray(
                                new float[] { 25, 25, 15, 15, 20 }))
                            .UseAllAvailableWidth();

                        tablaBoletos.AddHeaderCell(
                            new Cell().Add(
                                new Paragraph("Partido").SetFont(fuenteNegrita)));

                        tablaBoletos.AddHeaderCell(
                            new Cell().Add(
                                new Paragraph("Zona").SetFont(fuenteNegrita)));

                        tablaBoletos.AddHeaderCell(
                            new Cell().Add(
                                new Paragraph("Fila").SetFont(fuenteNegrita)));

                        tablaBoletos.AddHeaderCell(
                            new Cell().Add(
                                new Paragraph("Asiento").SetFont(fuenteNegrita)));

                        tablaBoletos.AddHeaderCell(
                            new Cell().Add(
                                new Paragraph("Precio").SetFont(fuenteNegrita)));

                        // Agrega todos los asientos de la venta.
                        foreach (DataRow fila in tablaFacturaActual.Rows)
                        {
                            string partido =
                                ObtenerTexto(fila, "Equipo_Local") +
                                " vs " +
                                ObtenerTexto(fila, "Equipo_Visitante");

                            tablaBoletos.AddCell(
                                new Paragraph(partido).SetFont(fuenteNormal));

                            tablaBoletos.AddCell(
                                new Paragraph(
                                    ObtenerTexto(fila, "Nombre_Zona"))
                                    .SetFont(fuenteNormal));

                            tablaBoletos.AddCell(
                                new Paragraph(
                                    ObtenerTexto(fila, "Fila_Asiento"))
                                    .SetFont(fuenteNormal));

                            tablaBoletos.AddCell(
                                new Paragraph(
                                    ObtenerTexto(fila, "Num_Asiento"))
                                    .SetFont(fuenteNormal));

                            tablaBoletos.AddCell(
                                new Paragraph(
                                    ObtenerImporte(fila, "Precio_Unitario", cultura))
                                    .SetFont(fuenteNormal));
                        }

                        documento.Add(tablaBoletos);

                        documento.Add(new Paragraph("\n"));

                        // Tabla de importes.
                        Table tablaImportes = new Table(
                            UnitValue.CreatePercentArray(
                                new float[] { 40, 60 }))
                            .UseAllAvailableWidth();

                        AgregarFila(tablaImportes, "Subtotal",
                            ObtenerImporte(factura, "Subtotal", cultura),
                            fuenteNormal);

                        AgregarFila(tablaImportes, "IVA (16%)",
                            ObtenerImporte(factura, "IVA", cultura),
                            fuenteNormal);

                        AgregarFila(tablaImportes, "TOTAL DE LA VENTA",
                            ObtenerImporte(factura, "Total_de_Venta", cultura),
                            fuenteNegrita);

                        documento.Add(tablaImportes);

                        documento.Add(new Paragraph("\n"));

                        // Mensaje al final del comprobante.
                        documento.Add(
                            new Paragraph(
                                "Este documento corresponde a la información " +
                                "de la venta registrada en el sistema.")
                                .SetFont(fuenteNormal)
                                .SetFontSize(9)
                                .SetTextAlignment(TextAlignment.CENTER));
                    }

                    MessageBox.Show(
                        "El comprobante PDF se generó correctamente.\n\n" +
                        guardar.FileName,
                        "PDF generado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible generar el documento PDF.\n\n" +
                    ex.Message,
                    "Error al generar PDF",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // AGREGA UNA FILA A LA TABLA DEL PDF.
        private void AgregarFila(
            Table tabla,
            string concepto,
            string informacion,
            PdfFont fuente)
        {
            tabla.AddCell(
                new Cell().Add(
                    new Paragraph(concepto).SetFont(fuente)));

            tabla.AddCell(
                new Cell().Add(
                    new Paragraph(informacion).SetFont(fuente)));
        }

        // OBTIENE EL TEXTO DE UNA COLUMNA.
        private string ObtenerTexto(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna))
                return "No disponible";

            if (fila[columna] == DBNull.Value)
                return "No disponible";

            return Convert.ToString(fila[columna]);
        }

        // OBTIENE UNA FECHA SIN MOSTRAR LA HORA.
        private string ObtenerFecha(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna))
                return "No disponible";

            if (fila[columna] == DBNull.Value)
                return "No disponible";

            return Convert.ToDateTime(fila[columna])
                .ToString("dd/MM/yyyy");
        }

        // OBTIENE UN IMPORTE CON FORMATO MONETARIO.
        private string ObtenerImporte(
            DataRow fila,
            string columna,
            CultureInfo cultura)
        {
            if (!fila.Table.Columns.Contains(columna))
                return "$0.00";

            if (fila[columna] == DBNull.Value)
                return "$0.00";

            decimal importe = Convert.ToDecimal(fila[columna]);

            return importe.ToString("C2", cultura);
        }
    }
}
