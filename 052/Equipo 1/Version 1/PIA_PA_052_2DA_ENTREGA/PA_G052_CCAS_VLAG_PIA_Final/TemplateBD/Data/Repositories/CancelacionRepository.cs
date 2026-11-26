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
    public class CancelacionRepository : ICancelacionRepository
    {
        private readonly DatabaseConnection _database;

        public CancelacionRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public Cancelacion? GetByVenta(Guid idVenta)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Cancelacion_ConsultarPorVenta", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_venta", SqlDbType.UniqueIdentifier).Value = idVenta;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return new Cancelacion
            {
                IdCancelacion = reader.GetInt32(reader.GetOrdinal("id_cancelacion")),
                IdVenta = reader.GetGuid(reader.GetOrdinal("id_venta")),
                IdUsuarioAdmin = reader.GetInt32(reader.GetOrdinal("id_usuario_admin")),
                Motivo = reader.GetString(reader.GetOrdinal("motivo")),
                FechaCancelacion = reader.GetDateTime(reader.GetOrdinal("fecha_cancelacion")),
                HoraCancelacion = reader.GetTimeSpan(reader.GetOrdinal("hora_cancelacion")),
                NombreAdmin = reader.GetString(reader.GetOrdinal("nombre_admin"))
            };
        }

        public void Add(Cancelacion cancelacion)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Cancelacion_Insertar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_venta", SqlDbType.UniqueIdentifier).Value = cancelacion.IdVenta;
            command.Parameters.Add("@id_usuario_admin", SqlDbType.Int).Value = cancelacion.IdUsuarioAdmin;
            command.Parameters.Add("@motivo", SqlDbType.VarChar, 255).Value = cancelacion.Motivo;
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
