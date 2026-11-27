using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public List<Cliente> GetAll(string? filtro = null)
    {
        List<Cliente> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ObtenerClientes", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = (object?)filtro ?? DBNull.Value;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Cliente
            {
                IdCliente = reader.GetInt32(reader.GetOrdinal("idCliente")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                ApellidoPaterno = reader.GetString(reader.GetOrdinal("apellidoPaterno")),
                ApellidoMaterno = reader.GetString(reader.GetOrdinal("apellidoMaterno")),
                RFC = reader.GetString(reader.GetOrdinal("rfc")),
                Domicilio = reader.GetString(reader.GetOrdinal("domicilio")),
                Correo = reader.GetString(reader.GetOrdinal("correo")),
                TelefonoCasa = reader.GetString(reader.GetOrdinal("telefonoCasa")),
                TelefonoCelular = reader.GetString(reader.GetOrdinal("telefonoCelular")),
                FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fechaNacimiento"))
            });
        }

        return lista;
    }

    public void Add(Cliente cliente)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_CrearCliente", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        AgregarParametrosCliente(command, cliente);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(Cliente cliente)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ActualizarCliente", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = cliente.IdCliente;
        AgregarParametrosCliente(command, cliente);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int idCliente, string usuarioAuditoria)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_EliminarCliente", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = usuarioAuditoria;

        connection.Open();
        command.ExecuteNonQuery();
    }

    private static void AgregarParametrosCliente(SqlCommand command, Cliente c)
    {
        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = c.Nombre;
        command.Parameters.Add("@ApellidoPaterno", SqlDbType.VarChar, 50).Value = c.ApellidoPaterno;
        command.Parameters.Add("@ApellidoMaterno", SqlDbType.VarChar, 50).Value = c.ApellidoMaterno;
        command.Parameters.Add("@RFC", SqlDbType.VarChar, 13).Value = c.RFC;
        command.Parameters.Add("@Domicilio", SqlDbType.VarChar, 250).Value = c.Domicilio;
        command.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = c.Correo;
        command.Parameters.Add("@TelefonoCasa", SqlDbType.VarChar, 15).Value = c.TelefonoCasa;
        command.Parameters.Add("@TelefonoCelular", SqlDbType.VarChar, 15).Value = c.TelefonoCelular;
        command.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = c.FechaNacimiento.Date;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = c.UsuarioAuditoria;
    }
}
