using System;
namespace TemplateCSharp.Models;

public class Cliente
{
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string? Rfc { get; set; }
    public int? IdUsuarioRegistro { get; set; }
    public DateTime FechaHoraRegistro { get; set; }
    public string? CURP { get; set; }
}