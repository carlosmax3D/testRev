
using System;
using System.Windows.Forms;

namespace Menu_p_Operativo
{
    public partial class eligio_p_tarjeta : Form
    {
        public decimal TotalCompra { get; set; }

        // Se conserva por compatibilidad, pero el folio se genera
        // hasta que se confirma el pago.
        public int FolioVenta { get; set; }

        public int IDSeleccionTemporal { get; set; }

        public string RFCCliente { get; set; } = string.Empty;

        public int NominaOperador { get; set; }

        public int NominaAdmin { get; set; }

        public string RFCEmisor { get; set; } = "EPC261105AB3";

        public eligio_p_tarjeta()
        {
            InitializeComponent();
        }

        // Regresar a la selección del método de pago.
        private void regresar_a_met_pago_Click(
            object sender, EventArgs e)
        {
            elegir_metodo_pago pantalla =
                new elegir_metodo_pago();

            TransferirDatos(pantalla);

            pantalla.Show();
            this.Hide();
        }

        // Seleccionar tarjeta de débito.
        private void eligio_tarjeta_debito_Click(
            object sender, EventArgs e)
        {
            eligio_tarjeta_debito pantalla =
                new eligio_tarjeta_debito();

            TransferirDatos(pantalla);

            pantalla.Show();
            this.Hide();
        }

        // Seleccionar tarjeta de crédito.
        private void eligio_tarjeta_credito_Click(
            object sender, EventArgs e)
        {
            eligio_tarjeta_credito pantalla =
                new eligio_tarjeta_credito();

            TransferirDatos(pantalla);

            pantalla.Show();
            this.Hide();
        }

        // Transfiere los datos de la compra a la pantalla siguiente.
        private void TransferirDatos(elegir_metodo_pago pantalla)
        {
            pantalla.TotalCompra = TotalCompra;
            pantalla.IDSeleccionTemporal = IDSeleccionTemporal;
            pantalla.RFCCliente = RFCCliente;
            pantalla.NominaOperador = NominaOperador;
            pantalla.NominaAdmin = NominaAdmin;
            pantalla.RFCEmisor = RFCEmisor;
        }

        private void TransferirDatos(eligio_tarjeta_debito pantalla)
        {
            pantalla.TotalCompra = TotalCompra;
            pantalla.IDSeleccionTemporal = IDSeleccionTemporal;
            pantalla.RFCCliente = RFCCliente;
            pantalla.NominaOperador = NominaOperador;
            pantalla.NominaAdmin = NominaAdmin;
            pantalla.RFCEmisor = RFCEmisor;
        }

        private void TransferirDatos(eligio_tarjeta_credito pantalla)
        {
            pantalla.TotalCompra = TotalCompra;
            pantalla.IDSeleccionTemporal = IDSeleccionTemporal;
            pantalla.RFCCliente = RFCCliente;
            pantalla.NominaOperador = NominaOperador;
            pantalla.NominaAdmin = NominaAdmin;
            pantalla.RFCEmisor = RFCEmisor;
        }

        private void eligio_p_tarjeta_Load(
            object sender, EventArgs e)
        {
        }
    }
}
