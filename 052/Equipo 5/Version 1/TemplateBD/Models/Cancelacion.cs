using System;

namespace TemplateCSharp.Models;

public class Cancelacion
{
    public int FolioCancelacion { get; set; }
    public string FolioVenta { get; set; } = string.Empty;
    public int? IdCliente { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Motivo { get; set; } = string.Empty;
    public DateTime FechaHoraCancelacion { get; set; }
}