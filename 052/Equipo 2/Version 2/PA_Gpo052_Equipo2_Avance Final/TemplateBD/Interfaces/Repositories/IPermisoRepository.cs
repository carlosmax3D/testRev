using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IPermisoRepository
    {
        Task<List<Permiso>> ObtenerPermisosPorTipoAsync(int idTipo);
    }
}
