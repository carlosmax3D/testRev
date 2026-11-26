using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TemplateCSharp.Data;
public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection(IConfiguration configuration)
    {
        string rawConn = configuration.GetConnectionString("DefaultConnection")
            ?? "Server=.\\SQLEXPRESS;Database=VtaBoletos_DB;Trusted_Connection=True;TrustServerCertificate=True;";

        _connectionString = rawConn.Replace("TemplateCSharp", "VtaBoletos_DB");
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
