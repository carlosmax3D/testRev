namespace PIA_PA.Entidades.Contrasena;

public class Contrasena
{
    public int Contrasena_ID { get; set; }
public string Contrasena_Hash { get; set; } = string.Empty;
public DateTime Contrasena_Fecha { get; set; }
public int? Cliente_ID { get; set; }
public int? Usuario_ID { get; set; }
public bool Contrasena_Actual { get; set; }
}
