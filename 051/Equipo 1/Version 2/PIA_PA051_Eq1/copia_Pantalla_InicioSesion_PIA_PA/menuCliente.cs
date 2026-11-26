
using System;
using System.Windows.Forms;
using Pantalla_RegistroCliente_PIA_PA;
using Pantalla_HistorialCliente_PIA_PA;
using Pantalla_Facturacion_PIA_PA;
using Prueba2_PrimerAvance_PAyMAD;

namespace Pantalla_MenuClientes_PIA_PA
{
    // Define el formulario que funciona como menú principal de las opciones de clientes.
    public partial class pantalla_menuCliente : Form
    {
        // Guarda la referencia al menú del administrador desde el cual se abrió
        // este formulario, para poder regresar a él.
        private readonly pantalla_menuAdmin menuAdminAnterior;

        // Almacena la nómina del usuario que inició sesión.
        // Se utiliza para identificar al usuario que realiza determinadas operaciones.
        private readonly int nominaUsuario;

        // Indica si el usuario que inició sesión tiene permisos de administrador.
        private readonly bool esAdministrador;

        // Constructor que recibe el menú anterior, la nómina y el tipo de usuario.
        public pantalla_menuCliente(
            pantalla_menuAdmin menuAdmin,
            int nomina,
            bool administrador)
        {
            // Inicializa los controles visuales creados en el diseñador de Windows Forms.
            InitializeComponent();

            // Guarda la referencia al menú del administrador para regresar posteriormente.
            menuAdminAnterior = menuAdmin;

            // Almacena la nómina recibida desde la pantalla anterior.
            nominaUsuario = nomina;

            // Guarda si el usuario actual es administrador.
            esAdministrador = administrador;
        }

        // Evento que se ejecuta cuando se carga la pantalla del menú de clientes.
        private void pantalla_menuCliente_Load(object sender, EventArgs e)
        {
            // Actualmente no se realiza ninguna acción al cargar el formulario.
        }

        // BOTÓN 1: HISTORIAL DE CLIENTES.
        // BOTÓN 1: HISTORIAL DE CLIENTES.
        private void boton1_menuCliente_Click(object sender, EventArgs e)
        {
            // Oculta el menú de clientes mientras se consulta el historial.
            this.Hide();

            // Crea la pantalla del historial de clientes.
            // Se envían la referencia al menú anterior y la nómina del usuario.
            using (pantalla_historialCliente historial =
                   new pantalla_historialCliente(this, nominaUsuario))
            {
                // Abre el historial como ventana modal.
                historial.ShowDialog();
            }

            // Vuelve a mostrar el menú de clientes después de cerrar el historial.
            this.Show();

            // Activa el menú para que quede al frente de las demás ventanas.
            this.Activate();
        }

        // BOTÓN 2: REGISTRAR CLIENTE.
        private void boton2_menuCliente_Click(object sender, EventArgs e)
        {
            // Oculta el menú de clientes antes de abrir el formulario de registro.
            this.Hide();

            // Crea la pantalla para registrar un nuevo cliente.
            // Envía la nómina y el indicador de administrador para que el formulario
            // pueda utilizar esos datos durante el registro.
            using (pantalla_registroCliente registro =
                   new pantalla_registroCliente(
                       nominaUsuario, esAdministrador))
            {
                // Abre la pantalla de registro como ventana modal.
                // El usuario debe cerrar esa pantalla para regresar al menú.
                registro.ShowDialog();
            }

            // Muestra nuevamente el menú de clientes al cerrar el registro.
            this.Show();

            // Activa el menú para devolverle el enfoque.
            this.Activate();
        }

        // BOTÓN 3: MENÚ FACTURA.
        private void boton3_menuCliente_Click(object sender, EventArgs e)
        {
            // Oculta el menú de clientes mientras se utiliza el menú de facturación.
            this.Hide();

            // Crea una instancia de la pantalla del menú de facturación.
            using (pantalla_Facturación factura =
                   new pantalla_Facturación())
            {
                // Abre el menú de facturación como ventana modal.
                factura.ShowDialog();
            }

            // Vuelve a mostrar el menú de clientes al cerrar la pantalla de facturación.
            this.Show();

            // Activa el menú para que quede en primer plano.
            this.Activate();
        }

        // BOTÓN 4: VOLVER AL MENÚ ADMINISTRADOR.
        private void boton4_menuCliente_Click(object sender, EventArgs e)
        {
            // Comprueba que exista una referencia al menú del administrador.
            if (menuAdminAnterior != null)
            {
                // Muestra nuevamente el menú del administrador.
                menuAdminAnterior.Show();

                // Activa el menú del administrador para dejarlo en primer plano.
                menuAdminAnterior.Activate();
            }

            // Cierra el formulario actual del menú de clientes.
            this.Close();
        }
    }
}
