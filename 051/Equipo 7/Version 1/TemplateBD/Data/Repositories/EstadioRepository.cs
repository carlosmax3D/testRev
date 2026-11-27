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

public class EstadioRepository : IEstadioRepository
{
    private readonly DatabaseConnection _database;

    public EstadioRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Estadio> GetAll()
    {
        List<Estadio> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ObtenerEstadios", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Estadio
            {
                IdEstadio = reader.GetInt32(reader.GetOrdinal("idEstadio")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                Ciudad = reader.GetString(reader.GetOrdinal("ciudad")),
                Direccion = reader.GetString(reader.GetOrdinal("direccion")),
                Capacidad = reader.GetInt32(reader.GetOrdinal("capacidad")),
                MapaEstadio = reader.GetString(reader.GetOrdinal("mapaEstadio"))
            });
        }

        return lista;
    }

    public void Add(Estadio estadio)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_CrearEstadio", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = estadio.Nombre;
        command.Parameters.Add("@Ciudad", SqlDbType.VarChar, 100).Value = estadio.Ciudad;
        command.Parameters.Add("@Direccion", SqlDbType.VarChar, 200).Value = estadio.Direccion;
        command.Parameters.Add("@Capacidad", SqlDbType.Int).Value = estadio.Capacidad;
        command.Parameters.Add("@MapaEstadio", SqlDbType.VarChar, 255).Value = estadio.MapaEstadio;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = estadio.UsuarioAuditoria;

        connection.Open();
        command.ExecuteNonQuery();
    }
}