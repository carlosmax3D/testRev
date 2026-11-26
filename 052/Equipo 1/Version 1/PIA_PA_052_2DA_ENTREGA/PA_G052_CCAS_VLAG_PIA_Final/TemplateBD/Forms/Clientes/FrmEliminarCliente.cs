using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateCSharp.Forms.Clientes
{
    public partial class FrmEliminarCliente : Form
    {
        public FrmEliminarCliente()
        {
            InitializeComponent();
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cliente eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
