using PIA_PA.Entidades.Pagador;

namespace PIA_PA.Entidades.Pagador;

public interface IPagadorRepository
{
    List<Pagador> ObtenerTodos();
    Pagador? ObtenerPorId(string pagador_RFC);
    void Insertar(Pagador entity);
    void Actualizar(Pagador entity);
    void Eliminar(string pagador_RFC);
}
