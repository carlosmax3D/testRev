
using Pantalla_BusquedaCompraCliente_PIA_PA;
using Pantalla_InicioSesion_PIA_PA;
using Pantalla_MenuClientes_PIA_PA;
using Pantalla_MenuUsuario_PIA_PA;
using Pantalla_RegistroVentasBoletos_PIA_PA;
using Pantalla3_PrimerAvancePAyMAD;

using System;
using System.Windows.Forms;

namespace Prueba2_PrimerAvance_PAyMAD
{
    // Define el formulario que funciona como menú principal del administrador.
    public partial class pantalla_menuAdmin : Form
    {
        // Almacena la nómina del usuario que inició sesión.
        private int nomina;

        // Almacena el tipo de usuario que inició sesión.
        private string tipoUsuario = "";

        // Constructor para el diseñador.
        public pantalla_menuAdmin()
        {
            // Inicializa los controles visuales del formulario.
            // Este constructor permite que el diseñador de Windows Forms
            // pueda crear y mostrar la pantalla.
            InitializeComponent();
        }

        // Constructor que recibe la sesión.
        public pantalla_menuAdmin(int nominaUsuario, string tipo)
        {
            // Inicializa los controles visuales del formulario.
            InitializeComponent();

            // Guarda la nómina del usuario recibida desde el inicio de sesión.
            nomina = nominaUsuario;

            // Guarda el tipo de usuario recibido, por ejemplo, ADMIN.
            tipoUsuario = tipo;
        }

        // Evento que se ejecuta cuando se carga el menú del administrador.
        private void pantalla_menuAdmin_Load(object sender, EventArgs e)
        {
            // Actualmente no se ejecuta ninguna acción al cargar la pantalla.
        }

        // Evento asociado al primer texto o etiqueta del menú.
        private void label1_menuAdmin_Click(object sender, EventArgs e)
        {
            // Actualmente no se realiza ninguna acción al hacer clic en esta etiqueta.
        }

        // Evento asociado al segundo texto o etiqueta del menú.
        private void label2_menuAdmin_Click(object sender, EventArgs e)
        {
            // Actualmente no se realiza ninguna acción al hacer clic en esta etiqueta.
        }

        // BOTÓN 1: MENÚ CATÁLOGO.
        private void boton1_menuAdmin_Click(object sender, EventArgs e)
        {
            // Oculta el menú del administrador mientras se utiliza el catálogo.
            this.Hide();

            try
            {
                // Crea la pantalla del catálogo y le pasa una referencia
                // al menú actual para permitir regresar a él.
                using (pantalla_menucatalogo catalogo =
                    new pantalla_menucatalogo(this, nomina))
                {
                    // Abre el catálogo como ventana modal.
                    // El código espera hasta que se cierre esa ventana.
                    catalogo.ShowDialog();
                }
            }
            finally
            {
                // Comprueba que el menú no haya sido eliminado o liberado.
                if (!this.IsDisposed)
                {
                    // Vuelve a mostrar el menú del administrador.
                    this.Show();

                    // Activa el menú para que quede al frente.
                    this.Activate();
                }
            }
        }

