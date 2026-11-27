using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;


namespace TemplateCSharp.Data.Repositories;

public class ReporteRepository : IReporteRepository
{
    private readonly DatabaseConnection _database;

    public ReporteRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<ReporteVentaItem> ObtenerReporteVentas(DateTime inicio, DateTime fin, int idEstadio, int idPartido)
    {
        List<ReporteVentaItem> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ReporteVentas", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = inicio.Date;
        command.Parameters.Add("@FechaFin", SqlDbType.Date).Value = fin.Date;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = idEstadio;
        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = idPartido;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new ReporteVentaItem
            {
                FolioVenta = reader.GetString(reader.GetOrdinal("folioVenta")),
                FechaHoraVenta = reader.GetString(reader.GetOrdinal("fechaHoraVenta")),
                Estadio = reader.GetString(reader.GetOrdinal("estadio")),
                Partido = reader.GetString(reader.GetOrdinal("partido")),
                FechaPartido = reader.GetString(reader.GetOrdinal("fechaPartido")),
                Zona = reader.GetString(reader.GetOrdinal("zona")),
                CantidadBoletos = reader.GetInt32(reader.GetOrdinal("cantidadBoletos")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("subtotal")),
                Total = reader.GetDecimal(reader.GetOrdinal("total")),
                MetodoPago = reader.GetString(reader.GetOrdinal("metodoPago"))
            });
        }

        return lista;
    }

    public List<ReporteOcupacionItem> ObtenerReporteOcupacion(int idPartido, string? zona, bool soloDisponibles)
    {
        List<ReporteOcupacionItem> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ReporteOcupacion", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = idPartido;
        command.Parameters.Add("@Zona", SqlDbType.VarChar, 50).Value = (object?)zona ?? DBNull.Value;
        command.Parameters.Add("@SoloDisponibles", SqlDbType.Bit).Value = soloDisponibles;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new ReporteOcupacionItem
            {
                IdPartido = reader.GetInt32(reader.GetOrdinal("idPartido")),
                Partido = reader.GetString(reader.GetOrdinal("Partido")),
                Zona = reader.GetString(reader.GetOrdinal("Zona")),
                PrecioBase = reader.GetDecimal(reader.GetOrdinal("PrecioBase")),
                AsientosTotales = reader.GetInt32(reader.GetOrdinal("AsientosTotales")),
                AsientosVendidos = reader.GetInt32(reader.GetOrdinal("AsientosVendidos")),
                AsientosDisponibles = reader.GetInt32(reader.GetOrdinal("AsientosDisponibles")),
                PorcentajeOcupacion = reader.GetDecimal(reader.GetOrdinal("PorcentajeOcupacion"))
            });
        }

        return lista;
    }

    public List<HistorialClienteItem> ObtenerHistorialCliente(int idCliente, int tipoOrden)
    {
        List<HistorialClienteItem> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_HistorialCliente", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
        command.Parameters.Add("@TipoOrden", SqlDbType.Int).Value = tipoOrden;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new HistorialClienteItem
            {
                FolioCompra = reader.GetString(reader.GetOrdinal("folioCompra")),
                FechaCompra = reader.GetString(reader.GetOrdinal("fechaCompra")),
                Partido = reader.GetString(reader.GetOrdinal("partido")),
                Estadio = reader.GetString(reader.GetOrdinal("estadio")),
                Zona = reader.GetString(reader.GetOrdinal("zona")),
                FechaPartido = reader.GetString(reader.GetOrdinal("fechaPartido")),
                CantidadBoletos = reader.GetInt32(reader.GetOrdinal("cantidadBoletos")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("subtotal")),
                Total = reader.GetDecimal(reader.GetOrdinal("total")),
                MetodoPago = reader.GetString(reader.GetOrdinal("metodoPago")),
                Factura = reader.GetString(reader.GetOrdinal("factura"))
            });
        }

        return lista;
    }
}