
// Importa funcionalidades básicas de C#.
using System;

// Permite utilizar tipos de datos para SQL, como SqlDbType,
// y estructuras como DataTable.
using System.Data;

// Permite usar funciones de LINQ, como All(),
// para comprobar si todos los caracteres cumplen una condición.
using System.Linq;

// Permite validar textos mediante expresiones regulares.
using System.Text.RegularExpressions;

// Permite trabajar con formatos de fecha específicos.
using System.Globalization;

// Permite construir cadenas de texto.
using System.Text;

// Permite crear ventanas, botones, cuadros de texto y mensajes.
using System.Windows.Forms;

// Permite conectar la aplicación con SQL Server
// y ejecutar comandos SQL.
using Microsoft.Data.SqlClient;

// Define el espacio de nombres al que pertenece esta clase.
namespace Menu_p_Operativo
{
    // Define la ventana para registrar un nuevo cliente.
    public partial class dclientes_eligio_registrar : Form
    {
        // Cadena de conexión con SQL Server.
        private readonly string cadenaConexion =
            @"Data Source=XLMD007\SQLEXPRESS;
              Initial Catalog=PrimerAvance;
              Integrated Security=True;
              TrustServerCertificate=True;";

        // Guarda la nómina del operador que registra al cliente.
        public int NominaOperador { get; private set; }

        // Evita que el evento TextChanged se ejecute repetidamente
        // mientras se modifica automáticamente el formato de la fecha.
        private bool actualizandoFecha = false;

        // Constructor sin parámetros.
        public dclientes_eligio_registrar()
        {
            // Inicializa los controles creados en el diseñador.
            InitializeComponent();

            // Asigna cero como valor inicial de la nómina.
            NominaOperador = 0;

            // Configura el campo de fecha de nacimiento.
            ConfigurarFechaNacimiento();
        }

        // Constructor que recibe la nómina del operador.
        public dclientes_eligio_registrar(int nominaUsuario)
        {
            // Inicializa los controles de la ventana.
            InitializeComponent();

            // Guarda la nómina recibida como parámetro.
            NominaOperador = nominaUsuario;

            // Configura el campo de fecha de nacimiento.
            ConfigurarFechaNacimiento();
        }

        // CONFIGURACIÓN DEL CAMPO DE FECHA DE NACIMIENTO.
        private void ConfigurarFechaNacimiento()
        {
            // Establece el máximo de caracteres: dd/MM/aaaa.
            fecha_naci_client.MaxLength = 10;

            // Evita registrar varias veces el evento KeyPress.
            fecha_naci_client.KeyPress -=
                fecha_naci_client_KeyPress;

            fecha_naci_client.KeyPress +=
                fecha_naci_client_KeyPress;

            // Evita registrar varias veces el evento TextChanged.
            fecha_naci_client.TextChanged -=
                fecha_naci_client_TextChanged;

            fecha_naci_client.TextChanged +=
                fecha_naci_client_TextChanged;
        }

        // EVENTO PARA CONTROLAR LOS CARACTERES DE LA FECHA.
        private void fecha_naci_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            // Permite únicamente números y teclas de control.
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // EVENTO QUE AGREGA AUTOMÁTICAMENTE LAS DIAGONALES.
        private void fecha_naci_client_TextChanged(
            object sender, EventArgs e)
        {
            // Evita que el evento se ejecute durante su propia modificación.
            if (actualizandoFecha)
                return;

            actualizandoFecha = true;

