using PIA_PA.Entidades.Deporte;

namespace PIA_PA.Entidades.Deporte;

public interface IDeporteRepository
{
    List<Deporte> ObtenerTodos();
    Deporte? ObtenerPorId(int deporte_ID);
    void Insertar(Deporte entity);
    void Actualizar(Deporte entity);
    void Eliminar(int deporte_ID);
}
