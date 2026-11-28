using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories
{
    public class FacturaRepository
    {
        private readonly DatabaseConnection _db;

        public FacturaRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public List<Factura> ObtenerTodas()
        {
            var lista = new List<Factura>();
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarFacturas", conn) { CommandType = CommandType.StoredProcedure };

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Factura
                {
                    FolioFactura = Convert.ToInt32(reader["folio_factura"]),
                    FolioVenta = reader["folio_venta"].ToString() ?? string.Empty,
                    FechaHoraFactura = Convert.ToDateTime(reader["fecha_hora_factura"]),
                    NomEmisor = reader["nom_emisor"].ToString() ?? string.Empty,
                    RfcEmisor = reader["RFC_emisor"].ToString() ?? string.Empty,
                    DomEmisor = reader["Dom_emisor"].ToString() ?? string.Empty,
                    NomReceptor = reader["nom_receptor"].ToString() ?? string.Empty,
                    RfcReceptor = reader["RFC_receptor"].ToString() ?? string.Empty,
                    DomReceptor = reader["Dom_receptor"].ToString() ?? string.Empty,
                    Estado = reader["estado"].ToString() ?? "activa"
                });
            }
            return lista;
        }

        public void RegistrarFactura(Factura factura)
        {
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_RegistrarFactura", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@folio_venta", SqlDbType.VarChar, 36).Value = factura.FolioVenta;
            cmd.Parameters.Add("@nom_emisor", SqlDbType.VarChar, 150).Value = factura.NomEmisor;
            cmd.Parameters.Add("@RFC_emisor", SqlDbType.VarChar, 20).Value = factura.RfcEmisor;
            cmd.Parameters.Add("@Dom_emisor", SqlDbType.VarChar, 200).Value = factura.DomEmisor;
            cmd.Parameters.Add("@nom_receptor", SqlDbType.VarChar, 150).Value = factura.NomReceptor;
            cmd.Parameters.Add("@RFC_receptor", SqlDbType.VarChar, 20).Value = factura.RfcReceptor;
            cmd.Parameters.Add("@Dom_receptor", SqlDbType.VarChar, 200).Value = factura.DomReceptor;

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}