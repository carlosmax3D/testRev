using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BoleteraPrototipo
{
    // Clase de conexion a SQL Server. Las credenciales salen del App.config
    // (connectionStrings > BoleteraDB) y TODAS las consultas se hacen llamando
    // procedimientos almacenados (CommandType.StoredProcedure), nunca SQL directo.
    public static class Conexion
    {
        static string Cadena()
        {
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["BoleteraDB"];
            if (cs == null || string.IsNullOrWhiteSpace(cs.ConnectionString))
                throw new InvalidOperationException("No se encontró la cadena de conexión 'BoleteraDB' en el App.config.");
            return cs.ConnectionString;
        }

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(Cadena());
        }

        // Parametro normal (si el valor es null manda NULL a la base)
        public static SqlParameter P(string nombre, object valor)
        {
            return new SqlParameter(nombre, valor ?? DBNull.Value);
        }

        // Parametro VARBINARY(MAX) (por ejemplo el mapa del estadio)
        public static SqlParameter PBinario(string nombre, byte[] valor)
        {
            SqlParameter p = new SqlParameter(nombre, SqlDbType.VarBinary, -1);
            p.Value = valor == null ? (object)DBNull.Value : valor;
            return p;
        }

        // Parametro de tipo tabla (lista de boletos de una venta)
        public static SqlParameter PTabla(string nombre, DataTable tabla, string tipo)
        {
            SqlParameter p = new SqlParameter(nombre, SqlDbType.Structured);
            p.TypeName = tipo;
            p.Value = tabla;
            return p;
        }

        // Parametro de salida
        public static SqlParameter PSalida(string nombre, SqlDbType tipo)
        {
            SqlParameter p = new SqlParameter(nombre, tipo);
            p.Direction = ParameterDirection.Output;
            return p;
        }

        static SqlCommand Comando(SqlConnection con, string sp, SqlParameter[] ps)
        {
            SqlCommand cmd = new SqlCommand(sp, con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ps != null) cmd.Parameters.AddRange(ps);
            return cmd;
        }

        // Procedimiento que regresa una tabla
        public static DataTable Consultar(string sp, params SqlParameter[] ps)
        {
            using (SqlConnection con = ObtenerConexion())
            using (SqlCommand cmd = Comando(con, sp, ps))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Procedimiento que regresa varias tablas (reportes, factura)
        public static DataSet ConsultarVarios(string sp, params SqlParameter[] ps)
        {
            using (SqlConnection con = ObtenerConexion())
            using (SqlCommand cmd = Comando(con, sp, ps))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        // Procedimiento que regresa un solo valor
        public static object Escalar(string sp, params SqlParameter[] ps)
        {
            using (SqlConnection con = ObtenerConexion())
            using (SqlCommand cmd = Comando(con, sp, ps))
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        // Procedimiento que inserta / actualiza / elimina
        public static int Ejecutar(string sp, params SqlParameter[] ps)
        {
            using (SqlConnection con = ObtenerConexion())
            using (SqlCommand cmd = Comando(con, sp, ps))
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
