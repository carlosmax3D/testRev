using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Ventas vigentes por rango de fechas, estadio y partido, agrupadas por partido
public partial class FrmReporteVentas : Form
{
    private const string OpcionTodos = "Todos";

    private readonly IReporteService _reportes;

    // Todos los partidos; el combo solo muestra los del estadio elegido
    private List<Partido> _listaPartidos = [];

    public FrmReporteVentas(IReporteService reportes)
    {
        InitializeComponent();
        _reportes = reportes;
        dgvVentas.AutoGenerateColumns = false;
        dgvTotales.AutoGenerateColumns = false;
        colSubtotal.DefaultCellStyle.FormatProvider = Formato.Cultura;
        colTotal.DefaultCellStyle.FormatProvider = Formato.Cultura;
        colTotSubtotal.DefaultCellStyle.FormatProvider = Formato.Cultura;
        colTotTotal.DefaultCellStyle.FormatProvider = Formato.Cultura;
        Portapapeles.HabilitarCopiaFolio(dgvVentas, colFolio);
        ReiniciarFechas();
        MostrarTotales(new ReporteVentas());
    }

    private void ReiniciarFechas()
    {
        dtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        dtpHasta.Value = DateTime.Today;
    }

    private void CargarFiltros()
    {
        List<Estadio> estadios = [];
        try
        {
            estadios = _reportes.ObtenerEstadiosParaFiltro();
            _listaPartidos = _reportes.ObtenerPartidosParaFiltro();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }

        cboEstadio.Items.Clear();
        cboEstadio.Items.Add(OpcionTodos);
        foreach (Estadio estadio in estadios)
        {
            cboEstadio.Items.Add(estadio);
        }

        // Dispara cboEstadio_SelectedIndexChanged, que llena los partidos
        cboEstadio.SelectedIndex = 0;
    }

    private void LlenarPartidos()
    {
        Estadio? estadio = cboEstadio.SelectedItem as Estadio;

        cboPartido.Items.Clear();
        cboPartido.Items.Add(OpcionTodos);
        foreach (Partido partido in _listaPartidos)
        {
            if (estadio is null || partido.IdEstadio == estadio.IdEstadio)
            {
                cboPartido.Items.Add(partido);
            }
        }

        cboPartido.SelectedIndex = 0;
    }

    private void MostrarTotales(ReporteVentas reporte)
    {
        lblIngreso.Text = $"Ingreso total: {Formato.Moneda(reporte.IngresoTotal)}";
        lblBoletos.Text = $"Boletos vendidos: {reporte.BoletosVendidos:N0}";
    }

    private void Consultar(object? sender, EventArgs e)
    {
        try
        {
            ReporteVentas reporte = _reportes.ObtenerReporteVentas(
                dtpDesde.Value,
                dtpHasta.Value,
                (cboEstadio.SelectedItem as Estadio)?.IdEstadio,
                (cboPartido.SelectedItem as Partido)?.IdPartido);

            dgvVentas.DataSource = reporte.Ventas;
            dgvTotales.DataSource = reporte.Partidos;
            MostrarTotales(reporte);

            if (reporte.Ventas.Count == 0)
            {
                Mensajes.Informacion("No hay ventas con esos filtros.");
            }
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Limpiar(object? sender, EventArgs e)
    {
        ReiniciarFechas();
        if (cboEstadio.Items.Count > 0)
        {
            cboEstadio.SelectedIndex = 0;
        }

        dgvVentas.DataSource = null;
        dgvTotales.DataSource = null;
        MostrarTotales(new ReporteVentas());
    }

    private void cboEstadio_SelectedIndexChanged(object? sender, EventArgs e)
    {
        LlenarPartidos();
    }

    private void FrmReporteVentas_Load(object? sender, EventArgs e)
    {
        CargarFiltros();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
