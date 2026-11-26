using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PIA_PA.Comun.Data;

public class DatabaseConnection
{
    private readonly IConfiguration _configuration;

    public DatabaseConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection GetConnection()
    {
        string? connectionString = _configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("No se encontró ConnectionStrings:DefaultConnection.");

        return new SqlConnection(connectionString);
    }
}
