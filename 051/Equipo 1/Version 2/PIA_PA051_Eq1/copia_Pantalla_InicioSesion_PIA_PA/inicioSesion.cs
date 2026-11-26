
// Importa las clases básicas de C#.
using System;
using System.Windows.Forms;

// Permite utilizar las clases del proyecto indicado.
using Prueba2_PrimerAvance_PAyMAD;

// Permite utilizar las clases del menú operativo.
using Menu_p_Operativo;

// Permite utilizar la clase de acceso a usuarios.
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_InicioSesion_PIA_PA
{
    // Define el formulario de inicio de sesión.
    public partial class pantalla_inicioSesion : Form
    {
        // Constructor del formulario.
        public pantalla_inicioSesion()
        {
            InitializeComponent();

            // Desconecta el evento KeyPress para evitar
            // registrarlo más de una vez.
            txtbox1_inicioSesion.KeyPress -=
                txtbox1_inicioSesion_KeyPress;

            // Conecta el evento KeyPress con su método.
            txtbox1_inicioSesion.KeyPress +=
                txtbox1_inicioSesion_KeyPress;

            // Oculta los caracteres de la contraseña.
            txtbox2_inicioSesion.UseSystemPasswordChar = true;
        }

        // Controla qué caracteres se pueden escribir
        // en el campo de nómina.
        private void txtbox1_inicioSesion_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Permite pasar del campo de nómina al de contraseña
        // al presionar Enter.
        private void txtbox1_inicioSesion_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtbox2_inicioSesion.Focus();
            }
        }

        // Permite iniciar sesión al presionar Enter
        // dentro del campo de contraseña.
        private void txtbox2_inicioSesion_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                boton1_inicioSesion.PerformClick();
            }
        }

        // Evento que se ejecuta cuando el usuario
        // hace clic en el botón de inicio de sesión.
        private void boton1_inicioSesion_Click(
            object sender, EventArgs e)
        {
            IniciarSesion();
        }

        // Método principal para verificar el inicio de sesión.
        private void IniciarSesion()
        {
            // Obtiene la nómina y elimina espacios.
            string nomina =
                txtbox1_inicioSesion.Text.Trim();

            // Obtiene la contraseña escrita.
            string contrasena =
                txtbox2_inicioSesion.Text;

            // Comprueba que la nómina no esté vacía
            // y que contenga solamente números.
            if (string.IsNullOrWhiteSpace(nomina) ||
                !System.Text.RegularExpressions.Regex.IsMatch(
                    nomina, @"^\d+$"))
            {
                MessageBox.Show(
                    "Ingresa una nómina numérica válida.",
                    "Error de inicio de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox1_inicioSesion.Focus();
                txtbox1_inicioSesion.SelectAll();

                return;
            }

            // Comprueba que la contraseña no esté vacía.
            if (string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show(
                    "Ingresa tu contraseña.",
                    "Campo vacío",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox2_inicioSesion.Focus();

                return;
            }

            try
            {
                // Valida las credenciales mediante la clase
                // accesoUsuarios.
                string tipoUsuario =
                    accesoUsuarios.ValidarUsuario(
                        nomina, contrasena);

                // Si el usuario está dado de baja.
                if (tipoUsuario == "BAJA")
                {
                    MessageBox.Show(
                        "No puedes ingresar al sistema.\n\n" +
                        "Tu usuario se encuentra dado de baja.\n\n" +
                        "Comunícate con el administrador del sistema " +
                        "si consideras que se trata de un error.",
                        "Acceso denegado - Usuario dado de baja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox2_inicioSesion.Clear();
                    txtbox1_inicioSesion.Focus();
                    txtbox1_inicioSesion.SelectAll();

                    return;
                }

                // Si las credenciales corresponden a un administrador.
                else if (tipoUsuario == "ADMIN")
                {
                    int nominaNumerica;

                    if (!int.TryParse(
                        nomina, out nominaNumerica))
                    {
                        MessageBox.Show(
                            "La nómina no tiene un formato numérico válido.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    MessageBox.Show(
                        "Bienvenido, administrador.",
                        "Inicio de sesión exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    AbrirMenuAdministrador(nominaNumerica);
                }

                // Si las credenciales corresponden a un operativo.
                else if (tipoUsuario == "OPERATIVO")
                {
                    int nominaNumerica;

                    if (!int.TryParse(
                        nomina, out nominaNumerica))
                    {
                        MessageBox.Show(
                            "La nómina no tiene un formato numérico válido.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    MessageBox.Show(
                        "Bienvenido, operativo.",
                        "Inicio de sesión exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    AbrirMenuOperativo(nominaNumerica);
                }

                // Si no se encontró un usuario válido.
                else
                {
                    MessageBox.Show(
                        "La nómina o la contraseña son incorrectas, " +
                        "o el usuario no está registrado como administrador u operativo.",
                        "Acceso denegado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox2_inicioSesion.Clear();
                    txtbox2_inicioSesion.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible iniciar sesión o conectar con la base de datos.\n\n" +
                    ex.Message,
                    "Error de inicio de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Abre el menú del administrador.
        private void AbrirMenuAdministrador(int nominaUsuario)
        {
            this.Hide();

            try
            {
                using (pantalla_menuAdmin menu =
                    new pantalla_menuAdmin(
                        nominaUsuario, "ADMIN"))
                {
                    // Si se cierra el menú administrador,
                    // se cierra también el inicio de sesión.
                    menu.FormClosed +=
                        (s, args) => this.Close();

                    menu.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.Show();

                MessageBox.Show(
                    "No se pudo abrir el menú de administrador.\n\n" +
                    ex.Message,
                    "Error al abrir el menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Abre el menú del usuario operativo.
        private void AbrirMenuOperativo(int nominaUsuario)
        {
            // Oculta la pantalla original de inicio de sesión.
            this.Hide();

            try
            {
                // Crea el menú operativo y le pasa la nómina.
                menu_operador menu =
                    new menu_operador(nominaUsuario);

                // Cuando se cierre el menú operativo,
                // muestra nuevamente esta misma pantalla.
                menu.FormClosed += (s, args) =>
                {
                    if (!this.IsDisposed)
                    {
                        this.Show();

                        // Limpia los campos para un nuevo inicio de sesión.
                        txtbox1_inicioSesion.Clear();
                        txtbox2_inicioSesion.Clear();

                        // Coloca el cursor en el campo de nómina.
                        txtbox1_inicioSesion.Focus();
                    }
                };

                // Abre el menú operativo como ventana no modal.
                menu.Show(this);
            }
            catch (Exception ex)
            {
                // Si no se puede abrir el menú,
                // vuelve a mostrar el inicio de sesión.
                this.Show();

                MessageBox.Show(
                    "No se pudo abrir el menú operativo.\n\n" +
                    ex.Message,
                    "Error al abrir el menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void pantalla_inicioSesion_Load(
            object sender, EventArgs e)
        {
        }

        // Evento del primer Label.
        private void label1_inicioSesion_Click(
            object sender, EventArgs e)
        {
        }

        // Evento del segundo Label.
        private void label2_inicioSesion_Click(
            object sender, EventArgs e)
        {
        }

        // Evento del tercer Label.
        private void label3_inicioSesion_Click(
            object sender, EventArgs e)
        {
        }

        // Evento que se ejecuta cuando cambia el texto
        // del campo de nómina.
        private void txtbox1_inicioSesion_TextChanged(
            object sender, EventArgs e)
        {
        }

        // Evento que se ejecuta cuando cambia la contraseña.
        private void txtbox2_inicioSesion_TextChanged(
            object sender, EventArgs e)
        {
        }

        // BOTÓN 2: SALIR DEL PROGRAMA.
        private void boton2_salirIniciosesion_Click(
            object sender, EventArgs e)
        {
            // Pregunta si el usuario desea salir.
            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de que deseas salir del programa?",
                "Salir de la aplicación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            // Si selecciona Sí, cierra toda la aplicación.
            if (respuesta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
