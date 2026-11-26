using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IFacturaRepository
    {
        Factura? GetByVenta(Guid idVenta);
        void Add(Factura factura);
        void UpdateEstado(int idFactura, string nuevoEstado);
    }
}
