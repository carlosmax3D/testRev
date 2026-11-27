using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;


namespace TemplateCSharp.Interfaces.Repositories;

public interface IFacturaRepository
{
    List<CompraResumen> ListarCompras();
    FacturaCompleta ObtenerFacturaPorFolio(string folioCompra);
    void CancelarCompra(string folioCompra, string motivo, string usuarioAuditoria);
}