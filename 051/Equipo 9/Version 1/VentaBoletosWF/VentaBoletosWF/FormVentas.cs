using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.IO;

namespace VentaBoletosWF
{
    public partial class FormVentas : Form
    {
        

        DataTable dtDetalle = new DataTable();

        public FormVentas()
        {
            InitializeComponent();
        }

        

        private void FormVentas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarPartidos();
            CargarZonas();

            
            cmbZona.SelectedIndex = -1;

            
            cmbMetodoPago.Items.Add("Efectivo");
            cmbMetodoPago.Items.Add("Tarjeta");
            cmbMetodoPago.Items.Add("Transferencia");

            // COLUMNAS DEL DETALLE
            //dtDetalle.Columns.Add("IdPartido");
            //dtDetalle.Columns.Add("IdZona");
            //dtDetalle.Columns.Add("Cantidad");
            //dtDetalle.Columns.Add("Precio");
            //dtDetalle.Columns.Add("Subtotal");

            //dtDetalle.Columns.Add("Partido");
            //dtDetalle.Columns.Add("Zona");

            dtDetalle.Columns.Add("IdPartido");
            dtDetalle.Columns.Add("IdZona");

            dtDetalle.Columns.Add("Partido");
            dtDetalle.Columns.Add("Zona");

            dtDetalle.Columns.Add("Cantidad");
            dtDetalle.Columns.Add("Precio");
            dtDetalle.Columns.Add("Subtotal");

            




            dgvBoletos.DataSource = dtDetalle;

            dgvBoletos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            
            lblPrecio.Text = "0.00";
            lblSubtotal.Text = "0.00";
            lblIVA.Text = "0.00";
            lblTotal.Text = "0.00";
        }

        


