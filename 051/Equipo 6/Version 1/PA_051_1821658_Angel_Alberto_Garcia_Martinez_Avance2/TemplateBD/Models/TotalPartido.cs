using TemplateCSharp.Services;

namespace TemplateCSharp.Models;

// Totales de un partido en el reporte de ventas
public class TotalPartido
{
    public string Estadio { get; set; } = string.Empty;

    public string EquipoLocal { get; set; } = string.Empty;

    public string EquipoVisitante { get; set; } = string.Empty;

    public DateTime FechaPartido { get; set; }

    public int Ventas { get; set; }

    public int Boletos { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public string Partido => $"{EquipoLocal} vs {EquipoVisitante}";

    public string FechaPartidoTexto => Formato.Fecha(FechaPartido);
}
