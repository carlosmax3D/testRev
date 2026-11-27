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

public class VentaRepository : IVentaRepository
{
    private readonly DatabaseConnection _database;

    public VentaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Partido> ObtenerPartidosDisponibles(string? filtro = null)
    {
        List<Partido> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ObtenerPartidosDisponibles", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = (object?)filtro ?? DBNull.Value;

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

    public List<ZonaDisponible> ObtenerZonasDisponibles(int idPartido)
    {
        List<ZonaDisponible> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ObtenerZonasDisponiblesPartido", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = idPartido;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new ZonaDisponible
            {
                IdZona = reader.GetInt32(reader.GetOrdinal("idZona")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                PrecioBase = reader.GetDecimal(reader.GetOrdinal("precioBase")),
                AsientosDisponibles = reader.GetInt32(reader.GetOrdinal("asientosDisponibles"))
            });
        }

        return lista;
    }

    public (string folio, string asientos) RegistrarVenta(SolicitudVenta s)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_RegistrarVenta", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = s.IdCliente;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = s.IdUsuario;
        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = s.IdPartido;
        command.Parameters.Add("@IdZona", SqlDbType.Int).Value = s.IdZona;
        command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = s.Cantidad;
        command.Parameters.Add("@MetodoPago", SqlDbType.VarChar, 50).Value = s.MetodoPago;
        command.Parameters.Add("@NombreTitular", SqlDbType.VarChar, 100).Value = s.NombreTitular;
        command.Parameters.Add("@NumeroTarjeta", SqlDbType.VarChar, 16).Value = s.NumeroTarjeta;
        command.Parameters.Add("@FechaExpiracion", SqlDbType.VarChar, 5).Value = s.FechaExpiracion;
        command.Parameters.Add("@CCV", SqlDbType.VarChar, 4).Value = s.CCV;

        SqlParameter folioOut = new("@FolioCompra", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
        SqlParameter asientosOut = new("@AsientosAsignados", SqlDbType.VarChar, 200) { Direction = ParameterDirection.Output };

        command.Parameters.Add(folioOut);
        command.Parameters.Add(asientosOut);

        connection.Open();
        command.ExecuteNonQuery();

        string folio = folioOut.Value?.ToString() ?? string.Empty;
        string asientos = asientosOut.Value?.ToString() ?? string.Empty;

        return (folio, asientos);
    }
}