
// Importa las clases básicas de C#, como Convert y Exception.
using System;

// Permite crear formularios, utilizar controles y mostrar mensajes al usuario.
using System.Windows.Forms;

// Permite conectarse a SQL Server y ejecutar instrucciones SQL.
using Microsoft.Data.SqlClient;

// Define el espacio de nombres al que pertenece esta clase.
namespace Menu_p_Operativo
{
    // Declara el formulario donde el usuario selecciona la cantidad de boletos.
    public partial class selec_cant_boletos : Form
    {
        // Cadena de conexión para acceder a la base de datos PrimerAvance.
        // Data Source indica el servidor y la instancia de SQL Server.
        // Initial Catalog indica la base de datos que se utilizará.
        // Integrated Security utiliza la autenticación de Windows.
        // TrustServerCertificate permite confiar en el certificado del servidor.
        private readonly string cadenaConexion =
            @"Data Source=XLMD007\SQLEXPRESS;
              Initial Catalog=PrimerAvance;
              Integrated Security=True;
              TrustServerCertificate=True;";

        // Datos recibidos de la pantalla de selección de zona.
        // Guarda el número del partido seleccionado.
        public int NumeroPartido { get; set; }

        // Guarda el identificador de la zona seleccionada por el cliente.
        public int IDZonaSeleccionada { get; set; }

        // Guarda el número de nómina del operador que está atendiendo la compra.
        public int NominaOperador { get; set; }

        // Guarda el RFC del cliente relacionado con la compra.
        public string RFCCliente { get; set; }

        // Guarda el identificador del estadio donde se realizará el partido.
        public int IDEstadio { get; set; }

        // Identificador y cantidad de la selección guardada.
        // Almacena el ID que SQL Server genera para identificar
        // la selección temporal de boletos.
        // private set permite consultar la propiedad desde otras clases,
        // pero impide modificarla directamente desde fuera de este formulario.
        public int IDSeleccionTemporal { get; private set; }

        // Guarda la cantidad de boletos seleccionada por el usuario.
        // get permite consultar el valor y set permite modificarlo.
        public int CantidadBoletos { get; set; }

        // Constructor del formulario.
        // Se ejecuta cuando se crea una instancia de esta pantalla.
        public selec_cant_boletos()
        {
            // Inicializa los controles visuales creados en el diseñador,
            // como el ComboBox y los botones.
            InitializeComponent();
        }

        // Cargar las cantidades disponibles en el ComboBox.
        // Este evento se ejecuta cuando se carga la pantalla.
        private void selec_cant_boletos_Load(
            object sender, EventArgs e)
        {
            // Comprueba si el ComboBox todavía no contiene elementos.
            // Esto evita agregar las mismas cantidades repetidamente.
            if (cant_boletos_1_a_4.Items.Count == 0)
            {
                // Agrega la opción para comprar un boleto.
                cant_boletos_1_a_4.Items.Add("1");

                // Agrega la opción para comprar dos boletos.
                cant_boletos_1_a_4.Items.Add("2");

                // Agrega la opción para comprar tres boletos.
                cant_boletos_1_a_4.Items.Add("3");

                // Agrega la opción para comprar cuatro boletos.
                cant_boletos_1_a_4.Items.Add("4");
            }

            // Deja el ComboBox sin una opción seleccionada inicialmente.
            // El usuario tendrá que elegir la cantidad de boletos.
            cant_boletos_1_a_4.SelectedIndex = -1;
        }

        // Regresar a la selección de zona conservando los datos.
        // Este evento se ejecuta cuando el usuario presiona el botón para regresar.
        private void ir_pa_atras_Click(
            object sender, EventArgs e)
        {
            // Crea una nueva instancia del formulario de selección de zona.
            selec_zona_p_compboleto pantalla =
                new selec_zona_p_compboleto();

            // Transfiere el número del partido a la pantalla anterior.
            pantalla.NumeroPartido = NumeroPartido;

            // Transfiere el identificador de la zona seleccionada.
            pantalla.IDZonaSeleccionada = IDZonaSeleccionada;

            // Conserva la información del operador.
            pantalla.NominaOperador = NominaOperador;

            // Conserva el RFC del cliente.
            pantalla.RFCCliente = RFCCliente;

            // Conserva el identificador del estadio.
            pantalla.IDEstadio = IDEstadio;

            // Muestra nuevamente el formulario de selección de zona.
            pantalla.Show();

            // Oculta el formulario actual sin cerrarlo.
            this.Hide();
        }

