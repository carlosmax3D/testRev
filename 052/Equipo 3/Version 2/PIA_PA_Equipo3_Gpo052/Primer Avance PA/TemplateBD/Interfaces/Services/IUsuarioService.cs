using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IUsuarioService
{
    List<Usuario> ObtenerUsuarios();
    Usuario? ObtenerUsuario(int id);
    Usuario? IniciarSesion(string correo, string contrasena);
    void CrearUsuario(Usuario usuario);
    void ActualizarUsuario(Usuario usuario);
    void EliminarUsuario(int id);
}
