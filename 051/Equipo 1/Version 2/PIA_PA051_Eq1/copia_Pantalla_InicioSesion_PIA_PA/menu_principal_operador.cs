
// Importa las clases básicas de C#, como EventArgs.
using System;

// Permite utilizar los componentes básicos de Windows Forms.
using System.Windows.Forms;

// Define el espacio de nombres donde se encuentra esta clase.
namespace Menu_p_Operativo
{
    // Declara la clase del formulario del menú del operador.
    public partial class menu_operador : Form
    {
        // Guarda la nómina del operativo que inició sesión.
        public int NominaOperador { get; private set; }

        // Conserva la instancia de la pantalla de venta.
        private dmenu_eligio_ventaboleto pantallaVenta;

        // Conserva la instancia de la pantalla de clientes.
        private dmenu_eligio_clientes pantallaClientes;

        // Conserva la instancia de evaluación gerencial.
        private dmenu_eligio_ev_geren pantallaEvGerencial;

        // Conserva la instancia de reportes.
        private eligio_opcion_reportes pantallaReportes;

        // Constructor sin parámetros para mantener compatibilidad
        // con otras pantallas que abran el menú sin enviar una nómina.
        public menu_operador()
        {
            InitializeComponent();
            NominaOperador = 0;
        }

        // Constructor que recibe la nómina desde el inicio de sesión.
        public menu_operador(int nominaUsuario)
        {
            InitializeComponent();
            NominaOperador = nominaUsuario;
        }

        // Abre el submenú de clientes y le transmite la nómina.
        private void opcion_clientes_Click(object sender, EventArgs e)
        {
            // Evita abrir varias pantallas de clientes.
            if (pantallaClientes != null && !pantallaClientes.IsDisposed)
            {
                pantallaClientes.Show();
                pantallaClientes.Activate();
                this.Hide();
                return;
            }

            // Crea el menú de clientes y transmite la nómina.
            pantallaClientes =
                new dmenu_eligio_clientes(NominaOperador);

            // Cuando se cierre el menú de clientes,
            // recupera el menú operativo original.
            pantallaClientes.FormClosed += (s, args) =>
            {
                pantallaClientes = null;

                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                }
            };

            // Abre el menú de clientes y oculta el menú operativo.
            pantallaClientes.Show();
            this.Hide();
        }

        // Cierra la sesión y regresa a la pantalla de inicio de sesión.
        private void cerrar_secion_operativo_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                // Cierra únicamente el menú operativo.
                // El inicio de sesión se mostrará nuevamente.
                this.Close();
            }
        }

        // Abre la pantalla de evaluación gerencial.
        private void opcion_evaluaciongerencial_Click(object sender, EventArgs e)
        {
            if (pantallaEvGerencial != null && !pantallaEvGerencial.IsDisposed)
            {
                pantallaEvGerencial.Show();
                pantallaEvGerencial.Activate();
                this.Hide();
                return;
            }

            pantallaEvGerencial = new dmenu_eligio_ev_geren();

            pantallaEvGerencial.FormClosed += (s, args) =>
            {
                pantallaEvGerencial = null;

                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                }
            };

            pantallaEvGerencial.Show();
            this.Hide();
        }

        // Abre la pantalla de reportes.
        private void opcion_reportes_Click(object sender, EventArgs e)
        {
            if (pantallaReportes != null && !pantallaReportes.IsDisposed)
            {
                pantallaReportes.Show();
                pantallaReportes.Activate();
                this.Hide();
                return;
            }

            pantallaReportes = new eligio_opcion_reportes();

            pantallaReportes.FormClosed += (s, args) =>
            {
                pantallaReportes = null;

                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                }
            };

            pantallaReportes.Show();
            this.Hide();
        }

        // Abre la pantalla de venta de boletos.
        private void opcion_ventaboletos_Click(object sender, EventArgs e)
        {
            // Evita abrir varias pantallas de venta al mismo tiempo.
            if (pantallaVenta != null && !pantallaVenta.IsDisposed)
            {
                pantallaVenta.Show();
                pantallaVenta.Activate();
                this.Hide();
                return;
            }

            // Crea la pantalla de venta y transmite la nómina.
            pantallaVenta =
                new dmenu_eligio_ventaboleto(NominaOperador);

            // Cuando se cierre la pantalla de venta,
            // recupera el menú operativo.
            pantallaVenta.FormClosed += (s, args) =>
            {
                pantallaVenta = null;

                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                }
            };

            // Abre la pantalla de venta y oculta el menú operativo.
            pantallaVenta.Show();
            this.Hide();
        }
    }
}
