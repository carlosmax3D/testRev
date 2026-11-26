namespace PIA_PA.Entidades.Pago;

public class Pago
{
    public int Pago_ID { get; set; }
public string Pago_Metodo { get; set; } = string.Empty;
public DateOnly Pago_Fecha { get; set; }
public double Pago_Cantidad { get; set; }
}
