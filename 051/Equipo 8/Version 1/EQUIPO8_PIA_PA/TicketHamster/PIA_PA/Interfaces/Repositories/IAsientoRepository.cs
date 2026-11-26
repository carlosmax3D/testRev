using PIA_PA.Entidades.Asiento;

namespace PIA_PA.Entidades.Asiento;

public interface IAsientoRepository
{
    List<Asiento> ObtenerTodos();
    Asiento? ObtenerPorId(int asiento_ID);
    void Insertar(Asiento entity);
    void Actualizar(Asiento entity);
    void Eliminar(int asiento_ID);
}
