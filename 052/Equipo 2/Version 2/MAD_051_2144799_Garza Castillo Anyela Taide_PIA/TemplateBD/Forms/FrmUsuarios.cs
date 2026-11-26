using System;

using System.Data;

using System.Windows.Forms;

using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using TemplateCSharp.Utilities;

namespace TemplateCSharp.Forms

{

    public partial class FrmUsuarios : Form

    {

        private readonly string _connectionString;

        private int _idSeleccionado = 0;

        public FrmUsuarios()

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

            iconButton1.Click += btnGuardar_Click;

            iconButton2.Click += btnEditar_Click;

            iconButton3.Click += btnEliminar_Click;

            dataGridView1.CellClick += dataGridView1_CellClick;

            textBox2.UseSystemPasswordChar = true;

            textBox4.UseSystemPasswordChar = true;

            comboBox2.Items.Add("Activo");

            comboBox2.Items.Add("Inactivo");

            comboBox2.SelectedIndex = 0;

            dataGridView1.ReadOnly = true;

            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.SelectionMode =

                DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.MultiSelect = false;

            CargarTipos();

            CargarUsuarios();

        }

        private void CargarTipos()

        {

            try

            {

                using SqlConnection conexion =

                    new SqlConnection(_connectionString);

                string consulta =

                    "SELECT Id_tipo, Nombre FROM Tipo ORDER BY Nombre";

                using SqlDataAdapter adaptador =

                    new SqlDataAdapter(consulta, conexion);

                DataTable tabla = new DataTable();

                adaptador.Fill(tabla);

                comboBox1.DataSource = tabla;

                comboBox1.DisplayMember = "Nombre";

                comboBox1.ValueMember = "Id_tipo";

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "No se pudieron cargar los tipos de usuario.\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private void CargarUsuarios()

        {

            try

            {

                using SqlConnection conexion =

                    new SqlConnection(_connectionString);

                string consulta = @"

                    SELECT

                        U.Id_user AS IdUsuario,

                        U.Nombre AS NombreUsuario,

                        U.Apellido AS ApellidosUsuario,

                        U.Correo AS EmailUsuario,

                        U.Fecha_nacimiento AS FechaNacUsuario,

                        T.Nombre AS TipoUsuario,

                        CASE

                            WHEN U.Estatus = 1 THEN 'Activo'

                            ELSE 'Inactivo'

                        END AS EstatusUsuario

                    FROM User_ U

                    INNER JOIN Tipo T

                        ON U.Id_tipo = T.Id_tipo

                    ORDER BY U.Nombre";

                using SqlDataAdapter adaptador =

                    new SqlDataAdapter(consulta, conexion);

                DataTable tabla = new DataTable();

                adaptador.Fill(tabla);

                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.Columns["IdUsuario"].DataPropertyName =

                    "IdUsuario";

                dataGridView1.Columns["NombreUsuario"].DataPropertyName =

                    "NombreUsuario";

                dataGridView1.Columns["ApellidosUsuario"].DataPropertyName =

                    "ApellidosUsuario";

                dataGridView1.Columns["EmailUsuario"].DataPropertyName =

                    "EmailUsuario";

                dataGridView1.Columns["FechaNacUsuario"].DataPropertyName =

                    "FechaNacUsuario";

                dataGridView1.Columns["TipoUsuario"].DataPropertyName =

                    "TipoUsuario";

                dataGridView1.Columns["EstatusUsuario"].DataPropertyName =

                    "EstatusUsuario";

                dataGridView1.DataSource = tabla;

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "No se pudieron cargar los usuarios.\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private bool ValidarDatos(bool esEdicion)

        {

            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||

                string.IsNullOrWhiteSpace(textBox1.Text) ||

                string.IsNullOrWhiteSpace(textBox5.Text) ||

                string.IsNullOrWhiteSpace(textBox3.Text))

            {

                MessageBox.Show("Completa todos los campos obligatorios.");

                return false;

            }

            if (!System.Net.Mail.MailAddress.TryCreate(

                textBox3.Text.Trim(), out _))

            {

                MessageBox.Show("Ingresa un correo electrónico válido.");

                return false;

            }

            if (!esEdicion || !string.IsNullOrEmpty(textBox2.Text))

            {

                if (!PasswordHelper.EsValida(textBox2.Text))

                {

                    MessageBox.Show(

                        "La contraseña debe tener al menos 8 caracteres, " +

                        "una mayúscula, una minúscula y un carácter especial.");

                    return false;

                }

                if (textBox2.Text != textBox4.Text)

                {

                    MessageBox.Show("Las contraseñas no coinciden.");

                    return false;

                }

            }

            if (comboBox1.SelectedValue == null ||

                comboBox2.SelectedItem == null)

            {

                MessageBox.Show("Selecciona el tipo y estatus del usuario.");

                return false;

            }

            if (dateTimePicker1.Value.Date >= DateTime.Today)

            {

                MessageBox.Show(

                    "La fecha de nacimiento debe ser anterior a hoy.");

                return false;

            }

            return true;

        }

        private bool ExisteCorreo(string correo)

        {

            using SqlConnection conexion =

                new SqlConnection(_connectionString);

            string consulta = @"

                SELECT COUNT(*)

                FROM User_

                WHERE Correo = @correo

                  AND (@id = 0 OR Id_user <> @id)";

            using SqlCommand comando =

                new SqlCommand(consulta, conexion);

            comando.Parameters.AddWithValue("@correo", correo);

            comando.Parameters.AddWithValue("@id", _idSeleccionado);

            conexion.Open();

            return Convert.ToInt32(comando.ExecuteScalar()) > 0;

        }

        private void btnGuardar_Click(object sender, EventArgs e)

        {

            try

            {

                if (!ValidarDatos(false))

                    return;

                string correo = textBox3.Text.Trim();

                if (ExisteCorreo(correo))

                {

                    MessageBox.Show("Ese correo ya está registrado.");

                    return;

                }

                string hash = PasswordHelper.CrearHash(textBox2.Text);

                using SqlConnection conexion =

                    new SqlConnection(_connectionString);

                string consulta = @"

                    INSERT INTO User_

                    (

                        Correo, Contraseña, Nombre, Nomina,

                        Fecha_nacimiento, Fecha_registro,

                        Apellido, Id_tipo, Estatus

                    )

                    VALUES

                    (

                        @correo, @pw, @nombre, @nomina,

                        @fecha, GETDATE(),

                        @apellido, @tipo, @estatus

                    )";

                using SqlCommand comando =

                    new SqlCommand(consulta, conexion);

                AgregarParametros(comando, hash);

                conexion.Open();

                comando.ExecuteNonQuery();

                MessageBox.Show("Usuario registrado correctamente.");

                LimpiarCampos();

                CargarUsuarios();

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "No se pudo registrar el usuario.\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private void btnEditar_Click(object sender, EventArgs e)

        {

            try

            {

                if (_idSeleccionado == 0)

                {

                    MessageBox.Show("Selecciona un usuario de la tabla.");

                    return;

                }

                if (!ValidarDatos(true))

                    return;

                string correo = textBox3.Text.Trim();

                if (ExisteCorreo(correo))

                {

                    MessageBox.Show("Ese correo ya está registrado.");

                    return;

                }

                using SqlConnection conexion =

                    new SqlConnection(_connectionString);

                string consulta = @"

                    UPDATE User_

                    SET Correo = @correo,

                        Nombre = @nombre,

                        Nomina = @nomina,

                        Fecha_nacimiento = @fecha,

                        Apellido = @apellido,

                        Id_tipo = @tipo,

                        Estatus = @estatus

                    WHERE Id_user = @id";

                if (!string.IsNullOrEmpty(textBox2.Text))

                {

                    consulta = consulta.Replace(

                        "WHERE Id_user = @id",

                        "Contraseña = @pw WHERE Id_user = @id");

                }

                using SqlCommand comando =

                    new SqlCommand(consulta, conexion);

                string hash = string.IsNullOrEmpty(textBox2.Text)

                    ? ""

                    : PasswordHelper.CrearHash(textBox2.Text);

                AgregarParametros(comando, hash);

                comando.Parameters.AddWithValue("@id", _idSeleccionado);

                conexion.Open();

                comando.ExecuteNonQuery();

                MessageBox.Show("Usuario actualizado correctamente.");

                LimpiarCampos();

                CargarUsuarios();

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "No se pudo editar el usuario.\n" + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)

        {

            if (_idSeleccionado == 0)

            {

                MessageBox.Show("Selecciona un usuario de la tabla.");

                return;

            }

            DialogResult respuesta = MessageBox.Show(

                "¿Deseas desactivar este usuario?",

                "Confirmar operación",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)

                return;

            try

            {

                using SqlConnection conexion =

                    new SqlConnection(_connectionString);

                string consulta = @"

                    UPDATE User_

                    SET Estatus = 0

                    WHERE Id_user = @id";

                using SqlCommand comando =

                    new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@id", _idSeleccionado);

                conexion.Open();

                comando.ExecuteNonQuery();

                MessageBox.Show("Usuario desactivado correctamente.");

                LimpiarCampos();

                CargarUsuarios();

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "No se pudo desactivar el usuario.\n" + ex.Message);

            }

        }

        private void AgregarParametros(SqlCommand comando, string hash)

        {

            comando.Parameters.AddWithValue(

                "@correo", textBox3.Text.Trim());

            comando.Parameters.AddWithValue(

                "@nombre", txtNombreUsuario.Text.Trim());

            comando.Parameters.AddWithValue(

                "@nomina", textBox5.Text.Trim());

            comando.Parameters.AddWithValue(

                "@fecha", dateTimePicker1.Value.Date);

            comando.Parameters.AddWithValue(

                "@apellido", textBox1.Text.Trim());

            comando.Parameters.AddWithValue(

                "@tipo", Convert.ToInt32(comboBox1.SelectedValue));

            comando.Parameters.AddWithValue(

                "@estatus",

                comboBox2.SelectedItem.ToString() == "Activo");

            if (!string.IsNullOrEmpty(hash))

            {

                comando.Parameters.AddWithValue("@pw", hash);

            }

        }

        private void dataGridView1_CellClick(

            object sender, DataGridViewCellEventArgs e)

        {

            if (e.RowIndex < 0)

                return;

            DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

            _idSeleccionado =

                Convert.ToInt32(fila.Cells["IdUsuario"].Value);

            txtNombreUsuario.Text =

                fila.Cells["NombreUsuario"].Value?.ToString();

            textBox1.Text =

                fila.Cells["ApellidosUsuario"].Value?.ToString();

            textBox3.Text =

                fila.Cells["EmailUsuario"].Value?.ToString();

            dateTimePicker1.Value =

                Convert.ToDateTime(

                    fila.Cells["FechaNacUsuario"].Value);

            comboBox1.Text =

                fila.Cells["TipoUsuario"].Value?.ToString();

            comboBox2.Text =

                fila.Cells["EstatusUsuario"].Value?.ToString();

            textBox2.Clear();

            textBox4.Clear();

        }

        private void LimpiarCampos()

        {

            _idSeleccionado = 0;

            txtNombreUsuario.Clear();

            textBox1.Clear();

            textBox3.Clear();

            textBox5.Clear();

            textBox2.Clear();

            textBox4.Clear();

            dateTimePicker1.Value = DateTime.Today;

            if (comboBox1.Items.Count > 0)

                comboBox1.SelectedIndex = 0;

            comboBox2.SelectedIndex = 0;

            dataGridView1.ClearSelection();

        }

    }

}
