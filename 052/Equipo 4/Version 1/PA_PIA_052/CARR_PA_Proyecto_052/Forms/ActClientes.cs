using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Forms;
using CARR_PA_Proyecto_052.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public partial class ActClientes : Form
    {
        private string UsuarioSesion;
        public ActClientes(string nombre)
        {
            InitializeComponent();
            UsuarioSesion = nombre; 
        }

        private void CargarDatosClientes()
        {
            using (var db = new InventarioContext())
            {
                dgvClientes.DataSource = db.VistaInformacionCliente.ToList();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            CargarDatosClientes();
        }

        private int IdCliente;

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvClientes.SelectedRows[0];

                IdCliente = Convert.ToInt32(fila.Cells["IDCts"].Value);
            }

            ACModificar ventanaModifcar = new ACModificar(IdCliente, UsuarioSesion);
            this.Hide();
            ventanaModifcar.ShowDialog();
            this.Show();
        }
    }
}
