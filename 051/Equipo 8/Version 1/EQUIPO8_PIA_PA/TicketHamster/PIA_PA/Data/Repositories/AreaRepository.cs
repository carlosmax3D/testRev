using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Area;

public class AreaRepository : IAreaRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public AreaRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Area> ObtenerTodos()
    {
        List<Area> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Area_ID], [Area_Nombre], [Area_Capacidad], [Estadio_ID] FROM [Area]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Area? ObtenerPorId(int area_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Area_ID], [Area_Nombre], [Area_Capacidad], [Estadio_ID] FROM [Area] WHERE [Area_ID] = @Area_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = area_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Area entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Area] ([Area_ID], [Area_Nombre], [Area_Capacidad], [Estadio_ID])
                         VALUES (@Area_ID, @Area_Nombre, @Area_Capacidad, @Estadio_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = entity.Area_ID;
    command.Parameters.Add("@Area_Nombre", SqlDbType.VarChar, 50).Value = entity.Area_Nombre;
    command.Parameters.Add("@Area_Capacidad", SqlDbType.Int).Value = entity.Area_Capacidad;
    command.Parameters.Add("@Estadio_ID", SqlDbType.Int).Value = entity.Estadio_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Area entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Area] SET
            [Area_Nombre] = @Area_Nombre,
            [Area_Capacidad] = @Area_Capacidad,
            [Estadio_ID] = @Estadio_ID
            WHERE [Area_ID] = @Area_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = entity.Area_ID;
    command.Parameters.Add("@Area_Nombre", SqlDbType.VarChar, 50).Value = entity.Area_Nombre;
    command.Parameters.Add("@Area_Capacidad", SqlDbType.Int).Value = entity.Area_Capacidad;
    command.Parameters.Add("@Estadio_ID", SqlDbType.Int).Value = entity.Estadio_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int area_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Area] WHERE [Area_ID] = @Area_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Area_ID", SqlDbType.Int).Value = area_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Area Mapear(SqlDataReader reader)
    {
        return new Area
        {
            Area_ID = reader.GetInt32(reader.GetOrdinal("Area_ID")),
        Area_Nombre = reader.GetString(reader.GetOrdinal("Area_Nombre")),
        Area_Capacidad = reader.GetInt32(reader.GetOrdinal("Area_Capacidad")),
        Estadio_ID = reader.GetInt32(reader.GetOrdinal("Estadio_ID"))
        };
    }
}
