using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class ReporteRepository : IReporteRepository
{
    private readonly DatabaseConnection _database;

    public ReporteRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<ReporteVentaItem> ReporteVentas(
        DateTime? fechaDesde, DateTime? fechaHasta, int? idEstadio, int? idPartido)
    {
        List<ReporteVentaItem> items = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Reporte_Ventas", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@FechaDesde", SqlDbType.DateTime).Value = (object?)fechaDesde ?? DBNull.Value;
        command.Parameters.Add("@FechaHasta", SqlDbType.DateTime).Value = (object?)fechaHasta ?? DBNull.Value;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = (object?)idEstadio ?? DBNull.Value;
        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = (object?)idPartido ?? DBNull.Value;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            items.Add(new ReporteVentaItem
            {
                Folio = reader.GetGuid(reader.GetOrdinal("Folio")),
                FechaVenta = reader.GetDateTime(reader.GetOrdinal("FechaVenta")),
                NombreEstadio = reader.GetString(reader.GetOrdinal("NombreEstadio")),
                EquipoLocal = reader.GetString(reader.GetOrdinal("EquipoLocal")),
                EquipoVisitante = reader.GetString(reader.GetOrdinal("EquipoVisitante")),
                FechaPartido = reader.GetDateTime(reader.GetOrdinal("FechaPartido")),
                NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
                CantidadBoletos = reader.GetInt32(reader.GetOrdinal("CantidadBoletos")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                MetodoPago = reader.GetString(reader.GetOrdinal("MetodoPago"))
            });
        }

        return items;
    }

    public List<DisponibilidadZona> ReporteOcupacion(int? idPartido, int? idZona, bool soloConDisponibles)
    {
        List<DisponibilidadZona> items = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Reporte_Ocupacion", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = (object?)idPartido ?? DBNull.Value;
        command.Parameters.Add("@IdZona", SqlDbType.Int).Value = (object?)idZona ?? DBNull.Value;
        command.Parameters.Add("@SoloConDisponibles", SqlDbType.Bit).Value = soloConDisponibles;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            string local = reader.GetString(reader.GetOrdinal("EquipoLocal"));
            string visitante = reader.GetString(reader.GetOrdinal("EquipoVisitante"));

            items.Add(new DisponibilidadZona
            {
                IdPartido = reader.GetInt32(reader.GetOrdinal("IdPartido")),
                DescripcionPartido = $"{local} vs {visitante}",
                FechaPartido = reader.GetDateTime(reader.GetOrdinal("FechaHora")),
                IdZona = reader.GetInt32(reader.GetOrdinal("IdZona")),
                NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                AsientosTotales = reader.GetInt32(reader.GetOrdinal("AsientosTotales")),
                AsientosVendidos = reader.GetInt32(reader.GetOrdinal("AsientosVendidos")),
                AsientosDisponibles = reader.GetInt32(reader.GetOrdinal("AsientosDisponibles")),
                PorcentajeOcupacion = reader.GetDecimal(reader.GetOrdinal("PorcentajeOcupacion"))
            });
        }

        return items;
    }

    public List<HistorialClienteItem> HistorialCliente(string rfc)
    {
        List<HistorialClienteItem> items = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Reporte_HistorialCliente", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@RFC", SqlDbType.Char, 13).Value = rfc;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            int ordinalFactura = reader.GetOrdinal("FolioFactura");

            items.Add(new HistorialClienteItem
            {
                Folio = reader.GetGuid(reader.GetOrdinal("Folio")),
                FechaVenta = reader.GetDateTime(reader.GetOrdinal("FechaVenta")),
                EquipoLocal = reader.GetString(reader.GetOrdinal("EquipoLocal")),
                EquipoVisitante = reader.GetString(reader.GetOrdinal("EquipoVisitante")),
                NombreEstadio = reader.GetString(reader.GetOrdinal("NombreEstadio")),
                FechaPartido = reader.GetDateTime(reader.GetOrdinal("FechaPartido")),
                NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
                CantidadBoletos = reader.GetInt32(reader.GetOrdinal("CantidadBoletos")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                MetodoPago = reader.GetString(reader.GetOrdinal("MetodoPago")),
                EstadoVenta = reader.GetString(reader.GetOrdinal("EstadoVenta")),
                FolioFactura = reader.IsDBNull(ordinalFactura) ? null : reader.GetString(ordinalFactura)
            });
        }

        return items;
    }
}
