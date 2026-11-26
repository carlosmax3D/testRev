
using System.Data;

namespace Menu_p_Operativo
{
    public partial class resultbusq_ya_existia : Form
    {
        public DataRow ClienteEncontrado { get; set; }

        public resultbusq_ya_existia()
        {
            InitializeComponent();
        }

        private void ok_a_client_ya_regis_Click(object sender, EventArgs e)
        {
            // Cierra el resultado y permite regresar a la búsqueda.
            this.Close();
        }

        private void resultbusq_ya_existia_Load(object sender, EventArgs e)
        {
        }
    }
}
