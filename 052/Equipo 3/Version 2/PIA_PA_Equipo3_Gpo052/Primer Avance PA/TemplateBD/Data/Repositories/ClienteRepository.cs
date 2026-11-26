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

        const string sql = """
            SELECT id_cliente, nombre, apellido_paterno, apellido_materno, calle, numero, colonia, ciudad, estado, cp, rfc, correo_electronico, telefono_casa, telefono_celular, fecha_nacimiento, usuario_registro 
            FROM CLIENTE
            ORDER BY apellido_paterno, apellido_materno
            """;

        using SqlCommand command = new(sql, connection);
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
        // Similar al anterior, buscando por id_cliente
        return null; // Cambiar por la implementación
    }

    public void Add(Cliente cliente)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            INSERT INTO CLIENTE (nombre, apellido_paterno, apellido_materno, calle, numero, colonia, ciudad, estado, cp, rfc, correo_electronico, telefono_casa, telefono_celular, fecha_nacimiento, usuario_registro)
            VALUES (@nombre, @paterno, @materno, @calle, @numero, @colonia, @ciudad, @estado, @cp, @rfc, @correo, @telCasa, @telCel, @fechaNac, @usuarioReg)
            """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = cliente.Nombre;
        command.Parameters.Add("@paterno", SqlDbType.NVarChar, 100).Value = cliente.ApellidoPaterno;
        command.Parameters.Add("@materno", SqlDbType.NVarChar, 100).Value = (object?)cliente.ApellidoMaterno ?? DBNull.Value;
        command.Parameters.Add("@calle", SqlDbType.NVarChar, 100).Value = cliente.Calle;
        command.Parameters.Add("@numero", SqlDbType.NVarChar, 10).Value = cliente.Numero;
        command.Parameters.Add("@colonia", SqlDbType.NVarChar, 100).Value = cliente.Colonia;
        command.Parameters.Add("@ciudad", SqlDbType.NVarChar, 100).Value = cliente.Ciudad;
        command.Parameters.Add("@estado", SqlDbType.NVarChar, 50).Value = cliente.Estado;
        command.Parameters.Add("@cp", SqlDbType.NVarChar, 10).Value = cliente.CP;
        command.Parameters.Add("@rfc", SqlDbType.NVarChar, 13).Value = cliente.RFC;
        command.Parameters.Add("@correo", SqlDbType.NVarChar, 100).Value = cliente.CorreoElectronico;
        command.Parameters.Add("@telCasa", SqlDbType.NVarChar, 15).Value = cliente.TelefonoCasa;
        command.Parameters.Add("@telCel", SqlDbType.NVarChar, 15).Value = cliente.TelefonoCelular;
        command.Parameters.Add("@fechaNac", SqlDbType.Date).Value = cliente.FechaNacimiento;
        command.Parameters.Add("@usuarioReg", SqlDbType.Int).Value = cliente.UsuarioRegistro;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(Cliente cliente)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ActualizarCliente", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Id", SqlDbType.Int).Value = cliente.IdCliente;
        command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = cliente.Nombre;
        command.Parameters.Add("@Paterno", SqlDbType.NVarChar, 100).Value = cliente.ApellidoPaterno;
        command.Parameters.Add("@Materno", SqlDbType.NVarChar, 100).Value = (object?)cliente.ApellidoMaterno ?? DBNull.Value;
        command.Parameters.Add("@Calle", SqlDbType.NVarChar, 100).Value = cliente.Calle;
        command.Parameters.Add("@Numero", SqlDbType.NVarChar, 10).Value = cliente.Numero;
        command.Parameters.Add("@Colonia", SqlDbType.NVarChar, 100).Value = cliente.Colonia;
        command.Parameters.Add("@Ciudad", SqlDbType.NVarChar, 100).Value = cliente.Ciudad;
        command.Parameters.Add("@Estado", SqlDbType.NVarChar, 50).Value = cliente.Estado;
        command.Parameters.Add("@CP", SqlDbType.NVarChar, 10).Value = cliente.CP;
        command.Parameters.Add("@RFC", SqlDbType.NVarChar, 13).Value = cliente.RFC;
        command.Parameters.Add("@Correo", SqlDbType.NVarChar, 100).Value = cliente.CorreoElectronico;
        command.Parameters.Add("@TelCasa", SqlDbType.NVarChar, 15).Value = cliente.TelefonoCasa;
        command.Parameters.Add("@TelCel", SqlDbType.NVarChar, 15).Value = cliente.TelefonoCelular;
        command.Parameters.Add("@FechaNac", SqlDbType.Date).Value = cliente.FechaNacimiento;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_EliminarCliente", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static Cliente MapCliente(SqlDataReader reader)
    {
        return new Cliente
        {
            IdCliente = reader.GetInt32(reader.GetOrdinal("id_cliente")),
            Nombre = reader.GetString(reader.GetOrdinal("nombre")),
            ApellidoPaterno = reader.GetString(reader.GetOrdinal("apellido_paterno")),
            ApellidoMaterno = reader.IsDBNull(reader.GetOrdinal("apellido_materno")) ? null : reader.GetString(reader.GetOrdinal("apellido_materno")),
            Calle = reader.GetString(reader.GetOrdinal("calle")),
            Numero = reader.GetString(reader.GetOrdinal("numero")),
            Colonia = reader.GetString(reader.GetOrdinal("colonia")),
            Ciudad = reader.GetString(reader.GetOrdinal("ciudad")),
            Estado = reader.GetString(reader.GetOrdinal("estado")),
            CP = reader.GetString(reader.GetOrdinal("cp")),
            RFC = reader.GetString(reader.GetOrdinal("rfc")),
            CorreoElectronico = reader.GetString(reader.GetOrdinal("correo_electronico")),
            TelefonoCasa = reader.GetString(reader.GetOrdinal("telefono_casa")),
            TelefonoCelular = reader.GetString(reader.GetOrdinal("telefono_celular")),
            FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")),
            UsuarioRegistro = reader.GetInt32(reader.GetOrdinal("usuario_registro"))
        };
    }
}
