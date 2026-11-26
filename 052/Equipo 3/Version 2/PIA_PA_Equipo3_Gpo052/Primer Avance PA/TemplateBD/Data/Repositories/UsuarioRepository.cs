using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
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

    public List<Usuario> GetAll()
    {
        List<Usuario> usuarios = [];
        using SqlConnection connection = _database.CreateConnection();

        // Usamos Stored Procedure
        using SqlCommand command = new("sp_ObtenerUsuarios", connection);
        command.CommandType = CommandType.StoredProcedure;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            usuarios.Add(MapUsuario(reader));
        }

        return usuarios;
    }

    public Usuario? GetById(int id) { /* Implementar con SP si es necesario */ return null; }

    public Usuario? Login(string correo, string contrasena)
    {
        using SqlConnection connection = _database.CreateConnection();

        // Usamos Stored Procedure
        using SqlCommand command = new("sp_LoginUsuario", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Correo", SqlDbType.NVarChar, 100).Value = correo;
        command.Parameters.Add("@Contrasena", SqlDbType.NVarChar, 50).Value = contrasena;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read()) return null;

        return MapUsuario(reader);
    }

    public void Add(Usuario usuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        // Usamos Stored Procedure
        using SqlCommand command = new("sp_InsertarUsuario", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Correo", SqlDbType.NVarChar, 100).Value = usuario.CorreoElectronico;
        command.Parameters.Add("@Contrasena", SqlDbType.NVarChar, 50).Value = usuario.Contrasena;
        command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = usuario.Nombre;
        command.Parameters.Add("@Paterno", SqlDbType.NVarChar, 100).Value = usuario.ApellidoPaterno;
        command.Parameters.Add("@Materno", SqlDbType.NVarChar, 100).Value = (object?)usuario.ApellidoMaterno ?? DBNull.Value;
        command.Parameters.Add("@Nomina", SqlDbType.NVarChar, 20).Value = usuario.NumeroNomina;
        command.Parameters.Add("@FechaNac", SqlDbType.Date).Value = usuario.FechaNacimiento;
        command.Parameters.Add("@Tipo", SqlDbType.NVarChar, 20).Value = usuario.TipoUsuario;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(Usuario usuario)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ActualizarUsuario", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Id", SqlDbType.Int).Value = usuario.IdUsuario;
        command.Parameters.Add("@Correo", SqlDbType.NVarChar, 100).Value = usuario.CorreoElectronico;
        command.Parameters.Add("@Contrasena", SqlDbType.NVarChar, 50).Value = usuario.Contrasena;
        command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = usuario.Nombre;
        command.Parameters.Add("@Paterno", SqlDbType.NVarChar, 100).Value = usuario.ApellidoPaterno;
        command.Parameters.Add("@Materno", SqlDbType.NVarChar, 100).Value = (object?)usuario.ApellidoMaterno ?? DBNull.Value;
        command.Parameters.Add("@Nomina", SqlDbType.NVarChar, 20).Value = usuario.NumeroNomina;
        command.Parameters.Add("@FechaNac", SqlDbType.Date).Value = usuario.FechaNacimiento;
        command.Parameters.Add("@Tipo", SqlDbType.NVarChar, 20).Value = usuario.TipoUsuario;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_EliminarUsuario", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static int BuscarColumna(SqlDataReader reader, string nombre)
    {
        for (int i = 0; i < reader.FieldCount; i++)
            if (string.Equals(reader.GetName(i), nombre, StringComparison.OrdinalIgnoreCase)) return i;
        return -1;
    }

    private static Usuario MapUsuario(SqlDataReader reader)
    {
        string tipo = reader.GetString(reader.GetOrdinal("tipo_usuario"));
        
        Usuario usuario = tipo == "Administrador" ? new Administrador() : new Operativo();

        usuario.IdUsuario = reader.GetInt32(reader.GetOrdinal("id_usuario"));
        usuario.CorreoElectronico = reader.GetString(reader.GetOrdinal("correo_electronico"));
        usuario.Nombre = reader.GetString(reader.GetOrdinal("nombre"));
        usuario.ApellidoPaterno = reader.GetString(reader.GetOrdinal("apellido_paterno"));
        usuario.ApellidoMaterno = reader.IsDBNull(reader.GetOrdinal("apellido_materno")) ? null : reader.GetString(reader.GetOrdinal("apellido_materno"));
        usuario.NumeroNomina = reader.GetString(reader.GetOrdinal("numero_nomina"));
        usuario.FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento"));

        int posRegistro = BuscarColumna(reader, "fecha_registro");
        if (posRegistro >= 0 && !reader.IsDBNull(posRegistro))
            usuario.FechaRegistro = reader.GetDateTime(posRegistro);
        
        return usuario;
    }
}
