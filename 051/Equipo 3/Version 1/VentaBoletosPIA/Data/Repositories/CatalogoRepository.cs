using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models.Lookups;

namespace VentaBoletosApp.Data.Repositories;

public class CatalogoRepository : ICatalogoRepository
{
    private readonly DatabaseConnection _database;

    public CatalogoRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<CatalogoItem> ObtenerTiposUsuario() =>
        Ejecutar("sp_TiposUsuario_ObtenerTodos", "IdTipoUsuario", "Nombre");

    public List<CatalogoItem> ObtenerMetodosPago() =>
        Ejecutar("sp_MetodosPago_ObtenerTodos", "IdMetodoPago", "Metodo");

    public List<CatalogoItem> ObtenerEstadosPartido() =>
        Ejecutar("sp_EstadosPartido_ObtenerTodos", "IdEstadoPartido", "Estado");

    private List<CatalogoItem> Ejecutar(string nombreSp, string columnaId, string columnaNombre)
    {
        List<CatalogoItem> resultado = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new(nombreSp, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            resultado.Add(new CatalogoItem
            {
                Id = reader.GetInt32(reader.GetOrdinal(columnaId)),
                Nombre = reader.GetString(reader.GetOrdinal(columnaNombre))
            });
        }

        return resultado;
    }
}
