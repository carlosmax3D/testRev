using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class RegPartido : Form
    {
        private int IDEstadio;
        private void CargarDatosEstadios()
        {
            using (var db = new InventarioContext())
            {
                dgvEstadios.DataSource = db.Estadios.ToList();
            }
        }
        private void ValidarCamposRegistro()
        {
            bool camposCompletos = !string.IsNullOrWhiteSpace(cajatxtEqLocal.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtEqVis.Text);

            bool estadioSeleccionado = dgvEstadios.SelectedRows.Count > 0;

            btnConfirmar.Enabled = camposCompletos && estadioSeleccionado;
        }
        public RegPartido()
        {
            InitializeComponent();
        }

        private void RegPartido_Load(object sender, EventArgs e)
        {

            dtpFechaPartido.MinDate = DateTime.Now;

            CargarDatosEstadios();

            btnConfirmar.Enabled = false;

            cajatxtEqLocal.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtEqVis.TextChanged += (s, ev) => ValidarCamposRegistro();
            dgvEstadios.SelectionChanged += (s, ev) => ValidarCamposRegistro();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (dgvEstadios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un estadio de la lista.", "Estadio no seleccionado", MessageBoxButtons.OK);
                return;
            }

            DataGridViewRow fila = dgvEstadios.SelectedRows[0];
            IDEstadio = Convert.ToInt32(fila.Cells["IDEst"].Value);

            DialogResult resultado = MessageBox.Show("¿Están los datos del partido correctos?", "Confirmación", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    RegistrarPartido RegP = new RegistrarPartido();
                    int idPartido = RegP.RegistrarP(dtpFechaPartido.Value, dtpHoraPartido.Value, IDEstadio);

                    RegistrarEquiposAJugar RegEqAJ = new RegistrarEquiposAJugar();
                    RegEqAJ.RegistrarEqAJug(cajatxtEqLocal.Text, cajatxtEqVis.Text, idPartido);

                    MessageBox.Show("Partido registrado exitosamente.", "Éxito", MessageBoxButtons.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al registrar el partido:\n" + ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }
    }
}
