using CARR_PA_Proyecto_052.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public partial class ConsClientes : Form
    {
        public ConsClientes()
        {
            InitializeComponent();
        }
        private void CargarDatosClientes()
        {
            using (var db = new InventarioContext())
            {
                dgvClientes.DataSource = db.Clientes.ToList();
                dgvTelefonosCliente.DataSource = db.TelefonosClientes.ToList();
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            CargarDatosClientes();
        }
    }
}
