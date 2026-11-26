namespace Menu_p_Operativo
{
    public partial class caution_cliente_repetido : Form
    {
        public caution_cliente_repetido()
        {
            InitializeComponent();
        }

        private void ok_pa_cerrar_cyaexiste_Click(object sender, EventArgs e)
        {
            //se abre nuevamente el submenu de cliente para que se cierre
            dclientes_eligio_registrar regreso_a_regist_client = new dclientes_eligio_registrar();
            regreso_a_regist_client.Show();
            //CERRAR el submenu de clientes 
            this.Close();
        }

        private void caution_cliente_repetido_Load(object sender, EventArgs e)
        {

        }
    }
}
