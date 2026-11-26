namespace VentaBoletosApp.Models.Dtos;

public class SolicitudVenta
{
    public string RfcCliente { get; set; } = string.Empty;

    public int IdPartido { get; set; }

    public int IdMetodoPago { get; set; }

    public List<int> IdsAsiento { get; set; } = [];

    public DatosTarjeta? Tarjeta { get; set; }
}
