using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface IReporteService
    {
        List<ReporteVenta> ObtenerReporteVentas(DateTime fechaInicio, DateTime fechaFin, int? idEstadio, int? idPartido);
        List<ReporteOcupacion> ObtenerReporteOcupacion(int? idPartido, int? idZona, bool? soloDisponibles);
        List<HistorialCompra> ObtenerHistorialPorCliente(int idCliente, string orden);
        List<ReporteTopPartido> ObtenerTopPartidos(int cantidad);

        List<ReporteClienteFrecuente> ObtenerClientesFrecuentes(int cantidad);
        List<ReporteVentasPorZona> ObtenerVentasPorZona();
        ResumenPartido? ObtenerResumenPartido(int idPartido);

    }
}
