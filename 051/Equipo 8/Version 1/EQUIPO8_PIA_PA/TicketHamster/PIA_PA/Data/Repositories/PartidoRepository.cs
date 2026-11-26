using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Partido;

public class PartidoRepository : IPartidoRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public PartidoRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public DataTable ObtenerTablaConsulta()
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        using SqlDataAdapter adapter = new(@"SELECT p.Partido_ID AS ID, p.Partido_Fecha AS Fecha, p.Partido_Hora AS Hora, el.Equipo_Nombre AS Local, ev.Equipo_Nombre AS Visitante, e.Estadio_Nombre AS Estadio, p.Partido_Estado AS Estado FROM Partido p INNER JOIN Equipos el ON p.Equipo_ID_Local=el.Equipo_ID INNER JOIN Equipos ev ON p.Equipo_ID_Visitante=ev.Equipo_ID INNER JOIN Estadio e ON p.Estadio_ID=e.Estadio_ID ORDER BY p.Partido_ID", connection);
        DataTable tabla = new();
        adapter.Fill(tabla);
        return tabla;
    }

    public List<Partido> ObtenerTodos()
    {
        List<Partido> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Partido_ID], [Partido_Estado], [Partido_Fecha], [Partido_Hora], [Equipo_ID_Local], [Equipo_ID_Visitante], [Estadio_ID] FROM [Partido]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Partido? ObtenerPorId(int partido_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Partido_ID], [Partido_Estado], [Partido_Fecha], [Partido_Hora], [Equipo_ID_Local], [Equipo_ID_Visitante], [Estadio_ID] FROM [Partido] WHERE [Partido_ID] = @Partido_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = partido_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Partido entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Partido] ([Partido_ID], [Partido_Estado], [Partido_Fecha], [Partido_Hora], [Equipo_ID_Local], [Equipo_ID_Visitante], [Estadio_ID])
                         VALUES (@Partido_ID, @Partido_Estado, @Partido_Fecha, @Partido_Hora, @Equipo_ID_Local, @Equipo_ID_Visitante, @Estadio_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = entity.Partido_ID;
    command.Parameters.Add("@Partido_Estado", SqlDbType.VarChar, 50).Value = entity.Partido_Estado;
    command.Parameters.Add("@Partido_Fecha", SqlDbType.Date).Value = entity.Partido_Fecha.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Partido_Hora", SqlDbType.Time).Value = entity.Partido_Hora.ToTimeSpan();
    command.Parameters.Add("@Equipo_ID_Local", SqlDbType.Int).Value = entity.Equipo_ID_Local;
    command.Parameters.Add("@Equipo_ID_Visitante", SqlDbType.Int).Value = entity.Equipo_ID_Visitante;
    command.Parameters.Add("@Estadio_ID", SqlDbType.Int).Value = entity.Estadio_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Partido entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Partido] SET
            [Partido_Estado] = @Partido_Estado,
            [Partido_Fecha] = @Partido_Fecha,
            [Partido_Hora] = @Partido_Hora,
            [Equipo_ID_Local] = @Equipo_ID_Local,
            [Equipo_ID_Visitante] = @Equipo_ID_Visitante,
            [Estadio_ID] = @Estadio_ID
            WHERE [Partido_ID] = @Partido_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = entity.Partido_ID;
    command.Parameters.Add("@Partido_Estado", SqlDbType.VarChar, 50).Value = entity.Partido_Estado;
    command.Parameters.Add("@Partido_Fecha", SqlDbType.Date).Value = entity.Partido_Fecha.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Partido_Hora", SqlDbType.Time).Value = entity.Partido_Hora.ToTimeSpan();
    command.Parameters.Add("@Equipo_ID_Local", SqlDbType.Int).Value = entity.Equipo_ID_Local;
    command.Parameters.Add("@Equipo_ID_Visitante", SqlDbType.Int).Value = entity.Equipo_ID_Visitante;
    command.Parameters.Add("@Estadio_ID", SqlDbType.Int).Value = entity.Estadio_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int partido_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Partido] WHERE [Partido_ID] = @Partido_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Partido_ID", SqlDbType.Int).Value = partido_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Partido Mapear(SqlDataReader reader)
    {
        return new Partido
        {
            Partido_ID = reader.GetInt32(reader.GetOrdinal("Partido_ID")),
        Partido_Estado = reader.GetString(reader.GetOrdinal("Partido_Estado")),
        Partido_Fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Partido_Fecha"))),
        Partido_Hora = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("Partido_Hora"))),
        Equipo_ID_Local = reader.GetInt32(reader.GetOrdinal("Equipo_ID_Local")),
        Equipo_ID_Visitante = reader.GetInt32(reader.GetOrdinal("Equipo_ID_Visitante")),
        Estadio_ID = reader.GetInt32(reader.GetOrdinal("Estadio_ID"))
        };
    }
}
