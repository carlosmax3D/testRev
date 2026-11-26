using System.Data;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IReporteRepository
{
    DataTable Ventas(DateTime? fechaInicio, DateTime? fechaFin, int? idEstadio, int? idPartido);

    DataTable Ocupacion(int? idPartido, int? idZona, string? disponibilidad);

    DataTable HistorialCliente(int idCliente, string orden);
}
