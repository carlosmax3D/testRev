namespace Menu_p_Operativo
{
    public partial class p_credito_exitoso : Form
    {
        public p_credito_exitoso()
        {
            InitializeComponent();
        }

        private void fin_d_pago_credito_Click(object sender, EventArgs e)
        {
            menu_operador pantalla_menu_oper = new menu_operador();
            pantalla_menu_oper.Show();
            this.Hide();
        }
    }
}
