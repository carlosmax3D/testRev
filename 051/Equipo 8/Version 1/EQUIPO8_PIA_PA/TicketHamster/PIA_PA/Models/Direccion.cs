namespace PIA_PA.Entidades.Direccion;

public class Direccion
{
    public int Direccion_ID { get; set; }
public int Direccion_Numero { get; set; }
public string Direccion_Calle { get; set; } = string.Empty;
public int Direccion_CP { get; set; }
public string Direccion_Colonia { get; set; } = string.Empty;
public string Direccion_Ciudad { get; set; } = string.Empty;
public string Direccion_Estado { get; set; } = string.Empty;
public string Direccion_Pais { get; set; } = string.Empty;
}
