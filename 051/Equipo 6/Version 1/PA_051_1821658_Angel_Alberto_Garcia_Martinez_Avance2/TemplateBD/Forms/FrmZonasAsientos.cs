using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Consulta de los asientos que se generaron para cada zona
public partial class FrmZonasAsientos : Form
{
    private readonly IEstadioService _estadios;

    // Evita consultar la base varias veces mientras se llenan los combos
    private bool _cargando;

    public FrmZonasAsientos(IEstadioService estadios)
    {
        InitializeComponent();
        _estadios = estadios;
        dgvAsientos.AutoGenerateColumns = false;
    }

    private void CargarEstadios()
    {
        _cargando = true;
        try
        {
            cboEstadio.DataSource = _estadios.ObtenerEstadios();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
        finally
        {
            _cargando = false;
        }

        CargarZonas();
    }

    private void CargarZonas()
    {
        _cargando = true;
        try
        {
            cboZona.DataSource = null;
            if (cboEstadio.SelectedItem is Estadio estadio)
            {
                cboZona.DataSource = _estadios.ObtenerZonas(estadio.IdEstadio);
            }
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
        finally
        {
            _cargando = false;
        }

        MostrarAsientos();
    }

    private void MostrarAsientos()
    {
        if (cboZona.SelectedItem is not Zona zona)
        {
            lblDetalleZona.Text = "Seleccione un estadio con zonas registradas.";
            dgvAsientos.DataSource = null;
            return;
        }

        lblDetalleZona.Text = $"Precio base: {Formato.Moneda(zona.PrecioBase)}    Cupo: {zona.Cupo:N0}    Asientos: {zona.AsientosGenerados:N0}    Estado: {zona.EstadoTexto}" +
            $"{Environment.NewLine}{zona.DescripcionRegistro}";

        try
        {
            dgvAsientos.DataSource = _estadios.ObtenerAsientos(zona.IdZona);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void cboEstadio_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_cargando)
        {
            CargarZonas();
        }
    }

    private void cboZona_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_cargando)
        {
            MostrarAsientos();
        }
    }

    private void FrmZonasAsientos_Load(object? sender, EventArgs e)
    {
        CargarEstadios();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
