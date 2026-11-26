using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Services;

public interface IPartidoService
{
    int CrearPartido(Partido partido);

    Partido? ObtenerPartido(int idPartido);

    /// <summary>Todos los partidos (cualquier estado), para consultas y filtros.</summary>
    List<Partido> ObtenerPartidos(DateTime? fechaDesde, DateTime? fechaHasta, int? idEstadio, string? equipo);

    /// <summary>Solo partidos programados que aun no ocurren, filtrables por fecha, estadio o equipo.</summary>
    List<Partido> BuscarDisponiblesParaVenta(DateTime? fecha, int? idEstadio, string? equipo);

    List<DisponibilidadZona> ObtenerDisponibilidad(int idPartido);
}
