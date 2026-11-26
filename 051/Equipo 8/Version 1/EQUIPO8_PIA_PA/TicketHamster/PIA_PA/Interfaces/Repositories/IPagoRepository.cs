using PIA_PA.Entidades.Pago;

namespace PIA_PA.Entidades.Pago;

public interface IPagoRepository
{
    List<Pago> ObtenerTodos();
    Pago? ObtenerPorId(int pago_ID);
    void Insertar(Pago entity);
    void Actualizar(Pago entity);
    void Eliminar(int pago_ID);
}
