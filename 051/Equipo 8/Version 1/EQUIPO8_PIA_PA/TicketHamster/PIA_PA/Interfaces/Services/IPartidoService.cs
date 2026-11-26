using PIA_PA.Entidades.Partido;

using System.Data;

namespace PIA_PA.Entidades.Partido;

public interface IPartidoService
{
    DataTable ObtenerTablaConsulta();
    List<Partido> ObtenerTodos();
    Partido? ObtenerPorId(int partido_ID);
    void Insertar(Partido entity);
    void Actualizar(Partido entity);
    void Eliminar(int partido_ID);
}
