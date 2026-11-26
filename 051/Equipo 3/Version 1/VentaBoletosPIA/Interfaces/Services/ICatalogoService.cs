using VentaBoletosApp.Models.Lookups;

namespace VentaBoletosApp.Interfaces.Services;

public interface ICatalogoService
{
    List<CatalogoItem> ObtenerTiposUsuario();

    List<CatalogoItem> ObtenerMetodosPago();
}
