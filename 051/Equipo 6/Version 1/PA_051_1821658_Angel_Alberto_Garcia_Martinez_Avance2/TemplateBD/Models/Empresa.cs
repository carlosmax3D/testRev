namespace TemplateCSharp.Models;

// Empresa que vende los boletos; sus datos aparecen como emisor en las facturas
public class Empresa
{
    public string RazonSocial { get; set; } = string.Empty;

    public string RFC { get; set; } = string.Empty;

    public string Domicilio { get; set; } = string.Empty;

    public DateTime FechaModificacion { get; set; }

    // Administrador que capturó los datos por última vez
    public string ModificadoPor { get; set; } = string.Empty;
}
