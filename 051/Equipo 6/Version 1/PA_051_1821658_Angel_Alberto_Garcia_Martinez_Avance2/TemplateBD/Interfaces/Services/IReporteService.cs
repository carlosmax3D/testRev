using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IReporteService
{
    // El reporte de ventas y sus filtros los pueden consultar administradores y operativos
    ReporteVentas ObtenerReporteVentas(DateTime fechaInicial, DateTime fechaFinal, int? idEstadio, int? idPartido);

    List<Estadio> ObtenerEstadiosParaFiltro();

    List<Partido> ObtenerPartidosParaFiltro();

    ReporteOcupacion ObtenerOcupacion(int? idPartido, int? idZona, bool soloDisponibles);

    List<ResumenVenta> ObtenerHistorialCliente(int idCliente, bool ordenarPorPartido);
}
