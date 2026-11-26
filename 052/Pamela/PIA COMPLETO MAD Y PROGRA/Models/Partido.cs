namespace TemplateCSharp.Models;

public class Partido
{
    public int IdPartido { get; set; }

    public int IdEstadio { get; set; }

    public DateTime FechaHora { get; set; }

    public string EquipoLocal { get; set; } = string.Empty;

    public string EquipoVisitante { get; set; } = string.Empty;

    public string Estado { get; set; } = "Programado";
}
