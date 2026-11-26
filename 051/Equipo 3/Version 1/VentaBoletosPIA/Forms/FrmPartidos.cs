using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

/// <summary>Alta y consulta de partidos (solo administrador).</summary>
public partial class FrmPartidos : Form
{
    private readonly IPartidoService _partidos;
    private readonly IEstadioService _estadios;

    public FrmPartidos(IPartidoService partidos, IEstadioService estadios)
    {
        _partidos = partidos;
        _estadios = estadios;

        InitializeComponent();
        Ui.Estilizar(dgvPartidos);

        dtpFecha.MinDate = DateTime.Now.Date;
        dtpFecha.Value = DateTime.Now.Date.AddDays(7).AddHours(20);

        Load += (_, _) =>
        {
            Ui.Ejecutar(this, () =>
                Ui.LlenarCombo(cmbEstadio, _estadios.ObtenerEstadios(), nameof(Estadio.Nombre), nameof(Estadio.IdEstadio)));
            Recargar();
        };
    }

    private void Recargar() =>
        Ui.Ejecutar(this, () => dgvPartidos.DataSource = _partidos.ObtenerPartidos(null, null, null, null));

    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        Partido partido = new()
        {
            EquipoLocal = txtLocal.Text,
            EquipoVisitante = txtVisitante.Text,
            FechaHora = dtpFecha.Value,
            IdEstadio = cmbEstadio.SelectedValue is int id ? id : 0
        };

        if (Ui.Ejecutar(this, () => _partidos.CrearPartido(partido)))
        {
            Ui.Aviso(this, "Partido registrado correctamente.");
            txtLocal.Clear();
            txtVisitante.Clear();
            Recargar();
        }
    }
}
