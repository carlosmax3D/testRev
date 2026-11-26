using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class EstadioRepository : IEstadioRepository
{
    private readonly DatabaseConnection _database;

    public EstadioRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Estadio> GetAll()
    {
        List<Estadio> estadios = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Estadio_ObtenerTodos", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            estadios.Add(MapEstadio(reader));
        }

        return estadios;
    }

    public Estadio? GetById(int id)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Estadio_ObtenerPorId", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = id;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? MapEstadio(reader) : null;
    }

    public int Add(Estadio estadio)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Estadio_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = estadio.Nombre;
        command.Parameters.Add("@Ciudad", SqlDbType.VarChar, 100).Value = estadio.Ciudad;
        command.Parameters.Add("@Capacidad", SqlDbType.Int).Value = estadio.Capacidad;
        command.Parameters.Add("@MapaEstadio", SqlDbType.VarChar, 300).Value =
            (object?)estadio.MapaEstadio ?? DBNull.Value;
        command.Parameters.Add("@IdUsuarioRegistro", SqlDbType.Int).Value = estadio.IdUsuarioRegistro!.Value;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado);
    }

    public bool ExisteNombre(string nombre, int idExcluir = 0)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Estadio_ExisteNombre", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombre;
        command.Parameters.Add("@IdExcluir", SqlDbType.Int).Value = idExcluir;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado) > 0;
    }

    private static Estadio MapEstadio(SqlDataReader reader)
    {
        return new Estadio
        {
            IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio")),
            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
            Ciudad = reader.GetString(reader.GetOrdinal("Ciudad")),
            Capacidad = reader.GetInt32(reader.GetOrdinal("Capacidad")),
            MapaEstadio = reader.IsDBNull(reader.GetOrdinal("MapaEstadio"))
                ? null
                : reader.GetString(reader.GetOrdinal("MapaEstadio")),
            FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
            IdUsuarioRegistro = reader.GetInt32(reader.GetOrdinal("IdUsuarioRegistro"))
        };
    }
}
