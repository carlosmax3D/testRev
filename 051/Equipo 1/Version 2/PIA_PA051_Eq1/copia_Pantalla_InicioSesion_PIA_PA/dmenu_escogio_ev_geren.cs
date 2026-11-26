
// Importa funciones básicas de C#, como tipos de datos y conversiones.
using System;

// Permite crear formularios, botones, cuadros de texto y mensajes.
using System.Windows.Forms;

// Permite utilizar la clase central de conexión a la base de datos.
using Pantalla_InicioSesion_PIA_PA.conexionBD;

// Define el espacio de nombres al que pertenece esta clase.
namespace Menu_p_Operativo
{
    // Declara la clase de la ventana de evaluación gerencial.
    // partial permite dividir la clase en varios archivos.
    // Form indica que esta clase representa una ventana de Windows Forms.
    public partial class dmenu_eligio_ev_geren : Form
    {
        // Constructor del formulario.
        // Se ejecuta cuando se crea una instancia de esta ventana.
        public dmenu_eligio_ev_geren()
        {
            // Inicializa los controles creados desde el diseñador:
            // botones, cuadros de texto, etiquetas y opciones de calificación.
            InitializeComponent();
        }

        // EVENTO PARA VALIDAR LA NÓMINA.
        // Se ejecuta cada vez que el usuario presiona una tecla
        // dentro del cuadro de texto de la nómina.
        private void nom_admin_a_evaluar_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            // Comprueba si la tecla presionada no es una tecla de control
            // y tampoco es un número.
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                // Cancela la entrada del carácter.
                // Así se bloquean letras y símbolos.
                e.Handled = true;
            }
        }

        // EVENTO DEL BOTÓN PARA GUARDAR LA EVALUACIÓN.
        // Se ejecuta cuando el usuario hace clic en guardar.
        private void guardar_ev_gerencial_Click(
            object sender, EventArgs e)
        {
            // Obtiene la nómina escrita en el cuadro de texto.
            // Trim elimina los espacios al principio y al final.
            string nominaTexto =
                nom_admin_a_evaluar.Text.Trim();

            // VALIDAR QUE SE HAYA ESCRITO UNA NÓMINA.
            // IsNullOrWhiteSpace comprueba si el texto es nulo,
            // está vacío o contiene solamente espacios.
            if (string.IsNullOrWhiteSpace(nominaTexto))
            {
                // Muestra una advertencia si no se ingresó la nómina.
                MessageBox.Show(
                    "Por favor, ingrese la nómina del administrador.",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Coloca el cursor en el cuadro de texto de la nómina.
                nom_admin_a_evaluar.Focus();

                // Detiene el método para que no continúe el guardado.
                return;
            }

            // CONVERTIR LA NÓMINA A UN NÚMERO ENTERO.
            // TryParse intenta convertir el texto a int.
            // Si funciona, guarda el resultado en nominaAdmin.
            // Si falla, muestra un mensaje y no continúa.
            if (!int.TryParse(nominaTexto, out int nominaAdmin))
            {
                MessageBox.Show(
                    "La nómina ingresada no es válida.",
                    "Nómina incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Regresa el cursor al campo de la nómina.
                nom_admin_a_evaluar.Focus();

                // Selecciona todo el texto para facilitar su corrección.
                nom_admin_a_evaluar.SelectAll();

                // Termina el método.
                return;
            }

            // Declara la variable que almacenará la calificación
            // de liderazgo. Se inicia en cero porque aún no hay selección.
            int valorLiderazgo = 0;

            // Declara la variable para la calificación de comunicación.
            // También inicia en cero porque aún no hay selección.
            int valorComunicacion = 0;

            // OBTENER LA CALIFICACIÓN DE LIDERAZGO.
            // Checked indica si una opción está seleccionada.
            if (liderazgo_1_def.Checked)
                valorLiderazgo = 1; // Asigna 1 si seleccionaron la primera opción.
            else if (liderazgo_2.Checked)
                valorLiderazgo = 2; // Asigna 2 si seleccionaron la segunda opción.
            else if (liderazgo_3.Checked)
                valorLiderazgo = 3; // Asigna 3 si seleccionaron la tercera opción.
            else if (liderazgo_4.Checked)
                valorLiderazgo = 4; // Asigna 4 si seleccionaron la cuarta opción.
            else if (liderazgo_5_excelente.Checked)
                valorLiderazgo = 5; // Asigna 5 si seleccionaron la quinta opción.

            // OBTENER LA CALIFICACIÓN DE COMUNICACIÓN EFECTIVA.
            if (com_efect_1_def.Checked)
                valorComunicacion = 1; // Primera calificación.
            else if (com_efect_2.Checked)
                valorComunicacion = 2; // Segunda calificación.
            else if (com_efect_3.Checked)
                valorComunicacion = 3; // Tercera calificación.
            else if (com_efect_4.Checked)
                valorComunicacion = 4; // Cuarta calificación.
            else if (com_efect_5_excelente.Checked)
                valorComunicacion = 5; // Quinta calificación.

            // VALIDAR QUE SE HAYA CALIFICADO EL LIDERAZGO.
            // Si el valor sigue en cero, no se seleccionó una opción.
            if (valorLiderazgo == 0)
            {
                // Informa al usuario que falta responder esta evaluación.
                MessageBox.Show(
                    "Selecciona una calificación para liderazgo.",
                    "Evaluación incompleta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Detiene el proceso de guardado.
                return;
            }

            // VALIDAR QUE SE HAYA CALIFICADO LA COMUNICACIÓN.
            if (valorComunicacion == 0)
            {
                // Informa que falta seleccionar una calificación.
                MessageBox.Show(
                    "Selecciona una calificación para comunicación efectiva.",
                    "Evaluación incompleta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Evita guardar una evaluación incompleta.
                return;
            }

            // TRY: inicia el bloque donde se intentará guardar la información.
            // Si ocurre un error, se manejará en el bloque catch.
            try
            {
                // Llama al método central de ConexionBD.
                // Este método ejecuta el procedimiento almacenado
                // sp_GuardarEvaluacionGerencial en SQL Server.
                int filasActualizadas =
                    ConexionBD.GuardarEvaluacionGerencial(
                        nominaAdmin,
                        valorLiderazgo,
                        valorComunicacion);

                // Si se actualizaron cero filas, no se encontró
                // un administrador con la nómina proporcionada.
                if (filasActualizadas == 0)
                {
                    // Muestra un mensaje de advertencia.
                    MessageBox.Show(
                        "La nómina ingresada no existe en la tabla ADMINISTRADORES. Verifica el número e inténtalo nuevamente.",
                        "Nómina incorrecta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    // Regresa al cuadro de texto de la nómina.
                    nom_admin_a_evaluar.Focus();

                    // Selecciona el texto para que pueda reemplazarse.
                    nom_admin_a_evaluar.SelectAll();

                    // Termina el método sin mostrar el mensaje de éxito.
                    return;
                }

                // Este mensaje se muestra cuando la operación
                // termina sin lanzar una excepción.
                MessageBox.Show(
                    "La evaluación gerencial se guardó correctamente.",
                    "Guardado exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Cierra solamente el formulario de evaluación.
                // No crea otro menú del operador.
                // El menú original se volverá a mostrar mediante
                // el evento FormClosed de la ventana que lo abrió.
                this.Close();
            }

            // CAPTURA DE ERRORES.
            // Maneja excepciones de SQL Server y otros errores.
            catch (Exception ex)
            {
                // Muestra un mensaje general con la descripción del error.
                MessageBox.Show(
                    "Ocurrió un error al guardar la evaluación:\n\n"
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // EVENTO DEL BOTÓN PARA REGRESAR AL MENÚ SIN GUARDAR.
        private void regresar_a_menu_op_Click(
            object sender, EventArgs e)
        {
            // Cierra únicamente la ventana de evaluación.
            // No crea una nueva instancia del menú del operador.
            this.Close();
        }

        // EVENTO QUE SE EJECUTA CUANDO SE CARGA EL FORMULARIO.
        private void dmenu_eligio_ev_geren_Load(
            object sender, EventArgs e)
        {
            // No se ejecutan acciones adicionales al abrir la ventana.
        }
    }
}
