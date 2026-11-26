using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Emisor;

public class EmisorRepository : IEmisorRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public EmisorRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Emisor> ObtenerTodos()
    {
        List<Emisor> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Emisor_ID], [Emisor_Nombre_Legal], [Emisor_RFC], [Emisor_Direccion] FROM [Emisor]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Emisor? ObtenerPorId(int emisor_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Emisor_ID], [Emisor_Nombre_Legal], [Emisor_RFC], [Emisor_Direccion] FROM [Emisor] WHERE [Emisor_ID] = @Emisor_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Emisor_ID", SqlDbType.Int).Value = emisor_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Emisor entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Emisor] ([Emisor_ID], [Emisor_Nombre_Legal], [Emisor_RFC], [Emisor_Direccion])
                         VALUES (@Emisor_ID, @Emisor_Nombre_Legal, @Emisor_RFC, @Emisor_Direccion)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Emisor_ID", SqlDbType.Int).Value = entity.Emisor_ID;
    command.Parameters.Add("@Emisor_Nombre_Legal", SqlDbType.VarChar, 50).Value = entity.Emisor_Nombre_Legal;
    command.Parameters.Add("@Emisor_RFC", SqlDbType.VarChar, 50).Value = entity.Emisor_RFC;
    command.Parameters.Add("@Emisor_Direccion", SqlDbType.VarChar, 50).Value = entity.Emisor_Direccion;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Emisor entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Emisor] SET
            [Emisor_Nombre_Legal] = @Emisor_Nombre_Legal,
            [Emisor_RFC] = @Emisor_RFC,
            [Emisor_Direccion] = @Emisor_Direccion
            WHERE [Emisor_ID] = @Emisor_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Emisor_ID", SqlDbType.Int).Value = entity.Emisor_ID;
    command.Parameters.Add("@Emisor_Nombre_Legal", SqlDbType.VarChar, 50).Value = entity.Emisor_Nombre_Legal;
    command.Parameters.Add("@Emisor_RFC", SqlDbType.VarChar, 50).Value = entity.Emisor_RFC;
    command.Parameters.Add("@Emisor_Direccion", SqlDbType.VarChar, 50).Value = entity.Emisor_Direccion;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int emisor_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Emisor] WHERE [Emisor_ID] = @Emisor_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Emisor_ID", SqlDbType.Int).Value = emisor_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Emisor Mapear(SqlDataReader reader)
    {
        return new Emisor
        {
            Emisor_ID = reader.GetInt32(reader.GetOrdinal("Emisor_ID")),
        Emisor_Nombre_Legal = reader.GetString(reader.GetOrdinal("Emisor_Nombre_Legal")),
        Emisor_RFC = reader.GetString(reader.GetOrdinal("Emisor_RFC")),
        Emisor_Direccion = reader.GetString(reader.GetOrdinal("Emisor_Direccion"))
        };
    }
}
