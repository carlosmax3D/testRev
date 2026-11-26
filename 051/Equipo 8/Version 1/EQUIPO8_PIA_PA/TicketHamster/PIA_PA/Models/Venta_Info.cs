namespace PIA_PA.Entidades.Venta_Info;

public class Venta_Info
{
    public int Venta_Info_ID { get; set; }
public DateOnly Venta_Info_Fecha { get; set; }
public double Venta_Info_Subtotal { get; set; }
public double Venta_Info_IVA { get; set; }
public double Venta_Info_Total { get; set; }
public int Usuario_ID { get; set; }
public int Pago_ID { get; set; }
public string Pagador_RFC { get; set; } = string.Empty;
public int Emisor_ID { get; set; }
}
