using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class VentaRepository : IVentaRepository
{
    private readonly DatabaseConnection _database;

    public VentaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public (int IdVenta, Guid Folio) Registrar(
        int idMetodoPago, int idUsuario, int idPartido, string rfc, IReadOnlyCollection<int> idsAsiento)
    {
        DataTable asientos = new();
        asientos.Columns.Add("IdAsiento", typeof(int));

        foreach (int idAsiento in idsAsiento)
        {
            asientos.Rows.Add(idAsiento);
        }

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Venta_Registrar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdMetodoPago", SqlDbType.Int).Value = idMetodoPago;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = idPartido;
        command.Parameters.Add("@RFC", SqlDbType.Char, 13).Value = rfc;

        SqlParameter parametroAsientos = command.Parameters.Add("@Asientos", SqlDbType.Structured);
        parametroAsientos.TypeName = "dbo.ListaAsientos";
        parametroAsientos.Value = asientos;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            throw new InvalidOperationException("No fue posible registrar la venta.");
        }

        int idVenta = reader.GetInt32(reader.GetOrdinal("IdVenta"));
        Guid folio = reader.GetGuid(reader.GetOrdinal("Folio"));

        return (idVenta, folio);
    }

    public Venta? GetByFolio(Guid folio)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Venta_ObtenerPorFolio", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Folio", SqlDbType.UniqueIdentifier).Value = folio;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new Venta
        {
            IdVenta = reader.GetInt32(reader.GetOrdinal("IdVenta")),
            Folio = reader.GetGuid(reader.GetOrdinal("Folio")),
            FechaVenta = reader.GetDateTime(reader.GetOrdinal("FechaVenta")),
            Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
            IVA = reader.GetDecimal(reader.GetOrdinal("IVA")),
            Total = reader.GetDecimal(reader.GetOrdinal("Total")),
            IdMetodoPago = reader.GetInt32(reader.GetOrdinal("IdMetodoPago")),
            MetodoPago = reader.GetString(reader.GetOrdinal("MetodoPago")),
            IdEstadoVenta = reader.GetInt32(reader.GetOrdinal("IdEstadoVenta")),
            EstadoVenta = reader.GetString(reader.GetOrdinal("EstadoVenta")),
            IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
            NombreUsuario = reader.GetString(reader.GetOrdinal("NombreUsuario")),
            IdPartido = reader.GetInt32(reader.GetOrdinal("IdPartido")),
            EquipoLocal = reader.GetString(reader.GetOrdinal("EquipoLocal")),
            EquipoVisitante = reader.GetString(reader.GetOrdinal("EquipoVisitante")),
            FechaPartido = reader.GetDateTime(reader.GetOrdinal("FechaPartido")),
            IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio")),
            NombreEstadio = reader.GetString(reader.GetOrdinal("NombreEstadio")),
            RFC = reader.GetString(reader.GetOrdinal("RFC")).Trim(),
            NombreCliente = reader.GetString(reader.GetOrdinal("NombreCliente"))
        };
    }

    public List<DetalleVenta> GetDetallesPorVenta(int idVenta)
    {
        List<DetalleVenta> detalles = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_DetalleVenta_ObtenerPorVenta", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdVenta", SqlDbType.Int).Value = idVenta;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            detalles.Add(new DetalleVenta
            {
                IdDetalle = reader.GetInt32(reader.GetOrdinal("IdDetalle")),
                PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                IdVenta = reader.GetInt32(reader.GetOrdinal("IdVenta")),
                IdAsiento = reader.GetInt32(reader.GetOrdinal("IdAsiento")),
                Fila = reader.GetString(reader.GetOrdinal("Fila")),
                Numero = reader.GetInt32(reader.GetOrdinal("Numero")),
                IdZona = reader.GetInt32(reader.GetOrdinal("IdZona")),
                NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
                IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio"))
            });
        }

        return detalles;
    }

    public bool PuedeCancelarse(int idVenta)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Venta_PuedeCancelarse", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdVenta", SqlDbType.Int).Value = idVenta;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado) > 0;
    }
}
