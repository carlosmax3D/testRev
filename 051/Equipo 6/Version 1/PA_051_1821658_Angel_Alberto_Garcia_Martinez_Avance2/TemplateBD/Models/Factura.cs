namespace TemplateCSharp.Models;

public class Factura
{
    public string FolioFactura { get; set; } = string.Empty;

    public DateTime FechaHoraEmision { get; set; }

    public Guid FolioVenta { get; set; }

    public string MetodoPago { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;

    public string EmisorRazonSocial { get; set; } = string.Empty;

    public string EmisorRFC { get; set; } = string.Empty;

    public string EmisorDomicilio { get; set; } = string.Empty;

    public string ReceptorNombre { get; set; } = string.Empty;

    public string ReceptorRFC { get; set; } = string.Empty;

    public string ReceptorDomicilio { get; set; } = string.Empty;

    public decimal TasaIva { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Iva { get; set; }

    public decimal Total { get; set; }

    public List<DetalleFactura> Detalle { get; set; } = [];
}
