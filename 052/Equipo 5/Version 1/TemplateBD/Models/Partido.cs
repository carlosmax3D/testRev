using System;

namespace TemplateCSharp.Models;

public class Partido
{
    public int IdPartido { get; set; }
    public int IdEstadio { get; set; }
    public string NombreEstadio { get; set; } = string.Empty;
    public string EquipoLocal { get; set; } = string.Empty;
    public string EquipoVisitante { get; set; } = string.Empty;
    public DateTime FechaPartido { get; set; }
    public TimeSpan HoraPartido { get; set; }
}