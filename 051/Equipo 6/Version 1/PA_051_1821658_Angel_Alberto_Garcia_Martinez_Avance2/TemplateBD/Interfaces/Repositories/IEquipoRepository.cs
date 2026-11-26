using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IEquipoRepository
{
    List<Equipo> Consultar(string? busqueda, int idAdministrador);

    int Insertar(string nombre, int idAdministrador);

    void Actualizar(int idEquipo, string nombre, int idAdministrador);

    // Borrado lógico (Activo = 0)
    void Eliminar(int idEquipo, int idAdministrador);

    void Reactivar(int idEquipo, int idAdministrador);
}
