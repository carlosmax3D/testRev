using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms.VentaBoletos
{
    public partial class FrmCompletadaVenta : Form
    {
        public FrmCompletadaVenta()
        {
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void FrmCompletadaVenta_Load(object sender, EventArgs e)
        {
            // Mostramos el folio real de la venta
            if (VentaContext.UltimoFolioVenta != Guid.Empty)
            {
                lblGUID.Text = VentaContext.UltimoFolioVenta.ToString().ToUpper();
            }
        }
    }
}
