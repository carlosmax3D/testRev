using CARR_PA_Proyecto_052.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class VSeleccionarEstadios : Form
    {
        public int idSeleccionado { get; set; }
        private int IdEstadio;
        public VSeleccionarEstadios()
        {
            InitializeComponent();
        }

        private void CargarDatosEstadios()
        {
            using (var db = new InventarioContext())
            {
                dgvEstadios.DataSource = db.Estadios.ToList();
            }
        }

        private void VSeleccionarEstadios_Load(object sender, EventArgs e)
        {
            CargarDatosEstadios();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvEstadios.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvEstadios.SelectedRows[0];

                IdEstadio = Convert.ToInt32(fila.Cells["IDEst"].Value);
            }

            idSeleccionado = Convert.ToInt32(IdEstadio);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
