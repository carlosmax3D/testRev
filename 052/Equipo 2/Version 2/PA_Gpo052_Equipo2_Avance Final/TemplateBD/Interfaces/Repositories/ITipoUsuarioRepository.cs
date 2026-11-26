using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface ITipoUsuarioRepository
    {
        Task<List<TipoUsuario>> ObtenerTodosAsync();
    }
}