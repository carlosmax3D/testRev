using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IPartidoRepository
{
    int Add(Partido partido);

    Partido? GetById(int id);

    List<Partido> GetDisponiblesParaVenta();

    List<Partido> Buscar(DateTime? fechaDesde, DateTime? fechaHasta, int? idEstadio, string? equipo);
}
