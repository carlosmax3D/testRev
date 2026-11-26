using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace TemplateCSharp.Forms
{
    public partial class FrmCancelaciones : Form
    {
        private readonly string cadenaConexion;
        private ComboBox cmbVenta;
        private ComboBox cmbUsuario;
        private TextBox txtMotivo;
        private DataGridView dgvCancelaciones;
        public FrmCancelaciones()
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
            iconButton1.Click += Guardar_Click;
            iconButton2.Click += Limpiar_Click;
            iconButton3.Click += Actualizar_Click;
            txtNombreUsuario.TextChanged +=
                (s, e) => CargarCancelaciones(txtNombreUsuario.Text.Trim());
            CargarUsuarios();
            CargarVentas();
            CargarCancelaciones();
        }
        private SqlConnection CrearConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
        private void ConfigurarFormulario()
        {
            Text = "Cancelaciones";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 250);
            lblNombreUsuario.Text = "Buscar cancelación:";
            txtNombreUsuario.PlaceholderText =
                "Buscar por folio o cliente";
            iconButton1.Text = "Cancelar venta";
            iconButton2.Text = "Limpiar";
            iconButton3.Text = "Actualizar";
            iconButton1.IconChar =
                FontAwesome.Sharp.IconChar.Save;
            iconButton2.IconChar =
                FontAwesome.Sharp.IconChar.Broom;
            iconButton3.IconChar =
                FontAwesome.Sharp.IconChar.Refresh;
            Label lblVenta = new Label();
            lblVenta.Text = "Venta:";
            lblVenta.Font =
                new Font("Century Gothic", 10);
            lblVenta.Location = new Point(41, 175);
            lblVenta.AutoSize = true;
            panel1.Controls.Add(lblVenta);
            cmbVenta = new ComboBox();
            cmbVenta.Location = new Point(41, 205);
            cmbVenta.Size = new Size(500, 32);
            cmbVenta.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cmbVenta.Font = new Font("Segoe UI", 10);
            panel1.Controls.Add(cmbVenta);
            Label lblUsuario = new Label();
            lblUsuario.Text = "Administrador:";
            lblUsuario.Font =
                new Font("Century Gothic", 10);
            lblUsuario.Location = new Point(600, 89);
            lblUsuario.AutoSize = true;
            panel1.Controls.Add(lblUsuario);
            cmbUsuario = new ComboBox();
            cmbUsuario.Location = new Point(600, 115);
            cmbUsuario.Size = new Size(350, 32);
            cmbUsuario.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cmbUsuario.Font = new Font("Segoe UI", 10);
            panel1.Controls.Add(cmbUsuario);
            Label lblMotivo = new Label();
            lblMotivo.Text = "Motivo de cancelación:";
            lblMotivo.Font =
                new Font("Century Gothic", 10);
            lblMotivo.Location = new Point(600, 175);
            lblMotivo.AutoSize = true;
            panel1.Controls.Add(lblMotivo);
            txtMotivo = new TextBox();
            txtMotivo.Location = new Point(600, 205);
            txtMotivo.Size = new Size(350, 60);
            txtMotivo.Multiline = true;
            txtMotivo.MaxLength = 250;
            txtMotivo.Font = new Font("Segoe UI", 10);
            panel1.Controls.Add(txtMotivo);
            dgvCancelaciones = new DataGridView();
            dgvCancelaciones.Location =
                new Point(25, 400);
            dgvCancelaciones.Size =
                new Size(1490, 420);
            dgvCancelaciones.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right |
                AnchorStyles.Bottom;
            dgvCancelaciones.BackgroundColor = Color.White;
            dgvCancelaciones.BorderStyle = BorderStyle.None;
            dgvCancelaciones.AllowUserToAddRows = false;
            dgvCancelaciones.AllowUserToDeleteRows = false;
            dgvCancelaciones.ReadOnly = true;
            dgvCancelaciones.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvCancelaciones.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvCancelaciones.MultiSelect = false;
            dgvCancelaciones.RowHeadersVisible = false;
            dgvCancelaciones.Font =
                new Font("Segoe UI", 9);
            dgvCancelaciones.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(40, 65, 100);
            dgvCancelaciones.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            dgvCancelaciones.EnableHeadersVisualStyles = false;
            Controls.Add(dgvCancelaciones);
        }
        // CARGAR ADMINISTRADORES
        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection conexion = CrearConexion())
                {
                    string consulta = @"
                       SELECT U.Id_user,
                              U.Nombre + ' ' + U.Apellido AS NombreCompleto
                       FROM User_ U
                       INNER JOIN Tipo T
                           ON U.Id_tipo = T.Id_tipo
                       WHERE U.Estatus = 1
                       AND LOWER(LTRIM(RTRIM(T.Nombre))) = 'administrador'
                       ORDER BY U.Nombre";
                    SqlDataAdapter da =
                        new SqlDataAdapter(consulta, conexion);
                    DataTable tabla = new DataTable();
                    da.Fill(tabla);
                    cmbUsuario.DataSource = tabla;
                    cmbUsuario.DisplayMember = "NombreCompleto";
                    cmbUsuario.ValueMember = "Id_user";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar administradores: " + ex.Message);
            }
        }
        // CARGAR VENTAS QUE TODAVÍA NO ESTÁN CANCELADAS
        private void CargarVentas()
        {
            try
            {
                using (SqlConnection conexion = CrearConexion())
                {
                    string consulta = @"
                       SELECT
                           V.Id_venta,
                           V.Id_venta + ' - ' +
                           C.Nombre + ' ' + C.Apellido AS Descripcion
                       FROM Venta V
                       INNER JOIN Cliente C
                           ON V.RFC = C.RFC
                       WHERE NOT EXISTS
                       (
                           SELECT 1
                           FROM Cancelacion CA
                           WHERE CA.Id_venta = V.Id_venta
                       )
                       ORDER BY V.Fecha DESC, V.Hora DESC";
                    SqlDataAdapter da =
                        new SqlDataAdapter(consulta, conexion);
                    DataTable tabla = new DataTable();
                    da.Fill(tabla);
                    cmbVenta.DataSource = tabla;
                    cmbVenta.DisplayMember = "Descripcion";
                    cmbVenta.ValueMember = "Id_venta";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar ventas: " + ex.Message);
            }
        }
        // CARGAR HISTORIAL DE CANCELACIONES
        private void CargarCancelaciones(string busqueda = "")
        {
            try
            {
                using (SqlConnection conexion = CrearConexion())
                {
                    string consulta = @"
                       SELECT
                           CA.Id_cancelacion AS [ID Cancelación],
                           CA.Id_venta AS [Folio de venta],
                           C.Nombre + ' ' + C.Apellido AS [Cliente],
                           CA.Motivo AS [Motivo],
                           CA.Fecha AS [Fecha],
                           CA.Hora AS [Hora],
                           U.Nombre + ' ' + U.Apellido AS [Administrador]
                       FROM Cancelacion CA
                       INNER JOIN Venta V
                           ON CA.Id_venta = V.Id_venta
                       INNER JOIN Cliente C
                           ON V.RFC = C.RFC
                       INNER JOIN User_ U
                           ON CA.Id_user = U.Id_user
                       WHERE
                           CA.Id_venta LIKE @Busqueda
                           OR C.Nombre LIKE @Busqueda
                           OR C.Apellido LIKE @Busqueda
                       ORDER BY CA.Id_cancelacion DESC";
                    SqlDataAdapter da =
                        new SqlDataAdapter(consulta, conexion);
                    da.SelectCommand.Parameters.Add(
                        "@Busqueda", SqlDbType.VarChar, 100).Value =
                        "%" + busqueda + "%";
                    DataTable tabla = new DataTable();
                    da.Fill(tabla);
                    dgvCancelaciones.DataSource = tabla;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar cancelaciones: " + ex.Message,
                    "Cancelaciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        // REGISTRAR CANCELACIÓN
        private void Guardar_Click(object sender, EventArgs e)
        {
            if (cmbVenta.SelectedValue == null)
            {
                MessageBox.Show(
                    "Selecciona una venta para cancelar.");
                return;
            }
            if (cmbUsuario.SelectedValue == null)
            {
                MessageBox.Show(
                    "No hay un administrador disponible.");
                return;
            }
            string motivo = txtMotivo.Text.Trim();
            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show(
                    "Escribe el motivo de la cancelación.");
                txtMotivo.Focus();
                return;
            }
            string idVenta = cmbVenta.SelectedValue.ToString();
            int idUsuario =
                Convert.ToInt32(cmbUsuario.SelectedValue);
            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de cancelar la venta " +
                idVenta + "?\n\nEsta operación desactivará sus boletos.",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (respuesta != DialogResult.Yes)
                return;
            try
            {
                using (SqlConnection conexion = CrearConexion())
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "sp_CancelarVenta", conexion))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;
                        cmd.Parameters.Add(
                            "@Id_venta", SqlDbType.VarChar, 50).Value =
                            idVenta;
                        cmd.Parameters.Add(
                            "@Id_user", SqlDbType.Int).Value =
                            idUsuario;
                        cmd.Parameters.Add(
                            "@Motivo", SqlDbType.VarChar, 250).Value =
                            motivo;
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show(
                    "La venta fue cancelada correctamente.",
                    "Cancelación registrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtMotivo.Clear();
                CargarVentas();
                CargarCancelaciones(txtNombreUsuario.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cancelar la venta:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        // LIMPIAR CAMPOS
        private void Limpiar_Click(object sender, EventArgs e)
        {
            txtNombreUsuario.Clear();
            txtMotivo.Clear();
            if (cmbVenta.Items.Count > 0)
                cmbVenta.SelectedIndex = 0;
            if (cmbUsuario.Items.Count > 0)
                cmbUsuario.SelectedIndex = 0;
            CargarCancelaciones();
        }
        // ACTUALIZAR INFORMACIÓN
        private void Actualizar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
            CargarVentas();
            CargarCancelaciones(txtNombreUsuario.Text.Trim());
            MessageBox.Show(
                "Información actualizada.",
                "Cancelaciones",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}