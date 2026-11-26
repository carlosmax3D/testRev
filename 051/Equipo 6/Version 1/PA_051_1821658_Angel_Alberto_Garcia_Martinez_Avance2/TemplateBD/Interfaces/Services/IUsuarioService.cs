using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IUsuarioService
{
    bool RequiereAdministradorInicial();

    void CrearAdministradorInicial(Usuario usuario, Empresa empresa, string contrasena, string confirmacion);

    Usuario? IniciarSesion(string correo, string contrasena);

    List<Usuario> ObtenerUsuarios(string? busqueda, bool incluirInactivos = false);

    void RegistrarUsuario(Usuario usuario, string contrasena, string confirmacion);

    void ActualizarUsuario(Usuario usuario, string? nuevaContrasena, string? confirmacion);

    void EliminarUsuario(int idUsuario);

    void ReactivarUsuario(Usuario usuario);
}
