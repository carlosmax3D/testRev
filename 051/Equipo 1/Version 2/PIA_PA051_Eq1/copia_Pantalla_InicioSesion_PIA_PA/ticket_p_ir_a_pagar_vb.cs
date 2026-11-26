
using System;
using System.Data;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    public partial class ticket_d_compra_p_pagar : Form
    {
        // Identificador de la selección temporal de boletos.
        public int IDSeleccionTemporal { get; set; }

        // RFC del cliente que realizará la compra.
        public string RFCCliente { get; set; } = "";

        // Nómina del operativo que registra la venta.
        public int NominaOperador { get; set; }

        // Nómina del administrador relacionado con la venta.
        public int NominaAdmin { get; set; }

        // RFC del emisor de la factura.
        public string RFCEmisor { get; set; } = "EPC261105AB3";

        // Total de la compra.
        private decimal totalCompra;

        // Evita continuar si no se cargó correctamente el ticket.
        private bool ticketGuardado = false;

        public ticket_d_compra_p_pagar()
        {
            InitializeComponent();
        }

        // Evento Load del formulario.
        private void ticket_d_compra_p_pagar_Load(object sender, EventArgs e)
        {
            CargarResumenTicket();
        }

        // Consulta únicamente el resumen de la selección.
        // No registra la venta ni genera un folio.
        private void CargarResumenTicket()
        {
            ticketGuardado = false;
            totalCompra = 0;

            try
            {
                // Comprobar que exista una selección.
                if (IDSeleccionTemporal <= 0)
                {
                    MessageBox.Show(
                        "No se recibió un identificador válido de la selección.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Comprobar el RFC del cliente.
                if (string.IsNullOrWhiteSpace(RFCCliente))
                {
                    MessageBox.Show(
                        "No se recibió el RFC del cliente.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Comprobar la nómina del operativo.
                if (NominaOperador <= 0)
                {
                    MessageBox.Show(
                        "No se recibió una nómina válida del operativo.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Establecer el RFC del emisor predeterminado.
                if (string.IsNullOrWhiteSpace(RFCEmisor))
                {
                    RFCEmisor = "EPC261105AB3";
                }

                // Consultar el procedimiento almacenado mediante ConexionBD.
                DataTable tabla = ConexionBD.ObtenerResumenSeleccion(
                    IDSeleccionTemporal);

                if (tabla == null || tabla.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontró información de la selección de boletos.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DataRow registro = tabla.Rows[0];

                // Obtener los importes calculados en SQL Server.
                decimal subtotal = Convert.ToDecimal(registro["Subtotal"]);
                decimal iva = Convert.ToDecimal(registro["IVA"]);

                totalCompra = Convert.ToDecimal(
                    registro["Total_de_Venta"]);

                int cantidadBoletos = Convert.ToInt32(
                    registro["Total_de_Boletos"]);

                // Comprobar que los importes sean válidos.
                if (subtotal < 0 || iva < 0 || totalCompra <= 0 ||
                    cantidadBoletos <= 0)
                {
                    MessageBox.Show(
                        "Los datos económicos de la compra no son válidos.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    totalCompra = 0;
                    return;
                }

                // Mostrar la información en los Label del ticket.
                // El folio todavía no existe porque no se ha confirmado el pago.
                folio_d_ticket.Text = "Pendiente de pago";

                subtotal_ticket.Text = subtotal.ToString("C2");

                iva_compra.Text = iva.ToString("C2");

                tot_d_venta.Text = totalCompra.ToString("C2");

                tot_d_boletos.Text = cantidadBoletos.ToString();

                // Estos datos se reciben desde las propiedades del formulario.
                rfc_client_ticket.Text = RFCCliente.Trim();

                nom_op_ticket.Text = NominaOperador.ToString();

                nom_admin_ticket.Text = NominaAdmin.ToString();

                rfc_emisor_ticket.Text = RFCEmisor.Trim();

                // Confirmar que el ticket se cargó correctamente.
                ticketGuardado = true;
            }
            catch (Exception ex)
            {
                totalCompra = 0;
                ticketGuardado = false;

                MessageBox.Show(
                    "Error al cargar el ticket:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Botón para continuar al proceso de pago.
        private void ir_a_pagar_Click(object sender, EventArgs e)
        {
            if (!ticketGuardado)
            {
                MessageBox.Show(
                    "No se pudo cargar el resumen de la compra.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (totalCompra <= 0)
            {
                MessageBox.Show(
                    "El total de la compra no es válido.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // Crear la pantalla para elegir el método de pago.
                elegir_metodo_pago pago =
                    new elegir_metodo_pago();

                // Transferir el importe calculado.
                pago.TotalCompra = totalCompra;

                // Transferir los datos necesarios para registrar la venta.
                pago.IDSeleccionTemporal = IDSeleccionTemporal;
                pago.RFCCliente = RFCCliente;
                pago.NominaOperador = NominaOperador;
                pago.NominaAdmin = NominaAdmin;
                pago.RFCEmisor = RFCEmisor;

                // Ocultar el ticket antes de abrir la siguiente pantalla.
                this.Hide();

                // Abrir la pantalla de métodos de pago conservando
                // el ticket como formulario propietario.
                pago.Show(this);
            }
            catch (Exception ex)
            {
                this.Show();

                MessageBox.Show(
                    "Error al abrir la pantalla de métodos de pago:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Botón para cancelar o cerrar el ticket.
        private void cancelar_siempre_no_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Eventos de etiquetas.
        private void folio_d_ticket_Click(object sender, EventArgs e)
        {
        }

        private void subtotal_ticket_Click(object sender, EventArgs e)
        {
        }

        private void iva_compra_Click(object sender, EventArgs e)
        {
        }

        private void tot_d_venta_Click(object sender, EventArgs e)
        {
        }

        private void tot_d_boletos_Click(object sender, EventArgs e)
        {
        }

        private void rfc_client_ticket_Click(object sender, EventArgs e)
        {
        }

        private void nom_op_ticket_Click(object sender, EventArgs e)
        {
        }

        private void nom_admin_ticket_Click(object sender, EventArgs e)
        {
        }

        private void rfc_emisor_ticket_Click(object sender, EventArgs e)
        {
        }
    }
}
