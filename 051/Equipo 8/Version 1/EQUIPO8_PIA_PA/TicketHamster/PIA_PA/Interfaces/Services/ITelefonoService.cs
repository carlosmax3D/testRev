using PIA_PA.Entidades.Telefono;

namespace PIA_PA.Entidades.Telefono;

public interface ITelefonoService
{
    List<Telefono> ObtenerTodos();
    Telefono? ObtenerPorId(int telefono_ID);
    void Insertar(Telefono entity);
    void Actualizar(Telefono entity);
    void Eliminar(int telefono_ID);
}
