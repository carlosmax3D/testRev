namespace TemplateCSharp.Models;

public class ReporteVentas
{
    // Una fila por venta, ordenadas por estadio y partido
    public List<ResumenVenta> Ventas { get; set; } = [];

    public List<TotalPartido> Partidos { get; set; } = [];

    public decimal IngresoTotal
    {
        get
        {
            decimal total = 0;
            foreach (ResumenVenta venta in Ventas)
            {
                total += venta.Total;
            }

            return total;
        }
    }

    public int BoletosVendidos
    {
        get
        {
            int boletos = 0;
            foreach (ResumenVenta venta in Ventas)
            {
                boletos += venta.CantidadBoletos;
            }

            return boletos;
        }
    }
}
