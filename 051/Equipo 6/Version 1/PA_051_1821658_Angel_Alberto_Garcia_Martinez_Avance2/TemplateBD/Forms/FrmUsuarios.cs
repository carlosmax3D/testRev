using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

public partial class FrmUsuarios : Form
{
    private readonly IUsuarioService _usuarioService;
    private int _idSeleccionado;
    private Usuario? _seleccionado;

    public FrmUsuarios(IUsuarioService usuarioService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
        dtpNacimiento.MaxDate = DateTime.Today;
        cboTipo.Items.AddRange(new object[] { TiposUsuario.Administrador, TiposUsuario.Operativo });
        cboTipo.SelectedItem = TiposUsuario.Operativo;
        dgvUsuarios.AutoGenerateColumns = false;
    }

    private void CargarUsuarios()
    {
        try
        {
            dgvUsuarios.DataSource = _usuarioService.ObtenerUsuarios(txtBuscar.Text, chkInactivos.Checked);
            dgvUsuarios.ClearSelection();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible cargar los usuarios.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private Usuario LeerFormulario()
    {
        if (string.IsNullOrWhiteSpace(txtNomina.Text))
        {
            throw new ArgumentException("Ningún campo puede quedar vacío.");
        }

        if (!int.TryParse(txtNomina.Text.Trim(), out int nomina))
        {
            throw new ArgumentException("El número de nómina debe ser un número entero.");
        }

        return new Usuario
        {
            IdUsuario = _idSeleccionado,
            TipoUsuario = cboTipo.SelectedItem?.ToString() ?? string.Empty,
            Correo = txtCorreo.Text,
            NombreCompleto = txtNombre.Text,
            NumeroNomina = nomina,
            FechaNacimiento = dtpNacimiento.Value.Date
        };
    }

    private void btnRegistrar_Click(object? sender, EventArgs e)
    {
        try
        {
            Usuario usuario = LeerFormulario();
            if (!Mensajes.Confirmar($"¿Registrar a {usuario.NombreCompleto.Trim()} como {usuario.TipoUsuario}?"))
            {
                return;
            }

            _usuarioService.RegistrarUsuario(usuario, txtClave.Text, txtConfirmar.Text);
            Mensajes.Informacion("Usuario registrado correctamente.");
            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible registrar el usuario.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private void btnActualizar_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_idSeleccionado <= 0)
            {
                Mensajes.Aviso("Seleccione un usuario de la lista.");
                return;
            }

            if (_seleccionado is { Activo: false })
            {
                Mensajes.Aviso("El usuario está dado de baja. Reactívelo para modificarlo.");
                return;
            }

            Usuario usuario = LeerFormulario();
            if (!Mensajes.Confirmar("¿Guardar los cambios del usuario seleccionado?"))
            {
                return;
            }

            _usuarioService.ActualizarUsuario(usuario, txtClave.Text, txtConfirmar.Text);
            Mensajes.Informacion("Usuario actualizado correctamente.");
            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible actualizar el usuario.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private void btnEliminar_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_idSeleccionado <= 0)
            {
                Mensajes.Aviso("Seleccione un usuario de la lista.");
                return;
            }

            if (_seleccionado is { Activo: false })
            {
                Mensajes.Aviso("El usuario ya está dado de baja.");
                return;
            }

            if (!Mensajes.Confirmar("¿Está seguro de dar de baja al usuario seleccionado?"))
            {
                return;
            }

            _usuarioService.EliminarUsuario(_idSeleccionado);
            Mensajes.Informacion("Usuario dado de baja; se conserva su historial.");
            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible dar de baja al usuario.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private void dgvUsuarios_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvUsuarios.Rows[e.RowIndex].DataBoundItem is not Usuario usuario)
        {
            return;
        }

        _idSeleccionado = usuario.IdUsuario;
        _seleccionado = usuario;
        txtCorreo.Text = usuario.Correo;
        txtNombre.Text = usuario.NombreCompleto;
        txtNomina.Text = usuario.NumeroNomina.ToString();
        dtpNacimiento.Value = usuario.FechaNacimiento > dtpNacimiento.MaxDate ? dtpNacimiento.MaxDate : usuario.FechaNacimiento;
        cboTipo.SelectedItem = usuario.TipoUsuario;
        txtClave.Clear();
        txtConfirmar.Clear();
        lblAuditoria.Text = $"{usuario.DescripcionRegistro}{Environment.NewLine}{usuario.DescripcionModificacion}";
    }

    private void btnReactivar_Click(object? sender, EventArgs e)
    {
        if (_seleccionado is null)
        {
            Mensajes.Aviso("Marque \"Mostrar dados de baja\" y seleccione un usuario dado de baja.");
            return;
        }

        try
        {
            if (!Mensajes.Confirmar($"¿Reactivar a {_seleccionado.NombreCompleto}? Podrá volver a iniciar sesión."))
            {
                return;
            }

            _usuarioService.ReactivarUsuario(_seleccionado);
            Mensajes.Informacion("Usuario reactivado correctamente.");
            LimpiarFormulario();
            CargarUsuarios();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible reactivar al usuario.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private void chkInactivos_CheckedChanged(object? sender, EventArgs e)
    {
        LimpiarFormulario();
        CargarUsuarios();
    }

    private void LimpiarFormulario()
    {
        _idSeleccionado = 0;
        _seleccionado = null;
        txtCorreo.Clear();
        txtClave.Clear();
        txtConfirmar.Clear();
        txtNombre.Clear();
        txtNomina.Clear();
        dtpNacimiento.Value = new DateTime(2000, 1, 1);
        cboTipo.SelectedItem = TiposUsuario.Operativo;
        lblAuditoria.Text = string.Empty;
        dgvUsuarios.ClearSelection();
        txtCorreo.Focus();
    }

    private void btnLimpiar_Click(object? sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void btnBuscar_Click(object? sender, EventArgs e)
    {
        // Al cambiar la lista se olvida el usuario que estaba seleccionado
        LimpiarFormulario();
        CargarUsuarios();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void FrmUsuarios_Load(object? sender, EventArgs e)
    {
        CargarUsuarios();
    }
}
