using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Partido_Area_Precio;

public class Partido_Area_PrecioRepository : IPartido_Area_PrecioRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public Partido_Area_PrecioRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Partido_Area_Precio> ObtenerTodos()
    {
        List<Partido_Area_Precio> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [PA_Precio_Final], [Partido_ID], [Area_ID] FROM [Partido_Area_Precio]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Partido_Area_Precio? ObtenerPorId(int partido_ID, int area_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [PA_Precio_Final], [Partido_ID], [Area_ID] FROM [Partido_Area_Precio] WHERE [Partido_ID] = @Partido_ID AND [Area_ID] = @Area_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = partido_ID;
    command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = area_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Partido_Area_Precio entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Partido_Area_Precio] ([PA_Precio_Final], [Partido_ID], [Area_ID])
                         VALUES (@PA_Precio_Final, @Partido_ID, @Area_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@PA_Precio_Final", SqlDbType.Float).Value = entity.PA_Precio_Final;
    command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = entity.Partido_ID;
    command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = entity.Area_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Partido_Area_Precio entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Partido_Area_Precio] SET
            [PA_Precio_Final] = @PA_Precio_Final
            WHERE [Partido_ID] = @Partido_ID AND [Area_ID] = @Area_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@PA_Precio_Final", SqlDbType.Float).Value = entity.PA_Precio_Final;
    command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = entity.Partido_ID;
    command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = entity.Area_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int partido_ID, int area_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Partido_Area_Precio] WHERE [Partido_ID] = @Partido_ID AND [Area_ID] = @Area_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = partido_ID;
    command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = area_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Partido_Area_Precio Mapear(SqlDataReader reader)
    {
        return new Partido_Area_Precio
        {
            PA_Precio_Final = reader.GetDouble(reader.GetOrdinal("PA_Precio_Final")),
        Partido_ID = reader.GetInt32(reader.GetOrdinal("Partido_ID")),
        Area_ID = reader.GetInt32(reader.GetOrdinal("Area_ID"))
        };
    }
}
