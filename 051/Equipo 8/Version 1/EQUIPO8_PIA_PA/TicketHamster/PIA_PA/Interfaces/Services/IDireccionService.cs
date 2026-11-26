using PIA_PA.Entidades.Direccion;

namespace PIA_PA.Entidades.Direccion;

public interface IDireccionService
{
    List<Direccion> ObtenerTodos();
    Direccion? ObtenerPorId(int direccion_ID);
    void Insertar(Direccion entity);
    void Actualizar(Direccion entity);
    void Eliminar(int direccion_ID);
}
