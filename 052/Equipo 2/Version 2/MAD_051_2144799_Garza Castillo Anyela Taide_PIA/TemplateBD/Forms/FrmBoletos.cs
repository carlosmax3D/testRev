using System;

using System.Data;

using System.Drawing;

using System.Windows.Forms;

using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;

namespace TemplateCSharp.Forms

{

    public partial class FrmBoletos : Form

    {

        private readonly string cadenaConexion;

        private DataGridView dgvBoletos;

        private TextBox txtPrecio;

        private Label lblPrecio;

        private int idBoletoSeleccionado = -1;

        public FrmBoletos()

        {

            InitializeComponent();

            IConfiguration configuration =

                new ConfigurationBuilder()

                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)

                .AddJsonFile("appsettings.json", optional: false)

                .Build();

            cadenaConexion =

                configuration.GetConnectionString("DefaultConnection");

            ConfigurarFormulario();

            CargarBoletos();

            iconButton1.Click += Guardar_Click;

            iconButton2.Click += Eliminar_Click;

            iconButton3.Click += Editar_Click;

            txtNombreUsuario.TextChanged +=

                (s, e) => CargarBoletos(txtNombreUsuario.Text.Trim());

        }

        private SqlConnection CrearConexion()

        {

            return new SqlConnection(cadenaConexion);

        }

        private void ConfigurarFormulario()

        {

            Text = "Administración de boletos";

            BackColor = Color.FromArgb(245, 247, 250);

            StartPosition = FormStartPosition.CenterScreen;

            lblNombreUsuario.Text = "Buscar boleto:";

            txtNombreUsuario.PlaceholderText =

                "ID, folio de venta o asiento";

            lblPrecio = new Label();

            lblPrecio.Text = "Precio del boleto:";

            lblPrecio.Font =

                new Font("Century Gothic", 10, FontStyle.Regular);

            lblPrecio.Location = new Point(500, 89);

            lblPrecio.AutoSize = true;

            panel1.Controls.Add(lblPrecio);

            txtPrecio = new TextBox();

            txtPrecio.Font = new Font("Segoe UI", 10);

            txtPrecio.Location = new Point(500, 115);

            txtPrecio.Size = new Size(250, 34);

            txtPrecio.Enabled = false;

            panel1.Controls.Add(txtPrecio);

            iconButton1.Text = "Guardar";

            iconButton3.Text = "Editar";

            iconButton2.Text = "Eliminar";

            iconButton1.IconChar =

                FontAwesome.Sharp.IconChar.Save;

            iconButton3.IconChar =

                FontAwesome.Sharp.IconChar.Edit;

            iconButton2.IconChar =

                FontAwesome.Sharp.IconChar.Trash;

            dgvBoletos = new DataGridView();

            dgvBoletos.Location = new Point(25, 400);

            dgvBoletos.Size = new Size(1490, 420);

            dgvBoletos.Anchor =

                AnchorStyles.Top | AnchorStyles.Left |

                AnchorStyles.Right | AnchorStyles.Bottom;

            dgvBoletos.BackgroundColor = Color.White;

            dgvBoletos.BorderStyle = BorderStyle.None;

            dgvBoletos.AllowUserToAddRows = false;

            dgvBoletos.AllowUserToDeleteRows = false;

            dgvBoletos.ReadOnly = true;

            dgvBoletos.AutoSizeColumnsMode =

                DataGridViewAutoSizeColumnsMode.Fill;

            dgvBoletos.SelectionMode =

                DataGridViewSelectionMode.FullRowSelect;

            dgvBoletos.MultiSelect = false;

            dgvBoletos.RowHeadersVisible = false;

            dgvBoletos.Font =

                new Font("Segoe UI", 9);

            dgvBoletos.ColumnHeadersDefaultCellStyle.BackColor =

                Color.FromArgb(40, 65, 100);

            dgvBoletos.ColumnHeadersDefaultCellStyle.ForeColor =

                Color.White;

            dgvBoletos.EnableHeadersVisualStyles = false;

            dgvBoletos.SelectionChanged +=

                DgvBoletos_SelectionChanged;

            Controls.Add(dgvBoletos);

        }

        private void CargarBoletos(string busqueda = "")

