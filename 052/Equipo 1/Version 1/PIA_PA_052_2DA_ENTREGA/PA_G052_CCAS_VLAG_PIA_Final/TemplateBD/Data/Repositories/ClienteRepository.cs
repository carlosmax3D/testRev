using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
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

    public List<Cliente> GetAll()
    {
        List<Cliente> clientes = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_Cliente_Consultar", connection);
        command.CommandType = CommandType.StoredProcedure;
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            clientes.Add(MapCliente(reader));
        }
        return clientes;
    }

    public Cliente? GetById(int id)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_Cliente_ConsultarPorId", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@id_cliente", SqlDbType.Int).Value = id;
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read()) return null;
        return MapCliente(reader);
    }

    public List<Cliente> BuscarPorApellidos(string apellido_pa, string apellido_ma)
    {
        List<Cliente> clientes = new();
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_Cliente_BuscarApellidos", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@apellido_pa", SqlDbType.VarChar, 100).Value = (object?)apellido_pa ?? DBNull.Value;
        command.Parameters.Add("@apellido_ma", SqlDbType.VarChar, 100).Value = (object?)apellido_ma ?? DBNull.Value;
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
        using SqlCommand command = new("sp_Cliente_Insertar", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = cliente.Nombre;
        command.Parameters.Add("@apellido_pa", SqlDbType.VarChar, 50).Value = cliente.ApellidoPa;
        command.Parameters.Add("@apellido_ma", SqlDbType.VarChar, 50).Value = cliente.ApellidoMa;
        command.Parameters.Add("@correo", SqlDbType.VarChar, 100).Value = cliente.Correo;
        command.Parameters.Add("@calle", SqlDbType.VarChar, 100).Value = cliente.Calle;
        command.Parameters.Add("@numero", SqlDbType.VarChar, 20).Value = cliente.Numero;
        command.Parameters.Add("@colonia", SqlDbType.VarChar, 100).Value = cliente.Colonia;
        command.Parameters.Add("@CP", SqlDbType.VarChar, 10).Value = cliente.CP;
        command.Parameters.Add("@telefono_casa", SqlDbType.VarChar, 20).Value = cliente.TelefonoCasa;
        command.Parameters.Add("@telefono_celular", SqlDbType.VarChar, 20).Value = cliente.TelefonoCelular;
        command.Parameters.Add("@fecha_nacimiento", SqlDbType.Date).Value = cliente.FechaNacimiento;
        command.Parameters.Add("@rfc", SqlDbType.VarChar, 13).Value = cliente.RFC;
        command.Parameters.Add("@usuario_cambio", SqlDbType.Int).Value = cliente.UsuarioCambio ?? (object)DBNull.Value;
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(Cliente cliente)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_Cliente_Actualizar", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@id_cliente", SqlDbType.Int).Value = cliente.IdCliente;
        command.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = cliente.Nombre;
        command.Parameters.Add("@apellido_pa", SqlDbType.VarChar, 50).Value = cliente.ApellidoPa;
        command.Parameters.Add("@apellido_ma", SqlDbType.VarChar, 50).Value = cliente.ApellidoMa;
        command.Parameters.Add("@correo", SqlDbType.VarChar, 100).Value = cliente.Correo;
        command.Parameters.Add("@calle", SqlDbType.VarChar, 100).Value = cliente.Calle;
        command.Parameters.Add("@numero", SqlDbType.VarChar, 20).Value = cliente.Numero;
        command.Parameters.Add("@colonia", SqlDbType.VarChar, 100).Value = cliente.Colonia;
        command.Parameters.Add("@CP", SqlDbType.VarChar, 10).Value = cliente.CP;
        command.Parameters.Add("@telefono_casa", SqlDbType.VarChar, 20).Value = cliente.TelefonoCasa;
        command.Parameters.Add("@telefono_celular", SqlDbType.VarChar, 20).Value = cliente.TelefonoCelular;
        command.Parameters.Add("@fecha_nacimiento", SqlDbType.Date).Value = cliente.FechaNacimiento;
        command.Parameters.Add("@rfc", SqlDbType.VarChar, 13).Value = cliente.RFC;
        command.Parameters.Add("@usuario_cambio", SqlDbType.Int).Value = cliente.UsuarioCambio ?? (object)DBNull.Value;
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_Cliente_Eliminar", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@id_cliente", SqlDbType.Int).Value = id;
        connection.Open();
        command.ExecuteNonQuery();
    }
    public int ExisteRFC(string rfc, int idExcluir = 0)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_Cliente_ExisteRFC", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@rfc", SqlDbType.VarChar, 13).Value = rfc;
        command.Parameters.Add("@id_excluir", SqlDbType.Int).Value = idExcluir;
        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public int ExisteCorreo(string correo, int idExcluir = 0)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_Cliente_ExisteCorreo", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@correo", SqlDbType.VarChar, 100).Value = correo;
        command.Parameters.Add("@id_excluir", SqlDbType.Int).Value = idExcluir;
        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar());
    }

    private static Cliente MapCliente(SqlDataReader reader)
    {
        return new Cliente
        {
            IdCliente = reader.GetInt32(reader.GetOrdinal("id_cliente")),
            Nombre = reader.GetString(reader.GetOrdinal("nombre")),
            ApellidoPa = reader.GetString(reader.GetOrdinal("apellido_pa")),
            ApellidoMa = reader.GetString(reader.GetOrdinal("apellido_ma")),
            Correo = reader.GetString(reader.GetOrdinal("correo")),
            Calle = reader.GetString(reader.GetOrdinal("calle")),
            Numero = reader.GetString(reader.GetOrdinal("numero")),
            Colonia = reader.GetString(reader.GetOrdinal("colonia")),
            CP = reader.GetString(reader.GetOrdinal("CP")),
            TelefonoCasa = reader.GetString(reader.GetOrdinal("telefono_casa")),
            TelefonoCelular = reader.GetString(reader.GetOrdinal("telefono_celular")),
            FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")),
            RFC = reader.GetString(reader.GetOrdinal("rfc")),
            UsuarioCambio = reader.IsDBNull(reader.GetOrdinal("usuario_cambio")) ? null : reader.GetInt32(reader.GetOrdinal("usuario_cambio")),
            FechaCambio = reader.IsDBNull(reader.GetOrdinal("fecha_cambio")) ? null : reader.GetDateTime(reader.GetOrdinal("fecha_cambio")),
            HoraCambio = reader.IsDBNull(reader.GetOrdinal("hora_cambio")) ? null : reader.GetTimeSpan(reader.GetOrdinal("hora_cambio"))
        };
    }
}