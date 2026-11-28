using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DatabaseConnection _db;

        public ClienteRepository(DatabaseConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public List<Cliente> ObtenerTodos()
        {
            //try
            {
                var lista = new List<Cliente>();
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_ListarClientes", conn) { CommandType = CommandType.StoredProcedure };

                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(MapClienteFromReader(reader));
                }
                return lista;
            }
            /*
            catch (Exception ex)
            {
                throw new ApplicationException("Error al consultar clientes: " + ex.Message, ex);
            }
            */
        }

        public List<Cliente> BuscarPorApellido(string apellido)
        {
            try
            {
                var lista = new List<Cliente>();
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_BuscarClientesPorApellido", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 100).Value = apellido ?? string.Empty;

                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(MapClienteFromReader(reader));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar cliente por apellido: " + ex.Message, ex);
            }
        }

        public void Insertar(Cliente cliente)
        {
            try
            {
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_InsertarCliente", conn) { CommandType = CommandType.StoredProcedure };

                string nombreCompleto = $"{cliente.Nombre} {cliente.Apellidos}".Trim();

                cmd.Parameters.Add("@nombre_com", SqlDbType.VarChar, 150).Value = nombreCompleto;
                cmd.Parameters.Add("@RFC", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(cliente.Rfc) ? DBNull.Value : cliente.Rfc;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = cliente.Email ?? string.Empty;
                cmd.Parameters.Add("@fecha_nac", SqlDbType.Date).Value = new DateTime(2000, 1, 1); // Fecha base de respaldo
                cmd.Parameters.Add("@tel_casa", SqlDbType.VarChar, 30).Value = DBNull.Value;
                cmd.Parameters.Add("@tel_celular", SqlDbType.VarChar, 15).Value = cliente.Telefono ?? string.Empty;
                cmd.Parameters.Add("@domicilio", SqlDbType.VarChar, 200).Value = "Domicilio No Especificado";
                cmd.Parameters.Add("@id_usuario_registro", SqlDbType.Int).Value = cliente.IdUsuarioRegistro ?? 1;
                cmd.Parameters.Add("@CURP", SqlDbType.VarChar, 18).Value = cliente.CURP ?? string.Empty;


                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al guardar cliente: " + ex.Message, ex);
            }
        }

        public void Modificar(Cliente cliente)
        {
            try
            {
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_ModificarCliente", conn) { CommandType = CommandType.StoredProcedure };

                string nombreCompleto = $"{cliente.Nombre} {cliente.Apellidos}".Trim();

                cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = cliente.IdCliente;
                cmd.Parameters.Add("@nombre_com", SqlDbType.VarChar, 150).Value = nombreCompleto;
                cmd.Parameters.Add("@RFC", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(cliente.Rfc) ? DBNull.Value : cliente.Rfc;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = cliente.Email ?? string.Empty;
                cmd.Parameters.Add("@tel_celular", SqlDbType.VarChar, 15).Value = cliente.Telefono ?? string.Empty;
                cmd.Parameters.Add("@CURP", SqlDbType.VarChar, 18).Value = cliente.CURP ?? string.Empty;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al modificar cliente: " + ex.Message, ex);
            }
        }

        public void Eliminar(int idCliente)
        {
            try
            {
                using var conn = _db.CreateConnection();
                using var cmd = new SqlCommand("sp_EliminarCliente", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = idCliente;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar cliente: " + ex.Message, ex);
            }
        }

        private Cliente MapClienteFromReader(SqlDataReader reader)
        {
            string nombreCompleto = reader["nombre_com"] != DBNull.Value ? reader["nombre_com"].ToString() ?? string.Empty : string.Empty;
            string[] partes = nombreCompleto.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            return new Cliente
            {
                IdCliente = Convert.ToInt32(reader["id_cliente"]),
                Nombre = partes.Length > 0 ? partes[0] : nombreCompleto,
                Apellidos = partes.Length > 1 ? partes[1] : string.Empty,
                Email = reader["email"] != DBNull.Value ? reader["email"].ToString() ?? string.Empty : string.Empty,
                Telefono = reader["tel_celular"] != DBNull.Value ? reader["tel_celular"].ToString() ?? string.Empty : string.Empty,
                Rfc = reader["RFC"] == DBNull.Value ? string.Empty : reader["RFC"].ToString(),
                IdUsuarioRegistro = reader["id_usuario_registro"] == DBNull.Value ? null : Convert.ToInt32(reader["id_usuario_registro"]),
                FechaHoraRegistro = reader["fecha_hora_registro"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_hora_registro"]) : DateTime.Now,
                CURP = reader["CURP"] != DBNull.Value ? reader["CURP"].ToString() ?? string.Empty : string.Empty
            };
        }
    }
}