using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Services;

public interface IUsuarioService
{
    List<Usuario> ObtenerUsuarios();

    void CrearUsuario(Usuario usuario, string contrasena);

    void ActualizarUsuario(Usuario usuario);

    void CambiarContrasena(int idUsuario, string contrasena);

    void EliminarUsuario(int idUsuario);

    /// <summary>Regresa el usuario si las credenciales son correctas; si no, null.</summary>
    Usuario? ValidarCredenciales(string correo, string contrasena);
}
