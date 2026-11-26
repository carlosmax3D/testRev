namespace VentaBoletosApp.Models;

public class Venta
{
    public int IdVenta { get; set; }

    public Guid Folio { get; set; }

    public DateTime FechaVenta { get; set; }

    public decimal Subtotal { get; set; }

    public decimal IVA { get; set; }

    public decimal Total { get; set; }

    public int IdMetodoPago { get; set; }

    public string MetodoPago { get; set; } = string.Empty;

    public int IdEstadoVenta { get; set; }

    public string EstadoVenta { get; set; } = string.Empty;

    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = string.Empty;

    public int IdPartido { get; set; }

    public string EquipoLocal { get; set; } = string.Empty;

    public string EquipoVisitante { get; set; } = string.Empty;

    public DateTime FechaPartido { get; set; }

    public int IdEstadio { get; set; }

    public string NombreEstadio { get; set; } = string.Empty;

    public string RFC { get; set; } = string.Empty;

    public string NombreCliente { get; set; } = string.Empty;

    public bool EstaActiva => string.Equals(EstadoVenta, "Activa", StringComparison.OrdinalIgnoreCase);

    public List<DetalleVenta> Detalles { get; set; } = [];
}
