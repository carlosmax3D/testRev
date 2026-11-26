using System.Data;

namespace TemplateCSharp.Interfaces.Services;

public interface IReporteService
{
    DataTable ReporteVentas(DateTime fechaInicio, DateTime fechaFin, int? idEstadio, int? idPartido);

    DataTable ReporteOcupacion(int? idPartido, int? idZona, string? disponibilidad);

    DataTable HistorialCliente(int idCliente, string orden);
}
