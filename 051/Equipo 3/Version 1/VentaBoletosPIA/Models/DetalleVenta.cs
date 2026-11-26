namespace VentaBoletosApp.Models;

public class DetalleVenta
{
    public int IdDetalle { get; set; }

    public decimal PrecioUnitario { get; set; }

    public int IdVenta { get; set; }

    public int IdAsiento { get; set; }

    public string Fila { get; set; } = string.Empty;

    public int Numero { get; set; }

    public int IdZona { get; set; }

    public string NombreZona { get; set; } = string.Empty;

    public int IdEstadio { get; set; }
}
