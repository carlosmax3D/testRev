using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface IAsientoService
    {
        List<Asiento> ObtenerAsientos();
        Asiento? ObtenerAsiento(int id);
        List<Asiento> ObtenerAsientosPorZona(int idZona);
        void CrearAsiento(Asiento asiento);
        void ActualizarAsiento(Asiento asiento);
    }
}
