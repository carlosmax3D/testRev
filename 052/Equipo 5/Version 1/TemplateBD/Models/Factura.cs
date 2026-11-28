using System;

namespace TemplateCSharp.Models;

public class Factura
{
    public int FolioFactura { get; set; }
    public string FolioVenta { get; set; } = string.Empty;
    public DateTime FechaHoraFactura { get; set; }
    public string NomEmisor { get; set; } = string.Empty;
    public string RfcEmisor { get; set; } = string.Empty;
    public string DomEmisor { get; set; } = string.Empty;
    public string NomReceptor { get; set; } = string.Empty;
    public string RfcReceptor { get; set; } = string.Empty;
    public string DomReceptor { get; set; } = string.Empty;
    public string Estado { get; set; } = "activa";
}