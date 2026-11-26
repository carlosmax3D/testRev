namespace TemplateCSharp.Models;

public class Estadio
{
    public int IdEstadio { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;

    public int CapacidadTotal { get; set; }

    /// <summary>Imagen del mapa del estadio (se guarda en la base de datos como VARBINARY).</summary>
    public byte[] MapaEstadio { get; set; } = Array.Empty<byte>();
}
