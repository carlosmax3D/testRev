namespace TemplateCSharp.Models;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string CorreoElectronico { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string? ApellidoMaterno { get; set; }
    public string NumeroNomina { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string TipoUsuario { get; set; } = string.Empty;

    // Fecha y hora en que se registró el usuario (la asigna la base de datos)
    public DateTime? FechaRegistro { get; set; }

    // Método virtual para demostrar Polimorfismo
    public virtual string ObtenerRol()
    {
        return "Usuario Genérico";
    }
}
