using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IUsuarioService
{
    Task<Usuario?> IniciarSesionAsync(
        string email,
        string pw
    );
}
