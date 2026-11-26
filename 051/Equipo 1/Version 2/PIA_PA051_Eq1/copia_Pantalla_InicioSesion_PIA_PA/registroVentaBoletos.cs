
using System;
using System.Data;
using System.Windows.Forms;
using Prueba2_PrimerAvance_PAyMAD;
using Pantalla_EdicionInfoVB_PIA_PA;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_RegistroVentasBoletos_PIA_PA
{
    public partial class pantalla_registroVB : Form
    {
        // Referencia al menú Admin original.
        // Permite volver a mostrar la ventana del administrador
        // cuando el usuario termina de consultar o editar las ventas.
        private pantalla_menuAdmin menuAdminAnterior;

        // Constructor del formulario de registro de ventas de boletos.
        // Recibe la instancia del menú de administrador desde donde se abrió.
        public pantalla_registroVB(pantalla_menuAdmin menuAdmin)
        {
            // Inicializa los controles visuales del formulario.
            InitializeComponent();

            // Guarda la referencia al menú anterior para poder regresar a él.
            menuAdminAnterior = menuAdmin;
        }

        // Al abrir la ventana, configurar la selección y cargar las ventas.
        private void pantalla_registroVB_Load(object sender, EventArgs e)
        {
            // Selecciona la fila completa cuando el usuario hace clic
            // en cualquiera de sus celdas.
            datagridview1_registroVB.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            // Impide seleccionar varias ventas al mismo tiempo.
            datagridview1_registroVB.MultiSelect = false;

            // Impide que el usuario modifique directamente los datos
            // desde las celdas del DataGridView.
            datagridview1_registroVB.ReadOnly = true;

            // Oculta la fila vacía que normalmente se utiliza
            // para agregar nuevos registros desde el DataGridView.
            datagridview1_registroVB.AllowUserToAddRows = false;

            // Impide eliminar registros directamente desde la tabla.
            datagridview1_registroVB.AllowUserToDeleteRows = false;

            // Obtiene las ventas de SQL Server y las muestra en la tabla.
            CargarVentas();
        }

        // Cargar las ventas desde la vista almacenada en SQL Server.
        private void CargarVentas()
        {
            try
            {
                // Llama al método de ConexionBD.
                // La conexión y la consulta SQL se realizan desde esa clase.
                DataTable tabla = ConexionBD.ObtenerVentasBoletos();

                // Asigna la tabla como fuente de datos
                // del DataGridView para mostrar las ventas.
                datagridview1_registroVB.DataSource = tabla;

                // Quita cualquier selección automática
                // después de cargar los registros.
                datagridview1_registroVB.ClearSelection();
            }
            catch (Exception ex)
            {
                // Captura errores que puedan ocurrir al consultar
                // la vista o al conectarse con SQL Server.
                MessageBox.Show(
                    "No se pudieron cargar las ventas.\n\n" +
                    "Verifica que exista la vista " +
                    "dbo.vw_RegistroVentasBoletos y que la conexión " +
                    "a la base de datos sea correcta.\n\n" +
                    ex.Message,
                    "Error al cargar ventas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Evento que se ejecuta cuando se hace clic
        // en un elemento del menú desplegable.
        // Actualmente no contiene instrucciones.
        private void menustrip1_registroVB_ItemClicked(
            object sender, ToolStripItemClickedEventArgs e)
        {
        }

        // Evento que se ejecuta al hacer clic en el contenido
        // de una celda del DataGridView.
        // Actualmente no contiene instrucciones.
        private void datagridview1_registroVB_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        // BOTÓN ATRÁS: REGRESAR AL MENÚ ADMIN.
        private void atraspestañaRVB_Click(
            object sender, EventArgs e)
        {
            // Comprueba que exista una ventana anterior.
            if (menuAdminAnterior != null)
            {
                // Vuelve a mostrar la ventana del menú de administrador.
                menuAdminAnterior.Show();

                // Activa el menú para que quede al frente.
                menuAdminAnterior.Activate();
            }

            // Cierra la ventana actual de registro de ventas.
            this.Close();
        }

        // BOTÓN EDITAR: ABRIR LA SEGUNDA VENTANA.
        private void editarpestañaRVB_Click(object sender, EventArgs e)
        {
            // Comprueba si el usuario seleccionó alguna fila.
            if (datagridview1_registroVB.SelectedRows.Count == 0)
            {
                // Informa que es necesario seleccionar una venta
                // antes de intentar editarla.
                MessageBox.Show(
                    "Primero selecciona una venta.",
                    "Venta no seleccionada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Detiene el método porque no hay una venta seleccionada.
                return;
            }

            // Obtiene la primera fila seleccionada del DataGridView.
            // Como MultiSelect está desactivado, solo se permite
            // seleccionar una venta a la vez.
            DataGridViewRow fila =
                datagridview1_registroVB.SelectedRows[0];

            // Crea la ventana de edición y le pasa la fila seleccionada.
            // La ventana recibe los datos de la venta para trabajar con ellos.
            // using libera los recursos de la ventana al terminar.
            using (pantalla_edicionVB ventanaEdicion =
                new pantalla_edicionVB(fila))
            {
                // Abre la ventana de edición como cuadro de diálogo.
                // El usuario debe cerrarla para regresar a esta pantalla.
                ventanaEdicion.ShowDialog();
            }

            // Actualiza la tabla al cerrar la ventana de edición.
            // Vuelve a consultar la vista para mostrar los datos actuales.
            CargarVentas();
        }
    }
}
