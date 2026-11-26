using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PIA_PA.Data;

public class DatabaseConnection
{
    private readonly IConfiguration _configuration;

    public DatabaseConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection GetConnection()
    {
        string? connectionString = _configuration
            .GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "No se encontró la cadena de conexión DefaultConnection.");
        }

        return new SqlConnection(connectionString);
    }
}