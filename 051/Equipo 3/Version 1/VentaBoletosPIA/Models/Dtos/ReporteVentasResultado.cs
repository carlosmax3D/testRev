namespace VentaBoletosApp.Models.Dtos;

public class ResumenPartidoItem
{
    public string Partido { get; set; } = string.Empty;

    public string Estadio { get; set; } = string.Empty;

    public DateTime FechaPartido { get; set; }

    public int Boletos { get; set; }

    public decimal Ingresos { get; set; }
}

public class ReporteVentasResultado
{
    public List<ReporteVentaItem> Detalle { get; set; } = [];

    /// <summary>Informacion agrupada por partido.</summary>
    public List<ResumenPartidoItem> PorPartido { get; set; } = [];

    public decimal IngresoTotal { get; set; }

    public int BoletosTotales { get; set; }
}
