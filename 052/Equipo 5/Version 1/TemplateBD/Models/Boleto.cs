using System;

namespace TemplateCSharp.Models;

public class Boleto
{
    public int FolioBoleto { get; set; }
    public string FolioVenta { get; set; } = string.Empty;
    public int IdPartido { get; set; }
    public string PartidoNombre { get; set; } = string.Empty;
    public int IdZona { get; set; }
    public string Fila { get; set; } = string.Empty;
    public string NumAsiento { get; set; } = string.Empty;
    public decimal PrecioUnitario { get; set; }
}