namespace TemplateCSharp.Models;

public class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Apellido { get; set; } = string.Empty;

    public string DomicilioCompleto { get; set; } = string.Empty;

    public string RFC { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string TelefonoCasa { get; set; } = string.Empty;

    public string TelefonoCelular { get; set; } = string.Empty;

    public string NombreCompleto => (Nombre + " " + Apellido).Trim();
}
