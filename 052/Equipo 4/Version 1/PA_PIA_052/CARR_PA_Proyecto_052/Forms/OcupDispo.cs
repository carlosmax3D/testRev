using CARR_PA_Proyecto_052.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public partial class OcupDispo : Form
    {
        public OcupDispo()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            try 
            {
                using (var db = new InventarioContext())
                {
                    dgv_OyD.DataSource = db.VistaOcupacionDisponibilidad.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la ocupación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
