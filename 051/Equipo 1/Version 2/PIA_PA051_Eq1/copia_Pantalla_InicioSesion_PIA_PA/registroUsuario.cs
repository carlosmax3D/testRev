using Pantalla_GuardarUsuario;
using Pantalla_InicioSesion_PIA_PA.conexionBD;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Pantalla4_PrimerAvance_PAyMAD
{
    public partial class pantalla_registroUsuario : Form
    {
        // Evita que el evento TextChanged se ejecute recursivamente
        // mientras se está aplicando el formato de la fecha.
        private bool formateandoFecha = false;

        // Constructor del formulario de registro de usuarios.
        public pantalla_registroUsuario()
        {
            // Inicializa los controles visuales definidos en el diseñador.
            InitializeComponent();

            // Configura la tecla Enter para avanzar entre los campos.
            ConfigurarEnter();

            // Configura los campos de nombres y apellidos.
            ConfigurarCamposNombre();

            // Configura los caracteres permitidos en el correo.
            txtbox5_registroUsuario.KeyPress += Correo_KeyPress;

            // Configura el campo de fecha para aceptar solamente números.
            txtbox6_registroUsuario.KeyPress += Fecha_KeyPress;
        }

        // Evento que se ejecuta cuando cambia el texto de textBox4.
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        // Asigna el evento KeyDown a los seis campos del formulario.
        private void ConfigurarEnter()
        {
            txtbox1_registroUsuario.KeyDown += Campo_KeyDown;
            txtbox2_registroUsuario.KeyDown += Campo_KeyDown;
            txtbox3_registroUsuario.KeyDown += Campo_KeyDown;
            txtbox4_registroUsuario.KeyDown += Campo_KeyDown;
            txtbox5_registroUsuario.KeyDown += Campo_KeyDown;
            txtbox6_registroUsuario.KeyDown += Campo_KeyDown;
        }

        // Configura los campos correspondientes a nombres y apellidos.
        private void ConfigurarCamposNombre()
        {
            TextBox[] campos =
            {
                txtbox1_registroUsuario,
                txtbox2_registroUsuario,
                txtbox3_registroUsuario,
                txtbox4_registroUsuario
            };

            foreach (TextBox campo in campos)
            {
                // Convierte automáticamente las letras a mayúsculas.
                campo.CharacterCasing = CharacterCasing.Upper;

                // Restringe los caracteres permitidos.
                campo.KeyPress += SoloLetras_KeyPress;
            }
        }

        // Controla los caracteres permitidos en nombres y apellidos.
        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Comprueba que una cadena contenga únicamente letras y espacios.
        private bool NombreValido(string texto)
        {
            return texto.All(c =>
                char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        // Permite únicamente caracteres habituales de una dirección de correo.
        private void Correo_KeyPress(object sender, KeyPressEventArgs e)
        {
            string caracteresPermitidos =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@._%+-";

            if (!char.IsControl(e.KeyChar) &&
                !caracteresPermitidos.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Comprueba que el correo tenga una estructura válida.
        private bool CorreoValido(string correo)
        {
            try
            {
                // La expresión regular comprueba la estructura general.
                string patron =
                    @"^[A-Z0-9.!#$%&'*+/=?^_`{|}~-]+@" +
                    @"[A-Z0-9](?:[A-Z0-9-]*[A-Z0-9])?" +
                    @"(?:\.[A-Z0-9](?:[A-Z0-9-]*[A-Z0-9])?)+$";

                if (!Regex.IsMatch(
                    correo,
                    patron,
                    RegexOptions.IgnoreCase))
                {
                    return false;
                }

                // MailAddress realiza una segunda comprobación.
                MailAddress direccion = new MailAddress(correo);

                // Evita aceptar textos que MailAddress haya normalizado.
                return direccion.Address.Equals(
                    correo,
                    StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        // Permite únicamente números en el campo de fecha.
        // Las diagonales se agregan automáticamente mediante TextChanged.
        private void Fecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Controla el formato automático de la fecha dd/MM/aaaa.
        private void txtbox6_registroUsuario_TextChanged(object sender, EventArgs e)
        {
            if (formateandoFecha)
                return;

            TextBox campo = txtbox6_registroUsuario;

            // Cuenta cuántos números había antes del cursor.
            int posicionAnterior = campo.SelectionStart;
            int numerosAntesCursor = campo.Text
                .Take(posicionAnterior)
                .Count(char.IsDigit);

            // Conserva exclusivamente los números.
            string numeros = new string(
                campo.Text.Where(char.IsDigit).ToArray());

            // Limita la entrada a ocho números: ddMMyyyy.
            if (numeros.Length > 8)
                numeros = numeros.Substring(0, 8);

            // Agrega automáticamente las diagonales.
            string fechaFormateada = numeros;

            if (numeros.Length > 2)
            {
                fechaFormateada =
                    numeros.Insert(2, "/");
            }

            if (numeros.Length > 4)
            {
                fechaFormateada =
                    fechaFormateada.Insert(5, "/");
            }

            try
            {
                formateandoFecha = true;

                campo.Text = fechaFormateada;

                // Restaura la posición del cursor considerando las diagonales.
                int nuevaPosicion = 0;
                int numerosContados = 0;

                while (nuevaPosicion < campo.Text.Length &&
                       numerosContados < numerosAntesCursor)
                {
                    if (char.IsDigit(campo.Text[nuevaPosicion]))
                        numerosContados++;

                    nuevaPosicion++;
                }

                // Si el cursor queda justo después del día o del mes,
                // permite que avance después de la diagonal automática.
                if (nuevaPosicion == 2 && campo.Text.Length > 2)
                    nuevaPosicion++;

                if (nuevaPosicion == 5 && campo.Text.Length > 5)
                    nuevaPosicion++;

                campo.SelectionStart = Math.Min(
                    nuevaPosicion,
                    campo.Text.Length);
            }
            finally
            {
                formateandoFecha = false;
            }
        }

        // Comprueba que la fecha tenga exactamente el formato dd/MM/aaaa
        // y que represente una fecha existente.
        private bool FechaValida(string texto, out DateTime fecha)
        {
            return DateTime.TryParseExact(
                texto,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fecha);
        }

        // Controla el movimiento entre campos cuando se presiona Enter.
        private void Campo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (sender == txtbox6_registroUsuario)
                {
                    boton1_resgistroUsuario.PerformClick();
                }
                else
                {
                    SelectNextControl(
                        (Control)sender,
                        true,
                        true,
                        true,
                        true);
                }
            }
        }

        // Evento de cambio de texto de otro TextBox.
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // Evento del DataGridView.
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Evento de cambio de texto del primer campo.
        private void txtbox1_registroUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        // Evento de cambio de texto del segundo campo.
        private void txtbox2_registroUsuario_TextChanged_1(object sender, EventArgs e)
        {

        }

        // Evento del botón para regresar o cerrar el formulario.
        private void boton2_registroUsuario_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Evento de cambio de texto del tercer campo.
        private void txtbox3_registroUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        // Evento del botón de opción para usuarios operativos.
        private void radioboton2_registroUsuario_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento principal del botón para registrar al usuario.
        private void boton1_resgistroUsuario_Click(object sender, EventArgs e)
        {
            // Comprueba que los campos obligatorios no estén vacíos.
            // El segundo nombre es opcional.
            if (string.IsNullOrWhiteSpace(txtbox1_registroUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtbox3_registroUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtbox4_registroUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtbox5_registroUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtbox6_registroUsuario.Text))
            {
                MessageBox.Show(
                    "Completa todos los campos obligatorios. El segundo nombre es opcional.",
                    "Campos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Valida que los nombres y apellidos solo contengan letras y espacios.
            if (!NombreValido(txtbox1_registroUsuario.Text.Trim()) ||
                !NombreValido(txtbox2_registroUsuario.Text.Trim()) ||
                !NombreValido(txtbox3_registroUsuario.Text.Trim()) ||
                !NombreValido(txtbox4_registroUsuario.Text.Trim()))
            {
                MessageBox.Show(
                    "Los nombres y apellidos solo pueden contener letras y espacios.",
                    "Datos incorrectos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Obtiene el correo sin espacios externos.
            string correo = txtbox5_registroUsuario.Text.Trim();

            // Valida la estructura del correo electrónico.
            if (!CorreoValido(correo))
            {
                MessageBox.Show(
                    "Ingresa un correo electrónico válido. Ejemplo: usuario@correo.com.",
                    "Correo inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox5_registroUsuario.Focus();
                return;
            }

            // Valida que la fecha tenga el formato dd/MM/aaaa
            // y que sea una fecha existente.
            if (!FechaValida(
                txtbox6_registroUsuario.Text.Trim(),
                out DateTime fechaNacimiento))
            {
                MessageBox.Show(
                    "Ingresa una fecha válida con el formato dd/MM/aaaa. Ejemplo: 15/08/2005.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox6_registroUsuario.Focus();
                return;
            }

            // Evita registrar una fecha de nacimiento futura.
            if (fechaNacimiento.Date > DateTime.Today)
            {
                MessageBox.Show(
                    "La fecha de nacimiento no puede ser posterior al día de hoy.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox6_registroUsuario.Focus();
                return;
            }

            // Determina el tipo de usuario seleccionado.
            string tipoUsuario;

            if (radioboton1_registroUsuario.Checked)
            {
                tipoUsuario = "ADMINISTRADOR";
            }
            else if (radioboton2_registroUsuario.Checked)
            {
                tipoUsuario = "OPERATIVO";
            }
            else
            {
                MessageBox.Show(
                    "Selecciona Administrador u Operativo.",
                    "Tipo de usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Obtiene los datos escritos en los campos.
            string nombre = txtbox1_registroUsuario.Text.Trim();
            string segundoNombre = txtbox2_registroUsuario.Text.Trim();
            string primerApellido = txtbox3_registroUsuario.Text.Trim();
            string segundoApellido = txtbox4_registroUsuario.Text.Trim();

            // Genera una contraseña aleatoria para el nuevo usuario.
            string contrasena = GenerarContrasena();

            try
            {
                // Llama al método de ConexionBD.
                // La conexión y el procedimiento SQL se ejecutan fuera del formulario.
                int nomina = ConexionBD.RegistrarUsuario(
                    tipoUsuario,
                    nombre,
                    segundoNombre,
                    primerApellido,
                    segundoApellido,
                    correo,
                    fechaNacimiento,
                    contrasena);

                // Informa que el usuario fue registrado correctamente.
                MessageBox.Show(
                    "Usuario registrado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Convierte la nómina a texto con seis posiciones.
                // Los ceros iniciales se muestran aunque la columna sea INT.
                string nominaMostrar = nomina.ToString("D6");

                // Abre la ventana que muestra las credenciales generadas.
                using (Pantalla_guardarUsuario ventana =
                    new Pantalla_guardarUsuario(nominaMostrar, contrasena))
                {
                    ventana.ShowDialog();
                }

                // Limpia los campos después del registro.
                txtbox1_registroUsuario.Clear();
                txtbox2_registroUsuario.Clear();
                txtbox3_registroUsuario.Clear();
                txtbox4_registroUsuario.Clear();
                txtbox5_registroUsuario.Clear();
                txtbox6_registroUsuario.Clear();

                // Quita la selección de los botones de tipo de usuario.
                radioboton1_registroUsuario.Checked = false;
                radioboton2_registroUsuario.Checked = false;
            }
            catch (Exception ex)
            {
                // Captura errores de conexión o del procedimiento almacenado.
                MessageBox.Show(
                    "No se pudo registrar el usuario.\n\n" + ex.Message,
                    "Error de registro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void pantalla_registroUsuario_Load(object sender, EventArgs e)
        {

        }

        // Eventos de clic de las etiquetas.
        private void label1_registroUsuario_Click(object sender, EventArgs e)
        {

        }

        private void label2_registroUsuario_Click(object sender, EventArgs e)
        {

        }

        private void label3_registroUsuario_Click(object sender, EventArgs e)
        {

        }

        private void label4_registroUsuario_Click(object sender, EventArgs e)
        {

        }

        private void label5_registroUsuario_Click(object sender, EventArgs e)
        {

        }

        private void label6_registroUsuario_Click(object sender, EventArgs e)
        {

        }

        private void label7_registroUsuario_Click(object sender, EventArgs e)
        {

        }

        private void label8_registroUsuario_Click(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta al entrar al GroupBox.
        private void grupobox1_registroUsuario_Enter(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando cambia la selección
        // del botón de opción Administrador.
        private void radioboton1_registroUsuario_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Eventos de cambio de texto de los campos.
        private void txtbox4_registroUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbox5_registroUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        // El evento del campo de fecha se encarga de agregar las diagonales.
        // No es necesario agregar otro evento TextChanged en el constructor.

        // Genera una contraseña aleatoria de ocho caracteres.
        private string GenerarContrasena()
        {
            // Conjunto de letras mayúsculas permitidas.
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Conjunto de letras minúsculas permitidas.
            const string minusculas = "abcdefghijklmnopqrstuvwxyz";

            // Conjunto de números permitidos.
            const string numeros = "0123456789";

            // Conjunto de caracteres especiales permitidos.
            const string especiales = "!@#$%&*?";

            // Combina todos los grupos para elegir caracteres.
            string todos = mayusculas + minusculas + numeros + especiales;

            // Reserva un arreglo para los ocho caracteres.
            char[] caracteres = new char[8];

            // Garantiza al menos un carácter de cada tipo.
            caracteres[0] = mayusculas[
                RandomNumberGenerator.GetInt32(mayusculas.Length)];

            caracteres[1] = especiales[
                RandomNumberGenerator.GetInt32(especiales.Length)];

            caracteres[2] = minusculas[
                RandomNumberGenerator.GetInt32(minusculas.Length)];

            caracteres[3] = numeros[
                RandomNumberGenerator.GetInt32(numeros.Length)];

            // Completa los cuatro caracteres restantes.
            for (int i = 4; i < caracteres.Length; i++)
            {
                caracteres[i] = todos[
                    RandomNumberGenerator.GetInt32(todos.Length)];
            }

            // Mezcla los caracteres para que no sigan siempre el mismo orden.
            for (int i = caracteres.Length - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i + 1);

                char temporal = caracteres[i];
                caracteres[i] = caracteres[j];
                caracteres[j] = temporal;
            }

            return new string(caracteres);
        }
    }
}