using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface IVentaBoletoService
    {
        List<VentaBoleto> ObtenerVentas();
        VentaBoleto? ObtenerVenta(Guid id);
        Guid RegistrarVenta(VentaBoleto venta, List<DetalleBoleto> detalles);
        List<DetalleBoleto> ObtenerDetalles(Guid idVenta);
    }
}
