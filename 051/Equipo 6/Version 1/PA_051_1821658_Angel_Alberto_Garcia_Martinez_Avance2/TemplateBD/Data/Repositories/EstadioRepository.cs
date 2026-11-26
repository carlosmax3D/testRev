using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class EstadioRepository : IEstadioRepository
{
    private readonly DatabaseConnection _database;

    public EstadioRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Estadio> Consultar(int idAdministrador)
    {
        List<Estadio> estadios = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Estadio_Consultar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Estadio estadio = new()
            {
                IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Ciudad = reader.GetString(reader.GetOrdinal("Ciudad")),
                CapacidadTotal = reader.GetInt32(reader.GetOrdinal("CapacidadTotal")),
                CupoAsignado = reader.GetInt32(reader.GetOrdinal("CupoAsignado"))
            };
            RegistroMapper.LeerRegistro(reader, estadio);
            RegistroMapper.LeerEstado(reader, estadio);
            estadios.Add(estadio);
        }

        return estadios;
    }

    public int Insertar(Estadio estadio, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Estadio_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, Longitudes.NombreEstadio).Value = estadio.Nombre;
        command.Parameters.Add("@Ciudad", SqlDbType.VarChar, Longitudes.CiudadEstadio).Value = estadio.Ciudad;
        command.Parameters.Add("@CapacidadTotal", SqlDbType.Int).Value = estadio.CapacidadTotal;
        // -1 = VARBINARY(MAX)
        command.Parameters.Add("@MapaEstadio", SqlDbType.VarBinary, -1).Value = estadio.MapaEstadio;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        return Convert.ToInt32(command.ExecuteScalar());
    }

    public void Actualizar(Estadio estadio, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Estadio_Actualizar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = estadio.IdEstadio;
        command.Parameters.Add("@Nombre", SqlDbType.VarChar, Longitudes.NombreEstadio).Value = estadio.Nombre;
        command.Parameters.Add("@Ciudad", SqlDbType.VarChar, Longitudes.CiudadEstadio).Value = estadio.Ciudad;
        command.Parameters.Add("@CapacidadTotal", SqlDbType.Int).Value = estadio.CapacidadTotal;
        command.Parameters.Add("@MapaEstadio", SqlDbType.VarBinary, -1).Value =
            estadio.MapaEstadio.Length == 0 ? DBNull.Value : estadio.MapaEstadio;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int idEstadio, int idAdministrador)
    {
        EjecutarPorId("dbo.sp_Estadio_Eliminar", "@IdEstadio", idEstadio, idAdministrador);
    }

    public void Reactivar(int idEstadio, int idAdministrador)
    {
        EjecutarPorId("dbo.sp_Estadio_Reactivar", "@IdEstadio", idEstadio, idAdministrador);
    }

    public byte[] ObtenerMapa(int idEstadio, int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Estadio_ObtenerMapa", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = idEstadio;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        return command.ExecuteScalar() as byte[] ?? [];
    }

    public List<Zona> ConsultarZonas(int idEstadio, int idAdministrador)
    {
        List<Zona> zonas = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Zona_Consultar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = idEstadio;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Zona zona = new()
            {
                IdZona = reader.GetInt32(reader.GetOrdinal("IdZona")),
                IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio")),
                NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
                PrecioBase = reader.GetDecimal(reader.GetOrdinal("PrecioBase")),
                Cupo = reader.GetInt32(reader.GetOrdinal("Cupo")),
                AsientosGenerados = reader.GetInt32(reader.GetOrdinal("AsientosGenerados"))
            };
            RegistroMapper.LeerRegistro(reader, zona);
            RegistroMapper.LeerEstado(reader, zona);
            zonas.Add(zona);
        }

        return zonas;
    }

    public int InsertarZona(Zona zona, int asientosPorFila, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        // Una zona grande genera miles de asientos en un ciclo: se da más tiempo que los 30 s por omisión
        using SqlCommand command = new("dbo.sp_Zona_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure,
            CommandTimeout = 180
        };

        command.Parameters.Add("@IdEstadio", SqlDbType.Int).Value = zona.IdEstadio;
        command.Parameters.Add("@NombreZona", SqlDbType.VarChar, Longitudes.NombreZona).Value = zona.NombreZona;

        AgregarPrecio(command, zona.PrecioBase);

        command.Parameters.Add("@Cupo", SqlDbType.Int).Value = zona.Cupo;
        command.Parameters.Add("@AsientosPorFila", SqlDbType.Int).Value = asientosPorFila;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        return Convert.ToInt32(command.ExecuteScalar());
    }

    public void ActualizarZona(Zona zona, int idAdministrador)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Zona_Actualizar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdZona", SqlDbType.Int).Value = zona.IdZona;
        command.Parameters.Add("@NombreZona", SqlDbType.VarChar, Longitudes.NombreZona).Value = zona.NombreZona;

        AgregarPrecio(command, zona.PrecioBase);

        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void EliminarZona(int idZona, int idAdministrador)
    {
        EjecutarPorId("dbo.sp_Zona_Eliminar", "@IdZona", idZona, idAdministrador);
    }

    public void ReactivarZona(int idZona, int idAdministrador)
    {
        EjecutarPorId("dbo.sp_Zona_Reactivar", "@IdZona", idZona, idAdministrador);
    }

    public List<Asiento> ConsultarAsientos(int idZona, int idAdministrador)
    {
        List<Asiento> asientos = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Asiento_Consultar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdZona", SqlDbType.Int).Value = idZona;
        command.Parameters.Add("@IdAdministradorAccion", SqlDbType.Int).Value = idAdministrador;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            asientos.Add(new Asiento
            {
                IdAsiento = reader.GetInt32(reader.GetOrdinal("IdAsiento")),
                IdZona = reader.GetInt32(reader.GetOrdinal("IdZona")),
                Fila = reader.GetString(reader.GetOrdinal("Fila")),
                Numero = reader.GetInt32(reader.GetOrdinal("Numero"))
            });
        }

        return asientos;
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

    // Zona.PrecioBase es DECIMAL(10,2)
    private static void AgregarPrecio(SqlCommand command, decimal precioBase)
    {
        SqlParameter precio = command.Parameters.Add("@PrecioBase", SqlDbType.Decimal);
        precio.Precision = 10;
        precio.Scale = 2;
        precio.Value = precioBase;
    }
}
