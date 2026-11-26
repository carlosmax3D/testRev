using Microsoft.Data.SqlClient;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

// Convierte una fila de dbo.vw_VentaResumen en ResumenVenta
internal static class ResumenVentaMapper
{
    public static ResumenVenta Map(SqlDataReader reader)
    {
        return new ResumenVenta
        {
            Folio = reader.GetGuid(reader.GetOrdinal("Folio")),
            FechaVenta = reader.GetDateTime(reader.GetOrdinal("FechaVenta")),
            Cliente = reader.GetString(reader.GetOrdinal("Cliente")),
            FechaPartido = reader.GetDateTime(reader.GetOrdinal("FechaPartido")),
            Estadio = reader.GetString(reader.GetOrdinal("Estadio")),
            EquipoLocal = reader.GetString(reader.GetOrdinal("EquipoLocal")),
            EquipoVisitante = reader.GetString(reader.GetOrdinal("EquipoVisitante")),
            Zonas = reader.GetString(reader.GetOrdinal("Zonas")),
            CantidadBoletos = reader.GetInt32(reader.GetOrdinal("CantidadBoletos")),
            Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
            Total = reader.GetDecimal(reader.GetOrdinal("Total")),
            MetodoPago = reader.GetString(reader.GetOrdinal("MetodoPago")),
            FolioFactura = reader.GetString(reader.GetOrdinal("FolioFactura")),
            Cancelada = reader.GetBoolean(reader.GetOrdinal("Cancelada"))
        };
    }
}
