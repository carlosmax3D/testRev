
using System;
using System.Data;
using System.Windows.Forms;
using Pantalla_MenuClientes_PIA_PA;
using Pantalla_EdicionCliente_PIA_PA;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_HistorialCliente_PIA_PA
{
    // Define la pantalla encargada de mostrar el historial de clientes registrados.
    public partial class pantalla_historialCliente : Form
    {
        // Guarda una referencia al menú de clientes para regresar a esa pantalla
        // cuando el usuario termine de consultar el historial.
        private pantalla_menuCliente menuClienteAnterior;

        // Guarda la nómina del usuario que inició sesión.
        private int nominaUsuario;

        // Constructor de la pantalla.
        // Recibe el menú de clientes y la nómina del usuario.
        public pantalla_historialCliente(
            pantalla_menuCliente menuCliente,
            int nomina)
        {
            // Inicializa los controles visuales creados en el diseñador de Windows Forms.
            InitializeComponent();

            // Guarda el menú anterior para poder mostrarlo nuevamente al regresar.
            menuClienteAnterior = menuCliente;

            // Guarda la nómina del usuario responsable de las operaciones.
            nominaUsuario = nomina;
        }

        // Evento que se ejecuta cuando se carga la pantalla del historial.
        private void pantalla_historialCliente_Load(
            object sender, EventArgs e)
        {
            // Consulta los clientes de la base de datos y los muestra en la tabla.
            CargarClientes();
        }

        // CARGAR LOS CLIENTES DESDE SQL SERVER MEDIANTE CONEXIONBD.
        private void CargarClientes()
        {
            try
            {
                // Obtener los clientes mediante la clase central de conexión.
                DataTable tabla = ConexionBD.ObtenerHistorialClientes();

                // Mostrar los resultados en el DataGridView.
                datagridview1_historialCliente.DataSource = tabla;

                datagridview1_historialCliente.ReadOnly = true;
                datagridview1_historialCliente.AllowUserToAddRows = false;
                datagridview1_historialCliente.AllowUserToDeleteRows = false;

                datagridview1_historialCliente.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                datagridview1_historialCliente.MultiSelect = false;

                datagridview1_historialCliente.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.DisplayedCells;

                // Comprobar si existen clientes registrados.
                if (tabla.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay clientes registrados.",
                        "Historial de clientes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar los clientes:\n\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // BOTÓN ATRÁS: REGRESAR AL MENÚ CLIENTE.
        private void atraspestañaHC_Click(
            object sender, EventArgs e)
        {
            // Cerrar la pantalla actual del historial.
            this.Close();
        }

        // Evento del botón 1 del historial.
        private void boton1_historialCliente_Click(
            object sender, EventArgs e)
        {
            // Reutilizar el evento del botón Atrás.
            atraspestañaHC_Click(sender, e);
        }

        // Evento del menú contextual.
        private void menustrip1_historialCliente_ItemClicked(
            object sender, ToolStripItemClickedEventArgs e)
        {
            // Actualmente no realiza ninguna acción.
        }

        // Evento que se ejecuta cuando el usuario hace clic en una celda.
        private void datagridview1_historialCliente_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            // Actualmente no necesita ejecutar ninguna acción.
        }

        // BOTÓN EDITAR: ABRIR LA PANTALLA DE EDICIÓN.
        private void editarpestañaHC_Click(
            object sender, EventArgs e)
        {
            // Comprobar que exista una fila seleccionada.
            if (datagridview1_historialCliente.CurrentRow == null ||
                datagridview1_historialCliente.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Selecciona un cliente de la tabla antes de editar.",
                    "Seleccionar cliente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Ocultar la pantalla del historial antes de abrir la edición.
            this.Hide();

            try
            {
                // Abrir la pantalla de edición de clientes.
                // Se envía también la nómina del usuario responsable.
                using (pantalla_edicionCliente edicion =
                       new pantalla_edicionCliente(this, nominaUsuario))
                {
                    edicion.ShowDialog();
                }
            }
            finally
            {
                // Volver a mostrar el historial cuando se cierre la edición.
                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();

                    // Actualizar el historial al cerrar la pantalla de edición.
                    CargarClientes();
                }
            }
        }

        // Evento que se ejecuta cuando se cierra la pantalla del historial.
        private void pantalla_historialCliente_FormClosed(
            object sender, FormClosedEventArgs e)
        {
            // Mostrar nuevamente el menú de clientes.
            if (menuClienteAnterior != null &&
                !menuClienteAnterior.IsDisposed)
            {
                menuClienteAnterior.Show();
                menuClienteAnterior.Activate();
            }
        }
    }
}
