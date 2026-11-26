namespace TemplateCSharp.Models;

public class Zona
{
    public int IdZona { get; set; }

    public int IdEstadio { get; set; }

    public string NombreZona { get; set; } = string.Empty;

    public int Asientos { get; set; }

    public decimal PrecioBase { get; set; }
}
