namespace TemplateCSharp.Models;

public class SesionUsuario
{
    public Usuario? UsuarioActual { get; set; }

    public bool EsAdmin => UsuarioActual?.Rol?.Equals("Administrador", StringComparison.OrdinalIgnoreCase) ?? false;
}