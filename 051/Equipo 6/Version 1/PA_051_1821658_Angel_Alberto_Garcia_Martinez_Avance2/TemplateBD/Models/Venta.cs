namespace TemplateCSharp.Models;

public class Venta
{
    public int IdCliente { get; set; }

    public int IdPartido { get; set; }

    public string MetodoPago { get; set; } = string.Empty;

    public decimal TasaIva { get; set; }

    public List<Boleto> Boletos { get; set; } = [];

    public decimal Subtotal
    {
        get
        {
            decimal subtotal = 0;
            foreach (Boleto boleto in Boletos)
            {
                subtotal += boleto.PrecioUnitario;
            }

            return subtotal;
        }
    }

    // IVA a dos decimales, redondeando .5 hacia arriba
    public decimal Iva => Math.Round(Subtotal * TasaIva, 2, MidpointRounding.AwayFromZero);

    public decimal Total => Subtotal + Iva;

    public int ContarBoletosDeZona(int idZona)
    {
        int cantidad = 0;
        foreach (Boleto boleto in Boletos)
        {
            if (boleto.IdZona == idZona)
            {
                cantidad++;
            }
        }

        return cantidad;
    }
}
