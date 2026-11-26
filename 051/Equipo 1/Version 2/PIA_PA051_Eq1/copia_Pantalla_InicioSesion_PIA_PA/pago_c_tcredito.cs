
using System;
using System.Linq;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    public partial class eligio_tarjeta_credito : Form
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

        public eligio_tarjeta_credito()
        {
            InitializeComponent();

            // Conectar los eventos de validación.
            ConfigurarEventosValidacion();
        }

        // =========================================================
        // CONECTAR EVENTOS DE VALIDACIÓN
        // =========================================================

        private void ConfigurarEventosValidacion()
        {
            // Evento Load.
            this.Load -= eligio_tarjeta_credito_Load;
            this.Load += eligio_tarjeta_credito_Load;

            // Número de tarjeta.
            num_tarjeta_credito.TextChanged -= num_tarjeta_credito_TextChanged;
            num_tarjeta_credito.TextChanged += num_tarjeta_credito_TextChanged;

            num_tarjeta_credito.KeyPress -= num_tarjeta_credito_KeyPress;
            num_tarjeta_credito.KeyPress += num_tarjeta_credito_KeyPress;

            // CVV.
            cvv_tarjeta_credito.TextChanged -= ccv_tarjeta_credito_TextChanged;
            cvv_tarjeta_credito.TextChanged += ccv_tarjeta_credito_TextChanged;

            cvv_tarjeta_credito.KeyPress -= cvv_tarjeta_credito_KeyPress;
            cvv_tarjeta_credito.KeyPress += cvv_tarjeta_credito_KeyPress;

            // Nombre del banco.
            nom_banco_credito.TextChanged -= nom_banco_credito_TextChanged;
            nom_banco_credito.TextChanged += nom_banco_credito_TextChanged;

            nom_banco_credito.KeyPress -= nom_banco_credito_KeyPress;
            nom_banco_credito.KeyPress += nom_banco_credito_KeyPress;

            // Nombre del titular.
            nom_titular_tarjeta_credito.TextChanged -= nom_titular_tarjeta_credito_TextChanged;
            nom_titular_tarjeta_credito.TextChanged += nom_titular_tarjeta_credito_TextChanged;

            nom_titular_tarjeta_credito.KeyPress -= nom_titular_tarjeta_credito_KeyPress;
            nom_titular_tarjeta_credito.KeyPress += nom_titular_tarjeta_credito_KeyPress;

            // Fecha de expiración.
            fecha_exp_tarjeta_credito.TextChanged -= fecha_exp_tarjeta_credito_TextChanged;
            fecha_exp_tarjeta_credito.TextChanged += fecha_exp_tarjeta_credito_TextChanged;

            fecha_exp_tarjeta_credito.KeyPress -= fecha_exp_tarjeta_credito_KeyPress;
            fecha_exp_tarjeta_credito.KeyPress += fecha_exp_tarjeta_credito_KeyPress;
        }

        // Regresar a la selección de tarjeta.
        private void atras_C_a_met_p_tar_Click(
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

        // Confirmar pago simulado con tarjeta de crédito.
        private void ir_a_pagar_c_credito_Click(
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

            // Obtener los meses seleccionados.
            int meses;

            if (msi_0.Checked)
            {
                meses = 0;
            }
            else if (msi_1.Checked)
            {
                meses = 1;
            }
            else if (msi_2.Checked)
            {
                meses = 2;
            }
            else if (msi_3.Checked)
            {
                meses = 3;
            }
            else
            {
                MessageBox.Show(
                    "Selecciona una opción de meses sin intereses.",
                    "Selecciona una opción",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Confirmar que el usuario desea realizar el pago simulado.
            DialogResult confirmacion = MessageBox.Show(
                "Se realizará una simulación de pago con tarjeta de crédito " +
                "por $" + TotalCompra.ToString("N2") +
                "\nMeses seleccionados: " + meses +
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
                        "CREDITO",
                        meses,
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
                    "La simulación del pago con crédito fue exitosa.\n\n" +
                    "Folio: " + resultado.Folio +
                    "\nSubtotal: $" + resultado.Subtotal.ToString("N2") +
                    "\nIVA: $" + resultado.IVA.ToString("N2") +
                    "\nTotal: $" + resultado.TotalVenta.ToString("N2") +
                    "\nBoletos: " + resultado.TotalBoletos,
                    "Pago confirmado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                p_credito_exitoso siguiente =
                    new p_credito_exitoso();

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
            string numero = num_tarjeta_credito.Text.Trim();
            string banco = nom_banco_credito.Text.Trim();
            string titular = nom_titular_tarjeta_credito.Text.Trim();
            string fecha = fecha_exp_tarjeta_credito.Text.Trim();
            string cvv = cvv_tarjeta_credito.Text.Trim();

            if (numero.Length < 13 || numero.Length > 19 ||
                !numero.All(c => c >= '0' && c <= '9'))
            {
                MessageBox.Show(
                    "El número de tarjeta debe contener entre 13 y 19 dígitos.",
                    "Número de tarjeta inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                num_tarjeta_credito.Focus();
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

                nom_titular_tarjeta_credito.Focus();
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

                nom_banco_credito.Focus();
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

                fecha_exp_tarjeta_credito.Focus();
                return false;
            }

            if (cvv.Length != 3 ||
                !cvv.All(c => c >= '0' && c <= '9'))
            {
                MessageBox.Show(
                    "El CVV debe contener exactamente 3 dígitos.",
                    "CVV inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cvv_tarjeta_credito.Focus();
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

            if (!mesTexto.All(c => c >= '0' && c <= '9') ||
                !anioTexto.All(c => c >= '0' && c <= '9'))
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

        // =========================================================
        // FILTRAR LOS CAMPOS
        // =========================================================

        // Filtra un texto y permite únicamente letras y espacios.
        private void FiltrarLetras(TextBox campo)
        {
            if (actualizandoCampos)
                return;

            actualizandoCampos = true;

            try
            {
                int posicion = campo.SelectionStart;

                string texto = new string(
                    campo.Text
                        .Where(c => char.IsLetter(c) || c == ' ')
                        .ToArray())
                        .ToUpperInvariant();

                campo.Text = texto;

                campo.SelectionStart =
                    Math.Min(posicion, campo.Text.Length);
            }
            finally
            {
                actualizandoCampos = false;
            }
        }

        // Filtra un campo para permitir únicamente números.
        private void FiltrarNumeros(TextBox campo, int longitudMaxima)
        {
            if (actualizandoCampos)
                return;

            actualizandoCampos = true;

            try
            {
                int posicion = campo.SelectionStart;

                string texto = new string(
                    campo.Text
                        .Where(c => c >= '0' && c <= '9')
                        .Take(longitudMaxima)
                        .ToArray());

                campo.Text = texto;

                campo.SelectionStart =
                    Math.Min(posicion, campo.Text.Length);
            }
            finally
            {
                actualizandoCampos = false;
            }
        }

        // Formatea automáticamente la fecha como MM/AA.
        private void FormatearFechaExpiracion()
        {
            if (actualizandoCampos)
                return;

            actualizandoCampos = true;

            try
            {
                string textoOriginal = fecha_exp_tarjeta_credito.Text;
                int posicionOriginal =
                    fecha_exp_tarjeta_credito.SelectionStart;

                // Contar los dígitos que están antes del cursor.
                int digitosAntes = textoOriginal
                    .Take(posicionOriginal)
                    .Count(c => c >= '0' && c <= '9');

                // Conservar únicamente cuatro dígitos.
                string numeros = new string(
                    textoOriginal
                        .Where(c => c >= '0' && c <= '9')
                        .Take(4)
                        .ToArray());

                string fechaFormateada = numeros;

                if (numeros.Length > 2)
                {
                    fechaFormateada =
                        numeros.Substring(0, 2) + "/" +
                        numeros.Substring(2);
                }

                fecha_exp_tarjeta_credito.Text = fechaFormateada;

                int nuevaPosicion = digitosAntes;

                if (digitosAntes > 2)
                    nuevaPosicion++;

                fecha_exp_tarjeta_credito.SelectionStart =
                    Math.Min(nuevaPosicion, fechaFormateada.Length);
            }
            finally
            {
                actualizandoCampos = false;
            }
        }

        // =========================================================
        // EVENTOS DE LOS CAMPOS
        // =========================================================

        private void eligio_tarjeta_credito_Load(
            object sender, EventArgs e)
        {
            // Longitudes máximas de los campos.
            num_tarjeta_credito.MaxLength = 19;
            nom_titular_tarjeta_credito.MaxLength = 80;
            nom_banco_credito.MaxLength = 60;
            fecha_exp_tarjeta_credito.MaxLength = 5;
            cvv_tarjeta_credito.MaxLength = 3;
        }

        // Número de tarjeta: únicamente números.
        private void num_tarjeta_credito_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void num_tarjeta_credito_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarNumeros(num_tarjeta_credito, 19);
        }

        // CVV: únicamente números.
        private void cvv_tarjeta_credito_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void ccv_tarjeta_credito_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarNumeros(cvv_tarjeta_credito, 3);
        }

        // Banco: únicamente letras y espacios.
        private void nom_banco_credito_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetter(e.KeyChar) &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void nom_banco_credito_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarLetras(nom_banco_credito);
        }

        // Titular: únicamente letras y espacios.
        private void nom_titular_tarjeta_credito_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetter(e.KeyChar) &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void nom_titular_tarjeta_credito_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarLetras(nom_titular_tarjeta_credito);
        }

        // Fecha: únicamente números. La diagonal se agrega automáticamente.
        private void fecha_exp_tarjeta_credito_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void fecha_exp_tarjeta_credito_TextChanged(
            object sender, EventArgs e)
        {
            FormatearFechaExpiracion();
        }

        private void opciones_msi_Enter(
            object sender, EventArgs e)
        {
        }

        private void msi_0_CheckedChanged(
            object sender, EventArgs e)
        {
        }

        private void msi_1_CheckedChanged(
            object sender, EventArgs e)
        {
        }

        private void msi_2_CheckedChanged(
            object sender, EventArgs e)
        {
        }

        private void msi_3_CheckedChanged(
            object sender, EventArgs e)
        {
        }

        private void label1_Click(
            object sender, EventArgs e)
        {
        }
    }
}
