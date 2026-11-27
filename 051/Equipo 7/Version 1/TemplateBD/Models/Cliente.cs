using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models;

public class Cliente
{
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public string RFC { get; set; } = string.Empty;
    public string Domicilio { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string TelefonoCasa { get; set; } = string.Empty;
    public string TelefonoCelular { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; } = DateTime.Today.AddYears(-18);
    public string UsuarioAuditoria { get; set; } = string.Empty;

    public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}".Trim();
}