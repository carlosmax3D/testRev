using VentaBoletosApp.Models;
using VentaBoletosApp.Models.Dtos;

namespace VentaBoletosApp.Interfaces.Services;

public enum OrdenHistorial
{
    /// <summary>Fecha de compra, de la mas lejana a la mas reciente.</summary>
    FechaCompra,

    /// <summary>Fecha de partido, de los pendientes a los ya realizados.</summary>
    FechaPartido
}

public interface IReporteService
{
    ReporteVentasResultado ReporteVentas(DateTime? fechaDesde, DateTime? fechaHasta, int? idEstadio, int? idPartido);

    ReporteOcupacionResultado ReporteOcupacion(int? idPartido, int? idZona, bool soloConDisponibles);

    List<HistorialClienteItem> HistorialCliente(string rfc, OrdenHistorial orden);
}
