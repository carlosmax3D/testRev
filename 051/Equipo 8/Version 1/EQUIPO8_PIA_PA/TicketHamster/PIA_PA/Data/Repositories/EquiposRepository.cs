using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Equipos;

public class EquiposRepository : IEquiposRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public EquiposRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public DataTable ObtenerTablaConsulta()
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        using SqlDataAdapter adapter = new(@"SELECT e.Equipo_ID AS ID, e.Equipo_Nombre AS Nombre, e.Equipo_Estatus AS Estatus, d.Deporte_Nombre AS Deporte FROM Equipos e INNER JOIN Deporte d ON e.Deporte_ID=d.Deporte_ID ORDER BY e.Equipo_ID", connection);
        DataTable tabla = new();
        adapter.Fill(tabla);
        return tabla;
    }

    public List<Equipos> ObtenerTodos()
    {
        List<Equipos> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Equipo_ID], [Equipo_Nombre], [Equipo_Estatus], [Deporte_ID] FROM [Equipos]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Equipos? ObtenerPorId(int equipo_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Equipo_ID], [Equipo_Nombre], [Equipo_Estatus], [Deporte_ID] FROM [Equipos] WHERE [Equipo_ID] = @Equipo_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Equipo_ID", SqlDbType.Int).Value = equipo_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Equipos entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Equipos] ([Equipo_ID], [Equipo_Nombre], [Equipo_Estatus], [Deporte_ID])
                         VALUES (@Equipo_ID, @Equipo_Nombre, @Equipo_Estatus, @Deporte_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Equipo_ID", SqlDbType.Int).Value = entity.Equipo_ID;
    command.Parameters.Add("@Equipo_Nombre", SqlDbType.VarChar, 50).Value = entity.Equipo_Nombre;
    command.Parameters.Add("@Equipo_Estatus", SqlDbType.VarChar, 50).Value = entity.Equipo_Estatus;
    command.Parameters.Add("@Deporte_ID", SqlDbType.Int).Value = entity.Deporte_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Equipos entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Equipos] SET
            [Equipo_Nombre] = @Equipo_Nombre,
            [Equipo_Estatus] = @Equipo_Estatus,
            [Deporte_ID] = @Deporte_ID
            WHERE [Equipo_ID] = @Equipo_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Equipo_ID", SqlDbType.Int).Value = entity.Equipo_ID;
    command.Parameters.Add("@Equipo_Nombre", SqlDbType.VarChar, 50).Value = entity.Equipo_Nombre;
    command.Parameters.Add("@Equipo_Estatus", SqlDbType.VarChar, 50).Value = entity.Equipo_Estatus;
    command.Parameters.Add("@Deporte_ID", SqlDbType.Int).Value = entity.Deporte_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int equipo_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Equipos] WHERE [Equipo_ID] = @Equipo_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Equipo_ID", SqlDbType.Int).Value = equipo_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Equipos Mapear(SqlDataReader reader)
    {
        return new Equipos
        {
            Equipo_ID = reader.GetInt32(reader.GetOrdinal("Equipo_ID")),
        Equipo_Nombre = reader.GetString(reader.GetOrdinal("Equipo_Nombre")),
        Equipo_Estatus = reader.GetString(reader.GetOrdinal("Equipo_Estatus")),
        Deporte_ID = reader.GetInt32(reader.GetOrdinal("Deporte_ID"))
        };
    }
}
