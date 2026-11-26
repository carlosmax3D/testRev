using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IZonaRepository
{
    List<Zona> GetByEstadio(int idEstadio);

    int Add(Zona zona);

    bool ExisteNombreEnEstadio(string nombreZona, int idEstadio);

    List<DisponibilidadZona> ObtenerDisponibilidadPorPartido(int idPartido);
}
