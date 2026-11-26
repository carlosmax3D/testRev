using System;
using System.Windows.Forms;
using System.Linq;
using TemplateCSharp.Models;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    public partial class FrmDetalleUsuario : Form
    {
        private readonly IUsuarioService _usuarioService;
        private readonly Usuario _usuarioActual;
        private readonly Usuario _usuarioAEditar;
        public FrmDetalleUsuario(IUsuarioService usuarioService,Usuario usuarioActual,Usuario usuarioAEditar = null)
        {
            InitializeComponent();

            _usuarioService = usuarioService;
            _usuarioActual = usuarioActual;
            _usuarioAEditar = usuarioAEditar;

            cBTipoUsrioDU.SelectedIndexChanged +=cBTipoUsrioDU_SelectedIndexChanged;

            Text = _usuarioAEditar == null ? "Nuevo usuario" : "Editar usuario";
            tBContraUsrioDU.UseSystemPasswordChar = true;
            tBConfirmContraUsrioDU.UseSystemPasswordChar = true;
            tBNomComUsrioDU.MaxLength = 150;
            tBNumNomiUsrioDU.MaxLength = 20;
            tBCorreoUsrioDU.MaxLength = 50;
            tBContraUsrioDU.MaxLength = 255;
            tBConfirmContraUsrioDU.MaxLength = 255;
            ClaveAdminTB.MaxLength = 50;
            tBCajaAsig.MaxLength = 50;
            UiHelper.ConfigurarFecha(dTPFechaNacimUsrioDU);

            if (_usuarioAEditar == null)
            {
                cbEstUsrio.SelectedItem = "Activo";
            }

            ConfigurarCamposTipoUsuario();
            if (_usuarioAEditar != null)
            {
                CargarDatosUsuario();
            }
        }

        private void CargarDatosUsuario()
        {
            tBNomComUsrioDU.Text = _usuarioAEditar.NombreCompleto;
            tBNumNomiUsrioDU.Text = _usuarioAEditar.NumeroNomina;
            tBCorreoUsrioDU.Text = _usuarioAEditar.Correo;

            dTPFechaNacimUsrioDU.Value = _usuarioAEditar.FechaNacimiento;

            cBTipoUsrioDU.SelectedItem = _usuarioAEditar.TipoUsuario;
            cbEstUsrio.SelectedItem = _usuarioAEditar.EstadoUsuario;

            ClaveAdminTB.Text = _usuarioAEditar.ClaveAdmin;
            tBCajaAsig.Text = _usuarioAEditar.CajaAsignada;
        }

        private void cBTipoUsrioDU_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClaveAdminTB.Clear();

            tBCajaAsig.Clear();

            ConfigurarCamposTipoUsuario();
        }

        private void ConfigurarCamposTipoUsuario()
        {
            bool esAdministrador =cBTipoUsrioDU.SelectedItem?.ToString() == "Administrador";

            ClaveAdminTxt.Visible = esAdministrador;
            ClaveAdminTB.Visible = esAdministrador;

            CajaAsigUOpe.Visible = !esAdministrador;
            tBCajaAsig.Visible = !esAdministrador;
        }
        private void botonGuardarDU_Click(object sender, EventArgs e)
        {
            bool esEdicion = _usuarioAEditar != null;
            if (string.IsNullOrWhiteSpace(tBNomComUsrioDU.Text))
            {
                MessageBox.Show("Escribe el nombre completo del usuario.");
                tBNomComUsrioDU.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tBNumNomiUsrioDU.Text))
            {
                MessageBox.Show("Escribe el número de nómina.");
                tBNumNomiUsrioDU.Focus();
                return;
            }
            string numeroNomina = tBNumNomiUsrioDU.Text.Trim();

            try
            {
                bool numeroNominaCambio =
                    !esEdicion ||
                    numeroNomina != _usuarioAEditar.NumeroNomina;

                if (numeroNominaCambio &&
                    _usuarioService.ExisteNumeroNomina(numeroNomina))
                {
                    MessageBox.Show("Ese número de nómina ya está registrado.");
                    tBNumNomiUsrioDU.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo comprobar el número de nómina.\n" + ex.Message);
                return;
            }

            if (string.IsNullOrWhiteSpace(tBCorreoUsrioDU.Text))
            {
                MessageBox.Show("Escribe el correo electrónico.");
                tBCorreoUsrioDU.Focus();
                return;
            }

            string correo = tBCorreoUsrioDU.Text.Trim().ToLowerInvariant();

            if (!Validaciones.EsCorreoValido(correo))
            {
                MessageBox.Show("Escribe un correo electrónico válido (ejemplo: nombre@dominio.com).");
                tBCorreoUsrioDU.Focus();
                return;
            }
            try
            {
                bool correoCambio =
                    !esEdicion ||
                    !string.Equals(correo,_usuarioAEditar.Correo,StringComparison.OrdinalIgnoreCase);

                if (correoCambio && _usuarioService.ExisteCorreo(correo))
                {
                    MessageBox.Show("Ese correo electrónico ya está registrado.");

                    tBCorreoUsrioDU.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo comprobar el correo electrónico.\n" + ex.Message);

                return;
            }

            if (cBTipoUsrioDU.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona el tipo de usuario.");
                cBTipoUsrioDU.Focus();
                return;
            }

            if (cbEstUsrio.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona el estado del usuario.");
                cbEstUsrio.Focus();
                return;
            }

            if (dTPFechaNacimUsrioDU.Value.Date >= DateTime.Today)
            {
                MessageBox.Show(
                    "La fecha de nacimiento debe ser anterior al día de hoy.");

                dTPFechaNacimUsrioDU.Focus();
                return;
            }

            string contrasena = tBContraUsrioDU.Text;
            string confirmarContrasena = tBConfirmContraUsrioDU.Text;

            bool cambiarContrasena =
                !string.IsNullOrWhiteSpace(contrasena) ||
                !string.IsNullOrWhiteSpace(confirmarContrasena);

            if (!esEdicion || cambiarContrasena)
            {
                if (string.IsNullOrWhiteSpace(contrasena))
                {
                    MessageBox.Show("Escribe la contraseña del usuario.");
                    tBContraUsrioDU.Focus();
                    return;
                }

                string? errorContrasena = Validaciones.ErrorContrasena(contrasena);

                if (errorContrasena != null)
                {
                    MessageBox.Show(errorContrasena);
                    tBContraUsrioDU.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(confirmarContrasena))
                {
                    MessageBox.Show("Confirma la contraseña.");
                    tBConfirmContraUsrioDU.Focus();
                    return;
                }

                if (contrasena != confirmarContrasena)
                {
                    MessageBox.Show("Las contraseñas no coinciden.");
                    tBConfirmContraUsrioDU.Focus();
                    return;
                }
            }
            else
            {
                // Contraseña vacía = no se cambia (el procedimiento conserva la actual)
                contrasena = string.Empty;
            }

            if (cBTipoUsrioDU.SelectedItem.ToString() == "Administrador")
            {
                if (string.IsNullOrWhiteSpace(ClaveAdminTB.Text))
                {
                    MessageBox.Show("Escribe la clave del administrador.");
                    ClaveAdminTB.Focus();
                    return;
                }
            }
            else if (cBTipoUsrioDU.SelectedItem.ToString() == "Operativo")
            {
                if (string.IsNullOrWhiteSpace(tBCajaAsig.Text))
                {
                    MessageBox.Show("Escribe la caja asignada al operativo.");
                    tBCajaAsig.Focus();
                    return;
                }
            }

            if (esEdicion && _usuarioAEditar.IdUsuario == _usuarioActual.IdUsuario
                && (cbEstUsrio.SelectedItem.ToString() != "Activo"
                    || cBTipoUsrioDU.SelectedItem.ToString() != _usuarioAEditar.TipoUsuario))
            {
                MessageBox.Show("No puedes cambiar el tipo ni el estado del usuario con el que iniciaste sesión.");
                return;
            }

            try
            {
                string tipoUsuario = cBTipoUsrioDU.SelectedItem.ToString();
                string estadoUsuario = cbEstUsrio.SelectedItem.ToString();

                Usuario nuevoUsuario = new Usuario
                {
                    IdUsuario = esEdicion ? _usuarioAEditar.IdUsuario : 0,
                    NombreCompleto = tBNomComUsrioDU.Text.Trim(),
                    NumeroNomina = numeroNomina,
                    Correo = correo,
                    Contrasena = contrasena,
                    TipoUsuario = tipoUsuario,
                    EstadoUsuario = estadoUsuario,
                    FechaNacimiento = dTPFechaNacimUsrioDU.Value.Date,

                    IdAdministradorReg = esEdicion
                        ? _usuarioAEditar.IdAdministradorReg
                        : _usuarioActual.IdUsuario
                };

                string claveAdmin = tipoUsuario == "Administrador"
                    ? ClaveAdminTB.Text.Trim()
                    : string.Empty;

                string cajaAsignada = tipoUsuario == "Operativo"
                    ? tBCajaAsig.Text.Trim()
                    : string.Empty;

                if (esEdicion)
                {
                    _usuarioService.ActualizarUsuario(
                        nuevoUsuario,
                        claveAdmin,
                        cajaAsignada);

                    MessageBox.Show("Usuario actualizado correctamente.");
                }
                else
                {
                    _usuarioService.RegistrarUsuario(
                        nuevoUsuario,
                        claveAdmin,
                        cajaAsignada);

                    MessageBox.Show(
                        "Usuario registrado correctamente.");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                string accion = esEdicion ? "actualizar" : "registrar";

                UiHelper.MostrarError(ex, "No se pudo " + accion + " el usuario");
            }
        }
        private void botonCancelarDU_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
