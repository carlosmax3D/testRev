using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;


namespace TemplateCSharp.Interfaces.Services;

public interface IReporteService
{
    List<ReporteVentaItem> ConsultarVentas(DateTime inicio, DateTime fin, int idEstadio, int idPartido);
    List<ReporteOcupacionItem> ConsultarOcupacion(int idPartido, string? zona, bool soloDisponibles);
    List<HistorialClienteItem> ConsultarHistorialCliente(int idCliente, int tipoOrden);
}