
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    // Formulario que muestra la confirmación de compra.
    public partial class mostrar_conf_compra : Form
    {
        // Datos del partido y los boletos seleccionados.
        public int NumeroPartido { get; set; }
        public int IDZonaSeleccionada { get; set; }
        public int CantidadBoletos { get; set; }
        public int IDSeleccionTemporal { get; set; }

        // La fila es string para admitir números y letras.
        public string FilaSeleccionada { get; set; } = string.Empty;

        // Datos del operador y cliente.
        public int NominaOperador { get; set; }
        public string RFCCliente { get; set; } = string.Empty;
        public int IDEstadio { get; set; }

        // Datos del administrador y emisor.
        public int NominaAdmin { get; set; }
        public string RFCEmisor { get; set; } = "EPC261105AB3";

        // Precio base de cada asiento.
        private decimal precioBase;

        // Precio total de los boletos.
        private decimal precioTotal;

        // Constructor del formulario.
        public mostrar_conf_compra()
        {
            InitializeComponent();
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void mostrar_conf_compra_Load(object sender, EventArgs e)
        {
            CargarInformacionCompra();
        }

        // Consulta y muestra los datos de la selección temporal.
        private void CargarInformacionCompra()
        {
            if (IDSeleccionTemporal <= 0)
            {
                MessageBox.Show(
                    "No se recibió el identificador de la selección.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (NumeroPartido <= 0 || IDZonaSeleccionada <= 0)
            {
                MessageBox.Show(
                    "No se recibió el partido o la zona de la compra.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // Consulta los datos a través de ConexionBD.
                DataTable resultado =
                    ConexionBD.ObtenerResumenSeleccion(
                        IDSeleccionTemporal);

                if (resultado.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron datos para esta selección.",
                        "Sin información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Comprueba que la cantidad de registros coincida
                // con la cantidad de boletos solicitada.
                if (resultado.Rows.Count != CantidadBoletos)
                {
                    MessageBox.Show(
                        "La cantidad de asientos registrados no coincide " +
                        "con la cantidad de boletos solicitada.",
                        "Selección incompleta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DataRow primerRegistro = resultado.Rows[0];

                // Comprueba que los datos devueltos correspondan
                // al partido y zona que recibió este formulario.
                int partidoConsultado =
                    Convert.ToInt32(primerRegistro["Numero_partido"]);

                int zonaConsultada =
                    Convert.ToInt32(primerRegistro["ID_Zona"]);

                int cantidadConsultada =
                    Convert.ToInt32(primerRegistro["Cantidad"]);

                if (partidoConsultado != NumeroPartido ||
                    zonaConsultada != IDZonaSeleccionada ||
                    cantidadConsultada != CantidadBoletos)
                {
                    MessageBox.Show(
                        "Los datos consultados no coinciden con la selección original.",
                        "Datos inconsistentes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Recupera la información general de la compra.
                string nombreZona =
                    Convert.ToString(primerRegistro["Nombre_Zona"])
                    ?? string.Empty;

                string equipoLocal =
                    Convert.ToString(primerRegistro["Equipo_Local"])
                    ?? string.Empty;

                string equipoVisitante =
                    Convert.ToString(primerRegistro["Equipo_Visitante"])
                    ?? string.Empty;

                DateTime fechaPartido =
                    Convert.ToDateTime(primerRegistro["Fecha"]);

                string horaPartido =
                    Convert.ToString(primerRegistro["Hora"])
                    ?? string.Empty;

                precioBase =
                    Convert.ToDecimal(primerRegistro["Precio_Base"]);

                // Recupera los asientos y comprueba que todos
                // correspondan a la misma fila seleccionada.
                List<int> asientos = new List<int>();

                foreach (DataRow registro in resultado.Rows)
                {
                    string filaRegistro =
                        Convert.ToString(registro["Fila_Asiento"])
                        ?? string.Empty;

                    if (!string.Equals(
                        filaRegistro,
                        FilaSeleccionada,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show(
                            "Los asientos consultados no corresponden " +
                            "a la fila seleccionada.",
                            "Fila incorrecta",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    asientos.Add(
                        Convert.ToInt32(registro["Num_Asiento"]));
                }

                // Calcula el total de los boletos.
                precioTotal = precioBase * asientos.Count;

                // Muestra el nombre de la zona.
                nom_d_zona.Text = nombreZona;

                // Muestra la fila, que puede ser numérica o alfabética.
                nom_d_fila.Text = FilaSeleccionada;

                // Muestra los números de asiento separados por comas.
                asientos.Sort();

                num_asiento.Text =
                    string.Join(", ", asientos);

                // Muestra partido, fecha y hora.
                nom_partido.Text =
                    equipoLocal + " vs. " + equipoVisitante +
                    " | " + fechaPartido.ToString("dd/MM/yyyy") +
                    " | " + horaPartido;

                // Muestra el precio individual.
                precio_base_asiento.Text =
                    precioBase.ToString("C2");

                // Muestra el total de la selección.
                precio_base_total.Text =
                    precioTotal.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la información de la compra:\n" +
                    ex.Message,
                    "Error de consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Regresa a la pantalla de selección de asientos.
        private void regresar_asientos_disp_Click(
            object sender, EventArgs e)
        {
            selec_asientos_disp pantalla =
                new selec_asientos_disp();

            pantalla.NumeroPartido = NumeroPartido;
            pantalla.IDZonaSeleccionada = IDZonaSeleccionada;
            pantalla.CantidadBoletos = CantidadBoletos;
            pantalla.IDSeleccionTemporal = IDSeleccionTemporal;
            pantalla.FilaSeleccionada = FilaSeleccionada;

            pantalla.NominaOperador = NominaOperador;
            pantalla.RFCCliente = RFCCliente;
            pantalla.IDEstadio = IDEstadio;
            pantalla.NominaAdmin = NominaAdmin;
            pantalla.RFCEmisor = RFCEmisor;

            pantalla.Show();
            this.Hide();
        }

        // Continúa al formulario de vista previa y pago.
        private void aceptar_conf_compra_Click(
            object sender, EventArgs e)
        {
            if (IDSeleccionTemporal <= 0)
            {
                MessageBox.Show(
                    "No se recibió una selección válida.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(RFCCliente))
            {
                MessageBox.Show(
                    "No se recibió el RFC del cliente.",
                    "RFC faltante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (precioTotal <= 0)
            {
                MessageBox.Show(
                    "El total de la compra no es válido. " +
                    "Vuelve a consultar la selección.",
                    "Total incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                ticket_d_compra_p_pagar ticket =
                    new ticket_d_compra_p_pagar();

                ticket.IDSeleccionTemporal = IDSeleccionTemporal;
                ticket.RFCCliente = RFCCliente.Trim();
                ticket.NominaOperador = NominaOperador;
                ticket.NominaAdmin = NominaAdmin;

                ticket.RFCEmisor =
                    string.IsNullOrWhiteSpace(RFCEmisor)
                    ? "EPC261105AB3"
                    : RFCEmisor.Trim();

                ticket.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo abrir el ticket:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Eventos Click de los controles visuales.
        // Se conservan para no romper las conexiones del diseñador.

        private void nom_d_zona_Click(object sender, EventArgs e) { }

        private void nom_d_fila_Click(object sender, EventArgs e) { }

        private void num_asiento_Click(object sender, EventArgs e) { }

        private void nom_partido_Click(object sender, EventArgs e) { }

        private void precio_base_asiento_Click(object sender, EventArgs e) { }

        private void precio_base_total_Click(object sender, EventArgs e) { }

        private void etiqueta_indica_zona_Click(object sender, EventArgs e) { }

        private void etiqueta_indica_fila_Click(object sender, EventArgs e) { }

        private void etiqueta_indica_asiento_Click(object sender, EventArgs e) { }

        private void etiqueta_indica_partido_Click(object sender, EventArgs e) { }

        private void etiqueta_indica_pbase_asient_Click(object sender, EventArgs e) { }

        private void etiqueta_indica_pbase_total_Click(object sender, EventArgs e) { }
    }
}
