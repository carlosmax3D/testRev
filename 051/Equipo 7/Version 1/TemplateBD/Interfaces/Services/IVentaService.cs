using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;



namespace TemplateCSharp.Interfaces.Services;

public interface IVentaService
{
    List<Partido> ObtenerPartidosDisponibles(string? filtro = null);
    List<ZonaDisponible> ObtenerZonasDisponibles(int idPartido);
    (decimal subtotal, decimal iva, decimal total) CalcularTotales(decimal precioBase, int cantidad);
    ResultadoVenta ProcesarVenta(SolicitudVenta solicitud, decimal precioUnitario);
}