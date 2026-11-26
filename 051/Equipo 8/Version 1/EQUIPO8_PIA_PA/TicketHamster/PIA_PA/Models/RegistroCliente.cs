namespace PIA_PA.Entidades.Cliente;

public class RegistroCliente
{
    public string RFC { get; set; } = "";
    public string Correo { get; set; } = "";
    public DateTime FechaNacimiento { get; set; }
    public string Nombre { get; set; } = "";
    public string SegundoNombre { get; set; } = "";
    public string ApellidoPaterno { get; set; } = "";
    public string ApellidoMaterno { get; set; } = "";
    public int Numero { get; set; }
    public string Calle { get; set; } = "";
    public string Colonia { get; set; } = "";
    public int? TelefonoFijo { get; set; }
    public int? Celular { get; set; }
    public string Contrasena { get; set; } = "";
}
