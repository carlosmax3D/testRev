using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Catálogo de estadios y sus zonas. Al registrar una zona se generan sus asientos.
// Un clic en la tabla carga el estadio o la zona para modificarlos, eliminarlos (borrado lógico) o reactivarlos.
public partial class FrmEstadios : Form
{
    private readonly IEstadioService _estadios;
    private readonly IServiceProvider _serviceProvider;

    // Evita consultar las zonas varias veces mientras se llena la tabla de estadios
    private bool _cargando;

    // Estadio cargado en los campos con un clic en la tabla
    private Estadio? _estadioCargado;
    private Zona? _zonaSeleccionada;

    public FrmEstadios(IEstadioService estadios, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _estadios = estadios;
        _serviceProvider = serviceProvider;
        dgvEstadios.AutoGenerateColumns = false;
        dgvZonas.AutoGenerateColumns = false;
        colPrecioBase.DefaultCellStyle.FormatProvider = Formato.Cultura;
    }

    private Estadio? EstadioSeleccionado()
    {
        return dgvEstadios.CurrentRow?.DataBoundItem as Estadio;
    }

    // idSeleccionar: estadio que queda seleccionado después de recargar
    private void CargarEstadios(int idSeleccionar = 0)
    {
        _cargando = true;
        try
        {
            dgvEstadios.DataSource = _estadios.ObtenerEstadios();

            foreach (DataGridViewRow fila in dgvEstadios.Rows)
            {
                if (fila.DataBoundItem is Estadio estadio && estadio.IdEstadio == idSeleccionar)
                {
                    dgvEstadios.CurrentCell = fila.Cells[0];
                    break;
                }
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

        CargarZonas();
    }

    private void CargarZonas()
    {
        Estadio? estadio = EstadioSeleccionado();
        lblRegistroEstadio.Text = estadio?.DescripcionRegistro ?? string.Empty;
        if (estadio is null)
        {
            dgvZonas.DataSource = null;
            _zonaSeleccionada = null;
            return;
        }

        try
        {
            dgvZonas.DataSource = _estadios.ObtenerZonas(estadio.IdEstadio);
            dgvZonas.ClearSelection();
            _zonaSeleccionada = null;
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void ElegirMapa(object? sender, EventArgs e)
    {
        using OpenFileDialog dialogo = new()
        {
            Filter = "Imágenes|*.png;*.jpg;*.jpeg;*.bmp"
        };
        if (dialogo.ShowDialog(this) == DialogResult.OK)
        {
            txtMapa.Text = dialogo.FileName;
        }
    }

    private void RegistrarEstadio(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtCapacidad.Text.Trim(), NumberStyles.AllowThousands, Formato.Cultura, out int capacidad))
        {
            Mensajes.Aviso("La capacidad debe ser un número entero mayor que cero.");
            return;
        }

        if (!File.Exists(txtMapa.Text))
        {
            Mensajes.Aviso("Seleccione la imagen del mapa del estadio.");
            return;
        }

        try
        {
            _estadios.ValidarTamanoMapa(new FileInfo(txtMapa.Text).Length);

            Estadio estadio = new()
            {
                Nombre = txtNombre.Text,
                Ciudad = txtCiudad.Text,
                CapacidadTotal = capacidad,
                MapaEstadio = File.ReadAllBytes(txtMapa.Text)
            };

            if (!Mensajes.Confirmar("¿Registrar este estadio?"))
            {
                return;
            }

            _estadios.RegistrarEstadio(estadio);
            Mensajes.Informacion("Estadio registrado correctamente.");
            LimpiarEstadio();
            CargarEstadios(estadio.IdEstadio);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void RegistrarZona(object? sender, EventArgs e)
    {
        Estadio? estadio = EstadioSeleccionado();
        if (estadio is null)
        {
            Mensajes.Aviso("Seleccione primero un estadio de la lista.");
            return;
        }

        if (!estadio.Activo)
        {
            Mensajes.Aviso("El estadio está inactivo. Reactívelo para agregarle zonas.");
            return;
        }

        if (!decimal.TryParse(txtPrecio.Text.Replace("$", string.Empty).Trim(), NumberStyles.Number, Formato.Cultura, out decimal precio))
        {
            Mensajes.Aviso("El precio base debe ser un número mayor que cero.");
            return;
        }

        if (!int.TryParse(txtCupo.Text.Trim(), NumberStyles.AllowThousands, Formato.Cultura, out int cupo))
        {
            Mensajes.Aviso("El cupo debe ser un número entero mayor que cero.");
            return;
        }

        Zona zona = new()
        {
            IdEstadio = estadio.IdEstadio,
            NombreZona = txtZona.Text,
            PrecioBase = precio,
            Cupo = cupo
        };

        if (!Mensajes.Confirmar($"¿Registrar la zona en {estadio} y generar sus {cupo:N0} asientos?"))
        {
            return;
        }

        // Crear muchos asientos puede tardar unos segundos
        UseWaitCursor = true;
        try
        {
            _estadios.RegistrarZona(zona, (int)nudAsientosFila.Value);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
            return;
        }
        finally
        {
            UseWaitCursor = false;
        }

        Mensajes.Informacion($"Zona guardada con {cupo:N0} asientos.");
        LimpiarZona();
        CargarEstadios(estadio.IdEstadio);
    }

    private void ModificarEstadio(object? sender, EventArgs e)
    {
        Estadio? cargado = _estadioCargado;
        if (cargado is null)
        {
            Mensajes.Aviso("Haga clic en un estadio de la lista para modificarlo.");
            return;
        }

        if (!int.TryParse(txtCapacidad.Text.Trim(), NumberStyles.AllowThousands, Formato.Cultura, out int capacidad))
        {
            Mensajes.Aviso("La capacidad debe ser un número entero mayor que cero.");
            return;
        }

        if (txtMapa.Text.Length > 0 && !File.Exists(txtMapa.Text))
        {
            Mensajes.Aviso("No se encontró la imagen del mapa seleccionada.");
            return;
        }

        try
        {
            byte[] mapa = [];
            if (txtMapa.Text.Length > 0)
            {
                _estadios.ValidarTamanoMapa(new FileInfo(txtMapa.Text).Length);
                mapa = File.ReadAllBytes(txtMapa.Text);
            }

            Estadio estadio = new()
            {
                IdEstadio = cargado.IdEstadio,
                Activo = cargado.Activo,
                Nombre = txtNombre.Text,
                Ciudad = txtCiudad.Text,
                CapacidadTotal = capacidad,
                MapaEstadio = mapa
            };

            string avisoMapa = mapa.Length == 0 ? "\n\nNo eligió un mapa nuevo: se conserva el actual." : string.Empty;
            if (!Mensajes.Confirmar($"¿Guardar los cambios del estadio?{avisoMapa}"))
            {
                return;
            }

            _estadios.ActualizarEstadio(estadio);
            Mensajes.Informacion("Estadio modificado correctamente.");
            LimpiarEstadio();
            CargarEstadios(estadio.IdEstadio);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void EliminarEstadio(object? sender, EventArgs e)
    {
        Estadio? estadio = _estadioCargado;
        if (estadio is null)
        {
            Mensajes.Aviso("Haga clic en un estadio de la lista para eliminarlo.");
            return;
        }

        if (!Mensajes.Confirmar($"¿Eliminar el estadio {estadio}?\n\nQuedará inactivo: ya no se podrá elegir al programar partidos; " +
            "sus zonas y sus partidos anteriores se conservan. No se puede eliminar si tiene partidos programados."))
        {
            return;
        }

        try
        {
            _estadios.EliminarEstadio(estadio);
            Mensajes.Informacion("Estadio eliminado (inactivo).");
            LimpiarEstadio();
            LimpiarZona();
            CargarEstadios(estadio.IdEstadio);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void ReactivarEstadio(object? sender, EventArgs e)
    {
        Estadio? estadio = _estadioCargado;
        if (estadio is null)
        {
            Mensajes.Aviso("Haga clic en un estadio inactivo de la lista para reactivarlo.");
            return;
        }

        if (!Mensajes.Confirmar($"¿Reactivar el estadio {estadio.Nombre} ({estadio.Ciudad})?"))
        {
            return;
        }

        try
        {
            _estadios.ReactivarEstadio(estadio);
            Mensajes.Informacion("Estadio reactivado correctamente.");
            LimpiarEstadio();
            CargarEstadios(estadio.IdEstadio);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void ModificarZona(object? sender, EventArgs e)
    {
        Zona? actual = _zonaSeleccionada;
        if (actual is null)
        {
            Mensajes.Aviso("Haga clic en una zona de la lista para modificarla.");
            return;
        }

        if (!decimal.TryParse(txtPrecio.Text.Replace("$", string.Empty).Trim(), NumberStyles.Number, Formato.Cultura, out decimal precio))
        {
            Mensajes.Aviso("El precio base debe ser un número mayor que cero.");
            return;
        }

        if (!int.TryParse(txtCupo.Text.Trim(), NumberStyles.AllowThousands, Formato.Cultura, out int cupo) || cupo != actual.Cupo)
        {
            Mensajes.Aviso($"El cupo de una zona no se puede modificar porque ya tiene sus asientos generados ({actual.Cupo:N0}). " +
                "Si necesita otro cupo, elimine la zona y registre una nueva con otro nombre.");
            txtCupo.Text = actual.Cupo.ToString("N0", Formato.Cultura);
            return;
        }

        Zona zona = new()
        {
            IdZona = actual.IdZona,
            IdEstadio = actual.IdEstadio,
            Activo = actual.Activo,
            NombreZona = txtZona.Text,
            PrecioBase = precio,
            Cupo = actual.Cupo
        };

        if (!Mensajes.Confirmar("¿Guardar los cambios de la zona?\n\nEl precio nuevo aplica a las ventas futuras; los boletos ya vendidos conservan su precio."))
        {
            return;
        }

        try
        {
            _estadios.ActualizarZona(zona);
            Mensajes.Informacion("Zona modificada correctamente.");
            LimpiarZona();
            CargarEstadios(zona.IdEstadio);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void EliminarZona(object? sender, EventArgs e)
    {
        Zona? zona = _zonaSeleccionada;
        if (zona is null)
        {
            Mensajes.Aviso("Haga clic en una zona de la lista para eliminarla.");
            return;
        }

        if (!Mensajes.Confirmar($"¿Eliminar la zona {zona.NombreZona}?\n\nQuedará inactiva: sus {zona.AsientosGenerados:N0} asientos dejan de venderse " +
            "y su cupo deja de contar en la capacidad del estadio. No se puede eliminar si tiene boletos vendidos para partidos programados."))
        {
            return;
        }

        try
        {
            _estadios.EliminarZona(zona);
            Mensajes.Informacion("Zona eliminada (inactiva).");
            LimpiarZona();
            CargarEstadios(zona.IdEstadio);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void ReactivarZona(object? sender, EventArgs e)
    {
        Zona? zona = _zonaSeleccionada;
        if (zona is null)
        {
            Mensajes.Aviso("Haga clic en una zona inactiva de la lista para reactivarla.");
            return;
        }

        if (!Mensajes.Confirmar($"¿Reactivar la zona {zona.NombreZona}? Su cupo ({zona.Cupo:N0}) volverá a contar en la capacidad del estadio."))
        {
            return;
        }

        try
        {
            _estadios.ReactivarZona(zona);
            Mensajes.Informacion("Zona reactivada correctamente.");
            LimpiarZona();
            CargarEstadios(zona.IdEstadio);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void dgvEstadios_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvEstadios.Rows[e.RowIndex].DataBoundItem is not Estadio estadio)
        {
            return;
        }

        _estadioCargado = estadio;
        txtNombre.Text = estadio.Nombre;
        txtCiudad.Text = estadio.Ciudad;
        txtCapacidad.Text = estadio.CapacidadTotal.ToString("N0", Formato.Cultura);
        txtMapa.Clear();
    }

    private void dgvZonas_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvZonas.Rows[e.RowIndex].DataBoundItem is not Zona zona)
        {
            return;
        }

        _zonaSeleccionada = zona;
        txtZona.Text = zona.NombreZona;
        txtPrecio.Text = zona.PrecioBase.ToString("N2", Formato.Cultura);
        txtCupo.Text = zona.Cupo.ToString("N0", Formato.Cultura);
    }

    private void LimpiarEstadio()
    {
        _estadioCargado = null;
        txtNombre.Clear();
        txtCiudad.Clear();
        txtCapacidad.Clear();
        txtMapa.Clear();
    }

    private void LimpiarZona()
    {
        _zonaSeleccionada = null;
        txtZona.Clear();
        txtPrecio.Clear();
        txtCupo.Clear();
        dgvZonas.ClearSelection();
    }

    private void btnLimpiarEstadio_Click(object? sender, EventArgs e)
    {
        LimpiarEstadio();
        txtNombre.Focus();
    }

    private void btnLimpiarZona_Click(object? sender, EventArgs e)
    {
        LimpiarZona();
        txtZona.Focus();
    }

    private void btnVerAsientos_Click(object? sender, EventArgs e)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        FrmZonasAsientos formulario = scope.ServiceProvider.GetRequiredService<FrmZonasAsientos>();
        formulario.ShowDialog(this);
    }

    // Muestra el mapa guardado en la base del estadio seleccionado (también si está inactivo)
    private void VerMapa(object? sender, EventArgs e)
    {
        Estadio? estadio = EstadioSeleccionado();
        if (estadio is null)
        {
            Mensajes.Aviso("Seleccione un estadio de la lista.");
            return;
        }

        try
        {
            byte[] mapa = _estadios.ObtenerMapa(estadio.IdEstadio);
            VisorMapa.Mostrar(this, mapa, $"Mapa de {estadio.Nombre} ({estadio.Ciudad}) - {estadio.EstadoTexto}");
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void dgvEstadios_SelectionChanged(object? sender, EventArgs e)
    {
        if (!_cargando)
        {
            CargarZonas();
        }
    }

    private void FrmEstadios_Load(object? sender, EventArgs e)
    {
        CargarEstadios();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
