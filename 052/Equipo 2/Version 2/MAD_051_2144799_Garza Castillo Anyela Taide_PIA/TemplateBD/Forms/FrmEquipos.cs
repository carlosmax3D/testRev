using System;

using System.Data;

using System.Drawing;

using System.Windows.Forms;

using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;

namespace TemplateCSharp.Forms

{

    public partial class FrmEquipos : Form

    {

        private readonly string _connectionString;

        private DataGridView dgvEquipos = new DataGridView();

        private TextBox txtTipo = new TextBox();

        private int? idEquipoSeleccionado = null;

        public FrmEquipos()

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

            CargarEquipos();

        }

        // PREPARAR FORMULARIO

        private void PrepararFormulario()

        {

            panel1.Height = 381;

            // Campo Nombre

            lblNombreUsuario.Text = "Nombre:";

            txtNombreUsuario.Location = new Point(41, 115);

            txtNombreUsuario.Size = new Size(318, 34);

            // Campo Tipo

            Label lblTipo = new Label();

            lblTipo.Text = "Tipo:";

            lblTipo.Location = new Point(400, 89);

            lblTipo.AutoSize = true;

            lblTipo.Font = new Font(

                "Century Gothic", 10F, FontStyle.Regular);

            panel1.Controls.Add(lblTipo);

            txtTipo.Location = new Point(400, 115);

            txtTipo.Size = new Size(318, 34);

            txtTipo.Font = new Font("Segoe UI", 10F);

            panel1.Controls.Add(txtTipo);

            // CREAR TABLA

            dgvEquipos = new DataGridView();

            dgvEquipos.Location = new Point(20, 410);

            dgvEquipos.Size = new Size(1500, 420);

            dgvEquipos.Anchor =

                AnchorStyles.Top |

                AnchorStyles.Left |

                AnchorStyles.Right |

                AnchorStyles.Bottom;

            dgvEquipos.BackgroundColor = Color.White;

            dgvEquipos.BorderStyle = BorderStyle.None;

            dgvEquipos.CellBorderStyle =

                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvEquipos.ReadOnly = true;

            dgvEquipos.AllowUserToAddRows = false;

            dgvEquipos.AllowUserToDeleteRows = false;

            dgvEquipos.SelectionMode =

                DataGridViewSelectionMode.FullRowSelect;

            dgvEquipos.MultiSelect = false;

            // ACTIVAR DESPLAZAMIENTO HORIZONTAL Y VERTICAL

            dgvEquipos.ScrollBars = ScrollBars.Both;

            // Permitir que las columnas tengan su propio ancho

            dgvEquipos.AutoSizeColumnsMode =

                DataGridViewAutoSizeColumnsMode.None;

            dgvEquipos.RowHeadersVisible = false;

            dgvEquipos.RowTemplate.Height = 35;

            dgvEquipos.ColumnHeadersHeight = 40;

            dgvEquipos.EnableHeadersVisualStyles = false;

            // DISEÑO DE ENCABEZADOS

            dgvEquipos.ColumnHeadersDefaultCellStyle.BackColor =

                Color.FromArgb(0, 192, 210);

            dgvEquipos.ColumnHeadersDefaultCellStyle.ForeColor =

                Color.Black;

            dgvEquipos.ColumnHeadersDefaultCellStyle.Font =

                new Font("Century Gothic", 10F, FontStyle.Bold);

            // DISEÑO DE CELDAS

            dgvEquipos.DefaultCellStyle.Font =

                new Font("Century Gothic", 9F);

            dgvEquipos.DefaultCellStyle.SelectionBackColor =

                Color.FromArgb(180, 230, 235);

            dgvEquipos.DefaultCellStyle.SelectionForeColor =

                Color.Black;

            dgvEquipos.CellClick += dgvEquipos_CellClick;

            Controls.Add(dgvEquipos);

            dgvEquipos.BringToFront();

        }

        // CONEXIÓN

        private SqlConnection CrearConexion()

        {

            return new SqlConnection(_connectionString);

        }

        // CONSULTAR EQUIPOS

        private void CargarEquipos()

