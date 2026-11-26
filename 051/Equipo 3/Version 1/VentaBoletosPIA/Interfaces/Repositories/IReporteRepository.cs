using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IReporteRepository
{
    List<ReporteVentaItem> ReporteVentas(DateTime? fechaDesde, DateTime? fechaHasta, int? idEstadio, int? idPartido);

    List<DisponibilidadZona> ReporteOcupacion(int? idPartido, int? idZona, bool soloConDisponibles);

    List<HistorialClienteItem> HistorialCliente(string rfc);
}
