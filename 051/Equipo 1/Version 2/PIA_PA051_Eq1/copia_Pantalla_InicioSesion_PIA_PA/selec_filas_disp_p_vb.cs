
using System;
using System.Data;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    // Formulario donde se muestran las filas disponibles de la zona seleccionada.
    public partial class selec_fila_disp : Form
    {
        // Datos de la compra recibidos de la pantalla anterior.
        public int NumeroPartido { get; set; }
        public int IDZonaSeleccionada { get; set; }
        public int CantidadBoletos { get; set; }
        public int IDSeleccionTemporal { get; set; }

        // Identidad del operativo y del cliente.
        public int NominaOperador { get; set; }
        public string RFCCliente { get; set; } = string.Empty;
        public int IDEstadio { get; set; }

        // Datos que deben conservarse durante el proceso de compra.
        public int NominaAdmin { get; set; }
        public string RFCEmisor { get; set; } = "EPC261105AB3";

        // Representa una fila disponible.
        private class FilaItem
        {
            public string NumeroFila { get; set; } = string.Empty;

            public override string ToString()
            {
                return "Fila " + NumeroFila;
            }
        }

        // Constructor del formulario.
        public selec_fila_disp()
        {
            InitializeComponent();
        }

        // Cargar las filas al abrir la pantalla.
        private void selec_fila_disp_Load(object sender, EventArgs e)
        {
            CargarFilas();
        }

        // Consultar las filas disponibles en la zona y partido seleccionados.
        private void CargarFilas()
        {
            try
            {
                if (NumeroPartido <= 0 || IDZonaSeleccionada <= 0)
                {
                    MessageBox.Show(
                        "No se recibió un partido o una zona válida. Regresa y selecciona nuevamente.",
                        "Datos incompletos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Consulta las filas mediante la clase centralizada.
                DataTable resultado =
                    ConexionBD.ObtenerFilasDisponibles(
                        NumeroPartido,
                        IDZonaSeleccionada);

                // Convierte los resultados en elementos para el ComboBox.
                selec_1_fila_disp.DataSource = null;
                selec_1_fila_disp.Items.Clear();

                foreach (DataRow registro in resultado.Rows)
                {
                    selec_1_fila_disp.Items.Add(
                        new FilaItem
                        {
                            NumeroFila =
                                Convert.ToString(registro["Fila_Asiento"])
                                ?? string.Empty
                        });
                }

                selec_1_fila_disp.SelectedIndex = -1;

                if (resultado.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay filas con asientos disponibles para este partido y zona.",
                        "Sin filas disponibles",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar las filas:\n" + ex.Message,
                    "Error de consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Se ejecuta cuando cambia la fila seleccionada en el ComboBox.
        private void selec_1_fila_disp_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            // La fila elegida se procesa al continuar.
        }

        // Regresar a cantidad de boletos conservando los datos.
        private void regresar_a_cant_boleto_Click(
            object sender, EventArgs e)
        {
            selec_cant_boletos pantalla =
                new selec_cant_boletos();

            pantalla.NumeroPartido = NumeroPartido;
            pantalla.IDZonaSeleccionada = IDZonaSeleccionada;
            pantalla.CantidadBoletos = CantidadBoletos;
            pantalla.NominaOperador = NominaOperador;
            pantalla.RFCCliente = RFCCliente;
            pantalla.IDEstadio = IDEstadio;

            pantalla.Show();
            this.Hide();
        }

        // Enviar los datos a la selección de asientos.
        private void sig_p_selec_asiento_Click(
            object sender, EventArgs e)
        {
            if (selec_1_fila_disp.SelectedItem == null)
            {
                MessageBox.Show(
                    "Selecciona una fila antes de continuar.",
                    "Selección requerida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (IDSeleccionTemporal <= 0)
            {
                MessageBox.Show(
                    "No se recibió el identificador de la selección temporal.",
                    "Faltan datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (CantidadBoletos < 1 || CantidadBoletos > 4)
            {
                MessageBox.Show(
                    "La cantidad de boletos no es válida.",
                    "Cantidad incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            FilaItem filaSeleccionada =
                (FilaItem)selec_1_fila_disp.SelectedItem;

            selec_asientos_disp pantalla =
                new selec_asientos_disp();

            pantalla.NumeroPartido = NumeroPartido;
            pantalla.IDZonaSeleccionada = IDZonaSeleccionada;
            pantalla.CantidadBoletos = CantidadBoletos;
            pantalla.IDSeleccionTemporal = IDSeleccionTemporal;

            // La fila se transmite como texto para admitir A, B y números.
            pantalla.FilaSeleccionada = filaSeleccionada.NumeroFila;

            pantalla.NominaOperador = NominaOperador;
            pantalla.RFCCliente = RFCCliente;
            pantalla.IDEstadio = IDEstadio;
            pantalla.NominaAdmin = NominaAdmin;
            pantalla.RFCEmisor = RFCEmisor;

            pantalla.Show();
            this.Hide();
        }
    }
}
