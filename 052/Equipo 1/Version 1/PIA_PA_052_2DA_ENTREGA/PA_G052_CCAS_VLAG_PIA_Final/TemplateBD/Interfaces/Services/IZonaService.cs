using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface IZonaService
    {
        List<Zona> ObtenerZonas();
        Zona? ObtenerZona(int id);
        List<Zona> ObtenerZonasPorEstadio(int idEstadio);
        void CrearZona(Zona zona);
        void ActualizarZona(Zona zona);
    }
}
