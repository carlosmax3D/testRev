using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DatabaseConnection _database;

    public UsuarioRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public Task<Usuario?> ObtenerPorEmailAsync(string email)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            SELECT
                Id_usuario, Email, PW, Nombre, Apellidos,
                NumNomina, FechaNac, Id_tipo, Estatus, Id_usuarioReg, FechaHoraRegistro
            FROM Usuarios
            WHERE Email = @email
            """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@email", SqlDbType.VarChar, 200).Value = email;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            return Task.FromResult<Usuario?>(null);

        return Task.FromResult<Usuario?>(LeerUsuario(reader));
    }

    public Task<List<Usuario>> ObtenerTodosAsync()
    {
        List<Usuario> usuarios = new();

        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            SELECT
                Id_usuario, Email, PW, Nombre, Apellidos,
                NumNomina, FechaNac, Id_tipo, Estatus, Id_usuarioReg, FechaHoraRegistro
            FROM Usuarios
            ORDER BY Nombre, Apellidos
            """;

        using SqlCommand command = new(sql, connection);
        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            usuarios.Add(LeerUsuario(reader));

        return Task.FromResult(usuarios);
    }

    public Task<Usuario?> ObtenerPorIdAsync(int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            SELECT
                Id_usuario, Email, PW, Nombre, Apellidos,
                NumNomina, FechaNac, Id_tipo, Estatus, Id_usuarioReg, FechaHoraRegistro
            FROM Usuarios
            WHERE Id_usuario = @idUsuario
            """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            return Task.FromResult<Usuario?>(null);

        return Task.FromResult<Usuario?>(LeerUsuario(reader));
    }

    public Task<bool> ExisteEmailAsync(
        string email,
        int? idUsuarioExcluir = null)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            SELECT COUNT(1)
            FROM Usuarios
            WHERE Email = @email
              AND (@idUsuarioExcluir IS NULL
                   OR Id_usuario <> @idUsuarioExcluir)
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.Add("@email", SqlDbType.VarChar, 200).Value = email;

        SqlParameter parametroId = command.Parameters.Add(
            "@idUsuarioExcluir", SqlDbType.Int);

        parametroId.Value = idUsuarioExcluir.HasValue
            ? idUsuarioExcluir.Value
            : DBNull.Value;

        connection.Open();

        int cantidad = Convert.ToInt32(command.ExecuteScalar());

        return Task.FromResult(cantidad > 0);
    }

    public Task<int> RegistrarAsync(
        Usuario usuario, 
        int? idUsuarioReg)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            INSERT INTO Usuarios
                (Email, PW, Nombre, Apellidos, NumNomina,
                 FechaNac, Id_tipo, Estatus, Id_usuarioReg)
            OUTPUT INSERTED.Id_usuario
            VALUES
                (@email, @pw, @nombre, @apellidos, @numnomina,
                 @fechaNac, @idTipo, @estatus, @idUsuarioReg)
            """;
        using SqlCommand command = new(sql, connection);

        AgregarParametrosUsuario(command, usuario, incluirId: false);

        command.Parameters.Add("@idUsuarioReg", SqlDbType.Int).Value = 
            idUsuarioReg.HasValue
                ? idUsuarioReg.Value
                : DBNull.Value;

        connection.Open();

        int idGenerado = Convert.ToInt32(command.ExecuteScalar());

        return Task.FromResult(idGenerado);
    }

    public Task<bool> EditarAsync(Usuario usuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            UPDATE Usuarios
            SET Email = @email,
                PW = @pw,
                Nombre = @nombre,
                Apellidos = @apellidos,
                NumNomina = @numnomina,
                FechaNac = @fechaNac,
                Id_tipo = @idTipo,
                Estatus = @estatus
            WHERE Id_usuario = @idUsuario
            """;

        using SqlCommand command = new(sql, connection);

        AgregarParametrosUsuario(command, usuario, incluirId: false);
        command.Parameters.Add("@idUsuario", SqlDbType.Int).Value =
            usuario.Id_usuario;

        connection.Open();

        int filasAfectadas = command.ExecuteNonQuery();

        return Task.FromResult(filasAfectadas > 0);
    }

    public Task<bool> DesactivarAsync(int idUsuario)
    {
        return CambiarEstatusAsync(idUsuario, false);
    }

    public Task<bool> ActivarAsync(int idUsuario)
    {
        return CambiarEstatusAsync(idUsuario, true);
    }

    private Task<bool> CambiarEstatusAsync(int idUsuario, bool estatus)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            UPDATE Usuarios
            SET Estatus = @estatus
            WHERE Id_usuario = @idUsuario
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.Add("@estatus", SqlDbType.Bit).Value = estatus;
        command.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        int filasAfectadas = command.ExecuteNonQuery();

        return Task.FromResult(filasAfectadas > 0);
    }

    private static void AgregarParametrosUsuario(
        SqlCommand command,
        Usuario usuario,
        bool incluirId)
    {
        command.Parameters.Add("@email", SqlDbType.VarChar, 200).Value =
            usuario.Email;

        command.Parameters.Add("@pw", SqlDbType.VarChar, 255).Value =
            usuario.PW;

        command.Parameters.Add("@nombre", SqlDbType.VarChar, 75).Value =
            usuario.Nombre;

        command.Parameters.Add("@apellidos", SqlDbType.VarChar, 150).Value =
            usuario.Apellidos;

        command.Parameters.Add("@numnomina", SqlDbType.VarChar, 50).Value =
            usuario.NumNomina;

        command.Parameters.Add("@fechaNac", SqlDbType.Date).Value =
            usuario.FechaNac.Date;

        command.Parameters.Add("@idTipo", SqlDbType.Int).Value =
            usuario.Id_tipo;

        command.Parameters.Add("@estatus", SqlDbType.Bit).Value =
            usuario.Estatus;

        if (incluirId)
        {
            command.Parameters.Add("@idUsuario", SqlDbType.Int).Value =
                usuario.Id_usuario;
        }
    }

    private static Usuario LeerUsuario(SqlDataReader reader)
    {
        return new Usuario
        {
            Id_usuario = reader.GetInt32(reader.GetOrdinal("Id_usuario")),
            Email = reader.GetString(reader.GetOrdinal("Email")),
            PW = reader.GetString(reader.GetOrdinal("PW")),
            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
            Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
            NumNomina = reader.GetString(reader.GetOrdinal("NumNomina")),
            FechaNac = reader.GetDateTime(reader.GetOrdinal("FechaNac")),
            Id_tipo = reader.GetInt32(reader.GetOrdinal("Id_tipo")),
            Estatus = reader.GetBoolean(reader.GetOrdinal("Estatus")),

            Id_usuarioReg = reader.IsDBNull(reader.GetOrdinal("Id_usuarioReg"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("Id_usuarioReg")),
            FechaHoraRegistro = reader.GetDateTime(
                reader.GetOrdinal("FechaHoraRegistro"))
        };
    }
}
