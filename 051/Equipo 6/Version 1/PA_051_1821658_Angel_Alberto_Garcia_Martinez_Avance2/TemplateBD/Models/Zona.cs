namespace TemplateCSharp.Models;

public class Zona : RegistroConEstado
{
    public int IdZona { get; set; }

    public int IdEstadio { get; set; }

    public string NombreZona { get; set; } = string.Empty;

    public decimal PrecioBase { get; set; }

    public int Cupo { get; set; }

    public int AsientosGenerados { get; set; }

    // Asientos libres para un partido (solo en la venta)
    public int Disponibles { get; set; }

    public override string ToString()
    {
        return Activo ? NombreZona : $"{NombreZona} - inactiva";
    }
}
