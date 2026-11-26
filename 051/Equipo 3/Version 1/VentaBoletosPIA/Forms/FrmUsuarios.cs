using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Models.Lookups;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

public partial class FrmUsuarios : Form
{
    private readonly IUsuarioService _usuarios;
    private readonly ICatalogoService _catalogos;

    private int _idSeleccionado;

    public FrmUsuarios(IUsuarioService usuarios, ICatalogoService catalogos)
    {
        _usuarios = usuarios;
        _catalogos = catalogos;

        InitializeComponent();
        Ui.Estilizar(dgvUsuarios);

        dtpNacimiento.MaxDate = DateTime.Today;
        dtpNacimiento.Value = DateTime.Today.AddYears(-25);

        Load += (_, _) =>
        {
            Ui.Ejecutar(this, () =>
                Ui.LlenarCombo(cmbTipo, _catalogos.ObtenerTiposUsuario(), nameof(CatalogoItem.Nombre), nameof(CatalogoItem.Id)));
            Recargar();
        };
    }

    private void Recargar()
    {
        Ui.Ejecutar(this, () => dgvUsuarios.DataSource = _usuarios.ObtenerUsuarios());
        Limpiar();
    }

    private void BtnNuevo_Click(object? sender, EventArgs e) => Limpiar();

    private void Limpiar()
    {
        _idSeleccionado = 0;
        txtNombre.Clear();
        txtNomina.Clear();
        txtCorreo.Clear();
        txtContrasena.Clear();
        txtContrasena.Enabled = true;
        dtpNacimiento.Value = DateTime.Today.AddYears(-25);

        if (cmbTipo.Items.Count > 0)
        {
            cmbTipo.SelectedIndex = 0;
        }

        dgvUsuarios.ClearSelection();
        txtNombre.Focus();
    }

    private void DgvUsuarios_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvUsuarios.CurrentRow?.DataBoundItem is not Usuario u || !dgvUsuarios.CurrentRow.Selected)
        {
            return;
        }

        _idSeleccionado = u.IdUsuario;
        txtNombre.Text = u.NombreCompleto;
        txtNomina.Text = u.NumNomina;
        txtCorreo.Text = u.Correo;
        dtpNacimiento.Value = u.FechaNacimiento < dtpNacimiento.MinDate ? dtpNacimiento.MinDate : u.FechaNacimiento;
        cmbTipo.SelectedValue = u.IdTipoUsuario;
        txtContrasena.Clear();
        txtContrasena.Enabled = false;
    }

    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        Usuario usuario = new()
        {
            IdUsuario = _idSeleccionado,
            NombreCompleto = txtNombre.Text,
            NumNomina = txtNomina.Text,
            FechaNacimiento = dtpNacimiento.Value.Date,
            Correo = txtCorreo.Text,
            IdTipoUsuario = cmbTipo.SelectedValue is int id ? id : 0
        };

        bool nuevo = _idSeleccionado == 0;

        bool ok = Ui.Ejecutar(this, () =>
        {
            if (nuevo)
            {
                _usuarios.CrearUsuario(usuario, txtContrasena.Text);
            }
            else
            {
                _usuarios.ActualizarUsuario(usuario);
            }
        });

        if (ok)
        {
            Ui.Aviso(this, nuevo ? "Usuario registrado correctamente." : "Usuario actualizado correctamente.");
            Recargar();
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
        if (_idSeleccionado == 0)
        {
            Ui.Aviso(this, "Seleccione un usuario de la lista.");
            return;
        }

        if (!Ui.Confirmar(this, "¿Desea dar de baja al usuario seleccionado? (baja lógica)"))
        {
            return;
        }

        int id = _idSeleccionado;

        if (Ui.Ejecutar(this, () => _usuarios.EliminarUsuario(id)))
        {
            Ui.Aviso(this, "Usuario dado de baja.");
            Recargar();
        }
    }

    private void BtnContrasena_Click(object? sender, EventArgs e)
    {
        if (_idSeleccionado == 0)
        {
            Ui.Aviso(this, "Seleccione un usuario de la lista.");
            return;
        }

        string? nueva = Ui.PedirTexto(this, "Cambiar contraseña", "Nueva contraseña:", true);

        if (nueva is null)
        {
            return;
        }

        int id = _idSeleccionado;

        if (Ui.Ejecutar(this, () => _usuarios.CambiarContrasena(id, nueva)))
        {
            Ui.Aviso(this, "Contraseña actualizada.");
        }
    }
}
