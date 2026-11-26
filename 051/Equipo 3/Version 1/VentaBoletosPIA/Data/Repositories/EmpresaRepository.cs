using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly DatabaseConnection _database;

    public EmpresaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public Empresa? Obtener()
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Empresa_Obtener", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new Empresa
        {
            IdEmpresa = reader.GetInt32(reader.GetOrdinal("IdEmpresa")),
            RazonSocial = reader.GetString(reader.GetOrdinal("RazonSocial")),
            RFC = reader.GetString(reader.GetOrdinal("RFC")).Trim(),
            DomicilioCompleto = reader.GetString(reader.GetOrdinal("DomicilioCompleto"))
        };
    }
}
