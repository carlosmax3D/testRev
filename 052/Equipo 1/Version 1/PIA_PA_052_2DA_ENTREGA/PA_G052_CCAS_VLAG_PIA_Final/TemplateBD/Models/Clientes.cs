namespace TemplateCSharp.Models;

public class Cliente
{
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string ApellidoPa { get; set; } = string.Empty;
    public string ApellidoMa { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Calle { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Colonia { get; set; } = string.Empty;
    public string CP { get; set; } = string.Empty;
    public string TelefonoCasa { get; set; } = string.Empty;
    public string TelefonoCelular { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string RFC { get; set; } = string.Empty;
    public int? UsuarioCambio { get; set; }
    public DateTime? FechaCambio { get; set; }
    public TimeSpan? HoraCambio { get; set; }
}