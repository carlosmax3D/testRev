using System.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IPartidoService
{
    DataTable ObtenerPartidos();

    DataTable BuscarPartidos(string texto);

    Partido? ObtenerPartido(int idPartido);

    int RegistrarPartido(Partido partido, int idUsuario);

    void ActualizarPartido(Partido partido, int idUsuario);

    void EliminarPartido(int idPartido, int idUsuario);

    DataTable ObtenerPartidosDisponibles(string? criterio, string? texto);

    DataTable ObtenerZonasDePartido(int idPartido, bool soloDisponibles);
}
