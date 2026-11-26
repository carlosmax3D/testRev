namespace TemplateCSharp.Models;

public class Cliente
{
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string? ApellidoMaterno { get; set; }
    public string Calle { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Colonia { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string CP { get; set; } = string.Empty;
    public string RFC { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public string TelefonoCasa { get; set; } = string.Empty;
    public string TelefonoCelular { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public int UsuarioRegistro { get; set; }
}
