namespace VentaBoletosApp.Models;

/// <summary>
/// Clase base para las entidades que registran quien y cuando se dieron de
/// alta. Usuario, Cliente, Estadio y Partido heredan de aqui (herencia)
/// para no repetir estas propiedades en cada clase.
/// </summary>
public abstract class EntidadAuditable
{
    public DateTime FechaRegistro { get; set; }

    public int? IdUsuarioRegistro { get; set; }
}
