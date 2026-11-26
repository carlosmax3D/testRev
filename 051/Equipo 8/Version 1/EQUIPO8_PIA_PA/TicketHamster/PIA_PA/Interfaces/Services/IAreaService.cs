using PIA_PA.Entidades.Area;

namespace PIA_PA.Entidades.Area;

public interface IAreaService
{
    List<Area> ObtenerTodos();
    Area? ObtenerPorId(int area_ID);
    void Insertar(Area entity);
    void Actualizar(Area entity);
    void Eliminar(int area_ID);
}
