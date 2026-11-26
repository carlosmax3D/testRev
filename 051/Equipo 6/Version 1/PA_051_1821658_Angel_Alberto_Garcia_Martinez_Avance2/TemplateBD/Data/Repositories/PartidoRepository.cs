using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class PartidoRepository : IPartidoRepository
{
    private readonly DatabaseConnection _database;

    public PartidoRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Partido> Consultar(int idAdministrador)
    {
        List<Partido> partidos = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Partido_Consultar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Partido partido = MapPartido(reader);
            partido.VentasVigentes = reader.GetInt32(reader.GetOrdinal("VentasVigentes"));
            RegistroMapper.LeerRegistro(reader, partido);
            RegistroMapper.LeerEstado(reader, partido);
            partidos.Add(partido);
        }

        return partidos;
    }

    public int Insertar(Partido partido, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Partido_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = partido.IdEstadio;
        command.Parameters.Add("@IdEquipoLocal", SqlDbType.Int).Value = partido.IdEquipoLocal;
        command.Parameters.Add("@IdEquipoVisitante", SqlDbType.Int).Value = partido.IdEquipoVisitante;
        command.Parameters.Add("@FechaHora", SqlDbType.DateTime2).Value = partido.FechaHora;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        return Convert.ToInt32(command.ExecuteScalar());
    }

    public void Actualizar(Partido partido, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Partido_Actualizar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = partido.IdPartido;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = partido.IdEstadio;
        command.Parameters.Add("@IdEquipoLocal", SqlDbType.Int).Value = partido.IdEquipoLocal;
        command.Parameters.Add("@IdEquipoVisitante", SqlDbType.Int).Value = partido.IdEquipoVisitante;
        command.Parameters.Add("@FechaHora", SqlDbType.DateTime2).Value = partido.FechaHora;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int idPartido, int idAdministrador)
    {
        EjecutarPorId("dbo.sp_Partido_Eliminar", "@IdPartido", idPartido, idAdministrador);
    }

    public void Reactivar(int idPartido, int idAdministrador)
    {
        EjecutarPorId("dbo.sp_Partido_Reactivar", "@IdPartido", idPartido, idAdministrador);
    }

    public int Cancelar(int idPartido, string motivo, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Partido_Cancelar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = idPartido;
        command.Parameters.Add("@Motivo", SqlDbType.VarChar, Longitudes.MotivoCancelacion).Value = motivo;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        return Convert.ToInt32(command.ExecuteScalar());
    }

    public List<Partido> ConsultarDisponibles(int idUsuario)
    {
        List<Partido> partidos = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Partido_ConsultarDisponibles", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Partido partido = MapPartido(reader);
            partido.ZonasDisponibles = reader.GetString(reader.GetOrdinal("ZonasDisponibles"));
            partidos.Add(partido);
        }

        return partidos;
    }

    private static Partido MapPartido(SqlDataReader reader)
    {
        return new Partido
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
        };
    }

    // Eliminar y reactivar solo mandan el Id del registro y el del administrador
    private void EjecutarPorId(string procedimiento, string parametro, int id, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new(procedimiento, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add(parametro, SqlDbType.Int).Value = id;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();
        command.ExecuteNonQuery();
    }
}
