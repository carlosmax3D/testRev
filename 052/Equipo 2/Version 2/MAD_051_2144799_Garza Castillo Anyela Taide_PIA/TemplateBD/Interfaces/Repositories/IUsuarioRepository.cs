using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        // Método utilizado por el inicio de sesión.
        Task<Usuario?> ObtenerPorEmailAsync(string email);

        // Consultar todos los usuarios.
        Task<List<Usuario>> ObtenerTodosAsync();

        // Consultar un usuario por su identificador.
        Task<Usuario?> ObtenerPorIdAsync(int idUsuario);

        // Comprobar si un correo ya está registrado.
        Task<bool> ExisteEmailAsync(string email, int? idUsuarioExcluir = null);

        // Registrar un usuario.
        Task<int> RegistrarAsync(Usuario usuario);

        // Actualizar los datos de un usuario.
        Task<bool> EditarAsync(Usuario usuario);

        // Desactivar un usuario sin eliminar su historial.
        Task<bool> DesactivarAsync(int idUsuario);

        // Activar nuevamente un usuario.
        Task<bool> ActivarAsync(int idUsuario);
    }
}
