
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pantalla4_PrimerAvance_PAyMAD;
using Pantalla_HistorialUsuarios_PIA_PA;

namespace Pantalla_MenuUsuario_PIA_PA
{
    // Define el formulario que funciona como menú principal de las opciones de usuario.
    public partial class Pantalla_menuUsuario : Form
    {
        // Constructor del formulario.
        public Pantalla_menuUsuario()
        {
            // Inicializa los controles visuales creados en el diseñador de Windows Forms.
            InitializeComponent();
        }

        // Evento que se ejecuta cuando se carga la pantalla del menú de usuario.
        private void Pantalla_menuUsuario_Load(object sender, EventArgs e)
        {
            // Actualmente no se realiza ninguna acción al cargar el formulario.
        }

        // BOTÓN 1: REGISTRAR USUARIO.
        private void boton1_menuUsuario_Click(object sender, EventArgs e)
        {
            // Oculta el menú de usuario mientras se abre la pantalla de registro.
            this.Hide();

            // Crea una instancia del formulario para registrar usuarios.
            // El bloque using permite liberar sus recursos al terminar de utilizarlo.
            using (pantalla_registroUsuario registro =
                new pantalla_registroUsuario())
            {
                // Abre el formulario de registro como ventana modal.
                // El menú permanece oculto hasta que se cierre esta ventana.
                registro.ShowDialog();
            }

            // Vuelve a mostrar el menú de usuario después de cerrar el registro.
            this.Show();
        }

        // BOTÓN 2: HISTORIAL DE USUARIOS.
        private void boton2_menuUsuario_Click(object sender, EventArgs e)
        {
            // Oculta el menú principal mientras se consulta el historial.
            this.Hide();

            // Crea una instancia del formulario que muestra los usuarios registrados.
            using (pantalla_historialUsuario historial =
                new pantalla_historialUsuario())
            {
                // Abre el historial como ventana modal.
                // La ejecución continúa cuando el usuario cierra esa pantalla.
                historial.ShowDialog();
            }

            // Muestra nuevamente el menú de usuario al cerrar el historial.
            this.Show();
        }

        // BOTÓN 3: CERRAR MENÚ.
        private void boton3_menuUsuario_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual del menú de usuario.
            this.Close();
        }
    }
}
