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
    public class FacturaRepository : IFacturaRepository
    {
        private readonly DatabaseConnection _database;

        public FacturaRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public Factura? GetByVenta(Guid idVenta)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Factura_ConsultarPorVenta", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_venta", SqlDbType.UniqueIdentifier).Value = idVenta;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return new Factura
            {
                IdFactura = reader.GetInt32(reader.GetOrdinal("id_factura")),
                IdVenta = reader.GetGuid(reader.GetOrdinal("id_venta")),
                IdEmisor = reader.GetInt32(reader.GetOrdinal("id_emisor")),
                FolioFactura = reader.GetString(reader.GetOrdinal("folio_factura")),
                FechaEmision = reader.GetDateTime(reader.GetOrdinal("fecha_emision")),
                HoraEmision = reader.GetTimeSpan(reader.GetOrdinal("hora_emision")),
                EstadoFactura = reader.GetString(reader.GetOrdinal("estado_factura")),
                RazonSocialEmisor = reader.GetString(reader.GetOrdinal("razon_social"))
            };
        }

        public void Add(Factura factura)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Factura_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_venta", SqlDbType.UniqueIdentifier).Value = factura.IdVenta;
            command.Parameters.Add("@id_emisor", SqlDbType.Int).Value = factura.IdEmisor;
            command.Parameters.Add("@folio_factura", SqlDbType.VarChar, 50).Value = factura.FolioFactura;
            command.Parameters.Add("@estado_factura", SqlDbType.VarChar, 20).Value = factura.EstadoFactura;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void UpdateEstado(int idFactura, string nuevoEstado)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Factura_ActualizarEstado", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_factura", SqlDbType.Int).Value = idFactura;
            command.Parameters.Add("@estado_factura", SqlDbType.VarChar, 20).Value = nuevoEstado;
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
