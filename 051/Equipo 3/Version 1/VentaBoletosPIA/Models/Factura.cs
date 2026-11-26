namespace VentaBoletosApp.Models;

public class Factura
{
    public int IdFactura { get; set; }

    public string Folio { get; set; } = string.Empty;

    public DateTime Fecha { get; set; }

    public int IdEstadoFactura { get; set; }

    public string EstadoFactura { get; set; } = string.Empty;

    public int IdUsuario { get; set; }

    public int IdVenta { get; set; }

    public bool EstaVigente => string.Equals(EstadoFactura, "Vigente", StringComparison.OrdinalIgnoreCase);
}
