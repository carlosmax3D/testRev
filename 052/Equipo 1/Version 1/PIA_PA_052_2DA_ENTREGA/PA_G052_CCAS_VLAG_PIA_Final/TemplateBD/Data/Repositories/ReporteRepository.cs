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
    public class ReporteRepository : IReporteRepository
    {
        private readonly DatabaseConnection _database;

        public ReporteRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<ReporteVenta> ObtenerReporteVentas(DateTime fechaInicio, DateTime fechaFin, int? idEstadio, int? idPartido)
        {
            List<ReporteVenta> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Reporte_Ventas", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@fecha_inicio", SqlDbType.Date).Value = fechaInicio;
            command.Parameters.Add("@fecha_fin", SqlDbType.Date).Value = fechaFin;
            command.Parameters.Add("@id_estadio", SqlDbType.Int).Value = (object?)idEstadio ?? DBNull.Value;
            command.Parameters.Add("@id_partido", SqlDbType.Int).Value = (object?)idPartido ?? DBNull.Value;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new ReporteVenta
                {
                    IdVenta = reader.GetGuid(reader.GetOrdinal("id_venta")),
                    FechaVenta = reader.GetDateTime(reader.GetOrdinal("fecha_venta")),
                    HoraVenta = reader.GetTimeSpan(reader.GetOrdinal("hora_venta")),
                    Estadio = reader.GetString(reader.GetOrdinal("estadio")),
                    Partido = reader.GetString(reader.GetOrdinal("partido")),
                    FechaPartido = reader.GetDateTime(reader.GetOrdinal("fecha_partido")),
                    Zona = reader.GetString(reader.GetOrdinal("zona")),
                    CantidadBoletos = reader.GetInt32(reader.GetOrdinal("cantidad_boletos")),
                    Subtotal = reader.GetDecimal(reader.GetOrdinal("subtotal")),
                    Total = reader.GetDecimal(reader.GetOrdinal("total")),
                    MetodoPago = reader.GetString(reader.GetOrdinal("metodo_pago"))
                });
            }
            return lista;
        }

        public List<ReporteOcupacion> ObtenerReporteOcupacion(int? idPartido, int? idZona, bool? soloDisponibles)
        {
            List<ReporteOcupacion> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Reporte_Ocupacion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_partido", SqlDbType.Int).Value = (object?)idPartido ?? DBNull.Value;
            command.Parameters.Add("@id_zona", SqlDbType.Int).Value = (object?)idZona ?? DBNull.Value;
            command.Parameters.Add("@solo_disponibles", SqlDbType.Bit).Value = (object?)soloDisponibles ?? DBNull.Value;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new ReporteOcupacion
                {
                    IdPartido = reader.GetInt32(reader.GetOrdinal("id_partido")),
                    Partido = reader.GetString(reader.GetOrdinal("partido")),
                    Zona = reader.GetString(reader.GetOrdinal("zona")),
                    PrecioBase = reader.GetDecimal(reader.GetOrdinal("precio_base")),
                    AsientosTotales = reader.GetInt32(reader.GetOrdinal("asientos_totales")),
                    AsientosVendidos = reader.GetInt32(reader.GetOrdinal("asientos_vendidos")),
                    AsientosDisponibles = reader.GetInt32(reader.GetOrdinal("asientos_disponibles")),
                    PorcentajeOcupacion = reader.GetDecimal(reader.GetOrdinal("porcentaje_ocupacion"))
                });
            }
            return lista;
        }

        public List<HistorialCompra> ObtenerHistorialPorCliente(int idCliente, string orden)
        {
            List<HistorialCompra> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Reporte_HistorialCliente", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_cliente", SqlDbType.Int).Value = idCliente;
            command.Parameters.Add("@orden", SqlDbType.VarChar, 20).Value = orden;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new HistorialCompra
                {
                    IdVenta = reader.GetGuid(reader.GetOrdinal("id_venta")),
                    FechaVenta = reader.GetDateTime(reader.GetOrdinal("fecha_venta")),
                    Partido = reader.GetString(reader.GetOrdinal("partido")),
                    Estadio = reader.GetString(reader.GetOrdinal("estadio")),
                    Zona = reader.GetString(reader.GetOrdinal("zona")),
                    FechaPartido = reader.GetDateTime(reader.GetOrdinal("fecha_partido")),
                    CantidadBoletos = reader.GetInt32(reader.GetOrdinal("cantidad_boletos")),
                    Subtotal = reader.GetDecimal(reader.GetOrdinal("subtotal")),
                    Total = reader.GetDecimal(reader.GetOrdinal("total")),
                    MetodoPago = reader.GetString(reader.GetOrdinal("metodo_pago")),
                    FolioFactura = reader.IsDBNull(reader.GetOrdinal("folio_factura")) ? "N/A" : reader.GetString(reader.GetOrdinal("folio_factura")),
                    EstadoFactura = reader.IsDBNull(reader.GetOrdinal("estado_factura")) ? "Sin Factura" : reader.GetString(reader.GetOrdinal("estado_factura"))
                });
            }
            return lista;
        }

        public List<ReporteTopPartido> ObtenerTopPartidos(int cantidad)
        {
            List<ReporteTopPartido> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Reporte_TopPartidos", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@cantidad", SqlDbType.Int).Value = cantidad;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new ReporteTopPartido
                {
                    IdPartido = reader.GetInt32(reader.GetOrdinal("id_partido")),
                    Partido = reader.GetString(reader.GetOrdinal("partido")),
                    Estadio = reader.GetString(reader.GetOrdinal("estadio")),
                    FechaPartido = reader.GetDateTime(reader.GetOrdinal("fecha_partido")),
                    BoletosVendidos = reader.GetInt32(reader.GetOrdinal("boletos_vendidos")),
                    IngresoTotal = reader.GetDecimal(reader.GetOrdinal("ingreso_total"))
                });
            }
            return lista;
        }

        public List<ReporteClienteFrecuente> ObtenerClientesFrecuentes(int cantidad)
        {
            List<ReporteClienteFrecuente> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Reporte_ClientesFrecuentes", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@cantidad", SqlDbType.Int).Value = cantidad;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new ReporteClienteFrecuente
                {
                    IdCliente = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                    Cliente = reader.GetString(reader.GetOrdinal("cliente")),
                    Correo = reader.GetString(reader.GetOrdinal("correo")),
                    TotalCompras = reader.GetInt32(reader.GetOrdinal("total_compras")),
                    TotalGastado = reader.GetDecimal(reader.GetOrdinal("total_gastado"))
                });
            }
            return lista;
        }

        public List<ReporteVentasPorZona> ObtenerVentasPorZona()
        {
            List<ReporteVentasPorZona> lista = new();
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Reporte_VentasPorZona", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new ReporteVentasPorZona
                {
                    IdZona = reader.GetInt32(reader.GetOrdinal("id_zona")),
                    Zona = reader.GetString(reader.GetOrdinal("zona")),
                    Estadio = reader.GetString(reader.GetOrdinal("estadio")),
                    PrecioBase = reader.GetDecimal(reader.GetOrdinal("precio_base")),
                    BoletosVendidos = reader.GetInt32(reader.GetOrdinal("boletos_vendidos")),
                    IngresoTotal = reader.GetDecimal(reader.GetOrdinal("ingreso_total"))
                });
            }
            return lista;
        }

        public ResumenPartido? ObtenerResumenPartido(int idPartido)
        {
            using SqlConnection connection = _database.CreateConnection();
            using SqlCommand command = new("sp_Partido_ConsultarResumenCompleto", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id_partido", SqlDbType.Int).Value = idPartido;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return new ResumenPartido
            {
                IdPartido = reader.GetInt32(reader.GetOrdinal("id_partido")),
                Partido = reader.GetString(reader.GetOrdinal("partido")),
                Estadio = reader.GetString(reader.GetOrdinal("estadio")),
                FechaPartido = reader.GetDateTime(reader.GetOrdinal("fecha_partido")),
                TotalVendido = reader.GetDecimal(reader.GetOrdinal("total_vendido")),
                AsientosDisponiblesTotal = reader.GetInt32(reader.GetOrdinal("asientos_disponibles_total")),
                PorcentajeOcupacionPromedio = reader.GetDecimal(reader.GetOrdinal("porcentaje_ocupacion_promedio"))
            };
        }


    }
}
