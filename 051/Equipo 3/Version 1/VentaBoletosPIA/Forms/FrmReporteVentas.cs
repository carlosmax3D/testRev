using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Models.Dtos;
using VentaBoletosApp.Services;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

/// <summary>Reporte de ventas por fecha, estadio y partido (solo administrador).</summary>
public partial class FrmReporteVentas : Form
{
    private readonly IReporteService _reportes;
    private readonly IEstadioService _estadios;
    private readonly IPartidoService _partidos;

    public FrmReporteVentas(IReporteService reportes, IEstadioService estadios, IPartidoService partidos)
    {
        _reportes = reportes;
        _estadios = estadios;
        _partidos = partidos;

        InitializeComponent();
        Ui.Estilizar(dgvPartidos);
        Ui.Estilizar(dgvDetalle);

        dtpDesde.Checked = false;
        dtpHasta.Checked = false;

        cmbPartido.Format += (_, e) => e.Value = e.ListItem is Partido p ? Descripciones.DePartido(p) : string.Empty;

        Load += (_, _) =>
        {
            Ui.Ejecutar(this, () =>
            {
                List<Estadio> estadios = [Descripciones.TodosLosEstadios()];
                estadios.AddRange(_estadios.ObtenerEstadios());
                Ui.LlenarCombo(cmbEstadio, estadios, nameof(Estadio.Nombre), nameof(Estadio.IdEstadio));

                List<Partido> partidos = [Descripciones.TodosLosPartidos()];
                partidos.AddRange(_partidos.ObtenerPartidos(null, null, null, null));
                Ui.LlenarCombo(cmbPartido, partidos, nameof(Partido.DescripcionCorta), nameof(Partido.IdPartido));
            });
        };
    }

    private void BtnGenerar_Click(object? sender, EventArgs e)
    {
        DateTime? desde = dtpDesde.Checked ? dtpDesde.Value.Date : null;
        DateTime? hasta = dtpHasta.Checked ? dtpHasta.Value.Date : null;
        int? idEstadio = cmbEstadio.SelectedValue is int e1 && e1 > 0 ? e1 : null;
        int? idPartido = cmbPartido.SelectedValue is int p1 && p1 > 0 ? p1 : null;

        ReporteVentasResultado? resultado = null;

        if (!Ui.Ejecutar(this, () => resultado = _reportes.ReporteVentas(desde, hasta, idEstadio, idPartido)) || resultado is null)
        {
            return;
        }

        dgvDetalle.DataSource = resultado.Detalle;
        dgvPartidos.DataSource = resultado.PorPartido;

        lblTotales.Text =
            $"Boletos vendidos: {resultado.BoletosTotales:N0}     Ingreso total: {Formato.Moneda(resultado.IngresoTotal)}";

        if (resultado.Detalle.Count == 0)
        {
            Ui.Aviso(this, "No hay ventas activas con los criterios seleccionados.");
        }
    }
}
