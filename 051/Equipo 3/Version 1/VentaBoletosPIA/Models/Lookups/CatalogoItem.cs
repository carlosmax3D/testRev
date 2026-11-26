namespace VentaBoletosApp.Models.Lookups;

/// <summary>
/// Representa un renglon generico de un catalogo simple de la base de datos
/// (TiposUsuario, MetodosPago, EstadosPartido, etc.). Se usa para llenar
/// ComboBox sin tener que crear una clase distinta por cada catalogo.
/// </summary>
public class CatalogoItem
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public override string ToString() => Nombre;
}
