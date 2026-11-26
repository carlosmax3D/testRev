using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace Pantalla_GuardarUsuario
{
    public partial class Pantalla_guardarUsuario : Form
    {

        public Pantalla_guardarUsuario(string nomina, string contrasena)
        {
            InitializeComponent();

            label2_guardarUsuario.Text = "Nómina: " + nomina;
            label3_guardarUsuario.Text = "Contraseña: " + contrasena;
        }

        private void label2_guardarUsuario_Click(object sender, EventArgs e)
        {

        }

      
        private void Pantalla_guardarUsuario_Load(object sender, EventArgs e)
        {

        }


        private void label1_guardarUsuario_Click(object sender, EventArgs e)
        {

        }

        private void label3_guardarUsuario_Click(object sender, EventArgs e)
        {

        }

        private void boton1_guardarUsuario_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

