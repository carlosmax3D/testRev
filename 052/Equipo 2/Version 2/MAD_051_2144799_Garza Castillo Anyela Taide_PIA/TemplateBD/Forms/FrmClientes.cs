using System;
using System.Data;
using System.Net.Mail;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace TemplateCSharp.Forms
{
    public partial class FrmClientes : Form
    {
        private readonly string _connectionString;
        private DataGridView dgvClientes;
        private TextBox txtNumero;
        private string? rfcSeleccionado;
        private int idDireccionSeleccionada;
        public FrmClientes()
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
            iconButton1.Click += btnGuardar_Click;
            iconButton3.Click += btnEditar_Click;
            iconButton2.Click += btnEliminar_Click;
        }
        private void PrepararFormulario()
        {
            Text = "Clientes";
            // Configuración de campos
            comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
            dateTimePicker1.MaxDate = DateTime.Today;
            // Campo para número exterior
            Label lblNumero = new Label();
            lblNumero.Text = "Número:";
            lblNumero.Location = new System.Drawing.Point(1050, 248);
            lblNumero.AutoSize = true;
            lblNumero.Font = new System.Drawing.Font(
                "Century Gothic", 10F);
            txtNumero = new TextBox();
            txtNumero.Location = new System.Drawing.Point(1050, 316);
            txtNumero.Size = new System.Drawing.Size(163, 34);
            txtNumero.Font = new System.Drawing.Font("Segoe UI", 10F);
            panel1.Controls.Add(lblNumero);
            panel1.Controls.Add(txtNumero);
            // Tabla para mostrar clientes
            dgvClientes = new DataGridView();
            dgvClientes.Name = "dgvClientes";
            dgvClientes.Location = new System.Drawing.Point(20, 395);
            dgvClientes.Size = new System.Drawing.Size(1500, 440);
            dgvClientes.Anchor =
                AnchorStyles.Top | AnchorStyles.Bottom |
                AnchorStyles.Left | AnchorStyles.Right;
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.RowHeadersVisible = false;
            Controls.Add(dgvClientes);
            dgvClientes.CellClick += dgvClientes_CellClick;
            CargarClientes();
        }
        // CONEXIÓN
        private SqlConnection CrearConexion()
        {
            return new SqlConnection(_connectionString);
        }
        // CONSULTAR CLIENTES
        private void CargarClientes()
        {
            try
            {
                using SqlConnection conexion = CrearConexion();
                conexion.Open();
                string consulta = @"
                   SELECT
                       C.RFC,
                       C.Nombre,
                       C.Apellido,
                       C.Fecha_naci,
                       C.Correo,
                       D.Id_direccion,
                       D.Calle,
                       D.Numero,
                       D.Colonia,
                       D.Ciudad,
                       D.Estado,
                       T.Tel_casa,
                       T.Tel_cel,
                       C.Estatus
                   FROM Cliente C
                   INNER JOIN Direccion D
                       ON C.Id_direccion = D.Id_direccion
                   OUTER APPLY
                   (
                       SELECT TOP (1) Tel_casa, Tel_cel
                       FROM Telefono
                       WHERE RFC = C.RFC
                   ) T
                   ORDER BY C.Nombre;";
                using SqlDataAdapter adaptador =
                    new SqlDataAdapter(consulta, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                tabla.Columns.Add("EstadoCliente", typeof(string));
                foreach (DataRow fila in tabla.Rows)
                {
                    fila["EstadoCliente"] =
                        Convert.ToBoolean(fila["Estatus"])
                        ? "Activo" : "Inactivo";
                }
                dgvClientes.DataSource = tabla;
                dgvClientes.Columns["Id_direccion"].Visible = false;
                dgvClientes.Columns["Estatus"].Visible = false;
                dgvClientes.Columns["Fecha_naci"].HeaderText =
                    "Fecha de nacimiento";
                dgvClientes.Columns["Correo"].HeaderText =
                    "Correo electrónico";
                dgvClientes.Columns["Tel_casa"].HeaderText =
                    "Teléfono de casa";
                dgvClientes.Columns["Tel_cel"].HeaderText =
                    "Teléfono celular";
                dgvClientes.Columns["EstadoCliente"].HeaderText =
                    "Estatus";
                dgvClientes.Columns["RFC"].HeaderText = "RFC";
                dgvClientes.Columns["Nombre"].HeaderText = "Nombre";
                dgvClientes.Columns["Apellido"].HeaderText = "Apellidos";
                dgvClientes.Columns["Calle"].HeaderText = "Calle";
                dgvClientes.Columns["Numero"].HeaderText = "Número";
                dgvClientes.Columns["Colonia"].HeaderText = "Colonia";
                dgvClientes.Columns["Ciudad"].HeaderText = "Ciudad";
                dgvClientes.Columns["Estado"].HeaderText = "Estado";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar los clientes:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        // VALIDAR CAMPOS
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text) ||
                string.IsNullOrWhiteSpace(comboBox2.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show(
                    "Completa todos los campos obligatorios.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            string rfc = textBox3.Text.Trim();
            if (rfc.Length != 12 && rfc.Length != 13)
            {
                MessageBox.Show(
                    "El RFC debe tener 12 o 13 caracteres.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (!MailAddress.TryCreate(textBox1.Text.Trim(), out _))
            {
                MessageBox.Show(
                    "Ingresa un correo electrónico válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(txtNumero.Text.Trim(), out int numero)
                || numero <= 0)
            {
                MessageBox.Show(
                    "El número del domicilio debe ser un entero positivo.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (dateTimePicker1.Value.Date >= DateTime.Today)
            {
                MessageBox.Show(
                    "La fecha de nacimiento debe ser anterior a hoy.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBox2.Text) &&
                textBox2.Text.Trim().Length > 10)
            {
                MessageBox.Show(
                    "El teléfono de casa no debe superar 10 caracteres.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBox6.Text) &&
                textBox6.Text.Trim().Length > 10)
            {
                MessageBox.Show(
                    "El teléfono celular no debe superar 10 caracteres.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        // VERIFICAR SI EL RFC YA EXISTE
        private bool ExisteRFC(string rfc)
        {
            using SqlConnection conexion = CrearConexion();
            conexion.Open();
            string consulta =
                "SELECT COUNT(*) FROM Cliente WHERE RFC = @rfc";
            if (rfcSeleccionado != null)
            {
                consulta += " AND RFC <> @actual";
            }
            using SqlCommand comando =
                new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@rfc", rfc);
            if (rfcSeleccionado != null)
            {
                comando.Parameters.AddWithValue(
                    "@actual", rfcSeleccionado);
            }
            return Convert.ToInt32(comando.ExecuteScalar()) > 0;
        }
        // GUARDAR CLIENTE
        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;
            try
            {
                string rfc = textBox3.Text.Trim().ToUpper();
                if (ExisteRFC(rfc))
                {
                    MessageBox.Show(
                        "Ya existe un cliente registrado con ese RFC.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                using SqlConnection conexion = CrearConexion();
                conexion.Open();
                using SqlTransaction transaccion =
                    conexion.BeginTransaction();
                try
                {
                    // Registrar dirección
                    string sqlDireccion = @"
                       INSERT INTO Direccion
                       (Estado, Ciudad, Calle, Numero, Colonia)
                       OUTPUT INSERTED.Id_direccion
                       VALUES
                       (@estado, @ciudad, @calle, @numero, @colonia);";
                    int idDireccion;
                    using (SqlCommand comando = new SqlCommand(
                        sqlDireccion, conexion, transaccion))
                    {
                        AgregarParametrosDireccion(comando);
                        idDireccion = Convert.ToInt32(
                            comando.ExecuteScalar());
                    }
                    // Registrar cliente
                    string sqlCliente = @"
                       INSERT INTO Cliente
                       (RFC, Nombre, Apellido, Fecha_naci,
                        Correo, Id_direccion, Fecha_reg, Hora, Estatus)
                       VALUES
                       (@rfc, @nombre, @apellido, @fecha,
                        @correo, @direccion, CAST(GETDATE() AS DATE),
                        CAST(GETDATE() AS TIME), 1);";
                    using (SqlCommand comando = new SqlCommand(
                        sqlCliente, conexion, transaccion))
                    {
                        AgregarParametrosCliente(comando);
                        comando.Parameters.AddWithValue(
                            "@direccion", idDireccion);
                        comando.ExecuteNonQuery();
                    }
                    // Registrar teléfonos
                    InsertarTelefono(conexion, transaccion, rfc);
                    transaccion.Commit();
                    MessageBox.Show(
                        "Cliente registrado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarClientes();
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
                    "Error al guardar el cliente:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        // EDITAR CLIENTE
        private void btnEditar_Click(object? sender, EventArgs e)
        {
            if (rfcSeleccionado == null)
            {
                MessageBox.Show(
                    "Selecciona un cliente de la tabla.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarCampos())
                return;
            try
            {
                string nuevoRFC = textBox3.Text.Trim().ToUpper();
                if (nuevoRFC != rfcSeleccionado &&
                    ExisteRFC(nuevoRFC))
                {
                    MessageBox.Show(
                        "El RFC indicado ya pertenece a otro cliente.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                using SqlConnection conexion = CrearConexion();
                conexion.Open();
                using SqlTransaction transaccion =
                    conexion.BeginTransaction();
                try
                {
                    // Actualizar dirección
                    string sqlDireccion = @"
                       UPDATE Direccion
                       SET Estado = @estado,
                           Ciudad = @ciudad,
                           Calle = @calle,
                           Numero = @numero,
                           Colonia = @colonia
                       WHERE Id_direccion = @id;";
                    using (SqlCommand comando = new SqlCommand(
                        sqlDireccion, conexion, transaccion))
                    {
                        AgregarParametrosDireccion(comando);
                        comando.Parameters.AddWithValue(
                            "@id", idDireccionSeleccionada);
                        comando.ExecuteNonQuery();
                    }
                    // Actualizar cliente
                    string sqlCliente = @"
                       UPDATE Cliente
                       SET RFC = @rfc,
                           Nombre = @nombre,
                           Apellido = @apellido,
                           Fecha_naci = @fecha,
                           Correo = @correo
                       WHERE RFC = @actual;";
                    using (SqlCommand comando = new SqlCommand(
                        sqlCliente, conexion, transaccion))
                    {
                        AgregarParametrosCliente(comando);
                        comando.Parameters.AddWithValue(
                            "@actual", rfcSeleccionado);
                        comando.ExecuteNonQuery();
                    }
                    // Actualizar teléfonos
                    using (SqlCommand eliminar = new SqlCommand(
                        "DELETE FROM Telefono WHERE RFC = @rfc",
                        conexion, transaccion))
                    {
                        eliminar.Parameters.AddWithValue(
                            "@rfc", rfcSeleccionado);
                        eliminar.ExecuteNonQuery();
                    }
                    InsertarTelefono(
                        conexion, transaccion, nuevoRFC);
                    transaccion.Commit();
                    MessageBox.Show(
                        "Cliente actualizado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarClientes();
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
                    "Error al editar el cliente:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        // DESACTIVAR CLIENTE
        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (rfcSeleccionado == null)
            {
                MessageBox.Show(
                    "Selecciona un cliente de la tabla.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas desactivar este cliente?",
                "Confirmar desactivación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;
            try
            {
                using SqlConnection conexion = CrearConexion();
                conexion.Open();
                string consulta = @"
                   UPDATE Cliente
                   SET Estatus = 0
                   WHERE RFC = @rfc;";
                using SqlCommand comando =
                    new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue(
                    "@rfc", rfcSeleccionado);
                comando.ExecuteNonQuery();
                MessageBox.Show(
                    "Cliente desactivado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LimpiarCampos();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al desactivar el cliente:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        // INSERTAR TELÉFONOS
        private void InsertarTelefono(
            SqlConnection conexion,
            SqlTransaction transaccion,
            string rfc)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) &&
                string.IsNullOrWhiteSpace(textBox6.Text))
                return;
            string consulta = @"
               INSERT INTO Telefono (Tel_casa, Tel_cel, RFC)
               VALUES (@casa, @celular, @rfc);";
            using SqlCommand comando =
                new SqlCommand(consulta, conexion, transaccion);
            comando.Parameters.AddWithValue(
                "@casa",
                string.IsNullOrWhiteSpace(textBox2.Text)
                    ? DBNull.Value : textBox2.Text.Trim());
            comando.Parameters.AddWithValue(
                "@celular",
                string.IsNullOrWhiteSpace(textBox6.Text)
                    ? DBNull.Value : textBox6.Text.Trim());
            comando.Parameters.AddWithValue("@rfc", rfc);
            comando.ExecuteNonQuery();
        }
        // PARÁMETROS DE DIRECCIÓN
        private void AgregarParametrosDireccion(SqlCommand comando)
        {
            comando.Parameters.AddWithValue(
                "@estado", comboBox2.Text.Trim());
            comando.Parameters.AddWithValue(
                "@ciudad", textBox8.Text.Trim());
            comando.Parameters.AddWithValue(
                "@calle", textBox7.Text.Trim());
            comando.Parameters.AddWithValue(
                "@numero", int.Parse(txtNumero.Text.Trim()));
            comando.Parameters.AddWithValue(
                "@colonia", textBox4.Text.Trim());
        }
        // PARÁMETROS DEL CLIENTE
        private void AgregarParametrosCliente(SqlCommand comando)
        {
            comando.Parameters.AddWithValue(
                "@rfc", textBox3.Text.Trim().ToUpper());
            comando.Parameters.AddWithValue(
                "@nombre", txtNombreUsuario.Text.Trim());
            comando.Parameters.AddWithValue(
                "@apellido", textBox5.Text.Trim());
            comando.Parameters.AddWithValue(
                "@fecha", dateTimePicker1.Value.Date);
            comando.Parameters.AddWithValue(
                "@correo", textBox1.Text.Trim());
        }
        // SELECCIONAR CLIENTE
        private void dgvClientes_CellClick(
            object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            DataGridViewRow fila =
                dgvClientes.Rows[e.RowIndex];
            rfcSeleccionado =
                fila.Cells["RFC"].Value.ToString();
            idDireccionSeleccionada =
                Convert.ToInt32(
                    fila.Cells["Id_direccion"].Value);
            textBox3.Text = rfcSeleccionado;
            txtNombreUsuario.Text =
                fila.Cells["Nombre"].Value.ToString();
            textBox5.Text =
                fila.Cells["Apellido"].Value.ToString();
            dateTimePicker1.Value =
                Convert.ToDateTime(fila.Cells["Fecha_naci"].Value);
            textBox1.Text =
                fila.Cells["Correo"].Value.ToString();
            textBox7.Text =
                fila.Cells["Calle"].Value.ToString();
            txtNumero.Text =
                fila.Cells["Numero"].Value.ToString();
            textBox4.Text =
                fila.Cells["Colonia"].Value.ToString();
            textBox8.Text =
                fila.Cells["Ciudad"].Value.ToString();
            comboBox2.Text =
                fila.Cells["Estado"].Value.ToString();
            textBox2.Text =
                fila.Cells["Tel_casa"].Value == DBNull.Value
                    ? "" : fila.Cells["Tel_casa"].Value.ToString();
            textBox6.Text =
                fila.Cells["Tel_cel"].Value == DBNull.Value
                    ? "" : fila.Cells["Tel_cel"].Value.ToString();
        }
        // LIMPIAR CAMPOS
        private void LimpiarCampos()
        {
            txtNombreUsuario.Clear();
            textBox5.Clear();
            textBox3.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox4.Clear();
            textBox8.Clear();
            txtNumero.Clear();
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Today.AddDays(-1);
            rfcSeleccionado = null;
            idDireccionSeleccionada = 0;
            dgvClientes.ClearSelection();
        }
    }
}