using System;
using System.Linq;
using System.Windows.Forms;
using PIA_PA.Entidades.Partido;
using PIA_PA.Entidades.Equipos;
using PIA_PA.Entidades.Estadio;

namespace PIA_PA
{
    public partial class ModificarPartido : UserControl
    {
        private readonly IPartidoService _partidos;
        private readonly IEquiposService _equipos;
        private readonly IEstadioService _estadios;
        private Partido? _seleccionado;

        public ModificarPartido(IPartidoService partidos, IEquiposService equipos, IEstadioService estadios)
        {
            InitializeComponent();
            _partidos = partidos; _equipos = equipos; _estadios = estadios;
            CargarCatalogos();
            btnBuscar.Click += Buscar_Click;
            btnModificar.Click += Modificar_Click;
        }

        private void CargarCatalogos()
        {
            cBoxGpoLocal.DataSource = _equipos.ObtenerTodos().ToList();
            cBoxGpoLocal.DisplayMember = nameof(Equipos.Equipo_Nombre); cBoxGpoLocal.ValueMember = nameof(Equipos.Equipo_ID);
            cBoxGpoVisitante.DataSource = _equipos.ObtenerTodos().ToList();
            cBoxGpoVisitante.DisplayMember = nameof(Equipos.Equipo_Nombre); cBoxGpoVisitante.ValueMember = nameof(Equipos.Equipo_ID);
            cBoxEstadio.DataSource = _estadios.ObtenerTodos();
            cBoxEstadio.DisplayMember = nameof(Estadio.Estadio_Nombre); cBoxEstadio.ValueMember = nameof(Estadio.Estadio_ID);
        }

        private void Buscar_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtID1.Text.Trim(), out int id) || id <= 0)
            { MessageBox.Show("Escribe un ID de partido válido."); return; }
            _seleccionado = _partidos.ObtenerPorId(id);
            if (_seleccionado == null) { MessageBox.Show("No se encontró el partido."); return; }
            dtpFecha.Value = _seleccionado.Partido_Fecha.ToDateTime(TimeOnly.MinValue);
            txtHora.Text = _seleccionado.Partido_Hora.ToString("HH:mm");
            cBoxGpoLocal.SelectedValue = _seleccionado.Equipo_ID_Local;
            cBoxGpoVisitante.SelectedValue = _seleccionado.Equipo_ID_Visitante;
            cBoxEstadio.SelectedValue = _seleccionado.Estadio_ID;
            txtID1.ReadOnly = true;
        }

        private void Modificar_Click(object? sender, EventArgs e)
        {
            if (_seleccionado == null) { MessageBox.Show("Primero busca un partido."); return; }
            if (!TimeOnly.TryParse(txtHora.Text.Trim(), out TimeOnly hora)) { MessageBox.Show("La hora debe tener formato HH:mm."); return; }
            int local = Convert.ToInt32(cBoxGpoLocal.SelectedValue);
            int visitante = Convert.ToInt32(cBoxGpoVisitante.SelectedValue);
            if (local == visitante) { MessageBox.Show("Los equipos deben ser diferentes."); return; }
            try
            {
                _seleccionado.Partido_Fecha = DateOnly.FromDateTime(dtpFecha.Value.Date);
                _seleccionado.Partido_Hora = hora;
                _seleccionado.Equipo_ID_Local = local;
                _seleccionado.Equipo_ID_Visitante = visitante;
                _seleccionado.Estadio_ID = Convert.ToInt32(cBoxEstadio.SelectedValue);
                _partidos.Actualizar(_seleccionado);
                MessageBox.Show("Partido modificado correctamente.");
            }
            catch (Exception ex) { MessageBox.Show("No se pudo modificar: " + ex.Message); }
        }
    }
}
