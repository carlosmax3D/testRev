using PIA_PA.Entidades.Contrasena;

namespace PIA_PA.Entidades.Contrasena;

public interface IContrasenaRepository
{
    List<Contrasena> ObtenerTodos();
    Contrasena? ObtenerPorId(int contrasena_ID);
    void Insertar(Contrasena entity);
    void Actualizar(Contrasena entity);
    void Eliminar(int contrasena_ID);
}
