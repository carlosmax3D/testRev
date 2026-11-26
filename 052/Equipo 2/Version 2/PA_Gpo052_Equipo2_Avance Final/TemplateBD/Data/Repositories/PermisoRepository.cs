using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class PermisoRepository : IPermisoRepository
{
    private readonly DatabaseConnection _database;

    public PermisoRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public Task<List<Permiso>> ObtenerPermisosPorTipoAsync(int idTipo)
    {
        List<Permiso> permisos = new();

        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            SELECT
                Id_permiso,
                Id_tipo,
                NombreMenu,
                FechaHoraRegistro
            FROM Permisos
            WHERE Id_tipo = @idTipo
            ORDER BY NombreMenu
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.Add("@idTipo", SqlDbType.Int).Value = idTipo;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            permisos.Add(new Permiso
            {
                Id_permiso = reader.GetInt32(
                    reader.GetOrdinal("Id_permiso")),

                Id_tipo = reader.GetInt32(
                    reader.GetOrdinal("Id_tipo")),

                NombreMenu = reader.GetString(
                    reader.GetOrdinal("NombreMenu")),

                FechaHoraRegistro = reader.GetDateTime(
                    reader.GetOrdinal("FechaHoraRegistro"))
            });
        }

        return Task.FromResult(permisos);
    }
}
