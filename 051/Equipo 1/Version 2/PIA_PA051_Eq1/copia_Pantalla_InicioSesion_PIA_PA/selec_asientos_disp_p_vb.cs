
using System;
using System.Data;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    // Formulario que permite consultar y seleccionar asientos disponibles.
    public partial class selec_asientos_disp : Form
    {
        // Datos de la compra.
        public int NumeroPartido { get; set; }
        public int IDZonaSeleccionada { get; set; }
        public int CantidadBoletos { get; set; }
        public int IDSeleccionTemporal { get; set; }

        // La fila es string para admitir números y letras.
        public string FilaSeleccionada { get; set; } = string.Empty;

        // Datos del cliente, operador y estadio.
        public int NominaOperador { get; set; }
        public string RFCCliente { get; set; } = string.Empty;
        public int IDEstadio { get; set; }
        public int NominaAdmin { get; set; }
        public string RFCEmisor { get; set; } = "EPC261105AB3";

        // Indica si el programa está actualizando la lista de asientos.
        private bool actualizandoLista = false;

        // Representa un asiento dentro de la lista visual.
        private class AsientoItem
        {
            public int Numero { get; set; }

            public override string ToString()
            {
                return "Asiento " + Numero;
            }
        }

        // Constructor del formulario.
        public selec_asientos_disp()
        {
            InitializeComponent();
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void selec_asientos_disp_Load(object sender, EventArgs e)
        {
            lista_d_asientos_disp.CheckOnClick = true;
            lista_d_asientos_disp.SelectionMode = SelectionMode.One;

            lista_d_asientos_disp.ItemCheck -=
                lista_d_asientos_disp_ItemCheck;

            lista_d_asientos_disp.ItemCheck +=
                lista_d_asientos_disp_ItemCheck;

            // Comprueba que los datos recibidos sean válidos.
            if (IDZonaSeleccionada <= 0 ||
                string.IsNullOrWhiteSpace(FilaSeleccionada) ||
                CantidadBoletos < 1 ||
                CantidadBoletos > 4 ||
                IDSeleccionTemporal <= 0 ||
                NumeroPartido <= 0)
            {
                MessageBox.Show(
                    "Faltan datos de la selección anterior. " +
                    "Regresa y selecciona el partido, la zona, " +
                    "la cantidad de boletos y la fila.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            etiqueta_nom_fila_disp_selec.Text =
                "Fila seleccionada: " + FilaSeleccionada;

            CargarAsientos();
        }

        // Consulta los asientos de la zona y fila seleccionadas.
        private void CargarAsientos()
        {
            try
            {
                // Consulta los asientos mediante ConexionBD.
                DataTable resultado =
                    ConexionBD.ObtenerAsientosDisponibles(
                        NumeroPartido,
                        IDZonaSeleccionada,
                        FilaSeleccionada);

                actualizandoLista = true;

                lista_d_asientos_disp.Items.Clear();

                foreach (DataRow registro in resultado.Rows)
                {
                    lista_d_asientos_disp.Items.Add(
                        new AsientoItem
                        {
                            Numero = Convert.ToInt32(
                                registro["Num_Asiento"])
                        },
                        false);
                }

                actualizandoLista = false;

                ActualizarContador();

                if (resultado.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay asientos disponibles para la fila seleccionada.",
                        "Sin asientos disponibles",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                actualizandoLista = false;

                MessageBox.Show(
                    "No se pudieron consultar los asientos:\n" +
                    ex.Message,
                    "Error de consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Actualiza la etiqueta que indica cuántos asientos están marcados.
        private void ActualizarContador()
        {
            etiqueta_asientos_disp.Text =
                "Asientos marcados: " +
                lista_d_asientos_disp.CheckedItems.Count +
                " de " + CantidadBoletos;
        }

        // Evento que se ejecuta cuando el usuario marca o desmarca un asiento.
        private void lista_d_asientos_disp_ItemCheck(
            object sender, ItemCheckEventArgs e)
        {
            if (actualizandoLista)
                return;

            int marcados =
                lista_d_asientos_disp.CheckedItems.Count;

            if (e.CurrentValue != CheckState.Checked &&
                e.NewValue == CheckState.Checked)
            {
                marcados++;
            }
            else if (e.CurrentValue == CheckState.Checked &&
                     e.NewValue != CheckState.Checked)
            {
                marcados--;
            }

            if (marcados > CantidadBoletos)
            {
                e.NewValue = CheckState.Unchecked;

                MessageBox.Show(
                    "Solo puedes marcar " + CantidadBoletos +
                    " asiento(s).",
                    "Límite de asientos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (IsHandleCreated)
            {
                BeginInvoke(new Action(ActualizarContador));
            }
        }

        // Evento que se ejecuta cuando cambia el elemento seleccionado.
        private void lista_d_asientos_disp_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            ActualizarContador();
        }

        // Regresa a la pantalla de selección de filas.
        private void regresar_a_filas_disp_Click(
            object sender, EventArgs e)
        {
            selec_fila_disp pantalla =
                new selec_fila_disp();

            pantalla.NumeroPartido = NumeroPartido;
            pantalla.IDZonaSeleccionada = IDZonaSeleccionada;
            pantalla.CantidadBoletos = CantidadBoletos;
            pantalla.IDSeleccionTemporal = IDSeleccionTemporal;

            pantalla.NominaOperador = NominaOperador;
            pantalla.RFCCliente = RFCCliente;
            pantalla.IDEstadio = IDEstadio;
            pantalla.NominaAdmin = NominaAdmin;
            pantalla.RFCEmisor = RFCEmisor;

            pantalla.Show();
            this.Hide();
        }

        // Guarda los asientos y continúa a la confirmación de compra.
        private void guardar_p_ir_confirm_compra_Click(
            object sender, EventArgs e)
        {
            if (lista_d_asientos_disp.CheckedItems.Count != CantidadBoletos)
            {
                MessageBox.Show(
                    "Debes marcar exactamente " + CantidadBoletos +
                    " asiento(s).",
                    "Cantidad incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(RFCCliente))
            {
                MessageBox.Show(
                    "No se recibió el RFC del cliente. " +
                    "Regresa e ingrésalo nuevamente.",
                    "RFC faltante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!GuardarAsientos())
                return;

            mostrar_conf_compra pantalla =
                new mostrar_conf_compra();

            pantalla.NumeroPartido = NumeroPartido;
            pantalla.IDZonaSeleccionada = IDZonaSeleccionada;
            pantalla.CantidadBoletos = CantidadBoletos;
            pantalla.IDSeleccionTemporal = IDSeleccionTemporal;
            pantalla.FilaSeleccionada = FilaSeleccionada;

            pantalla.NominaOperador = NominaOperador;
            pantalla.RFCCliente = RFCCliente;
            pantalla.IDEstadio = IDEstadio;
            pantalla.NominaAdmin = NominaAdmin;

            pantalla.RFCEmisor =
                string.IsNullOrWhiteSpace(RFCEmisor)
                ? "EPC261105AB3"
                : RFCEmisor.Trim();

            pantalla.Show();
            this.Hide();
        }

        // Guarda los asientos seleccionados en la base de datos.
        private bool GuardarAsientos()
        {
            try
            {
                // Crea la tabla en memoria que requiere el parámetro tipo tabla.
                DataTable tablaAsientos = new DataTable();

                tablaAsientos.Columns.Add(
                    "Num_Asiento", typeof(int));

                foreach (AsientoItem asiento
                    in lista_d_asientos_disp.CheckedItems)
                {
                    tablaAsientos.Rows.Add(asiento.Numero);
                }

                // La operación SQL se realiza desde ConexionBD.
                int asientosGuardados =
                    ConexionBD.GuardarAsientosSeleccionados(
                        IDSeleccionTemporal,
                        NumeroPartido,
                        IDZonaSeleccionada,
                        CantidadBoletos,
                        FilaSeleccionada,
                        tablaAsientos);

                if (asientosGuardados != CantidadBoletos)
                {
                    throw new Exception(
                        "La cantidad de asientos guardados no coincide con la cantidad solicitada.");
                }

                MessageBox.Show(
                    "Los asientos se guardaron correctamente.",
                    "Selección guardada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron guardar los asientos:\n" +
                    ex.Message,
                    "Error al guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        // Evento Click de la etiqueta que muestra el contador de asientos.
        private void etiqueta_asientos_disp_Click(object sender, EventArgs e)
        {
        }

        // Evento Click de la etiqueta que muestra la fila seleccionada.
        private void etiqueta_nom_fila_disp_selec_Click(
            object sender, EventArgs e)
        {
        }
    }
}
