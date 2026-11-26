using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models.Lookups;

namespace VentaBoletosApp.Services;

public class CatalogoService : ICatalogoService
{
    private readonly ICatalogoRepository _repository;

    public CatalogoService(ICatalogoRepository repository)
    {
        _repository = repository;
    }

    public List<CatalogoItem> ObtenerTiposUsuario() => _repository.ObtenerTiposUsuario();

    public List<CatalogoItem> ObtenerMetodosPago() => _repository.ObtenerMetodosPago();
}