            try
            {
                // Obtiene la posición original del cursor.
                int posicionOriginal =
                    fecha_naci_client.SelectionStart;

                // Cuenta los números que están antes del cursor.
                int digitosAntes =
                    fecha_naci_client.Text
                    .Substring(
                        0,
                        Math.Min(
                            posicionOriginal,
                            fecha_naci_client.Text.Length))
                    .Count(char.IsDigit);

                // Conserva únicamente los números.
                // Limita la entrada a ocho dígitos.
                string numeros = new string(
                    fecha_naci_client.Text
                    .Where(char.IsDigit)
                    .Take(8)
                    .ToArray());

                // Construye el formato dd/MM/aaaa.
                StringBuilder fechaFormateada =
                    new StringBuilder();

                for (int i = 0; i < numeros.Length; i++)
                {
                    // Agrega la diagonal después del día y del mes.
                    if (i == 2 || i == 4)
                    {
                        fechaFormateada.Append('/');
                    }

                    fechaFormateada.Append(numeros[i]);
                }

                // Obtiene la fecha con las diagonales.
                string nuevoTexto =
                    fechaFormateada.ToString();

                // Actualiza el TextBox solamente si el texto cambió.
                if (fecha_naci_client.Text != nuevoTexto)
                {
                    fecha_naci_client.Text = nuevoTexto;
                }

                // Recupera la posición del cursor.
                int nuevaPosicion = 0;
                int digitosContados = 0;

                while (nuevaPosicion < nuevoTexto.Length &&
                       digitosContados < digitosAntes)
                {
                    if (char.IsDigit(nuevoTexto[nuevaPosicion]))
                    {
                        digitosContados++;
                    }

                    nuevaPosicion++;
                }

                // Considera la diagonal después del día.
                if (digitosAntes >= 2 &&
                    digitosAntes < 4 &&
                    nuevoTexto.Length >= 3)
                {
                    nuevaPosicion =
                        Math.Max(nuevaPosicion, 3);
                }
                // Considera la diagonal después del mes.
                else if (digitosAntes >= 4 &&
                         nuevoTexto.Length >= 6)
                {
                    nuevaPosicion =
                        Math.Max(nuevaPosicion, 6);
                }

                // Coloca el cursor en su nueva posición.
                fecha_naci_client.SelectionStart =
                    Math.Min(
                        nuevaPosicion,
                        nuevoTexto.Length);
            }
            finally
            {
                actualizandoFecha = false;
            }
        }

        // EVENTO DEL BOTÓN PARA CANCELAR EL REGISTRO.
        private void cancelar_reg_cliente_Click(
            object sender, EventArgs e)
        {
            // Cierra la ventana de registro.
            this.Close();
        }

        // MÉTODO PARA VALIDAR EL CORREO ELECTRÓNICO.
        private bool ValidarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            string patron =
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(correo.Trim(), patron);
        }

        // MÉTODO PARA VALIDAR EL RFC.
        // Acepta la estructura general de un RFC de persona física
        // o moral.
        private bool ValidarRFC(string rfc)
        {
            if (string.IsNullOrWhiteSpace(rfc))
                return false;

            string patron =
                @"^[A-Z&Ñ]{3,4}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{3}$";

            return Regex.IsMatch(
                rfc.Trim().ToUpperInvariant(),
                patron);
        }

