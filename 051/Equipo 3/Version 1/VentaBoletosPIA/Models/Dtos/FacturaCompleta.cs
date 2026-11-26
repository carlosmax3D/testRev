namespace VentaBoletosApp.Models.Dtos;

/// <summary>
/// Toda la informacion necesaria para mostrar o exportar a PDF una factura:
/// emisor, receptor, datos del comprobante y detalle de boletos.
/// </summary>
public class FacturaCompleta
{
    public Empresa Emisor { get; set; } = new();

    public Cliente Receptor { get; set; } = new();

    public Factura Factura { get; set; } = new();

    public Venta Venta { get; set; } = new();

    public List<DetalleVenta> Detalles { get; set; } = [];
}
