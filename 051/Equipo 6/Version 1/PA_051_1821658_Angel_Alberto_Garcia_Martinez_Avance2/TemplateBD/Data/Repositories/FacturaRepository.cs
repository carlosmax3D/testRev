using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class FacturaRepository : IFacturaRepository
{
    private readonly DatabaseConnection _database;

    public FacturaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public Factura? ObtenerPorFolioVenta(Guid folio, int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();
        connection.Open();

        Factura? factura = ObtenerEncabezado(connection, folio, idUsuario);

        if (factura != null)
        {
            factura.Detalle = ObtenerDetalle(connection, folio, idUsuario);
        }

        return factura;
    }

    private static Factura? ObtenerEncabezado(SqlConnection connection, Guid folio, int idUsuario)
    {
        using SqlCommand command = new("dbo.sp_Factura_Obtener", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Folio", SqlDbType.UniqueIdentifier).Value = folio;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new Factura
        {
            FolioFactura = reader.GetString(reader.GetOrdinal("FolioFactura")),
            FechaHoraEmision = reader.GetDateTime(reader.GetOrdinal("FechaHoraEmision")),
            FolioVenta = reader.GetGuid(reader.GetOrdinal("Folio")),
            MetodoPago = reader.GetString(reader.GetOrdinal("MetodoPago")),
            Estado = reader.GetString(reader.GetOrdinal("Estado")),
            EmisorRazonSocial = reader.GetString(reader.GetOrdinal("EmisorRazonSocial")),
            EmisorRFC = reader.GetString(reader.GetOrdinal("EmisorRFC")),
            EmisorDomicilio = reader.GetString(reader.GetOrdinal("EmisorDomicilio")),
            ReceptorNombre = reader.GetString(reader.GetOrdinal("ReceptorNombre")),
            ReceptorRFC = reader.GetString(reader.GetOrdinal("ReceptorRFC")),
            ReceptorDomicilio = reader.GetString(reader.GetOrdinal("ReceptorDomicilio")),
            TasaIva = reader.GetDecimal(reader.GetOrdinal("TasaIVA")),
            Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
            Iva = reader.GetDecimal(reader.GetOrdinal("IVA")),
            Total = reader.GetDecimal(reader.GetOrdinal("Total"))
        };
    }

    private static List<DetalleFactura> ObtenerDetalle(SqlConnection connection, Guid folio, int idUsuario)
    {
        List<DetalleFactura> detalle = [];

        using SqlCommand command = new("dbo.sp_Factura_ObtenerDetalle", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Folio", SqlDbType.UniqueIdentifier).Value = folio;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            detalle.Add(new DetalleFactura
            {
                EquipoLocal = reader.GetString(reader.GetOrdinal("EquipoLocal")),
                EquipoVisitante = reader.GetString(reader.GetOrdinal("EquipoVisitante")),
                FechaPartido = reader.GetDateTime(reader.GetOrdinal("FechaPartido")),
                Estadio = reader.GetString(reader.GetOrdinal("Estadio")),
                Zona = reader.GetString(reader.GetOrdinal("NombreZona")),
                Asientos = reader.GetString(reader.GetOrdinal("Asientos")),
                Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                Importe = reader.GetDecimal(reader.GetOrdinal("Importe"))
            });
        }

        return detalle;
    }
}
