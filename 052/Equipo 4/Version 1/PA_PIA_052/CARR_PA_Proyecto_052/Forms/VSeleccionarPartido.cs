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
    public partial class VSeleccionarPartido : Form
    {
        public string PartidoSeleccionado { get; set; }
        public int idEventoSeleccionado { get; set; }
        private int IdEstadio;
        private string Partido;
        public VSeleccionarPartido(int id)
        {
            InitializeComponent();
            IdEstadio = id;
        }

        private void CargarDatosPartidos()
        {
            using (var db = new InventarioContext())
            {
                var cargador = new CargarPartidosDisponibles();
                int idEstadioSeleccionado = Convert.ToInt32(IdEstadio);
                cargador.Ejecutar(dgvPartidos, idEstadioSeleccionado);
            }
        }

        private void VSeleccionarPartido_Load(object sender, EventArgs e)
        {
            CargarDatosPartidos();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvPartidos.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvPartidos.SelectedRows[0];

                idEventoSeleccionado = Convert.ToInt32(fila.Cells["ID_Partido"].Value);
                PartidoSeleccionado = Convert.ToString(fila.Cells["Enfrentamiento"].Value);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
