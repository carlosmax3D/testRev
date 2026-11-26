using System;

using System.Data;

using System.Drawing;

using System.Windows.Forms;

using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;

namespace TemplateCSharp.Forms

{

    public partial class FrmFacturacion : Form

    {

        private string cadenaConexion;

        private DataGridView dgvFacturas;

        private Label lblTituloTabla;

        public FrmFacturacion()

        {

            InitializeComponent();

            CargarConfiguracion();

            CrearInterfaz();

            ConfigurarEventos();

            CargarFacturas();

        }

        private void CargarConfiguracion()

        {

            try

            {

                IConfiguration configuration =

                    new ConfigurationBuilder()

                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)

                    .AddJsonFile("appsettings.json", optional: false)

                    .Build();

                cadenaConexion =

                    configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrWhiteSpace(cadenaConexion))

                {

                    MessageBox.Show(

                        "No se encontró la cadena de conexión.",

                        "Configuración",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Error);

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al cargar la configuración: " + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private SqlConnection CrearConexion()

        {

            return new SqlConnection(cadenaConexion);

        }

        private void CrearInterfaz()

        {

            lblNombreUsuario.Text = "Buscar factura:";

            txtNombreUsuario.PlaceholderText =

                "Escribe el folio o nombre del cliente";

            iconButton1.Text = "Consultar";

            iconButton2.Text = "Limpiar";

            iconButton3.Text = "Ver detalle";

            iconButton1.IconChar =

                FontAwesome.Sharp.IconChar.Sync;

            iconButton2.IconChar =

                FontAwesome.Sharp.IconChar.Eraser;

            iconButton3.IconChar =

                FontAwesome.Sharp.IconChar.FileInvoice;

            lblTituloTabla = new Label();

            lblTituloTabla.Text = "Facturas registradas";

            lblTituloTabla.Font =

                new Font("Century Gothic", 13, FontStyle.Bold);

            lblTituloTabla.AutoSize = true;

            lblTituloTabla.Location = new Point(41, 405);

            dgvFacturas = new DataGridView();

            dgvFacturas.Location = new Point(41, 445);

            dgvFacturas.Size = new Size(1457, 400);

            dgvFacturas.Anchor =

                AnchorStyles.Top |

                AnchorStyles.Bottom |

                AnchorStyles.Left |

                AnchorStyles.Right;

            dgvFacturas.ReadOnly = true;

            dgvFacturas.AllowUserToAddRows = false;

            dgvFacturas.AllowUserToDeleteRows = false;

            dgvFacturas.SelectionMode =

                DataGridViewSelectionMode.FullRowSelect;

            dgvFacturas.MultiSelect = false;

            dgvFacturas.AutoSizeColumnsMode =

                DataGridViewAutoSizeColumnsMode.Fill;

            dgvFacturas.RowHeadersVisible = false;

            dgvFacturas.BackgroundColor = Color.White;

            dgvFacturas.BorderStyle = BorderStyle.None;

            dgvFacturas.AutoGenerateColumns = true;

            // Agregar los controles directamente al formulario.

            // Así la tabla no queda recortada por panel1.

            this.Controls.Add(lblTituloTabla);

            this.Controls.Add(dgvFacturas);

            lblTituloTabla.BringToFront();

            dgvFacturas.BringToFront();

        }

        private void ConfigurarEventos()

        {

            iconButton1.Click += Consultar_Click;

            iconButton2.Click += Limpiar_Click;

            iconButton3.Click += VerDetalle_Click;

            txtNombreUsuario.TextChanged +=

                TxtNombreUsuario_TextChanged;

            dgvFacturas.CellDoubleClick +=

                DgvFacturas_CellDoubleClick;

        }

        private void CargarFacturas()

        {

            if (string.IsNullOrWhiteSpace(cadenaConexion))

                return;

            try

            {

                using (SqlConnection conexion = CrearConexion())

                {

                    string consulta = @"

                        SELECT

                            F.Id_factura AS [Número de factura],

                            F.Id_venta AS [Folio de venta],

                            C.RFC AS [RFC del cliente],

                            C.Nombre + ' ' + C.Apellido AS [Cliente],

                            F.Subtotal AS [Subtotal],

                            F.IVA AS [IVA],

                            F.Total AS [Total],

                            F.Metodo_pago AS [Método de pago],

                            V.Fecha AS [Fecha de venta],

                            CASE

                                WHEN F.Estatus = 1 THEN 'Activa'

                                ELSE 'Cancelada'

                            END AS [Estatus]

                        FROM Factura F

                        INNER JOIN Venta V

                            ON F.Id_venta = V.Id_venta

                        INNER JOIN Cliente C

                            ON V.RFC = C.RFC

                        WHERE

                            F.Id_venta LIKE @Buscar

                            OR C.Nombre LIKE @Buscar

                            OR C.Apellido LIKE @Buscar

                            OR C.RFC LIKE @Buscar

                        ORDER BY F.Id_factura DESC";

                    using (SqlCommand comando =

                        new SqlCommand(consulta, conexion))

                    {

                        comando.Parameters.Add(

                            "@Buscar",

                            SqlDbType.VarChar,

                            200).Value =

                            "%" + txtNombreUsuario.Text.Trim() + "%";

                        SqlDataAdapter adaptador =

                            new SqlDataAdapter(comando);

                        DataTable tabla = new DataTable();

                        adaptador.Fill(tabla);

                        dgvFacturas.DataSource = tabla;

                        if (dgvFacturas.Columns.Contains("Subtotal"))

                            dgvFacturas.Columns["Subtotal"]

                                .DefaultCellStyle.Format = "C2";

                        if (dgvFacturas.Columns.Contains("IVA"))

                            dgvFacturas.Columns["IVA"]

                                .DefaultCellStyle.Format = "C2";

                        if (dgvFacturas.Columns.Contains("Total"))

                            dgvFacturas.Columns["Total"]

                                .DefaultCellStyle.Format = "C2";

                        lblTituloTabla.Text =

                            tabla.Rows.Count > 0

                            ? "Facturas registradas: " + tabla.Rows.Count

                            : "No se encontraron facturas";

                    }

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al cargar las facturas: " + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private void Consultar_Click(object sender, EventArgs e)

        {

            CargarFacturas();

        }

        private void Limpiar_Click(object sender, EventArgs e)

        {

            txtNombreUsuario.Clear();

            CargarFacturas();

        }

        private void TxtNombreUsuario_TextChanged(

            object sender, EventArgs e)

        {

            CargarFacturas();

        }

        private void VerDetalle_Click(object sender, EventArgs e)

        {

            MostrarDetalleFactura();

        }

        private void DgvFacturas_CellDoubleClick(

            object sender, DataGridViewCellEventArgs e)

        {

            if (e.RowIndex >= 0)

                MostrarDetalleFactura();

        }

        private void MostrarDetalleFactura()

        {

            if (dgvFacturas.CurrentRow == null)

            {

                MessageBox.Show(

                    "Selecciona una factura.",

                    "Facturación",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Information);

                return;

            }

            try

            {

                string idVenta =

                    Convert.ToString(

                        dgvFacturas.CurrentRow.Cells[

                            "Folio de venta"].Value);

                using (SqlConnection conexion = CrearConexion())

                {

                    string consulta = @"

                        SELECT

                            F.Id_factura,

                            F.Id_venta,

                            C.RFC,

                            C.Nombre + ' ' + C.Apellido AS Cliente,

                            C.Correo,

                            F.Subtotal,

                            F.IVA,

                            F.Total,

                            F.Metodo_pago,

                            V.Fecha,

                            V.Hora,

                            CASE

                                WHEN F.Estatus = 1 THEN 'Activa'

                                ELSE 'Cancelada'

                            END AS Estatus

                        FROM Factura F

                        INNER JOIN Venta V

                            ON F.Id_venta = V.Id_venta

                        INNER JOIN Cliente C

                            ON V.RFC = C.RFC

                        WHERE F.Id_venta = @Id_venta";

                    using (SqlCommand comando =

                        new SqlCommand(consulta, conexion))

                    {

                        comando.Parameters.Add(

                            "@Id_venta",

                            SqlDbType.VarChar,

                            50).Value = idVenta;

                        conexion.Open();

                        using (SqlDataReader lector =

                            comando.ExecuteReader())

                        {

                            if (lector.Read())

                            {

                                string detalle =

                                    "FACTURA\n\n" +

                                    "Número de factura: " +

                                    lector["Id_factura"] + "\n" +

                                    "Folio de venta: " +

                                    lector["Id_venta"] + "\n\n" +

                                    "DATOS DEL CLIENTE\n" +

                                    "Nombre: " +

                                    lector["Cliente"] + "\n" +

                                    "RFC: " +

                                    lector["RFC"] + "\n" +

                                    "Correo: " +

                                    lector["Correo"] + "\n\n" +

                                    "DATOS DE LA FACTURA\n" +

                                    "Fecha: " +

                                    Convert.ToDateTime(

                                        lector["Fecha"]).ToString("dd/MM/yyyy") + "\n" +

                                    "Hora: " +

                                    lector["Hora"] + "\n" +

                                    "Método de pago: " +

                                    lector["Metodo_pago"] + "\n\n" +

                                    "Subtotal: " +

                                    Convert.ToDecimal(

                                        lector["Subtotal"]).ToString("C2") + "\n" +

                                    "IVA: " +

                                    Convert.ToDecimal(

                                        lector["IVA"]).ToString("C2") + "\n" +

                                    "TOTAL: " +

                                    Convert.ToDecimal(

                                        lector["Total"]).ToString("C2") + "\n\n" +

                                    "Estatus: " +

                                    lector["Estatus"];

                                MessageBox.Show(

                                    detalle,

                                    "Detalle de factura",

                                    MessageBoxButtons.OK,

                                    MessageBoxIcon.Information);

                            }

                            else

                            {

                                MessageBox.Show(

                                    "No se encontró la factura.",

                                    "Facturación",

                                    MessageBoxButtons.OK,

                                    MessageBoxIcon.Warning);

                            }

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al consultar el detalle: " + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

    }

}
