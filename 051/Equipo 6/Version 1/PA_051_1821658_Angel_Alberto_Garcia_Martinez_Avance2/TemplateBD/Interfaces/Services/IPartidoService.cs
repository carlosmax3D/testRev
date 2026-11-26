using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IPartidoService
{
    int LongitudMaximaMotivo { get; }

    List<Partido> ObtenerPartidos(string? busqueda);

    void RegistrarPartido(Partido partido);

    // Solo un partido programado que aún no se juega; con ventas solo cambian fecha y hora
    void ActualizarPartido(Partido partido);

    // Borrado lógico: solo un partido activo, sin ventas y que no esté cancelado
    void EliminarPartido(Partido partido);

    // Solo un partido inactivo, programado y cuya fecha no ha pasado
    void ReactivarPartido(Partido partido);

    // Cancela el partido y sus ventas vigentes; devuelve cuántas ventas se cancelaron
    int CancelarPartido(Partido partido, string motivo);

    List<Partido> ObtenerPartidosDisponibles(string? busqueda);
}
