using PIA_PA.Entidades.Partido_Area_Precio;

namespace PIA_PA.Entidades.Partido_Area_Precio;

public interface IPartido_Area_PrecioService
{
    List<Partido_Area_Precio> ObtenerTodos();
    Partido_Area_Precio? ObtenerPorId(int partido_ID, int area_ID);
    void Insertar(Partido_Area_Precio entity);
    void Actualizar(Partido_Area_Precio entity);
    void Eliminar(int partido_ID, int area_ID);
}
