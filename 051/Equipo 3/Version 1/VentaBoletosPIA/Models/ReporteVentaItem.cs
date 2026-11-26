namespace VentaBoletosApp.Models;

/// <summary>Un renglon del reporte de ventas (agrupado por venta+zona).</summary>
public class ReporteVentaItem
{
    public Guid Folio { get; set; }

    public DateTime FechaVenta { get; set; }

    public string NombreEstadio { get; set; } = string.Empty;

    public string EquipoLocal { get; set; } = string.Empty;

    public string EquipoVisitante { get; set; } = string.Empty;

    public DateTime FechaPartido { get; set; }

    public string NombreZona { get; set; } = string.Empty;

    public int CantidadBoletos { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public string MetodoPago { get; set; } = string.Empty;
}
