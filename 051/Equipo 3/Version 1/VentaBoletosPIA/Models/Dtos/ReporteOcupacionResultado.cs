namespace VentaBoletosApp.Models.Dtos;

public class ReporteOcupacionResultado
{
    public List<DisponibilidadZona> Items { get; set; } = [];

    /// <summary>Boletos vendidos en total (suma de lo mostrado).</summary>
    public int BoletosVendidosTotal { get; set; }
}
