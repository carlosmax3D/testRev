using CARR_PA_Proyecto_052.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class VSeleccionarAsiento : Form
    {
        public int idSeleccionado { get; set; }
        private int IdFila;
        private List<int> idsExcluidos;

        public VSeleccionarAsiento(int idfila, int idSelect1 = -1, int idSelect2 = -1, int idSelect3 = -1, int idSelect4 = -1)
        {
            InitializeComponent();
            IdFila = idfila;

            idsExcluidos = new[] { idSelect1, idSelect2, idSelect3, idSelect4 }.Where(x => x != -1).ToList();
        }

        private void CargarDatosAsientos()
        {
            using (var db = new InventarioContext())
            {
                var asientos = db.Asientos
                    .Where(A => A.IDFila == IdFila && !idsExcluidos.Contains(A.IDAsiento))
                    .ToList();

                dgvAsientos.DataSource = asientos;
            }
        }

        private void VSeleccionarAsiento_Load(object sender, EventArgs e)
        {
            CargarDatosAsientos();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvAsientos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un asiento de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila = dgvAsientos.SelectedRows[0];
            idSeleccionado = Convert.ToInt32(fila.Cells["IDAsiento"].Value);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
