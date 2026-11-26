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
    // Consultar usuario por correo electrónico.
    public Task<Usuario?> ObtenerPorEmailAsync(string email)
    {
        using SqlConnection connection = _database.CreateConnection();
        const string sql = """
           SELECT
               Id_user AS Id_usuario,
               Correo AS Email,
               [Contraseña] AS PW,
               Nombre,
               Apellido AS Apellidos,
               Nomina,
               Fecha_nacimiento AS FechaNac,
               Id_tipo,
               Estatus,
               Fecha_registro AS FechaHoraRegistro
           FROM User_
           WHERE Correo = @email
           """;
        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@email", SqlDbType.VarChar, 200).Value =
            email;
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read())
            return Task.FromResult<Usuario?>(null);
        return Task.FromResult<Usuario?>(LeerUsuario(reader));
    }
    // Consultar todos los usuarios.
    public Task<List<Usuario>> ObtenerTodosAsync()
    {
        List<Usuario> usuarios = new();
        using SqlConnection connection = _database.CreateConnection();
        const string sql = """
           SELECT
               Id_user AS Id_usuario,
               Correo AS Email,
               [Contraseña] AS PW,
               Nombre,
               Apellido AS Apellidos,
               Nomina,
               Fecha_nacimiento AS FechaNac,
               Id_tipo,
               Estatus,
               Fecha_registro AS FechaHoraRegistro
           FROM User_
           ORDER BY Nombre, Apellido
           """;
        using SqlCommand command = new(sql, connection);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
            usuarios.Add(LeerUsuario(reader));
        return Task.FromResult(usuarios);
    }
    // Consultar usuario por identificador.
    public Task<Usuario?> ObtenerPorIdAsync(int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();
        const string sql = """
           SELECT
               Id_user AS Id_usuario,
               Correo AS Email,
               [Contraseña] AS PW,
               Nombre,
               Apellido AS Apellidos,
               Nomina,
               Fecha_nacimiento AS FechaNac,
               Id_tipo,
               Estatus,
               Fecha_registro AS FechaHoraRegistro
           FROM User_
           WHERE Id_user = @idUsuario
           """;
        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@idUsuario", SqlDbType.Int).Value =
            idUsuario;
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read())
            return Task.FromResult<Usuario?>(null);
        return Task.FromResult<Usuario?>(LeerUsuario(reader));
    }
    // Comprobar si existe un correo.
    public Task<bool> ExisteEmailAsync(
        string email,
        int? idUsuarioExcluir = null)
    {
        using SqlConnection connection = _database.CreateConnection();
        const string sql = """
           SELECT COUNT(1)
           FROM User_
           WHERE Correo = @email
             AND (@idUsuarioExcluir IS NULL
                  OR Id_user <> @idUsuarioExcluir)
           """;
        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@email", SqlDbType.VarChar, 200).Value =
            email;
        SqlParameter parametroId = command.Parameters.Add(
            "@idUsuarioExcluir", SqlDbType.Int);
        parametroId.Value = idUsuarioExcluir.HasValue
            ? idUsuarioExcluir.Value
            : DBNull.Value;
        connection.Open();
        int cantidad = Convert.ToInt32(command.ExecuteScalar());
        return Task.FromResult(cantidad > 0);
    }
    // Registrar usuario.
    public Task<int> RegistrarAsync(Usuario usuario)
    {
        using SqlConnection connection = _database.CreateConnection();
        const string sql = """
           INSERT INTO User_
           (
               Correo,
               [Contraseña],
               Nombre,
               Nomina,
               Fecha_nacimiento,
               Fecha_registro,
               Apellido,
               Id_tipo,
               Estatus
           )
           OUTPUT INSERTED.Id_user
           VALUES
           (
               @email,
               @pw,
               @nombre,
               @nomina,
               @fechaNac,
               @fechaRegistro,
               @apellidos,
               @idTipo,
               @estatus
           )
           """;
        using SqlCommand command = new(sql, connection);
        AgregarParametrosUsuario(command, usuario);
        command.Parameters.Add(
            "@fechaRegistro", SqlDbType.Date).Value =
            usuario.FechaHoraRegistro.Date;
        connection.Open();
        int idGenerado = Convert.ToInt32(command.ExecuteScalar());
        return Task.FromResult(idGenerado);
    }
    // Modificar usuario.
    public Task<bool> EditarAsync(Usuario usuario)
    {
        using SqlConnection connection = _database.CreateConnection();
        const string sql = """
           UPDATE User_
           SET
               Correo = @email,
               [Contraseña] = @pw,
               Nombre = @nombre,
               Nomina = @nomina,
               Fecha_nacimiento = @fechaNac,
               Apellido = @apellidos,
               Id_tipo = @idTipo,
               Estatus = @estatus
           WHERE Id_user = @idUsuario
           """;
        using SqlCommand command = new(sql, connection);
        AgregarParametrosUsuario(command, usuario);
        command.Parameters.Add("@idUsuario", SqlDbType.Int).Value =
            usuario.Id_usuario;
        connection.Open();
        int filasAfectadas = command.ExecuteNonQuery();
        return Task.FromResult(filasAfectadas > 0);
    }
    // Desactivar usuario.
    public Task<bool> DesactivarAsync(int idUsuario)
    {
        return CambiarEstatusAsync(idUsuario, false);
    }
    // Activar usuario.
    public Task<bool> ActivarAsync(int idUsuario)
    {
        return CambiarEstatusAsync(idUsuario, true);
    }
    // Cambiar estado del usuario.
    private Task<bool> CambiarEstatusAsync(
        int idUsuario,
        bool estatus)
    {
        using SqlConnection connection = _database.CreateConnection();
        const string sql = """
           UPDATE User_
           SET Estatus = @estatus
           WHERE Id_user = @idUsuario
           """;
        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@estatus", SqlDbType.Bit).Value =
            estatus;
        command.Parameters.Add("@idUsuario", SqlDbType.Int).Value =
            idUsuario;
        connection.Open();
        int filasAfectadas = command.ExecuteNonQuery();
        return Task.FromResult(filasAfectadas > 0);
    }
    // Agregar parámetros.
    private static void AgregarParametrosUsuario(
        SqlCommand command,
        Usuario usuario)
    {
        command.Parameters.Add(
            "@email", SqlDbType.VarChar, 200).Value =
            usuario.Email;
        command.Parameters.Add(
            "@pw", SqlDbType.VarChar, 100).Value =
            usuario.PW;
        command.Parameters.Add(
            "@nombre", SqlDbType.VarChar, 200).Value =
            usuario.Nombre;
        command.Parameters.Add(
            "@apellidos", SqlDbType.VarChar, 100).Value =
            usuario.Apellidos;
        command.Parameters.Add(
            "@nomina", SqlDbType.VarChar, 50).Value =
            usuario.Nomina;
        command.Parameters.Add(
            "@fechaNac", SqlDbType.Date).Value =
            usuario.FechaNac.Date;
        command.Parameters.Add(
            "@idTipo", SqlDbType.Int).Value =
            usuario.Id_tipo;
        command.Parameters.Add(
            "@estatus", SqlDbType.Bit).Value =
            usuario.Estatus;
    }
    // Convertir resultado SQL a objeto Usuario.
    private static Usuario LeerUsuario(SqlDataReader reader)
    {
        return new Usuario
        {
            Id_usuario = reader.GetInt32(
                reader.GetOrdinal("Id_usuario")),
            Email = reader.GetString(
                reader.GetOrdinal("Email")),
            PW = reader.GetString(
                reader.GetOrdinal("PW")),
            Nombre = reader.GetString(
                reader.GetOrdinal("Nombre")),
            Apellidos = reader.GetString(
                reader.GetOrdinal("Apellidos")),
            Nomina = reader.GetString(
                reader.GetOrdinal("Nomina")),
            FechaNac = reader.GetDateTime(
                reader.GetOrdinal("FechaNac")),
            Id_tipo = reader.GetInt32(
                reader.GetOrdinal("Id_tipo")),
            Estatus = reader.GetBoolean(
                reader.GetOrdinal("Estatus")),
            FechaHoraRegistro = reader.GetDateTime(
                reader.GetOrdinal("FechaHoraRegistro"))
        };
    }
}