using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IVentaRepository
{
    List<Partido> ObtenerPartidosDisponibles(string? filtro = null);
    List<ZonaDisponible> ObtenerZonasDisponibles(int idPartido);
    (string folio, string asientos) RegistrarVenta(SolicitudVenta solicitud);
}
