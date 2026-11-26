using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

/// <summary>
/// Guarda al usuario que inició sesión. Se registra como Singleton para que
/// todas las pantallas sepan quién está operando (auditoría y permisos).
/// </summary>
public class SesionUsuario : ISesionUsuario
{
    public Usuario? Usuario { get; set; }

    public int IdUsuario => Usuario?.IdUsuario ?? 0;

    public bool EsAdministrador =>
        string.Equals(Usuario?.TipoUsuario, "Administrador", StringComparison.OrdinalIgnoreCase);
}
