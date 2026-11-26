using PIA_PA.Entidades.Usuario;

namespace PIA_PA.Entidades.Usuario;

public interface IUsuarioService
{
    List<Usuario> ObtenerTodos();
    Usuario? ObtenerPorId(int usuario_ID);
    void Insertar(Usuario entity);
    void Actualizar(Usuario entity);
    void Eliminar(int usuario_ID);
    bool ValidarCredenciales(string email, string contrasena);

    System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros);
    object? ValorOperacion(int id, params (string,object?)[] parametros);
    int EjecutarOperacion(int id, params (string,object?)[] parametros);
    void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion);
}
