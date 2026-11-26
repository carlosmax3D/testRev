using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Ticket;

public class TicketRepository : ITicketRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public TicketRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Ticket> ObtenerTodos()
    {
        List<Ticket> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Ticket_ID], [Ticket_Estado], [Cliente_ID], [Venta_Info_ID], [Partido_ID], [Asiento_ID] FROM [Ticket]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Ticket? ObtenerPorId(int ticket_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Ticket_ID], [Ticket_Estado], [Cliente_ID], [Venta_Info_ID], [Partido_ID], [Asiento_ID] FROM [Ticket] WHERE [Ticket_ID] = @Ticket_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Ticket_ID", SqlDbType.Int).Value = ticket_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Ticket entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Ticket] ([Ticket_ID], [Ticket_Estado], [Cliente_ID], [Venta_Info_ID], [Partido_ID], [Asiento_ID])
                         VALUES (@Ticket_ID, @Ticket_Estado, @Cliente_ID, @Venta_Info_ID, @Partido_ID, @Asiento_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Ticket_ID", SqlDbType.Int).Value = entity.Ticket_ID;
    command.Parameters.Add("@Ticket_Estado", SqlDbType.VarChar, 50).Value = entity.Ticket_Estado;
    command.Parameters.Add("@Cliente_ID", SqlDbType.Int).Value = entity.Cliente_ID;
    command.Parameters.Add("@Venta_Info_ID", SqlDbType.Int).Value = entity.Venta_Info_ID;
    command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = entity.Partido_ID;
    command.Parameters.Add("@Asiento_ID", SqlDbType.Int).Value = entity.Asiento_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Ticket entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Ticket] SET
            [Ticket_Estado] = @Ticket_Estado,
            [Cliente_ID] = @Cliente_ID,
            [Venta_Info_ID] = @Venta_Info_ID,
            [Partido_ID] = @Partido_ID,
            [Asiento_ID] = @Asiento_ID
            WHERE [Ticket_ID] = @Ticket_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Ticket_ID", SqlDbType.Int).Value = entity.Ticket_ID;
    command.Parameters.Add("@Ticket_Estado", SqlDbType.VarChar, 50).Value = entity.Ticket_Estado;
    command.Parameters.Add("@Cliente_ID", SqlDbType.Int).Value = entity.Cliente_ID;
    command.Parameters.Add("@Venta_Info_ID", SqlDbType.Int).Value = entity.Venta_Info_ID;
    command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = entity.Partido_ID;
    command.Parameters.Add("@Asiento_ID", SqlDbType.Int).Value = entity.Asiento_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int ticket_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Ticket] WHERE [Ticket_ID] = @Ticket_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Ticket_ID", SqlDbType.Int).Value = ticket_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Ticket Mapear(SqlDataReader reader)
    {
        return new Ticket
        {
            Ticket_ID = reader.GetInt32(reader.GetOrdinal("Ticket_ID")),
        Ticket_Estado = reader.GetString(reader.GetOrdinal("Ticket_Estado")),
        Cliente_ID = reader.GetInt32(reader.GetOrdinal("Cliente_ID")),
        Venta_Info_ID = reader.GetInt32(reader.GetOrdinal("Venta_Info_ID")),
        Partido_ID = reader.GetInt32(reader.GetOrdinal("Partido_ID")),
        Asiento_ID = reader.GetInt32(reader.GetOrdinal("Asiento_ID"))
        };
    }
}
