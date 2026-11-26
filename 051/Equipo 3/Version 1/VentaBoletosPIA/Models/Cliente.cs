namespace VentaBoletosApp.Models;

public class Cliente : EntidadModificable
{
    public string RFC { get; set; } = string.Empty;

    public string NombreCompleto { get; set; } = string.Empty;

    public string DomicilioCompleto { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string? TelefonoCasa { get; set; }

    public string TelefonoCelular { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public bool Activo { get; set; } = true;
}
