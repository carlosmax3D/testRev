using System.Data;
using Microsoft.Data.SqlClient;

namespace TemplateCSharp.Data;

/// <summary>
/// Clase base de los repositorios. Concentra la forma de llamar a los
/// procedimientos almacenados: ningún repositorio ejecuta SQL directo.
/// </summary>
public abstract class RepositorioBase
{
    private readonly DatabaseConnection _database;

    protected RepositorioBase(DatabaseConnection database)
    {
        _database = database;
    }

    protected SqlConnection CrearConexion()
    {
        return _database.CreateConnection();
    }

    // ---------- Creación de parámetros ----------

    protected static SqlParameter Parametro(string nombre, SqlDbType tipo, object? valor)
    {
        SqlParameter parametro = new SqlParameter(nombre, tipo);
        parametro.Value = valor ?? DBNull.Value;
        return parametro;
    }

    protected static SqlParameter ParametroTexto(string nombre, string? valor, int longitud)
    {
        SqlParameter parametro = new SqlParameter(nombre, SqlDbType.VarChar, longitud);
        parametro.Value = (object?)valor ?? DBNull.Value;
        return parametro;
    }

    /// <summary>Parámetro VARBINARY(MAX), por ejemplo para la imagen del mapa.</summary>
    protected static SqlParameter ParametroBinario(string nombre, byte[]? valor)
    {
        SqlParameter parametro = new SqlParameter(nombre, SqlDbType.VarBinary, -1);
        parametro.Value = valor == null || valor.Length == 0 ? DBNull.Value : valor;
        return parametro;
    }

    protected static SqlParameter ParametroDecimal(string nombre, decimal? valor)
    {
        SqlParameter parametro = new SqlParameter(nombre, SqlDbType.Decimal);
        parametro.Precision = 10;
        parametro.Scale = 2;
        parametro.Value = (object?)valor ?? DBNull.Value;
        return parametro;
    }

    // ---------- Ejecución de procedimientos almacenados ----------

    protected static SqlCommand CrearComando(
        string procedimiento,
        SqlConnection connection,
        SqlTransaction? transaction,
        SqlParameter[] parametros)
    {
        SqlCommand command = new SqlCommand(procedimiento, connection);
        command.CommandType = CommandType.StoredProcedure;

        if (transaction != null)
        {
            command.Transaction = transaction;
        }

        if (parametros.Length > 0)
        {
            command.Parameters.AddRange(parametros);
        }

        return command;
    }

    /// <summary>Ejecuta un procedimiento y devuelve todos sus resultados.</summary>
    protected DataSet EjecutarConjunto(string procedimiento, params SqlParameter[] parametros)
    {
        using SqlConnection connection = CrearConexion();
        using SqlCommand command = CrearComando(procedimiento, connection, null, parametros);
        using SqlDataAdapter adapter = new SqlDataAdapter(command);

        DataSet datos = new DataSet();
        adapter.Fill(datos);

        return datos;
    }

    /// <summary>Ejecuta un procedimiento y devuelve su primer resultado.</summary>
    protected DataTable EjecutarTabla(string procedimiento, params SqlParameter[] parametros)
    {
        DataSet datos = EjecutarConjunto(procedimiento, parametros);

        return datos.Tables.Count > 0 ? datos.Tables[0] : new DataTable();
    }

    /// <summary>Ejecuta un procedimiento que no devuelve renglones.</summary>
    protected void EjecutarAccion(string procedimiento, params SqlParameter[] parametros)
    {
        using SqlConnection connection = CrearConexion();
        using SqlCommand command = CrearComando(procedimiento, connection, null, parametros);

        connection.Open();
        command.ExecuteNonQuery();
    }

    /// <summary>Ejecuta un procedimiento y devuelve el primer valor del resultado.</summary>
    protected object? EjecutarEscalar(string procedimiento, params SqlParameter[] parametros)
    {
        using SqlConnection connection = CrearConexion();
        using SqlCommand command = CrearComando(procedimiento, connection, null, parametros);

        connection.Open();
        return command.ExecuteScalar();
    }

    protected static void EjecutarAccionEnTransaccion(
        SqlConnection connection,
        SqlTransaction transaction,
        string procedimiento,
        params SqlParameter[] parametros)
    {
        using SqlCommand command = CrearComando(procedimiento, connection, transaction, parametros);
        command.ExecuteNonQuery();
    }

    protected static object? EjecutarEscalarEnTransaccion(
        SqlConnection connection,
        SqlTransaction transaction,
        string procedimiento,
        params SqlParameter[] parametros)
    {
        using SqlCommand command = CrearComando(procedimiento, connection, transaction, parametros);
        return command.ExecuteScalar();
    }
}
