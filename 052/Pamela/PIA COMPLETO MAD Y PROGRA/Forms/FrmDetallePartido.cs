using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms
{
    /// <summary>Captura de un partido: estadio, fecha y hora, equipos y estado.</summary>
    public partial class FrmDetallePartido : Form
    {
        private readonly IPartidoService _partidoService;
        private readonly IEstadioService _estadioService;
        private readonly ISesionUsuario _sesion;
        private readonly Partido? _partidoAEditar;

        public FrmDetallePartido(
            IPartidoService partidoService,
            IEstadioService estadioService,
            ISesionUsuario sesion,
            Partido? partidoAEditar)
        {
            InitializeComponent();

            _partidoService = partidoService;
            _estadioService = estadioService;
            _sesion = sesion;
            _partidoAEditar = partidoAEditar;

            Text = _partidoAEditar == null ? "Nuevo partido" : "Editar partido";
            txtFehaPartDP.Text = "Fecha del partido:";
            tBEquipoLocDP.MaxLength = 100;
            tBEquipoVisiDP.MaxLength = 100;

            // dateTimePicker1 = fecha, dateTimePicker2 = hora
            UiHelper.ConfigurarFecha(dateTimePicker1);
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "HH:mm";
            dateTimePicker2.ShowUpDown = true;

            cBEsrdPartDP.DropDownStyle = ComboBoxStyle.DropDownList;

            botonGuardDP.Click += botonGuardDP_Click;
            botonCancelDP.Click += botonCancelDP_Click;

            CargarEstadios();

            if (_partidoAEditar != null)
            {
                CargarDatos(_partidoAEditar);
            }
            else
            {
                // Un partido nuevo siempre inicia como Programado
                dateTimePicker1.Value = DateTime.Today.AddDays(7);
                dateTimePicker2.Value = DateTime.Today.AddHours(19);
                cBEsrdPartDP.SelectedItem = "Programado";
                cBEsrdPartDP.Enabled = false;
            }
        }

        private void CargarEstadios()
        {
            List<OpcionCombo> opciones = new List<OpcionCombo>();

            try
            {
                DataTable estadios = _estadioService.ObtenerEstadios();

                foreach (DataRow fila in estadios.Rows)
                {
                    opciones.Add(new OpcionCombo
                    {
                        Id = DatosHelper.Entero(fila, "IdEstadio"),
                        Texto = DatosHelper.Texto(fila, "Nombre") + " (" + DatosHelper.Texto(fila, "Ciudad") + ")"
                    });
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al cargar los estadios");
            }

            UiHelper.LlenarCombo(cBNomEstdDP, opciones);
        }

        private void CargarDatos(Partido partido)
        {
            foreach (object? elemento in cBNomEstdDP.Items)
            {
                OpcionCombo? opcion = elemento as OpcionCombo;

                if (opcion != null && opcion.Id == partido.IdEstadio)
                {
                    cBNomEstdDP.SelectedItem = opcion;
                    break;
                }
            }

            dateTimePicker1.Value = partido.FechaHora.Date;
            dateTimePicker2.Value = DateTime.Today.Add(partido.FechaHora.TimeOfDay);
            tBEquipoLocDP.Text = partido.EquipoLocal;
            tBEquipoVisiDP.Text = partido.EquipoVisitante;
            cBEsrdPartDP.SelectedItem = partido.Estado;
        }

        private void botonGuardDP_Click(object? sender, EventArgs e)
        {
            int? idEstadio = UiHelper.IdSeleccionado(cBNomEstdDP);

            // Ningún campo vacío
            if (idEstadio == null)
            {
                UiHelper.Advertir("Selecciona el estadio. Si la lista está vacía, primero registra un estadio.");
                cBNomEstdDP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tBEquipoLocDP.Text))
            {
                UiHelper.Advertir("Escribe el equipo local.");
                tBEquipoLocDP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tBEquipoVisiDP.Text))
            {
                UiHelper.Advertir("Escribe el equipo visitante.");
                tBEquipoVisiDP.Focus();
                return;
            }

            if (cBEsrdPartDP.SelectedItem == null)
            {
                UiHelper.Advertir("Selecciona el estado del partido.");
                cBEsrdPartDP.Focus();
                return;
            }

            // Fecha del primer selector + hora y minutos del segundo
            DateTime fechaHora = dateTimePicker1.Value.Date
                .AddHours(dateTimePicker2.Value.Hour)
                .AddMinutes(dateTimePicker2.Value.Minute);

            bool esEdicion = _partidoAEditar != null;
            string estado = cBEsrdPartDP.SelectedItem.ToString() ?? "Programado";

            // No registrar partidos en fechas en el pasado
            if (!esEdicion && fechaHora <= DateTime.Now)
            {
                UiHelper.Advertir("No se pueden registrar partidos en una fecha y hora que ya pasaron.");
                dateTimePicker1.Focus();
                return;
            }

            if (esEdicion)
            {
                string pregunta = estado == "Cancelado"
                    ? "Al cancelar el partido se cancelan también todas sus ventas y facturas, " +
                      "y ya no se podrá modificar.\n\n¿Deseas cancelar el partido?"
                    : "¿Deseas guardar los cambios del partido?";

                if (!UiHelper.Confirmar(pregunta, "Confirmar cambios"))
                {
                    return;
                }
            }

            try
            {
                Partido partido = new Partido
                {
                    IdPartido = _partidoAEditar?.IdPartido ?? 0,
                    IdEstadio = idEstadio.Value,
                    FechaHora = fechaHora,
                    EquipoLocal = tBEquipoLocDP.Text,
                    EquipoVisitante = tBEquipoVisiDP.Text,
                    Estado = estado
                };

                if (esEdicion)
                {
                    _partidoService.ActualizarPartido(partido, _sesion.IdUsuario);

                    UiHelper.Informar("Los cambios del partido se guardaron correctamente.", "Partidos");
                }
                else
                {
                    _partidoService.RegistrarPartido(partido, _sesion.IdUsuario);

                    UiHelper.Informar("Partido registrado correctamente.", "Partidos");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, esEdicion ? "No se pudo actualizar el partido" : "No se pudo registrar el partido");
            }
        }

        private void botonCancelDP_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
