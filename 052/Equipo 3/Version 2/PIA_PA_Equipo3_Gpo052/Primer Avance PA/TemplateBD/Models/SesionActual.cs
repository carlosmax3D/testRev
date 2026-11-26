namespace TemplateCSharp.Models;

public static class SesionActual
{
    public static int IdUsuario { get; set; } = 0;
    public static string NombreUsuario { get; set; } = string.Empty;
    public static string RolUsuario { get; set; } = string.Empty;
    
    public static void CerrarSesion()
    {
        IdUsuario = 0;
        NombreUsuario = string.Empty;
        RolUsuario = string.Empty;
    }
}
