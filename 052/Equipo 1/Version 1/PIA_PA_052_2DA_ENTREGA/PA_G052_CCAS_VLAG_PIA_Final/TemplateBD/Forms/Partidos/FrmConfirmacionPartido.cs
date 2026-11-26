using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateCSharp.Forms.Partidos
{
    public partial class FrmConfirmacionPartido : Form
    {
        public FrmConfirmacionPartido()
        {
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
