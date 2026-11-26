namespace VentaBoletosApp.Models.Dtos;

public class ResultadoVenta
{
    public int IdVenta { get; set; }

    public Guid Folio { get; set; }

    public string? FolioFactura { get; set; }

    public ResumenCompra Resumen { get; set; } = new();
}
