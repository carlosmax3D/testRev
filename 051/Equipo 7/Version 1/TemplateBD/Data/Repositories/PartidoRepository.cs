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

public class PartidoRepository : IPartidoRepository
{
    private readonly DatabaseConnection _database;

    public PartidoRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Partido> GetAll()
    {
        List<Partido> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ObtenerPartidos", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Partido
            {
                IdPartido = reader.GetInt32(reader.GetOrdinal("idPartido")),
                Encuentro = reader.GetString(reader.GetOrdinal("Encuentro")),
                Estadio = reader.GetString(reader.GetOrdinal("Estadio")),
                Ciudad = reader.GetString(reader.GetOrdinal("Ciudad")),
                FechaFormateada = reader.GetString(reader.GetOrdinal("Fecha")),
                Hora = reader.GetTimeSpan(reader.GetOrdinal("Hora")),
                Estado = reader.GetString(reader.GetOrdinal("Estado"))
            });
        }

        return lista;
    }

    public void Add(Partido partido)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_CrearPartido", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Fecha", SqlDbType.Date).Value = partido.Fecha.Date;
        command.Parameters.Add("@Hora", SqlDbType.Time).Value = partido.Hora;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = partido.IdEstadio;
        command.Parameters.Add("@IdEquipoLocal", SqlDbType.Int).Value = partido.IdEquipoLocal;
        command.Parameters.Add("@IdEquipoVisitante", SqlDbType.Int).Value = partido.IdEquipoVisitante;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = partido.UsuarioAuditoria;

        connection.Open();
        command.ExecuteNonQuery();
    }
}
