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
using TemplateCSharp.Forms.Menu;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;
using TemplateCSharp.Utilities;


namespace TemplateCSharp.Forms
{
    public partial class FrmLogin : Form
    {
        int intentos = 0;
        private readonly IUsuarioService _usuarioService;
        private readonly IServiceProvider _serviceProvider;

        public FrmLogin(IUsuarioService usuarioService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _usuarioService = usuarioService;
            _serviceProvider = serviceProvider;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (intentos >= 3)
            {
                MessageBox.Show("Demasiados intentos fallidos. Contacte al administrador.");
                btnIngresar.Enabled = false;
                return;
            }



            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Ingrese correo y contraseña.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var usuario = _usuarioService.Autenticar(correo, contrasena);
                if (usuario == null)
                {
                    intentos++;
                    MessageBox.Show("Credenciales incorrectas.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Sesion.IdUsuario = usuario.IdUsuario;
                Sesion.NombreUsuario = usuario.Nombre;
                Sesion.TipoUsuario = usuario.TipoUsuario;

                var menu = _serviceProvider.GetRequiredService<FrmMenu>();
                menu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmLogin_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                intentos = 0; //Reiniciar los intentos
                btnIngresar.Enabled = true; //Volver a habilitar el botón
                txtContrasena.Clear(); //Limpiar la contraseña por seguridad
                txtCorreo.Focus(); //Poner el cursor en el correo
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Si el usuario ya inició sesión y el sistema ocultó esta ventana, no preguntamos nada
            //(Esto es para que al presionar "Ingresar" no te salga el mensaje de "¿Seguro que quieres salir?")
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("¿Seguro que quieres salir de la aplicación?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; //Cancela el cierre, se queda en el login
                }
                else
                {
                    Application.Exit();
                }
            }
        }

    }

}