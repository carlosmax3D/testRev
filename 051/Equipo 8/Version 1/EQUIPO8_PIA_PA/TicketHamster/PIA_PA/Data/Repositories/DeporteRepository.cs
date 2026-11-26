using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Deporte;

public class DeporteRepository : IDeporteRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public DeporteRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Deporte> ObtenerTodos()
    {
        List<Deporte> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Deporte_ID], [Deporte_Nombre] FROM [Deporte]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Deporte? ObtenerPorId(int deporte_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Deporte_ID], [Deporte_Nombre] FROM [Deporte] WHERE [Deporte_ID] = @Deporte_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Deporte_ID", SqlDbType.Int).Value = deporte_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Deporte entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Deporte] ([Deporte_ID], [Deporte_Nombre])
                         VALUES (@Deporte_ID, @Deporte_Nombre)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Deporte_ID", SqlDbType.Int).Value = entity.Deporte_ID;
    command.Parameters.Add("@Deporte_Nombre", SqlDbType.VarChar, 50).Value = entity.Deporte_Nombre;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Deporte entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Deporte] SET
            [Deporte_Nombre] = @Deporte_Nombre
            WHERE [Deporte_ID] = @Deporte_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Deporte_ID", SqlDbType.Int).Value = entity.Deporte_ID;
    command.Parameters.Add("@Deporte_Nombre", SqlDbType.VarChar, 50).Value = entity.Deporte_Nombre;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int deporte_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Deporte] WHERE [Deporte_ID] = @Deporte_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Deporte_ID", SqlDbType.Int).Value = deporte_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Deporte Mapear(SqlDataReader reader)
    {
        return new Deporte
        {
            Deporte_ID = reader.GetInt32(reader.GetOrdinal("Deporte_ID")),
        Deporte_Nombre = reader.GetString(reader.GetOrdinal("Deporte_Nombre"))
        };
    }
}
