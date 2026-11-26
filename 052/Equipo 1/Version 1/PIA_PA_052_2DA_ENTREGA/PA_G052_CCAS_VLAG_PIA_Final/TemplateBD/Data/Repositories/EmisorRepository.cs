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
    public class EmisorRepository : IEmisorRepository
    {
        private readonly DatabaseConnection _database;

        public EmisorRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<Emisor> GetAll()
        {
            List<Emisor> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Emisor_Consultar", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Emisor
                {
                    IdEmisor = reader.GetInt32(reader.GetOrdinal("id_emisor")),
                    RazonSocial = reader.GetString(reader.GetOrdinal("razon_social")),
                    EmisorRfc = reader.GetString(reader.GetOrdinal("emisor_rfc")),
                    Calle = reader.GetString(reader.GetOrdinal("calle")),
                    Numero = reader.GetString(reader.GetOrdinal("numero")),
                    Colonia = reader.GetString(reader.GetOrdinal("colonia")),
                    CP = reader.GetString(reader.GetOrdinal("CP")),
                    Ciudad = reader.GetString(reader.GetOrdinal("ciudad"))
                });
            }
            return lista;
        }
    }
}