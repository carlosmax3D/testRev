using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Forms;
using CARR_PA_Proyecto_052.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public partial class PaginaPrincipal : Form
    {
        private string nombreUsuarioInSesion;
        private string rolUsuario;
        public PaginaPrincipal(string nombreUsuario, string rol)
        {
            InitializeComponent();
            nombreUsuarioInSesion = nombreUsuario;
            rolUsuario = rol;
        }

        private void Form3_Load(object sender, EventArgs e)
        {


            txtWelcUsu.Text = "Bienvenido " + nombreUsuarioInSesion;

            switch (rolUsuario)
            {
                case "Operativo":
                    panelColor.BackColor = Color.Crimson;
                    mnuContAdmin.Visible = false;
                    mnuContOp.Visible = true;
                    btnVentaOp.Visible = true;
                    break;
                case "Administrativo":
                    panelColor.BackColor = Color.Green;
                    mnuContAdmin.Visible = true;
                    mnuContOp.Visible = false;
                    btnVentaOp.Visible = false;
                    break;
                default:
                    mnuContAdmin.Visible = false;
                    mnuContOp.Visible = false;
                    btnVentaOp.Visible = false;
                    break;

            }

            CargarPartidosDisponibles cargador = new CargarPartidosDisponibles();
            cargador.Ejecutar(dgv_PartidosDisp);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea salir? Esto cerrara su sesion", "Confirmacion", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.No)
            {
                e.Cancel = true;
            }

        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void operativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegOperativos ventanaRegistrar = new RegOperativos(nombreUsuarioInSesion);
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void administrativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegAdmins ventanaRegistrar = new RegAdmins(nombreUsuarioInSesion);
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsClientes ventanaRegistrar = new ConsClientes();
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void mnuRegCteOp_Click(object sender, EventArgs e)
        {
            ConsClientes ventanaRegistrar = new ConsClientes();
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void clienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ActClientes ventanaRegistrar = new ActClientes(nombreUsuarioInSesion);
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegCliente ventanaRegistrar = new RegCliente(nombreUsuarioInSesion);
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void ocupasionYDisponibilidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcupDispo ventanaOcYDi = new OcupDispo();
            this.Hide();
            ventanaOcYDi.ShowDialog();
            this.Show();
        }

        private void historialDelClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistorialCte ventanaHistorialCliente = new HistorialCte();
            this.Hide();
            ventanaHistorialCliente.ShowDialog();
            this.Show();
        }

        private void mnSTSConsOp_Click(object sender, EventArgs e)
        {
            ConsOperativos ventanaConsOp = new ConsOperativos();
            this.Hide();
            ventanaConsOp.ShowDialog();
            this.Show();
        }

        private void mnSTSConsAdmin_Click(object sender, EventArgs e)
        {
            ConsAdmins ventanaConsAdmin = new ConsAdmins();
            this.Hide();
            ventanaConsAdmin.ShowDialog();
            this.Show();
        }

        private void mnSTSActOp_Click(object sender, EventArgs e)
        {
            ActOperativo ventanaActOp = new ActOperativo(nombreUsuarioInSesion);
            this.Hide();
            ventanaActOp.ShowDialog();
            this.Show();
        }

        private void mnSTSActAdmin_Click(object sender, EventArgs e)
        {
            ActAdmin ventanaActAn = new ActAdmin(nombreUsuarioInSesion);
            this.Hide();
            ventanaActAn.ShowDialog();
            this.Show();
        }

        private void mnuOperaRegEst_Click(object sender, EventArgs e)
        {
            RegEstadio ventanaRegEst = new RegEstadio(nombreUsuarioInSesion);
            this.Hide();
            ventanaRegEst.ShowDialog();
            this.Show();
        }

        private void mnuOperaRegZn_Click(object sender, EventArgs e)
        {
            RegZonas ventanaRegZn = new RegZonas();
            this.Hide();
            ventanaRegZn.ShowDialog();
            this.Show();
        }

        private void mnuOperaRegPart_Click(object sender, EventArgs e)
        {
            RegPartido ventanaRegP = new RegPartido();
            this.Hide();
            ventanaRegP.ShowDialog();
            this.Show();
        }

        private void btnVentaOp_Click(object sender, EventArgs e)
        {
            Venta ventanaVenta = new Venta(nombreUsuarioInSesion, rolUsuario);
            this.Hide();
            ventanaVenta.ShowDialog();
            this.Show();
        }
    }
}
