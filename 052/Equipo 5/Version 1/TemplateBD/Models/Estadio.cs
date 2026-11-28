using System;

namespace TemplateCSharp.Models;

public class Estadio
{
    public int IdEstadio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string? Mapa { get; set; }
    public int CapacidadTotal { get; set; }
    public int IdUsuario { get; set; }
    public DateTime FechaHoraRegistro { get; set; }
}