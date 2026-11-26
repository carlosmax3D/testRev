using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IPermisoService
{
    Task<List<Permiso>> ObtenerPermisosPorTipoAsync(int idTipo);
}