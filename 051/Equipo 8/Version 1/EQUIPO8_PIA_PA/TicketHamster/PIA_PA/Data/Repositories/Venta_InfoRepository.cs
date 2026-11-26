using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Venta_Info;

public class Venta_InfoRepository : IVenta_InfoRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public Venta_InfoRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Venta_Info> ObtenerTodos()
    {
        List<Venta_Info> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Venta_Info_ID], [Venta_Info_Fecha], [Venta_Info_Subtotal], [Venta_Info_IVA], [Venta_Info_Total], [Usuario_ID], [Pago_ID], [Pagador_RFC], [Emisor_ID] FROM [Venta_Info]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Venta_Info? ObtenerPorId(int venta_Info_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Venta_Info_ID], [Venta_Info_Fecha], [Venta_Info_Subtotal], [Venta_Info_IVA], [Venta_Info_Total], [Usuario_ID], [Pago_ID], [Pagador_RFC], [Emisor_ID] FROM [Venta_Info] WHERE [Venta_Info_ID] = @Venta_Info_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Venta_Info_ID", SqlDbType.Int).Value = venta_Info_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Venta_Info entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Venta_Info] ([Venta_Info_ID], [Venta_Info_Fecha], [Venta_Info_Subtotal], [Venta_Info_IVA], [Venta_Info_Total], [Usuario_ID], [Pago_ID], [Pagador_RFC], [Emisor_ID])
                         VALUES (@Venta_Info_ID, @Venta_Info_Fecha, @Venta_Info_Subtotal, @Venta_Info_IVA, @Venta_Info_Total, @Usuario_ID, @Pago_ID, @Pagador_RFC, @Emisor_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Venta_Info_ID", SqlDbType.Int).Value = entity.Venta_Info_ID;
    command.Parameters.Add("@Venta_Info_Fecha", SqlDbType.Date).Value = entity.Venta_Info_Fecha.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Venta_Info_Subtotal", SqlDbType.Float).Value = entity.Venta_Info_Subtotal;
    command.Parameters.Add("@Venta_Info_IVA", SqlDbType.Float).Value = entity.Venta_Info_IVA;
    command.Parameters.Add("@Venta_Info_Total", SqlDbType.Float).Value = entity.Venta_Info_Total;
    command.Parameters.Add("@Usuario_ID", SqlDbType.Int).Value = entity.Usuario_ID;
    command.Parameters.Add("@Pago_ID", SqlDbType.Int).Value = entity.Pago_ID;
    command.Parameters.Add("@Pagador_RFC", SqlDbType.VarChar, 50).Value = entity.Pagador_RFC;
    command.Parameters.Add("@Emisor_ID", SqlDbType.Int).Value = entity.Emisor_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Venta_Info entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Venta_Info] SET
            [Venta_Info_Fecha] = @Venta_Info_Fecha,
            [Venta_Info_Subtotal] = @Venta_Info_Subtotal,
            [Venta_Info_IVA] = @Venta_Info_IVA,
            [Venta_Info_Total] = @Venta_Info_Total,
            [Usuario_ID] = @Usuario_ID,
            [Pago_ID] = @Pago_ID,
            [Pagador_RFC] = @Pagador_RFC,
            [Emisor_ID] = @Emisor_ID
            WHERE [Venta_Info_ID] = @Venta_Info_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Venta_Info_ID", SqlDbType.Int).Value = entity.Venta_Info_ID;
    command.Parameters.Add("@Venta_Info_Fecha", SqlDbType.Date).Value = entity.Venta_Info_Fecha.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Venta_Info_Subtotal", SqlDbType.Float).Value = entity.Venta_Info_Subtotal;
    command.Parameters.Add("@Venta_Info_IVA", SqlDbType.Float).Value = entity.Venta_Info_IVA;
    command.Parameters.Add("@Venta_Info_Total", SqlDbType.Float).Value = entity.Venta_Info_Total;
    command.Parameters.Add("@Usuario_ID", SqlDbType.Int).Value = entity.Usuario_ID;
    command.Parameters.Add("@Pago_ID", SqlDbType.Int).Value = entity.Pago_ID;
    command.Parameters.Add("@Pagador_RFC", SqlDbType.VarChar, 50).Value = entity.Pagador_RFC;
    command.Parameters.Add("@Emisor_ID", SqlDbType.Int).Value = entity.Emisor_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int venta_Info_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Venta_Info] WHERE [Venta_Info_ID] = @Venta_Info_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Venta_Info_ID", SqlDbType.Int).Value = venta_Info_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Venta_Info Mapear(SqlDataReader reader)
    {
        return new Venta_Info
        {
            Venta_Info_ID = reader.GetInt32(reader.GetOrdinal("Venta_Info_ID")),
        Venta_Info_Fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Venta_Info_Fecha"))),
        Venta_Info_Subtotal = reader.GetDouble(reader.GetOrdinal("Venta_Info_Subtotal")),
        Venta_Info_IVA = reader.GetDouble(reader.GetOrdinal("Venta_Info_IVA")),
        Venta_Info_Total = reader.GetDouble(reader.GetOrdinal("Venta_Info_Total")),
        Usuario_ID = reader.GetInt32(reader.GetOrdinal("Usuario_ID")),
        Pago_ID = reader.GetInt32(reader.GetOrdinal("Pago_ID")),
        Pagador_RFC = reader.GetString(reader.GetOrdinal("Pagador_RFC")),
        Emisor_ID = reader.GetInt32(reader.GetOrdinal("Emisor_ID"))
        };
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Tabla(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Valor(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Ejecutar(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Transaccion(accion);
}
