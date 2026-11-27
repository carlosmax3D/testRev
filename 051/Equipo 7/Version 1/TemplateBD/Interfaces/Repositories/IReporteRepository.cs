using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;


namespace TemplateCSharp.Interfaces.Repositories;

public interface IReporteRepository
{
    List<ReporteVentaItem> ObtenerReporteVentas(DateTime inicio, DateTime fin, int idEstadio, int idPartido);
    List<ReporteOcupacionItem> ObtenerReporteOcupacion(int idPartido, string? zona, bool soloDisponibles);
    List<HistorialClienteItem> ObtenerHistorialCliente(int idCliente, int tipoOrden);
}