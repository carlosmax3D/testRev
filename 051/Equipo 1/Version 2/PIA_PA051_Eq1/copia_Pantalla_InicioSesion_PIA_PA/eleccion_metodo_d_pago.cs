
using System;
using System.Windows.Forms;

namespace Menu_p_Operativo
{
    // Declara la clase del formulario que permite elegir el método de pago.
    public partial class elegir_metodo_pago : Form
    {
        // Importe total de la compra.
        public decimal TotalCompra { get; set; }

        // Datos necesarios para registrar la venta al confirmar el pago.
        public int IDSeleccionTemporal { get; set; }

        public string RFCCliente { get; set; } = string.Empty;

        public int NominaOperador { get; set; }

        public int NominaAdmin { get; set; }

        public string RFCEmisor { get; set; } = "EPC261105AB3";

        // Constructor del formulario.
        public elegir_metodo_pago()
        {
            InitializeComponent();
        }

        // Evento para seleccionar el pago en efectivo.
        private void eligio_pago_p_efectivo_Click(
            object sender, EventArgs e)
        {
            try
            {
                // Crea la pantalla de pago en efectivo.
                eligio_pagar_c_efectivo pantalla =
                    new eligio_pagar_c_efectivo();

                // Transfiere los datos de la compra.
                pantalla.TotalCompra = TotalCompra;
                pantalla.IDSeleccionTemporal = IDSeleccionTemporal;
                pantalla.RFCCliente = RFCCliente;
                pantalla.NominaOperador = NominaOperador;
                pantalla.NominaAdmin = NominaAdmin;
                pantalla.RFCEmisor = RFCEmisor;

                // Oculta la pantalla de métodos de pago.
                this.Hide();

                // Muestra la pantalla de efectivo conservando
                // la pantalla actual como propietaria.
                pantalla.Show(this);
            }
            catch (Exception ex)
            {
                this.Show();

                MessageBox.Show(
                    "Error al abrir la pantalla de pago en efectivo:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento para seleccionar el pago con tarjeta.
        private void eligio_pago_p_tarjeta_Click(
            object sender, EventArgs e)
        {
            try
            {
                // Crea la pantalla de pago con tarjeta.
                eligio_p_tarjeta pantalla =
                    new eligio_p_tarjeta();

                // Transfiere los datos de la compra.
                pantalla.TotalCompra = TotalCompra;
                pantalla.IDSeleccionTemporal = IDSeleccionTemporal;
                pantalla.RFCCliente = RFCCliente;
                pantalla.NominaOperador = NominaOperador;
                pantalla.NominaAdmin = NominaAdmin;
                pantalla.RFCEmisor = RFCEmisor;

                // Oculta la pantalla de métodos de pago.
                this.Hide();

                // Muestra la pantalla de tarjeta conservando
                // la pantalla actual como propietaria.
                pantalla.Show(this);
            }
            catch (Exception ex)
            {
                this.Show();

                MessageBox.Show(
                    "Error al abrir la pantalla de pago con tarjeta:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento para cancelar la operación y regresar al menú operativo.
        private void cancelar_venta_bolrtos_reg_men_oper_Click(
            object sender, EventArgs e)
        {
            // Obtiene el formulario propietario.
            Form formularioAnterior = this.Owner;

            if (formularioAnterior != null &&
                !formularioAnterior.IsDisposed)
            {
                // Regresa al formulario anterior.
                formularioAnterior.Show();
                formularioAnterior.Activate();

                // Cierra la pantalla actual.
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo recuperar la pantalla anterior.",
                    "Error de navegación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void elegir_metodo_pago_Load(
            object sender, EventArgs e)
        {
            // Actualmente no se realiza ninguna acción al cargar la pantalla.
        }
    }
}
