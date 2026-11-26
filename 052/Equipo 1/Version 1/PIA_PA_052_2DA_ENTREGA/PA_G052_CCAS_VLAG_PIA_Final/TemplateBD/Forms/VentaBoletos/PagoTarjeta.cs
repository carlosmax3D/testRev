using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;

namespace TemplateCSharp.Forms.VentaBoletos
{
    public partial class PagoTarjeta : Form
    {
        private readonly ITarjetaService _tarjetaService;
        public PagoTarjeta()
        {
            InitializeComponent();
            _tarjetaService = null!;
        }
        //Esto es para inyectar la dependencia del servicio de tarjeta, que se encargará de validar y
        //detectar la marca de la tarjeta.
        public PagoTarjeta(ITarjetaService tarjetaService)
        {
            InitializeComponent();
            _tarjetaService = tarjetaService;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //Validar Nombre (ya con verificaciones, solo letras y espacios)
            string titular = txtTitular.Text.Trim();
            if (string.IsNullOrWhiteSpace(titular))
            {
                MessageBox.Show("Ingrese el nombre del titular.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string patronNombre = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$";
            if (!Regex.IsMatch(titular, patronNombre))
            {
                MessageBox.Show("El nombre del titular solo puede contener letras y espacios (no se permiten números ni símbolos).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Validar Número de Tarjeta
            // Nota: El metodo ValidarTarjeta ya se encarga de que no haya letras ni simbolos
            string numero = txtNumeroTarjeta.Text.Replace(" ", "").Replace("-", "");
            if (!_tarjetaService.ValidarNumeroTarjeta(numero))  //LLAMADA AL SERVICIO
            {
                MessageBox.Show("El número de tarjeta no es válido o no pertenece a Visa, Mastercard o Amex.",
                    "Tarjeta Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Validar fecha de Expiración (formato MM/AA)
            string fecha = new string(txtExpiracion.Text.Where(char.IsDigit).ToArray()); // Esta línea extrae SOLO los numeros, ignorando guiones, barras, espacios, etc.
            if (fecha.Length != 4 || !fecha.All(char.IsDigit))
            {
                MessageBox.Show("Ingrese una fecha de expiración válida (MM/AA).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int mes = int.Parse(fecha.Substring(0, 2));
            int año = int.Parse(fecha.Substring(2, 2)) + 2000;

            if (mes < 1 || mes > 12)
            {
                MessageBox.Show("El mes de expiración no es válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaVencimiento = new DateTime(año, mes, 1).AddMonths(1).AddDays(-1);
            if (fechaVencimiento < DateTime.Today)
            {
                MessageBox.Show("La tarjeta está vencida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar CVV
            if (txtCVV.Text.Length < 3 || txtCVV.Text.Length > 4 || !txtCVV.Text.All(char.IsDigit))
            {
                MessageBox.Show("El CVV debe tener 3 o 4 dígitos numéricos (sin letras ni símbolos).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si todo está bien, da luz verde
            MessageBox.Show("Datos de tarjeta validados correctamente. Procesando pago...", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //dectecta la marca en tiempo real 
        private void txtNumeroTarjeta_TextChanged(object sender, EventArgs e)
        {
            // Limpiamos el texto de espacios para analizarlo
            string numero = txtNumeroTarjeta.Text.Replace(" ", "").Replace("-", "");

            if (string.IsNullOrEmpty(numero))
            {
                label7.Text = "Marca: No seleccionada";
                return;
            }

            // Logica para detectar la marca según los primeros dígitos
            label7.Text = "Marca: " + _tarjetaService.DetectarMarca(numero);
        }

        private void txtExpiracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si el usuario presiona el guion medio (-), lo bloquea
            if (e.KeyChar == '-')
            {
                e.Handled = true; // Esto cancela la tecla
            }
        }
    }
}
