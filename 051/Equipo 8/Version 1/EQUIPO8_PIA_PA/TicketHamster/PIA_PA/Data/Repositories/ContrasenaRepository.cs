using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Contrasena;

public class ContrasenaRepository : IContrasenaRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public ContrasenaRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Contrasena> ObtenerTodos()
    {
        List<Contrasena> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Contrasena_ID], [Contrasena_Hash], [Contrasena_Fecha], [Cliente_ID], [Usuario_ID], [Contrasena_Actual] FROM [Contrasena]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Contrasena? ObtenerPorId(int contrasena_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Contrasena_ID], [Contrasena_Hash], [Contrasena_Fecha], [Cliente_ID], [Usuario_ID], [Contrasena_Actual] FROM [Contrasena] WHERE [Contrasena_ID] = @Contrasena_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Contrasena_ID", SqlDbType.Int).Value = contrasena_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Contrasena entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Contrasena] ([Contrasena_ID], [Contrasena_Hash], [Contrasena_Fecha], [Cliente_ID], [Usuario_ID], [Contrasena_Actual])
                         VALUES (@Contrasena_ID, @Contrasena_Hash, @Contrasena_Fecha, @Cliente_ID, @Usuario_ID, @Contrasena_Actual)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Contrasena_ID", SqlDbType.Int).Value = entity.Contrasena_ID;
    command.Parameters.Add("@Contrasena_Hash", SqlDbType.VarChar, 255).Value = entity.Contrasena_Hash;
    command.Parameters.Add("@Contrasena_Fecha", SqlDbType.DateTime).Value = entity.Contrasena_Fecha;
    command.Parameters.Add("@Cliente_ID", SqlDbType.Int).Value = (object?)entity.Cliente_ID ?? DBNull.Value;
    command.Parameters.Add("@Usuario_ID", SqlDbType.Int).Value = (object?)entity.Usuario_ID ?? DBNull.Value;
    command.Parameters.Add("@Contrasena_Actual", SqlDbType.Bit).Value = entity.Contrasena_Actual;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Contrasena entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Contrasena] SET
            [Contrasena_Hash] = @Contrasena_Hash,
            [Contrasena_Fecha] = @Contrasena_Fecha,
            [Cliente_ID] = @Cliente_ID,
            [Usuario_ID] = @Usuario_ID,
            [Contrasena_Actual] = @Contrasena_Actual
            WHERE [Contrasena_ID] = @Contrasena_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Contrasena_ID", SqlDbType.Int).Value = entity.Contrasena_ID;
    command.Parameters.Add("@Contrasena_Hash", SqlDbType.VarChar, 255).Value = entity.Contrasena_Hash;
    command.Parameters.Add("@Contrasena_Fecha", SqlDbType.DateTime).Value = entity.Contrasena_Fecha;
    command.Parameters.Add("@Cliente_ID", SqlDbType.Int).Value = (object?)entity.Cliente_ID ?? DBNull.Value;
    command.Parameters.Add("@Usuario_ID", SqlDbType.Int).Value = (object?)entity.Usuario_ID ?? DBNull.Value;
    command.Parameters.Add("@Contrasena_Actual", SqlDbType.Bit).Value = entity.Contrasena_Actual;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int contrasena_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Contrasena] WHERE [Contrasena_ID] = @Contrasena_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Contrasena_ID", SqlDbType.Int).Value = contrasena_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Contrasena Mapear(SqlDataReader reader)
    {
        return new Contrasena
        {
            Contrasena_ID = reader.GetInt32(reader.GetOrdinal("Contrasena_ID")),
        Contrasena_Hash = reader.GetString(reader.GetOrdinal("Contrasena_Hash")),
        Contrasena_Fecha = reader.GetDateTime(reader.GetOrdinal("Contrasena_Fecha")),
        Cliente_ID = reader.IsDBNull(reader.GetOrdinal("Cliente_ID")) ? null : reader.GetInt32(reader.GetOrdinal("Cliente_ID")),
        Usuario_ID = reader.IsDBNull(reader.GetOrdinal("Usuario_ID")) ? null : reader.GetInt32(reader.GetOrdinal("Usuario_ID")),
        Contrasena_Actual = reader.GetBoolean(reader.GetOrdinal("Contrasena_Actual"))
        };
    }
}
