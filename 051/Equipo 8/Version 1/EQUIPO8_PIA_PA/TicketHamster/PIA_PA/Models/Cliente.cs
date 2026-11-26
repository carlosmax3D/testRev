namespace PIA_PA.Entidades.Cliente;

public class Cliente
{
    public int Cliente_ID { get; set; }
public string Cliente_RFC { get; set; } = string.Empty;
public string Cliente_Email { get; set; } = string.Empty;
public DateOnly Cliente_Fecha_Nacimiento { get; set; }
public string Cliente_Nombre { get; set; } = string.Empty;
public string? Cliente_Segundo_Nombre { get; set; }
public string Cliente_Apellido_Paterno { get; set; } = string.Empty;
public string? Cliente_Apellido_Materno { get; set; }
public int Direccion_ID { get; set; }
public int Telefono_ID { get; set; }
}
