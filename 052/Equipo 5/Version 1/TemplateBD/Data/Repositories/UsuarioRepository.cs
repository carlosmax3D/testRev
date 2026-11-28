using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
using TemplateCSharp.Helpers;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DatabaseConnection _db;

        public UsuarioRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public Usuario? ValidarLogin(string email, string contrasena)
        {
            // === ACCESO DIRECTO PARA PRUEBAS ===
            string correoIngresado = email.Trim().ToLower();
            if ((correoIngresado == "ingrid@sistema.com" || correoIngresado == "admin@sistema.com")
                && contrasena == "Ingrid28*")
            {
                return new Usuario
                {
                    IdUsuario = 1,
                    NombreCompleto = "Administrador del Sistema",
                    Email = email,
                    NumNomina = "NOM-0001",
                    Rol = "Admin",
                    FechaNacimiento = new DateTime(2000, 1, 1),
                    IdUsuarioRegistro = 1,
                    FechaHoraRegistro = DateTime.Now
                };
            }
            // ======================================

            try
            {
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_ObtenerCredencialesUsuario", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = email;

                conn.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string hashAlmacenado = reader["contrasena"].ToString() ?? string.Empty;
                    string salAlmacenada = reader["sal"] == DBNull.Value ? string.Empty : reader["sal"].ToString() ?? string.Empty;

                    bool esValida = false;
                    if (!string.IsNullOrEmpty(salAlmacenada))
                    {
                        // Se valida usando la función oficial de tu helper
                        string hashIngresado = SeguridadHelper.GenerarHashSHA256(contrasena, salAlmacenada);
                        esValida = (hashIngresado == hashAlmacenado);
                    }
                    else
                    {
                        esValida = (contrasena == hashAlmacenado);
                    }

                    if (!esValida) return null;

                    return new Usuario
                    {
                        IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                        NombreCompleto = reader["nombre_com"].ToString() ?? string.Empty,
                        Email = reader["email"].ToString() ?? string.Empty,
                        NumNomina = reader["num_nomina"].ToString() ?? string.Empty,
                        Rol = reader["rol"].ToString() ?? string.Empty,
                        FechaNacimiento = reader["fechaNac"] != DBNull.Value ? Convert.ToDateTime(reader["fechaNac"]) : DateTime.MinValue,
                        IdUsuarioRegistro = reader["id_usuario_registro"] == DBNull.Value ? null : Convert.ToInt32(reader["id_usuario_registro"]),
                        FechaHoraRegistro = reader["fecha_hora_registro"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_hora_registro"]) : DateTime.MinValue
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error: " + ex.Message);
            }
        }

        public void Insertar(Usuario usuario)
        {
            try
            {
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_InsertarUsuario", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@nombre_com", SqlDbType.VarChar, 150).Value = usuario.NombreCompleto;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = usuario.Email;
                cmd.Parameters.Add("@num_nomina", SqlDbType.VarChar, 50).Value = usuario.NumNomina;
                cmd.Parameters.Add("@contrasena", SqlDbType.VarChar, 256).Value = usuario.Contrasena;
                cmd.Parameters.Add("@sal", SqlDbType.VarChar, 100).Value = (object?)usuario.Sal ?? DBNull.Value;
                cmd.Parameters.Add("@rol", SqlDbType.VarChar, 20).Value = usuario.Rol;
                cmd.Parameters.Add("@fechaNac", SqlDbType.Date).Value = usuario.FechaNacimiento;
                cmd.Parameters.Add("@id_usuario_registro", SqlDbType.Int).Value = (object?)usuario.IdUsuarioRegistro ?? DBNull.Value;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error de base de datos al guardar usuario: " + ex.Message);
            }
        }

        public void Modificar(Usuario usuario)
        {
            try
            {
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_ModificarUsuario", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = usuario.IdUsuario;
                cmd.Parameters.Add("@nombre_com", SqlDbType.VarChar, 150).Value = usuario.NombreCompleto;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = usuario.Email;
                cmd.Parameters.Add("@num_nomina", SqlDbType.VarChar, 50).Value = usuario.NumNomina;
                cmd.Parameters.Add("@contrasena", SqlDbType.VarChar, 256).Value = string.IsNullOrEmpty(usuario.Contrasena) ? DBNull.Value : usuario.Contrasena;
                cmd.Parameters.Add("@sal", SqlDbType.VarChar, 100).Value = string.IsNullOrEmpty(usuario.Sal) ? DBNull.Value : usuario.Sal;
                cmd.Parameters.Add("@rol", SqlDbType.VarChar, 20).Value = usuario.Rol;
                cmd.Parameters.Add("@fechaNac", SqlDbType.Date).Value = usuario.FechaNacimiento;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error de base de datos al actualizar usuario: " + ex.Message);
            }
        }

        public void Eliminar(int idUsuario)
        {
            try
            {
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_EliminarUsuario", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al eliminar el usuario: " + ex.Message);
            }
        }

        public List<Usuario> ObtenerTodos()
        {
            try
            {
                var lista = new List<Usuario>();
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_ListarUsuarios", conn) { CommandType = CommandType.StoredProcedure };

                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Usuario
                    {
                        IdUsuario = reader.GetInt32(0),
                        NombreCompleto = reader.GetString(1),
                        Email = reader.GetString(2),
                        NumNomina = reader.GetString(3),
                        Rol = reader.GetString(4),
                        FechaNacimiento = reader.GetDateTime(5),
                        IdUsuarioRegistro = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                        FechaHoraRegistro = reader.GetDateTime(7)
                    });
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al consultar la lista de usuarios: " + ex.Message);
            }
        }
    }
}