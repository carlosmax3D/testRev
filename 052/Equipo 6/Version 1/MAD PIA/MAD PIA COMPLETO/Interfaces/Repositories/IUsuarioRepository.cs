using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IUsuarioRepository
{
    ResultadoInicioSesion ValidarAcceso(string correo,string contrasena);

    List<Usuario> ObtenerUsuarios();

    List<Usuario> BuscarUsuarios(string criterio, string texto);

    bool ExisteNumeroNomina(string numeroNomina);

    bool ExisteCorreo(string correo);

    int RegistrarUsuario(Usuario usuario, string claveAdmin, string cajaAsignada);

    Usuario ObtenerUsuarioPorId(int idUsuario);

    void ActualizarUsuario(Usuario usuario, string claveAdmin, string cajaAsignada);

    void EliminarUsuario(int idUsuario);
}