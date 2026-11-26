using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Utilities;

namespace TemplateCSharp.Forms
{
    public partial class FrmLogin : Form
    {

        private readonly IUsuarioService _usuarioService;
        private readonly IServiceProvider _serviceProvider;

        public FrmLogin(
            IUsuarioService usuarioService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _usuarioService = usuarioService;
            _serviceProvider = serviceProvider;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private async void btonIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string contrasenia = txtPassword.Text;

                Usuario? usuario = await _usuarioService.IniciarSesionAsync(
                    email,
                    contrasenia
                );

                if (usuario is null)
                {
                    return;
                }

                FrmPrincipal form =
                    ActivatorUtilities.CreateInstance<FrmPrincipal>(
                        _serviceProvider,
                        usuario
                    );
                
                form.FormClosing += frm_closing;
                form.Show();
                this.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado al iniciar sesión. Inténtalo nuevamente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }




        private void frm_closing(object sender, EventArgs e) {

            txtEmail.Text = "";
            txtPassword.Text = "";

            this.Show();
        }
    }
}
