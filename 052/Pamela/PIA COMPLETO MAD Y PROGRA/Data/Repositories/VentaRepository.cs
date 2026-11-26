using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

/// <summary>Acceso a datos de ventas, cancelaciones y facturas.</summary>
public class VentaRepository : RepositorioBase, IVentaRepository
{
    public VentaRepository(DatabaseConnection database) : base(database)
    {
    }

    public ResultadoVenta RealizarVenta(int idCliente, int idPartido, int idUsuario, string metodoPago, List<DetalleVenta> detalle)
    {
        // El detalle (zona y cantidad) viaja como parámetro de tipo tabla (dbo.TipoDetalleVenta)
        DataTable tablaDetalle = new DataTable();
        tablaDetalle.Columns.Add("IdZona", typeof(int));
        tablaDetalle.Columns.Add("Cantidad", typeof(int));

        foreach (DetalleVenta renglon in detalle)
        {
            tablaDetalle.Rows.Add(renglon.IdZona, renglon.Cantidad);
        }

        SqlParameter parametroDetalle = new SqlParameter("@p_detalle", SqlDbType.Structured);
        parametroDetalle.TypeName = "dbo.TipoDetalleVenta";
        parametroDetalle.Value = tablaDetalle;

        DataTable resultado = EjecutarTabla(
            "SP_RealizarVenta",
            Parametro("@p_idCliente", SqlDbType.Int, idCliente),
            Parametro("@p_idPartido", SqlDbType.Int, idPartido),
            Parametro("@p_idUsuarioVenta", SqlDbType.Int, idUsuario),
            ParametroTexto("@p_metodoPago", metodoPago, 10),
            parametroDetalle);

        if (resultado.Rows.Count == 0)
        {
            throw new InvalidOperationException("La base de datos no confirmó la venta.");
        }

        DataRow fila = resultado.Rows[0];

        return new ResultadoVenta
        {
            IdVenta = DatosHelper.Entero(fila, "IdVenta"),
            FolioCompra = DatosHelper.Texto(fila, "FolioCompra").ToUpperInvariant(),
            FolioFactura = DatosHelper.Texto(fila, "FolioFactura"),
            Subtotal = DatosHelper.Numero(fila, "Subtotal"),
            IVA = DatosHelper.Numero(fila, "IVA"),
            Total = DatosHelper.Numero(fila, "Total")
        };
    }

    public DataSet ConsultarPorFolio(string folioCompra)
    {
        return EjecutarConjunto(
            "SP_ConsultarVentaPorFolio",
            ParametroTexto("@p_folioCompra", folioCompra, 50));
    }

    public void Cancelar(string folioCompra, int idUsuarioAdmin, string motivo)
    {
        EjecutarAccion(
            "SP_CancelarVenta",
            ParametroTexto("@p_folioCompra", folioCompra, 50),
            Parametro("@p_idUsuarioAdmin", SqlDbType.Int, idUsuarioAdmin),
            ParametroTexto("@p_motivo", motivo, 250));
    }

    public DataSet ConsultarFactura(string folio)
    {
        return EjecutarConjunto(
            "SP_ConsultarFactura",
            ParametroTexto("@p_folio", folio, 50));
    }
}
