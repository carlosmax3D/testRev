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

public class ZonaRepository : IZonaRepository
{
    private readonly DatabaseConnection _database;

    public ZonaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Zona> GetAll()
    {
        List<Zona> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ObtenerZonas", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Zona
            {
                IdZona = reader.GetInt32(reader.GetOrdinal("idZona")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                PrecioBase = reader.GetDecimal(reader.GetOrdinal("precioBase")),
                Capacidad = reader.GetInt32(reader.GetOrdinal("capacidad")),
                IdEstadio = reader.GetInt32(reader.GetOrdinal("idEstadio")),
                NombreEstadio = reader.GetString(reader.GetOrdinal("nombreEstadio"))
            });
        }

        return lista;
    }

    public void Add(Zona zona)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_CrearZona", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = zona.Nombre;

        SqlParameter precioParam = command.Parameters.Add("@PrecioBase", SqlDbType.Decimal);
        precioParam.Precision = 10;
        precioParam.Scale = 2;
        precioParam.Value = zona.PrecioBase;

        command.Parameters.Add("@Capacidad", SqlDbType.Int).Value = zona.Capacidad;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = zona.IdEstadio;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = zona.UsuarioAuditoria;

        connection.Open();
        command.ExecuteNonQuery();
    }
}