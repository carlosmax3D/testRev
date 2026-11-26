using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateCSharp.Forms.Estadios
{
    public partial class FrmConfirmacionAsiento : Form
    {
        public FrmConfirmacionAsiento()
        {
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
