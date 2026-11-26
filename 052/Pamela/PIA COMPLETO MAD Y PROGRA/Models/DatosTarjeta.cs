namespace TemplateCSharp.Models;

/// <summary>
/// Datos de la tarjeta capturados en el pago simulado.
/// Solo se validan: por seguridad NO se guardan en la base de datos.
/// </summary>
public class DatosTarjeta
{
    public string Titular { get; set; } = string.Empty;

    public string Numero { get; set; } = string.Empty;

    public int MesExpiracion { get; set; }

    public int AnioExpiracion { get; set; }

    public string Cvv { get; set; } = string.Empty;
}
