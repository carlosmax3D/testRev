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

    public Usuario? Login(string correo, string contrasenaHash)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Login", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Correo", SqlDbType.VarChar, Longitudes.Correo).Value = correo;
        command.Parameters.Add("@ContrasenaHash", SqlDbType.Char, Longitudes.ContrasenaHash).Value = contrasenaHash;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return MapUsuario(reader);
    }

    public List<Usuario> Consultar(string? busqueda, int idAdministradorAccion, bool incluirInactivos = false)
    {
        List<Usuario> usuarios = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Usuario_Consultar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministradorAccion;
        command.Parameters.Add("@Busqueda", SqlDbType.VarChar, Longitudes.Busqueda).Value =
            string.IsNullOrWhiteSpace(busqueda) ? DBNull.Value : busqueda.Trim();
        command.Parameters.Add("@IncluirInactivos", SqlDbType.Bit).Value = incluirInactivos;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Usuario usuario = MapUsuario(reader);
            usuario.Activo = reader.GetBoolean(reader.GetOrdinal("Activo"));
            RegistroMapper.LeerRegistro(reader, usuario);
            RegistroMapper.LeerModificacion(reader, usuario);
            usuarios.Add(usuario);
        }

        return usuarios;
    }

    public int Insertar(Usuario usuario, string contrasenaHash, int idUsuarioRegistro)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Usuario_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@TipoUsuario", SqlDbType.VarChar, Longitudes.TipoUsuario).Value = usuario.TipoUsuario;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, Longitudes.Correo).Value = usuario.Correo;
        command.Parameters.Add("@ContrasenaHash", SqlDbType.Char, Longitudes.ContrasenaHash).Value = contrasenaHash;
        command.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, Longitudes.NombreUsuario).Value = usuario.NombreCompleto;
        command.Parameters.Add("@NumeroNomina", SqlDbType.Int).Value = usuario.NumeroNomina;
        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;
        command.Parameters.Add("@IdUsuarioRegistro", SqlDbType.Int).Value = idUsuarioRegistro;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado);
    }

    public void Actualizar(Usuario usuario, string? nuevaContrasenaHash, int idAdministradorAccion, string? hashEsperado)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Usuario_Actualizar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = usuario.IdUsuario;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministradorAccion;
        command.Parameters.Add("@HashEsperado", SqlDbType.Char, Longitudes.ContrasenaHash).Value = (object?)hashEsperado ?? DBNull.Value;
        command.Parameters.Add("@TipoUsuario", SqlDbType.VarChar, Longitudes.TipoUsuario).Value = usuario.TipoUsuario;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, Longitudes.Correo).Value = usuario.Correo;
        command.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, Longitudes.NombreUsuario).Value = usuario.NombreCompleto;
        command.Parameters.Add("@NumeroNomina", SqlDbType.Int).Value = usuario.NumeroNomina;
        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;

        // DBNull: se conserva la contraseña actual
        command.Parameters.Add("@ContrasenaHash", SqlDbType.Char, Longitudes.ContrasenaHash).Value =
            nuevaContrasenaHash is null ? DBNull.Value : nuevaContrasenaHash;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void Reactivar(int idUsuario, int idAdministradorAccion)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Usuario_Reactivar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministradorAccion;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void Eliminar(int idUsuario, int idAdministradorAccion)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Usuario_Eliminar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministradorAccion;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public string? ObtenerCredencial(string correo)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Usuario_ObtenerCredencial", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Correo", SqlDbType.VarChar, Longitudes.Correo).Value = correo;

        connection.Open();

        return command.ExecuteScalar() as string;
    }

    public List<string> ObtenerHistorial(int idUsuario, int idAdministradorAccion)
    {
        List<string> hashes = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Usuario_Historial", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministradorAccion;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            hashes.Add(reader.GetString(reader.GetOrdinal("ContrasenaHash")));
        }

        return hashes;
    }

    public bool RequiereAdministradorInicial()
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Usuario_RequiereInicial", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();

        return Convert.ToBoolean(command.ExecuteScalar());
    }

    public int CrearAdministradorInicial(Usuario usuario, Empresa empresa, string contrasenaHash)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Usuario_CrearAdministradorInicial", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Correo", SqlDbType.VarChar, Longitudes.Correo).Value = usuario.Correo;
        command.Parameters.Add("@ContrasenaHash", SqlDbType.Char, Longitudes.ContrasenaHash).Value = contrasenaHash;
        command.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, Longitudes.NombreUsuario).Value = usuario.NombreCompleto;
        command.Parameters.Add("@NumeroNomina", SqlDbType.Int).Value = usuario.NumeroNomina;
        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;
        command.Parameters.Add("@RazonSocial", SqlDbType.VarChar, Longitudes.RazonSocial).Value = empresa.RazonSocial;
        command.Parameters.Add("@RFC", SqlDbType.VarChar, Longitudes.RfcEmpresa).Value = empresa.RFC;
        command.Parameters.Add("@Domicilio", SqlDbType.VarChar, Longitudes.DomicilioFiscal).Value = empresa.Domicilio;

        connection.Open();

        return Convert.ToInt32(command.ExecuteScalar());
    }

    private static Usuario MapUsuario(SqlDataReader reader)
    {
        return new Usuario
        {
            IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
            TipoUsuario = reader.GetString(reader.GetOrdinal("TipoUsuario")),
            NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
            Correo = reader.GetString(reader.GetOrdinal("Correo")),
            NumeroNomina = reader.GetInt32(reader.GetOrdinal("NumeroNomina")),
            FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento"))
        };
    }
}
