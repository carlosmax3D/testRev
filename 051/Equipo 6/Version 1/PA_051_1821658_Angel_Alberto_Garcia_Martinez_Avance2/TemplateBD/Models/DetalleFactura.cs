using TemplateCSharp.Services;

namespace TemplateCSharp.Models;

// Un renglón de la factura: los boletos de una zona
public class DetalleFactura
{
    public string EquipoLocal { get; set; } = string.Empty;

    public string EquipoVisitante { get; set; } = string.Empty;

    public DateTime FechaPartido { get; set; }

    public string Estadio { get; set; } = string.Empty;

    public string Zona { get; set; } = string.Empty;

    // Fila-número de cada asiento, separados por coma
    public string Asientos { get; set; } = string.Empty;

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Importe { get; set; }

    public string Partido => $"{EquipoLocal} vs {EquipoVisitante} ({Formato.FechaHora(FechaPartido)})";
}