        // Guardar la cantidad seleccionada y abrir la selección de filas.
        // Este evento se ejecuta cuando el usuario presiona el botón Siguiente.
        private void sig_pa_selec_filas_Click(
            object sender, EventArgs e)
        {
            // Intenta convertir el texto del ComboBox a un número entero.
            // Trim elimina espacios al inicio y al final.
            // TryParse intenta realizar la conversión sin provocar
            // una excepción si el texto no es un número válido.
            // La variable cantidad recibe el valor convertido.
            if (!int.TryParse(
                    cant_boletos_1_a_4.Text.Trim(),
                    out int cantidad) ||
                cantidad < 1 || cantidad > 4)
            {
                // Si no se seleccionó una cantidad válida,
                // muestra un mensaje indicando el rango permitido.
                MessageBox.Show(
                    "Selecciona una cantidad de 1 a 4 boletos.",
                    "Cantidad no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Detiene el método para no guardar datos incorrectos.
                return;
            }

            // Comprueba que el número del partido y el identificador de la zona
            // sean positivos, lo que indica que se recibieron valores válidos.
            if (NumeroPartido <= 0 || IDZonaSeleccionada <= 0)
            {
                // Informa que los datos del partido o de la zona no son válidos.
                MessageBox.Show(
                    "No se recibió el partido o la zona correctamente.",
                    "Faltan datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Impide continuar con datos incompletos.
                return;
            }

            // Verifica que el RFC del cliente no sea nulo,
            // esté vacío o contenga únicamente espacios.
            if (string.IsNullOrWhiteSpace(RFCCliente))
            {
                // Informa al usuario que falta el RFC necesario para la compra.
                MessageBox.Show(
                    "No se recibió el RFC del cliente.",
                    "Faltan datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Detiene el proceso hasta contar con el RFC.
                return;
            }

            // Intenta guardar la selección temporal en la base de datos.
            // También captura errores relacionados con SQL Server
            // y otros errores que puedan ocurrir durante el proceso.
            try
            {
                // Crea una conexión con la base de datos utilizando
                // la cadena de conexión declarada al inicio de la clase.
                using (SqlConnection conexion =
                    new SqlConnection(cadenaConexion))
                {
                    // Inserta un registro en SeleccionTemporalBoletos
                    // con el partido, la zona y la cantidad seleccionada.
                    // OUTPUT INSERTED.ID_Seleccion devuelve el identificador
                    // generado para el nuevo registro.
                    string consulta = @"
                        INSERT INTO dbo.SeleccionBoletos
                            (Numero_partido, ID_Zona, Cantidad)
                        OUTPUT INSERTED.ID_Seleccion
                        VALUES
                            (@NumeroPartido, @IDZona, @Cantidad);";

                    // Crea el comando SQL y lo asocia con la conexión.
                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        // Asigna el número del partido al parámetro SQL.
                        comando.Parameters.AddWithValue(
                            "@NumeroPartido", NumeroPartido);

                        // Asigna el identificador de la zona seleccionada.
                        comando.Parameters.AddWithValue(
                            "@IDZona", IDZonaSeleccionada);

                        // Asigna la cantidad de boletos que desea comprar el usuario.
                        comando.Parameters.AddWithValue(
                            "@Cantidad", cantidad);

                        // Abre la conexión con SQL Server.
                        conexion.Open();

                        // Ejecuta la instrucción INSERT y recupera
                        // el identificador generado por la base de datos.
                        // Convert.ToInt32 convierte el resultado a un entero.
                        IDSeleccionTemporal =
                            Convert.ToInt32(comando.ExecuteScalar());
                    }
                }

                // Guarda en la propiedad la cantidad de boletos validada.
                CantidadBoletos = cantidad;

                // Abrir la pantalla de selección de filas.
                // Crea una nueva instancia del formulario para elegir la fila.
                selec_fila_disp pantalla = new selec_fila_disp();

                // Transfiere el número del partido a la siguiente pantalla.
                pantalla.NumeroPartido = NumeroPartido;

                // Transfiere la zona seleccionada.
                pantalla.IDZonaSeleccionada = IDZonaSeleccionada;

                // Transfiere la cantidad de boletos solicitada.
                pantalla.CantidadBoletos = CantidadBoletos;

                // Transfiere el identificador de la selección temporal
                // que se acaba de guardar en SQL Server.
                pantalla.IDSeleccionTemporal = IDSeleccionTemporal;

                // Transmitir también la identidad de la compra.
                // Transfiere el número de nómina del operador.
                pantalla.NominaOperador = NominaOperador;

                // Transfiere el RFC del cliente.
                pantalla.RFCCliente = RFCCliente;

                // Transfiere el identificador del estadio.
                pantalla.IDEstadio = IDEstadio;

                // Muestra el formulario donde el usuario seleccionará la fila.
                pantalla.Show();

                // Oculta la pantalla actual para continuar con el siguiente paso.
                this.Hide();
            }
            // Captura específicamente los errores relacionados con SQL Server,
            // como problemas de conexión, restricciones o errores en la consulta.
            catch (SqlException ex)
            {
                // Muestra un mensaje con la descripción del error de SQL Server.
                MessageBox.Show(
                    "No se pudo guardar la selección en SQL Server.\n\n" +
                    ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            // Captura otros errores que no sean específicamente SqlException.
            catch (Exception ex)
            {
                // Informa que ocurrió un error general durante el proceso.
                MessageBox.Show(
                    "Ocurrió un error al continuar:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta cuando cambia la opción seleccionada en el ComboBox.
        private void cant_boletos_1_a_4_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            // La cantidad se procesa al presionar Siguiente.
            // Por eso este evento no realiza ninguna operación adicional.
        }
    }
}
