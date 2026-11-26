namespace TemplateCSharp.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    public string Correo { get; set; } = string.Empty;

    public string Contrasena { get; set; } = string.Empty;

    public string NombreCompleto { get; set; } = string.Empty;

    public string TipoUsuario { get; set; } = string.Empty;

    public string EstadoUsuario { get; set; } = string.Empty;

    public string NumeroNomina { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public DateTime FechaHoraRegistro { get; set; }

    public int? IdAdministradorReg { get; set; }

    public string ClaveAdmin { get; set; } = string.Empty;

    public string CajaAsignada { get; set; } = string.Empty;
}