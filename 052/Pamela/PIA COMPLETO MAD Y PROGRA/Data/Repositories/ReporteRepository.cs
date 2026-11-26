using System.Data;
using TemplateCSharp.Interfaces.Repositories;

namespace TemplateCSharp.Data.Repositories;

/// <summary>
/// Reportes. No se guardan en la base de datos: se obtienen de la
/// información ya registrada, con los filtros como parámetros.
/// </summary>
public class ReporteRepository : RepositorioBase, IReporteRepository
{
    public ReporteRepository(DatabaseConnection database) : base(database)
    {
    }

    public DataTable Ventas(DateTime? fechaInicio, DateTime? fechaFin, int? idEstadio, int? idPartido)
    {
        return EjecutarTabla(
            "SP_ReporteVentas",
            Parametro("@p_fechaInicio", SqlDbType.Date, fechaInicio.HasValue ? (object)fechaInicio.Value.Date : null),
            Parametro("@p_fechaFin", SqlDbType.Date, fechaFin.HasValue ? (object)fechaFin.Value.Date : null),
            Parametro("@p_idEstadio", SqlDbType.Int, idEstadio),
            Parametro("@p_idPartido", SqlDbType.Int, idPartido));
    }

    public DataTable Ocupacion(int? idPartido, int? idZona, string? disponibilidad)
    {
        return EjecutarTabla(
            "SP_ReporteOcupacionDisponibilidad",
            Parametro("@p_idPartido", SqlDbType.Int, idPartido),
            Parametro("@p_idZona", SqlDbType.Int, idZona),
            ParametroTexto("@p_disponibilidad", disponibilidad, 15));
    }

    public DataTable HistorialCliente(int idCliente, string orden)
    {
        return EjecutarTabla(
            "SP_ReporteHistorialCliente",
            Parametro("@p_idCliente", SqlDbType.Int, idCliente),
            ParametroTexto("@p_orden", orden, 10));
    }
}