        {

            try

            {

                using SqlConnection conexion = CrearConexion();

                conexion.Open();

                string consulta = @"

                    SELECT

                        Id_equipo,

                        Tipo,

                        Nombre

                    FROM Equipo

                    ORDER BY Nombre;";

                using SqlDataAdapter adaptador =

                    new SqlDataAdapter(consulta, conexion);

                DataTable tabla = new DataTable();

                adaptador.Fill(tabla);

                dgvEquipos.DataSource = tabla;

                dgvEquipos.Columns["Id_equipo"].HeaderText =

                    "ID del equipo";

                dgvEquipos.Columns["Tipo"].HeaderText =

                    "Tipo de equipo";

                dgvEquipos.Columns["Nombre"].HeaderText =

                    "Nombre del equipo";

                // ANCHOS DE LAS COLUMNAS

                dgvEquipos.Columns["Id_equipo"].Width = 180;

                dgvEquipos.Columns["Tipo"].Width = 600;

                dgvEquipos.Columns["Nombre"].Width = 900;

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al consultar los equipos:\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        // VALIDAR CAMPOS

        private bool ValidarCampos()

        {

            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||

                string.IsNullOrWhiteSpace(txtTipo.Text))

            {

                MessageBox.Show(

                    "Completa todos los campos.",

                    "Validación",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

                return false;

            }

            if (txtNombreUsuario.Text.Trim().Length > 100 ||

                txtTipo.Text.Trim().Length > 100)

            {

                MessageBox.Show(

                    "El nombre y el tipo no deben superar 100 caracteres.",

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

                "@tipo", txtTipo.Text.Trim());

            comando.Parameters.AddWithValue(

                "@nombre", txtNombreUsuario.Text.Trim());

        }

        // GUARDAR EQUIPO

        private void btnGuardar_Click(object? sender, EventArgs e)

        {

            if (!ValidarCampos())

                return;

            try

            {

                using SqlConnection conexion = CrearConexion();

                conexion.Open();

                string consulta = @"

                    INSERT INTO Equipo (Tipo, Nombre)

                    VALUES (@tipo, @nombre);";

                using SqlCommand comando =

                    new SqlCommand(consulta, conexion);

                AgregarParametros(comando);

                comando.ExecuteNonQuery();

                MessageBox.Show(

                    "Equipo registrado correctamente.",

                    "Éxito",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Information);

                LimpiarCampos();

                CargarEquipos();

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al guardar el equipo:\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        // EDITAR EQUIPO

        private void btnEditar_Click(object? sender, EventArgs e)

        {

            if (idEquipoSeleccionado == null)

            {

                MessageBox.Show(

                    "Selecciona un equipo de la tabla.",

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

                    UPDATE Equipo

                    SET Tipo = @tipo,

                        Nombre = @nombre

                    WHERE Id_equipo = @id;";

                using SqlCommand comando =

                    new SqlCommand(consulta, conexion);

                AgregarParametros(comando);

                comando.Parameters.AddWithValue(

                    "@id", idEquipoSeleccionado.Value);

                int filas = comando.ExecuteNonQuery();

                if (filas > 0)

                {

                    MessageBox.Show(

                        "Equipo actualizado correctamente.",

                        "Éxito",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Information);

                    LimpiarCampos();

                    CargarEquipos();

                }

                else

                {

                    MessageBox.Show(

                        "No se encontró el equipo seleccionado.");

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al editar el equipo:\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        // ELIMINAR EQUIPO

        private void btnEliminar_Click(object? sender, EventArgs e)

        {

            if (idEquipoSeleccionado == null)

            {

                MessageBox.Show(

                    "Selecciona un equipo de la tabla.",

                    "Aviso",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

                return;

            }

            DialogResult respuesta = MessageBox.Show(

                "¿Deseas eliminar este equipo?",

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

                    DELETE FROM Equipo

                    WHERE Id_equipo = @id;";

                using SqlCommand comando =

                    new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue(

                    "@id", idEquipoSeleccionado.Value);

                int filas = comando.ExecuteNonQuery();

                if (filas > 0)

                {

                    MessageBox.Show(

                        "Equipo eliminado correctamente.",

                        "Éxito",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Information);

                    LimpiarCampos();

                    CargarEquipos();

                }

                else

                {

                    MessageBox.Show(

                        "No se encontró el equipo seleccionado.");

                }

            }

            catch (SqlException ex)

            {

                MessageBox.Show(

                    "No se pudo eliminar el equipo. Es posible que tenga partidos relacionados.\n\n" +

                    ex.Message,

                    "No se puede eliminar",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al eliminar el equipo:\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        // SELECCIONAR EQUIPO

        private void dgvEquipos_CellClick(

            object? sender, DataGridViewCellEventArgs e)

        {

            if (e.RowIndex < 0)

                return;

            DataGridViewRow fila = dgvEquipos.Rows[e.RowIndex];

            idEquipoSeleccionado =

                Convert.ToInt32(fila.Cells["Id_equipo"].Value);

            txtTipo.Text =

                Convert.ToString(fila.Cells["Tipo"].Value) ?? "";

            txtNombreUsuario.Text =

                Convert.ToString(fila.Cells["Nombre"].Value) ?? "";

        }

        // LIMPIAR CAMPOS

        private void LimpiarCampos()

        {

            txtNombreUsuario.Clear();

            txtTipo.Clear();

            idEquipoSeleccionado = null;

            dgvEquipos.ClearSelection();

        }

    }

}
