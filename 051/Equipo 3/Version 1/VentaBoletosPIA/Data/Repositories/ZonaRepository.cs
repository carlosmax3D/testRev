using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class ZonaRepository : IZonaRepository
{
    private readonly DatabaseConnection _database;

    public ZonaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Zona> GetByEstadio(int idEstadio)
    {
        List<Zona> zonas = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Zona_ObtenerPorEstadio", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = idEstadio;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            zonas.Add(new Zona
            {
                IdZona = reader.GetInt32(reader.GetOrdinal("IdZona")),
                NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                Cupo = reader.GetInt32(reader.GetOrdinal("Cupo")),
                IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio")),
                NombreEstadio = reader.GetString(reader.GetOrdinal("NombreEstadio")),
                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                IdUsuarioRegistro = reader.GetInt32(reader.GetOrdinal("IdUsuarioRegistro"))
            });
        }

        return zonas;
    }

    public int Add(Zona zona)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Zona_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@NombreZona", SqlDbType.VarChar, 50).Value = zona.NombreZona;

        SqlParameter precio = command.Parameters.Add("@Precio", SqlDbType.Decimal);
        precio.Precision = 10;
        precio.Scale = 2;
        precio.Value = zona.Precio;

        command.Parameters.Add("@Cupo", SqlDbType.Int).Value = zona.Cupo;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = zona.IdEstadio;
        command.Parameters.Add("@IdUsuarioRegistro", SqlDbType.Int).Value = zona.IdUsuarioRegistro!.Value;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado);
    }

    public bool ExisteNombreEnEstadio(string nombreZona, int idEstadio)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Zona_ExisteNombreEnEstadio", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@NombreZona", SqlDbType.VarChar, 50).Value = nombreZona;
        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = idEstadio;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado) > 0;
    }

    public List<DisponibilidadZona> ObtenerDisponibilidadPorPartido(int idPartido)
    {
        List<DisponibilidadZona> lista = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Zona_DisponibilidadPorPartido", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = idPartido;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            int total = reader.GetInt32(reader.GetOrdinal("AsientosTotales"));
            int vendidos = reader.GetInt32(reader.GetOrdinal("AsientosVendidos"));

            lista.Add(new DisponibilidadZona
            {
                IdZona = reader.GetInt32(reader.GetOrdinal("IdZona")),
                NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                AsientosTotales = total,
                AsientosVendidos = vendidos,
                AsientosDisponibles = reader.GetInt32(reader.GetOrdinal("AsientosDisponibles")),
                PorcentajeOcupacion = total == 0 ? 0 : Math.Round(vendidos * 100m / total, 2)
            });
        }

        return lista;
    }
}
