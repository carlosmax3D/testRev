using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IFacturaRepository
{
    Factura? GetByVenta(int idVenta);
}
