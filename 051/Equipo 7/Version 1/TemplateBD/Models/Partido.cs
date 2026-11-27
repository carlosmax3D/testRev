using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models;

public class Partido
{
    public int IdPartido { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today.AddDays(1);
    public TimeSpan Hora { get; set; }
    public string Estado { get; set; } = "Programado";
    public int IdEstadio { get; set; }
    public int IdEquipoLocal { get; set; }
    public int IdEquipoVisitante { get; set; }
    public string UsuarioAuditoria { get; set; } = string.Empty;

    // Propiedades de lectura provenientes de vw_CarteleraPartidos
    public string Encuentro { get; set; } = string.Empty;
    public string Estadio { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string FechaFormateada { get; set; } = string.Empty;
}