        // EVENTO PARA CONTROLAR LOS CARACTERES DEL RFC.
        private void ingresa_rfc_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            // Convierte el carácter presionado a mayúscula.
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);

            // Permite teclas de control, letras, números y el símbolo &.
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '&')
            {
                e.Handled = true;
            }
        }

        // MÉTODO PARA CONTROLAR LOS CARACTERES DE LOS NOMBRES.
        private void Nombre_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            // Convierte las letras a mayúsculas.
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);

            // Permite teclas de control, letras y espacios.
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // EVENTO DEL CUADRO DE TEXTO DEL PRIMER NOMBRE.
        private void primer_nom_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            Nombre_KeyPress(sender, e);
        }

        // EVENTO DEL CUADRO DE TEXTO DEL SEGUNDO NOMBRE.
        private void segundo_nom_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            Nombre_KeyPress(sender, e);
        }

        // EVENTO DEL CUADRO DE TEXTO DEL APELLIDO PATERNO.
        private void apellido_pater_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            Nombre_KeyPress(sender, e);
        }

        // EVENTO DEL CUADRO DE TEXTO DEL APELLIDO MATERNO.
        private void apellido_mater_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            Nombre_KeyPress(sender, e);
        }

        // EVENTO DEL TELÉFONO DE CASA.
        private void tel_casa_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            // Permite únicamente números y teclas de control.
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // EVENTO DEL NÚMERO DE CELULAR.
        private void celular_cliente_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            // Permite únicamente números y teclas de control.
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // EVENTO DEL MUNICIPIO.
        private void domicilio_municipio_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
        }

        // EVENTO DE LA CIUDAD.
        private void domicilio_ciudad_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
        }

        // EVENTO DE LA CALLE.
        private void domicilio_calle_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
        }

        // EVENTO DE LA COLONIA.
        private void domicilio_colonia_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
        }

        // EVENTO DEL NÚMERO DE VIVIENDA.
        private void domicilio_numviv_client_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
        }

        // EVENTO DEL BOTÓN PARA GUARDAR EL CLIENTE.
        private void save_registro_client_Click(
            object sender, EventArgs e)
        {
            // Obtiene el RFC escrito en el formulario.
            string rfc =
                ingresa_rfc.Text.Trim().ToUpperInvariant();

            // Obtiene el primer nombre.
            string primerNombre =
                primer_nom_client.Text.Trim().ToUpperInvariant();

            // Obtiene el segundo nombre.
            string segundoNombre =
                segundo_nom_client.Text.Trim().ToUpperInvariant();

            // Obtiene el apellido paterno.
            string apellidoPaterno =
                apellido_pater_client.Text.Trim().ToUpperInvariant();

            // Obtiene el apellido materno.
            string apellidoMaterno =
                apellido_mater_client.Text.Trim().ToUpperInvariant();

            // Obtiene el correo.
            string correo = mail_cliente.Text.Trim();

            // Obtiene los números telefónicos como texto.
            string telefonoCasa = tel_casa_client.Text.Trim();
            string celular = celular_cliente.Text.Trim();

            // Obtiene el municipio.
            string municipio =
                domicilio_municipio_client.Text.Trim().ToUpperInvariant();

            // Obtiene la ciudad.
            string ciudad =
                domicilio_ciudad_client.Text.Trim().ToUpperInvariant();

            // Obtiene la calle.
            string calle =
                domicilio_calle_client.Text.Trim().ToUpperInvariant();

            // Obtiene la colonia.
            string colonia =
                domicilio_colonia_client.Text.Trim().ToUpperInvariant();

            // Obtiene el número de vivienda.
            string numeroVivienda =
                domicilio_numviv_client.Text.Trim();

            // VALIDACIÓN DEL RFC.
            if (!ValidarRFC(rfc))
            {
                MessageBox.Show(
                    "El formato del RFC no es válido.",
                    "RFC inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                ingresa_rfc.Focus();
                return;
            }

            // VALIDACIÓN DEL CORREO ELECTRÓNICO.
            if (!ValidarCorreo(correo))
            {
                MessageBox.Show(
                    "Escribe un correo válido, por ejemplo: ejemplo@mail.com.",
                    "Correo inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                mail_cliente.Focus();
                return;
            }

            // VALIDACIÓN DE LOS CAMPOS OBLIGATORIOS.
            // El segundo nombre y el teléfono de casa son opcionales.
            if (string.IsNullOrWhiteSpace(primerNombre) ||
                string.IsNullOrWhiteSpace(apellidoPaterno) ||
                string.IsNullOrWhiteSpace(apellidoMaterno) ||
                string.IsNullOrWhiteSpace(celular) ||
                string.IsNullOrWhiteSpace(municipio) ||
                string.IsNullOrWhiteSpace(ciudad) ||
                string.IsNullOrWhiteSpace(calle) ||
                string.IsNullOrWhiteSpace(colonia) ||
                string.IsNullOrWhiteSpace(numeroVivienda))
            {
                MessageBox.Show(
                    "Completa todos los campos obligatorios.",
                    "Campos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // VALIDACIÓN DEL CELULAR.
            if (celular.Length != 10 ||
                !celular.All(char.IsDigit))
            {
                MessageBox.Show(
                    "El celular debe contener exactamente 10 dígitos.",
                    "Celular inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                celular_cliente.Focus();
                return;
            }

            // VALIDACIÓN DEL TELÉFONO DE CASA.
            // Solo se realiza si el usuario escribió un número.
            if (!string.IsNullOrWhiteSpace(telefonoCasa) &&
                (telefonoCasa.Length != 10 ||
                 !telefonoCasa.All(char.IsDigit)))
            {
                MessageBox.Show(
                    "El teléfono de casa debe contener 10 dígitos.",
                    "Teléfono inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                tel_casa_client.Focus();
                return;
            }

            // VALIDACIÓN DE LA FECHA DE NACIMIENTO.
            DateTime fechaNacimiento;

            // Comprueba que la fecha tenga exactamente el formato dd/MM/aaaa.
            if (fecha_naci_client.Text.Length != 10 ||
                !DateTime.TryParseExact(
                    fecha_naci_client.Text,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out fechaNacimiento))
            {
                MessageBox.Show(
                    "Ingresa una fecha de nacimiento válida con el formato dd/MM/aaaa.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                fecha_naci_client.Focus();
                return;
            }

            // Comprueba que la fecha no sea posterior al día actual.
            if (fechaNacimiento.Date > DateTime.Today)
            {
                MessageBox.Show(
                    "La fecha de nacimiento no puede ser futura.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                fecha_naci_client.Focus();
                return;
            }

            // VALIDACIÓN DEL OPERADOR.
            if (NominaOperador < 0)
            {
                MessageBox.Show(
                    "No se recibió la nómina del operador que registra al cliente. Vuelve a entrar desde el menú de operador.",
                    "Operador no identificado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // TRY PRINCIPAL.
            try
            {
                // Crea la conexión con la base de datos.
                using (SqlConnection conexion =
                    new SqlConnection(cadenaConexion))
                {
                    // Abre la conexión.
                    conexion.Open();

                    // Inicia una transacción.
                    using (SqlTransaction transaccion =
                        conexion.BeginTransaction())
                    {
                        try
                        {
                            // COMPROBAR QUE EL OPERADOR EXISTA.
                            using (SqlCommand cmdOperador =
                                new SqlCommand(
                                    @"SELECT COUNT(*)
                                      FROM dbo.OPERATIVOS
                                      WHERE Nomina_Ope = @Nomina;",
                                    conexion,
                                    transaccion))
                            {
                                cmdOperador.Parameters.Add(
                                    "@Nomina",
                                    SqlDbType.Int).Value = NominaOperador;

                                int existeOperador =
                                    Convert.ToInt32(
                                        cmdOperador.ExecuteScalar());

                                if (existeOperador == 0)
                                {
                                    throw new Exception(
                                        "La nómina del operador no existe en la tabla OPERATIVOS.");
                                }
                            }

                            // COMPROBAR QUE EL RFC NO ESTÉ DUPLICADO.
                            using (SqlCommand cmdRFC =
                                new SqlCommand(
                                    @"SELECT COUNT(*)
                                      FROM dbo.CLIENTES
                                      WHERE RFC_Cliente = @RFC;",
                                    conexion,
                                    transaccion))
                            {
                                cmdRFC.Parameters.Add(
                                    "@RFC",
                                    SqlDbType.VarChar,
                                    13).Value = rfc;

                                int existeRFC =
                                    Convert.ToInt32(
                                        cmdRFC.ExecuteScalar());

                                if (existeRFC > 0)
                                {
                                    throw new Exception(
                                        "Ya existe un cliente registrado con ese RFC.");
                                }
                            }

                            // CONSULTA SQL PARA INSERTAR AL CLIENTE.
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
                                    Calle,
                                    Colonia,
                                    Ciudad,
                                    Numero_Vivienda,
                                    Municipio,
                                    Nomina_Ope,
                                    Nomina_Admin,
                                    Fecha_nacimiento
                                )
                                VALUES
                                (
                                    @RFC,
                                    @PrimerNombre,
                                    @SegundoNombre,
                                    @ApellidoPaterno,
                                    @ApellidoMaterno,
                                    @Correo,
                                    @TelefonoCasa,
                                    @Celular,
                                    @Calle,
                                    @Colonia,
                                    @Ciudad,
                                    @NumeroVivienda,
                                    @Municipio,
                                    @NominaOperador,
                                    NULL,
                                    @FechaNacimiento
                                );";

                            // Crea el comando SQL para ejecutar el INSERT.
                            using (SqlCommand cmd =
                                new SqlCommand(
                                    consulta,
                                    conexion,
                                    transaccion))
                            {
                                // RFC del cliente.
                                cmd.Parameters.Add(
                                    "@RFC",
                                    SqlDbType.VarChar,
                                    13).Value = rfc;

                                // Primer nombre.
                                cmd.Parameters.Add(
                                    "@PrimerNombre",
                                    SqlDbType.VarChar,
                                    100).Value = primerNombre;

                                // Segundo nombre.
                                // Si está vacío, envía NULL.
                                cmd.Parameters.Add(
                                    "@SegundoNombre",
                                    SqlDbType.VarChar,
                                    100).Value =
                                    string.IsNullOrWhiteSpace(segundoNombre)
                                        ? (object)DBNull.Value
                                        : segundoNombre;

                                // Apellido paterno.
                                cmd.Parameters.Add(
                                    "@ApellidoPaterno",
                                    SqlDbType.VarChar,
                                    100).Value = apellidoPaterno;

                                // Apellido materno.
                                cmd.Parameters.Add(
                                    "@ApellidoMaterno",
                                    SqlDbType.VarChar,
                                    100).Value = apellidoMaterno;

                                // Correo electrónico.
                                cmd.Parameters.Add(
                                    "@Correo",
                                    SqlDbType.VarChar,
                                    150).Value = correo;

                                // Teléfono de casa.
                                // Si está vacío, envía NULL.
                                cmd.Parameters.Add(
                                    "@TelefonoCasa",
                                    SqlDbType.VarChar,
                                    10).Value =
                                    string.IsNullOrWhiteSpace(telefonoCasa)
                                        ? (object)DBNull.Value
                                        : telefonoCasa;

                                // Celular.
                                cmd.Parameters.Add(
                                    "@Celular",
                                    SqlDbType.VarChar,
                                    10).Value = celular;

                                // Calle.
                                cmd.Parameters.Add(
                                    "@Calle",
                                    SqlDbType.VarChar,
                                    150).Value = calle;

                                // Colonia.
                                cmd.Parameters.Add(
                                    "@Colonia",
                                    SqlDbType.VarChar,
                                    100).Value = colonia;

                                // Ciudad.
                                cmd.Parameters.Add(
                                    "@Ciudad",
                                    SqlDbType.VarChar,
                                    100).Value = ciudad;

                                // Número de vivienda.
                                cmd.Parameters.Add(
                                    "@NumeroVivienda",
                                    SqlDbType.VarChar,
                                    10).Value = numeroVivienda;

                                // Municipio.
                                cmd.Parameters.Add(
                                    "@Municipio",
                                    SqlDbType.VarChar,
                                    100).Value = municipio;

                                // Nómina del operador.
                                cmd.Parameters.Add(
                                    "@NominaOperador",
                                    SqlDbType.Int).Value = NominaOperador;

                                // Fecha de nacimiento.
                                cmd.Parameters.Add(
                                    "@FechaNacimiento",
                                    SqlDbType.Date).Value =
                                    fechaNacimiento.Date;

                                // Ejecuta el INSERT.
                                cmd.ExecuteNonQuery();
                            }

                            // Confirma la transacción.
                            transaccion.Commit();
                        }
                        catch
                        {
                            // Si ocurre un error, intenta deshacer la transacción.
                            try
                            {
                                transaccion.Rollback();
                            }
                            catch
                            {
                                // Ignora un posible error de Rollback.
                            }

                            // Vuelve a lanzar el error original.
                            throw;
                        }
                    }
                }

                // Mensaje de registro exitoso.
                MessageBox.Show(
                    "El cliente se registró correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Cierra la ventana de registro.
                this.Close();
            }

            // CAPTURA DE ERRORES DE SQL SERVER.
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "SQL Server no pudo guardar el cliente.\n\n" +
                    ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            // CAPTURA DE OTROS ERRORES.
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "No se pudo registrar el cliente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // EVENTO QUE SE EJECUTA AL CARGAR LA VENTANA.
        private void dclientes_eligio_registrar_Load(
            object sender, EventArgs e)
        {
            // No se definieron acciones adicionales al abrir la ventana.
        }

        // EVENTO QUE SE EJECUTA CUANDO CAMBIA EL TEXTO DEL RFC.
        private void ingresa_rfc_TextChanged(
            object sender, EventArgs e)
        {
        }

        // EVENTO QUE SE EJECUTA CUANDO CAMBIA EL TEXTO DEL CORREO.
        private void mail_cliente_TextChanged(
            object sender, EventArgs e)
        {
        }
    }
}
