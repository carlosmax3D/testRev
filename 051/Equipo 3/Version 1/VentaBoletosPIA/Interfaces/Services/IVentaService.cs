using VentaBoletosApp.Models;
using VentaBoletosApp.Models.Dtos;

namespace VentaBoletosApp.Interfaces.Services;

public interface IVentaService
{
    ResumenCompra CalcularResumen(IEnumerable<decimal> preciosBoletos);

    List<Asiento> ObtenerAsientosDisponibles(int idZona, int idPartido);

    ResultadoVenta RegistrarVenta(SolicitudVenta solicitud);

    /// <summary>Busca una venta (con su detalle) por el folio GUID. Null si no existe.</summary>
    Venta? BuscarPorFolio(string folioTexto);
}
