using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Services;

public class PartidoService : ServicioBase, IPartidoService
{
    private readonly IPartidoRepository _partidos;
    private readonly IEstadioRepository _estadios;
    private readonly IZonaRepository _zonas;

    public PartidoService(
        IPartidoRepository partidos,
        IEstadioRepository estadios,
        IZonaRepository zonas,
        ISesionActual sesion) : base(sesion)
    {
        _partidos = partidos;
        _estadios = estadios;
        _zonas = zonas;
    }

    public int CrearPartido(Partido partido)
    {
        ExigirAdministrador();

        if (partido is null)
        {
            throw new ArgumentNullException(nameof(partido));
        }

        ValidarTexto(partido.EquipoLocal, "Equipo local", 50);
        ValidarTexto(partido.EquipoVisitante, "Equipo visitante", 50);

        partido.EquipoLocal = partido.EquipoLocal.Trim();
        partido.EquipoVisitante = partido.EquipoVisitante.Trim();

        if (string.Equals(partido.EquipoLocal, partido.EquipoVisitante, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("El equipo local y el visitante no pueden ser el mismo.");
        }

        if (partido.FechaHora <= DateTime.Now)
        {
            throw new ArgumentException("No se pueden registrar partidos en una fecha y hora pasadas.");
        }

        if (_estadios.GetById(partido.IdEstadio) is null)
        {
            throw new ArgumentException("Seleccione un estadio válido.");
        }

        if (_zonas.GetByEstadio(partido.IdEstadio).Count == 0)
        {
            throw new InvalidOperationException(
                "El estadio seleccionado aún no tiene zonas registradas. Registre al menos una zona antes de crear partidos.");
        }

        DateTime inicio = new(partido.FechaHora.Year, partido.FechaHora.Month, partido.FechaHora.Day,
            partido.FechaHora.Hour, partido.FechaHora.Minute, 0);

        bool yaExiste = _partidos
            .Buscar(inicio, inicio.AddSeconds(59), partido.IdEstadio, null)
            .Any(p => !string.Equals(p.EstadoPartido, "Cancelado", StringComparison.OrdinalIgnoreCase));

        if (yaExiste)
        {
            throw new ArgumentException("Ya existe un partido registrado en ese estadio a la misma fecha y hora.");
        }

        partido.FechaHora = inicio;
        partido.IdUsuarioRegistro = Sesion.IdUsuario;

        return _partidos.Add(partido);
    }

    public Partido? ObtenerPartido(int idPartido)
    {
        ExigirSesion();

        return idPartido <= 0 ? null : _partidos.GetById(idPartido);
    }

    public List<Partido> ObtenerPartidos(DateTime? fechaDesde, DateTime? fechaHasta, int? idEstadio, string? equipo)
    {
        ExigirSesion();

        return _partidos.Buscar(fechaDesde, fechaHasta, idEstadio, LimpiarEquipo(equipo));
    }

    public List<Partido> BuscarDisponiblesParaVenta(DateTime? fecha, int? idEstadio, string? equipo)
    {
        ExigirSesion();

        DateTime? desde = fecha?.Date;
        DateTime? hasta = fecha?.Date.AddDays(1).AddSeconds(-1);

        DateTime ahora = DateTime.Now;

        return _partidos
            .Buscar(desde, hasta, idEstadio, LimpiarEquipo(equipo))
            .Where(p => string.Equals(p.EstadoPartido, "Programado", StringComparison.OrdinalIgnoreCase)
                        && p.FechaHora >= ahora)
            .ToList();
    }

    public List<DisponibilidadZona> ObtenerDisponibilidad(int idPartido)
    {
        ExigirSesion();

        if (idPartido <= 0)
        {
            throw new ArgumentException("Seleccione un partido.");
        }

        return _zonas.ObtenerDisponibilidadPorPartido(idPartido);
    }

    private static string? LimpiarEquipo(string? equipo) =>
        string.IsNullOrWhiteSpace(equipo) ? null : equipo.Trim();
}
