namespace TemplateCSharp.Models;

public class Estadio : RegistroConEstado
{
    public int IdEstadio { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;

    public int CapacidadTotal { get; set; }

    // Suma de los cupos de sus zonas activas
    public int CupoAsignado { get; set; }

    // Imagen del mapa: se llena al registrar o al modificar con un mapa nuevo (vacío = conservar el guardado).
    // La lista de estadios no la trae; se pide aparte con ObtenerMapa
    public byte[] MapaEstadio { get; set; } = [];

    public int CupoLibre => CapacidadTotal - CupoAsignado;

    public override string ToString()
    {
        return Activo ? $"{Nombre} ({Ciudad})" : $"{Nombre} ({Ciudad}) - inactivo";
    }
}
