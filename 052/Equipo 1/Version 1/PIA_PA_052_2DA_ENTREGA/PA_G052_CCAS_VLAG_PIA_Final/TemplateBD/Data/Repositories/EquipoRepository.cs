using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Models;


namespace TemplateCSharp.Data.Repositories
{
    public class EquipoRepository : IEquipoRepository
    {
        private readonly DatabaseConnection _database;

        public EquipoRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<Equipo> GetAll()
        {
            List<Equipo> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Equipo_Consultar", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapEquipo(reader));
            }
            return lista;
        }

        public Equipo? GetById(int id)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Equipo_ConsultarPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_equipo", SqlDbType.Int).Value = id;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return MapEquipo(reader);
        }

        public void Add(Equipo equipo)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Equipo_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@nombre_equipo", SqlDbType.VarChar, 100).Value = equipo.NombreEquipo;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Equipo equipo)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Equipo_Actualizar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_equipo", SqlDbType.Int).Value = equipo.IdEquipo;
            command.Parameters.Add("@nombre_equipo", SqlDbType.VarChar, 100).Value = equipo.NombreEquipo;
            connection.Open();
            command.ExecuteNonQuery();
        }

        private static Equipo MapEquipo(SqlDataReader reader)
        {
            return new Equipo
            {
                IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo")),
                NombreEquipo = reader.GetString(reader.GetOrdinal("nombre_equipo"))
            };
        }

        public int ExisteEquipo(string nombre, int idExcluir = 0)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Equipo_Existe", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@nombre_equipo", SqlDbType.VarChar, 100).Value = nombre;
            command.Parameters.Add("@id_excluir", SqlDbType.Int).Value = idExcluir;
            connection.Open();
            return Convert.ToInt32(command.ExecuteScalar());
        }

    }
}
