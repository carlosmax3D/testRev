using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class VentaRepository : IVentaRepository
{
    private readonly DatabaseConnection _database;

    public VentaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Zona> ConsultarZonasDisponibles(int idPartido, int idUsuario)
    {
        List<Zona> zonas = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Zona_ConsultarDisponibles", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = idPartido;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            zonas.Add(new Zona
            {
                IdZona = reader.GetInt32(reader.GetOrdinal("IdZona")),
                IdEstadio = reader.GetInt32(reader.GetOrdinal("IdEstadio")),
                NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
                PrecioBase = reader.GetDecimal(reader.GetOrdinal("PrecioBase")),
                Cupo = reader.GetInt32(reader.GetOrdinal("Cupo")),
                Disponibles = reader.GetInt32(reader.GetOrdinal("Disponibles"))
            });
        }

        return zonas;
    }

    public List<Boleto> ConsultarAsientosDisponibles(int idPartido, int idZona, int cantidad, int idUsuario)
    {
        List<Boleto> boletos = [];

        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Asiento_ConsultarDisponibles", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = idPartido;
        command.Parameters.Add("@IdZona", SqlDbType.Int).Value = idZona;
        command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;
        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            boletos.Add(new Boleto
            {
                IdAsiento = reader.GetInt32(reader.GetOrdinal("IdAsiento")),
                IdZona = reader.GetInt32(reader.GetOrdinal("IdZona")),
                NombreZona = reader.GetString(reader.GetOrdinal("NombreZona")),
                Fila = reader.GetString(reader.GetOrdinal("Fila")),
                Numero = reader.GetInt32(reader.GetOrdinal("Numero")),
                PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioBase"))
            });
        }

        return boletos;
    }

    public decimal ObtenerTasaIva()
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Venta_ObtenerTasaIva", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();

        return Convert.ToDecimal(command.ExecuteScalar());
    }

    // El procedimiento guarda venta, boletos y factura en una sola transacción
    public Guid Registrar(Venta venta, int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        using SqlCommand command = new("dbo.sp_Venta_Registrar", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = venta.IdCliente;
        command.Parameters.Add("@IdPartido", SqlDbType.Int).Value = venta.IdPartido;
        command.Parameters.Add("@MetodoPago", SqlDbType.VarChar, Longitudes.MetodoPago).Value = venta.MetodoPago;

        // Parámetro tabla (tipo dbo.ListaAsientos): un renglón por asiento
        DataTable asientos = new();
        asientos.Columns.Add("IdAsiento", typeof(int));
        foreach (Boleto boleto in venta.Boletos)
        {
            asientos.Rows.Add(boleto.IdAsiento);
        }

        SqlParameter lista = command.Parameters.Add("@Asientos", SqlDbType.Structured);
        lista.TypeName = "dbo.ListaAsientos";
        lista.Value = asientos;

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        if (command.ExecuteScalar() is not Guid folio)
        {
            throw new InvalidOperationException("La venta no devolvió un folio.");
        }

        return folio;
    }
}
