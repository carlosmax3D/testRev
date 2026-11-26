using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VentaBoletosWF
{
    public partial class FormMenu : Form
    {

        string tipoUsuario;

        public FormMenu(string tipo)
        {
            InitializeComponent();
            tipoUsuario = tipo;
        }


        private void FormMenu_Load(object sender, EventArgs e)
        {
            if (tipoUsuario != "Administrador")
            {
                btnReportes.Enabled = false;
                btnEstadios.Enabled = false;
                btnPartidos.Enabled = false;
                btnZonas.Enabled = false;
                btnFacturacion.Enabled = false;
                btnCancelaciones.Enabled = false;
                //btnHistorialClientes.Enabled = false;
                btnDashboard.Enabled = false;
            }
        }



        private void btnClientes_Click(object sender, EventArgs e)
        {
            new FormClientes().Show();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            new FormVentas().Show();
        }

        private void btnCancelaciones_Click(object sender, EventArgs e)
        {
            new FormCancelacion().Show();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            new FormReportes().Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void btnEstadios_Click(object sender, EventArgs e)
        {
            new FormEstadios().Show();
        }

        private void btnPartidos_Click(object sender, EventArgs e)
        {
            new FormPartidos().Show();
        }

        private void btnZona_Click(object sender, EventArgs e)
        {
            new FormZona().Show();
        }

        
        private void btnDisponibilidad_Click(object sender, EventArgs e)
        {
            new FormDisponibilidad().Show();
        }

        
        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            new FormFacturacion().Show();
        }
        
        private void btnHistorialClientes_Click(object sender, EventArgs e)
        {
            new FormHistorialClientes().Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            new FormDashboard().Show();
        }

    }
}
