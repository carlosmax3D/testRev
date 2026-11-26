using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DatabaseConnection _database;

    public UsuarioRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Usuario> GetAll()
    {
        List<Usuario> usuarios = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Usuario_ObtenerTodos", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            usuarios.Add(MapUsuario(reader, incluyeContrasena: false));
        }

        return usuarios;
    }

    public Usuario? GetById(int id)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Usuario_ObtenerPorId", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = id;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? MapUsuario(reader, incluyeContrasena: false) : null;
    }

    public Usuario? GetByCorreo(string correo)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Usuario_ObtenerPorCorreo", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = correo;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? MapUsuario(reader, incluyeContrasena: true) : null;
    }

    public int Add(Usuario usuario, string contrasenaHash)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Usuario_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, 150).Value = usuario.NombreCompleto;
        command.Parameters.Add("@NumNomina", SqlDbType.VarChar, 20).Value = usuario.NumNomina;
        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = usuario.Correo;
        command.Parameters.Add("@IdTipoUsuario", SqlDbType.Int).Value = usuario.IdTipoUsuario;
        command.Parameters.Add("@IdUsuarioRegistro", SqlDbType.Int).Value =
            (object?)usuario.IdUsuarioRegistro ?? DBNull.Value;
        command.Parameters.Add("@ContrasenaHash", SqlDbType.VarChar, 255).Value = contrasenaHash;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado);
    }

    public void Update(Usuario usuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Usuario_Actualizar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = usuario.IdUsuario;
        command.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, 150).Value = usuario.NombreCompleto;
        command.Parameters.Add("@NumNomina", SqlDbType.VarChar, 20).Value = usuario.NumNomina;
        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = usuario.Correo;
        command.Parameters.Add("@IdTipoUsuario", SqlDbType.Int).Value = usuario.IdTipoUsuario;
        command.Parameters.Add("@IdUsuarioModificacion", SqlDbType.Int).Value = usuario.IdUsuarioModificacion!.Value;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void ChangePassword(int idUsuario, string contrasenaHash, int idUsuarioCambio)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Usuario_CambiarContrasena", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
        command.Parameters.Add("@ContrasenaHash", SqlDbType.VarChar, 255).Value = contrasenaHash;
        command.Parameters.Add("@IdUsuarioCambio", SqlDbType.Int).Value = idUsuarioCambio;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void DeleteLogico(int id, int idUsuarioModificacion)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Usuario_EliminarLogico", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = id;
        command.Parameters.Add("@IdUsuarioModificacion", SqlDbType.Int).Value = idUsuarioModificacion;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public bool ExisteCorreoONomina(string correo, string numNomina, int idExcluir = 0)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Usuario_ExisteCorreoONomina", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = correo;
        command.Parameters.Add("@NumNomina", SqlDbType.VarChar, 20).Value = numNomina;
        command.Parameters.Add("@IdExcluir", SqlDbType.Int).Value = idExcluir;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado) > 0;
    }

    private static Usuario MapUsuario(SqlDataReader reader, bool incluyeContrasena)
    {
        Usuario usuario = new()
        {
            IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
            NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
            NumNomina = reader.GetString(reader.GetOrdinal("NumNomina")),
            FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
            Correo = reader.GetString(reader.GetOrdinal("Correo")),
            IdTipoUsuario = reader.GetInt32(reader.GetOrdinal("IdTipoUsuario")),
            TipoUsuario = reader.GetString(reader.GetOrdinal("TipoUsuario")),
            Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
            FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
            IdUsuarioRegistro = reader.IsDBNull(reader.GetOrdinal("IdUsuarioRegistro"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("IdUsuarioRegistro")),
            FechaModificacion = reader.IsDBNull(reader.GetOrdinal("FechaModificacion"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("FechaModificacion")),
            IdUsuarioModificacion = reader.IsDBNull(reader.GetOrdinal("IdUsuarioModificacion"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("IdUsuarioModificacion"))
        };

        if (incluyeContrasena)
        {
            int ordinalContrasena = reader.GetOrdinal("Contrasena");

            usuario.ContrasenaHash = reader.IsDBNull(ordinalContrasena)
                ? null
                : reader.GetString(ordinalContrasena);
        }

        return usuario;
    }
}
