using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Services;

public class SesionActual : ISesionActual
{
    public int IdUsuario { get; private set; }

    public string Correo { get; private set; } = string.Empty;

    public string NombreUsuario { get; private set; } = string.Empty;

    public string TipoUsuario { get; private set; } = string.Empty;

    public bool EsAdministrador =>
        HaySesionIniciada && string.Equals(TipoUsuario, "Administrador", StringComparison.OrdinalIgnoreCase);

    public bool HaySesionIniciada { get; private set; }

    public void IniciarSesion(Usuario usuario)
    {
        IdUsuario = usuario.IdUsuario;
        Correo = usuario.Correo;
        NombreUsuario = usuario.NombreCompleto;
        TipoUsuario = usuario.TipoUsuario;
        HaySesionIniciada = true;
    }

    public void CerrarSesion()
    {
        IdUsuario = 0;
        Correo = string.Empty;
        NombreUsuario = string.Empty;
        TipoUsuario = string.Empty;
        HaySesionIniciada = false;
    }
}
