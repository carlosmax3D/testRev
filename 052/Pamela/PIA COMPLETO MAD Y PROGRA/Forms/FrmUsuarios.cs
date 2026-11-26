using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms
{
    public partial class FrmUsuarios : Form
    {
        private readonly IUsuarioService _usuarioService;
        private readonly Usuario _usuarioActual;

        public FrmUsuarios(IUsuarioService usuarioService, Usuario usuarioActual)
        {
            InitializeComponent();

            _usuarioService = usuarioService;
            _usuarioActual = usuarioActual;

            Text = "Gestión de usuarios";
            AcceptButton = botonBuscUsrioGU;
            botonElimUsrioGU.Text = "Dar de baja";

            // Letreros propios en cada columna; no se muestran Id ni contraseñas
            UiHelper.ConfigurarGrid(
                dGVUsrioGU,
                true,
                ("NombreCompleto", "Nombre completo", TipoColumna.Texto),
                ("Correo", "Correo electrónico", TipoColumna.Texto),
                ("NumeroNomina", "Núm. de nómina", TipoColumna.Texto),
                ("TipoUsuario", "Tipo de usuario", TipoColumna.Texto),
                ("EstadoUsuario", "Estado", TipoColumna.Texto),
                ("FechaNacimiento", "Fecha de nacimiento", TipoColumna.Fecha),
                ("FechaHoraRegistro", "Registrado el", TipoColumna.FechaHora));
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                dGVUsrioGU.DataSource = _usuarioService.ObtenerUsuarios();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al cargar los usuarios");
            }
        }

        private Usuario? UsuarioSeleccionado()
        {
            DataGridViewRow? renglon = dGVUsrioGU.SelectedRows.Count > 0
                ? dGVUsrioGU.SelectedRows[0]
                : dGVUsrioGU.CurrentRow;

            return renglon?.DataBoundItem as Usuario;
        }

        private void botonBuscUsrioGU_Click(object sender, EventArgs e)
        {
            string criterio = cBBuscUsrioGU.SelectedItem?.ToString() ?? "";
            string texto = tBBuscUsrioGU.Text.Trim();

            if (string.IsNullOrWhiteSpace(criterio))
            {
                UiHelper.Advertir("Selecciona una opción para buscar.", "Búsqueda");
                cBBuscUsrioGU.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(texto))
            {
                UiHelper.Advertir("Escribe el texto que deseas buscar.", "Búsqueda");
                tBBuscUsrioGU.Focus();
                return;
            }

            try
            {
                List<Usuario> usuarios = _usuarioService.BuscarUsuarios(criterio, texto);

                dGVUsrioGU.DataSource = null;
                dGVUsrioGU.DataSource = usuarios;

                if (usuarios.Count == 0)
                {
                    UiHelper.Informar("No se encontraron usuarios con ese criterio de búsqueda.", "Sin resultados");
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al buscar usuarios");
            }
        }

        private void botonMostrarTodosGU_Click(object sender, EventArgs e)
        {
            CargarUsuarios();

            tBBuscUsrioGU.Clear();
            cBBuscUsrioGU.SelectedIndex = -1;
        }

        private void botonNuevoUsrioGU_Click(object sender, EventArgs e)
        {
            using FrmDetalleUsuario formulario = new FrmDetalleUsuario(_usuarioService, _usuarioActual);

            formulario.StartPosition = FormStartPosition.CenterParent;
            formulario.ShowDialog(this);

            CargarUsuarios();
        }

        private void botonEditUsrioGU_Click(object sender, EventArgs e)
        {
            Usuario? seleccionado = UsuarioSeleccionado();

            if (seleccionado == null)
            {
                UiHelper.Advertir("Selecciona un usuario para editar.", "Editar usuario");
                return;
            }

            try
            {
                Usuario usuario = _usuarioService.ObtenerUsuarioPorId(seleccionado.IdUsuario);

                using FrmDetalleUsuario formulario = new FrmDetalleUsuario(_usuarioService, _usuarioActual, usuario);

                formulario.StartPosition = FormStartPosition.CenterParent;
                formulario.ShowDialog(this);

                CargarUsuarios();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al abrir la edición del usuario");
            }
        }

        private void botonElimUsrioGU_Click(object sender, EventArgs e)
        {
            Usuario? seleccionado = UsuarioSeleccionado();

            if (seleccionado == null)
            {
                UiHelper.Advertir("Selecciona un usuario para dar de baja.", "Eliminar usuario");
                return;
            }

            if (seleccionado.IdUsuario == _usuarioActual.IdUsuario)
            {
                UiHelper.Advertir("No puedes dar de baja al usuario con el que iniciaste sesión.", "Eliminar usuario");
                return;
            }

            if (seleccionado.EstadoUsuario == "Inactivo")
            {
                UiHelper.Informar("Ese usuario ya está dado de baja (Inactivo).", "Eliminar usuario");
                return;
            }

            // Eliminación lógica: el usuario queda Inactivo para conservar el historial
            // de las ventas, clientes y estadios que registró.
            if (!UiHelper.Confirmar(
                    "¿Estás seguro de que deseas dar de baja al usuario " + seleccionado.NombreCompleto + "?\n\n" +
                    "Ya no podrá iniciar sesión, pero se conserva su historial.",
                    "Confirmar eliminación"))
            {
                return;
            }

            try
            {
                _usuarioService.EliminarUsuario(seleccionado.IdUsuario);

                UiHelper.Informar("Usuario dado de baja correctamente.", "Eliminar usuario");

                CargarUsuarios();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo eliminar el usuario");
            }
        }

        private void botonRegresarGU_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
