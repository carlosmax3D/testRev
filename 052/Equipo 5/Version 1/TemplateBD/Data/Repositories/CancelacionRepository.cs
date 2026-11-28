using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories
{
    public class CancelacionRepository
    {
        private readonly DatabaseConnection _db;

        public CancelacionRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public List<Cancelacion> ObtenerTodas()
        {
            var lista = new List<Cancelacion>();
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarCancelaciones", conn) { CommandType = CommandType.StoredProcedure };

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Cancelacion
                {
                    FolioCancelacion = Convert.ToInt32(reader["folio_cancelacion"]),
                    FolioVenta = reader["folio_venta"].ToString() ?? string.Empty,
                    IdCliente = reader["id_cliente"] == DBNull.Value ? null : Convert.ToInt32(reader["id_cliente"]),
                    NombreCliente = reader["nombre_cliente"].ToString() ?? string.Empty,
                    IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                    NombreUsuario = reader["nombre_usuario"].ToString() ?? string.Empty,
                    Motivo = reader["motivo"].ToString() ?? string.Empty,
                    FechaHoraCancelacion = Convert.ToDateTime(reader["fecha_hora_cancelacion"])
                });
            }
            return lista;
        }

        public void RegistrarCancelacion(string folioVenta, int idUsuario, string motivo)
        {
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_RegistrarCancelacion", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@folio_venta", SqlDbType.VarChar, 36).Value = folioVenta;
            cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@motivo", SqlDbType.VarChar, 250).Value = motivo;

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}