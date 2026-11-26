namespace VentaBoletosApp.Models;

/// <summary>
/// Un asiento disponible dentro de una zona (se genera automaticamente al
/// crear la zona). Se usa para mostrar la lista de asientos libres al
/// vender boletos.
/// </summary>
public class Asiento
{
    public int IdAsiento { get; set; }

    public string Fila { get; set; } = string.Empty;

    public int Numero { get; set; }

    public override string ToString() => $"Fila {Fila} - Asiento {Numero}";
}
