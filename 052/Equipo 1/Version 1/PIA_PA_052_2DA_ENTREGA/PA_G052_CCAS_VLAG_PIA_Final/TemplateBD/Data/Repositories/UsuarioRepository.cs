using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DatabaseConnection _database;

        public UsuarioRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public Usuario? GetByCorreo(string correo)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Usuario_ConsultarPorCorreo", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@correo", SqlDbType.VarChar, 100).Value = correo;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read()) return null;

            return MapUsuario(reader);
        }


        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = [];
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Usuario_Consultar", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) usuarios.Add(MapUsuario(reader));
            return usuarios;
        }

        public List<Usuario> BuscarPorApellidos(string apellidos)
        {
            List<Usuario> usuarios = [];
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Usuario_BuscarPorApellidos", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@apellidos", SqlDbType.VarChar, 100).Value = apellidos;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) usuarios.Add(MapUsuario(reader));
            return usuarios;
        }

        public void Add(Usuario u)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Usuario_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@correo", SqlDbType.VarChar, 100).Value = u.Correo;
            command.Parameters.Add("@contrasena", SqlDbType.VarChar, 255).Value = u.Contrasena;
            command.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = u.Nombre;
            command.Parameters.Add("@apellido_p", SqlDbType.VarChar, 40).Value = u.ApellidoP;
            command.Parameters.Add("@apellido_m", SqlDbType.VarChar, 40).Value = u.ApellidoM;
            command.Parameters.Add("@numero_nomina", SqlDbType.VarChar, 50).Value = u.NumeroNomina;
            command.Parameters.Add("@fecha_nacimiento", SqlDbType.Date).Value = u.FechaNacimiento;
            command.Parameters.Add("@tipo_usuario", SqlDbType.VarChar, 20).Value = u.TipoUsuario;
            command.Parameters.Add("@usuario_registro", SqlDbType.Int).Value = u.UsuarioRegistro;

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Usuario u)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Usuario_Actualizar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_usuario", SqlDbType.Int).Value = u.IdUsuario;
            command.Parameters.Add("@correo", SqlDbType.VarChar, 100).Value = u.Correo;
            command.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = u.Nombre;
            command.Parameters.Add("@apellido_p", SqlDbType.VarChar, 40).Value = u.ApellidoP;
            command.Parameters.Add("@apellido_m", SqlDbType.VarChar, 40).Value = u.ApellidoM;
            command.Parameters.Add("@numero_nomina", SqlDbType.VarChar, 50).Value = u.NumeroNomina;
            command.Parameters.Add("@fecha_nacimiento", SqlDbType.Date).Value = u.FechaNacimiento;
            command.Parameters.Add("@tipo_usuario", SqlDbType.VarChar, 20).Value = u.TipoUsuario;
            command.Parameters.Add("@contrasena", SqlDbType.VarChar, 255).Value = string.IsNullOrWhiteSpace(u.Contrasena) ? "" : u.Contrasena;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Usuario_Eliminar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public int ExisteCorreo(string correo, int idExcluir = 0)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Usuario_ExisteCorreo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@correo", SqlDbType.VarChar, 100).Value = correo;
            command.Parameters.Add("@id_excluir", SqlDbType.Int).Value = idExcluir;
            connection.Open();
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public int ExisteNomina(string nomina, int idExcluir = 0)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Usuario_ExisteNomina", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@numero_nomina", SqlDbType.VarChar, 50).Value = nomina;
            command.Parameters.Add("@id_excluir", SqlDbType.Int).Value = idExcluir;
            connection.Open();
            return Convert.ToInt32(command.ExecuteScalar());
        }

        private static Usuario MapUsuario(SqlDataReader r) => new()
        {
            IdUsuario = r.GetInt32(r.GetOrdinal("id_usuario")),
            Correo = r.GetString(r.GetOrdinal("correo")),
            Nombre = r.GetString(r.GetOrdinal("nombre")),
            ApellidoP = r.GetString(r.GetOrdinal("apellido_p")),
            ApellidoM = r.GetString(r.GetOrdinal("apellido_m")),
            NumeroNomina = r.GetString(r.GetOrdinal("numero_nomina")),
            FechaNacimiento = r.GetDateTime(r.GetOrdinal("fecha_nacimiento")),
            TipoUsuario = r.GetString(r.GetOrdinal("tipo_usuario")),
            UsuarioRegistro = r.GetInt32(r.GetOrdinal("usuario_registro")),
            Contrasena = r.GetString(r.GetOrdinal("contrasena"))
        };

        //OBTENER ULTIMAS 2 CONTRASEÑAS DEL HISTORIAL
        //devuelve las últimas 2 contraseñas hasheadas del usuario (de la tabla Historial_Contrasenas).
        public List<string> ObtenerContrasenasRecientes(int idUsuario)
        {
            List<string> contrasenas = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Usuario_ObtenerContrasenasRecientes", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                contrasenas.Add(reader.GetString(reader.GetOrdinal("contrasena_hash")));
            }
            return contrasenas;
        }
    }
}
