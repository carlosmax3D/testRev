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
    public class AsientoRepository : IAsientoRepository
    {
        private readonly DatabaseConnection _database;

        public AsientoRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<Asiento> GetAll()
        {
            List<Asiento> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Asiento_Consultar", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapAsiento(reader));
            }
            return lista;
        }

        public Asiento? GetById(int id)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Asiento_ConsultarPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_asiento", SqlDbType.Int).Value = id;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return MapAsiento(reader);
        }

        public List<Asiento> GetByZona(int idZona)
        {
            List<Asiento> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Asiento_ConsultarPorZona", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_zona", SqlDbType.Int).Value = idZona;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapAsiento(reader));
            }
            return lista;
        }

        public void Add(Asiento asiento)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Asiento_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_zona", SqlDbType.Int).Value = asiento.IdZona;
            command.Parameters.Add("@fila", SqlDbType.VarChar, 10).Value = asiento.Fila;
            command.Parameters.Add("@numero_asiento", SqlDbType.Int).Value = asiento.NumeroAsiento;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Asiento asiento)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Asiento_Actualizar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_asiento", SqlDbType.Int).Value = asiento.IdAsiento;
            command.Parameters.Add("@id_zona", SqlDbType.Int).Value = asiento.IdZona;
            command.Parameters.Add("@fila", SqlDbType.VarChar, 10).Value = asiento.Fila;
            command.Parameters.Add("@numero_asiento", SqlDbType.Int).Value = asiento.NumeroAsiento;
            connection.Open();
            command.ExecuteNonQuery();
        }

        private static Asiento MapAsiento(SqlDataReader reader)
        {
            return new Asiento
            {
                IdAsiento = reader.GetInt32(reader.GetOrdinal("id_asiento")),
                IdZona = reader.GetInt32(reader.GetOrdinal("id_zona")),
                Fila = reader.GetString(reader.GetOrdinal("fila")),
                NumeroAsiento = reader.GetInt32(reader.GetOrdinal("numero_asiento")),
                NombreZona = reader.GetString(reader.GetOrdinal("nombre_zona")),
                NombreEstadio = reader.GetString(reader.GetOrdinal("nombre_estadio"))
            };
        }

        public int ExisteAsiento(int idZona, string fila, int numeroAsiento, int idExcluir = 0)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Asiento_Existe", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_zona", SqlDbType.Int).Value = idZona;
            command.Parameters.Add("@fila", SqlDbType.VarChar, 10).Value = fila;
            command.Parameters.Add("@numero_asiento", SqlDbType.Int).Value = numeroAsiento;
            command.Parameters.Add("@id_excluir", SqlDbType.Int).Value = idExcluir;
            connection.Open();
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
