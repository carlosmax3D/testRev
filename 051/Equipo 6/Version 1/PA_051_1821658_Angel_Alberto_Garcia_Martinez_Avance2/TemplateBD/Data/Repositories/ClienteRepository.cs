using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly DatabaseConnection _database;

    public ClienteRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Cliente> Consultar(string? busqueda, int idUsuario, bool incluirInactivos = false)
    {
        List<Cliente> clientes = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Cliente_Consultar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
        command.Parameters.Add("@Busqueda", SqlDbType.VarChar, Longitudes.Busqueda).Value =
            string.IsNullOrWhiteSpace(busqueda) ? DBNull.Value : busqueda.Trim();
        command.Parameters.Add("@IncluirInactivos", SqlDbType.Bit).Value = incluirInactivos;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            clientes.Add(MapCliente(reader));
        }

        return clientes;
    }

    public int Insertar(Cliente cliente, int idUsuarioRegistro, bool corregirCodigoPostal)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Cliente_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        AgregarParametrosCliente(command, cliente);
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuarioRegistro;
        command.Parameters.Add("@CorregirCodigoPostal", SqlDbType.Bit).Value = corregirCodigoPostal;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado);
    }

    public void Actualizar(Cliente cliente, int idUsuarioModifico, bool corregirCodigoPostal)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Cliente_Actualizar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = cliente.IdCliente;
        AgregarParametrosCliente(command, cliente);
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuarioModifico;
        command.Parameters.Add("@CorregirCodigoPostal", SqlDbType.Bit).Value = corregirCodigoPostal;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void Reactivar(int idCliente, int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Cliente_Reactivar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void Eliminar(int idCliente, int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Cliente_Eliminar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public CodigoPostalInfo? ObtenerCodigoPostal(string codigoPostal, int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_CodigoPostal_Obtener", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
        command.Parameters.Add("@CodigoPostal", SqlDbType.Char, Longitudes.CodigoPostal).Value = codigoPostal;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new CodigoPostalInfo
        {
            CodigoPostal = reader.GetString(reader.GetOrdinal("CodigoPostal")),
            Ciudad = reader.GetString(reader.GetOrdinal("Ciudad")),
            EntidadFederativa = reader.GetString(reader.GetOrdinal("EntidadFederativa"))
        };
    }

    private static void AgregarParametrosCliente(SqlCommand command, Cliente cliente)
    {
        command.Parameters.Add("@Nombre", SqlDbType.VarChar, Longitudes.NombreCliente).Value = cliente.Nombre;
        command.Parameters.Add("@ApellidoPaterno", SqlDbType.VarChar, Longitudes.Apellido).Value = cliente.ApellidoPaterno;
        command.Parameters.Add("@ApellidoMaterno", SqlDbType.VarChar, Longitudes.Apellido).Value = cliente.ApellidoMaterno;
        command.Parameters.Add("@Calle", SqlDbType.VarChar, Longitudes.Calle).Value = cliente.Calle;
        command.Parameters.Add("@NumeroExterior", SqlDbType.VarChar, Longitudes.NumeroExterior).Value = cliente.NumeroExterior;
        command.Parameters.Add("@Colonia", SqlDbType.VarChar, Longitudes.Colonia).Value = cliente.Colonia;
        command.Parameters.Add("@CodigoPostal", SqlDbType.Char, Longitudes.CodigoPostal).Value = cliente.CodigoPostal;
        command.Parameters.Add("@Ciudad", SqlDbType.VarChar, Longitudes.Ciudad).Value = cliente.Ciudad;
        command.Parameters.Add("@EntidadFederativa", SqlDbType.VarChar, Longitudes.EntidadFederativa).Value = cliente.EntidadFederativa;
        command.Parameters.Add("@RFC", SqlDbType.Char, Longitudes.RfcPersona).Value = cliente.RFC;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, Longitudes.Correo).Value = cliente.Correo;
        command.Parameters.Add("@TelefonoCasa", SqlDbType.Char, Longitudes.Telefono).Value = cliente.TelefonoCasa;
        command.Parameters.Add("@TelefonoCelular", SqlDbType.Char, Longitudes.Telefono).Value = cliente.TelefonoCelular;
        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = cliente.FechaNacimiento.Date;
    }

    private static Cliente MapCliente(SqlDataReader reader)
    {
        Cliente cliente = new()
        {
            IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
            ApellidoPaterno = reader.GetString(reader.GetOrdinal("ApellidoPaterno")),
            ApellidoMaterno = reader.GetString(reader.GetOrdinal("ApellidoMaterno")),
            Calle = reader.GetString(reader.GetOrdinal("Calle")),
            NumeroExterior = reader.GetString(reader.GetOrdinal("NumeroExterior")),
            Colonia = reader.GetString(reader.GetOrdinal("Colonia")),
            CodigoPostal = reader.GetString(reader.GetOrdinal("CodigoPostal")),
            Ciudad = reader.GetString(reader.GetOrdinal("Ciudad")),
            EntidadFederativa = reader.GetString(reader.GetOrdinal("EntidadFederativa")),
            RFC = reader.GetString(reader.GetOrdinal("RFC")),
            Correo = reader.GetString(reader.GetOrdinal("Correo")),
            TelefonoCasa = reader.GetString(reader.GetOrdinal("TelefonoCasa")),
            TelefonoCelular = reader.GetString(reader.GetOrdinal("TelefonoCelular")),
            FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
            Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
        };

        RegistroMapper.LeerRegistro(reader, cliente);
        RegistroMapper.LeerModificacion(reader, cliente);
        return cliente;
    }
}
