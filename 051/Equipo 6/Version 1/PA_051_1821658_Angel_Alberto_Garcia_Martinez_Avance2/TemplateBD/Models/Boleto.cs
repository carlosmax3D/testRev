namespace TemplateCSharp.Models;

// Boleto de la compra en captura: un asiento con su precio
public class Boleto
{
    public int IdAsiento { get; set; }

    public int IdZona { get; set; }

    public string NombreZona { get; set; } = string.Empty;

    public string Fila { get; set; } = string.Empty;

    public int Numero { get; set; }

    public decimal PrecioUnitario { get; set; }

    public string Ubicacion => $"{NombreZona} {Fila}-{Numero}";
}
