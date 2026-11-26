using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface IEquipoService
    {
        List<Equipo> ObtenerEquipos();
        Equipo? ObtenerEquipo(int id);
        void CrearEquipo(Equipo equipo);
        void ActualizarEquipo(Equipo equipo);
    }
}
