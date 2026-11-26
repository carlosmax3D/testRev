namespace VentaBoletosApp.Models;

/// <summary>Un renglon del historial de compras de un cliente.</summary>
public class HistorialClienteItem
{
    public Guid Folio { get; set; }

    public DateTime FechaVenta { get; set; }

    public string EquipoLocal { get; set; } = string.Empty;

    public string EquipoVisitante { get; set; } = string.Empty;

    public string NombreEstadio { get; set; } = string.Empty;

    public DateTime FechaPartido { get; set; }

    public string NombreZona { get; set; } = string.Empty;

    public int CantidadBoletos { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public string MetodoPago { get; set; } = string.Empty;

    public string EstadoVenta { get; set; } = string.Empty;

    public string? FolioFactura { get; set; }

    public bool EsFuturo => FechaPartido >= DateTime.Today;
}
