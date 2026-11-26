using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Direccion;

public class DireccionRepository : IDireccionRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public DireccionRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Direccion> ObtenerTodos()
    {
        List<Direccion> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Direccion_ID], [Direccion_Numero], [Direccion_Calle], [Direccion_CP], [Direccion_Colonia], [Direccion_Ciudad], [Direccion_Estado], [Direccion_Pais] FROM [Direccion]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Direccion? ObtenerPorId(int direccion_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Direccion_ID], [Direccion_Numero], [Direccion_Calle], [Direccion_CP], [Direccion_Colonia], [Direccion_Ciudad], [Direccion_Estado], [Direccion_Pais] FROM [Direccion] WHERE [Direccion_ID] = @Direccion_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Direccion_ID", SqlDbType.Int).Value = direccion_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Direccion entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Direccion] ([Direccion_ID], [Direccion_Numero], [Direccion_Calle], [Direccion_CP], [Direccion_Colonia], [Direccion_Ciudad], [Direccion_Estado], [Direccion_Pais])
                         VALUES (@Direccion_ID, @Direccion_Numero, @Direccion_Calle, @Direccion_CP, @Direccion_Colonia, @Direccion_Ciudad, @Direccion_Estado, @Direccion_Pais)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Direccion_ID", SqlDbType.Int).Value = entity.Direccion_ID;
    command.Parameters.Add("@Direccion_Numero", SqlDbType.Int).Value = entity.Direccion_Numero;
    command.Parameters.Add("@Direccion_Calle", SqlDbType.VarChar, 200).Value = entity.Direccion_Calle;
    command.Parameters.Add("@Direccion_CP", SqlDbType.Int).Value = entity.Direccion_CP;
    command.Parameters.Add("@Direccion_Colonia", SqlDbType.VarChar, 200).Value = entity.Direccion_Colonia;
    command.Parameters.Add("@Direccion_Ciudad", SqlDbType.VarChar, 200).Value = entity.Direccion_Ciudad;
    command.Parameters.Add("@Direccion_Estado", SqlDbType.VarChar, 50).Value = entity.Direccion_Estado;
    command.Parameters.Add("@Direccion_Pais", SqlDbType.VarChar, 150).Value = entity.Direccion_Pais;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Direccion entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Direccion] SET
            [Direccion_Numero] = @Direccion_Numero,
            [Direccion_Calle] = @Direccion_Calle,
            [Direccion_CP] = @Direccion_CP,
            [Direccion_Colonia] = @Direccion_Colonia,
            [Direccion_Ciudad] = @Direccion_Ciudad,
            [Direccion_Estado] = @Direccion_Estado,
            [Direccion_Pais] = @Direccion_Pais
            WHERE [Direccion_ID] = @Direccion_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Direccion_ID", SqlDbType.Int).Value = entity.Direccion_ID;
    command.Parameters.Add("@Direccion_Numero", SqlDbType.Int).Value = entity.Direccion_Numero;
    command.Parameters.Add("@Direccion_Calle", SqlDbType.VarChar, 200).Value = entity.Direccion_Calle;
    command.Parameters.Add("@Direccion_CP", SqlDbType.Int).Value = entity.Direccion_CP;
    command.Parameters.Add("@Direccion_Colonia", SqlDbType.VarChar, 200).Value = entity.Direccion_Colonia;
    command.Parameters.Add("@Direccion_Ciudad", SqlDbType.VarChar, 200).Value = entity.Direccion_Ciudad;
    command.Parameters.Add("@Direccion_Estado", SqlDbType.VarChar, 50).Value = entity.Direccion_Estado;
    command.Parameters.Add("@Direccion_Pais", SqlDbType.VarChar, 150).Value = entity.Direccion_Pais;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int direccion_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Direccion] WHERE [Direccion_ID] = @Direccion_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Direccion_ID", SqlDbType.Int).Value = direccion_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Direccion Mapear(SqlDataReader reader)
    {
        return new Direccion
        {
            Direccion_ID = reader.GetInt32(reader.GetOrdinal("Direccion_ID")),
        Direccion_Numero = reader.GetInt32(reader.GetOrdinal("Direccion_Numero")),
        Direccion_Calle = reader.GetString(reader.GetOrdinal("Direccion_Calle")),
        Direccion_CP = reader.GetInt32(reader.GetOrdinal("Direccion_CP")),
        Direccion_Colonia = reader.GetString(reader.GetOrdinal("Direccion_Colonia")),
        Direccion_Ciudad = reader.GetString(reader.GetOrdinal("Direccion_Ciudad")),
        Direccion_Estado = reader.GetString(reader.GetOrdinal("Direccion_Estado")),
        Direccion_Pais = reader.GetString(reader.GetOrdinal("Direccion_Pais"))
        };
    }
}
