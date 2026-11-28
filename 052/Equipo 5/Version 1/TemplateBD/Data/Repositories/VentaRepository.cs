using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories
{
    public class ZonaOpcion
    {
        public int IdZona { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
        public int Disponibles { get; set; }
        public string Descripcion => $"{Nombre} - ${PrecioBase:N2} ({Disponibles} disp.)";
    }

    public class VentaRepository
    {
        private readonly DatabaseConnection _db;

        public VentaRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public List<ZonaOpcion> ObtenerZonasPorPartido(int idPartido)
        {
            var lista = new List<ZonaOpcion>();
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarZonasPorPartido", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@id_partido", SqlDbType.Int).Value = idPartido;

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new ZonaOpcion
                {
                    IdZona = Convert.ToInt32(reader["id_zona"]),
                    Nombre = reader["nombre"].ToString() ?? string.Empty,
                    PrecioBase = Convert.ToDecimal(reader["precio_base"]),
                    Disponibles = Convert.ToInt32(reader["disponibles"])
                });
            }
            return lista;
        }

        public List<Boleto> ObtenerBoletos()
        {
            var lista = new List<Boleto>();
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarBoletos", conn) { CommandType = CommandType.StoredProcedure };

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Boleto
                {
                    FolioBoleto = Convert.ToInt32(reader["folio_boleto"]),
                    FolioVenta = reader["folio_venta"].ToString() ?? string.Empty,
                    IdPartido = Convert.ToInt32(reader["id_partido"]),
                    PartidoNombre = reader["partido_nombre"].ToString() ?? string.Empty,
                    IdZona = Convert.ToInt32(reader["id_zona"]),
                    Fila = reader["fila"].ToString() ?? string.Empty,
                    NumAsiento = reader["num_asiento"].ToString() ?? string.Empty,
                    PrecioUnitario = Convert.ToDecimal(reader["precio_unitario"])
                });
            }
            return lista;
        }

        public void RegistrarVentaBoleto(Boleto boleto, int idCliente, int idUsuario, string metodoPago)
        {
            using var conn = _db.CreateConnection();
            using var cmd = new SqlCommand("sp_RegistrarVentaBoleto", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@folio_venta", SqlDbType.VarChar, 36).Value = boleto.FolioVenta;
            cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = idCliente;
            cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@metodo_pago", SqlDbType.VarChar, 30).Value = metodoPago;
            cmd.Parameters.Add("@id_partido", SqlDbType.Int).Value = boleto.IdPartido;
            cmd.Parameters.Add("@id_zona", SqlDbType.Int).Value = boleto.IdZona;
            cmd.Parameters.Add("@fila", SqlDbType.VarChar, 10).Value = boleto.Fila;
            cmd.Parameters.Add("@num_asiento", SqlDbType.VarChar, 10).Value = boleto.NumAsiento;
            cmd.Parameters.Add("@precio_unitario", SqlDbType.Decimal).Value = boleto.PrecioUnitario;

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}