using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class PermisoService : IPermisoService
{
    private readonly IPermisoRepository _permisoRepository;

    public PermisoService(IPermisoRepository permisoRepository)
    {
        _permisoRepository = permisoRepository;
    }

    public Task<List<Permiso>> ObtenerPermisosPorTipoAsync(int idTipo)
    {
        return _permisoRepository.ObtenerPermisosPorTipoAsync(idTipo);
    }
}