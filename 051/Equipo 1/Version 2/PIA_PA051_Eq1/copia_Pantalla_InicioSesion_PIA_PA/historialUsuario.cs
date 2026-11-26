
using System;
using System.Data;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_HistorialUsuarios_PIA_PA
{
    // Define el formulario encargado de mostrar el historial de usuarios registrados.
    public partial class pantalla_historialUsuario : Form
    {
        // Almacena temporalmente los usuarios obtenidos de la base de datos.
        private DataTable tablaUsuarios;

        // Guarda el filtro y orden actuales para conservarlos al editar o eliminar.
        private string tipoActual = "TODOS";
        private string ordenActual = "ASC";

        // Constructor del formulario.
        public pantalla_historialUsuario()
        {
            // Inicializa los componentes visuales creados desde el diseñador.
            InitializeComponent();
        }

        // Evento que se ejecuta cuando se carga la pantalla.
        private void pantalla_historialUsuario_Load(object sender, EventArgs e)
        {
            // Carga todos los usuarios registrados y los muestra en la tabla.
            CargarUsuarios();
        }

        // Consulta los usuarios mediante ConexionBD y permite filtrarlos por tipo
        // y ordenarlos alfabéticamente.
        private void CargarUsuarios(string tipoUsuario = "TODOS", string orden = "ASC")
        {
            try
            {
                tipoActual = tipoUsuario;
                ordenActual = orden;

                // Obtener los usuarios desde la vista mediante la clase ConexionBD.
                tablaUsuarios = ConexionBD.ObtenerHistorialUsuarios();

                // Crear una vista para filtrar y ordenar los registros en C#.
                DataView vistaUsuarios = new DataView(tablaUsuarios);

                // Filtrar los registros según el tipo de usuario seleccionado.
                switch (tipoUsuario.ToUpper())
                {
                    case "ADMIN":
                        vistaUsuarios.RowFilter =
                            "Tipo_Usuario = 'Administrador'";
                        break;

                    case "OPERATIVO":
                        vistaUsuarios.RowFilter =
                            "Tipo_Usuario = 'Operativo'";
                        break;

                    default:
                        // Mostrar todos los usuarios.
                        vistaUsuarios.RowFilter = "";
                        break;
                }

                // Determinar el orden alfabético.
                string direccion = orden == "DESC" ? "DESC" : "ASC";

                // Ordenar por nombre y después por apellido.
                vistaUsuarios.Sort =
                    "Primer_Nombre " + direccion +
                    ", Primer_Apellido " + direccion;

                // Mostrar los registros en el DataGridView.
                dtaview1_historialUsuario.DataSource = vistaUsuarios;

                // Impedir que el usuario modifique directamente los datos mostrados.
                dtaview1_historialUsuario.ReadOnly = true;

                // Evitar que se agreguen filas nuevas manualmente desde la tabla.
                dtaview1_historialUsuario.AllowUserToAddRows = false;

                // Evitar que se eliminen filas manualmente desde la tabla.
                dtaview1_historialUsuario.AllowUserToDeleteRows = false;

                // Permitir seleccionar filas completas.
                dtaview1_historialUsuario.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                // Ajustar el ancho de las columnas al espacio disponible.
                dtaview1_historialUsuario.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                // Mostrar el estado y la fecha de baja.
                if (dtaview1_historialUsuario.Columns.Contains("Activo"))
                {
                    dtaview1_historialUsuario.Columns["Activo"].HeaderText =
                        "Estado activo";
                }

                if (dtaview1_historialUsuario.Columns.Contains("Fecha_Baja"))
                {
                    dtaview1_historialUsuario.Columns["Fecha_Baja"].HeaderText =
                        "Fecha de baja";
                    dtaview1_historialUsuario.Columns["Fecha_Baja"].DefaultCellStyle.Format =
                        "dd/MM/yyyy HH:mm";
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier error producido durante la consulta.
                MessageBox.Show(
                    "No se pudieron cargar los usuarios.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento del menú para ordenar todos los usuarios de forma ascendente.
        private void adminAZToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            // Mostrar todos los usuarios ordenados de A a Z.
            CargarUsuarios("TODOS", "ASC");
        }

        // Evento del menú para ordenar todos los usuarios de forma descendente.
        private void adminZAToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            // Mostrar todos los usuarios ordenados de Z a A.
            CargarUsuarios("TODOS", "DESC");
        }

        // Evento que se ejecuta cuando se hace clic en un elemento del menú.
        private void menustrip1_historialUsuario_ItemClicked(
            object sender, ToolStripItemClickedEventArgs e)
        {
            // Actualmente no realiza ninguna acción.
        }

        // Evento que se ejecuta cuando se hace clic en el contenido de una celda.
        private void dtaview1_historialUsuario_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            // Actualmente no realiza ninguna acción.
        }

        // Evento del botón que permite cerrar la pantalla del historial.
        private void boton1_historialUsuario_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario del historial.
            this.Close();
        }

        // Segundo evento asociado al menú de la pantalla.
        private void menustrip1_historialUsuario_ItemClicked_1(
            object sender, ToolStripItemClickedEventArgs e)
        {
            // Actualmente no realiza ninguna acción.
        }

        // Evento para mostrar únicamente los administradores en orden ascendente.
        private void azAdmin_Click(object sender, EventArgs e)
        {
            // Filtrar administradores y ordenarlos de A a Z.
            CargarUsuarios("ADMIN", "ASC");
        }

        // Evento para mostrar únicamente los administradores en orden descendente.
        private void zaAdmin_Click(object sender, EventArgs e)
        {
            // Filtrar administradores y ordenarlos de Z a A.
            CargarUsuarios("ADMIN", "DESC");
        }

        // Evento para mostrar únicamente los usuarios operativos en orden ascendente.
        private void azOper_Click(object sender, EventArgs e)
        {
            // Filtrar operativos y ordenarlos de A a Z.
            CargarUsuarios("OPERATIVO", "ASC");
        }

        // Evento para mostrar únicamente los usuarios operativos en orden descendente.
        private void zaOper_Click(object sender, EventArgs e)
        {
            // Filtrar operativos y ordenarlos de Z a A.
            CargarUsuarios("OPERATIVO", "DESC");
        }

        // Evento del menú que permite regresar cerrando la pantalla actual.
        private void atrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario del historial de usuarios.
            this.Close();
        }

        // Obtiene los datos del usuario seleccionado en la tabla.
        private DataRowView ObtenerUsuarioSeleccionado()
        {
            if (dtaview1_historialUsuario.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Primero debes seleccionar un usuario de la tabla.",
                    "Usuario no seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return null;
            }

            DataGridViewRow fila =
                dtaview1_historialUsuario.SelectedRows[0];

            if (fila.DataBoundItem is DataRowView usuario)
            {
                return usuario;
            }

            MessageBox.Show(
                "No se pudieron obtener los datos del usuario seleccionado.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return null;
        }

        // Comprueba si el usuario seleccionado continúa activo.
        // Comprueba si el usuario seleccionado continúa activo.
        private bool UsuarioEstaActivo(DataRowView usuario)
        {
            if (usuario == null)
                return false;

            // Comprobar si la consulta realmente devuelve la columna Activo.
            if (!usuario.DataView.Table.Columns.Contains("Activo"))
            {
                MessageBox.Show(
                    "No se puede comprobar si el usuario está activo porque " +
                    "la consulta del historial no devuelve la columna 'Activo'.\n\n" +
                    "Es necesario corregir ObtenerHistorialUsuarios() para incluir " +
                    "el estado del usuario.",
                    "Estado no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (usuario["Activo"] == DBNull.Value)
            {
                MessageBox.Show(
                    "No se pudo comprobar el estado del usuario.",
                    "Estado no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            bool activo = Convert.ToBoolean(usuario["Activo"]);

            if (!activo)
            {
                MessageBox.Show(
                    "Este usuario ya está dado de baja.\n\n" +
                    "No es posible editarlo ni volver a darlo de baja.",
                    "Usuario inactivo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        // Evento para editar la información de un usuario seleccionado.
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView usuario = ObtenerUsuarioSeleccionado();

            if (usuario == null)
                return;

            // No permitir editar usuarios dados de baja.
            if (!UsuarioEstaActivo(usuario))
                return;

            try
            {
                int nomina = Convert.ToInt32(usuario["Nomina"]);

                string primerNombre =
                    Convert.ToString(usuario["Primer_Nombre"]);

                string segundoNombre =
                    usuario["Segundo_Nombre"] == DBNull.Value
                    ? ""
                    : Convert.ToString(usuario["Segundo_Nombre"]);

                string primerApellido =
                    Convert.ToString(usuario["Primer_Apellido"]);

                string segundoApellido =
                    Convert.ToString(usuario["Segundo_Apellido"]);

                string correo =
                    Convert.ToString(usuario["Correo_electronico"]);

                DateTime fechaNacimiento =
                    Convert.ToDateTime(usuario["Fecha_Nacimiento"]);

                using (pantalla_editarUsuario formulario =
                    new pantalla_editarUsuario(
                        nomina,
                        primerNombre,
                        segundoNombre,
                        primerApellido,
                        segundoApellido,
                        correo,
                        fechaNacimiento))
                {
                    if (formulario.ShowDialog() == DialogResult.OK)
                    {
                        CargarUsuarios(tipoActual, ordenActual);

                        MessageBox.Show(
                            "Los datos del usuario se actualizaron correctamente.",
                            "Actualización exitosa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo abrir la edición del usuario.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento para dar de baja a un usuario seleccionado.
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView usuario = ObtenerUsuarioSeleccionado();

            if (usuario == null)
                return;

            // No permitir dar de baja a un usuario que ya está inactivo.
            if (!UsuarioEstaActivo(usuario))
                return;

            try
            {
                // Obtener los datos que se mostrarán en la confirmación.
                int nomina = Convert.ToInt32(usuario["Nomina"]);

                string primerNombre =
                    Convert.ToString(usuario["Primer_Nombre"]);

                string segundoNombre =
                    usuario["Segundo_Nombre"] == DBNull.Value
                    ? ""
                    : Convert.ToString(usuario["Segundo_Nombre"]);

                string primerApellido =
                    Convert.ToString(usuario["Primer_Apellido"]);

                string segundoApellido =
                    Convert.ToString(usuario["Segundo_Apellido"]);

                string correo =
                    Convert.ToString(usuario["Correo_electronico"]);

                DateTime fechaNacimiento =
                    Convert.ToDateTime(usuario["Fecha_Nacimiento"]);

                string tipoUsuario =
                    Convert.ToString(usuario["Tipo_Usuario"]);

                string nombreCompleto =
                    (primerNombre + " " +
                     segundoNombre + " " +
                     primerApellido + " " +
                     segundoApellido).Trim();

                string informacionUsuario =
                    "INFORMACIÓN DEL USUARIO QUE SE DARÁ DE BAJA\n\n" +
                    "Nómina: " + nomina + "\n" +
                    "Tipo de usuario: " + tipoUsuario + "\n" +
                    "Nombre: " + nombreCompleto + "\n" +
                    "Correo: " + correo + "\n" +
                    "Fecha de nacimiento: " +
                    fechaNacimiento.ToString("dd/MM/yyyy") + "\n\n" +
                    "El usuario permanecerá registrado en el historial, " +
                    "pero quedará inactivo y ya no podrá editarse ni " +
                    "darse de baja nuevamente.\n\n" +
                    "¿Deseas continuar?";

                // Mostrar la información y solicitar confirmación.
                DialogResult confirmacion = MessageBox.Show(
                    informacionUsuario,
                    "Confirmar baja de usuario",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                // Si el usuario selecciona No, cancelar la operación.
                if (confirmacion != DialogResult.Yes)
                    return;

                // Dar de baja al usuario sin eliminarlo de la base de datos.
                int resultado =
                    ConexionBD.DarDeBajaUsuario(nomina);

                if (resultado > 0)
                {
                    MessageBox.Show(
                        "El usuario fue dado de baja correctamente.",
                        "Baja exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Actualizar la tabla conservando el filtro y orden.
                    CargarUsuarios(tipoActual, ordenActual);
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo dar de baja al usuario.\n\n" +
                        "Es posible que ya esté inactivo o que el registro " +
                        "haya cambiado.",
                        "Baja no realizada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    // Actualizar el historial para mostrar el estado actual.
                    CargarUsuarios(tipoActual, ordenActual);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al dar de baja al usuario.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
