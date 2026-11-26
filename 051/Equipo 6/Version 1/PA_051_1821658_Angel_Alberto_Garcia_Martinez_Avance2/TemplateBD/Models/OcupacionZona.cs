using TemplateCSharp.Services;

namespace TemplateCSharp.Models;

// Ocupación de una zona en un partido
public class OcupacionZona
{
    public DateTime FechaPartido { get; set; }

    public string Estadio { get; set; } = string.Empty;

    public string EquipoLocal { get; set; } = string.Empty;

    public string EquipoVisitante { get; set; } = string.Empty;

    public string NombreZona { get; set; } = string.Empty;

    public decimal PrecioBase { get; set; }

    public int AsientosTotales { get; set; }

    public int AsientosVendidos { get; set; }

    public int AsientosDisponibles => AsientosTotales - AsientosVendidos;

    public decimal PorcentajeOcupacion => AsientosTotales == 0 ? 0 : 100m * AsientosVendidos / AsientosTotales;

    public string OcupacionTexto => $"{PorcentajeOcupacion:0.0}%";

    public string Partido => $"{EquipoLocal} vs {EquipoVisitante} ({Formato.Fecha(FechaPartido)})";
}
