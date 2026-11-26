namespace TemplateCSharp.Models;

public class ReporteOcupacion
{
    public List<OcupacionZona> Zonas { get; set; } = [];

    // Del partido elegido (o de todos); no cambia con los filtros de zona ni de disponibilidad
    public int BoletosVendidos { get; set; }
}
