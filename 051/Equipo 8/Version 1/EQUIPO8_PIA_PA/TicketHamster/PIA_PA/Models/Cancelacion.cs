namespace PIA_PA.Entidades.Cancelacion;

public class Cancelacion
{
    public int Cancelacion_ID { get; set; }
public string Cancelacion_Motivo { get; set; } = string.Empty;
public DateOnly Cancelacion_Fecha { get; set; }
public int Ticket_ID { get; set; }
}
