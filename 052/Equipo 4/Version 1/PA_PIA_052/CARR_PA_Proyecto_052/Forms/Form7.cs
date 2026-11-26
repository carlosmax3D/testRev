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
    public partial class Form7 : Form
    {
        private void ValidarCamposRegistro()
        {
            bool camposCompletos = !string.IsNullOrWhiteSpace(cajatxtIDNum.Text);
            btnEditar.Enabled = camposCompletos;
        }
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            /*CargarDatosClientes();*/

            btnEditar.Enabled = false;
            cBoxColumnClintes.Visible = false;
            cBoxColumnClintes.Enabled = false;
            txtQueEditar.Visible = false;

            cBoxColumnClintes.Items.Add("Nombre");
            cBoxColumnClintes.Items.Add("Domicilio");
            cBoxColumnClintes.Items.Add("RFC");
            cBoxColumnClintes.Items.Add("Correo");
            cBoxColumnClintes.Items.Add("FechaNac");

            cajatxtIDNum.TextChanged += (s, ev) => ValidarCamposRegistro();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            txtQueEditar.Visible = true;
            cBoxColumnClintes.Visible = true;
            cBoxColumnClintes.Enabled = true;
        }
    }
}
