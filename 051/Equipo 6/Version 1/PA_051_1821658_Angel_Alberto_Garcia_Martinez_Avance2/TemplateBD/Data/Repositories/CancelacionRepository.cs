using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class CancelacionRepository : ICancelacionRepository
{
    private readonly DatabaseConnection _database;

    public CancelacionRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public ResumenVenta? ObtenerVenta(Guid folio, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Venta_ObtenerResumen", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Folio", SqlDbType.UniqueIdentifier).Value = folio;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? ResumenVentaMapper.Map(reader) : null;
    }

    public void Cancelar(Guid folio, string motivo, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Venta_Cancelar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Folio", SqlDbType.UniqueIdentifier).Value = folio;
        command.Parameters.Add("@Motivo", SqlDbType.VarChar, Longitudes.MotivoCancelacion).Value = motivo;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        command.ExecuteNonQuery();
    }
}
