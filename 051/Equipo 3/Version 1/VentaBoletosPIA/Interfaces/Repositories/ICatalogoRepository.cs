using VentaBoletosApp.Models.Lookups;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface ICatalogoRepository
{
    List<CatalogoItem> ObtenerTiposUsuario();

    List<CatalogoItem> ObtenerMetodosPago();

    List<CatalogoItem> ObtenerEstadosPartido();
}
