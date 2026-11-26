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
    public class DetalleBoletoRepository : IDetalleBoletoRepository
    {
        private readonly DatabaseConnection _database;

        public DetalleBoletoRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<DetalleBoleto> GetByVenta(Guid idVenta)
        {
            List<DetalleBoleto> detalles = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Detalle_ConsultarPorVenta", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_venta", SqlDbType.UniqueIdentifier).Value = idVenta;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                detalles.Add(MapDetalle(reader));
            }
            return detalles;
        }

        public void Add(DetalleBoleto detalle)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Detalle_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_venta", SqlDbType.UniqueIdentifier).Value = detalle.IdVenta;
            command.Parameters.Add("@id_partido", SqlDbType.Int).Value = detalle.IdPartido;
            command.Parameters.Add("@id_asiento", SqlDbType.Int).Value = detalle.IdAsiento;
            command.Parameters.Add("@precio_unitario", SqlDbType.Decimal).Value = detalle.PrecioUnitario;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public bool IsAsientoOcupado(int idAsiento, int idPartido)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Detalle_ValidarAsientoOcupado", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_asiento", SqlDbType.Int).Value = idAsiento;
            command.Parameters.Add("@id_partido", SqlDbType.Int).Value = idPartido;
            connection.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }


        public void Delete(int idDetalle)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Detalle_Eliminar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_detalle", SqlDbType.Int).Value = idDetalle;
            connection.Open();
            command.ExecuteNonQuery();
        }
        private static DetalleBoleto MapDetalle(SqlDataReader reader)
        {
            return new DetalleBoleto
            {
                IdDetalle = reader.GetInt32(reader.GetOrdinal("id_detalle")),
                IdVenta = reader.GetGuid(reader.GetOrdinal("id_venta")),
                IdPartido = reader.GetInt32(reader.GetOrdinal("id_partido")),
                IdAsiento = reader.GetInt32(reader.GetOrdinal("id_asiento")),
                PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("precio_unitario")),
                NombrePartido = reader.GetString(reader.GetOrdinal("nombre_partido")),
                NombreZona = reader.GetString(reader.GetOrdinal("nombre_zona")),
                Fila = reader.GetString(reader.GetOrdinal("fila")),
                NumeroAsiento = reader.GetInt32(reader.GetOrdinal("numero_asiento")),
                NombreEstadio = reader.GetString(reader.GetOrdinal("nombre_estadio"))
            };
        }
    }
}
