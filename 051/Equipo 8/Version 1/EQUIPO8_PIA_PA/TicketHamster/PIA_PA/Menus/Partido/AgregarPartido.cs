using System;
using System.Linq;
using System.Windows.Forms;
using PIA_PA.Entidades.Partido;
using PIA_PA.Entidades.Equipos;
using PIA_PA.Entidades.Estadio;

namespace PIA_PA
{
    public partial class AgregarPartido : UserControl
    {
        private readonly IPartidoService _partidos;
        private readonly IEquiposService _equipos;
        private readonly IEstadioService _estadios;

        public AgregarPartido(IPartidoService partidos, IEquiposService equipos, IEstadioService estadios)
        {
            InitializeComponent();
            _partidos = partidos;
            _equipos = equipos;
            _estadios = estadios;
            CargarCatalogos();
            btnGuardar.Click += btnGuardar_Click;
            txtHora.PlaceholderText = "HH:mm";
        }

        private void CargarCatalogos()
        {
            cBGrupoLocal.DataSource = _equipos.ObtenerTodos().Where(e => !string.Equals(e.Equipo_Estatus, "Inactivo", StringComparison.OrdinalIgnoreCase)).ToList();
            cBGrupoLocal.DisplayMember = nameof(Equipos.Equipo_Nombre);
            cBGrupoLocal.ValueMember = nameof(Equipos.Equipo_ID);
            cBCrupoVisitante.DataSource = _equipos.ObtenerTodos().Where(e => !string.Equals(e.Equipo_Estatus, "Inactivo", StringComparison.OrdinalIgnoreCase)).ToList();
            cBCrupoVisitante.DisplayMember = nameof(Equipos.Equipo_Nombre);
            cBCrupoVisitante.ValueMember = nameof(Equipos.Equipo_ID);
            cBEstadio.DataSource = _estadios.ObtenerTodos();
            cBEstadio.DisplayMember = nameof(Estadio.Estadio_Nombre);
            cBEstadio.ValueMember = nameof(Estadio.Estadio_ID);
            dTFecha.MinDate = DateTime.Today;
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (cBGrupoLocal.SelectedValue == null || cBCrupoVisitante.SelectedValue == null || cBEstadio.SelectedValue == null)
            {
                MessageBox.Show("Selecciona los dos equipos y el estadio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!TimeOnly.TryParse(txtHora.Text.Trim(), out TimeOnly hora))
            {
                MessageBox.Show("La hora debe tener formato HH:mm (por ejemplo, 19:30).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHora.Focus();
                return;
            }
            int local = Convert.ToInt32(cBGrupoLocal.SelectedValue);
            int visitante = Convert.ToInt32(cBCrupoVisitante.SelectedValue);
            if (local == visitante)
            {
                MessageBox.Show("El equipo local y visitante deben ser diferentes.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var existentes = _partidos.ObtenerTodos();
                int id = existentes.Count == 0 ? 1 : existentes.Max(p => p.Partido_ID) + 1;
                _partidos.Insertar(new Partido
                {
                    Partido_ID = id,
                    Partido_Estado = "Programado",
                    Partido_Fecha = DateOnly.FromDateTime(dTFecha.Value.Date),
                    Partido_Hora = hora,
                    Equipo_ID_Local = local,
                    Equipo_ID_Visitante = visitante,
                    Estadio_ID = Convert.ToInt32(cBEstadio.SelectedValue)
                });
                MessageBox.Show("Partido creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHora.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo crear el partido. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
