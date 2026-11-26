namespace Menu_p_Operativo
{
    public partial class p_efectivo_pago_exacto : Form
    {
        public p_efectivo_pago_exacto()
        {
            InitializeComponent();
        }

        private void fin_met_p_efect_sin_feria_Click(object sender, EventArgs e)
        {
            menu_operador pantalla_menu_oper = new menu_operador();
            pantalla_menu_oper.Show();
            this.Hide();
        }

        private void p_efectivo_pago_exacto_Load(object sender, EventArgs e)
        {

        }
    }
}
