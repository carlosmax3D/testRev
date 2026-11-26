namespace VentaBoletosApp.Models;

public class Partido : EntidadAuditable
{
    public int IdPartido { get; set; }

    public string EquipoLocal { get; set; } = string.Empty;

    public string EquipoVisitante { get; set; } = string.Empty;

    public DateTime FechaHora { get; set; }

    public int IdEstadio { get; set; }

    public string NombreEstadio { get; set; } = string.Empty;

    public string CiudadEstadio { get; set; } = string.Empty;

    public int IdEstadoPartido { get; set; }

    public string EstadoPartido { get; set; } = string.Empty;

    public string DescripcionCorta => $"{EquipoLocal} vs {EquipoVisitante}";
}
