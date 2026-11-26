using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class AsientoRepository : IAsientoRepository
{
    private readonly DatabaseConnection _database;

    public AsientoRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Asiento> GetDisponiblesPorZonaPartido(int idZona, int idPartido)
    {
        List<Asiento> asientos = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Asiento_DisponiblesPorZonaPartido", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdZona", SqlDbType.Int).Value = idZona;
        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = idPartido;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            asientos.Add(new Asiento
            {
                IdAsiento = reader.GetInt32(reader.GetOrdinal("IdAsiento")),
                Fila = reader.GetString(reader.GetOrdinal("Fila")),
                Numero = reader.GetInt32(reader.GetOrdinal("Numero"))
            });
        }

        return asientos;
    }
}
