namespace VentaBoletosApp.Models;

public class Usuario : EntidadModificable
{
    public int IdUsuario { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public string NumNomina { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string Correo { get; set; } = string.Empty;

    public int IdTipoUsuario { get; set; }

    public string TipoUsuario { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    /// <summary>
    /// Hash de la contraseña actual. Solo se llena cuando se consulta por
    /// correo (para validar login); nunca se expone en listados.
    /// </summary>
    public string? ContrasenaHash { get; set; }

    public bool EsAdministrador => string.Equals(TipoUsuario, "Administrador", StringComparison.OrdinalIgnoreCase);
}
