using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Repositorios;
using System.Text.RegularExpressions;

namespace CARR_PA_Proyecto_052
{
    public partial class IniciarSesion : Form
    {
        public IniciarSesion()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirmInSes_Click(object sender, EventArgs e)
        {
            string correoUsuario = cajatxtCorreo.Text;
            string contUsuario = cajatxtContras.Text;

            LogicaUsuario logica = new LogicaUsuario();

            if (string.IsNullOrWhiteSpace(correoUsuario) && string.IsNullOrWhiteSpace(contUsuario))
            {
                MessageBox.Show("Debe introducir su informacion", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(correoUsuario))
            {
                MessageBox.Show("Debe introducir un correo", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(contUsuario))
            {
                MessageBox.Show("Debe introducir una contraseña", "Error");
                return;
            }

            if (logica.ValidarCredenciales(correoUsuario, contUsuario))
            {
                using (var db = new InventarioContext()) 
                {
                    var checarNombre = db.Usuarios
                            .Where(p => p.Correo == correoUsuario)
                            .FirstOrDefault();

                    if (checarNombre != null)
                    {
                        PaginaPrincipal ventanaPrincipal = new PaginaPrincipal(checarNombre.Nombre, checarNombre.Rol);
                        this.Hide();
                        ventanaPrincipal.ShowDialog();
                        cajatxtCorreo.Clear();
                        cajatxtContras.Clear();
                        this.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrecta", "Error");
            }
        }
    }
}
