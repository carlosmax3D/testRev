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
    public class VentaBoletoRepository : IVentaBoletoRepository
    {
        private readonly DatabaseConnection _database;

        public VentaBoletoRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<VentaBoleto> GetAll()
        {
            List<VentaBoleto> ventas = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Venta_Consultar", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ventas.Add(MapVenta(reader));
            }
            return ventas;
        }

        public VentaBoleto? GetById(Guid id)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Venta_ConsultarPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_venta", SqlDbType.UniqueIdentifier).Value = id;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return MapVenta(reader);
        }

        public Guid Add(VentaBoleto venta)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Venta_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            //de entrada
            command.Parameters.Add("@id_cliente", SqlDbType.Int).Value = venta.IdCliente;
            command.Parameters.Add("@id_usuario", SqlDbType.Int).Value = venta.IdUsuario;
            command.Parameters.Add("@metodo_pago", SqlDbType.VarChar, 20).Value = venta.MetodoPago;
            command.Parameters.Add("@subtotal", SqlDbType.Decimal).Value = venta.Subtotal;
            command.Parameters.Add("@iva", SqlDbType.Decimal).Value = venta.Iva;
            command.Parameters.Add("@total", SqlDbType.Decimal).Value = venta.Total;

            //Parametro de salida para obtener el GUID generado
            SqlParameter idVentaParam = new("@id_venta", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(idVentaParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (Guid)idVentaParam.Value;
        }

        private static VentaBoleto MapVenta(SqlDataReader reader)
        {
            return new VentaBoleto
            {
                IdVenta = reader.GetGuid(reader.GetOrdinal("id_venta")),
                IdCliente = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                IdUsuario = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                FechaVenta = reader.GetDateTime(reader.GetOrdinal("fecha_venta")),
                HoraVenta = reader.GetTimeSpan(reader.GetOrdinal("hora_venta")),
                MetodoPago = reader.GetString(reader.GetOrdinal("metodo_pago")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("subtotal")),
                Iva = reader.GetDecimal(reader.GetOrdinal("iva")),
                Total = reader.GetDecimal(reader.GetOrdinal("total")),
                NombreCliente = reader.GetString(reader.GetOrdinal("nombre_cliente"))
            };
        }
    }
}
