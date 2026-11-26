using TemplateCSharp.Services;

namespace TemplateCSharp.Models;

// Una venta resumida con sus totales
public class ResumenVenta
{
    public Guid Folio { get; set; }

    public DateTime FechaVenta { get; set; }

    public string Cliente { get; set; } = string.Empty;

    public DateTime FechaPartido { get; set; }

    public string Estadio { get; set; } = string.Empty;

    public string EquipoLocal { get; set; } = string.Empty;

    public string EquipoVisitante { get; set; } = string.Empty;

    // Zonas de los boletos, separadas por coma
    public string Zonas { get; set; } = string.Empty;

    public int CantidadBoletos { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public string MetodoPago { get; set; } = string.Empty;

    public string FolioFactura { get; set; } = string.Empty;

    public bool Cancelada { get; set; }

    public string Partido => $"{EquipoLocal} vs {EquipoVisitante}";

    public string FechaVentaTexto => Formato.FechaHora(FechaVenta);

    public string FechaCompraTexto => Formato.Fecha(FechaVenta);

    public string FechaPartidoTexto => Formato.Fecha(FechaPartido);

    public string Estado => Cancelada ? "Cancelada" : "Vigente";

    public string FacturaTexto => Cancelada ? $"{FolioFactura} (cancelada)" : FolioFactura;
}
