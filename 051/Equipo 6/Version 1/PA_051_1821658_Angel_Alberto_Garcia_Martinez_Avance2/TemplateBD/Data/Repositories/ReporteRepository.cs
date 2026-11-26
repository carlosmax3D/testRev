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

    // sp_Reporte_Ventas devuelve dos resultados: las ventas y los totales por partido
    public ReporteVentas ConsultarVentas(DateTime fechaInicial, DateTime fechaFinal, int? idEstadio, int? idPartido, int idUsuario)
    {
        ReporteVentas reporte = new();

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Reporte_Ventas", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@FechaInicial", SqlDbType.Date).Value = fechaInicial.Date;
        command.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = fechaFinal.Date;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = (object?)idEstadio ?? DBNull.Value;
        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = (object?)idPartido ?? DBNull.Value;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            reporte.Ventas.Add(ResumenVentaMapper.Map(reader));
        }

        reader.NextResult();

        while (reader.Read())
        {
            reporte.Partidos.Add(new TotalPartido
            {
                Estadio = reader.GetString(reader.GetOrdinal("Estadio")),
                EquipoLocal = reader.GetString(reader.GetOrdinal("EquipoLocal")),
                EquipoVisitante = reader.GetString(reader.GetOrdinal("EquipoVisitante")),
                FechaPartido = reader.GetDateTime(reader.GetOrdinal("FechaPartido")),
                Ventas = reader.GetInt32(reader.GetOrdinal("Ventas")),
                Boletos = reader.GetInt32(reader.GetOrdinal("Boletos")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                Total = reader.GetDecimal(reader.GetOrdinal("Total"))
            });
        }

        return reporte;
    }

    public List<Estadio> ConsultarEstadiosFiltro(int idUsuario)
    {
        List<Estadio> estadios = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Reporte_EstadiosFiltro", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            estadios.Add(new Estadio
            {
                IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Ciudad = reader.GetString(reader.GetOrdinal("Ciudad"))
            });
        }

        return estadios;
    }

    public List<Partido> ConsultarPartidosFiltro(int idUsuario)
    {
        List<Partido> partidos = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Reporte_PartidosFiltro", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            partidos.Add(new Partido
            {
                IdPartido = reader.GetInt32(reader.GetOrdinal("IdPartido")),
                FechaHora = reader.GetDateTime(reader.GetOrdinal("FechaHora")),
                Estado = reader.GetString(reader.GetOrdinal("Estado")),
                IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio")),
                Estadio = reader.GetString(reader.GetOrdinal("Estadio")),
                Ciudad = reader.GetString(reader.GetOrdinal("Ciudad")),
                IdEquipoLocal = reader.GetInt32(reader.GetOrdinal("IdEquipoLocal")),
                EquipoLocal = reader.GetString(reader.GetOrdinal("EquipoLocal")),
                IdEquipoVisitante = reader.GetInt32(reader.GetOrdinal("IdEquipoVisitante")),
                EquipoVisitante = reader.GetString(reader.GetOrdinal("EquipoVisitante"))
            });
        }

        return partidos;
    }

    public ReporteOcupacion ConsultarOcupacion(int? idPartido, int? idZona, bool soloDisponibles, int idAdministrador)
    {
        ReporteOcupacion reporte = new();

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Reporte_Ocupacion", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = (object?)idPartido ?? DBNull.Value;
        command.Parameters.Add("@IdZona", SqlDbType.Int).Value = (object?)idZona ?? DBNull.Value;
        command.Parameters.Add("@SoloDisponibles", SqlDbType.Bit).Value = soloDisponibles;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;
        SqlParameter vendidos = command.Parameters.Add("@BoletosVendidos", SqlDbType.Int);
        vendidos.Direction = ParameterDirection.Output;

        connection.Open();

        // El parámetro de salida se llena al cerrar el lector
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                reporte.Zonas.Add(LeerOcupacion(reader));
            }
        }

        reporte.BoletosVendidos = vendidos.Value is int total ? total : 0;
        return reporte;
    }

    private static OcupacionZona LeerOcupacion(SqlDataReader reader)
    {
        return new OcupacionZona
        {
            FechaPartido = reader.GetDateTime(reader.GetOrdinal("FechaPartido")),
            Estadio = reader.GetString(reader.GetOrdinal("Estadio")),
            EquipoLocal = reader.GetString(reader.GetOrdinal("EquipoLocal")),
            EquipoVisitante = reader.GetString(reader.GetOrdinal("EquipoVisitante")),
            NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
            PrecioBase = reader.GetDecimal(reader.GetOrdinal("PrecioBase")),
            AsientosTotales = reader.GetInt32(reader.GetOrdinal("AsientosTotales")),
            AsientosVendidos = reader.GetInt32(reader.GetOrdinal("AsientosVendidos"))
        };
    }

    public List<ResumenVenta> ConsultarHistorialCliente(int idCliente, bool ordenarPorPartido, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Cliente_Historial", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
        command.Parameters.Add("@OrdenarPorPartido", SqlDbType.Bit).Value = ordenarPorPartido;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        return LeerVentas(connection, command);
    }

    private static List<ResumenVenta> LeerVentas(SqlConnection connection, SqlCommand command)
    {
        List<ResumenVenta> ventas = [];

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            ventas.Add(ResumenVentaMapper.Map(reader));
        }

        return ventas;
    }
}
