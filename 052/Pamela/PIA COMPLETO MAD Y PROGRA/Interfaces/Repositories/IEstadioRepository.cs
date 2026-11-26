using System.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IEstadioRepository
{
    DataTable Consultar();

    DataTable Buscar(string texto);

    Estadio? ObtenerPorId(int idEstadio);

    List<Zona> ConsultarZonas(int idEstadio);

    int Guardar(Estadio estadio, List<Zona> zonasNuevas, List<int> zonasEliminadas, int idUsuario);

    void Eliminar(int idEstadio);
}
