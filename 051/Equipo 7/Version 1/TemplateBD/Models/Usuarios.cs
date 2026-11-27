using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string NumeroNomina { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public byte[] ContrasenaHash { get; set; } = [];
    public byte[] ContrasenaSalt { get; set; } = [];
    public DateTime FechaNacimiento { get; set; } = DateTime.Today.AddYears(-18);
    public string TipoUsuario { get; set; } = string.Empty;
    public string UsuarioAuditoria { get; set; } = string.Empty;

    public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}".Trim();

    public static Usuario CrearInstanciaPorRol(string tipo)
    {
        return tipo == "Administrador" ? new Administrador() : new Operativo();
    }
}

public class Administrador : Usuario
{
    public Administrador() => TipoUsuario = "Administrador";
}

public class Operativo : Usuario
{
    public Operativo() => TipoUsuario = "Operativo";
}

public static class SesionActual
{
    public static Usuario? UsuarioLogueado { get; set; }
}