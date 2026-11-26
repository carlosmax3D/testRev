namespace Menu_p_Operativo
{
    public partial class pago_c_tdebito_exitoso : Form
    {
        public pago_c_tdebito_exitoso()
        {
            InitializeComponent();
        }

        private void fin_d_pago_tdebito_Click(object sender, EventArgs e)
        {
            menu_operador pantalla_menu_oper = new menu_operador();
            pantalla_menu_oper.Show();
            this.Hide();
        }
    }
}
