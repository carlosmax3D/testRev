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
    public class ZonaRepository : IZonaRepository
    {
        private readonly DatabaseConnection _database;

        public ZonaRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<Zona> GetAll()
        {
            List<Zona> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Zona_Consultar", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapZona(reader));
            }
            return lista;
        }

        public Zona? GetById(int id)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Zona_ConsultarPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_zona", SqlDbType.Int).Value = id;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return MapZona(reader);
        }

        public List<Zona> GetByEstadio(int idEstadio)
        {
            List<Zona> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Zona_ConsultarPorEstadio", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = idEstadio;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapZona(reader));
            }
            return lista;
        }

        public void Add(Zona zona)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Zona_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = zona.IdEstadio;
            command.Parameters.Add("@nombre_zona", SqlDbType.VarChar, 100).Value = zona.NombreZona;
            command.Parameters.Add("@precio_base", SqlDbType.Decimal).Value = zona.PrecioBase;
            command.Parameters.Add("@cupo_de_la_zona", SqlDbType.Int).Value = zona.CupoDeLaZona;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Zona zona)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Zona_Actualizar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_zona", SqlDbType.Int).Value = zona.IdZona;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = zona.IdEstadio;
            command.Parameters.Add("@nombre_zona", SqlDbType.VarChar, 100).Value = zona.NombreZona;
            command.Parameters.Add("@precio_base", SqlDbType.Decimal).Value = zona.PrecioBase;
            command.Parameters.Add("@cupo_de_la_zona", SqlDbType.Int).Value = zona.CupoDeLaZona;

            connection.Open();
            command.ExecuteNonQuery();
        }

        private static Zona MapZona(SqlDataReader reader)
        {
            return new Zona
            {
                IdZona = reader.GetInt32(reader.GetOrdinal("id_zona")),
                IdEstadio = reader.GetInt32(reader.GetOrdinal("id_estadio")),
                NombreZona = reader.GetString(reader.GetOrdinal("nombre_zona")),
                PrecioBase = reader.GetDecimal(reader.GetOrdinal("precio_base")),
                CupoDeLaZona = reader.GetInt32(reader.GetOrdinal("cupo_de_la_zona")),
                //El nombre del estadio viene del JOIN en el SP
                NombreEstadio = reader.GetString(reader.GetOrdinal("nombre_estadio"))
            };
        }
        public int ExisteZona(int idEstadio, string nombreZona, int idExcluir = 0)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Zona_Existe", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = idEstadio;
            command.Parameters.Add("@nombre_zona", SqlDbType.VarChar, 100).Value = nombreZona;
            command.Parameters.Add("@id_excluir", SqlDbType.Int).Value = idExcluir;
            connection.Open();
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
