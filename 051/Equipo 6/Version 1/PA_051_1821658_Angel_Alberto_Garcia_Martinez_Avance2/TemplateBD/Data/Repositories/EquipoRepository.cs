using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class EquipoRepository : IEquipoRepository
{
    private readonly DatabaseConnection _database;

    public EquipoRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Equipo> Consultar(string? busqueda, int idAdministrador)
    {
        List<Equipo> equipos = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Equipo_Consultar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Busqueda", SqlDbType.VarChar, Longitudes.NombreEquipo).Value =
            string.IsNullOrWhiteSpace(busqueda) ? DBNull.Value : busqueda.Trim();
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Equipo equipo = new()
            {
                IdEquipo = reader.GetInt32(reader.GetOrdinal("IdEquipo")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
            };
            RegistroMapper.LeerRegistro(reader, equipo);
            RegistroMapper.LeerEstado(reader, equipo);
            equipos.Add(equipo);
        }

        return equipos;
    }

    public int Insertar(string nombre, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Equipo_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, Longitudes.NombreEquipo).Value = nombre;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        return Convert.ToInt32(command.ExecuteScalar());
    }

    public void Actualizar(int idEquipo, string nombre, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Equipo_Actualizar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = idEquipo;
        command.Parameters.Add("@Nombre", SqlDbType.VarChar, Longitudes.NombreEquipo).Value = nombre;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int idEquipo, int idAdministrador)
    {
        EjecutarPorId("dbo.sp_Equipo_Eliminar", "@IdEquipo", idEquipo, idAdministrador);
    }

    public void Reactivar(int idEquipo, int idAdministrador)
    {
        EjecutarPorId("dbo.sp_Equipo_Reactivar", "@IdEquipo", idEquipo, idAdministrador);
    }

    // Eliminar y reactivar solo mandan el Id del registro y el del administrador
    private void EjecutarPorId(string procedimiento, string parametro, int id, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new(procedimiento, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add(parametro, SqlDbType.Int).Value = id;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();
        command.ExecuteNonQuery();
    }
}
