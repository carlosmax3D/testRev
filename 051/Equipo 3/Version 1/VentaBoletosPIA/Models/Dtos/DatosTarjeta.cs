namespace VentaBoletosApp.Models.Dtos;

/// <summary>
/// Datos de tarjeta capturados para SIMULAR el pago con credito/debito.
/// Solo se validan; nunca se guardan en la base de datos.
/// </summary>
public class DatosTarjeta
{
    public string Titular { get; set; } = string.Empty;

    public string Numero { get; set; } = string.Empty;

    /// <summary>Formato MM/AA.</summary>
    public string Expiracion { get; set; } = string.Empty;

    public string CCV { get; set; } = string.Empty;
}
