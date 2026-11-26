using PIA_PA.Entidades.Equipos;

using System.Data;

namespace PIA_PA.Entidades.Equipos;

public interface IEquiposRepository
{
    DataTable ObtenerTablaConsulta();
    List<Equipos> ObtenerTodos();
    Equipos? ObtenerPorId(int equipo_ID);
    void Insertar(Equipos entity);
    void Actualizar(Equipos entity);
    void Eliminar(int equipo_ID);
}
