using System.Data;
using Microsoft.Data.SqlClient;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly DatabaseConnection _database;

    public ClienteRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Cliente> GetAll()
    {
        List<Cliente> clientes = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Cliente_ObtenerTodos", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            clientes.Add(MapCliente(reader));
        }

        return clientes;
    }

    public Cliente? GetByRFC(string rfc)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Cliente_ObtenerPorRFC", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@RFC", SqlDbType.Char, 13).Value = rfc;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? MapCliente(reader) : null;
    }

    public List<Cliente> BuscarPorApellidoOCorreo(string texto)
    {
        List<Cliente> clientes = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Cliente_BuscarPorApellidoOCorreo", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Texto", SqlDbType.VarChar, 150).Value = texto;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            clientes.Add(MapCliente(reader));
        }

        return clientes;
    }

    public void Add(Cliente cliente)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Cliente_Insertar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@RFC", SqlDbType.Char, 13).Value = cliente.RFC;
        AgregarParametrosComunes(command, cliente);
        command.Parameters.Add("@IdUsuarioRegistro", SqlDbType.Int).Value = cliente.IdUsuarioRegistro!.Value;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void Update(Cliente cliente)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Cliente_Actualizar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@RFC", SqlDbType.Char, 13).Value = cliente.RFC;
        AgregarParametrosComunes(command, cliente);
        command.Parameters.Add("@IdUsuarioModificacion", SqlDbType.Int).Value = cliente.IdUsuarioModificacion!.Value;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void DeleteLogico(string rfc, int idUsuarioModificacion)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Cliente_EliminarLogico", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@RFC", SqlDbType.Char, 13).Value = rfc;
        command.Parameters.Add("@IdUsuarioModificacion", SqlDbType.Int).Value = idUsuarioModificacion;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public bool ExisteRfcOCorreo(string rfc, string correo, string? rfcExcluir = null)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("sp_Cliente_ExisteRFCOCorreo", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@RFC", SqlDbType.Char, 13).Value = rfc;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = correo;
        command.Parameters.Add("@RFCExcluir", SqlDbType.Char, 13).Value = (object?)rfcExcluir ?? DBNull.Value;

        connection.Open();

        object? resultado = command.ExecuteScalar();

        return Convert.ToInt32(resultado) > 0;
    }

    private static void AgregarParametrosComunes(SqlCommand command, Cliente cliente)
    {
        command.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, 150).Value = cliente.NombreCompleto;
        command.Parameters.Add("@DomicilioCompleto", SqlDbType.VarChar, 255).Value = cliente.DomicilioCompleto;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = cliente.Correo;
        command.Parameters.Add("@TelefonoCasa", SqlDbType.VarChar, 15).Value =
            (object?)cliente.TelefonoCasa ?? DBNull.Value;
        command.Parameters.Add("@TelefonoCelular", SqlDbType.VarChar, 15).Value = cliente.TelefonoCelular;
        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = cliente.FechaNacimiento;
    }

    private static Cliente MapCliente(SqlDataReader reader)
    {
        return new Cliente
        {
            RFC = reader.GetString(reader.GetOrdinal("RFC")).Trim(),
            NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
            DomicilioCompleto = reader.GetString(reader.GetOrdinal("DomicilioCompleto")),
            Correo = reader.GetString(reader.GetOrdinal("Correo")),
            TelefonoCasa = reader.IsDBNull(reader.GetOrdinal("TelefonoCasa"))
                ? null
                : reader.GetString(reader.GetOrdinal("TelefonoCasa")),
            TelefonoCelular = reader.GetString(reader.GetOrdinal("TelefonoCelular")),
            FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
            Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
            FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
            IdUsuarioRegistro = reader.GetInt32(reader.GetOrdinal("IdUsuarioRegistro")),
            FechaModificacion = reader.IsDBNull(reader.GetOrdinal("FechaModificacion"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("FechaModificacion")),
            IdUsuarioModificacion = reader.IsDBNull(reader.GetOrdinal("IdUsuarioModificacion"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("IdUsuarioModificacion"))
        };
    }
}
