
using System;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Pantalla_RegistroCliente_PIA_PA
{
    // Clase que representa la ventana de registro de clientes.
    public partial class pantalla_registroCliente : Form
    {
        // Cadena de conexión utilizada para conectarse a SQL Server.
        // Especifica el servidor, la base de datos y la autenticación integrada.
        private readonly string cadenaConexion =
            @"Data Source=XLMD007\SQLEXPRESS;
              Initial Catalog=PrimerAvance;
              Integrated Security=True;
              TrustServerCertificate=True;";

        // Almacena la nómina del usuario que inició sesión.
        // Es nullable porque puede no existir una sesión recibida.
        private readonly int? nominaUsuario;

        // Indica si el usuario que inició sesión es administrador.
        private readonly bool esAdministrador;

        // Indica si esta ventana recibió los datos de una sesión.
        private readonly bool sesionRecibida;

        // Constructor cuando se recibe la sesión del usuario.
        public pantalla_registroCliente(int nomina, bool administrador)
        {
            InitializeComponent();

            // Guarda los datos de la sesión recibida.
            nominaUsuario = nomina;
            esAdministrador = administrador;
            sesionRecibida = true;

            // Configura los campos y el avance con Enter.
            ConfigurarCampos();
            ConfigurarEnterTextBox();
        }

        // Agrega automáticamente las diagonales en la fecha de nacimiento.
        private void FechaNacimiento_TextChanged(object sender, EventArgs e)
        {
            TextBox campo = txtbox9_registroCliente;
            int posicion = campo.SelectionStart;

            // Cuenta los dígitos que estaban antes del cursor.
            int digitosAntes = 0;
            for (int i = 0; i < Math.Min(posicion, campo.Text.Length); i++)
            {
                if (char.IsDigit(campo.Text[i]))
                    digitosAntes++;
            }

            // Conserva solo los números y permite ddMMyyyy.
            string numeros = Regex.Replace(campo.Text, @"\D", "");
            if (numeros.Length > 8)
                numeros = numeros.Substring(0, 8);

            // Inserta / después del día y del mes.
            string fecha = numeros;
            if (numeros.Length > 2)
                fecha = numeros.Insert(2, "/");
            if (numeros.Length > 4)
                fecha = fecha.Insert(5, "/");

            if (campo.Text != fecha)
            {
                campo.Text = fecha;

                // Restaura la posición del cursor considerando las diagonales.
                int nuevaPosicion = 0;
                int digitosContados = 0;
                while (nuevaPosicion < fecha.Length && digitosContados < digitosAntes)
                {
                    if (char.IsDigit(fecha[nuevaPosicion]))
                        digitosContados++;
                    nuevaPosicion++;
                }

                if (nuevaPosicion < fecha.Length && fecha[nuevaPosicion] == '/')
                    nuevaPosicion++;

                campo.SelectionStart = Math.Min(nuevaPosicion, campo.Text.Length);
            }
        }

        // Constructor sin sesión
        public pantalla_registroCliente()
        {
            // Inicializa los controles visuales de la ventana.
            InitializeComponent();

            // Indica que no se recibió una nómina.
            nominaUsuario = null;

            // Establece que no se recibió el rol de administrador.
            esAdministrador = false;

            // Indica que la ventana se abrió sin información de sesión.
            sesionRecibida = false;

            // Configura los límites y las validaciones de los campos.
            ConfigurarCampos();

            // Configura el comportamiento de la tecla Enter en los TextBox.
            ConfigurarEnterTextBox();
        }

        // Obtener los 14 TextBox en el orden de la pantalla
        private TextBox[] ObtenerCampos()
        {
            // Devuelve un arreglo con los cuadros de texto del formulario.
            // El orden permite recorrerlos y aplicar configuraciones o validaciones.
            return new TextBox[]
            {
                txtbox1_registroCliente,
                txtbox2_registroCliente,
                txtbox3_registroCliente,
                txtbox4_registroCliente,
                txtbox5_registroCliente,
                txtbox6_registroCliente,
                txtbox7_registroCliente,
                txtbox8_registroCliente,
                txtbox9_registroCliente,
                txtbox10_registroCliente,
                txtbox11_registroCliente,
                txtbox12_registroCliente,
                txtbox13_registroCliente,
                txtbox14_registroCliente
            };
        }

        // Configurar límites y validaciones de entrada
        private void ConfigurarCampos()
        {
            // Obtiene todos los TextBox para configurarlos mediante un ciclo.
            TextBox[] campos = ObtenerCampos();

            // Recorre cada cuadro de texto del formulario.
            foreach (TextBox campo in campos)
            {
                // El correo conserva las mayúsculas/minúsculas que escriba el usuario.
                if (campo != txtbox6_registroCliente)
                {
                    // Convierte automáticamente los caracteres ingresados a mayúsculas.
                    campo.CharacterCasing = CharacterCasing.Upper;
                }

                // Desvincula el evento TextChanged para evitar asociarlo varias veces.
                campo.TextChanged -= Campo_TextChanged;

                // Asocia el evento que controla el uso de mayúsculas.
                campo.TextChanged += Campo_TextChanged;

                // Desvincula el evento KeyPress para evitar duplicaciones.
                campo.KeyPress -= Campo_KeyPress;

                // Asocia el evento que valida los caracteres que se pueden escribir.
                campo.KeyPress += Campo_KeyPress;
            }

            // RFC
            // Limita la longitud del RFC a 13 caracteres.
            txtbox1_registroCliente.MaxLength = 13;

            // Nombres y apellidos
            // Establece una longitud máxima de 100 caracteres para cada campo.
            txtbox2_registroCliente.MaxLength = 100;
            txtbox3_registroCliente.MaxLength = 100;
            txtbox4_registroCliente.MaxLength = 100;
            txtbox5_registroCliente.MaxLength = 100;

            // Correo
            // Permite un máximo de 150 caracteres para el correo electrónico.
            txtbox6_registroCliente.MaxLength = 150;

            // Teléfonos
            // Limita cada campo telefónico a 10 caracteres.
            txtbox7_registroCliente.MaxLength = 10;
            txtbox8_registroCliente.MaxLength = 10;

            // Fecha
            // Limita la fecha de nacimiento a 10 caracteres.
            txtbox9_registroCliente.MaxLength = 10;
            // Activa la inserción automática de diagonales en la fecha.
            txtbox9_registroCliente.TextChanged -= FechaNacimiento_TextChanged;
            txtbox9_registroCliente.TextChanged += FechaNacimiento_TextChanged;
            // Dirección
            // Define la longitud máxima de los campos de dirección.
            txtbox10_registroCliente.MaxLength = 100;
            txtbox11_registroCliente.MaxLength = 150;
            txtbox12_registroCliente.MaxLength = 100;
            txtbox13_registroCliente.MaxLength = 100;
            txtbox14_registroCliente.MaxLength = 10;
        }

        // Mantener los campos en mayúsculas, excepto el correo
        private void Campo_TextChanged(object sender, EventArgs e)
        {
            // Intenta obtener el TextBox que generó el evento.
            TextBox campo = sender as TextBox;

            // Si no se obtuvo un TextBox o se trata del correo,
            // no modifica el contenido y termina el método.
            if (campo == null ||
                campo == txtbox6_registroCliente)
            {
                return;
            }

            // Guarda la posición actual del cursor para restaurarla después.
            int posicion = campo.SelectionStart;

            // Convierte el contenido del campo a mayúsculas.
            string textoMayusculas = campo.Text.ToUpperInvariant();

            // Comprueba si el texto necesita convertirse.
            if (campo.Text != textoMayusculas)
            {
                // Reemplaza el contenido con su versión en mayúsculas.
                campo.Text = textoMayusculas;

                // Restaura la posición del cursor para facilitar la escritura.
                // Math.Min evita colocar el cursor fuera del texto existente.
                campo.SelectionStart = Math.Min(
                    posicion,
                    campo.Text.Length
                );
            }
        }

        // Validar los caracteres permitidos en cada campo
        private void Campo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite teclas de control como Retroceso.
            if (char.IsControl(e.KeyChar))
                return;

            // Obtiene el TextBox en el que se está escribiendo.
            TextBox campo = sender as TextBox;

            // Si el control no es un TextBox, no continúa con la validación.
            if (campo == null)
                return;

            // Obtiene el nombre del control para identificar el campo.
            string nombre = campo.Name;

            // Obtiene el carácter que el usuario intenta ingresar.
            char caracter = e.KeyChar;

            // Comprueba si se está escribiendo en el campo del RFC.
            if (nombre == "txtbox1_registroCliente")
            {
                // RFC: letras, números y &
                // Rechaza los caracteres que no sean letras, números o el símbolo &.
                if (!char.IsLetterOrDigit(caracter) &&
                    caracter != '&')
                {
                    // Cancela la entrada del carácter no permitido.
                    e.Handled = true;
                }
            }
            else if (
                nombre == "txtbox2_registroCliente" ||
                nombre == "txtbox3_registroCliente" ||
                nombre == "txtbox4_registroCliente" ||
                nombre == "txtbox5_registroCliente")
            {
                // Nombres y apellidos
                // Permite letras, espacios, guiones y apóstrofes.
                if (!char.IsLetter(caracter) &&
                    caracter != ' ' &&
                    caracter != '-' &&
                    caracter != '\'')
                {
                    // Impide escribir caracteres no permitidos en los nombres.
                    e.Handled = true;
                }
            }
            else if (nombre == "txtbox6_registroCliente")
            {
                // Correo: letras, números y caracteres habituales de email
                // Define los símbolos adicionales que se permiten en el correo.
                string caracteresCorreo = "!#$%&'*+-/=?^_`{|}~.@";

                // Comprueba si el carácter es una letra, un número
                // o uno de los símbolos permitidos para el correo.
                if (!char.IsLetterOrDigit(caracter) &&
                    !caracteresCorreo.Contains(caracter.ToString()))
                {
                    // Rechaza el carácter si no está permitido.
                    e.Handled = true;
                }
            }
            else if (
                nombre == "txtbox7_registroCliente" ||
                nombre == "txtbox8_registroCliente" ||
                nombre == "txtbox14_registroCliente")
            {
                // Teléfonos y número de vivienda
                // Solo permite ingresar dígitos.
                if (!char.IsDigit(caracter))
                    e.Handled = true;
            }
            else if (nombre == "txtbox9_registroCliente")
            {
                // Fecha de nacimiento
                // Permite dígitos y los separadores / y -.
                if (!char.IsDigit(caracter) &&
                    caracter != '/' &&
                    caracter != '-')
                {
                    // Rechaza otros caracteres en la fecha.
                    e.Handled = true;
                }
            }
            else
            {
                // Campos de dirección
                // Permite letras, números, espacios y algunos signos habituales.
                if (!char.IsLetterOrDigit(caracter) &&
                    !" #.,/-'".Contains(caracter.ToString()))
                {
                    // Impide escribir caracteres que no estén permitidos.
                    e.Handled = true;
                }
            }
        }

        // Permitir pasar al siguiente TextBox con Enter
        private void ConfigurarEnterTextBox()
        {
            // Recorre todos los campos de texto de la ventana.
            foreach (TextBox campo in ObtenerCampos())
            {
                // Elimina una asociación anterior del evento KeyDown.
                campo.KeyDown -= Campo_KeyDown;

                // Asocia el evento que permite avanzar con Enter.
                campo.KeyDown += Campo_KeyDown;
            }
        }

        // Controla las teclas presionadas en los cuadros de texto.
        private void Campo_KeyDown(object sender, KeyEventArgs e)
        {
            // Comprueba si la tecla presionada es Enter.
            if (e.KeyCode == Keys.Enter)
            {
                // Evita que Enter produzca su comportamiento predeterminado.
                e.SuppressKeyPress = true;

                // Mueve el foco al siguiente control disponible.
                // Los parámetros indican la dirección de avance
                // y permiten recorrer los controles del formulario.
                SelectNextControl(
                    (Control)sender,
                    true,
                    true,
                    true,
                    true
                );
            }
        }

        // Validar los campos antes de guardar
        private bool ValidarCampos(out DateTime fechaNacimiento)
        {
            // Inicializa la variable de salida con una fecha predeterminada.
            // Si la validación tiene éxito, se reemplaza por la fecha ingresada.
            fechaNacimiento = DateTime.MinValue;

            // Obtiene todos los cuadros de texto del formulario.
            TextBox[] campos = ObtenerCampos();

            // Recorre los campos para comprobar que los obligatorios tengan información.
            foreach (TextBox campo in campos)
            {
                // Segundo nombre y teléfono de casa son opcionales.
                if (campo == txtbox3_registroCliente ||
                    campo == txtbox7_registroCliente)
                {
                    // Omite la comprobación de estos campos opcionales.
                    continue;
                }

                // Comprueba si el campo está vacío o contiene solo espacios.
                if (string.IsNullOrWhiteSpace(campo.Text))
                {
                    // Informa que falta completar un campo obligatorio.
                    MessageBox.Show(
                        "Completa todos los campos obligatorios antes de guardar.",
                        "Campos incompletos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    // Coloca el cursor en el campo que necesita completarse.
                    campo.Focus();

                    // Indica que la validación falló.
                    return false;
                }
            }

            // Validar RFC
            // Obtiene el RFC y elimina los espacios de sus extremos.
            string rfc = txtbox1_registroCliente.Text.Trim();

            // Comprueba que el RFC cumpla con uno de los formatos definidos.
            // Acepta las estructuras de RFC para personas físicas y morales.
            if (!Regex.IsMatch(
                rfc,
                @"^(?:[A-ZÑ&]{3}\d{6}[A-Z0-9]{3}|[A-ZÑ&]{4}\d{6}[A-Z0-9]{3})$"))
            {
                // Informa que el RFC no cumple con el formato esperado.
                MessageBox.Show(
                    "El RFC no tiene un formato válido.",
                    "RFC inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Coloca el cursor en el campo RFC.
                txtbox1_registroCliente.Focus();

                // Detiene la validación.
                return false;
            }

            // Validar nombres y apellidos
            // Agrupa los campos que contienen nombres y apellidos.
            TextBox[] nombres =
            {
                txtbox2_registroCliente,
                txtbox3_registroCliente,
                txtbox4_registroCliente,
                txtbox5_registroCliente
            };

            // Revisa cada nombre y apellido.
            foreach (TextBox campo in nombres)
            {
                // El segundo nombre puede quedar vacío.
                if (campo == txtbox3_registroCliente &&
                    string.IsNullOrWhiteSpace(campo.Text))
                {
                    // Omite la validación si no se ingresó un segundo nombre.
                    continue;
                }

                // Comprueba que el texto contenga letras y permita
                // espacios, guiones o apóstrofes entre las palabras.
                if (!Regex.IsMatch(
                    campo.Text.Trim(),
                    @"^[\p{L}]+(?:[ '-][\p{L}]+)*$"))
                {
                    // Informa que el nombre o apellido contiene datos no permitidos.
                    MessageBox.Show(
                        "Revisa los nombres y apellidos. " +
                        "Solo se permiten letras, espacios, guiones y apóstrofes.",
                        "Dato inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    // Coloca el cursor en el campo que presenta el error.
                    campo.Focus();

                    // Detiene la validación.
                    return false;
                }
            }

            // Validar correo electrónico
            // Obtiene el correo ingresado y elimina espacios exteriores.
            string correo = txtbox6_registroCliente.Text.Trim();

            try
            {
                // Intenta crear una dirección de correo con el texto ingresado.
                // MailAddress comprueba que tenga una estructura reconocible.
                MailAddress direccion = new MailAddress(correo);

                // Comprueba que la dirección no haya sido interpretada
                // como una dirección distinta, que contenga @ y que no termine en punto.
                if (direccion.Address != correo ||
                    !correo.Contains("@") ||
                    correo.EndsWith("."))
                {
                    // Genera una excepción si alguna comprobación falla.
                    throw new FormatException();
                }
            }
            catch
            {
                // Informa que el correo electrónico no tiene un formato válido.
                MessageBox.Show(
                    "Escribe un correo electrónico válido.",
                    "Correo inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Coloca el cursor en el campo de correo.
                txtbox6_registroCliente.Focus();

                // Detiene la validación.
                return false;
            }

            // Validar teléfonos:
            // Teléfono de casa opcional; celular obligatorio.
            // Obtiene los números ingresados en ambos campos.
            string telefonoCasa =
                txtbox7_registroCliente.Text.Trim();

            string celular =
                txtbox8_registroCliente.Text.Trim();

            // Si se ingresó teléfono de casa, exige 10 dígitos.
            // También exige que el celular tenga exactamente 10 dígitos.
            if ((!string.IsNullOrWhiteSpace(telefonoCasa) &&
                 !Regex.IsMatch(telefonoCasa, @"^\d{10}$")) ||
                !Regex.IsMatch(celular, @"^\d{10}$"))
            {
                // Informa los requisitos de longitud de los teléfonos.
                MessageBox.Show(
                    "El celular debe tener 10 dígitos. " +
                    "Si capturas el teléfono de casa, también debe tener 10 dígitos.",
                    "Teléfono inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Si el teléfono de casa es incorrecto, enfoca ese campo.
                if (!string.IsNullOrWhiteSpace(telefonoCasa) &&
                    !Regex.IsMatch(telefonoCasa, @"^\d{10}$"))
                {
                    txtbox7_registroCliente.Focus();
                }
                else
                {
                    // De lo contrario, enfoca el campo del celular.
                    txtbox8_registroCliente.Focus();
                }

                // Detiene la validación porque un teléfono no es válido.
                return false;
            }

            // Validar fecha de nacimiento
            // Define los formatos de fecha que se aceptan.
            string[] formatosFecha =
            {
                "dd/MM/yyyy",
                "d/M/yyyy",
                "yyyy-MM-dd"
            };

            // Intenta convertir el texto a una fecha exacta
            // utilizando alguno de los formatos permitidos.
            bool fechaValida = DateTime.TryParseExact(
                txtbox9_registroCliente.Text.Trim(),
                formatosFecha,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fechaNacimiento
            );

            // Comprueba si el texto pudo convertirse correctamente.
            if (!fechaValida)
            {
                // Informa el formato de fecha que se espera.
                MessageBox.Show(
                    "Escribe la fecha como dd/MM/aaaa.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Coloca el cursor en el campo de fecha.
                txtbox9_registroCliente.Focus();

                // Detiene la validación.
                return false;
            }

            // Comprueba que la fecha no sea futura
            // y que el año no sea anterior a 1900.
            if (fechaNacimiento > DateTime.Today ||
                fechaNacimiento.Year < 1900)
            {
                // Informa que la fecha de nacimiento no es válida.
                MessageBox.Show(
                    "La fecha de nacimiento no es válida.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Coloca el cursor en el campo de fecha.
                txtbox9_registroCliente.Focus();

                // Detiene la validación.
                return false;
            }

            // Validar dirección
            // Agrupa los campos que contienen la dirección del cliente.
            TextBox[] direcciones =
            {
                txtbox10_registroCliente,
                txtbox11_registroCliente,
                txtbox12_registroCliente,
                txtbox13_registroCliente
            };

            // Revisa el contenido de cada campo de dirección.
            foreach (TextBox campo in direcciones)
            {
                // Permite letras, números, espacios y los signos
                // especificados en la expresión regular.
                if (!Regex.IsMatch(
                    campo.Text.Trim(),
                    @"^[\p{L}\p{N} #.,/'-]+$"))
                {
                    // Informa que los datos de la dirección no son válidos.
                    MessageBox.Show(
                        "Revisa los datos de la dirección.",
                        "Dirección inválida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    // Coloca el cursor en el campo incorrecto.
                    campo.Focus();

                    // Detiene la validación.
                    return false;
                }
            }

            // Validar número de vivienda
            // Exige que el número de vivienda contenga entre 1 y 10 dígitos.
            if (!Regex.IsMatch(
                txtbox14_registroCliente.Text.Trim(),
                @"^\d{1,10}$"))
            {
                // Informa que el número de vivienda no cumple el formato.
                MessageBox.Show(
                    "El número de vivienda debe contener entre 1 y 10 dígitos.",
                    "Número inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Coloca el cursor en el campo del número de vivienda.
                txtbox14_registroCliente.Focus();

                // Detiene la validación.
                return false;
            }

            // Si todas las comprobaciones se cumplen, indica que los datos son válidos.
            return true;
        }

        // BOTÓN 1: REGRESAR
        private void boton1_registroCliente_Click(
            object sender,
            EventArgs e)
        {
            // Cierra la ventana de registro de clientes.
            Close();
        }

        // BOTÓN 2: GUARDAR
        private void boton2_registroCliente_Click(
            object sender,
            EventArgs e)
        {
            // Comprueba que la ventana haya recibido una sesión
            // y que exista una nómina asociada al usuario.
            if (!sesionRecibida || !nominaUsuario.HasValue)
            {
                // Informa que no se identificó la sesión del usuario.
                MessageBox.Show(
                    "No se recibió la nómina del usuario que inició sesión. " +
                    "Regresa al menú e ingresa nuevamente desde tu sesión.",
                    "Sesión no identificada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Detiene el guardado porque falta la información de sesión.
                return;
            }

            // Variable que recibirá la fecha de nacimiento validada.
            DateTime fechaNacimiento;

            // Valida todos los campos.
            // Si alguno no cumple los requisitos, termina el evento.
            if (!ValidarCampos(out fechaNacimiento))
                return;

            // Define la consulta SQL para insertar un nuevo cliente.
            // Los parámetros evitan concatenar directamente los datos del usuario
            // dentro de la consulta SQL.
            string consulta = @"
                INSERT INTO dbo.CLIENTES
                (
                    RFC_Cliente,
                    Primer_Nombre,
                    Segundo_Nombre,
                    Primer_Apellido,
                    Segundo_Apellido,
                    Correoelectronico,
                    Tel_casa,
                    Celular,
                    Fecha_nacimiento,
                    Calle,
                    Colonia,
                    Ciudad,
                    Numero_Vivienda,
                    Municipio,
                    Nomina_Ope,
                    Nomina_Admin
                )
                VALUES
                (
                    @RFC,
                    @PrimerNombre,
                    @SegundoNombre,
                    @PrimerApellido,
                    @SegundoApellido,
                    @Correo,
                    @TelCasa,
                    @Celular,
                    @FechaNacimiento,
                    @Calle,
                    @Colonia,
                    @Ciudad,
                    @NumeroVivienda,
                    @Municipio,
                    @NominaOpe,
                    @NominaAdmin
                );";

            try
            {
                // Crea la conexión con SQL Server y el comando SQL.
                // using permite liberar ambos recursos al terminar.
                using (SqlConnection conexion =
                    new SqlConnection(cadenaConexion))
                using (SqlCommand comando =
                    new SqlCommand(consulta, conexion))
                {
                    // Asigna el RFC al parámetro de la consulta.
                    comando.Parameters.Add(
                        "@RFC", SqlDbType.VarChar, 13
                    ).Value = txtbox1_registroCliente.Text.Trim();

                    // Asigna el primer nombre del cliente.
                    comando.Parameters.Add(
                        "@PrimerNombre", SqlDbType.VarChar, 100
                    ).Value = txtbox2_registroCliente.Text.Trim();

                    // Segundo nombre: guardar NULL si está vacío.
                    // DBNull.Value representa un valor SQL NULL.
                    comando.Parameters.Add(
                        "@SegundoNombre", SqlDbType.VarChar, 100
                    ).Value = string.IsNullOrWhiteSpace(
                        txtbox3_registroCliente.Text)
                            ? (object)DBNull.Value
                            : txtbox3_registroCliente.Text.Trim();

                    // Asigna el primer apellido del cliente.
                    comando.Parameters.Add(
                        "@PrimerApellido", SqlDbType.VarChar, 100
                    ).Value = txtbox4_registroCliente.Text.Trim();

                    // Asigna el segundo apellido del cliente.
                    comando.Parameters.Add(
                        "@SegundoApellido", SqlDbType.VarChar, 100
                    ).Value = txtbox5_registroCliente.Text.Trim();

                    // Asigna el correo electrónico del cliente.
                    comando.Parameters.Add(
                        "@Correo", SqlDbType.VarChar, 150
                    ).Value = txtbox6_registroCliente.Text.Trim();

                    // Teléfono de casa: guardar NULL si está vacío.
                    // Si el campo está vacío, envía SQL NULL;
                    // de lo contrario, envía el teléfono capturado.
                    comando.Parameters.Add(
                        "@TelCasa", SqlDbType.VarChar, 10
                    ).Value = string.IsNullOrWhiteSpace(
                        txtbox7_registroCliente.Text)
                            ? (object)DBNull.Value
                            : txtbox7_registroCliente.Text.Trim();

                    // Asigna el número de celular.
                    comando.Parameters.Add(
                        "@Celular", SqlDbType.VarChar, 10
                    ).Value = txtbox8_registroCliente.Text.Trim();

                    // Asigna la fecha de nacimiento como tipo DATE.
                    // .Date conserva la fecha y elimina la parte de hora.
                    comando.Parameters.Add(
                        "@FechaNacimiento", SqlDbType.Date
                    ).Value = fechaNacimiento.Date;

                    // Según el orden de campos de tu pantalla:
                    // txtbox10 = Municipio
                    // txtbox11 = Calle
                    // txtbox12 = Colonia
                    // txtbox13 = Ciudad
                    // txtbox14 = Número de vivienda

                    // Asigna la calle de la dirección.
                    comando.Parameters.Add(
                        "@Calle", SqlDbType.VarChar, 150
                    ).Value = txtbox11_registroCliente.Text.Trim();

                    // Asigna la colonia del cliente.
                    comando.Parameters.Add(
                        "@Colonia", SqlDbType.VarChar, 100
                    ).Value = txtbox12_registroCliente.Text.Trim();

                    // Asigna la ciudad del cliente.
                    comando.Parameters.Add(
                        "@Ciudad", SqlDbType.VarChar, 100
                    ).Value = txtbox13_registroCliente.Text.Trim();

                    // Asigna el número de vivienda.
                    comando.Parameters.Add(
                        "@NumeroVivienda", SqlDbType.VarChar, 10
                    ).Value = txtbox14_registroCliente.Text.Trim();

                    // Asigna el municipio del cliente.
                    comando.Parameters.Add(
                        "@Municipio", SqlDbType.VarChar, 100
                    ).Value = txtbox10_registroCliente.Text.Trim();

                    // Si el usuario es administrador, guarda NULL en Nomina_Ope.
                    // En caso contrario, registra la nómina del operador.
                    comando.Parameters.Add(
                        "@NominaOpe", SqlDbType.Int
                    ).Value = esAdministrador
                        ? (object)DBNull.Value
                        : nominaUsuario.Value;

                    // Si el usuario es administrador, guarda su nómina en Nomina_Admin.
                    // Si es operador, guarda NULL en ese campo.
                    comando.Parameters.Add(
                        "@NominaAdmin", SqlDbType.Int
                    ).Value = esAdministrador
                        ? (object)nominaUsuario.Value
                        : DBNull.Value;

                    // Abre la conexión con SQL Server.
                    conexion.Open();

                    // Ejecuta la consulta INSERT y obtiene
                    // la cantidad de filas afectadas.
                    int filasAfectadas = comando.ExecuteNonQuery();

                    // Comprueba si se insertó al menos un registro.
                    if (filasAfectadas > 0)
                    {
                        // Informa que el cliente se guardó correctamente.
                        MessageBox.Show(
                            "El cliente se registró correctamente.",
                            "Registro exitoso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        // Limpia todos los cuadros de texto después del registro.
                        foreach (TextBox campo in ObtenerCampos())
                        {
                            campo.Clear();
                        }

                        // Coloca el cursor en el campo del RFC
                        // para facilitar el registro de otro cliente.
                        txtbox1_registroCliente.Focus();
                    }
                    else
                    {
                        // Informa que no se insertó ningún registro.
                        MessageBox.Show(
                            "No se pudo registrar el cliente.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                // Captura errores específicos generados por SQL Server.

                // Los errores 2627 y 2601 suelen indicar
                // que se intentó insertar un valor duplicado.
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Informa que ya existe un cliente con ese RFC.
                    MessageBox.Show(
                        "Ya existe un cliente con ese RFC.",
                        "RFC duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    // Coloca el cursor en el campo del RFC.
                    txtbox1_registroCliente.Focus();
                }
                // El error 547 suele indicar un conflicto de clave foránea.
                else if (ex.Number == 547)
                {
                    // Informa que la nómina no coincide con un registro relacionado.
                    MessageBox.Show(
                        "No se pudo registrar el cliente porque la nómina " +
                        "no existe en la tabla de operadores o administradores. " +
                        "Verifica la sesión iniciada.",
                        "Error de relación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                // El error 515 indica que se intentó guardar NULL
                // en una columna que no lo permite.
                /*else if (ex.Number == 515)
                {
                    // Informa qué campos opcionales podrían estar
                    // definidos como obligatorios en la base de datos.
                    MessageBox.Show(
                        "No se pudo guardar porque una columna de la base de datos " +
                        "no permite valores NULL. Revisa que Segundo_Nombre y " +
                        "Tel_casa permitan NULL.",
                        "Campo obligatorio en SQL Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }*/
                else
                {
                    // Muestra el detalle de cualquier otro error de SQL Server.
                    MessageBox.Show(
                        "Error de SQL al guardar el cliente:\n" + ex.Message,
                        "Error de base de datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                // Captura otros errores que no sean específicamente de SQL Server.
                MessageBox.Show(
                    "Ocurrió un error al guardar el cliente:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // EVENTOS DEL DISEÑADOR
        // Se conservan para que el Designer.cs encuentre todos los métodos.

        // Evento que se ejecuta cuando se carga el formulario.
        private void pantalla_registroCliente_Load(
            object sender, EventArgs e)
        {
            // Actualmente no contiene instrucciones adicionales.
        }

        // Eventos TextChanged asociados a los cuadros de texto.
        // Se mantienen para conservar las asociaciones del diseñador.
        private void txtbox1_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox2_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox3_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox4_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox5_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox6_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox7_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox8_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox9_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox10_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox11_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox12_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox13_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtbox14_registroCliente_TextChanged(
            object sender, EventArgs e)
        { }

        // Eventos Click de las etiquetas del formulario.
        // Actualmente no ejecutan ninguna acción.
        private void label1_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label2_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label3_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label4_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label5_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label6_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label7_registroCliente_Click_1(
            object sender, EventArgs e)
        { }

        private void label8_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label9_registroCliente_Click_1(
            object sender, EventArgs e)
        { }

        private void label10_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label11_registroCliente_Click_1(
            object sender, EventArgs e)
        { }

        private void label12_registroCliente_Click_1(
            object sender, EventArgs e)
        { }

        private void label13_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label14_registroCliente_Click(
            object sender, EventArgs e)
        { }

        private void label15_registroCliente_Click(
            object sender, EventArgs e)
        { }
    }
}
