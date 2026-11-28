using System;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;

namespace TemplateCSharp.Data.Repositories
{
    public class ReporteRepository
    {
        private readonly DatabaseConnection _db;

        public ReporteRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public DataTable ObtenerReporteVentas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            DataTable dt = new DataTable();
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ReporteVentas", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@fecha_inicio", SqlDbType.Date).Value = (object?)fechaInicio ?? DBNull.Value;
            cmd.Parameters.Add("@fecha_fin", SqlDbType.Date).Value = (object?)fechaFin ?? DBNull.Value;

            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable ObtenerResumenPartidos()
        {
            DataTable dt = new DataTable();
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ReporteResumenPartidos", conn) { CommandType = CommandType.StoredProcedure };

            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
    }
}