using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly DatabaseConnection _database;

    public ProductoRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<Producto> GetAll()
    {
        List<Producto> productos = [];

        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            SELECT id, nombre, precio
            FROM productos
            ORDER BY id
            """;

        using SqlCommand command = new(sql, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            productos.Add(MapProducto(reader));
        }

        return productos;
    }

    public Producto? GetById(int id)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            SELECT id, nombre, precio
            FROM productos
            WHERE id = @id
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return MapProducto(reader);
    }

    public void Add(Producto producto)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            INSERT INTO productos (nombre, precio)
            VALUES (@nombre, @precio)
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.Add(
            "@nombre",
            SqlDbType.NVarChar,
            100).Value = producto.Nombre;

        SqlParameter precioParameter =
            command.Parameters.Add("@precio", SqlDbType.Decimal);

        precioParameter.Precision = 10;
        precioParameter.Scale = 2;
        precioParameter.Value = producto.Precio;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void Update(Producto producto)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            UPDATE productos
            SET nombre = @nombre,
                precio = @precio
            WHERE id = @id
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.Add(
            "@id",
            SqlDbType.Int).Value = producto.Id;

        command.Parameters.Add(
            "@nombre",
            SqlDbType.NVarChar,
            100).Value = producto.Nombre;

        SqlParameter precioParameter =
            command.Parameters.Add("@precio", SqlDbType.Decimal);

        precioParameter.Precision = 10;
        precioParameter.Scale = 2;
        precioParameter.Value = producto.Precio;

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            DELETE FROM productos
            WHERE id = @id
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        connection.Open();

        command.ExecuteNonQuery();
    }

    private static Producto MapProducto(SqlDataReader reader)
    {
        return new Producto
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Nombre = reader.GetString(reader.GetOrdinal("nombre")),
            Precio = reader.GetDecimal(reader.GetOrdinal("precio"))
        };
    }
}
