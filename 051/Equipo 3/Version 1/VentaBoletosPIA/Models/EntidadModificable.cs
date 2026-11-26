namespace VentaBoletosApp.Models;

/// <summary>
/// Extiende EntidadAuditable para las entidades que ademas se pueden editar
/// o dar de baja (Usuario y Cliente): guarda quien y cuando hizo el ultimo
/// cambio.
/// </summary>
public abstract class EntidadModificable : EntidadAuditable
{
    public DateTime? FechaModificacion { get; set; }

    public int? IdUsuarioModificacion { get; set; }
}
