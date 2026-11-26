using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class FacturaRepository : IFacturaRepository
{
    private readonly DatabaseConnection _database;

    public FacturaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public Factura? GetByVenta(int idVenta)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Factura_ObtenerPorVenta", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdVenta", SqlDbType.Int).Value = idVenta;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new Factura
        {
            IdFactura = reader.GetInt32(reader.GetOrdinal("IdFactura")),
            Folio = reader.GetString(reader.GetOrdinal("Folio")),
            Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
            IdEstadoFactura = reader.GetInt32(reader.GetOrdinal("IdEstadoFactura")),
            EstadoFactura = reader.GetString(reader.GetOrdinal("EstadoFactura")),
            IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
            IdVenta = reader.GetInt32(reader.GetOrdinal("IdVenta"))
        };
    }
}
