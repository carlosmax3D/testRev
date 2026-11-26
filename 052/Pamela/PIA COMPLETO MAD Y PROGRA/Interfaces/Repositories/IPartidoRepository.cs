using System.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IPartidoRepository
{
    DataTable Consultar();

    DataTable Buscar(string texto);

    Partido? ObtenerPorId(int idPartido);

    int Agregar(Partido partido, int idUsuario);

    void Modificar(Partido partido, int idUsuario);

    void Eliminar(int idPartido, int idUsuario);

    DataTable ConsultarDisponibles(string? criterio, string? texto, DateTime? fecha);

    DataTable ConsultarZonas(int idPartido, bool soloDisponibles);
}
