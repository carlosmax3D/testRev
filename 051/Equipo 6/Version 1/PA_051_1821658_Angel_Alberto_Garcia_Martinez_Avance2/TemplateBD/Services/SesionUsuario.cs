using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class SesionUsuario : ISesionUsuario
{
    public Usuario? UsuarioActual { get; set; }
}
