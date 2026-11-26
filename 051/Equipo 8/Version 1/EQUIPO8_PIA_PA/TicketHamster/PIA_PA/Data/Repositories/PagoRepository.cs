using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Pago;

public class PagoRepository : IPagoRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public PagoRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Pago> ObtenerTodos()
    {
        List<Pago> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Pago_ID], [Pago_Metodo], [Pago_Fecha], [Pago_Cantidad] FROM [Pago]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Pago? ObtenerPorId(int pago_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Pago_ID], [Pago_Metodo], [Pago_Fecha], [Pago_Cantidad] FROM [Pago] WHERE [Pago_ID] = @Pago_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Pago_ID", SqlDbType.Int).Value = pago_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Pago entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Pago] ([Pago_ID], [Pago_Metodo], [Pago_Fecha], [Pago_Cantidad])
                         VALUES (@Pago_ID, @Pago_Metodo, @Pago_Fecha, @Pago_Cantidad)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Pago_ID", SqlDbType.Int).Value = entity.Pago_ID;
    command.Parameters.Add("@Pago_Metodo", SqlDbType.VarChar, 50).Value = entity.Pago_Metodo;
    command.Parameters.Add("@Pago_Fecha", SqlDbType.Date).Value = entity.Pago_Fecha.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Pago_Cantidad", SqlDbType.Float).Value = entity.Pago_Cantidad;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Pago entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Pago] SET
            [Pago_Metodo] = @Pago_Metodo,
            [Pago_Fecha] = @Pago_Fecha,
            [Pago_Cantidad] = @Pago_Cantidad
            WHERE [Pago_ID] = @Pago_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Pago_ID", SqlDbType.Int).Value = entity.Pago_ID;
    command.Parameters.Add("@Pago_Metodo", SqlDbType.VarChar, 50).Value = entity.Pago_Metodo;
    command.Parameters.Add("@Pago_Fecha", SqlDbType.Date).Value = entity.Pago_Fecha.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Pago_Cantidad", SqlDbType.Float).Value = entity.Pago_Cantidad;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int pago_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Pago] WHERE [Pago_ID] = @Pago_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Pago_ID", SqlDbType.Int).Value = pago_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Pago Mapear(SqlDataReader reader)
    {
        return new Pago
        {
            Pago_ID = reader.GetInt32(reader.GetOrdinal("Pago_ID")),
        Pago_Metodo = reader.GetString(reader.GetOrdinal("Pago_Metodo")),
        Pago_Fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Pago_Fecha"))),
        Pago_Cantidad = reader.GetDouble(reader.GetOrdinal("Pago_Cantidad"))
        };
    }
}
