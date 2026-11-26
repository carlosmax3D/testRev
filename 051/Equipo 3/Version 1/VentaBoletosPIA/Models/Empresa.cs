namespace VentaBoletosApp.Models;

/// <summary>
/// Datos del emisor que aparecen en la factura (la empresa que vende los
/// boletos). Se guarda un unico renglon en la base de datos.
/// </summary>
public class Empresa
{
    public int IdEmpresa { get; set; }

    public string RazonSocial { get; set; } = string.Empty;

    public string RFC { get; set; } = string.Empty;

    public string DomicilioCompleto { get; set; } = string.Empty;
}
