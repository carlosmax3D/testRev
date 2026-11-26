namespace TemplateCSharp.Models;

/// <summary>Renglón de la compra: una zona y la cantidad de boletos.</summary>
public class DetalleVenta
{
    public int IdZona { get; set; }

    public string NombreZona { get; set; } = string.Empty;

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Importe => PrecioUnitario * Cantidad;
}
