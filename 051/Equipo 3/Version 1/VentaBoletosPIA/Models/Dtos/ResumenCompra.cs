namespace VentaBoletosApp.Models.Dtos;

public class ResumenCompra
{
    public int CantidadBoletos { get; set; }

    public decimal Subtotal { get; set; }

    public decimal IVA { get; set; }

    public decimal Total => Subtotal + IVA;
}
