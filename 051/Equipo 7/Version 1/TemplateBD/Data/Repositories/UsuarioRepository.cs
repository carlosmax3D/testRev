using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public Usuario? ObtenerPorCorreo(string correo)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_IniciarSesion", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = correo;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read()) return null;

        Usuario u = MapUsuario(reader);
        u.ContrasenaHash = (byte[])reader["contrasenaHash"];
        u.ContrasenaSalt = (byte[])reader["contrasenaSalt"];
        return u;
    }

    public List<Usuario> GetAll()
    {
        List<Usuario> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ObtenerUsuarios", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(MapUsuario(reader));
        }

        return lista;
    }

    public void Add(Usuario usuario)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_CrearUsuario", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@NumeroNomina", SqlDbType.VarChar, 20).Value = usuario.NumeroNomina;
        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
        command.Parameters.Add("@ApellidoPaterno", SqlDbType.VarChar, 50).Value = usuario.ApellidoPaterno;
        command.Parameters.Add("@ApellidoMaterno", SqlDbType.VarChar, 50).Value = usuario.ApellidoMaterno;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = usuario.Correo;
        command.Parameters.Add("@ContrasenaHash", SqlDbType.VarBinary, 32).Value = usuario.ContrasenaHash;
        command.Parameters.Add("@ContrasenaSalt", SqlDbType.VarBinary, 16).Value = usuario.ContrasenaSalt;
        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;
        command.Parameters.Add("@TipoUsuario", SqlDbType.VarChar, 30).Value = usuario.TipoUsuario;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = usuario.UsuarioAuditoria;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(Usuario usuario)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ActualizarUsuario", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = usuario.IdUsuario;
        command.Parameters.Add("@NumeroNomina", SqlDbType.VarChar, 20).Value = usuario.NumeroNomina;
        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
        command.Parameters.Add("@ApellidoPaterno", SqlDbType.VarChar, 50).Value = usuario.ApellidoPaterno;
        command.Parameters.Add("@ApellidoMaterno", SqlDbType.VarChar, 50).Value = usuario.ApellidoMaterno;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = usuario.Correo;

        command.Parameters.Add("@ContrasenaHash", SqlDbType.VarBinary, 32).Value =
            usuario.ContrasenaHash.Length > 0 ? usuario.ContrasenaHash : DBNull.Value;

        command.Parameters.Add("@ContrasenaSalt", SqlDbType.VarBinary, 16).Value =
            usuario.ContrasenaSalt.Length > 0 ? usuario.ContrasenaSalt : DBNull.Value;

        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;
        command.Parameters.Add("@TipoUsuario", SqlDbType.VarChar, 30).Value = usuario.TipoUsuario;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = usuario.UsuarioAuditoria;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int idUsuario, string usuarioAuditoria)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_EliminarUsuario", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = usuarioAuditoria;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Usuario MapUsuario(SqlDataReader reader)
    {
        string tipo = reader.GetString(reader.GetOrdinal("tipoUsuario"));
        Usuario u = Usuario.CrearInstanciaPorRol(tipo);

        u.IdUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario"));
        u.NumeroNomina = reader.GetString(reader.GetOrdinal("numeroNomina"));
        u.Nombre = reader.GetString(reader.GetOrdinal("nombre"));
        u.ApellidoPaterno = reader.GetString(reader.GetOrdinal("apellidoPaterno"));
        u.ApellidoMaterno = reader.GetString(reader.GetOrdinal("apellidoMaterno"));
        u.Correo = reader.GetString(reader.GetOrdinal("correo"));
        u.FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fechaNacimiento"));

        return u;
    }
}