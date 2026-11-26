
using System;
using System.Linq;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    public partial class eligio_tarjeta_debito : Form
    {
        public decimal TotalCompra { get; set; }

        // Datos necesarios para confirmar la venta.
        public int IDSeleccionTemporal { get; set; }

        public string RFCCliente { get; set; } = string.Empty;

        public int NominaOperador { get; set; }

        public int NominaAdmin { get; set; }

        public string RFCEmisor { get; set; } = "EPC261105AB3";

        // Evita que los eventos TextChanged se ejecuten repetidamente.
        private bool actualizandoCampos = false;

        public eligio_tarjeta_debito()
        {
            InitializeComponent();
        }

        // Regresar a la selección de tarjeta.
        private void atras_D_a_met_p_tar_Click(
            object sender, EventArgs e)
        {
            eligio_p_tarjeta anterior =
                new eligio_p_tarjeta();

            anterior.TotalCompra = TotalCompra;
            anterior.IDSeleccionTemporal = IDSeleccionTemporal;
            anterior.RFCCliente = RFCCliente;
            anterior.NominaOperador = NominaOperador;
            anterior.NominaAdmin = NominaAdmin;
            anterior.RFCEmisor = RFCEmisor;

            anterior.Show();
            this.Hide();
        }

        // Confirmar pago simulado con tarjeta de débito.
        private void ir_a_pagar_c_debito_Click(
            object sender, EventArgs e)
        {
            // Validar que se recibieron los datos de la selección.
            if (IDSeleccionTemporal <= 0 ||
                string.IsNullOrWhiteSpace(RFCCliente) ||
                NominaOperador <= 0 ||
                TotalCompra <= 0)
            {
                MessageBox.Show(
                    "No se recibieron correctamente los datos de la compra. " +
                    "Regresa al ticket e intenta nuevamente.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Validar los datos de la tarjeta.
            if (!ValidarDatosTarjeta())
                return;

            // Confirmar la simulación del pago.
            DialogResult confirmacion = MessageBox.Show(
                "Se realizará una simulación de pago con tarjeta de débito " +
                "por $" + TotalCompra.ToString("N2") +
                "\n\n¿Deseas confirmar la operación?",
                "Confirmar pago",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                // El procedimiento almacenado genera el folio,
                // registra la venta, el pago y los boletos,
                // y elimina las selecciones temporales al confirmar.
                ResultadoVentaPago resultado =
                    ConexionBD.RegistrarVentaConPago(
                        IDSeleccionTemporal,
                        RFCCliente,
                        NominaOperador,
                        NominaAdmin,
                        RFCEmisor,
                        "DEBITO",
                        null,
                        null,
                        0,
                        0);

                if (resultado == null || resultado.Folio <= 0)
                {
                    MessageBox.Show(
                        "No se recibió un folio válido de la operación.",
                        "Error de confirmación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "La simulación del pago con débito fue exitosa.\n\n" +
                    "Folio: " + resultado.Folio +
                    "\nSubtotal: $" + resultado.Subtotal.ToString("N2") +
                    "\nIVA: $" + resultado.IVA.ToString("N2") +
                    "\nTotal: $" + resultado.TotalVenta.ToString("N2") +
                    "\nBoletos: " + resultado.TotalBoletos,
                    "Pago confirmado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                pago_c_tdebito_exitoso siguiente =
                    new pago_c_tdebito_exitoso();

                siguiente.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo confirmar la venta:\n\n" + ex.Message,
                    "Error al confirmar el pago",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // VALIDACIONES DE LOS CAMPOS DE LA TARJETA
        // =========================================================

        private bool ValidarDatosTarjeta()
        {
            string numero = num_tarjeta_debito.Text.Trim();
            string banco = nom_banco_debito.Text.Trim();
            string titular = nom_titular_tarjeta_debito.Text.Trim();
            string fecha = fecha_exp_tarjeta_debito.Text.Trim();
            string cvv = cvv_tarjeta_debito.Text.Trim();

            if (numero.Length < 13 || numero.Length > 19 ||
                !numero.All(char.IsDigit))
            {
                MessageBox.Show(
                    "El número de tarjeta debe contener entre 13 y 19 dígitos.",
                    "Número de tarjeta inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                num_tarjeta_debito.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(titular) ||
                !EsNombreValido(titular))
            {
                MessageBox.Show(
                    "Ingresa el nombre del titular utilizando únicamente letras y espacios.",
                    "Nombre inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                nom_titular_tarjeta_debito.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(banco) ||
                !EsNombreValido(banco))
            {
                MessageBox.Show(
                    "Ingresa el nombre del banco utilizando únicamente letras y espacios.",
                    "Banco inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                nom_banco_debito.Focus();
                return false;
            }

            if (!ValidarFechaExpiracion(fecha))
            {
                MessageBox.Show(
                    "La fecha debe tener el formato MM/AA, " +
                    "corresponder a un mes válido y no estar vencida.",
                    "Fecha de expiración inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                fecha_exp_tarjeta_debito.Focus();
                return false;
            }

            if (cvv.Length != 3 || !cvv.All(char.IsDigit))
            {
                MessageBox.Show(
                    "El CVV debe contener exactamente 3 dígitos.",
                    "CVV inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cvv_tarjeta_debito.Focus();
                return false;
            }

            return true;
        }

        // Comprueba que un nombre contenga únicamente letras y espacios.
        private bool EsNombreValido(string texto)
        {
            return texto.All(c => char.IsLetter(c) || c == ' ');
        }

        // Comprueba el formato MM/AA y que la fecha no esté vencida.
        private bool ValidarFechaExpiracion(string fecha)
        {
            if (fecha.Length != 5 || fecha[2] != '/')
                return false;

            string mesTexto = fecha.Substring(0, 2);
            string anioTexto = fecha.Substring(3, 2);

            if (!mesTexto.All(char.IsDigit) ||
                !anioTexto.All(char.IsDigit))
                return false;

            int mes = int.Parse(mesTexto);
            int anio = 2000 + int.Parse(anioTexto);

            if (mes < 1 || mes > 12)
                return false;

            DateTime ultimoDiaMes =
                new DateTime(
                    anio,
                    mes,
                    DateTime.DaysInMonth(anio, mes));

            return ultimoDiaMes.Date >= DateTime.Today;
        }

        // Filtra un texto y permite únicamente letras y espacios.
        private void FiltrarLetras(TextBox campo)
        {
            if (actualizandoCampos)
                return;

            actualizandoCampos = true;

            int posicion = campo.SelectionStart;

            string texto = new string(
                campo.Text
                    .Where(c => char.IsLetter(c) || c == ' ')
                    .ToArray())
                    .ToUpperInvariant();

            campo.Text = texto;

            campo.SelectionStart =
                Math.Min(posicion, campo.Text.Length);

            actualizandoCampos = false;
        }

        // Filtra un campo para permitir únicamente números.
        private void FiltrarNumeros(TextBox campo, int longitudMaxima)
        {
            if (actualizandoCampos)
                return;

            actualizandoCampos = true;

            int posicion = campo.SelectionStart;

            string texto = new string(
                campo.Text
                    .Where(char.IsDigit)
                    .Take(longitudMaxima)
                    .ToArray());

            campo.Text = texto;

            campo.SelectionStart =
                Math.Min(posicion, campo.Text.Length);

            actualizandoCampos = false;
        }

        // Formatea automáticamente la fecha como MM/AA.
        private void FormatearFechaExpiracion()
        {
            if (actualizandoCampos)
                return;

            actualizandoCampos = true;

            int posicion =
                fecha_exp_tarjeta_debito.SelectionStart;

            string numeros = new string(
                fecha_exp_tarjeta_debito.Text
                    .Where(char.IsDigit)
                    .Take(4)
                    .ToArray());

            string fechaFormateada = numeros;

            if (numeros.Length > 2)
            {
                fechaFormateada =
                    numeros.Substring(0, 2) + "/" +
                    numeros.Substring(2);
            }

            fecha_exp_tarjeta_debito.Text = fechaFormateada;

            int nuevaPosicion = posicion;

            if (numeros.Length > 2 &&
                posicion >= 2 &&
                posicion <= 4)
            {
                nuevaPosicion = posicion + 1;
            }

            fecha_exp_tarjeta_debito.SelectionStart =
                Math.Min(nuevaPosicion, fechaFormateada.Length);

            actualizandoCampos = false;
        }

        // =========================================================
        // EVENTOS DE LOS CAMPOS
        // =========================================================

        private void eligio_tarjeta_debito_Load(
            object sender, EventArgs e)
        {
            // Longitudes máximas de los campos.
            num_tarjeta_debito.MaxLength = 19;
            nom_titular_tarjeta_debito.MaxLength = 80;
            nom_banco_debito.MaxLength = 60;
            fecha_exp_tarjeta_debito.MaxLength = 5;
            cvv_tarjeta_debito.MaxLength = 3;
        }

        private void num_tarjeta_debito_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarNumeros(num_tarjeta_debito, 19);
        }

        private void nom_banco_debito_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarLetras(nom_banco_debito);
        }

        private void nom_titular_tarjeta_debito_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarLetras(nom_titular_tarjeta_debito);
        }

        private void fecha_exp_tarjeta_debito_TextChanged(
            object sender, EventArgs e)
        {
            FormatearFechaExpiracion();
        }

        private void cvv_tarjeta_debito_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarNumeros(cvv_tarjeta_debito, 3);
        }

        private void label5_cvv_Click(
            object sender, EventArgs e)
        {
        }
    }
}
