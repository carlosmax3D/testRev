using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IVentaBoletoRepository
    {
        List<VentaBoleto> GetAll();
        VentaBoleto? GetById(Guid id);
        Guid Add(VentaBoleto venta); //Retorna el GUID generado
    }
}
