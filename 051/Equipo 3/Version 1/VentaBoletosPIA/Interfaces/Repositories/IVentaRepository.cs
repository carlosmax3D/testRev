using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IVentaRepository
{
    /// <summary>
    /// Registra la venta completa (encabezado, boletos y factura) de forma
    /// atomica mediante sp_Venta_Registrar y regresa (IdVenta, Folio).
    /// </summary>
    (int IdVenta, Guid Folio) Registrar(
        int idMetodoPago, int idUsuario, int idPartido, string rfc, IReadOnlyCollection<int> idsAsiento);

    Venta? GetByFolio(Guid folio);

    List<DetalleVenta> GetDetallesPorVenta(int idVenta);

    bool PuedeCancelarse(int idVenta);
}
