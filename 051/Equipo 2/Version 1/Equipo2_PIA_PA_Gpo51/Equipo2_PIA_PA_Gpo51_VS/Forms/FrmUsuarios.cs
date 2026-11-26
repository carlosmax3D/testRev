using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;

namespace TemplateCSharp.Forms;

public class FrmUsuarios : Form
{
    private readonly Ejecutor _ejecutor;
    private int _idUsuarioSel = 0;

    private TextBox txtEmail = new() { Left = 125, Top = 20, Width = 210 };
    private TextBox txtPassword = new() { Left = 125, Top = 55, Width = 210, UseSystemPasswordChar = true };
    private TextBox txtNombre = new() { Left = 125, Top = 90, Width = 210 };
    private TextBox txtApellidos = new() { Left = 125, Top = 125, Width = 210 };
    private TextBox txtNomina = new() { Left = 470, Top = 20, Width = 210 };
    private DateTimePicker dtpFechaNac = new() { Left = 470, Top = 55, Width = 210, Format = DateTimePickerFormat.Custom, CustomFormat = "dd-MMM-yyyy" };
    private ComboBox cmbTipo = new() { Left = 470, Top = 90, Width = 210, DropDownStyle = ComboBoxStyle.DropDownList };

    private Button btnGuardar = new() { Text = "Registrar", Left = 20, Top = 170, Width = 110, Height = 32, BackColor = Color.LightGreen };
    private Button btnEditar = new() { Text = "Modificar", Left = 140, Top = 170, Width = 110, Height = 32, BackColor = Color.LightSkyBlue };
    private Button btnEliminar = new() { Text = "Eliminar", Left = 260, Top = 170, Width = 110, Height = 32, BackColor = Color.LightCoral };
    private Button btnLimpiar = new() { Text = "Limpiar", Left = 380, Top = 170, Width = 100, Height = 32 };

