using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Partidos: Programado al registrarse, Terminado cuando pasa su fecha y Cancelado con "Cancelar partido".
// Eliminar es una baja lógica (inactivo); solo se eligen estadios y equipos activos.
public partial class FrmPartidos : Form
{
    private readonly IEquipoService _equipos;
    private readonly IEstadioService _estadios;
    private readonly IPartidoService _partidos;

    // Partido cuyos datos se cargaron en los campos con un clic en la tabla
    private int _idCargado;

    public FrmPartidos(IEquipoService equipos, IEstadioService estadios, IPartidoService partidos)
    {
        InitializeComponent();
        _equipos = equipos;
        _estadios = estadios;
        _partidos = partidos;
        dgvPartidos.AutoGenerateColumns = false;
        dtpFecha.MinDate = DateTime.Today;
        txtMotivoCancelacion.MaxLength = _partidos.LongitudMaximaMotivo;
    }

    private void CargarCatalogos()
    {
        try
        {
            cboEstadio.DataSource = _estadios.ObtenerEstadiosActivos();

            List<Equipo> equipos = _equipos.ObtenerActivos();
            cboLocal.DataSource = equipos;
            cboLocal.DisplayMember = nameof(Equipo.Nombre);
            cboLocal.ValueMember = nameof(Equipo.IdEquipo);
            cboVisitante.DataSource = new List<Equipo>(equipos);
            cboVisitante.DisplayMember = nameof(Equipo.Nombre);
            cboVisitante.ValueMember = nameof(Equipo.IdEquipo);
            if (cboVisitante.Items.Count > 1)
            {
                cboVisitante.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void CargarPartidos()
    {
        try
        {
            dgvPartidos.DataSource = _partidos.ObtenerPartidos(txtBuscar.Text);
            dgvPartidos.ClearSelection();
            btnCancelarPartido.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnReactivar.Enabled = false;
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Registrar(object? sender, EventArgs e)
    {
        Partido partido = new()
        {
            IdEstadio = (cboEstadio.SelectedItem as Estadio)?.IdEstadio ?? 0,
            IdEquipoLocal = (cboLocal.SelectedItem as Equipo)?.IdEquipo ?? 0,
            IdEquipoVisitante = (cboVisitante.SelectedItem as Equipo)?.IdEquipo ?? 0,
            FechaHora = dtpFecha.Value.Date + dtpHora.Value.TimeOfDay
        };

        if (!Mensajes.Confirmar("¿Registrar este partido?"))
        {
            return;
        }

        try
        {
            _partidos.RegistrarPartido(partido);
            Mensajes.Informacion("Partido registrado correctamente.");
            txtBuscar.Clear();
            CargarPartidos();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    // Cancelar y modificar: solo un partido activo y Programado. Eliminar: uno activo que no esté cancelado.
    // Reactivar: uno inactivo que sigue Programado (su fecha no ha pasado).
    private void dgvPartidos_SelectionChanged(object? sender, EventArgs e)
    {
        Partido? partido = PartidoSeleccionado();
        bool activo = partido is not null && partido.Activo;
        btnCancelarPartido.Enabled = activo && partido!.Estado == EstadosPartido.Programado;
        btnModificar.Enabled = activo && partido!.Estado == EstadosPartido.Programado;
        btnEliminar.Enabled = activo && partido!.Estado != EstadosPartido.Cancelado;
        btnReactivar.Enabled = partido is not null && !partido.Activo && partido.Estado == EstadosPartido.Programado;
        lblRegistroPartido.Text = partido?.DescripcionRegistro ?? string.Empty;
    }

    private Partido? PartidoSeleccionado()
    {
        if (dgvPartidos.SelectedRows.Count == 0)
        {
            return null;
        }

        return dgvPartidos.SelectedRows[0].DataBoundItem as Partido;
    }

    // También se cancelan sus ventas vigentes: se liberan los boletos y se cancelan las facturas
    private void CancelarPartido(object? sender, EventArgs e)
    {
        Partido? partido = PartidoSeleccionado();
        if (partido is null)
        {
            Mensajes.Aviso("Seleccione en la lista el partido que desea cancelar.");
            return;
        }

        if (Validador.Vacio(txtMotivoCancelacion.Text))
        {
            Mensajes.Aviso("Escriba el motivo de la cancelación del partido.");
            txtMotivoCancelacion.Focus();
            return;
        }

        string ventas = partido.VentasVigentes switch
        {
            0 => "No tiene ventas vigentes.",
            1 => "Su venta vigente también se cancelará: el boleto se libera y la factura se cancela.",
            _ => $"Sus {partido.VentasVigentes} ventas vigentes también se cancelarán: los boletos se liberan y las facturas se cancelan."
        };

        if (!Mensajes.Confirmar($"¿Cancelar el partido {partido.Encuentro} del {partido.FechaHoraTexto}?\n\n" +
            $"{ventas}\n\nLa cancelación no se puede deshacer."))
        {
            return;
        }

        try
        {
            int canceladas = _partidos.CancelarPartido(partido, txtMotivoCancelacion.Text);
            Mensajes.Informacion($"Partido cancelado. Ventas canceladas: {canceladas}.");
            txtMotivoCancelacion.Clear();
            CargarPartidos();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Modificar(object? sender, EventArgs e)
    {
        Partido? seleccionado = PartidoSeleccionado();
        if (seleccionado is null || seleccionado.IdPartido != _idCargado)
        {
            Mensajes.Aviso("Haga clic en el partido de la lista para cargar sus datos y después modifíquelos.");
            return;
        }

        Partido partido = new()
        {
            IdPartido = seleccionado.IdPartido,
            Estado = seleccionado.Estado,
            Activo = seleccionado.Activo,
            IdEstadio = (cboEstadio.SelectedItem as Estadio)?.IdEstadio ?? 0,
            IdEquipoLocal = (cboLocal.SelectedItem as Equipo)?.IdEquipo ?? 0,
            IdEquipoVisitante = (cboVisitante.SelectedItem as Equipo)?.IdEquipo ?? 0,
            FechaHora = dtpFecha.Value.Date + dtpHora.Value.TimeOfDay
        };

        string ventas = seleccionado.VentasVigentes > 0
            ? $"\n\nTiene {seleccionado.VentasVigentes} venta(s): solo se pueden cambiar la fecha y la hora."
            : string.Empty;
        if (!Mensajes.Confirmar($"¿Guardar los cambios del partido {seleccionado.Encuentro}?{ventas}"))
        {
            return;
        }

        try
        {
            _partidos.ActualizarPartido(partido);
            Mensajes.Informacion("Partido modificado correctamente.");
            Limpiar();
            CargarPartidos();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Eliminar(object? sender, EventArgs e)
    {
        Partido? partido = PartidoSeleccionado();
        if (partido is null)
        {
            Mensajes.Aviso("Seleccione en la lista el partido que desea eliminar.");
            return;
        }

        if (!Mensajes.Confirmar($"¿Eliminar el partido {partido.Encuentro} del {partido.FechaHoraTexto}?\n\n" +
            "Quedará inactivo: no se venderá ni aparecerá en los reportes. Solo se puede eliminar un partido sin ventas registradas."))
        {
            return;
        }

        try
        {
            _partidos.EliminarPartido(partido);
            Mensajes.Informacion("Partido eliminado (inactivo).");
            Limpiar();
            CargarPartidos();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Reactivar(object? sender, EventArgs e)
    {
        Partido? partido = PartidoSeleccionado();
        if (partido is null)
        {
            Mensajes.Aviso("Seleccione en la lista el partido inactivo que desea reactivar.");
            return;
        }

        if (!Mensajes.Confirmar($"¿Reactivar el partido {partido.Encuentro} del {partido.FechaHoraTexto}?"))
        {
            return;
        }

        try
        {
            _partidos.ReactivarPartido(partido);
            Mensajes.Informacion("Partido reactivado correctamente.");
            Limpiar();
            CargarPartidos();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void dgvPartidos_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvPartidos.Rows[e.RowIndex].DataBoundItem is not Partido partido)
        {
            return;
        }

        _idCargado = partido.IdPartido;

        // Si el estadio o un equipo ya está inactivo no aparece en la lista: el campo queda vacío
        Seleccionar<Estadio>(cboEstadio, estadio => estadio.IdEstadio == partido.IdEstadio);
        Seleccionar<Equipo>(cboLocal, equipo => equipo.IdEquipo == partido.IdEquipoLocal);
        Seleccionar<Equipo>(cboVisitante, equipo => equipo.IdEquipo == partido.IdEquipoVisitante);

        // Un partido ya jugado no se modifica; su fecha queda fuera del rango del calendario
        if (partido.FechaHora.Date >= dtpFecha.MinDate)
        {
            dtpFecha.Value = partido.FechaHora.Date;
            dtpHora.Value = dtpHora.Value.Date + partido.FechaHora.TimeOfDay;
        }
    }

    private static void Seleccionar<T>(ComboBox combo, Func<T, bool> coincide)
    {
        combo.SelectedIndex = -1;
        foreach (object elemento in combo.Items)
        {
            if (elemento is T item && coincide(item))
            {
                combo.SelectedItem = elemento;
                break;
            }
        }
    }

    private void Limpiar()
    {
        _idCargado = 0;
        if (cboEstadio.Items.Count > 0)
        {
            cboEstadio.SelectedIndex = 0;
        }

        if (cboLocal.Items.Count > 0)
        {
            cboLocal.SelectedIndex = 0;
        }

        if (cboVisitante.Items.Count > 1)
        {
            cboVisitante.SelectedIndex = 1;
        }

        dtpFecha.Value = DateTime.Today;
        dgvPartidos.ClearSelection();
    }

    private void btnLimpiar_Click(object? sender, EventArgs e)
    {
        Limpiar();
    }

    private void btnBuscar_Click(object? sender, EventArgs e)
    {
        CargarPartidos();
    }

    private void btnActualizarEquipos_Click(object? sender, EventArgs e)
    {
        CargarCatalogos();
    }

    private void FrmPartidos_Load(object? sender, EventArgs e)
    {
        CargarCatalogos();
        CargarPartidos();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