        {

            try

            {

                using (SqlConnection conexion = CrearConexion())

                {

                    string consulta = @"

                        SELECT

                            B.Id_boleto AS [ID Boleto],

                            B.Id_venta AS [Folio Venta],

                            C.Nombre + ' ' + C.Apellido AS [Cliente],

                            P.Id_partido AS [ID Partido],

                            E.Nombre AS [Estadio],

                            Z.Nombre AS [Zona],

                            B.Id_asiento AS [Asiento],

                            B.Precio AS [Precio],

                            CASE

                                WHEN B.Estatus = 1 THEN 'Activo'

                                ELSE 'Inactivo'

                            END AS [Estatus]

                        FROM Boleta B

                        INNER JOIN Venta V

                            ON B.Id_venta = V.Id_venta

                        INNER JOIN Cliente C

                            ON V.RFC = C.RFC

                        INNER JOIN Partido P

                            ON B.Id_partido = P.Id_partido

                        INNER JOIN Estadio E

                            ON P.Id_estadio = E.Id_estadio

                        INNER JOIN Zona Z

                            ON B.Id_zona = Z.Id_zona

                        WHERE

                            CAST(B.Id_boleto AS VARCHAR(20))

                                LIKE @Busqueda

                            OR B.Id_venta LIKE @Busqueda

                            OR B.Id_asiento LIKE @Busqueda

                            OR C.Nombre LIKE @Busqueda

                            OR C.Apellido LIKE @Busqueda

                        ORDER BY B.Id_boleto DESC";

                    SqlDataAdapter da =

                        new SqlDataAdapter(consulta, conexion);

                    da.SelectCommand.Parameters.Add(

                        "@Busqueda", SqlDbType.VarChar, 100).Value =

                        "%" + busqueda + "%";

                    DataTable tabla = new DataTable();

                    da.Fill(tabla);

                    dgvBoletos.DataSource = tabla;

                    if (dgvBoletos.Columns.Contains("ID Partido"))

                        dgvBoletos.Columns["ID Partido"].Visible = false;

                    idBoletoSeleccionado = -1;

                    txtPrecio.Clear();

                    txtPrecio.Enabled = false;

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al cargar los boletos: " + ex.Message,

                    "Boletos",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private void DgvBoletos_SelectionChanged(

            object sender, EventArgs e)

        {

            if (dgvBoletos.CurrentRow == null)

                return;

            if (dgvBoletos.CurrentRow.Cells["ID Boleto"].Value == null)

                return;

            idBoletoSeleccionado = Convert.ToInt32(

                dgvBoletos.CurrentRow.Cells["ID Boleto"].Value);

            txtPrecio.Text =

                Convert.ToDecimal(

                    dgvBoletos.CurrentRow.Cells["Precio"].Value

                ).ToString("0.00");

            txtPrecio.Enabled = false;

        }

        private void Editar_Click(object sender, EventArgs e)

        {

            if (idBoletoSeleccionado == -1)

            {

                MessageBox.Show(

                    "Selecciona un boleto de la tabla.",

                    "Aviso",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

                return;

            }

            string estatus = dgvBoletos.CurrentRow

                .Cells["Estatus"].Value.ToString();

            if (estatus != "Activo")

            {

                MessageBox.Show(

                    "No puedes editar un boleto inactivo.");

                return;

            }

            txtPrecio.Enabled = true;

            txtPrecio.Focus();

            MessageBox.Show(

                "Modifica el precio y presiona Guardar.");

        }

        private void Guardar_Click(object sender, EventArgs e)

        {

            if (idBoletoSeleccionado == -1)

            {

                MessageBox.Show("Selecciona un boleto.");

                return;

            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio)

                || precio < 0)

            {

                MessageBox.Show(

                    "Ingresa un precio válido.");

                return;

            }

            DialogResult respuesta = MessageBox.Show(

                "¿Deseas guardar el nuevo precio del boleto?",

                "Confirmar modificación",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)

                return;

            try

            {

                using (SqlConnection conexion = CrearConexion())

                {

                    conexion.Open();

                    string consulta = @"

                        UPDATE Boleta

                        SET Precio = @Precio

                        WHERE Id_boleto = @Id_boleto

                        AND Estatus = 1";

                    using (SqlCommand cmd =

                        new SqlCommand(consulta, conexion))

                    {

                        cmd.Parameters.Add(

                            "@Precio", SqlDbType.Decimal).Value = precio;

                        cmd.Parameters["@Precio"].Precision = 20;

                        cmd.Parameters["@Precio"].Scale = 2;

                        cmd.Parameters.Add(

                            "@Id_boleto", SqlDbType.Int).Value =

                            idBoletoSeleccionado;

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)

                        {

                            MessageBox.Show(

                                "Precio actualizado correctamente.",

                                "Boletos",

                                MessageBoxButtons.OK,

                                MessageBoxIcon.Information);

                            CargarBoletos(txtNombreUsuario.Text.Trim());

                        }

                        else

                        {

                            MessageBox.Show(

                                "No se encontró un boleto activo para modificar.");

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al guardar los cambios: " + ex.Message,

                    "Boletos",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private void Eliminar_Click(object sender, EventArgs e)

        {

            if (idBoletoSeleccionado == -1)

            {

                MessageBox.Show(

                    "Selecciona un boleto de la tabla.");

                return;

            }

            string estatus = dgvBoletos.CurrentRow

                .Cells["Estatus"].Value.ToString();

            if (estatus != "Activo")

            {

                MessageBox.Show("Este boleto ya está inactivo.");

                return;

            }

            DialogResult respuesta = MessageBox.Show(

                "¿Deseas desactivar este boleto?",

                "Confirmar eliminación",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)

                return;

            try

            {

                using (SqlConnection conexion = CrearConexion())

                {

                    conexion.Open();

                    string consulta = @"

                        UPDATE Boleta

                        SET Estatus = 0

                        WHERE Id_boleto = @Id_boleto

                        AND Estatus = 1";

                    using (SqlCommand cmd =

                        new SqlCommand(consulta, conexion))

                    {

                        cmd.Parameters.Add(

                            "@Id_boleto", SqlDbType.Int).Value =

                            idBoletoSeleccionado;

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)

                        {

                            MessageBox.Show(

                                "Boleto desactivado correctamente.",

                                "Boletos",

                                MessageBoxButtons.OK,

                                MessageBoxIcon.Information);

                            CargarBoletos(txtNombreUsuario.Text.Trim());

                        }

                        else

                        {

                            MessageBox.Show(

                                "No se pudo desactivar el boleto.");

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al desactivar el boleto: " + ex.Message,

                    "Boletos",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

    }

}
