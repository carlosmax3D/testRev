using System;
using System.Windows.Forms;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

public partial class FrmUsuarios : Form
{
    private const int EdadMinima = 15;

    private readonly UsuarioService _usuarioService;
    private readonly SesionUsuario _sesion;

    public FrmUsuarios(UsuarioService usuarioService, SesionUsuario sesion)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
        _sesion = sesion;
    }

    private void FrmUsuarios_Load(object sender, EventArgs e)
    {
        CargarUsuarios();
        LimpiarFormulario();
        AplicarPermisos();
    }

    private void AplicarPermisos()
    {
        bool esAdmin = _sesion.EsAdmin ||
                      (_sesion.UsuarioActual != null &&
                       string.Equals(_sesion.UsuarioActual.Rol, "Admin", StringComparison.OrdinalIgnoreCase));

        // Si NO es Admin (por ejemplo, Operativo), bloqueamos las acciones de edición
        if (!esAdmin)
        {
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnNuevo.Enabled = false;

            txtNombre.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtNomina.ReadOnly = true;
            txtContrasena.ReadOnly = true;
            cmbRol.Enabled = false;
            dtpFechaNac.Enabled = false;
        }
    }

    private bool ValidarPermisoAdmin()
    {
        bool esAdmin = _sesion.EsAdmin ||
                      (_sesion.UsuarioActual != null &&
                       string.Equals(_sesion.UsuarioActual.Rol, "Admin", StringComparison.OrdinalIgnoreCase));

        if (!esAdmin)
        {
            MessageBox.Show(
                "No tienes permisos de Administrador para realizar cambios en los usuarios.",
                "Acceso Denegado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    private static int CalcularEdad(DateTime fechaNacimiento, DateTime fechaReferencia)
    {
        int edad = fechaReferencia.Year - fechaNacimiento.Year;
        if (fechaNacimiento.Date > fechaReferencia.Date.AddYears(-edad))
        {
            edad--;
        }
        return edad;
    }

    private bool ValidarEdadMinima()
    {
        DateTime fechaNac = dtpFechaNac.Value.Date;
        DateTime hoy = DateTime.Today;

        if (fechaNac > hoy)
        {
            MessageBox.Show(
                "La fecha de nacimiento no puede ser futura.",
                "Fecha inválida",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            dtpFechaNac.Focus();
            return false;
        }

        if (CalcularEdad(fechaNac, hoy) < EdadMinima)
        {
            MessageBox.Show(
                $"El usuario debe tener al menos {EdadMinima} años cumplidos.",
                "Edad no permitida",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            dtpFechaNac.Focus();
            return false;
        }

        return true;
    }

    private void CargarUsuarios()
    {
        try
        {
            dgvUsuarios.DataSource = _usuarioService.ObtenerUsuarios();

            if (dgvUsuarios.Columns["Contrasena"] != null)
                dgvUsuarios.Columns["Contrasena"].Visible = false;
            if (dgvUsuarios.Columns["Sal"] != null)
                dgvUsuarios.Columns["Sal"].Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"No fue posible cargar los usuarios.\n\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnNuevo_Click(object sender, EventArgs e)
    {
        if (!ValidarPermisoAdmin()) return;
        LimpiarFormulario();
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        if (!ValidarPermisoAdmin()) return;
        if (!ValidarEdadMinima()) return;

        try
        {
            Usuario usuario = new()
            {
                NombreCompleto = txtNombre.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                NumNomina = txtNomina.Text.Trim(),
                Contrasena = txtContrasena.Text.Trim(),
                Rol = cmbRol.SelectedItem?.ToString() ?? "Admin",
                FechaNacimiento = dtpFechaNac.Value.Date,
                IdUsuarioRegistro = _sesion.UsuarioActual?.IdUsuario
            };

            _usuarioService.GuardarUsuario(usuario, esNuevo: true);

            MessageBox.Show(
                "Usuario registrado correctamente con contraseña segura.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error de Validación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void btnActualizar_Click(object sender, EventArgs e)
    {
        if (!ValidarPermisoAdmin()) return;
        if (!ValidarEdadMinima()) return;

        try
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Seleccione un usuario de la lista para actualizar.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Usuario usuario = new()
            {
                IdUsuario = id,
                NombreCompleto = txtNombre.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                NumNomina = txtNomina.Text.Trim(),
                Contrasena = txtContrasena.Text.Trim(),
                Rol = cmbRol.SelectedItem?.ToString() ?? "Admin",
                FechaNacimiento = dtpFechaNac.Value.Date
            };

            _usuarioService.GuardarUsuario(usuario, esNuevo: false);

            MessageBox.Show(
                "Usuario actualizado correctamente.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error de Validación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        if (!ValidarPermisoAdmin()) return;

        try
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Seleccione un usuario de la tabla para eliminar.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_sesion.UsuarioActual != null && _sesion.UsuarioActual.IdUsuario == id)
            {
                MessageBox.Show("No puedes eliminar la cuenta con la que iniciaste sesión.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar este usuario?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            _usuarioService.BorrarUsuario(id);

            MessageBox.Show(
                "Usuario eliminado correctamente.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

        txtId.Text = fila.Cells["IdUsuario"].Value?.ToString() ?? string.Empty;
        txtNombre.Text = fila.Cells["NombreCompleto"].Value?.ToString() ?? string.Empty;
        txtEmail.Text = fila.Cells["Email"].Value?.ToString() ?? string.Empty;
        txtNomina.Text = fila.Cells["NumNomina"].Value?.ToString() ?? string.Empty;
        cmbRol.SelectedItem = fila.Cells["Rol"].Value?.ToString() ?? "Admin";

        if (DateTime.TryParse(fila.Cells["FechaNacimiento"].Value?.ToString(), out DateTime fecha))
        {
            dtpFechaNac.Value = fecha;
        }

        txtContrasena.Clear();
    }

    private void LimpiarFormulario()
    {
        txtId.Clear();
        txtNombre.Clear();
        txtEmail.Clear();
        txtNomina.Clear();
        txtContrasena.Clear();
        if (cmbRol.Items.Count > 0) cmbRol.SelectedIndex = 0;
        dtpFechaNac.Value = DateTime.Today.AddYears(-18);
        txtNombre.Focus();
    }
}