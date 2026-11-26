using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IUsuarioService
{
    ResultadoInicioSesion IniciarSesion(string correo,string contrasena);
    List<Usuario> ObtenerUsuarios();
    List<Usuario> BuscarUsuarios(string criterio, string texto);
    Usuario ObtenerUsuarioPorId(int idUsuario);
    bool ExisteNumeroNomina(string numeroNomina);
    bool ExisteCorreo(string correo);
    int RegistrarUsuario(Usuario usuario, string claveAdmin, string cajaAsignada);
    void ActualizarUsuario(Usuario usuario,string claveAdmin,string cajaAsignada);
    void EliminarUsuario(int idUsuario);
}