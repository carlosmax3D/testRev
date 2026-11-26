using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Pagador;

public class PagadorRepository : IPagadorRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public PagadorRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Pagador> ObtenerTodos()
    {
        List<Pagador> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Pagador_Tipo], [Pagador_RFC], [Pagador_Nombre_Legal], [Pagador_CURP], [Pagador_Regimen_Fiscal], [Pagador_Direccion] FROM [Pagador]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Pagador? ObtenerPorId(string pagador_RFC)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Pagador_Tipo], [Pagador_RFC], [Pagador_Nombre_Legal], [Pagador_CURP], [Pagador_Regimen_Fiscal], [Pagador_Direccion] FROM [Pagador] WHERE [Pagador_RFC] = @Pagador_RFC";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Pagador_RFC", SqlDbType.VarChar).Value = pagador_RFC;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Pagador entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Pagador] ([Pagador_Tipo], [Pagador_RFC], [Pagador_Nombre_Legal], [Pagador_CURP], [Pagador_Regimen_Fiscal], [Pagador_Direccion])
                         VALUES (@Pagador_Tipo, @Pagador_RFC, @Pagador_Nombre_Legal, @Pagador_CURP, @Pagador_Regimen_Fiscal, @Pagador_Direccion)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Pagador_Tipo", SqlDbType.VarChar, 50).Value = entity.Pagador_Tipo;
    command.Parameters.Add("@Pagador_RFC", SqlDbType.VarChar, 50).Value = entity.Pagador_RFC;
    command.Parameters.Add("@Pagador_Nombre_Legal", SqlDbType.VarChar, 50).Value = entity.Pagador_Nombre_Legal;
    command.Parameters.Add("@Pagador_CURP", SqlDbType.VarChar, 50).Value = entity.Pagador_CURP;
    command.Parameters.Add("@Pagador_Regimen_Fiscal", SqlDbType.Int).Value = entity.Pagador_Regimen_Fiscal;
    command.Parameters.Add("@Pagador_Direccion", SqlDbType.Int).Value = entity.Pagador_Direccion;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Pagador entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Pagador] SET
            [Pagador_Tipo] = @Pagador_Tipo,
            [Pagador_Nombre_Legal] = @Pagador_Nombre_Legal,
            [Pagador_CURP] = @Pagador_CURP,
            [Pagador_Regimen_Fiscal] = @Pagador_Regimen_Fiscal,
            [Pagador_Direccion] = @Pagador_Direccion
            WHERE [Pagador_RFC] = @Pagador_RFC";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Pagador_Tipo", SqlDbType.VarChar, 50).Value = entity.Pagador_Tipo;
    command.Parameters.Add("@Pagador_RFC", SqlDbType.VarChar, 50).Value = entity.Pagador_RFC;
    command.Parameters.Add("@Pagador_Nombre_Legal", SqlDbType.VarChar, 50).Value = entity.Pagador_Nombre_Legal;
    command.Parameters.Add("@Pagador_CURP", SqlDbType.VarChar, 50).Value = entity.Pagador_CURP;
    command.Parameters.Add("@Pagador_Regimen_Fiscal", SqlDbType.Int).Value = entity.Pagador_Regimen_Fiscal;
    command.Parameters.Add("@Pagador_Direccion", SqlDbType.Int).Value = entity.Pagador_Direccion;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(string pagador_RFC)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Pagador] WHERE [Pagador_RFC] = @Pagador_RFC";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Pagador_RFC", SqlDbType.VarChar).Value = pagador_RFC;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Pagador Mapear(SqlDataReader reader)
    {
        return new Pagador
        {
            Pagador_Tipo = reader.GetString(reader.GetOrdinal("Pagador_Tipo")),
        Pagador_RFC = reader.GetString(reader.GetOrdinal("Pagador_RFC")),
        Pagador_Nombre_Legal = reader.GetString(reader.GetOrdinal("Pagador_Nombre_Legal")),
        Pagador_CURP = reader.GetString(reader.GetOrdinal("Pagador_CURP")),
        Pagador_Regimen_Fiscal = reader.GetInt32(reader.GetOrdinal("Pagador_Regimen_Fiscal")),
        Pagador_Direccion = reader.GetInt32(reader.GetOrdinal("Pagador_Direccion"))
        };
    }
}
