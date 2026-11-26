using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Services;

/// <summary>
/// Guarda quien inicio sesion mientras la aplicacion esta abierta, para
/// registrar "usuario, fecha y hora" en cada operacion y para aplicar los
/// permisos de administrador / operativo.
/// </summary>
public interface ISesionActual
{
    int IdUsuario { get; }

    string Correo { get; }

    string NombreUsuario { get; }

    string TipoUsuario { get; }

    bool EsAdministrador { get; }

    bool HaySesionIniciada { get; }

    void IniciarSesion(Usuario usuario);

    void CerrarSesion();
}
