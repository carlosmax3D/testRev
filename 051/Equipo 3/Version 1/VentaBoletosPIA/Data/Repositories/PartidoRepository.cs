using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class PartidoRepository : IPartidoRepository
{
    private readonly DatabaseConnection _database;

    public PartidoRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public int Add(Partido partido)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Partido_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@EquipoLocal", SqlDbType.VarChar, 50).Value = partido.EquipoLocal;
        command.Parameters.Add("@EquipoVisitante", SqlDbType.VarChar, 50).Value = partido.EquipoVisitante;
        command.Parameters.Add("@FechaHora", SqlDbType.DateTime).Value = partido.FechaHora;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = partido.IdEstadio;
        command.Parameters.Add("@IdUsuarioRegistro", SqlDbType.Int).Value = partido.IdUsuarioRegistro!.Value;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado);
    }

    public Partido? GetById(int id)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Partido_ObtenerPorId", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = id;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? MapPartido(reader) : null;
    }

    public List<Partido> GetDisponiblesParaVenta()
    {
        List<Partido> partidos = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Partido_ObtenerDisponiblesParaVenta", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            partidos.Add(MapPartido(reader));
        }

        return partidos;
    }

    public List<Partido> Buscar(DateTime? fechaDesde, DateTime? fechaHasta, int? idEstadio, string? equipo)
    {
        List<Partido> partidos = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Partido_Buscar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@FechaDesde", SqlDbType.DateTime).Value = (object?)fechaDesde ?? DBNull.Value;
        command.Parameters.Add("@FechaHasta", SqlDbType.DateTime).Value = (object?)fechaHasta ?? DBNull.Value;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = (object?)idEstadio ?? DBNull.Value;
        command.Parameters.Add("@Equipo", SqlDbType.VarChar, 50).Value = (object?)equipo ?? DBNull.Value;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            partidos.Add(MapPartido(reader));
        }

        return partidos;
    }

    private static Partido MapPartido(SqlDataReader reader)
    {
        return new Partido
        {
            IdPartido = reader.GetInt32(reader.GetOrdinal("IdPartido")),
            EquipoLocal = reader.GetString(reader.GetOrdinal("EquipoLocal")),
            EquipoVisitante = reader.GetString(reader.GetOrdinal("EquipoVisitante")),
            FechaHora = reader.GetDateTime(reader.GetOrdinal("FechaHora")),
            IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio")),
            NombreEstadio = reader.GetString(reader.GetOrdinal("NombreEstadio")),
            CiudadEstadio = reader.GetString(reader.GetOrdinal("CiudadEstadio")),
            IdEstadoPartido = reader.GetInt32(reader.GetOrdinal("IdEstadoPartido")),
            EstadoPartido = reader.GetString(reader.GetOrdinal("EstadoPartido")),
            FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
            IdUsuarioRegistro = reader.GetInt32(reader.GetOrdinal("IdUsuarioRegistro"))
        };
    }
}
