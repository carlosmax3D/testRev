using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateCSharp.Forms.Usuarios
{
    public partial class FrmUsuarioCreado : Form
    {
        public FrmUsuarioCreado()
        {
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
