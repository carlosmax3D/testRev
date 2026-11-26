using PIA_PA.Entidades.Emisor;

namespace PIA_PA.Entidades.Emisor;

public interface IEmisorRepository
{
    List<Emisor> ObtenerTodos();
    Emisor? ObtenerPorId(int emisor_ID);
    void Insertar(Emisor entity);
    void Actualizar(Emisor entity);
    void Eliminar(int emisor_ID);
}
