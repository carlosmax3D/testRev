
using System;
using System.Windows.Forms;
using Pantalla_CatalogosRegistrados;
using Pantalla_RegistroCatalogos_PIA_PA;

namespace Prueba2_PrimerAvance_PAyMAD
{
    // Define el formulario que funciona como menú de opciones para administrar catálogos.
    public partial class pantalla_menucatalogo : Form
    {
        // Guarda la referencia al menú del administrador desde el cual se abrió esta pantalla.
        private readonly pantalla_menuAdmin menuAdminAnterior;

        // Guarda la nómina del administrador que inició sesión.
        private readonly int NominaAdmin;

        // Recibe el menú administrador y la nómina del usuario.
        public pantalla_menucatalogo(
            pantalla_menuAdmin menuAdmin, int nominaAdmin)
        {
            // Inicializa los controles visuales del formulario.
            InitializeComponent();

            // Guarda la referencia al menú administrador.
            menuAdminAnterior = menuAdmin;

            // Guarda la nómina recibida.
            NominaAdmin = nominaAdmin;
        }

        // BOTÓN 1: Catálogos registrados.
        private void boton1_menucatalogo_Click(
            object sender, EventArgs e)
        {
            // Oculta el menú de catálogos.
            this.Hide();

            try
            {
                // Crea el formulario de consulta de catálogos.
                using (pantalla_catalogosRegistrados catalogos =
                    new pantalla_catalogosRegistrados())
                {
                    // Abre la pantalla como ventana modal.
                    catalogos.ShowDialog();
                }
            }
            finally
            {
                // Vuelve a mostrar el menú al cerrar la consulta.
                if (!IsDisposed)
                    this.Show();
            }
        }

        // BOTÓN 2: Registrar catálogos.
        private void boton2_menucatalogo_Click(
            object sender, EventArgs e)
        {
            // Oculta el menú antes de abrir el registro.
            this.Hide();

            try
            {
                // Crea el formulario de registro.
                // Envía la nómina del administrador.
                using (pantalla_registrocatalogos registro =
                    new pantalla_registrocatalogos(NominaAdmin))
                {
                    // Abre el formulario como ventana modal.
                    registro.ShowDialog();
                }
            }
            finally
            {
                // Vuelve a mostrar el menú al cerrar el registro.
                if (!IsDisposed)
                    this.Show();
            }
        }

        // BOTÓN 3: Regresar al menú administrador.
        private void boton3_menucatalogo_Click(
            object sender, EventArgs e)
        {
            // Comprueba que exista el menú administrador anterior.
            if (menuAdminAnterior == null ||
                menuAdminAnterior.IsDisposed)
            {
                MessageBox.Show(
                    "No se encontró el menú administrador anterior.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Cierra el menú de catálogos.
                this.Close();
                return;
            }

            // Muestra nuevamente el menú administrador.
            menuAdminAnterior.Show();

            // Activa el menú administrador.
            menuAdminAnterior.Activate();

            // Cierra el menú de catálogos.
            this.Close();
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void pantalla_menucatalogo_Load(
            object sender, EventArgs e)
        {
        }
    }
}
