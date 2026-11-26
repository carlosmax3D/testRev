using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Usuario? Login(string correo, string contrasenaHash);

    List<Usuario> Consultar(string? busqueda, int idAdministradorAccion, bool incluirInactivos = false);

    int Insertar(Usuario usuario, string contrasenaHash, int idUsuarioRegistro);

    void Actualizar(Usuario usuario, string? nuevaContrasenaHash, int idAdministradorAccion, string? hashEsperado);

    // Baja lógica (Activo = 0)
    void Eliminar(int idUsuario, int idAdministradorAccion);

    void Reactivar(int idUsuario, int idAdministradorAccion);

    string? ObtenerCredencial(string correo);

    List<string> ObtenerHistorial(int idUsuario, int idAdministradorAccion);

    bool RequiereAdministradorInicial();

    int CrearAdministradorInicial(Usuario usuario, Empresa empresa, string contrasenaHash);
}
