using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;

namespace PIA_PA.Entidades.Usuario;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public UsuarioRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Usuario> ObtenerTodos()
    {
        List<Usuario> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Usuario_ID], [Usuario_Nombre], [Usuario_Nombre_2], [Usuario_Apellido], [Usuario_Apellido_2], [Usuario_Tipo], [Usuario_Email], [Contrasena_ID] FROM [Usuario]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Usuario? ObtenerPorId(int usuario_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Usuario_ID], [Usuario_Nombre], [Usuario_Nombre_2], [Usuario_Apellido], [Usuario_Apellido_2], [Usuario_Tipo], [Usuario_Email], [Contrasena_ID] FROM [Usuario] WHERE [Usuario_ID] = @Usuario_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Usuario_ID", SqlDbType.Int).Value = usuario_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Usuario entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Usuario] ([Usuario_ID], [Usuario_Nombre], [Usuario_Nombre_2], [Usuario_Apellido], [Usuario_Apellido_2], [Usuario_Tipo], [Usuario_Email], [Contrasena_ID])
                         VALUES (@Usuario_ID, @Usuario_Nombre, @Usuario_Nombre_2, @Usuario_Apellido, @Usuario_Apellido_2, @Usuario_Tipo, @Usuario_Email, @Contrasena_ID)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Usuario_ID", SqlDbType.Int).Value = entity.Usuario_ID;
    command.Parameters.Add("@Usuario_Nombre", SqlDbType.VarChar, 50).Value = entity.Usuario_Nombre;
    command.Parameters.Add("@Usuario_Nombre_2", SqlDbType.VarChar, 50).Value = (object?)entity.Usuario_Nombre_2 ?? DBNull.Value;
    command.Parameters.Add("@Usuario_Apellido", SqlDbType.VarChar, 50).Value = entity.Usuario_Apellido;
    command.Parameters.Add("@Usuario_Apellido_2", SqlDbType.VarChar, 50).Value = (object?)entity.Usuario_Apellido_2 ?? DBNull.Value;
    command.Parameters.Add("@Usuario_Tipo", SqlDbType.VarChar, 50).Value = entity.Usuario_Tipo;
    command.Parameters.Add("@Usuario_Email", SqlDbType.VarChar, 50).Value = entity.Usuario_Email;
    command.Parameters.Add("@Contrasena_ID", SqlDbType.Int).Value = entity.Contrasena_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Usuario entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Usuario] SET
            [Usuario_Nombre] = @Usuario_Nombre,
            [Usuario_Nombre_2] = @Usuario_Nombre_2,
            [Usuario_Apellido] = @Usuario_Apellido,
            [Usuario_Apellido_2] = @Usuario_Apellido_2,
            [Usuario_Tipo] = @Usuario_Tipo,
            [Usuario_Email] = @Usuario_Email,
            [Contrasena_ID] = @Contrasena_ID
            WHERE [Usuario_ID] = @Usuario_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Usuario_ID", SqlDbType.Int).Value = entity.Usuario_ID;
    command.Parameters.Add("@Usuario_Nombre", SqlDbType.VarChar, 50).Value = entity.Usuario_Nombre;
    command.Parameters.Add("@Usuario_Nombre_2", SqlDbType.VarChar, 50).Value = (object?)entity.Usuario_Nombre_2 ?? DBNull.Value;
    command.Parameters.Add("@Usuario_Apellido", SqlDbType.VarChar, 50).Value = entity.Usuario_Apellido;
    command.Parameters.Add("@Usuario_Apellido_2", SqlDbType.VarChar, 50).Value = (object?)entity.Usuario_Apellido_2 ?? DBNull.Value;
    command.Parameters.Add("@Usuario_Tipo", SqlDbType.VarChar, 50).Value = entity.Usuario_Tipo;
    command.Parameters.Add("@Usuario_Email", SqlDbType.VarChar, 50).Value = entity.Usuario_Email;
    command.Parameters.Add("@Contrasena_ID", SqlDbType.Int).Value = entity.Contrasena_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int usuario_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Usuario] WHERE [Usuario_ID] = @Usuario_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Usuario_ID", SqlDbType.Int).Value = usuario_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Usuario Mapear(SqlDataReader reader)
    {
        return new Usuario
        {
            Usuario_ID = reader.GetInt32(reader.GetOrdinal("Usuario_ID")),
        Usuario_Nombre = reader.GetString(reader.GetOrdinal("Usuario_Nombre")),
        Usuario_Nombre_2 = reader.IsDBNull(reader.GetOrdinal("Usuario_Nombre_2")) ? null : reader.GetString(reader.GetOrdinal("Usuario_Nombre_2")),
        Usuario_Apellido = reader.GetString(reader.GetOrdinal("Usuario_Apellido")),
        Usuario_Apellido_2 = reader.IsDBNull(reader.GetOrdinal("Usuario_Apellido_2")) ? null : reader.GetString(reader.GetOrdinal("Usuario_Apellido_2")),
        Usuario_Tipo = reader.GetString(reader.GetOrdinal("Usuario_Tipo")),
        Usuario_Email = reader.GetString(reader.GetOrdinal("Usuario_Email")),
        Contrasena_ID = reader.GetInt32(reader.GetOrdinal("Contrasena_ID"))
        };
    }

    public bool ValidarCredenciales(string email, string contrasena)
    {
        using var connection = _databaseConnection.GetConnection();
        connection.Open();

        string query = @"
        SELECT COUNT(1)
        FROM Usuario U
        INNER JOIN Contrasena C
            ON U.Contrasena_ID = C.Contrasena_ID
        WHERE U.Usuario_Email = @Email
          AND U.Usuario_Activo = 1
          AND U.Usuario_Tipo IN ('Administrador','Operativo')
          AND C.Contrasena_Actual = 1
          AND C.Contrasena_Hash = @Contrasena";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Email", email.Trim());
        command.Parameters.AddWithValue("@Contrasena", contrasena);

        object? resultado = command.ExecuteScalar();
        int cantidad = Convert.ToInt32(resultado);

        return cantidad > 0;
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Tabla(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Valor(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Ejecutar(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Transaccion(accion);
}
