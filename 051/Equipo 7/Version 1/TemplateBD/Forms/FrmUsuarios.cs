using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmUsuarios : Form
{
    private readonly IUsuarioService _usuarioService;
    private int _idUsuarioSeleccionado = 0;

    public FrmUsuarios(IUsuarioService usuarioService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
    }

    private void FrmUsuarios_Load(object sender, EventArgs e)
    {
        cmbRol.Items.Clear();
        cmbRol.Items.AddRange(["Administrador", "Operativo"]);
        cmbRol.SelectedIndex = 1;
        CargarUsuarios();
    }

    private void CargarUsuarios()
    {
        try
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = _usuarioService.ObtenerUsuarios();

            string[] ocultas = ["Contrasena", "UsuarioAuditoria", "NombreCompleto"];
            foreach (string col in ocultas)
            {
                if (dgvUsuarios.Columns[col] != null)
                    dgvUsuarios.Columns[col].Visible = false;
            }

            if (dgvUsuarios.Columns["FechaNacimiento"] != null)
                dgvUsuarios.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd-MMM-yyyy";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnRegistrar_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario nuevo = ObtenerDatosFormulario();
            _usuarioService.RegistrarUsuario(nuevo);

            MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario editado = ObtenerDatosFormulario();
            editado.IdUsuario = _idUsuarioSeleccionado;

            _usuarioService.ActualizarUsuario(editado);

            MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (_idUsuarioSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un usuario de la tabla para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _usuarioService.EliminarUsuario(_idUsuarioSeleccionado);
                MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                CargarUsuarios();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        if (dgvUsuarios.Rows[e.RowIndex].DataBoundItem is Usuario u)
        {
            _idUsuarioSeleccionado = u.IdUsuario;
            txtNomina.Text = u.NumeroNomina;
            txtNombre.Text = u.Nombre;
            txtApellidoP.Text = u.ApellidoPaterno;
            txtApellidoM.Text = u.ApellidoMaterno;
            txtCorreo.Text = u.Correo;
            txtContrasena.Clear(); // Se deja vacía por seguridad; solo se valida si escribe una nueva
            dtpFechaNacimiento.Value = u.FechaNacimiento;
            cmbRol.SelectedItem = u.TipoUsuario;
        }
    }

    private Usuario ObtenerDatosFormulario()
    {
        string rol = cmbRol.SelectedItem?.ToString() ?? "Operativo";
        Usuario u = Usuario.CrearInstanciaPorRol(rol);

        u.NumeroNomina = txtNomina.Text.Trim();
        u.Nombre = txtNombre.Text.Trim();
        u.ApellidoPaterno = txtApellidoP.Text.Trim();
        u.ApellidoMaterno = txtApellidoM.Text.Trim();
        u.Correo = txtCorreo.Text.Trim();
        u.Contrasena = txtContrasena.Text.Trim();
        u.FechaNacimiento = dtpFechaNacimiento.Value.Date;
        return u;
    }

    private void LimpiarFormulario()
    {
        _idUsuarioSeleccionado = 0;
        txtNomina.Clear();
        txtNombre.Clear();
        txtApellidoP.Clear();
        txtApellidoM.Clear();
        txtCorreo.Clear();
        txtContrasena.Clear();
        dtpFechaNacimiento.Value = DateTime.Today.AddYears(-18);
        cmbRol.SelectedIndex = 1;
        txtNomina.Focus();
    }
}