        // BOTÓN 2: MENÚ CLIENTE.
        private void boton2_menuAdmin_Click(object sender, EventArgs e)
        {
            // No rechazar la nómina 0 del administrador.
            // Comprueba que se haya recibido el tipo de usuario de la sesión.
            if (string.IsNullOrWhiteSpace(tipoUsuario))
            {
                // Informa al usuario que falta el tipo de cuenta de la sesión
                // y muestra la nómina recibida para facilitar la revisión.
                MessageBox.Show(
                    "No se recibió el tipo de usuario de la sesión.\n\n" +
                    "Nómina recibida: " + nomina + "\n" +
                    "Tipo de usuario: vacío\n\n" +
                    "Verifica que el menú se abra desde el inicio de sesión.",
                    "Error de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Detiene el método para no abrir el menú sin los datos necesarios.
                return;
            }

            // Compara el tipo de usuario con "ADMIN".
            // StringComparison.OrdinalIgnoreCase permite comparar sin distinguir
            // entre mayúsculas y minúsculas.
            bool esAdministrador = tipoUsuario.Equals(
                "ADMIN",
                StringComparison.OrdinalIgnoreCase
            );

            // Oculta el menú del administrador antes de abrir el menú de clientes.
            this.Hide();

            try
            {
                // Crea el menú de clientes y le pasa:
                // la referencia al menú actual, la nómina y si es administrador.
                using (pantalla_menuCliente menuClientes =
                    new pantalla_menuCliente(
                        this,
                        nomina,
                        esAdministrador))
                {
                    // Abre el menú de clientes como ventana modal.
                    menuClientes.ShowDialog();
                }
            }
            finally
            {
                // Comprueba que el formulario original siga disponible.
                if (!this.IsDisposed)
                {
                    // Muestra nuevamente el menú del administrador.
                    this.Show();

                    // Activa el menú para dejarlo en primer plano.
                    this.Activate();
                }
            }
        }

        // BOTÓN 3: CONDUCTA OPERATIVO.
        private void boton3_menuAdmin_Click(object sender, EventArgs e)
        {
            // Oculta el menú del administrador mientras se abre la otra pantalla.
            this.Hide();

            try
            {
                // Crea la pantalla relacionada con la conducta del personal operativo.
                // Se pasa la referencia al menú actual para facilitar el regreso.
                using (pantalla_conductaOpe conductaOpe =
                    new pantalla_conductaOpe(this))
                {
                    // Muestra la pantalla como ventana modal.
                    conductaOpe.ShowDialog();
                }
            }
            finally
            {
                // Se ejecuta al terminar el bloque try, incluso si ocurre un error.
                // Verifica que el menú original todavía exista.
                if (!this.IsDisposed)
                {
                    // Vuelve a mostrar el menú del administrador.
                    this.Show();

                    // Coloca el menú en primer plano.
                    this.Activate();
                }
            }
        }

        // BOTÓN 4: MENÚ USUARIO.
        private void boton4_menuAdmin_Click(object sender, EventArgs e)
        {
            // Oculta el menú del administrador antes de abrir el menú de usuarios.
            this.Hide();

            try
            {
                // Crea la pantalla del menú de usuarios.
                // El bloque using libera sus recursos cuando termina.
                using (Pantalla_menuUsuario menuUsuario =
                    new Pantalla_menuUsuario())
                {
                    // Abre el menú de usuarios como ventana modal.
                    menuUsuario.ShowDialog();
                }
            }
            finally
            {
                // Comprueba que el menú del administrador no se haya eliminado.
                if (!this.IsDisposed)
                {
                    // Muestra de nuevo el menú del administrador.
                    this.Show();

                    // Activa el menú para que quede al frente.
                    this.Activate();
                }
            }
        }

        // BOTÓN 5: REGISTRO DE VENTA DE BOLETOS.
        private void boton5_menuAdmin_Click_1(object sender, EventArgs e)
        {
            // Oculta el menú principal antes de abrir el registro de ventas.
            this.Hide();

            try
            {
                // Crea el formulario de registro de venta de boletos.
                // Se pasa la referencia al menú actual.
                using (pantalla_registroVB registroVenta =
                    new pantalla_registroVB(this))
                {
                    // Muestra el registro de ventas como ventana modal.
                    registroVenta.ShowDialog();
                }
            }
            finally
            {
                // Comprueba que el menú del administrador siga disponible.
                if (!this.IsDisposed)
                {
                    // Muestra otra vez el menú principal.
                    this.Show();

                    // Activa el formulario para devolverle el enfoque.
                    this.Activate();
                }
            }
        }

        // BOTÓN 6: BÚSQUEDA DE CLIENTES.
        private void boton6_menuAdmin_Click(object sender, EventArgs e)
        {
            // Oculta el menú del administrador mientras se realiza la búsqueda.
            this.Hide();

            try
            {
                // Crea la pantalla de búsqueda de clientes y le pasa
                // una referencia al menú actual.
                using (pantalla_busquedaClientes busquedaClientes =
                    new pantalla_busquedaClientes(this))
                {
                    // Abre la pantalla de búsqueda como ventana modal.
                    busquedaClientes.ShowDialog();
                }
            }
            finally
            {
                // Comprueba que el menú original no haya sido eliminado.
                if (!this.IsDisposed)
                {
                    // Vuelve a mostrar el menú del administrador.
                    this.Show();

                    // Activa el menú para que quede al frente.
                    this.Activate();
                }
            }
        }

        // BOTÓN 7: CERRAR SESIÓN.
        private void boton7_menuAdmin_Click(object sender, EventArgs e)
        {
            // Preguntar si el usuario está seguro de cerrar sesión.
            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de que deseas cerrar sesión?",
                "Confirmar cierre de sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            // Si selecciona Sí, regresar al inicio de sesión.
            if (respuesta == DialogResult.Yes)
            {
                // Crear una nueva instancia del inicio de sesión.
                pantalla_inicioSesion inicioSesion = new pantalla_inicioSesion();

                // Mostrar la pantalla de inicio de sesión.
                inicioSesion.Show();

                // Ocultar el menú del administrador sin cerrar el programa.
                this.Hide();
            }
        }

        // Si selecciona No, no hacer nada y permanecer en el menú.
    }
    
}
