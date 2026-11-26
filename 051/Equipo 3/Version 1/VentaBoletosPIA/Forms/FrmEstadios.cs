using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

/// <summary>Catalogo de estadios y de sus zonas (solo administrador).</summary>
public partial class FrmEstadios : Form
{
    private readonly IEstadioService _estadios;

    public FrmEstadios(IEstadioService estadios)
    {
        _estadios = estadios;

        InitializeComponent();
        Ui.Estilizar(dgvEstadios);
        Ui.Estilizar(dgvZonas);

        Load += (_, _) => RecargarEstadios();
    }

    private Estadio? EstadioSeleccionado =>
        dgvEstadios.CurrentRow is { Selected: true } fila ? fila.DataBoundItem as Estadio : null;

    private void RecargarEstadios()
    {
        Ui.Ejecutar(this, () => dgvEstadios.DataSource = _estadios.ObtenerEstadios());
        CargarZonas();
    }

    private void DgvEstadios_SelectionChanged(object? sender, EventArgs e) => CargarZonas();

    private void CargarZonas()
    {
        Estadio? e = EstadioSeleccionado;

        if (e is null)
        {
            lblEstadioZona.Text = "Seleccione un estadio";
            dgvZonas.DataSource = null;
            return;
        }

        lblEstadioZona.Text = $"Estadio: {e.Nombre} (capacidad {e.Capacidad:N0})";
        Ui.Ejecutar(this, () => dgvZonas.DataSource = _estadios.ObtenerZonas(e.IdEstadio));
    }

    private void BtnMapa_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialogo = new()
        {
            Title = "Seleccione la imagen del mapa del estadio",
            Filter = "Imágenes|*.png;*.jpg;*.jpeg;*.bmp|Todos los archivos|*.*"
        };

        if (dialogo.ShowDialog(this) == DialogResult.OK)
        {
            txtMapa.Text = dialogo.FileName;
        }
    }

    private void BtnGuardarEstadio_Click(object? sender, EventArgs e)
    {
        Estadio estadio = new()
        {
            Nombre = txtNombre.Text,
            Ciudad = txtCiudad.Text,
            Capacidad = (int)numCapacidad.Value,
            MapaEstadio = txtMapa.Text
        };

        if (Ui.Ejecutar(this, () => _estadios.CrearEstadio(estadio)))
        {
            Ui.Aviso(this, "Estadio registrado correctamente. Ahora agregue sus zonas.");
            txtNombre.Clear();
            txtCiudad.Clear();
            txtMapa.Clear();
            numCapacidad.Value = 1000;
            RecargarEstadios();
        }
    }

    private void BtnGuardarZona_Click(object? sender, EventArgs e)
    {
        Estadio? estadio = EstadioSeleccionado;

        if (estadio is null)
        {
            Ui.Aviso(this, "Seleccione primero un estadio de la lista.");
            return;
        }

        Zona zona = new()
        {
            IdEstadio = estadio.IdEstadio,
            NombreZona = txtZona.Text,
            Precio = numPrecio.Value,
            Cupo = (int)numCupo.Value
        };

        if (Ui.Ejecutar(this, () => _estadios.CrearZona(zona)))
        {
            Ui.Aviso(this, "Zona registrada y asientos generados.");
            txtZona.Clear();
            CargarZonas();
        }
    }
}
