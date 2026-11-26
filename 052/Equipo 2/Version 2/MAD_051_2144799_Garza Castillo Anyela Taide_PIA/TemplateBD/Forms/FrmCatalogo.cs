using System;

using System.Data;

using System.Drawing;

using System.Windows.Forms;

using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;

namespace TemplateCSharp.Forms

{

    public partial class FrmCatalogo : Form

    {

        private readonly string _connectionString;

        private DataGridView dgvEstadios = new DataGridView();

        private TextBox txtCiudad = new TextBox();

        private TextBox txtCapacidad = new TextBox();

        private TextBox txtMapa = new TextBox();

        private int? idEstadioSeleccionado = null;

        public FrmCatalogo()

        {

            InitializeComponent();

            IConfiguration configuration = new ConfigurationBuilder()

                .SetBasePath(AppContext.BaseDirectory)

                .AddJsonFile("appsettings.json", optional: false)

                .Build();

            _connectionString =

                configuration.GetConnectionString("DefaultConnection")

                ?? throw new InvalidOperationException(

                    "No se encontró la cadena de conexión.");

            PrepararFormulario();

            iconButton1.Click -= btnGuardar_Click;

            iconButton1.Click += btnGuardar_Click;

            iconButton3.Click -= btnEditar_Click;

            iconButton3.Click += btnEditar_Click;

            iconButton2.Click -= btnEliminar_Click;

            iconButton2.Click += btnEliminar_Click;

            CargarEstadios();

        }

        // PREPARAR FORMULARIO

        private void PrepararFormulario()

        {

            // Ajustar el panel superior

            panel1.Height = 390;

            // Campo Ciudad

            Label lblCiudad = new Label();

            lblCiudad.Text = "Ciudad:";

            lblCiudad.Location = new Point(400, 89);

            lblCiudad.AutoSize = true;

            panel1.Controls.Add(lblCiudad);

            txtCiudad.Location = new Point(400, 115);

            txtCiudad.Size = new Size(318, 34);

            panel1.Controls.Add(txtCiudad);

            // Campo Capacidad

            Label lblCapacidad = new Label();

            lblCapacidad.Text = "Capacidad:";

            lblCapacidad.Location = new Point(41, 180);

            lblCapacidad.AutoSize = true;

            panel1.Controls.Add(lblCapacidad);

            txtCapacidad.Location = new Point(41, 206);

            txtCapacidad.Size = new Size(318, 34);

            panel1.Controls.Add(txtCapacidad);

            // Campo Mapa

            Label lblMapa = new Label();

            lblMapa.Text = "Mapa:";

            lblMapa.Location = new Point(400, 180);

            lblMapa.AutoSize = true;

            panel1.Controls.Add(lblMapa);

            txtMapa.Location = new Point(400, 206);

            txtMapa.Size = new Size(318, 34);

            panel1.Controls.Add(txtMapa);

            // Ajustar el campo Nombre existente

            txtNombreUsuario.Location = new Point(41, 115);

            txtNombreUsuario.Size = new Size(318, 34);

            lblNombreUsuario.Text = "Nombre:";

            // Crear tabla

            dgvEstadios = new DataGridView();

            dgvEstadios.Location = new Point(20, 410);

            dgvEstadios.Size = new Size(1500, 420);

            dgvEstadios.Anchor =

                AnchorStyles.Top | AnchorStyles.Left |

                AnchorStyles.Right | AnchorStyles.Bottom;

            dgvEstadios.BackgroundColor = Color.White;

            dgvEstadios.BorderStyle = BorderStyle.None;

            dgvEstadios.CellBorderStyle =

                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvEstadios.ReadOnly = true;

            dgvEstadios.AllowUserToAddRows = false;

            dgvEstadios.AllowUserToDeleteRows = false;

            dgvEstadios.SelectionMode =

                DataGridViewSelectionMode.FullRowSelect;

            dgvEstadios.MultiSelect = false;

            dgvEstadios.AutoSizeColumnsMode =

                DataGridViewAutoSizeColumnsMode.Fill;

            dgvEstadios.RowHeadersVisible = false;

            dgvEstadios.RowTemplate.Height = 35;

            dgvEstadios.ColumnHeadersHeight = 40;

            dgvEstadios.EnableHeadersVisualStyles = false;

            dgvEstadios.ColumnHeadersDefaultCellStyle.BackColor =

                Color.FromArgb(0, 192, 210);

            dgvEstadios.ColumnHeadersDefaultCellStyle.ForeColor =

                Color.Black;

            dgvEstadios.ColumnHeadersDefaultCellStyle.Font =

                new Font("Century Gothic", 10F, FontStyle.Bold);

            dgvEstadios.DefaultCellStyle.Font =

                new Font("Century Gothic", 9F);

            dgvEstadios.DefaultCellStyle.SelectionBackColor =

                Color.FromArgb(180, 230, 235);

            dgvEstadios.DefaultCellStyle.SelectionForeColor =

                Color.Black;

            dgvEstadios.CellClick += dgvEstadios_CellClick;

            Controls.Add(dgvEstadios);

            dgvEstadios.BringToFront();

        }

