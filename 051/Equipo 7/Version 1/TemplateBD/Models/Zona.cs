using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models;

public class Zona
{
    public int IdZona { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioBase { get; set; }
    public int Capacidad { get; set; }
    public int IdEstadio { get; set; }
    public string NombreEstadio { get; set; } = string.Empty;
    public string UsuarioAuditoria { get; set; } = string.Empty;
}