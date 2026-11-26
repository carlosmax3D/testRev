using TemplateCSharp.Services;

namespace TemplateCSharp.Models;

public class Partido : RegistroConEstado
{
    public int IdPartido { get; set; }

    public int IdEstadio { get; set; }

    public string Estadio { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;

    public int IdEquipoLocal { get; set; }

    public string EquipoLocal { get; set; } = string.Empty;

    public int IdEquipoVisitante { get; set; }

    public string EquipoVisitante { get; set; } = string.Empty;

    public DateTime FechaHora { get; set; }

    // Programado, Terminado o Cancelado
    public string Estado { get; set; } = string.Empty;

    // Ventas sin cancelar (solo en la consulta de partidos del administrador)
    public int VentasVigentes { get; set; }

    // Zonas con asientos libres (solo en la venta)
    public string ZonasDisponibles { get; set; } = string.Empty;

    public string FechaHoraTexto => Formato.FechaHora(FechaHora);

    public string EstadioCiudad => $"{Estadio} ({Ciudad})";

    public string Encuentro => $"{EquipoLocal} vs {EquipoVisitante}";

    public override string ToString()
    {
        return $"{Encuentro} - {Formato.Fecha(FechaHora)}";
    }

    // Búsqueda por fecha, estadio, equipo, estado o Activo/Inactivo
    public bool Coincide(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            return true;
        }

        string busqueda = texto.Trim();
        return FechaHoraTexto.Contains(busqueda, StringComparison.OrdinalIgnoreCase)
            || EstadioCiudad.Contains(busqueda, StringComparison.OrdinalIgnoreCase)
            || Encuentro.Contains(busqueda, StringComparison.OrdinalIgnoreCase)
            || Estado.Contains(busqueda, StringComparison.OrdinalIgnoreCase)
            || EstadoTexto.Equals(busqueda, StringComparison.OrdinalIgnoreCase);
    }
}
