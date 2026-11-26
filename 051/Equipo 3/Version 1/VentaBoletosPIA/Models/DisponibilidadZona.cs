namespace VentaBoletosApp.Models;

/// <summary>
/// Resultado de consultar cuantos asientos hay, cuantos se han vendido y
/// cuantos quedan disponibles para una zona en un partido especifico.
/// Se usa tanto en la pantalla de venta de boletos como en el reporte de
/// ocupacion y disponibilidad.
/// </summary>
public class DisponibilidadZona
{
    public int IdZona { get; set; }

    public string NombreZona { get; set; } = string.Empty;

    public decimal Precio { get; set; }

    public int AsientosTotales { get; set; }

    public int AsientosVendidos { get; set; }

    public int AsientosDisponibles { get; set; }

    public decimal PorcentajeOcupacion { get; set; }

    public int? IdPartido { get; set; }

    public string? DescripcionPartido { get; set; }

    public DateTime? FechaPartido { get; set; }
}
