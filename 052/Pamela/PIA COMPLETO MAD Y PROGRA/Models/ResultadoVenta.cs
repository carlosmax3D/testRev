namespace TemplateCSharp.Models;

/// <summary>Datos que devuelve la base de datos al terminar una venta.</summary>
public class ResultadoVenta
{
    public int IdVenta { get; set; }

    public string FolioCompra { get; set; } = string.Empty;

    public string FolioFactura { get; set; } = string.Empty;

    public decimal Subtotal { get; set; }

    public decimal IVA { get; set; }

    public decimal Total { get; set; }
}
