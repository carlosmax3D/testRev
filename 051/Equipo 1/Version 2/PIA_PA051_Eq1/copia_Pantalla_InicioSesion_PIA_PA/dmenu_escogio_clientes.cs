
using System;
using System.Windows.Forms;

namespace Menu_p_Operativo
{
    public partial class dmenu_eligio_clientes : Form
    {
        // Conserva la nómina del operador.
        public int NominaOperador { get; private set; }

        // Constructor sin parámetros.
        public dmenu_eligio_clientes()
        {
            InitializeComponent();
            NominaOperador = 0;
        }

        // Constructor que recibe la nómina del operador.
        public dmenu_eligio_clientes(int nominaUsuario)
        {
            InitializeComponent();
            NominaOperador = nominaUsuario;
        }

        // Abre la pantalla para registrar clientes.
        private void c_opcion_regisclient_Click(object sender, EventArgs e)
        {
            // Crea la pantalla de registro y transmite la nómina.
            dclientes_eligio_registrar pantalla_reg_Clientes =
                new dclientes_eligio_registrar(NominaOperador);

            // Cuando se cierre el registro, vuelve a mostrar
            // el mismo menú de clientes.
            pantalla_reg_Clientes.FormClosed += (s, args) =>
            {
                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                }
            };

            // Abre la pantalla de registro y oculta el menú.
            pantalla_reg_Clientes.Show();
            this.Hide();
        }

        // Regresa al menú principal del operador.
        private void regresar_a_menu_oper_Click(object sender, EventArgs e)
        {
            // Cierra el menú de clientes.
            // El menú operativo original debe mostrarse
            // mediante su evento FormClosed.
            this.Close();
        }

        // Abre la pantalla para buscar clientes.
        private void c_opcion_buscarclient_Click(object sender, EventArgs e)
        {
            // Crea la pantalla de búsqueda.
            dcliente_eligio_buscar pantalla_busq_Clientes =
                new dcliente_eligio_buscar();

            // Cuando se cierre la búsqueda, vuelve a mostrar
            // el mismo menú de clientes.
            pantalla_busq_Clientes.FormClosed += (s, args) =>
            {
                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                }
            };

            // Abre la búsqueda y oculta el menú de clientes.
            pantalla_busq_Clientes.Show();
            this.Hide();
        }

        private void dmenu_eligio_clientes_Load(object sender, EventArgs e)
        {
        }
    }
}
