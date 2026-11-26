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
    public partial class VSeleccionarFila : Form
    {
        public int idSeleccionado { get; set; }
        private int IdZona;
        private int IdFila;
        public VSeleccionarFila(int id)
        {
            InitializeComponent();
            IdZona = id;
        }

        private void CargarDatosFilas()
        {
            using (var db = new InventarioContext())
            {
                dgvFilas.DataSource = db.Filas
                    .Where(f => f.IDZona == IdZona)
                    .ToList();
            }
        }

        private void VSeleccionarFila_Load(object sender, EventArgs e)
        {
            CargarDatosFilas();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvFilas.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvFilas.SelectedRows[0];

                IdFila = Convert.ToInt32(fila.Cells["IDFila"].Value);
            }

            idSeleccionado = Convert.ToInt32(IdFila);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
