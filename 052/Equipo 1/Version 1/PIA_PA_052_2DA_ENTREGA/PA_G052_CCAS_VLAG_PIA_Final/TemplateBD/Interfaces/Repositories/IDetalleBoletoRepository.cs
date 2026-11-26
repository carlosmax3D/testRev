using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IDetalleBoletoRepository
    {
        List<DetalleBoleto> GetByVenta(Guid idVenta);
        void Add(DetalleBoleto detalle);
        void Delete(int idDetalle);
        bool IsAsientoOcupado(int idAsiento, int idPartido);

    }
}
