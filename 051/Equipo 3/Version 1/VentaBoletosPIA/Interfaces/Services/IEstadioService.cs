using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Services;

public interface IEstadioService
{
    List<Estadio> ObtenerEstadios();

    int CrearEstadio(Estadio estadio);

    List<Zona> ObtenerZonas(int idEstadio);

    int CrearZona(Zona zona);
}
