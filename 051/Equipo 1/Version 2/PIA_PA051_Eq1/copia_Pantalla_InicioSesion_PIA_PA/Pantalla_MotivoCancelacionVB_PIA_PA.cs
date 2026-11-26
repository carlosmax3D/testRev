
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Pantalla_MotivoCancelacionVB_PIA_PA
{
    // Define el formulario donde el usuario escribe el motivo de cancelación
    // de una venta de boletos.
    public partial class pantalla_motivoCancelacionVB : Form
    {
        // Propiedad que almacena el motivo de cancelación escrito por el usuario.
        public string MotivoCancelacion { get; private set; }

        // Constructor del formulario.
        public pantalla_motivoCancelacionVB()
        {
            // Inicializa los controles visuales creados en el diseñador.
            InitializeComponent();

            // Establece la longitud máxima del motivo.
            txtbox1_motivoCancelacion.MaxLength = 250;
        }

        // Evento que se ejecuta cuando se carga la pantalla.
        private void pantalla_motivoCancelacionVB_Load(object sender, EventArgs e)
        {
        }

        // Evento que se ejecuta cuando se hace clic en la etiqueta de texto.
        private void label1_motivoCancelacion_Click(object sender, EventArgs e)
        {
        }

        // Evento que controla los caracteres permitidos.
        private void txtbox1_motivoCancelacion_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            // Permite teclas de control, como borrar y retroceso.
            if (char.IsControl(e.KeyChar))
                return;

            // Permite letras, números y espacios.
            if (char.IsLetterOrDigit(e.KeyChar) ||
                char.IsWhiteSpace(e.KeyChar))
                return;

            // Permite signos de puntuación básicos.
            if (".,;:¿?¡!()-".Contains(e.KeyChar))
                return;

            // Rechaza cualquier otro carácter.
            e.Handled = true;
        }

        // Evento que se ejecuta cuando cambia el texto del cuadro de motivo.
        private void txtbox1_motivoCancelacion_TextChanged(object sender, EventArgs e)
        {
        }

        // Botón Siguiente
        private void boton1_motivoCancelacion_Click(object sender, EventArgs e)
        {
            // Obtiene el texto escrito por el usuario.
            string motivo = txtbox1_motivoCancelacion.Text.Trim();

            // Comprueba si el campo está vacío.
            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show(
                    "Por favor, escriba el motivo de cancelación.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox1_motivoCancelacion.Focus();
                return;
            }

            // Comprueba que el motivo tenga al menos 5 caracteres.
            if (motivo.Length < 5)
            {
                MessageBox.Show(
                    "El motivo debe contener al menos 5 caracteres.",
                    "Motivo demasiado corto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox1_motivoCancelacion.Focus();
                return;
            }

            // Comprueba que el motivo no supere los 250 caracteres.
            if (motivo.Length > 250)
            {
                MessageBox.Show(
                    "El motivo no puede superar los 250 caracteres.",
                    "Límite excedido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox1_motivoCancelacion.Focus();
                return;
            }

            // Comprueba que contenga al menos una letra o número.
            if (!Regex.IsMatch(motivo, @"[\p{L}\p{N}]"))
            {
                MessageBox.Show(
                    "El motivo debe contener letras o números.",
                    "Motivo no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox1_motivoCancelacion.Focus();
                return;
            }

            // Guarda el motivo ingresado.
            MotivoCancelacion = motivo;

            // Devuelve el resultado correcto al formulario que lo abrió.
            this.DialogResult = DialogResult.OK;

            // Cierra la ventana.
            this.Close();
        }

        // Botón Regresar
        private void boton2_motivoCancelacion_Click(object sender, EventArgs e)
        {
            // Indica que el usuario decidió cancelar esta operación.
            this.DialogResult = DialogResult.Cancel;

            // Cierra el formulario.
            this.Close();
        }
    }
}
