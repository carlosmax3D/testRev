using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Cliente;

public class ClienteRepository : IClienteRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public ClienteRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Cliente> ObtenerTodos()
    {
        List<Cliente> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Cliente_ID], [Cliente_RFC], [Cliente_Email], [Cliente_Fecha_Nacimiento], [Cliente_Nombre], [Cliente_Segundo_Nombre], [Cliente_Apellido_Paterno], [Cliente_Apellido_Materno], [Direccion_ID], [Telefono_ID] FROM [Cliente]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Cliente? ObtenerPorId(int cliente_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Cliente_ID], [Cliente_RFC], [Cliente_Email], [Cliente_Fecha_Nacimiento], [Cliente_Nombre], [Cliente_Segundo_Nombre], [Cliente_Apellido_Paterno], [Cliente_Apellido_Materno], [Direccion_ID], [Telefono_ID] FROM [Cliente] WHERE [Cliente_ID] = @Cliente_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Cliente_ID", SqlDbType.Int).Value = cliente_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Cliente entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Cliente] ([Cliente_ID], [Cliente_RFC], [Cliente_Email], [Cliente_Fecha_Nacimiento], [Cliente_Nombre], [Cliente_Segundo_Nombre], [Cliente_Apellido_Paterno], [Cliente_Apellido_Materno], [Direccion_ID], [Telefono_ID])
                         VALUES (@Cliente_ID, @Cliente_RFC, @Cliente_Email, @Cliente_Fecha_Nacimiento, @Cliente_Nombre, @Cliente_Segundo_Nombre, @Cliente_Apellido_Paterno, @Cliente_Apellido_Materno, @Direccion_ID, @Telefono_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Cliente_ID", SqlDbType.Int).Value = entity.Cliente_ID;
    command.Parameters.Add("@Cliente_RFC", SqlDbType.VarChar, 50).Value = entity.Cliente_RFC;
    command.Parameters.Add("@Cliente_Email", SqlDbType.VarChar, 50).Value = entity.Cliente_Email;
    command.Parameters.Add("@Cliente_Fecha_Nacimiento", SqlDbType.Date).Value = entity.Cliente_Fecha_Nacimiento.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Cliente_Nombre", SqlDbType.VarChar, 50).Value = entity.Cliente_Nombre;
    command.Parameters.Add("@Cliente_Segundo_Nombre", SqlDbType.VarChar, 50).Value = (object?)entity.Cliente_Segundo_Nombre ?? DBNull.Value;
    command.Parameters.Add("@Cliente_Apellido_Paterno", SqlDbType.VarChar, 50).Value = entity.Cliente_Apellido_Paterno;
    command.Parameters.Add("@Cliente_Apellido_Materno", SqlDbType.VarChar, 50).Value = (object?)entity.Cliente_Apellido_Materno ?? DBNull.Value;
    command.Parameters.Add("@Direccion_ID", SqlDbType.Int).Value = entity.Direccion_ID;
    command.Parameters.Add("@Telefono_ID", SqlDbType.Int).Value = entity.Telefono_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Cliente entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Cliente] SET
            [Cliente_RFC] = @Cliente_RFC,
            [Cliente_Email] = @Cliente_Email,
            [Cliente_Fecha_Nacimiento] = @Cliente_Fecha_Nacimiento,
            [Cliente_Nombre] = @Cliente_Nombre,
            [Cliente_Segundo_Nombre] = @Cliente_Segundo_Nombre,
            [Cliente_Apellido_Paterno] = @Cliente_Apellido_Paterno,
            [Cliente_Apellido_Materno] = @Cliente_Apellido_Materno,
            [Direccion_ID] = @Direccion_ID,
            [Telefono_ID] = @Telefono_ID
            WHERE [Cliente_ID] = @Cliente_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Cliente_ID", SqlDbType.Int).Value = entity.Cliente_ID;
    command.Parameters.Add("@Cliente_RFC", SqlDbType.VarChar, 50).Value = entity.Cliente_RFC;
    command.Parameters.Add("@Cliente_Email", SqlDbType.VarChar, 50).Value = entity.Cliente_Email;
    command.Parameters.Add("@Cliente_Fecha_Nacimiento", SqlDbType.Date).Value = entity.Cliente_Fecha_Nacimiento.ToDateTime(TimeOnly.MinValue);
    command.Parameters.Add("@Cliente_Nombre", SqlDbType.VarChar, 50).Value = entity.Cliente_Nombre;
    command.Parameters.Add("@Cliente_Segundo_Nombre", SqlDbType.VarChar, 50).Value = (object?)entity.Cliente_Segundo_Nombre ?? DBNull.Value;
    command.Parameters.Add("@Cliente_Apellido_Paterno", SqlDbType.VarChar, 50).Value = entity.Cliente_Apellido_Paterno;
    command.Parameters.Add("@Cliente_Apellido_Materno", SqlDbType.VarChar, 50).Value = (object?)entity.Cliente_Apellido_Materno ?? DBNull.Value;
    command.Parameters.Add("@Direccion_ID", SqlDbType.Int).Value = entity.Direccion_ID;
    command.Parameters.Add("@Telefono_ID", SqlDbType.Int).Value = entity.Telefono_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int cliente_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Cliente] WHERE [Cliente_ID] = @Cliente_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Cliente_ID", SqlDbType.Int).Value = cliente_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Cliente Mapear(SqlDataReader reader)
    {
        return new Cliente
        {
            Cliente_ID = reader.GetInt32(reader.GetOrdinal("Cliente_ID")),
        Cliente_RFC = reader.GetString(reader.GetOrdinal("Cliente_RFC")),
        Cliente_Email = reader.GetString(reader.GetOrdinal("Cliente_Email")),
        Cliente_Fecha_Nacimiento = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Cliente_Fecha_Nacimiento"))),
        Cliente_Nombre = reader.GetString(reader.GetOrdinal("Cliente_Nombre")),
        Cliente_Segundo_Nombre = reader.IsDBNull(reader.GetOrdinal("Cliente_Segundo_Nombre")) ? null : reader.GetString(reader.GetOrdinal("Cliente_Segundo_Nombre")),
        Cliente_Apellido_Paterno = reader.GetString(reader.GetOrdinal("Cliente_Apellido_Paterno")),
        Cliente_Apellido_Materno = reader.IsDBNull(reader.GetOrdinal("Cliente_Apellido_Materno")) ? null : reader.GetString(reader.GetOrdinal("Cliente_Apellido_Materno")),
        Direccion_ID = reader.GetInt32(reader.GetOrdinal("Direccion_ID")),
        Telefono_ID = reader.GetInt32(reader.GetOrdinal("Telefono_ID"))
        };
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Tabla(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Valor(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Ejecutar(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Transaccion(accion);
}
