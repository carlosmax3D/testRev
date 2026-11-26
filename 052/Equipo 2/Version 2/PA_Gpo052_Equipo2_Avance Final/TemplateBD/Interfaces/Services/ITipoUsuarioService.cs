using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface ITipoUsuarioService
    {
        Task<List<TipoUsuario>> ObtenerTodosAsync();
    }
}