    private DataGridView dgvUsuarios = new() { Left = 20, Top = 220, Width = 740, Height = 220, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

    public FrmUsuarios(Ejecutor ejecutor)
    {
        _ejecutor = ejecutor;
        ConfigurarInterfaz();
        this.Load += (s, e) => CargarUsuarios();
    }

    private void ConfigurarInterfaz()
    {
        this.Text = "Administración de Usuarios del Sistema";
        this.Size = new Size(800, 495);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        cmbTipo.Items.AddRange(new string[] { "Administrador", "Operativo" });
        cmbTipo.SelectedIndex = 1;

        Controls.AddRange(new Control[] {
            new Label() { Text = "Correo:", Left = 20, Top = 23, Width = 100 }, txtEmail,
            new Label() { Text = "Contraseña:", Left = 20, Top = 58, Width = 100 }, txtPassword,
            new Label() { Text = "Nombre:", Left = 20, Top = 93, Width = 100 }, txtNombre,
            new Label() { Text = "Apellidos:", Left = 20, Top = 128, Width = 100 }, txtApellidos,
            new Label() { Text = "No. Nómina:", Left = 365, Top = 23, Width = 100 }, txtNomina,
            new Label() { Text = "Nacimiento:", Left = 365, Top = 58, Width = 100 }, dtpFechaNac,
            new Label() { Text = "Rol / Tipo:", Left = 365, Top = 93, Width = 100 }, cmbTipo,
            btnGuardar, btnEditar, btnEliminar, btnLimpiar, dgvUsuarios
        });

        btnGuardar.Click += BtnGuardar_Click;
        btnEditar.Click += BtnEditar_Click;
        btnEliminar.Click += BtnEliminar_Click;
        btnLimpiar.Click += (s, e) => LimpiarCampos();
        dgvUsuarios.CellClick += DgvUsuarios_CellClick;
    }

    private bool ValidarCampos()
    {
        if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) ||
            string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellidos.Text) ||
            string.IsNullOrWhiteSpace(txtNomina.Text))
        {
            MessageBox.Show("Ningún campo puede quedar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
        {
            MessageBox.Show("Ingrese un correo electrónico válido.", "Validación de Correo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        string pass = txtPassword.Text;
        bool tieneLongitud = pass.Length >= 8;
        bool tieneMayuscula = pass.Any(char.IsUpper);
        bool tieneMinuscula = pass.Any(char.IsLower);
        bool tieneEspecial = pass.Any(c => !char.IsLetterOrDigit(c));

        if (!tieneLongitud || !tieneMayuscula || !tieneMinuscula || !tieneEspecial)
        {
            MessageBox.Show("La contraseña debe tener al menos 8 caracteres e incluir una mayúscula, una minúscula y un carácter especial.", "Contraseña Débil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }


    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        if (!ValidarCampos()) return;

        SqlParameter[] param = {
            new SqlParameter("@Email", txtEmail.Text.Trim()),
            new SqlParameter("@Password", txtPassword.Text.Trim()),
            new SqlParameter("@Nombre", txtNombre.Text.Trim()),
            new SqlParameter("@Apellidos", txtApellidos.Text.Trim()),
            new SqlParameter("@NumNomina", txtNomina.Text.Trim()),
            new SqlParameter("@FechaNac", dtpFechaNac.Value.Date),
            new SqlParameter("@TipoUsuario", cmbTipo.Text),
            new SqlParameter("@UsuarioRegistro", SesionActual.IdUsuario == 0 ? 1 : SesionActual.IdUsuario)
        };

        if (_ejecutor.Ejecutar("sp_RegistrarUsuario", param))
        {
            MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
            CargarUsuarios();
        }
    }

    private void BtnEditar_Click(object? sender, EventArgs e)
    {
        if (_idUsuarioSel == 0)
        {
            MessageBox.Show("Seleccione un usuario de la lista para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!ValidarCampos()) return;

        SqlParameter[] param = {
            new SqlParameter("@ID_Usuario", _idUsuarioSel),
            new SqlParameter("@Email", txtEmail.Text.Trim()),
            new SqlParameter("@Password", txtPassword.Text.Trim()),
            new SqlParameter("@Nombre", txtNombre.Text.Trim()),
            new SqlParameter("@Apellidos", txtApellidos.Text.Trim()),
            new SqlParameter("@NumNomina", txtNomina.Text.Trim()),
            new SqlParameter("@FechaNac", dtpFechaNac.Value.Date),
            new SqlParameter("@TipoUsuario", cmbTipo.Text)
        };

        if (_ejecutor.Ejecutar("sp_EditarUsuario", param))
        {
            MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
            CargarUsuarios();
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
        if (_idUsuarioSel == 0)
        {
            MessageBox.Show("Seleccione un usuario de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (_idUsuarioSel == SesionActual.IdUsuario)
        {
            MessageBox.Show("No puedes eliminar tu propio usuario en sesión activa.", "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            SqlParameter[] param = { new SqlParameter("@ID_Usuario", _idUsuarioSel) };
            if (_ejecutor.Ejecutar("sp_EliminarUsuario", param))
            {
                MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarUsuarios();
            }
        }
    }

    private void DgvUsuarios_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];

        _idUsuarioSel = Convert.ToInt32(row.Cells["ID_Usuario"].Value);
        txtNomina.Text = row.Cells["NumNomina"].Value?.ToString() ?? "";
        txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
        txtApellidos.Text = row.Cells["Apellidos"].Value?.ToString() ?? "";
        txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
        cmbTipo.Text = row.Cells["TipoUsuario"].Value?.ToString() ?? "Operativo";
        txtPassword.Clear();
    }

    private void LimpiarCampos()
    {
        _idUsuarioSel = 0;
        txtEmail.Clear(); txtPassword.Clear(); txtNombre.Clear(); txtApellidos.Clear(); txtNomina.Clear();
        cmbTipo.SelectedIndex = 1;
    }

    private void CargarUsuarios()
    {
        dgvUsuarios.DataSource = _ejecutor.ConsultarSP("sp_ConsultarUsuarios");
        if (dgvUsuarios.Columns.Contains("ID_Usuario")) dgvUsuarios.Columns["ID_Usuario"].Visible = false;
        if (dgvUsuarios.Columns.Contains("NumNomina")) dgvUsuarios.Columns["NumNomina"].HeaderText = "No. Nómina";
        if (dgvUsuarios.Columns.Contains("TipoUsuario")) dgvUsuarios.Columns["TipoUsuario"].HeaderText = "Rol";
        if (dgvUsuarios.Columns.Contains("FechaRegistroFormateada")) dgvUsuarios.Columns["FechaRegistroFormateada"].HeaderText = "Fecha Registro";
    }
}
