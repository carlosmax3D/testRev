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
    public partial class VSeleccionarZona : Form
    {
        public int idSeleccionado { get; set; }
        private int IdZona;
        private int idEstadio;
        public VSeleccionarZona(int id)
        {
            InitializeComponent();
            idEstadio = id;
        }
        private void CargarDatosZonas()
        {
            using (var db = new InventarioContext())
            {
                dgvZonas.DataSource = db.Zonas
                    .Where(z => z.IDEst == idEstadio)
                    .ToList();
            }
        }

        private void VSeleccionarZona_Load(object sender, EventArgs e)
        {
            CargarDatosZonas();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvZonas.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvZonas.SelectedRows[0];

                IdZona = Convert.ToInt32(fila.Cells["IDZona"].Value);
            }

            idSeleccionado = Convert.ToInt32(IdZona);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
