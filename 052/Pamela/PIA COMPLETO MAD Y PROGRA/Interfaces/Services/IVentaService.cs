using System.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IVentaService
{
    decimal CalcularSubtotal(List<DetalleVenta> detalle);

    decimal CalcularIva(decimal subtotal);

    void ValidarTarjeta(DatosTarjeta tarjeta);

    ResultadoVenta RealizarVenta(int idCliente, int idPartido, int idUsuario, string metodoPago, List<DetalleVenta> detalle);

    DataSet ConsultarCompra(string folioCompra);

    void CancelarCompra(string folioCompra, Usuario usuario, string motivo);

    DataSet ConsultarFactura(string folio);
}
