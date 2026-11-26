using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Cancelacion;

public class CancelacionRepository : ICancelacionRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public CancelacionRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Cancelacion> ObtenerTodos()
    {
        List<Cancelacion> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Cancelacion_ID], [Cancelacion_Motivo], [Cancelacion_Fecha], [Ticket_ID] FROM [Cancelacion]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Cancelacion? ObtenerPorId(int cancelacion_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Cancelacion_ID], [Cancelacion_Motivo], [Cancelacion_Fecha], [Ticket_ID] FROM [Cancelacion] WHERE [Cancelacion_ID] = @Cancelacion_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Cancelacion_ID", SqlDbType.Int).Value = cancelacion_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Cancelacion entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Cancelacion] ([Cancelacion_ID], [Cancelacion_Motivo], [Cancelacion_Fecha], [Ticket_ID])
                         VALUES (@Cancelacion_ID, @Cancelacion_Motivo, @Cancelacion_Fecha, @Ticket_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Cancelacion_ID", SqlDbType.Int).Value = entity.Cancelacion_ID;
    command.Parameters.Add("@Cancelacion_Motivo", SqlDbType.VarChar, 200).Value = entity.Cancelacion_Motivo;
    command.Parameters.Add("@Cancelacion_Fecha", SqlDbType.Date).Value = entity.Cancelacion_Fecha.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Ticket_ID", SqlDbType.Int).Value = entity.Ticket_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Cancelacion entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Cancelacion] SET
            [Cancelacion_Motivo] = @Cancelacion_Motivo,
            [Cancelacion_Fecha] = @Cancelacion_Fecha,
            [Ticket_ID] = @Ticket_ID
            WHERE [Cancelacion_ID] = @Cancelacion_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Cancelacion_ID", SqlDbType.Int).Value = entity.Cancelacion_ID;
    command.Parameters.Add("@Cancelacion_Motivo", SqlDbType.VarChar, 200).Value = entity.Cancelacion_Motivo;
    command.Parameters.Add("@Cancelacion_Fecha", SqlDbType.Date).Value = entity.Cancelacion_Fecha.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Ticket_ID", SqlDbType.Int).Value = entity.Ticket_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int cancelacion_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Cancelacion] WHERE [Cancelacion_ID] = @Cancelacion_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Cancelacion_ID", SqlDbType.Int).Value = cancelacion_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Cancelacion Mapear(SqlDataReader reader)
    {
        return new Cancelacion
        {
            Cancelacion_ID = reader.GetInt32(reader.GetOrdinal("Cancelacion_ID")),
        Cancelacion_Motivo = reader.GetString(reader.GetOrdinal("Cancelacion_Motivo")),
        Cancelacion_Fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Cancelacion_Fecha"))),
        Ticket_ID = reader.GetInt32(reader.GetOrdinal("Ticket_ID"))
        };
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Tabla(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Valor(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Ejecutar(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Transaccion(accion);
}
