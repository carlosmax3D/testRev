namespace TemplateCSharp.Models;

public class Asiento
{
    public int IdAsiento { get; set; }

    public int IdZona { get; set; }

    public string Fila { get; set; } = string.Empty;

    public int Numero { get; set; }
}
