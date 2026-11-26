using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly DatabaseConnection _database;

    public TipoUsuarioRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public Task<List<TipoUsuario>> ObtenerTodosAsync()
    {
        List<TipoUsuario> tiposUsuario = new();

        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            SELECT Id_tipo, Descripcion
            FROM TiposUsuario
            ORDER BY Descripcion
            """;

        using SqlCommand command = new(sql, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            tiposUsuario.Add(new TipoUsuario
            {
                Id_tipo = reader.GetInt32(
                    reader.GetOrdinal("Id_tipo")),

                Descripcion = reader.GetString(
                    reader.GetOrdinal("Descripcion"))
            });
        }

        return Task.FromResult(tiposUsuario);
    }
}