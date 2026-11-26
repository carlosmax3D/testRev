using System.Data;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;

namespace TemplateCSharp.Services;

/// <summary>Validación de los filtros de los reportes.</summary>
public class ReporteService : IReporteService
{
    private readonly IReporteRepository _repository;

    public ReporteService(IReporteRepository repository)
    {
        _repository = repository;
    }

    public DataTable ReporteVentas(DateTime fechaInicio, DateTime fechaFin, int? idEstadio, int? idPartido)
    {
        if (fechaInicio.Date > fechaFin.Date)
        {
            throw new ArgumentException("La fecha inicial no puede ser mayor que la fecha final.");
        }

        if (fechaInicio.Date > DateTime.Today)
        {
            throw new ArgumentException("La fecha inicial no puede ser posterior al día de hoy.");
        }

        return _repository.Ventas(fechaInicio.Date, fechaFin.Date, idEstadio, idPartido);
    }

    public DataTable ReporteOcupacion(int? idPartido, int? idZona, string? disponibilidad)
    {
        if (disponibilidad != null && disponibilidad != "Disponibles" && disponibilidad != "Agotados")
        {
            throw new ArgumentException("El filtro de disponibilidad no es válido.");
        }

        return _repository.Ocupacion(idPartido, idZona, disponibilidad);
    }

    public DataTable HistorialCliente(int idCliente, string orden)
    {
        if (idCliente <= 0)
        {
            throw new ArgumentException("Selecciona un cliente.");
        }

        string clave = orden == "partido" ? "partido" : "compra";

        return _repository.HistorialCliente(idCliente, clave);
    }
}
