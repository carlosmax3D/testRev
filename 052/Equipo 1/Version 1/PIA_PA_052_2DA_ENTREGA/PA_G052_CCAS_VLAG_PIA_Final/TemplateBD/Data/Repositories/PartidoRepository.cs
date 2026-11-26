using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;


namespace TemplateCSharp.Data.Repositories
{
    public class PartidoRepository : IPartidoRepository
    {
        private readonly DatabaseConnection _database;

        public PartidoRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<Partido> GetAll()
        {
            List<Partido> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Partido_Consultar", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapPartido(reader));
            }
            return lista;
        }

        public Partido? GetById(int id)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Partido_ConsultarPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_partido", SqlDbType.Int).Value = id;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return MapPartido(reader);
        }

        public List<Partido> GetByFiltros(DateTime? fecha, int? idEstadio, string? nombreEquipo)
        {
            List<Partido> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Partido_ConsultarPorFiltros", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@fecha", SqlDbType.Date).Value = (object?)fecha ?? DBNull.Value;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = (object?)idEstadio ?? DBNull.Value;
            command.Parameters.Add("@nombre_equipo", SqlDbType.VarChar, 100).Value = (object?)nombreEquipo ?? DBNull.Value;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapPartido(reader));
            }
            return lista;
        }

        public void Add(Partido partido)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Partido_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = partido.IdEstadio;
            command.Parameters.Add("@fecha_partido", SqlDbType.Date).Value = partido.FechaPartido;
            command.Parameters.Add("@hora_partido", SqlDbType.Time).Value = partido.HoraPartido;
            command.Parameters.Add("@id_equipo_local", SqlDbType.Int).Value = partido.IdEquipoLocal;
            command.Parameters.Add("@id_equipo_visitante", SqlDbType.Int).Value = partido.IdEquipoVisitante;
            command.Parameters.Add("@estado", SqlDbType.VarChar, 20).Value = partido.Estado;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Partido partido)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Partido_Actualizar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_partido", SqlDbType.Int).Value = partido.IdPartido;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = partido.IdEstadio;
            command.Parameters.Add("@fecha_partido", SqlDbType.Date).Value = partido.FechaPartido;
            command.Parameters.Add("@hora_partido", SqlDbType.Time).Value = partido.HoraPartido;
            command.Parameters.Add("@id_equipo_local", SqlDbType.Int).Value = partido.IdEquipoLocal;
            command.Parameters.Add("@id_equipo_visitante", SqlDbType.Int).Value = partido.IdEquipoVisitante;
            command.Parameters.Add("@estado", SqlDbType.VarChar, 20).Value = partido.Estado;
            connection.Open();
            command.ExecuteNonQuery();
        }

        private static Partido MapPartido(SqlDataReader reader)
        {
            return new Partido
            {
                IdPartido = reader.GetInt32(reader.GetOrdinal("id_partido")),
                IdEstadio = reader.GetInt32(reader.GetOrdinal("id_estadio")),
                FechaPartido = reader.GetDateTime(reader.GetOrdinal("fecha_partido")),
                HoraPartido = reader.GetTimeSpan(reader.GetOrdinal("hora_partido")),
                IdEquipoLocal = reader.GetInt32(reader.GetOrdinal("id_equipo_local")),
                IdEquipoVisitante = reader.GetInt32(reader.GetOrdinal("id_equipo_visitante")),
                Estado = reader.GetString(reader.GetOrdinal("estado")),
                NombreEstadio = reader.GetString(reader.GetOrdinal("nombre_estadio")),
                NombreEquipoLocal = reader.GetString(reader.GetOrdinal("nombre_equipo_local")),
                NombreEquipoVisitante = reader.GetString(reader.GetOrdinal("nombre_equipo_visitante"))
            };
        }

        public void ActualizarPartidosTerminados()
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Partido_ActualizarTerminados", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.ExecuteNonQuery();
        }

    }
}
