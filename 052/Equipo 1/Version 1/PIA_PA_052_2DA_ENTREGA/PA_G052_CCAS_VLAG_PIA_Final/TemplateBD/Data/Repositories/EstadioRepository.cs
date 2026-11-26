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
    public class EstadioRepository : IEstadioRepository
    {
        private readonly DatabaseConnection _database;

        public EstadioRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<Estadio> GetAll()
        {
            List<Estadio> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Estadio_Consultar", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapEstadio(reader));
            }
            return lista;
        }

        public Estadio? GetById(int id)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Estadio_ConsultarPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = id;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return MapEstadio(reader);
        }

        public void Add(Estadio estadio)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Estadio_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = estadio.Nombre;
            command.Parameters.Add("@ciudad", SqlDbType.VarChar, 100).Value = estadio.Ciudad;
            command.Parameters.Add("@capacidad_total", SqlDbType.Int).Value = estadio.CapacidadTotal;
            command.Parameters.Add("@mapa_estadio", SqlDbType.VarChar, 255).Value = estadio.MapaEstadio;
            command.Parameters.Add("@usuario_registro", SqlDbType.Int).Value = estadio.UsuarioRegistro;

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Estadio estadio)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Estadio_Actualizar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = estadio.IdEstadio;
            command.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = estadio.Nombre;
            command.Parameters.Add("@ciudad", SqlDbType.VarChar, 100).Value = estadio.Ciudad;
            command.Parameters.Add("@capacidad_total", SqlDbType.Int).Value = estadio.CapacidadTotal;
            command.Parameters.Add("@mapa_estadio", SqlDbType.VarChar, 255).Value = estadio.MapaEstadio;

            connection.Open();
            command.ExecuteNonQuery();
        }

        private static Estadio MapEstadio(SqlDataReader reader)
        {
            return new Estadio
            {
                IdEstadio = reader.GetInt32(reader.GetOrdinal("id_estadio")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                Ciudad = reader.GetString(reader.GetOrdinal("ciudad")),
                CapacidadTotal = reader.GetInt32(reader.GetOrdinal("capacidad_total")),
                MapaEstadio = reader.GetString(reader.GetOrdinal("mapa_estadio")),
                UsuarioRegistro = reader.GetInt32(reader.GetOrdinal("usuario_registro")),
                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("fecha_registro")),
                HoraRegistro = reader.GetTimeSpan(reader.GetOrdinal("hora_registro"))
            };
        }

        public int ExisteEstadio(string nombre, int idExcluir = 0)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Estadio_Existe", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = nombre;
            command.Parameters.Add("@id_excluir", SqlDbType.Int).Value = idExcluir;
            connection.Open();
            return Convert.ToInt32(command.ExecuteScalar());
        }


    }
}
