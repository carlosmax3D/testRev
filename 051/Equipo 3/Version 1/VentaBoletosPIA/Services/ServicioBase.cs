using VentaBoletosApp.Interfaces.Services;

namespace VentaBoletosApp.Services;

/// <summary>
/// Clase base de los servicios: concentra las verificaciones de sesion y de
/// permisos para no repetirlas en cada servicio.
/// </summary>
public abstract class ServicioBase
{
    protected ServicioBase(ISesionActual sesion)
    {
        Sesion = sesion;
    }

    protected ISesionActual Sesion { get; }

    protected void ExigirSesion()
    {
        if (!Sesion.HaySesionIniciada)
        {
            throw new UnauthorizedAccessException("Debe iniciar sesión para realizar esta operación.");
        }
    }

    protected void ExigirAdministrador()
    {
        ExigirSesion();

        if (!Sesion.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un usuario administrador puede realizar esta operación.");
        }
    }

    protected static void ValidarTexto(string? valor, string campo, int longitudMaxima)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ArgumentException($"El campo \"{campo}\" es obligatorio.");
        }

        if (valor.Trim().Length > longitudMaxima)
        {
            throw new ArgumentException($"El campo \"{campo}\" no puede superar los {longitudMaxima} caracteres.");
        }
    }
}
