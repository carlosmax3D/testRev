using CARR_PA_Proyecto_052.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class VSeleccionarCte : Form
    {
        public int idSeleccionado { get; set; }
        private int IdCliente;

        public VSeleccionarCte()
        {
            InitializeComponent();
        }

        private void CargarDatosClientes()
        {
            using (var db = new InventarioContext())
            {
                dgvClientes.DataSource = db.VistaInformacionCliente.ToList();
            }
        }

        private void VSeleccionarCte_Load(object sender, EventArgs e)
        {
            CargarDatosClientes();
        }

        
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvClientes.SelectedRows[0];

                IdCliente = Convert.ToInt32(fila.Cells["IDCts"].Value);
            }

            idSeleccionado = Convert.ToInt32(IdCliente);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}
