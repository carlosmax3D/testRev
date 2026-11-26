using System.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IEstadioService
{
    DataTable ObtenerEstadios();

    DataTable BuscarEstadios(string texto);

    Estadio? ObtenerEstadio(int idEstadio);

    List<Zona> ObtenerZonas(int idEstadio);

    void ValidarZona(Zona zona);

    void ValidarEstadio(Estadio estadio, List<Zona> zonasActuales);

    int GuardarEstadio(Estadio estadio, List<Zona> zonasActuales, List<Zona> zonasNuevas, List<int> zonasEliminadas, int idUsuario);

    void EliminarEstadio(int idEstadio);
}
