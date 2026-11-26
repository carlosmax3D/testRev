
using Microsoft.Data.SqlClient;
using Pantalla_InicioSesion_PIA_PA.conexionBD;
using Prueba2_PrimerAvance_PAyMAD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pantalla3_PrimerAvancePAyMAD
{
    // Define el formulario donde se registra o actualiza la conducta del operativo.
    public partial class pantalla_conductaOpe : Form
    {
        // Guardar la referencia al menú Admin original.
        private pantalla_menuAdmin menuAdminAnterior;

        // Constructor que recibe el menú Admin.
        public pantalla_conductaOpe(pantalla_menuAdmin menuAdmin)
        {
            // Inicializa los controles visuales del formulario.
            InitializeComponent();

            // Guardar la ventana anterior para poder regresar.
            menuAdminAnterior = menuAdmin;
        }

        // Aquí continúa el resto de tu código...

        // Evento que se ejecuta cuando se carga la pantalla.
        private void pantalla_conductaOpe_Load(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta al presionar el botón para guardar la conducta del operativo.

        // Evento que se ejecuta al presionar el botón para guardar la conducta del operativo.
        private void boton1_conductaOpe_Click(object sender, EventArgs e)
        {
            // Obtener la nómina escrita.
            string textoNomina = txtbox1_conductaOpe.Text.Trim();

            // Validar que tenga seis dígitos y comience con 1.
            if (textoNomina.Length != 6 ||
                textoNomina[0] != '1' ||
                !textoNomina.All(char.IsDigit) ||
                !int.TryParse(textoNomina, out int nomina))
            {
                MessageBox.Show(
                    "La nómina debe tener 6 dígitos y comenzar con el número 1.",
                    "Nómina inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox1_conductaOpe.Focus();
                txtbox1_conductaOpe.SelectAll();
                return;
            }

            // Obtener la productividad seleccionada.
            int productividad = 0;

            if (radioboton1_conductaOpe.Checked) productividad = 1;
            else if (radioboton2_conductaOpe.Checked) productividad = 2;
            else if (radioboton3_conductaOpe.Checked) productividad = 3;
            else if (radioboton4_conductaOpe.Checked) productividad = 4;
            else if (radioboton5_conductaOpe.Checked) productividad = 5;
            else
            {
                MessageBox.Show(
                    "Selecciona una opción de productividad.",
                    "Dato faltante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Obtener la resistencia seleccionada.
            int resistencia = 0;

            if (radioboton6_conductaOpe.Checked) resistencia = 1;
            else if (radioboton7_conductaOpe.Checked) resistencia = 2;
            else if (radioboton8_conductaOpe.Checked) resistencia = 3;
            else if (radioboton9_conductaOpe.Checked) resistencia = 4;
            else if (radioboton10_conductaOpe.Checked) resistencia = 5;
            else
            {
                MessageBox.Show(
                    "Selecciona una opción de resistencia.",
                    "Dato faltante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // Llamar al método de ConexionBD para guardar los datos.
                int resultado = ConexionBD.GuardarConductaOperativo(
                    nomina,
                    productividad,
                    resistencia);

                // Mostrar el mensaje correspondiente al resultado.
                if (resultado == 0)
                {
                    MessageBox.Show(
                        "La nómina no existe en la tabla USUARIOS.",
                        "Operativo no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else if (resultado == 1)
                {
                    MessageBox.Show(
                        "Los datos del operativo se actualizaron correctamente.",
                        "Actualización exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LimpiarCampos();
                }
                else if (resultado == 2)
                {
                    MessageBox.Show(
                        "El operativo se registró correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(
                        "No se obtuvo un resultado válido al guardar los datos.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar los datos del operativo:\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // Método que limpia los campos del formulario después de guardar o actualizar.
        private void LimpiarCampos()
        {
            // Borra el texto escrito en el campo de nómina.
            txtbox1_conductaOpe.Clear();

            // Desmarca todas las opciones de productividad.
            radioboton1_conductaOpe.Checked = false;
            radioboton2_conductaOpe.Checked = false;
            radioboton3_conductaOpe.Checked = false;
            radioboton4_conductaOpe.Checked = false;
            radioboton5_conductaOpe.Checked = false;

            // Desmarca todas las opciones de resistencia.
            radioboton6_conductaOpe.Checked = false;
            radioboton7_conductaOpe.Checked = false;
            radioboton8_conductaOpe.Checked = false;
            radioboton9_conductaOpe.Checked = false;
            radioboton10_conductaOpe.Checked = false;

            // Devuelve el cursor al campo de nómina para permitir otra captura.
            txtbox1_conductaOpe.Focus();
        }

        // Evento del clic en la primera etiqueta; actualmente no realiza ninguna acción.
        private void label1_conductaOpe_Click(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la segunda opción de productividad.
        // Actualmente no contiene instrucciones.
        private void radioboton2_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta al entrar en el primer grupo de controles.
        // Actualmente no contiene instrucciones.
        private void grupbox1_conductaOpe_Enter(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la primera opción de productividad.
        // Actualmente no contiene instrucciones.
        private void radioboton1_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la tercera opción de productividad.
        // Actualmente no contiene instrucciones.
        private void radioboton3_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la cuarta opción de productividad.
        // Actualmente no contiene instrucciones.
        private void radioboton4_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la quinta opción de productividad.
        // Actualmente no contiene instrucciones.
        private void radioboton5_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta al entrar en el segundo grupo de controles.
        // Actualmente no contiene instrucciones.
        private void grupbox2_conductaOpe_Enter(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la primera opción de resistencia.
        // Actualmente no contiene instrucciones.
        private void radioboton6_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la segunda opción de resistencia.
        // Actualmente no contiene instrucciones.
        private void radioboton7_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la tercera opción de resistencia.
        // Actualmente no contiene instrucciones.
        private void radioboton8_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la cuarta opción de resistencia.
        // Actualmente no contiene instrucciones.
        private void radioboton9_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en la quinta opción de resistencia.
        // Actualmente no contiene instrucciones.
        private void radioboton10_conductaOpe_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta al presionar el botón para regresar al menú de administración.
        private void bton2_conductaOpe_Click(object sender, EventArgs e)
        {
            // Vuelve a mostrar el menú de administración que se recibió en el constructor.
            menuAdminAnterior.Show();

            // Activa la ventana del menú para que quede al frente.
            menuAdminAnterior.Activate();

            // Cierra la pantalla actual de conducta del operativo.
            this.Close();
        }

        // Evento del clic en la segunda etiqueta; actualmente no realiza ninguna acción.
        private void label2_conductaOpe_Click(object sender, EventArgs e)
        {

        }

        // Evento que detecta cambios en el texto del campo de nómina.
        // Actualmente no contiene instrucciones.
        private void txtbox1_conductaOpe_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
