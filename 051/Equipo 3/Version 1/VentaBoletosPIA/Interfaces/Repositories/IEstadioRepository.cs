using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IEstadioRepository
{
    List<Estadio> GetAll();

    Estadio? GetById(int id);

    int Add(Estadio estadio);

    bool ExisteNombre(string nombre, int idExcluir = 0);
}
