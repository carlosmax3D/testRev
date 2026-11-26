using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface IEstadioService
    {
        List<Estadio> ObtenerEstadios();
        Estadio? ObtenerEstadio(int id);
        void CrearEstadio(Estadio estadio);
        void ActualizarEstadio(Estadio estadio);
    }
}
