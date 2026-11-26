using System;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    public partial class LoginForm : Form
    {
        readonly UsuarioRepositorio usuarios = new UsuarioRepositorio();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            using (RegistroUsuarioForm f = new RegistroUsuarioForm())
            {
                f.ShowDialog(this);
            }
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !chkMostrar.Checked;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            if (correo == "" || txtPass.Text == "")
            {
                Tema.Info("Escribe tu correo y contraseña.", "Login");
                return;
            }
            string errorCorreo = Validacion.Correo(correo);
            if (errorCorreo != null)
            {
                Tema.Info(errorCorreo, "Login");
                txtCorreo.Focus();
                return;
            }

            try
            {
                Usuario u = usuarios.Login(correo, txtPass.Text);
                if (u == null)
                {
                    Tema.Aviso("Correo o contraseña incorrectos.", "Login");
                    txtPass.Clear();
                    txtPass.Focus();
                    return;
                }
                Sesion.IdUsuario = u.Id;
                Sesion.Correo = u.Correo;
                Sesion.Nombre = u.NombreCompleto;
                Sesion.Rol = u.Rol;
            }
            catch (Exception ex)
            {
                Tema.ErrorConexion(ex);
                return;
            }

            MenuForm menu = new MenuForm();
            menu.FormClosed += (s, ev) =>
            {
                Sesion.Limpiar();
                if (menu.CerrarSesion) { Show(); txtCorreo.Focus(); }
                else Application.Exit();
            };
            txtPass.Clear();
            chkMostrar.Checked = false;
            Hide();
            menu.Show();
        }
    }
}
