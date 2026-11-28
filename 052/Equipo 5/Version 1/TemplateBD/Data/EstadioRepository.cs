using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories
{
    public class EstadioRepository
    {
        private readonly DatabaseConnection _db;

        public EstadioRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public List<Estadio> ObtenerTodos()
        {
            var lista = new List<Estadio>();
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarEstadios", conn) { CommandType = CommandType.StoredProcedure };

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Estadio
                {
                    IdEstadio = Convert.ToInt32(reader["id_estadio"]),
                    Nombre = reader["nombre"].ToString() ?? string.Empty,
                    Ciudad = reader["ciudad"].ToString() ?? string.Empty,
                    Mapa = reader["mapa"] == DBNull.Value ? null : reader["mapa"].ToString(),
                    CapacidadTotal = Convert.ToInt32(reader["capacidad_t"]),
                    IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                    FechaHoraRegistro = Convert.ToDateTime(reader["fecha_hora_registro"])
                });
            }
            return lista;
        }

        public void Insertar(Estadio estadio)
        {
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarEstadio", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = estadio.Nombre;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = estadio.Ciudad;
            cmd.Parameters.Add("@mapa", SqlDbType.VarChar, 255).Value = (object?)estadio.Mapa ?? DBNull.Value;
            cmd.Parameters.Add("@capacidad_t", SqlDbType.Int).Value = estadio.CapacidadTotal;
            cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = estadio.IdUsuario;

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Modificar(Estadio estadio)
        {
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ModificarEstadio", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@id_estadio", SqlDbType.Int).Value = estadio.IdEstadio;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = estadio.Nombre;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = estadio.Ciudad;
            cmd.Parameters.Add("@mapa", SqlDbType.VarChar, 255).Value = (object?)estadio.Mapa ?? DBNull.Value;
            cmd.Parameters.Add("@capacidad_t", SqlDbType.Int).Value = estadio.CapacidadTotal;

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int idEstadio)
        {
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarEstadio", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@id_estadio", SqlDbType.Int).Value = idEstadio;

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}