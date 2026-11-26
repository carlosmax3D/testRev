using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IEquipoService
{
    // Activos e inactivos
    List<Equipo> Consultar(string? busqueda);

    // Solo activos, para programar partidos
    List<Equipo> ObtenerActivos();

    void Registrar(string nombre);

    void Actualizar(Equipo equipo, string nombre);

    // Borrado lógico: solo un equipo sin partidos programados
    void Eliminar(Equipo equipo);

    void Reactivar(Equipo equipo);
}
