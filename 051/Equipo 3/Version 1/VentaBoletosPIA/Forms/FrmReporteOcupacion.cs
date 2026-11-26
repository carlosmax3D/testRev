using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Models.Dtos;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

/// <summary>Reporte de ocupacion y disponibilidad por partido y zona (solo administrador).</summary>
public partial class FrmReporteOcupacion : Form
{
    private readonly IReporteService _reportes;
    private readonly IPartidoService _partidos;
    private readonly IEstadioService _estadios;

    public FrmReporteOcupacion(IReporteService reportes, IPartidoService partidos, IEstadioService estadios)
    {
        _reportes = reportes;
        _partidos = partidos;
        _estadios = estadios;

        InitializeComponent();
        Ui.Estilizar(dgvOcupacion);

        cmbPartido.Format += (_, e) => e.Value = e.ListItem is Partido p ? Descripciones.DePartido(p) : string.Empty;

        Load += (_, _) =>
        {
            Ui.Ejecutar(this, () =>
            {
                List<Partido> partidos = [Descripciones.TodosLosPartidos()];
                partidos.AddRange(_partidos.ObtenerPartidos(null, null, null, null));
                Ui.LlenarCombo(cmbPartido, partidos, nameof(Partido.DescripcionCorta), nameof(Partido.IdPartido));
            });
            CargarZonas();
        };
    }

    private void CmbPartido_SelectedIndexChanged(object? sender, EventArgs e) => CargarZonas();

    private void CargarZonas()
    {
        List<Zona> zonas = [Descripciones.TodasLasZonas()];

        if (cmbPartido.SelectedItem is Partido { IdPartido: > 0 } partido)
        {
            Ui.Ejecutar(this, () => zonas.AddRange(_estadios.ObtenerZonas(partido.IdEstadio)));
        }

        Ui.LlenarCombo(cmbZona, zonas, nameof(Zona.NombreZona), nameof(Zona.IdZona));
    }

    private void BtnGenerar_Click(object? sender, EventArgs e)
    {
        int? idPartido = cmbPartido.SelectedValue is int p && p > 0 ? p : null;
        int? idZona = cmbZona.SelectedValue is int z && z > 0 ? z : null;

        ReporteOcupacionResultado? resultado = null;

        if (!Ui.Ejecutar(this, () => resultado = _reportes.ReporteOcupacion(idPartido, idZona, chkDisponibles.Checked)) || resultado is null)
        {
            return;
        }

        dgvOcupacion.DataSource = resultado.Items;
        lblTotal.Text = $"Boletos vendidos (total mostrado): {resultado.BoletosVendidosTotal:N0}";

        if (resultado.Items.Count == 0)
        {
            Ui.Aviso(this, "No hay información con los criterios seleccionados.");
        }
    }
}
