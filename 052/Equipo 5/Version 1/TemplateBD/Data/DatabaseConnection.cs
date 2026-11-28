using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TemplateCSharp.Data;

public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection()
    {
        // 1. Intenta leer desde App.config
        var appConfigConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"]?.ConnectionString;

        if (!string.IsNullOrEmpty(appConfigConn))
        {
            _connectionString = appConfigConn;
        }
        else
        {
            // Cadena de respaldo si falla App.config
            _connectionString = "Data Source=.;Initial Catalog=PIA_MAD_IXDA_BLSS;Integrated Security=True;TrustServerCertificate=True;";
        }
    }

    public DatabaseConnection(Microsoft.Extensions.Configuration.IConfiguration configuration)
        : this()
    {
        // 2. Si existe la configuración JSON, la usa prioritariamente
        var jsonConn = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrEmpty(jsonConn))
        {
            _connectionString = jsonConn;
        }
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}