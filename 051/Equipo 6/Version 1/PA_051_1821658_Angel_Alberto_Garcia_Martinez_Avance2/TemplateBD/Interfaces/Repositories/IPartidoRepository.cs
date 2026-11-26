using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IPartidoRepository
{
    List<Partido> Consultar(int idAdministrador);

    int Insertar(Partido partido, int idAdministrador);

    void Actualizar(Partido partido, int idAdministrador);

    // Borrado lógico (Activo = 0)
    void Eliminar(int idPartido, int idAdministrador);

    void Reactivar(int idPartido, int idAdministrador);

    // Cancela el partido y todas sus ventas vigentes; devuelve cuántas ventas se cancelaron
    int Cancelar(int idPartido, string motivo, int idAdministrador);

    List<Partido> ConsultarDisponibles(int idUsuario);
}
