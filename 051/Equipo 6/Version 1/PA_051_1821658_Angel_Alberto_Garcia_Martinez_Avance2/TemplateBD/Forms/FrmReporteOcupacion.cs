using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Ocupación y disponibilidad de asientos por partido y zona
public partial class FrmReporteOcupacion : Form
{
    private const string OpcionTodos = "Todos";
    private const string OpcionTodas = "Todas";

    private readonly IReporteService _reportes;
    private readonly IPartidoService _partidos;
    private readonly IEstadioService _estadios;

    public FrmReporteOcupacion(IReporteService reportes, IPartidoService partidos, IEstadioService estadios)
    {
        InitializeComponent();
        _reportes = reportes;
        _partidos = partidos;
        _estadios = estadios;
        dgvOcupacion.AutoGenerateColumns = false;
        colPrecioBase.DefaultCellStyle.FormatProvider = Formato.Cultura;
        lblVendidos.Text = string.Empty;
    }

    private void CargarPartidos()
    {
        cboPartido.Items.Clear();
        cboPartido.Items.Add(OpcionTodos);
        try
        {
            // Los partidos eliminados o cancelados no aparecen en el reporte:
            // los primeros no tienen ventas y los segundos las tienen canceladas
            foreach (Partido partido in _partidos.ObtenerPartidos(null))
            {
                if (partido.Activo && partido.Estado != EstadosPartido.Cancelado)
                {
                    cboPartido.Items.Add(partido);
                }
            }
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }

        // Dispara cboPartido_SelectedIndexChanged, que llena las zonas
        cboPartido.SelectedIndex = 0;
    }

    // Las zonas dependen del estadio del partido; con "Todos" solo se puede elegir "Todas"
    private void LlenarZonas()
    {
        cboZona.Items.Clear();
        cboZona.Items.Add(OpcionTodas);

        if (cboPartido.SelectedItem is Partido partido)
        {
            try
            {
                foreach (Zona zona in _estadios.ObtenerZonas(partido.IdEstadio))
                {
                    cboZona.Items.Add(zona);
                }
            }
            catch (Exception ex)
            {
                Mensajes.Aviso(Errores.Mensaje(ex));
            }
        }

        cboZona.SelectedIndex = 0;
        cboZona.Enabled = cboZona.Items.Count > 1;
    }

    private void Consultar(object? sender, EventArgs e)
    {
        try
        {
            Partido? partido = cboPartido.SelectedItem as Partido;
            ReporteOcupacion reporte = _reportes.ObtenerOcupacion(
                partido?.IdPartido,
                (cboZona.SelectedItem as Zona)?.IdZona,
                chkDisponibles.Checked);

            dgvOcupacion.DataSource = reporte.Zonas;
            // El total no depende de los filtros de zona ni de "solo disponibles"
            lblVendidos.Text = partido is null
                ? $"Boletos vendidos en todos los partidos: {reporte.BoletosVendidos:N0}"
                : $"Boletos vendidos del partido: {reporte.BoletosVendidos:N0}";

            if (reporte.Zonas.Count == 0)
            {
                Mensajes.Informacion("No hay información con esos filtros.");
            }
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Limpiar(object? sender, EventArgs e)
    {
        if (cboPartido.Items.Count > 0)
        {
            cboPartido.SelectedIndex = 0;
        }

        chkDisponibles.Checked = false;
        dgvOcupacion.DataSource = null;
        lblVendidos.Text = string.Empty;
    }

    private void cboPartido_SelectedIndexChanged(object? sender, EventArgs e)
    {
        LlenarZonas();
    }

    private void FrmReporteOcupacion_Load(object? sender, EventArgs e)
    {
        CargarPartidos();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
