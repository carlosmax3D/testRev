using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly DatabaseConnection _database;

    public EmpresaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public Empresa? Obtener(int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Empresa_Obtener", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new Empresa
        {
            RazonSocial = reader.GetString(reader.GetOrdinal("RazonSocial")),
            RFC = reader.GetString(reader.GetOrdinal("RFC")),
            Domicilio = reader.GetString(reader.GetOrdinal("Domicilio")),
            FechaModificacion = reader.GetDateTime(reader.GetOrdinal("FechaModificacion")),
            ModificadoPor = reader.GetString(reader.GetOrdinal("ModificadoPor"))
        };
    }

    public void Actualizar(Empresa empresa, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Empresa_Actualizar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@RazonSocial", SqlDbType.VarChar, Longitudes.RazonSocial).Value = empresa.RazonSocial;
        command.Parameters.Add("@RFC", SqlDbType.VarChar, Longitudes.RfcEmpresa).Value = empresa.RFC;
        command.Parameters.Add("@Domicilio", SqlDbType.VarChar, Longitudes.DomicilioFiscal).Value = empresa.Domicilio;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        command.ExecuteNonQuery();
    }
}
