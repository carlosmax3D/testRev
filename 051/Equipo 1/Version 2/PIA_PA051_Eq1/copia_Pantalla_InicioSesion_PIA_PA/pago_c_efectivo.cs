
using System;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    public partial class eligio_pagar_c_efectivo : Form
    {
        // Datos de la compra recibidos de las pantallas anteriores.
        public decimal TotalCompra { get; set; }

        public int FolioVenta { get; set; }

        public int IDSeleccionTemporal { get; set; }

        public string RFCCliente { get; set; } = string.Empty;

        public int NominaOperador { get; set; }

        public int NominaAdmin { get; set; }

        public string RFCEmisor { get; set; } = "EPC261105AB3";

        public eligio_pagar_c_efectivo()
        {
            InitializeComponent();

            TextBox[] cajasNumericas =
            {
                un_peso,
                dos_pesos,
                cinco_pesos,
                diez_pesos,
                veinte_pesos,
                cincuenta_pesos,
                cien_pesos,
                doscientos_pesos,
                quinientos_pesos,
                mil_pesos
            };

            foreach (TextBox caja in cajasNumericas)
            {
                caja.KeyPress += SoloNumeros_KeyPress;
                caja.TextChanged += ActualizarTotal;
            }

            ActualizarTotal(this, EventArgs.Empty);
        }

        private void SoloNumeros_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private int LeerCantidad(TextBox caja)
        {
            if (string.IsNullOrWhiteSpace(caja.Text))
                return 0;

            int cantidad;

            if (!int.TryParse(caja.Text, out cantidad) ||
                cantidad < 0)
            {
                throw new FormatException(
                    "Ingresa cantidades enteras válidas.");
            }

            return cantidad;
        }

        private decimal ObtenerImporte(
            TextBox caja, int denominacion)
        {
            int cantidad;

            if (!int.TryParse(caja.Text, out cantidad) ||
                cantidad < 0)
            {
                cantidad = 0;
            }

            return (decimal)cantidad * denominacion;
        }

        private decimal CalcularEfectivoRecibido()
        {
            return
                ObtenerImporte(un_peso, 1) +
                ObtenerImporte(dos_pesos, 2) +
                ObtenerImporte(cinco_pesos, 5) +
                ObtenerImporte(diez_pesos, 10) +
                ObtenerImporte(veinte_pesos, 20) +
                ObtenerImporte(cincuenta_pesos, 50) +
                ObtenerImporte(cien_pesos, 100) +
                ObtenerImporte(doscientos_pesos, 200) +
                ObtenerImporte(quinientos_pesos, 500) +
                ObtenerImporte(mil_pesos, 1000);
        }

        private void ActualizarTotal(object sender, EventArgs e)
        {
            decimal total = CalcularEfectivoRecibido();
            total_recibido.Text = total.ToString("C2");
        }

        private void sig_p_cambio_Click(object sender, EventArgs e)
        {
            int monedas;
            int billetes;
            decimal efectivoRecibido;

            try
            {
                monedas =
                    LeerCantidad(un_peso) +
                    LeerCantidad(dos_pesos);

                billetes =
                    LeerCantidad(cinco_pesos) +
                    LeerCantidad(diez_pesos) +
                    LeerCantidad(veinte_pesos) +
                    LeerCantidad(cincuenta_pesos) +
                    LeerCantidad(cien_pesos) +
                    LeerCantidad(doscientos_pesos) +
                    LeerCantidad(quinientos_pesos) +
                    LeerCantidad(mil_pesos);

                efectivoRecibido = CalcularEfectivoRecibido();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Cantidad incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (monedas == 0 && billetes == 0)
            {
                MessageBox.Show(
                    "Ingresa al menos una moneda o un billete.",
                    "Falta efectivo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (TotalCompra <= 0)
            {
                MessageBox.Show(
                    "No se recibió un total de compra válido.",
                    "Total de compra no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (IDSeleccionTemporal <= 0 ||
                string.IsNullOrWhiteSpace(RFCCliente) ||
                NominaOperador <= 0)
            {
                MessageBox.Show(
                    "Faltan datos de la selección o del comprador. " +
                    "Regresa a la pantalla anterior.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (efectivoRecibido < TotalCompra)
            {
                decimal faltante = TotalCompra - efectivoRecibido;

                MessageBox.Show(
                    "El dinero recibido es insuficiente.\n\n" +
                    "Total de la compra: " + TotalCompra.ToString("C2") +
                    "\nEfectivo recibido: " + efectivoRecibido.ToString("C2") +
                    "\nFalta: " + faltante.ToString("C2"),
                    "Pago insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // La venta, el método de pago, los boletos y la limpieza
                // de las selecciones se realizan en el procedimiento almacenado.
                ResultadoVentaPago resultado =
                    ConexionBD.RegistrarVentaConPago(
                        IDSeleccionTemporal,
                        RFCCliente,
                        NominaOperador,
                        NominaAdmin,
                        RFCEmisor,
                        "EFECTIVO",
                        null,
                        Convert.ToInt32(efectivoRecibido),
                        billetes,
                        monedas);

                if (resultado == null || resultado.Folio <= 0)
                {
                    throw new Exception(
                        "No se recibió un folio válido después del pago.");
                }

                FolioVenta = resultado.Folio;

                decimal cambio = efectivoRecibido - TotalCompra;

                if (cambio > 0)
                {
                    feria_del_pago_efect pantalla =
                        new feria_del_pago_efect();

                    pantalla.Cambio = cambio;

                    pantalla.Show();
                    this.Hide();
                }
                else
                {
                    p_efectivo_pago_exacto pantalla =
                        new p_efectivo_pago_exacto();

                    pantalla.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo confirmar la venta y el pago:\n" +
                    ex.Message,
                    "Error de registro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void atras_regresa_met_pago_Click(
            object sender, EventArgs e)
        {
            elegir_metodo_pago pantalla =
                new elegir_metodo_pago();

            pantalla.TotalCompra = TotalCompra;
            pantalla.IDSeleccionTemporal = IDSeleccionTemporal;
            pantalla.RFCCliente = RFCCliente;
            pantalla.NominaOperador = NominaOperador;
            pantalla.NominaAdmin = NominaAdmin;
            pantalla.RFCEmisor = RFCEmisor;

            pantalla.Show();
            this.Hide();
        }

        private void un_peso_TextChanged(object sender, EventArgs e) { }
        private void dos_pesos_TextChanged(object sender, EventArgs e) { }
        private void cinco_pesos_TextChanged(object sender, EventArgs e) { }
        private void diez_pesos_TextChanged(object sender, EventArgs e) { }
        private void veinte_pesos_TextChanged(object sender, EventArgs e) { }
        private void cincuenta_pesos_TextChanged(object sender, EventArgs e) { }
        private void cien_pesos_TextChanged(object sender, EventArgs e) { }
        private void doscientos_pesos_TextChanged(object sender, EventArgs e) { }
        private void quinientos_pesos_TextChanged(object sender, EventArgs e) { }
        private void mil_pesos_TextChanged(object sender, EventArgs e) { }

        private void total_recibido_Click(object sender, EventArgs e) { }

        private void eligio_pagar_c_efectivo_Load(
            object sender, EventArgs e)
        {
        }
    }
}
