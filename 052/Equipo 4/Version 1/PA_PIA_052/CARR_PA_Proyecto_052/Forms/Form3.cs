using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public partial class Form3 : Form
    {
        private string nombreUsuarioInSesion;
        private string rolUsuario;
        public Form3(string nombreUsuario, string rol)
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
                    break;
                case "Administrativo":
                    panelColor.BackColor = Color.Green;
                    mnuContAdmin.Visible = true;
                    mnuContOp.Visible = false;
                    break;
                default:
                    mnuContAdmin.Visible = false;
                    mnuContOp.Visible = false;
                    break;

            }
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
            Form4 ventanaRegistrar = new Form4(nombreUsuarioInSesion);
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void administrativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 ventanaRegistrar = new Form5(nombreUsuarioInSesion);
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 ventanaRegistrar = new Form6();
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void mnuRegCteOp_Click(object sender, EventArgs e)
        {
            Form6 ventanaRegistrar = new Form6();
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void clienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form7 ventanaRegistrar = new Form7();
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 ventanaRegistrar = new Form2(nombreUsuarioInSesion);
            this.Hide();
            ventanaRegistrar.ShowDialog();
            this.Show();
        }

        private void ocupasionYDisponibilidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 ventanaOcYDi = new Form8();
            this.Hide();
            ventanaOcYDi.ShowDialog();
            this.Show();
        }

        private void historialDelClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
