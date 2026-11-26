using Prueba2_PrimerAvance_PAyMAD;
using Pantalla_InicioSesion_PIA_PA.conexionBD;
using Pantalla_InicioSesion_PIA_PA;
using System;
using System.Data;
using System.Windows.Forms;

namespace Pantalla_BusquedaCompraCliente_PIA_PA
{
    // Formulario que permite buscar clientes por su primer apellido.
    public partial class pantalla_busquedaClientes : Form
    {
        // Guarda una referencia al menú del administrador para regresar a él.
        private pantalla_menuAdmin menuAdminAnterior;

        // Instancia de la clase que contiene los métodos para consultar las vistas.
        private Vistas vistas = new Vistas();

        // Constructor que recibe el formulario del menú administrador.
        public pantalla_busquedaClientes(pantalla_menuAdmin menuAdmin)
        {
            InitializeComponent();

            menuAdminAnterior = menuAdmin;

            // Convierte automáticamente las letras a mayúsculas.
            txtbox1_busquedaClientes.CharacterCasing = CharacterCasing.Upper;

            // Permite ingresar únicamente letras y espacios.
            txtbox1_busquedaClientes.KeyPress +=
                txtbox1_busquedaClientes_KeyPress;

            // Permite ejecutar la búsqueda al presionar Enter.
            txtbox1_busquedaClientes.KeyDown +=
                txtbox1_busquedaClientes_KeyDown;
        }

        // Evento que permite ingresar únicamente letras y espacios.
        private void txtbox1_busquedaClientes_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            // Permite letras, espacios y teclas de control,
            // como Retroceso para borrar.
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Evento que permite ejecutar la búsqueda al presionar Enter.
        private void txtbox1_busquedaClientes_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Evita el comportamiento predeterminado de Enter.
                e.SuppressKeyPress = true;

                // Ejecuta el evento del botón de búsqueda.
                boton1_busquedaClientes.PerformClick();
            }
        }

        // Evento que se ejecuta cuando se carga la pantalla de búsqueda.
        private void pantalla_busquedaClientes_Load(object sender, EventArgs e)
        {
            txtbox1_busquedaClientes.Focus();
        }

        // Evento del clic en la etiqueta.
        private void label1_busquedaClientes_Click(object sender, EventArgs e)
        {
        }

        // Evento que se ejecuta cuando cambia el texto del cuadro de búsqueda.
        private void txtbox1_busquedaClientes_TextChanged(object sender, EventArgs e)
        {
        }

        // BOTÓN 1: buscar por primer apellido.
        private void boton1_busquedaClientes_Click(object sender, EventArgs e)
        {
            // Obtiene el texto escrito y elimina los espacios externos.
            string apellido = txtbox1_busquedaClientes.Text.Trim();

            // Comprueba que el usuario haya escrito un apellido.
            if (string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show(
                    "Escribe el primer apellido del cliente.",
                    "Campo vacío",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox1_busquedaClientes.Focus();
                return;
            }

            try
            {
                // Obtiene los clientes que coinciden con el apellido.
                DataTable resultados = vistas.BuscarClientes(apellido);

                // Comprueba si no se encontró ningún cliente.
                if (resultados.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron clientes cuyo primer apellido sea: "
                        + apellido,
                        "Sin resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtbox1_busquedaClientes.Focus();
                    return;
                }

                // Crea la ventana de resultados.
                using (pantalla_resultadosClientes ventana =
                    new pantalla_resultadosClientes(resultados))
                {
                    // Abre la ventana de resultados.
                    ventana.ShowDialog(this);

                    // Si se seleccionó un cliente, abre su historial.
                    if (!string.IsNullOrWhiteSpace(ventana.RFCSeleccionado))
                    {
                        historialCompraCliente historial =
                            new historialCompraCliente(ventana.RFCSeleccionado);

                        historial.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al buscar los clientes.\n\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // BOTÓN 2: CANCELAR Y REGRESAR AL MENÚ ADMIN.
        private void boton2_busquedaClientes_Click(object sender, EventArgs e)
        {
            menuAdminAnterior.Show();
            menuAdminAnterior.Activate();
            this.Close();
        }
    }

    // Ventana que muestra los nombres completos de los clientes encontrados.
    public class pantalla_resultadosClientes : Form
    {
        // Declara la tabla visual que mostrará los resultados.
        private readonly DataGridView tablaClientes;

        // Almacena el RFC del cliente seleccionado.
        public string RFCSeleccionado { get; private set; }

        // Constructor que recibe los clientes encontrados.
        public pantalla_resultadosClientes(DataTable clientes)
        {
            Text = "Seleccionar cliente";
            Width = 700;
            Height = 450;
            StartPosition = FormStartPosition.CenterParent;

            // Crea y configura la tabla que mostrará los resultados.
            tablaClientes = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoGenerateColumns = true,
                DataSource = clientes
            };

            // Oculta el RFC, pero lo conserva para identificar al cliente.
            if (tablaClientes.Columns.Contains("RFC_Cliente"))
            {
                tablaClientes.Columns["RFC_Cliente"].Visible = false;
            }

            // Cambia el encabezado de la columna del nombre.
            if (tablaClientes.Columns.Contains("Nombre_Completo"))
            {
                tablaClientes.Columns["Nombre_Completo"].HeaderText =
                    "Nombre completo";
            }

            // Permite abrir el historial al hacer doble clic en un cliente.
            tablaClientes.CellDoubleClick +=
                tablaClientes_CellDoubleClick;

            Controls.Add(tablaClientes);
        }

        // Evento que se ejecuta al hacer doble clic sobre un cliente.
        private void tablaClientes_CellDoubleClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Obtiene el RFC del cliente seleccionado.
            RFCSeleccionado =
                tablaClientes.Rows[e.RowIndex]
                .Cells["RFC_Cliente"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(RFCSeleccionado))
            {
                MessageBox.Show(
                    "No se pudo identificar al cliente seleccionado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Cierra la ventana y devuelve el RFC seleccionado.
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}