        // CONEXIÓN

        private SqlConnection CrearConexion()

        {

            return new SqlConnection(_connectionString);

        }

        // CONSULTAR ESTADIOS

        private void CargarEstadios()

        {

            try

            {

                using SqlConnection conexion = CrearConexion();

                conexion.Open();

                string consulta = @"

                    SELECT

                        Id_estadio,

                        Nombre,

                        Ciudad,

                        Capacidad,

                        Mapa,

                        Fecha_reg,

                        Hora

                    FROM Estadio

                    ORDER BY Nombre;";

                using SqlDataAdapter adaptador =

                    new SqlDataAdapter(consulta, conexion);

                DataTable tabla = new DataTable();

                adaptador.Fill(tabla);

                dgvEstadios.DataSource = tabla;

                dgvEstadios.Columns["Id_estadio"].HeaderText =

                    "ID del estadio";

                dgvEstadios.Columns["Nombre"].HeaderText = "Nombre";

                dgvEstadios.Columns["Ciudad"].HeaderText = "Ciudad";

                dgvEstadios.Columns["Capacidad"].HeaderText = "Capacidad";

                dgvEstadios.Columns["Mapa"].HeaderText = "Mapa";

                dgvEstadios.Columns["Fecha_reg"].HeaderText =

                    "Fecha de registro";

                dgvEstadios.Columns["Hora"].HeaderText =

                    "Hora de registro";

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al consultar los estadios:\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        // VALIDAR CAMPOS

        private bool ValidarCampos()

        {

            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||

                string.IsNullOrWhiteSpace(txtCiudad.Text) ||

                string.IsNullOrWhiteSpace(txtCapacidad.Text) ||

                string.IsNullOrWhiteSpace(txtMapa.Text))

            {

                MessageBox.Show(

                    "Completa todos los campos.",

                    "Validación",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

                return false;

            }

            if (!int.TryParse(txtCapacidad.Text.Trim(), out int capacidad)

                || capacidad < 0)

            {

                MessageBox.Show(

                    "La capacidad debe ser un número entero igual o mayor que cero.",

                    "Validación",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

                return false;

            }

            if (txtNombreUsuario.Text.Trim().Length > 200 ||

                txtCiudad.Text.Trim().Length > 200 ||

                txtMapa.Text.Trim().Length > 200)

            {

                MessageBox.Show(

                    "El nombre, ciudad y mapa no deben superar 200 caracteres.",

                    "Validación",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

                return false;

            }

            return true;

        }

        // AGREGAR PARÁMETROS

        private void AgregarParametros(SqlCommand comando)

        {

            comando.Parameters.AddWithValue(

                "@nombre", txtNombreUsuario.Text.Trim());

            comando.Parameters.AddWithValue(

                "@ciudad", txtCiudad.Text.Trim());

            comando.Parameters.AddWithValue(

                "@capacidad", int.Parse(txtCapacidad.Text.Trim()));

            comando.Parameters.AddWithValue(

                "@mapa", txtMapa.Text.Trim());

        }

        // GUARDAR ESTADIO

        private void btnGuardar_Click(object? sender, EventArgs e)

        {

            if (!ValidarCampos())

                return;

            try

            {

                using SqlConnection conexion = CrearConexion();

                conexion.Open();

                using SqlTransaction transaccion =

                    conexion.BeginTransaction(

                        IsolationLevel.Serializable);

                try

                {

                    string sqlId = @"

                        SELECT ISNULL(MAX(Id_estadio), 0) + 1

                        FROM Estadio WITH (UPDLOCK, HOLDLOCK);";

                    int nuevoId;

                    using (SqlCommand comando = new SqlCommand(

                        sqlId, conexion, transaccion))

                    {

                        nuevoId = Convert.ToInt32(

                            comando.ExecuteScalar());

                    }

                    string consulta = @"

                        INSERT INTO Estadio

                        (Id_estadio, Nombre, Ciudad, Capacidad,

                         Mapa, Fecha_reg, Hora)

                        VALUES

                        (@id, @nombre, @ciudad, @capacidad,

                         @mapa, CAST(GETDATE() AS DATE),

                         CAST(GETDATE() AS TIME));";

                    using (SqlCommand comando = new SqlCommand(

                        consulta, conexion, transaccion))

                    {

                        comando.Parameters.AddWithValue("@id", nuevoId);

                        AgregarParametros(comando);

                        comando.ExecuteNonQuery();

                    }

                    transaccion.Commit();

                    MessageBox.Show(

                        "Estadio registrado correctamente.",

                        "Éxito",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Information);

                    LimpiarCampos();

                    CargarEstadios();

                }

                catch

                {

                    transaccion.Rollback();

                    throw;

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al guardar el estadio:\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        // EDITAR ESTADIO

        private void btnEditar_Click(object? sender, EventArgs e)

        {

            if (idEstadioSeleccionado == null)

            {

                MessageBox.Show(

                    "Selecciona un estadio de la tabla.",

                    "Aviso",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

                return;

            }

            if (!ValidarCampos())

                return;

            try

            {

                using SqlConnection conexion = CrearConexion();

                conexion.Open();

                string consulta = @"

                    UPDATE Estadio

                    SET Nombre = @nombre,

                        Ciudad = @ciudad,

                        Capacidad = @capacidad,

                        Mapa = @mapa

                    WHERE Id_estadio = @id;";

                using SqlCommand comando =

                    new SqlCommand(consulta, conexion);

                AgregarParametros(comando);

                comando.Parameters.AddWithValue(

                    "@id", idEstadioSeleccionado.Value);

                int filas = comando.ExecuteNonQuery();

                if (filas > 0)

                {

                    MessageBox.Show(

                        "Estadio actualizado correctamente.",

                        "Éxito",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Information);

                    LimpiarCampos();

                    CargarEstadios();

                }

                else

                {

                    MessageBox.Show(

                        "No se encontró el estadio seleccionado.");

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al editar el estadio:\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        // ELIMINAR ESTADIO

        private void btnEliminar_Click(object? sender, EventArgs e)

        {

            if (idEstadioSeleccionado == null)

            {

                MessageBox.Show(

                    "Selecciona un estadio de la tabla.",

                    "Aviso",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

                return;

            }

            DialogResult respuesta = MessageBox.Show(

                "¿Deseas eliminar este estadio?",

                "Confirmar eliminación",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)

                return;

            try

            {

                using SqlConnection conexion = CrearConexion();

                conexion.Open();

                string consulta = @"

                    DELETE FROM Estadio

                    WHERE Id_estadio = @id;";

                using SqlCommand comando =

                    new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue(

                    "@id", idEstadioSeleccionado.Value);

                int filas = comando.ExecuteNonQuery();

                if (filas > 0)

                {

                    MessageBox.Show(

                        "Estadio eliminado correctamente.",

                        "Éxito",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Information);

                    LimpiarCampos();

                    CargarEstadios();

                }

                else

                {

                    MessageBox.Show(

                        "No se encontró el estadio seleccionado.");

                }

            }

            catch (SqlException ex)

            {

                MessageBox.Show(

                    "No se pudo eliminar el estadio. Es posible que tenga zonas, asientos o partidos relacionados.\n\n" +

                    ex.Message,

                    "No se puede eliminar",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al eliminar el estadio:\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        // SELECCIONAR ESTADIO

        private void dgvEstadios_CellClick(

            object? sender, DataGridViewCellEventArgs e)

        {

            if (e.RowIndex < 0)

                return;

            DataGridViewRow fila = dgvEstadios.Rows[e.RowIndex];

            idEstadioSeleccionado =

                Convert.ToInt32(fila.Cells["Id_estadio"].Value);

            txtNombreUsuario.Text =

                Convert.ToString(fila.Cells["Nombre"].Value) ?? "";

            txtCiudad.Text =

                Convert.ToString(fila.Cells["Ciudad"].Value) ?? "";

            txtCapacidad.Text =

                Convert.ToString(fila.Cells["Capacidad"].Value) ?? "";

            txtMapa.Text =

                Convert.ToString(fila.Cells["Mapa"].Value) ?? "";

        }

        // LIMPIAR CAMPOS

        private void LimpiarCampos()

        {

            txtNombreUsuario.Clear();

            txtCiudad.Clear();

            txtCapacidad.Clear();

            txtMapa.Clear();

            idEstadioSeleccionado = null;

            dgvEstadios.ClearSelection();

        }

    }

}