using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IAsientoRepository
{
    List<Asiento> GetDisponiblesPorZonaPartido(int idZona, int idPartido);
}
