using System;
using System.Windows.Forms;
using PIA_PA.Entidades.Partido;
using PIA_PA.Entidades.Equipos;
using PIA_PA.Entidades.Estadio;

namespace PIA_PA.Menus
{
    public partial class CancelarPartido : UserControl
    {
        private readonly IPartidoService _partidos;
        private Partido? _seleccionado;

        public CancelarPartido(IPartidoService partidos)
        {
            InitializeComponent();
            _partidos = partidos;
            txtBuscar.Click += Buscar_Click;
            btnCancelar.Click += Cancelar_Click;
            foreach (Control c in new Control[] { txtID2, txtHora, txtEstadio, txtGpoLocal, txtGpoVisitante }) 
                if (c is TextBox tb) tb.ReadOnly = true;
        }

        private void Buscar_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtID1.Text.Trim(), out int id) || id <= 0)
            { MessageBox.Show("Escribe un ID de partido válido."); return; }
            _seleccionado = _partidos.ObtenerPorId(id);
            if (_seleccionado == null) { MessageBox.Show("No se encontró el partido."); return; }
            txtID2.Text = _seleccionado.Partido_ID.ToString();
            txtHora.Text = _seleccionado.Partido_Fecha.ToString("yyyy-MM-dd") + " " + _seleccionado.Partido_Hora.ToString("HH:mm");
            txtEstadio.Text = _seleccionado.Estadio_ID.ToString();
            txtGpoLocal.Text = _seleccionado.Equipo_ID_Local.ToString();
            txtGpoVisitante.Text = _seleccionado.Equipo_ID_Visitante.ToString();
            btnCancelar.Enabled = !string.Equals(_seleccionado.Partido_Estado, "Cancelado", StringComparison.OrdinalIgnoreCase);
        }

        private void Cancelar_Click(object? sender, EventArgs e)
        {
            if (_seleccionado == null) { MessageBox.Show("Primero busca un partido."); return; }
            if (MessageBox.Show("¿Confirmas cancelar este partido?", "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                _seleccionado.Partido_Estado = "Cancelado";
                _partidos.Actualizar(_seleccionado);
                MessageBox.Show("Partido cancelado.");
                btnCancelar.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show("No se pudo cancelar el partido: " + ex.Message); }
        }
    }
}
