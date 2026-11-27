using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models;

public class Estadio
{
    public int IdEstadio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public string MapaEstadio { get; set; } = string.Empty;
    public string UsuarioAuditoria { get; set; } = string.Empty;
}
