using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace VentaBoletosWF
{
    public partial class FormFacturacion : Form
    {
        public FormFacturacion()
        {
            InitializeComponent();
        }

        private void FormFacturacion_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtIdVenta.Text == "")
            {
                MessageBox.Show(
                    "Ingresa ID Venta");

                return;
            }

            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlCommand cmd =
                    new SqlCommand(
                    @"
            SELECT
                V.IdVenta,
                C.NombreCompleto,
                V.Total,
                V.Estado
            FROM Venta V

            JOIN Cliente C
            ON V.IdCliente = C.IdCliente

            WHERE V.IdVenta = @IdVenta
            ", con);

                cmd.Parameters.AddWithValue(
                    "@IdVenta",
                    txtIdVenta.Text);

                con.Open();

                SqlDataReader dr =
                    cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblCliente.Text =
                        dr["NombreCompleto"]
                        .ToString();

                    lblTotal.Text =
                        dr["Total"]
                        .ToString();

                    lblEstado.Text =
                        dr["Estado"]
                        .ToString();
                }
                else
                {
                    MessageBox.Show(
                        "Venta no encontrada");
                }
            }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            //if (!ValidarRFC())
            //    return;

            //if (txtNombre.Text == "")
            //{
            //    MessageBox.Show(
            //        "Nombre requerido");

            //    return;
            //}

            ///
            if (txtRFC.Text.Length < 12)
            {
                MessageBox.Show(
                    "RFC inválido");

                return;
            }
            ///

            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlCommand cmd =
                    new SqlCommand(
                    "sp_InsertarFactura",
                    con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@IdVenta",
                    txtIdVenta.Text);

                cmd.Parameters.AddWithValue(
                    "@RFCReceptor",
                    txtRFC.Text);

                cmd.Parameters.AddWithValue(
                    "@NombreReceptor",
                    txtNombre.Text);

                cmd.Parameters.AddWithValue(
                    "@DomicilioReceptor",
                    txtDomicilio.Text);

                con.Open();

                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "Factura generada");

                GenerarPDF();

                Limpiar();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }






        bool ValidarRFC()
        {
            if (txtRFC.Text.Length < 12)
            {
                MessageBox.Show(
                    "RFC inválido");

                return false;
            }

            return true;
        }

        bool ValidarCampos()
        {
            if (txtRFC.Text == "")
            {
                MessageBox.Show(
                    "Ingresa RFC");

                return false;
            }

            if (txtNombre.Text == "")
            {
                MessageBox.Show(
                    "Ingresa nombre");

                return false;
            }

            if (txtDomicilio.Text == "")
            {
                MessageBox.Show(
                    "Ingresa domicilio");

                return false;
            }

            return true;
        }


        void GenerarPDF()
        {
            Document doc =
                new Document();

            string ruta =
                Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop)
                + "/Factura.pdf";

            PdfWriter.GetInstance(
                doc,
                new FileStream(
                    ruta,
                    FileMode.Create));

            doc.Open();

            doc.Add(
                new Paragraph(
                    "FACTURA"));

            doc.Add(
                new Paragraph(
                    "--------------------------------"));

            doc.Add(
                new Paragraph(
                    "ID Venta: "
                    + txtIdVenta.Text));

            doc.Add(
                new Paragraph(
                    "Cliente: "
                    + lblCliente.Text));

            doc.Add(
                new Paragraph(
                    "RFC: "
                    + txtRFC.Text));

            doc.Add(
                new Paragraph(
                    "Nombre Fiscal: "
                    + txtNombre.Text));

            doc.Add(
                new Paragraph(
                    "Domicilio: "
                    + txtDomicilio.Text));

            doc.Add(
                new Paragraph(
                    "Total: $"
                    + lblTotal.Text));

            doc.Close();

            MessageBox.Show(
                "PDF generado en escritorio");
        }

        void Limpiar()
        {
            txtIdVenta.Clear();

            txtRFC.Clear();

            txtNombre.Clear();

            txtDomicilio.Clear();

            lblCliente.Text = "";

            lblTotal.Text = "";

            lblEstado.Text = "";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
