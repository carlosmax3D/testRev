using System;

namespace TemplateCSharp.Models;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NumNomina { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public int? IdUsuarioRegistro { get; set; }
    public DateTime FechaHoraRegistro { get; set; }
    public string? Sal { get; set; }
}