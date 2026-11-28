using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories
{
    public class PartidoRepository
    {
        private readonly DatabaseConnection _db;

        public PartidoRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public List<Partido> ObtenerTodos()
        {
            var lista = new List<Partido>();
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarPartidos", conn) { CommandType = CommandType.StoredProcedure };

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Partido
                {
                    IdPartido = Convert.ToInt32(reader["id_partido"]),
                    IdEstadio = Convert.ToInt32(reader["id_estadio"]),
                    NombreEstadio = reader["nombre_estadio"].ToString() ?? string.Empty,
                    EquipoLocal = reader["equipo_local"].ToString() ?? string.Empty,
                    EquipoVisitante = reader["equipo_visitante"].ToString() ?? string.Empty,
                    FechaPartido = Convert.ToDateTime(reader["fecha_partido"]),
                    HoraPartido = (TimeSpan)reader["hora_partido"]
                });
            }
            return lista;
        }

        public void Insertar(Partido partido)
        {
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarPartido", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@id_estadio", SqlDbType.Int).Value = partido.IdEstadio;
            cmd.Parameters.Add("@equipo_local", SqlDbType.VarChar, 50).Value = partido.EquipoLocal;
            cmd.Parameters.Add("@equipo_visitante", SqlDbType.VarChar, 50).Value = partido.EquipoVisitante;
            cmd.Parameters.Add("@fecha_partido", SqlDbType.Date).Value = partido.FechaPartido;
            cmd.Parameters.Add("@hora_partido", SqlDbType.Time).Value = partido.HoraPartido;

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Modificar(Partido partido)
        {
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ModificarPartido", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@id_partido", SqlDbType.Int).Value = partido.IdPartido;
            cmd.Parameters.Add("@id_estadio", SqlDbType.Int).Value = partido.IdEstadio;
            cmd.Parameters.Add("@equipo_local", SqlDbType.VarChar, 50).Value = partido.EquipoLocal;
            cmd.Parameters.Add("@equipo_visitante", SqlDbType.VarChar, 50).Value = partido.EquipoVisitante;
            cmd.Parameters.Add("@fecha_partido", SqlDbType.Date).Value = partido.FechaPartido;
            cmd.Parameters.Add("@hora_partido", SqlDbType.Time).Value = partido.HoraPartido;

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int idPartido)
        {
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarPartido", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@id_partido", SqlDbType.Int).Value = idPartido;

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}