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

public class EquipoRepository : IEquipoRepository
{
    private readonly DatabaseConnection _database;

    public EquipoRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Equipo> GetAll()
    {
        List<Equipo> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ObtenerEquipos", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Equipo
            {
                IdEquipo = reader.GetInt32(reader.GetOrdinal("idEquipo")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                Ciudad = reader.GetString(reader.GetOrdinal("ciudad"))
            });
        }

        return lista;
    }

    public void Add(Equipo equipo)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_CrearEquipo", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = equipo.Nombre;
        command.Parameters.Add("@Ciudad", SqlDbType.VarChar, 100).Value = equipo.Ciudad;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = equipo.UsuarioAuditoria;

        connection.Open();
        command.ExecuteNonQuery();
    }
}