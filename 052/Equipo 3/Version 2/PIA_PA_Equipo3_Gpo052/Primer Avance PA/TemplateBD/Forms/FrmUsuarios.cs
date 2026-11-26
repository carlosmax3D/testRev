using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmUsuarios : FormBase
{
    private readonly IUsuarioService _usuarioService;
    private int _idUsuarioSeleccionado = 0;

    public FrmUsuarios(IUsuarioService usuarioService)
        : base("Gestión de Usuarios", 820, 520)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
        ConfigurarBotones();
    }

    private void ConfigurarBotones()
    {
        btnEditar.Click += btnEditar_Click;
        btnEliminar.Click += btnEliminar_Click;
        btnNuevo.Click += (s, e) => LimpiarFormulario();
        dgvUsuarios.CellClick += dgvUsuarios_CellClick;
    }

    private void FrmUsuarios_Load(object sender, EventArgs e) => CargarUsuarios();

    private void CargarUsuarios()
    {
        try { dgvUsuarios.DataSource = _usuarioService.ObtenerUsuarios(); }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario u = cmbTipoUsuario.SelectedIndex == 0 ? new Administrador() : new Operativo();
            u.CorreoElectronico = txtCorreo.Text.Trim();
            u.Contrasena = txtContrasena.Text;
            u.Nombre = txtNombre.Text.Trim();
            u.ApellidoPaterno = txtPaterno.Text.Trim();
            u.ApellidoMaterno = txtMaterno.Text.Trim();
            u.NumeroNomina = txtNomina.Text.Trim();
            u.FechaNacimiento = dtpFechaNac.Value;

            _usuarioService.CrearUsuario(u);
            MessageBox.Show("Usuario guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
        {
            MessageBox.Show("El correo o el número de nómina ya están registrados en otro usuario.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void btnEditar_Click(object sender, EventArgs e)
    {
        if (_idUsuarioSeleccionado == 0) { MessageBox.Show("Selecciona un usuario de la tabla."); return; }
        try
        {
            Usuario u = cmbTipoUsuario.SelectedIndex == 0 ? new Administrador() : new Operativo();
            u.IdUsuario = _idUsuarioSeleccionado;
            u.CorreoElectronico = txtCorreo.Text.Trim();
            u.Contrasena = txtContrasena.Text;
            u.Nombre = txtNombre.Text.Trim();
            u.ApellidoPaterno = txtPaterno.Text.Trim();
            u.ApellidoMaterno = txtMaterno.Text.Trim();
            u.NumeroNomina = txtNomina.Text.Trim();
            u.FechaNacimiento = dtpFechaNac.Value;

            _usuarioService.ActualizarUsuario(u);
            MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
        {
            MessageBox.Show("El correo o la nómina ya pertenecen a otro usuario.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        if (_idUsuarioSeleccionado == 0) { MessageBox.Show("Selecciona un usuario de la tabla."); return; }
        if (MessageBox.Show("¿Seguro que deseas eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            try
            {
                _usuarioService.EliminarUsuario(_idUsuarioSeleccionado);
                MessageBox.Show("Usuario eliminado.");
                LimpiarFormulario();
                CargarUsuarios();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Bloqueo de seguridad si un Operativo intenta borrar
                MessageBox.Show(ex.Message, "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("No se puede eliminar al usuario porque tiene ventas o clientes registrados a su nombre.", "Error de Integridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }

    private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var fila = dgvUsuarios.Rows[e.RowIndex];
        
        // Asumiendo que el DataSource es una List<Usuario>, podemos extraer el objeto directo
        if (fila.DataBoundItem is Usuario u)
        {
            _idUsuarioSeleccionado = u.IdUsuario;
            txtCorreo.Text = u.CorreoElectronico;
            txtContrasena.Text = u.Contrasena;
            txtNombre.Text = u.Nombre;
            txtPaterno.Text = u.ApellidoPaterno;
            txtMaterno.Text = u.ApellidoMaterno;
            txtNomina.Text = u.NumeroNomina;
            dtpFechaNac.Value = u.FechaNacimiento;
            cmbTipoUsuario.SelectedItem = u.TipoUsuario;
        }
    }

    private void LimpiarFormulario()
    {
        _idUsuarioSeleccionado = 0;
        txtCorreo.Clear(); txtContrasena.Clear(); txtNombre.Clear();
        txtPaterno.Clear(); txtMaterno.Clear(); txtNomina.Clear();
        cmbTipoUsuario.SelectedIndex = 0;
        dtpFechaNac.Value = DateTime.Now.AddYears(-18);
        txtCorreo.Focus();
    }
}
