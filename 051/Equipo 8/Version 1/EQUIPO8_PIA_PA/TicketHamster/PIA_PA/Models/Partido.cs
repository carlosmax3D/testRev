namespace PIA_PA.Entidades.Partido;

public class Partido
{
    public int Partido_ID { get; set; }
public string Partido_Estado { get; set; } = string.Empty;
public DateOnly Partido_Fecha { get; set; }
public TimeOnly Partido_Hora { get; set; }
public int Equipo_ID_Local { get; set; }
public int Equipo_ID_Visitante { get; set; }
public int Estadio_ID { get; set; }
}
