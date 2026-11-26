using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

/// <summary>Usuario que inició sesión (permisos y auditoría).</summary>
public interface ISesionUsuario
{
    Usuario? Usuario { get; set; }

    int IdUsuario { get; }

    bool EsAdministrador { get; }
}
