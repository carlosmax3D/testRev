
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Menu_p_Operativo
{
    // Formulario para seleccionar el partido al que se desea comprar boletos.
    public partial class selec_partido_p_ventb : Form
    {
        // Datos que se conservan durante la compra.
        public int NominaOperador { get; set; }
        public string RFCCliente { get; set; }
        public int IDEstadio { get; set; }

        // Guarda la fecha seleccionada en el calendario anterior.
        public DateTime FechaPartido { get; set; }

        // Recibe los partidos consultados en el formulario anterior.
        public DataTable TablaPartidos { get; set; }

        // Indica si existen partidos disponibles.
        private bool hayPartidosDisponibles = false;

        // Clase que representa un partido.
        private class PartidoItem
        {
            public int Numero_partido { get; set; }
            public string Estado { get; set; }
            public string DescripcionPartido { get; set; }

            public override string ToString()
            {
                return DescripcionPartido;
            }
        }

        // Constructor del formulario.
        public selec_partido_p_ventb()
        {
            InitializeComponent();
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void selec_partido_p_ventb_Load(
            object sender, EventArgs e)
        {
            CargarPartidos();
        }

        // Método que muestra los partidos recibidos del formulario anterior.
        private void CargarPartidos()
        {
            hayPartidosDisponibles = false;
            ok_a_seleccion_partido.Enabled = false;

            opciones_dpartidos_disp.DataSource = null;
            opciones_dpartidos_disp.Items.Clear();

            try
            {
                if (TablaPartidos == null ||
                    TablaPartidos.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se recibieron partidos para mostrar. Regresa a la pantalla anterior y selecciona otra fecha o estadio.",
                        "Sin partidos disponibles",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                // Verificar que la consulta incluya el estado del partido.
                if (!TablaPartidos.Columns.Contains("Estado"))
                {
                    MessageBox.Show(
                        "No se recibió el estado de los partidos. No es posible verificar si están cancelados.",
                        "Información incompleta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                List<PartidoItem> partidos =
                    new List<PartidoItem>();

                foreach (DataRow fila in TablaPartidos.Rows)
                {
                    string fecha =
                        fila["Fecha"] == DBNull.Value
                        ? "Sin fecha"
                        : Convert.ToDateTime(
                            fila["Fecha"]).ToString("dd/MM/yyyy");

                    string hora =
                        fila["Hora"] == DBNull.Value
                        ? "Sin hora"
                        : fila["Hora"].ToString();

                    string estado =
                        fila["Estado"] == DBNull.Value
                        ? "SIN ESTADO"
                        : fila["Estado"].ToString().Trim();

                    string descripcion =
                        fila["Equipo_Local"] + " vs. " +
                        fila["Equipo_Visitante"] +
                        " | Estadio: " +
                        fila["Nombre_Estadio"] +
                        " | Ciudad: " +
                        fila["Ciudad"] +
                        " | Fecha: " + fecha +
                        " | Hora: " + hora +
                        " | Estado: " + estado;

                    partidos.Add(new PartidoItem
                    {
                        Numero_partido =
                            Convert.ToInt32(
                                fila["Numero_partido"]),

                        Estado = estado,

                        DescripcionPartido = descripcion
                    });
                }

                if (partidos.Count == 0)
                {
                    MessageBox.Show(
                        "No hay partidos disponibles para esta selección.",
                        "Sin partidos disponibles",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                opciones_dpartidos_disp.DisplayMember =
                    nameof(PartidoItem.DescripcionPartido);

                opciones_dpartidos_disp.ValueMember =
                    nameof(PartidoItem.Numero_partido);

                opciones_dpartidos_disp.DataSource = partidos;

                opciones_dpartidos_disp.SelectedIndex = -1;

                hayPartidosDisponibles = true;
                ok_a_seleccion_partido.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al mostrar los partidos:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                hayPartidosDisponibles = false;
                ok_a_seleccion_partido.Enabled = false;
            }
        }

        // Evento del botón para regresar a la pantalla anterior.
        private void reg_a_selec_estadio_fecha_Click(
            object sender, EventArgs e)
        {
            // Recupera la misma pantalla de estadio y fecha.
            Form formularioAnterior = this.Owner;

            if (formularioAnterior != null &&
                !formularioAnterior.IsDisposed)
            {
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

        // Evento del botón OK para confirmar el partido seleccionado.
        private void ok_a_seleccion_partido_Click(
            object sender, EventArgs e)
        {
            if (!hayPartidosDisponibles)
            {
                MessageBox.Show(
                    "No puedes continuar porque no existen partidos disponibles para el estadio y la fecha seleccionados.",
                    "No hay partidos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (opciones_dpartidos_disp.SelectedItem == null ||
                opciones_dpartidos_disp.SelectedValue == null)
            {
                MessageBox.Show(
                    "Selecciona un partido antes de continuar.",
                    "Partido no seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Recuperar el partido seleccionado.
            PartidoItem partidoSeleccionado =
                opciones_dpartidos_disp.SelectedItem as PartidoItem;

            if (partidoSeleccionado == null)
            {
                MessageBox.Show(
                    "No se pudo obtener la información del partido seleccionado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Impedir la venta cuando el partido está cancelado.
            if (string.Equals(
                partidoSeleccionado.Estado,
                "CANCELADO",
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "No se puede continuar con el procedimiento de venta.\n\n" +
                    "El partido seleccionado está CANCELADO.\n\n" +
                    "Selecciona otro partido que no esté cancelado.",
                    "Venta no permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Impedir continuar si el partido no tiene estado.
            if (string.IsNullOrWhiteSpace(partidoSeleccionado.Estado) ||
                string.Equals(
                    partidoSeleccionado.Estado,
                    "SIN ESTADO",
                    StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "No se puede continuar con la venta porque el partido no tiene un estado registrado.\n\n" +
                    "Contacta al administrador para verificar la información.",
                    "Estado del partido no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(RFCCliente))
            {
                MessageBox.Show(
                    "No se recibió el RFC del cliente. Regresa e ingrésalo nuevamente.",
                    "RFC faltante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (NominaOperador <= 0)
            {
                MessageBox.Show(
                    "No se recibió la nómina del operativo.",
                    "Nómina faltante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                int numeroPartido =
                    partidoSeleccionado.Numero_partido;

                selec_zona_p_compboleto pantalla =
                    new selec_zona_p_compboleto();

                pantalla.NumeroPartido = numeroPartido;
                pantalla.NominaOperador = NominaOperador;
                pantalla.RFCCliente = RFCCliente;
                pantalla.IDEstadio = IDEstadio;

                // Oculta la pantalla actual y abre selección de zona.
                this.Hide();
                pantalla.Show(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo abrir la selección de zona:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta cuando cambia el elemento seleccionado.
        private void opciones_dpartidos_disp_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            // El partido se procesa al presionar OK.
        }
    }
}
