namespace PIA_PA.Entidades.Ticket;

public class Ticket
{
    public int Ticket_ID { get; set; }
public string Ticket_Estado { get; set; } = string.Empty;
public int Cliente_ID { get; set; }
public int Venta_Info_ID { get; set; }
public int Partido_ID { get; set; }
public int Asiento_ID { get; set; }
}
