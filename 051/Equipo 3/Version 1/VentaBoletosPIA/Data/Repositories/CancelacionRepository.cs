using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;

namespace VentaBoletosApp.Data.Repositories;

public class CancelacionRepository : ICancelacionRepository
{
    private readonly DatabaseConnection _database;

    public CancelacionRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public void Add(string motivo, int idUsuario, int idVenta)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Cancelacion_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Motivo", SqlDbType.VarChar, 255).Value = motivo;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
        command.Parameters.Add("@IdVenta", SqlDbType.Int).Value = idVenta;

        connection.Open();

        command.ExecuteNonQuery();
    }
}
