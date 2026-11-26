using System.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IVentaRepository
{
    ResultadoVenta RealizarVenta(int idCliente, int idPartido, int idUsuario, string metodoPago, List<DetalleVenta> detalle);

    DataSet ConsultarPorFolio(string folioCompra);

    void Cancelar(string folioCompra, int idUsuarioAdmin, string motivo);

    DataSet ConsultarFactura(string folio);
}
