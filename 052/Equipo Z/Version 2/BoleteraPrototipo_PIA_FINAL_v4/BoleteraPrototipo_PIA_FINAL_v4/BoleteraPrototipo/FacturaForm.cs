using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Consulta de facturas (una por venta) y exportación a PDF
    public class FacturaForm : Form
    {
        readonly VentaRepositorio ventas = new VentaRepositorio();
        TextBox txtFolio;
        DataGridView dgvVentas, dgvDetalle;
        Label lblComprobante, lblEmisor, lblReceptor, lblTotales;
        Button btnPdf;
        int idVentaElegida = 0;
        string folioElegido = "";

        public FacturaForm()
        {
            Ui.PrepararForm(this, "Facturación", "Facturación", 1000, 640);

            GroupBox g = Ui.Grupo(this, "Ventas (busca por folio)", 12, 70, 976, 200);
            Ui.Etiqueta(g, "Folio (puede ser una parte)", 12, 22);
            txtFolio = Ui.Texto(g, 12, 42, 380, 36);
            txtFolio.KeyDown += delegate (object s, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Buscar(); }
            };
            Ui.Boton(g, "Buscar", 402, 40, 100, 27, Ui.Azul, delegate { Buscar(); });
            Ui.Boton(g, "Ver todas", 510, 40, 100, 27, Ui.Gris, delegate { txtFolio.Clear(); Buscar(); });
            dgvVentas = Ui.Grid(g, 12, 74, 950, 118);
            dgvVentas.CellClick += dgvVentas_CellClick;

            GroupBox f = Ui.Grupo(this, "Factura", 12, 278, 976, 352);
            lblComprobante = Ui.Etiqueta(f, "Elige una venta para ver su factura.", 12, 24);
            lblComprobante.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblComprobante.ForeColor = Ui.Navy;
            lblEmisor = Ui.Etiqueta(f, "", 12, 50);
            lblReceptor = Ui.Etiqueta(f, "", 490, 50);
            dgvDetalle = Ui.Grid(f, 12, 134, 950, 130);
            lblTotales = Ui.Etiqueta(f, "", 600, 272);
            lblTotales.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPdf = Ui.Boton(f, "Exportar a PDF", 12, 300, 200, 40, Ui.Verde, btnPdf_Click);
            btnPdf.Enabled = false;
            Ui.Boton(f, "Cerrar", 222, 300, 120, 40, Ui.Gris, delegate { Close(); });

            Load += delegate
            {
                if (!Sesion.EsAdmin) { Tema.Aviso("Solo un administrador puede consultar la facturación.", "Facturación"); Close(); return; }
                Buscar();
            };
        }

        void Buscar()
        {
            try
            {
                dgvVentas.DataSource = ventas.Listar(txtFolio.Text);
                Tema.FormatoGrid(dgvVentas);
            }
            catch (Exception ex) { Tema.Manejar(ex, "Facturación"); }
        }

        void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvVentas.Rows[e.RowIndex];
            idVentaElegida = Convert.ToInt32(fila.Cells["ID"].Value);
            folioElegido = Convert.ToString(fila.Cells["Folio"].Value);
            try
            {
                DataSet ds = ventas.Factura(idVentaElegida);
                DataRow h = ds.Tables[0].Rows[0];
                string hora = Convert.ToString(h["hora_emision"]);
                if (hora.Length >= 5) hora = hora.Substring(0, 5);
                lblComprobante.Text = "Factura " + Convert.ToString(h["folio_factura"]) + "   |   " + Convert.ToString(h["estado"]).ToUpper();
                lblEmisor.Text = "EMISOR\n" + Convert.ToString(h["nombre_emisor"]) + "\nRFC: " + Convert.ToString(h["rfc_emisor"])
                    + "\nDomicilio: " + Convert.ToString(h["domicilio_emisor"]);
                lblReceptor.Text = "RECEPTOR\n" + Convert.ToString(h["nombre_receptor"]) + "\nRFC: " + Convert.ToString(h["rfc_receptor"])
                    + "\nDomicilio: " + Convert.ToString(h["domicilio_receptor"])
                    + "\nEmisión: " + Formato.Fecha(Convert.ToDateTime(h["fecha_emision"])) + " " + hora
                    + "   Pago: " + Convert.ToString(h["metodo_pago"]);
                dgvDetalle.DataSource = ds.Tables[1];
                Tema.FormatoGrid(dgvDetalle);
                lblTotales.Text = "Subtotal " + Formato.Dinero(Convert.ToDecimal(h["subtotal"])) + "    IVA " + Formato.Dinero(Convert.ToDecimal(h["iva"]))
                    + "    TOTAL " + Formato.Dinero(Convert.ToDecimal(h["total"]));
                btnPdf.Enabled = true;
            }
            catch (Exception ex)
            {
                btnPdf.Enabled = false;
                Tema.Manejar(ex, "Facturación");
            }
        }

        void btnPdf_Click(object sender, EventArgs e)
        {
            if (idVentaElegida == 0) { Tema.Aviso("Primero elige una venta.", "Facturación"); return; }
            FacturaPdf.ExportarDeVenta(idVentaElegida, folioElegido, this);
        }
    }
}
