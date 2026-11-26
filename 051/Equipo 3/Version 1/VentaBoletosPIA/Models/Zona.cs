namespace VentaBoletosApp.Models;

public class Zona : EntidadAuditable
{
    public int IdZona { get; set; }

    public string NombreZona { get; set; } = string.Empty;

    public decimal Precio { get; set; }

    public int Cupo { get; set; }

    public int IdEstadio { get; set; }

    public string NombreEstadio { get; set; } = string.Empty;
}
