
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Pantalla_InicioSesion_PIA_PA.conexionBD;


namespace Pantalla_CatalogosRegistrados
{
    // Define el formulario que muestra los catálogos registrados en el sistema.
    public partial class pantalla_catalogosRegistrados : Form
    {
        // Constructor del formulario.
        public pantalla_catalogosRegistrados()
        {
            // Inicializa los controles visuales creados en el diseñador de Windows Forms.
            InitializeComponent();
        }

        // Evento que se ejecuta cuando se carga la pantalla.
        private void pantalla_catalogosRegistrados_Load(object sender, EventArgs e)
        {
            // Consulta los catálogos registrados y los muestra en la tabla.
            CargarCatalogos();
        }

        // Método encargado de consultar y mostrar los datos de los catálogos.
        // Método encargado de consultar y mostrar los datos de los catálogos.
        private void CargarCatalogos()
        {
            try
            {
                // Crea una instancia de la clase Vistas.
                Vistas vistas = new Vistas();

                // Obtiene los catálogos registrados desde la clase Vistas.
                DataTable tabla = vistas.ObtenerCatalogosRegistrados();

                // Asigna los resultados al DataGridView.
                datagridview1_inicioSesion.DataSource = tabla;

                // Ajusta automáticamente el ancho de las columnas.
                datagridview1_inicioSesion.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                // Impide modificar directamente los datos.
                datagridview1_inicioSesion.ReadOnly = true;

                // Evita agregar filas manualmente.
                datagridview1_inicioSesion.AllowUserToAddRows = false;

                // Permite seleccionar una fila completa.
                datagridview1_inicioSesion.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                // Evita seleccionar varias filas al mismo tiempo.
                datagridview1_inicioSesion.MultiSelect = false;
            }
            catch (Exception ex)
            {
                // Muestra los errores que ocurran al consultar los catálogos.
                MessageBox.Show(
                    "Error al cargar los catálogos: " + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta cuando se hace clic en un elemento del menú.
        private void menustrip1_inicioSesion_ItemClicked(
            object sender,
            ToolStripItemClickedEventArgs e)
        {
            // Actualmente no se realiza ninguna acción al seleccionar un elemento del menú.
        }

        // Evento que se ejecuta cuando se hace clic en el contenido de una celda del DataGridView.
        private void datagridview1_inicioSesion_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            // Actualmente no se ha definido ninguna acción para este evento.
        }

        // Evento del elemento Atrás en el menú.
        private void atrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual y regresa al formulario anterior,
            // si este continúa abierto.
            this.Close();
        }
    }
}
