using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface ISesionUsuario
{
    Usuario? UsuarioActual { get; set; }
}
