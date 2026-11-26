using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Asiento;

public class AsientoRepository : IAsientoRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public AsientoRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Asiento> ObtenerTodos()
    {
        List<Asiento> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Asiento_ID], [Asiento_Numero], [Asiento_Fila], [Area_ID] FROM [Asiento]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Asiento? ObtenerPorId(int asiento_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Asiento_ID], [Asiento_Numero], [Asiento_Fila], [Area_ID] FROM [Asiento] WHERE [Asiento_ID] = @Asiento_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Asiento_ID", SqlDbType.Int).Value = asiento_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Asiento entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Asiento] ([Asiento_ID], [Asiento_Numero], [Asiento_Fila], [Area_ID])
                         VALUES (@Asiento_ID, @Asiento_Numero, @Asiento_Fila, @Area_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Asiento_ID", SqlDbType.Int).Value = entity.Asiento_ID;
    command.Parameters.Add("@Asiento_Numero", SqlDbType.Int).Value = entity.Asiento_Numero;
    command.Parameters.Add("@Asiento_Fila", SqlDbType.Int).Value = entity.Asiento_Fila;
    command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = entity.Area_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Asiento entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Asiento] SET
            [Asiento_Numero] = @Asiento_Numero,
            [Asiento_Fila] = @Asiento_Fila,
            [Area_ID] = @Area_ID
            WHERE [Asiento_ID] = @Asiento_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Asiento_ID", SqlDbType.Int).Value = entity.Asiento_ID;
    command.Parameters.Add("@Asiento_Numero", SqlDbType.Int).Value = entity.Asiento_Numero;
    command.Parameters.Add("@Asiento_Fila", SqlDbType.Int).Value = entity.Asiento_Fila;
    command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = entity.Area_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int asiento_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Asiento] WHERE [Asiento_ID] = @Asiento_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Asiento_ID", SqlDbType.Int).Value = asiento_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Asiento Mapear(SqlDataReader reader)
    {
        return new Asiento
        {
            Asiento_ID = reader.GetInt32(reader.GetOrdinal("Asiento_ID")),
        Asiento_Numero = reader.GetInt32(reader.GetOrdinal("Asiento_Numero")),
        Asiento_Fila = reader.GetInt32(reader.GetOrdinal("Asiento_Fila")),
        Area_ID = reader.GetInt32(reader.GetOrdinal("Area_ID"))
        };
    }
}
