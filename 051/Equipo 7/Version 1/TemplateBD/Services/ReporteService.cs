using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;


namespace TemplateCSharp.Services;

public class ReporteService : IReporteService
{
    private readonly IReporteRepository _repository;

    public ReporteService(IReporteRepository repository)
    {
        _repository = repository;
    }

    public List<ReporteVentaItem> ConsultarVentas(DateTime inicio, DateTime fin, int idEstadio, int idPartido)
    {
        if (inicio.Date > fin.Date)
            throw new ArgumentException("La fecha inicial no puede ser mayor a la fecha final.");

        return _repository.ObtenerReporteVentas(inicio, fin, idEstadio, idPartido);
    }

    public List<ReporteOcupacionItem> ConsultarOcupacion(int idPartido, string? zona, bool soloDisponibles)
    {
        return _repository.ObtenerReporteOcupacion(idPartido, zona, soloDisponibles);
    }

    public List<HistorialClienteItem> ConsultarHistorialCliente(int idCliente, int tipoOrden)
    {
        if (idCliente <= 0)
            throw new ArgumentException("Seleccione un cliente de la lista.");

        return _repository.ObtenerHistorialCliente(idCliente, tipoOrden);
    }
}