using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Telefono;

public class TelefonoRepository : ITelefonoRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public TelefonoRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Telefono> ObtenerTodos()
    {
        List<Telefono> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Telefono_ID], [Telefono_Fijo], [Telefono_Celular] FROM [Telefono]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Telefono? ObtenerPorId(int telefono_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Telefono_ID], [Telefono_Fijo], [Telefono_Celular] FROM [Telefono] WHERE [Telefono_ID] = @Telefono_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Telefono_ID", SqlDbType.Int).Value = telefono_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Telefono entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Telefono] ([Telefono_ID], [Telefono_Fijo], [Telefono_Celular])
                         VALUES (@Telefono_ID, @Telefono_Fijo, @Telefono_Celular)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Telefono_ID", SqlDbType.Int).Value = entity.Telefono_ID;
    command.Parameters.Add("@Telefono_Fijo", SqlDbType.Int).Value = (object?)entity.Telefono_Fijo ?? DBNull.Value;
    command.Parameters.Add("@Telefono_Celular", SqlDbType.Int).Value = (object?)entity.Telefono_Celular ?? DBNull.Value;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Telefono entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Telefono] SET
            [Telefono_Fijo] = @Telefono_Fijo,
            [Telefono_Celular] = @Telefono_Celular
            WHERE [Telefono_ID] = @Telefono_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Telefono_ID", SqlDbType.Int).Value = entity.Telefono_ID;
    command.Parameters.Add("@Telefono_Fijo", SqlDbType.Int).Value = (object?)entity.Telefono_Fijo ?? DBNull.Value;
    command.Parameters.Add("@Telefono_Celular", SqlDbType.Int).Value = (object?)entity.Telefono_Celular ?? DBNull.Value;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int telefono_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Telefono] WHERE [Telefono_ID] = @Telefono_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Telefono_ID", SqlDbType.Int).Value = telefono_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Telefono Mapear(SqlDataReader reader)
    {
        return new Telefono
        {
            Telefono_ID = reader.GetInt32(reader.GetOrdinal("Telefono_ID")),
        Telefono_Fijo = reader.IsDBNull(reader.GetOrdinal("Telefono_Fijo")) ? null : reader.GetInt32(reader.GetOrdinal("Telefono_Fijo")),
        Telefono_Celular = reader.IsDBNull(reader.GetOrdinal("Telefono_Celular")) ? null : reader.GetInt32(reader.GetOrdinal("Telefono_Celular"))
        };
    }
}
