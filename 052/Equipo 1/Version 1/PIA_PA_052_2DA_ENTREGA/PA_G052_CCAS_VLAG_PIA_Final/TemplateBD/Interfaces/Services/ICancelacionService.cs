using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface ICancelacionService
    {
        Cancelacion? ObtenerCancelacionPorVenta(Guid idVenta);
        void CancelarVenta(Guid idVenta, string motivo); //solo admin

    }
}
