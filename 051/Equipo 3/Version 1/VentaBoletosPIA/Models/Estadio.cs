namespace VentaBoletosApp.Models;

public class Estadio : EntidadAuditable
{
    public int IdEstadio { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;

    public int Capacidad { get; set; }

    public string? MapaEstadio { get; set; }
}