        void CargarClientes()
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT 
                    IdCliente,
                    NombreCompleto
                  FROM Cliente", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbCliente.DataSource = dt;
                cmbCliente.DisplayMember = "NombreCompleto";
                cmbCliente.ValueMember = "IdCliente";
            }
        }

        


        void CargarPartidos()
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT 
                    IdPartido,
                    EquipoLocal + ' vs ' + EquipoVisitante AS Partido
                  FROM Partido", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbPartido.DataSource = dt;
                cmbPartido.DisplayMember = "Partido";
                cmbPartido.ValueMember = "IdPartido";
            }
        }

       

        void CargarZonas()
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT 
                    IdZona,
                    NombreZona
                  FROM Zona", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbZona.DataSource = dt;
                cmbZona.DisplayMember = "NombreZona";
                cmbZona.ValueMember = "IdZona";
            }
        }

        


        private void cmbZona_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            
            if (cmbZona.SelectedValue == null)
                return;

            
            if (cmbZona.SelectedValue is DataRowView)
                return;

            using (SqlConnection con = Conexion.conectar())
            {
                SqlCommand cmd = new SqlCommand(
                @"SELECT PrecioBase
                  FROM Zona
                  WHERE IdZona = @IdZona", con);

                cmd.Parameters.AddWithValue(
                    "@IdZona",
                    Convert.ToInt32(
                        cmbZona.SelectedValue));

                con.Open();

                object precio =
                    cmd.ExecuteScalar();

                if (precio != null)
                {
                    lblPrecio.Text =
                        Convert.ToDecimal(precio)
                        .ToString("0.00");
                }
            }
        }

        

        int ObtenerDisponibles(int idZona)
        {
            int disponibles = 0;

            using (SqlConnection con = Conexion.conectar())
            {
                SqlCommand cmd = new SqlCommand(
                @"
                SELECT
                    Z.Cupo -
                    ISNULL(SUM(DV.Cantidad),0)
                    AS Disponibles
                FROM Zona Z
                LEFT JOIN DetalleVenta DV
                ON Z.IdZona = DV.IdZona
                WHERE Z.IdZona = @IdZona
                GROUP BY Z.Cupo
                ", con);

                cmd.Parameters.AddWithValue(
                    "@IdZona",
                    idZona);

                con.Open();

                object resultado =
                    cmd.ExecuteScalar();

                if (resultado != null)
                {
                    disponibles =
                        Convert.ToInt32(resultado);
                }
            }

            return disponibles;
        }




        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbPartido.SelectedValue == null || cmbZona.SelectedValue == null) return;

            int cantidad = (int)numCantidad.Value;

            // REQUERIMIENTO COMPLETO: Limitar a 4 boletos por transacción
            if (cantidad > 4)
            {
                MessageBox.Show("Se permite un máximo de 4 boletos por transacción.");
                return;
            }

            int disponibles = ObtenerDisponibles(Convert.ToInt32(cmbZona.SelectedValue));
            if (cantidad > disponibles)
            {
                MessageBox.Show("No hay suficientes boletos disponibles");
                return;
            }

            decimal precio = Convert.ToDecimal(lblPrecio.Text);
            decimal subtotal = cantidad * precio;

            // REQUERIMIENTO COMPLETO: Asignar Fila y Número de Asiento de forma obligatoria
            // Simulamos la asignación de un asiento libre (Fila A, número aleatorio para fines prácticos)
            string filaAsiento = "A";
            int numeroAsiento = new Random().Next(1, 20);

            dtDetalle.Rows.Add(
                cmbPartido.SelectedValue,
                cmbZona.SelectedValue,
                cmbPartido.Text,
                cmbZona.Text + " (Asiento: " + filaAsiento + "-" + numeroAsiento + ")", // Muestra el asiento en el Grid
                cantidad,
                precio,
                subtotal
            );

            CalcularTotales();
        }




        void CalcularTotales()
        {
            decimal subtotal = 0;

            foreach (DataRow row in dtDetalle.Rows)
            {
                subtotal +=
                    Convert.ToDecimal(
                        row["Subtotal"]);
            }

            decimal iva =
                subtotal * 0.16m;

            decimal total =
                subtotal + iva;

            lblSubtotal.Text =
                subtotal.ToString("0.00");

            lblIVA.Text =
                iva.ToString("0.00");

            lblTotal.Text =
                total.ToString("0.00");
        }




        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Selecciona cliente");
                return;
            }

            if (dtDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Agrega boletos");
                return;
            }

            if (cmbMetodoPago.Text == "")
            {
                MessageBox.Show("Selecciona método de pago");
                return;
            }


            if (cmbMetodoPago.Text == "Tarjeta")
            {
                // Supongamos que tienes los componentes de texto agregados en tu interfaz gráfica
                if (string.IsNullOrWhiteSpace(txtTitular.Text) || txtNumTarjeta.Text.Length != 16 || txtCCV.Text.Length != 3)
                {
                    MessageBox.Show("Datos de tarjeta inválidos. Verifique el número (16 dígitos) y CCV (3 dígitos).");
                    return;
                }
            }




            // Usamos un bloque try-catch para capturar las nuevas validaciones estrictas del Trigger de SQL
            try
            {
                using (SqlConnection con = Conexion.conectar())
                {
                    con.Open();

                    SqlCommand cmdVenta = new SqlCommand("sp_InsertarVenta", con);
                    cmdVenta.CommandType = CommandType.StoredProcedure;

                    cmdVenta.Parameters.AddWithValue("@IdCliente", Convert.ToInt32(cmbCliente.SelectedValue));
                    cmdVenta.Parameters.AddWithValue("@IdUsuario", 1); // Aquí puedes pasar el ID del usuario logueado
                    cmdVenta.Parameters.AddWithValue("@MetodoPago", cmbMetodoPago.Text);
                    cmdVenta.Parameters.AddWithValue("@Subtotal", Convert.ToDecimal(lblSubtotal.Text));

                    // ¡CORREGIDO!: Se eliminaron los parámetros de IVA y Total porque el nuevo SP los calcula internamente.

                    SqlParameter outId = new SqlParameter("@IdVenta", SqlDbType.Int);
                    outId.Direction = ParameterDirection.Output;
                    cmdVenta.Parameters.Add(outId);

                    cmdVenta.ExecuteNonQuery();

                    int idVenta = Convert.ToInt32(outId.Value);

                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        SqlCommand cmdDetalle = new SqlCommand("sp_InsertarDetalleVenta", con);
                        cmdDetalle.CommandType = CommandType.StoredProcedure;

                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                        cmdDetalle.Parameters.AddWithValue("@IdPartido", row["IdPartido"]);
                        cmdDetalle.Parameters.AddWithValue("@IdZona", row["IdZona"]);
                        // Cambiado de DBNull.Value para cumplir la restricción NOT NULL de tu BD original
                        cmdDetalle.Parameters.AddWithValue("@IdAsiento", 1);
                        
                        cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", row["Precio"]);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", row["Cantidad"]);

                        cmdDetalle.ExecuteNonQuery();
                    }

                    MessageBox.Show("Venta registrada correctamente\n\nID Venta: " + idVenta);
                    MessageBox.Show("Entrando al PDF");

                    GenerarTicketPDF(idVenta);
                    LimpiarVenta();
                }
            }
            catch (SqlException ex)
            {
                // Esto atrapará los THROW personalizados del trigger si se agotan los boletos en tiempo real
                MessageBox.Show("Error de Base de Datos: " + ex.Message, "Validación del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message);
            }
        }




        void LimpiarVenta()
        {
            dtDetalle.Clear();

            dgvBoletos.DataSource = null;
            dgvBoletos.DataSource = dtDetalle;

            lblPrecio.Text = "0.00";
            lblSubtotal.Text = "0.00";
            lblIVA.Text = "0.00";
            lblTotal.Text = "0.00";

            numCantidad.Value = 1;

            cmbMetodoPago.SelectedIndex = -1;
        }


        void GenerarTicketPDF(int idVenta)
        {
            
            string carpeta =
                Environment.GetFolderPath(
                    Environment.SpecialFolder.Desktop);

            string ruta =
                carpeta + "\\TicketVenta_" +
                idVenta + ".pdf";

            
            Document doc =
                new Document(PageSize.A4);

            PdfWriter.GetInstance(
                doc,
                new FileStream(
                    ruta,
                    FileMode.Create));

            doc.Open();

            
            Paragraph titulo =
                new Paragraph(
                    "TICKET DE COMPRA");

            titulo.Alignment = Element.ALIGN_CENTER;

            doc.Add(titulo);

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(
                "==================================="));

            
            doc.Add(new Paragraph(
                "TICKET: #" + idVenta));

            doc.Add(new Paragraph(
                "Cliente: " +
                cmbCliente.Text));

            doc.Add(new Paragraph(
                "Método Pago: " +
                cmbMetodoPago.Text));

            doc.Add(new Paragraph(
                "Fecha: " +
                DateTime.Now.ToString()));

            doc.Add(new Paragraph(" "));

            
            PdfPTable tabla =
                new PdfPTable(5);

            tabla.WidthPercentage = 100;

            tabla.AddCell("Partido");
            tabla.AddCell("Zona");
            tabla.AddCell("Cantidad");
            tabla.AddCell("Precio");
            tabla.AddCell("Subtotal");

            
            foreach (DataRow row in dtDetalle.Rows)
            {
                //tabla.AddCell(
                //    row["IdPartido"].ToString());

                //tabla.AddCell(
                //    row["IdZona"].ToString());
                tabla.AddCell(
                    row["Partido"].ToString());

                tabla.AddCell(
                    row["Zona"].ToString());

                tabla.AddCell(
                    row["Cantidad"].ToString());

                tabla.AddCell(
                    row["Precio"].ToString());

                tabla.AddCell(
                    row["Subtotal"].ToString());
            }

            doc.Add(tabla);

            doc.Add(new Paragraph(" "));

            
            doc.Add(new Paragraph(
                "Subtotal: $" +
                lblSubtotal.Text));

            doc.Add(new Paragraph(
                "IVA: $" +
                lblIVA.Text));

            doc.Add(new Paragraph(
                "TOTAL: $" +
                lblTotal.Text));

            doc.Add(new Paragraph(
                "==================================="));

            doc.Add(new Paragraph(
                "Gracias por tu compra"));

            // VALIDACIÓN OBLIGATORIA 4: Formato de fecha "dd-MMM-yyyy" sin hora
            string fechaTicket = DateTime.Now.ToString("dd-MMM-yyyy", new System.Globalization.CultureInfo("es-MX"));

            doc.Add(new Paragraph(
                "Fecha: " + fechaTicket)); // Imprimirá por ejemplo: 10-Nov-2026


            doc.Close();

            MessageBox.Show(
                "Ticket PDF generado en escritorio");
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
