using Microsoft.Data.SqlClient;
using System.Data;
using PIA_PA.Comun.Data;
using PIA_PA.Entidades.Estadio;
using AreaEntidad = PIA_PA.Entidades.Area.Area;
using AsientoEntidad = PIA_PA.Entidades.Asiento.Asiento;

namespace PIA_PA.Entidades.Estadio;

public class EstadioRepository : IEstadioRepository
{
    private readonly DatabaseConnection _databaseConnection;

    public EstadioRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public List<Estadio> ObtenerTodos()
    {
        List<Estadio> entities = new();

        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Estadio_ID], [Estadio_Nombre], [Estadio_Ciudad], [Estadio_Capacidad] FROM [Estadio]";
        using SqlCommand command = new(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            entities.Add(Mapear(reader));

        return entities;
    }

    public Estadio? ObtenerPorId(int estadio_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "SELECT [Estadio_ID], [Estadio_Nombre], [Estadio_Ciudad], [Estadio_Capacidad] FROM [Estadio] WHERE [Estadio_ID] = @Estadio_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Estadio_ID", SqlDbType.Int).Value = estadio_ID;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    public void Insertar(Estadio entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"INSERT INTO [Estadio] ([Estadio_ID], [Estadio_Nombre], [Estadio_Ciudad], [Estadio_Capacidad])
                         VALUES (@Estadio_ID, @Estadio_Nombre, @Estadio_Ciudad, @Estadio_Capacidad)";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Estadio_ID", SqlDbType.Int).Value = entity.Estadio_ID;
    command.Parameters.Add("@Estadio_Nombre", SqlDbType.VarChar, 50).Value = entity.Estadio_Nombre;
    command.Parameters.Add("@Estadio_Ciudad", SqlDbType.VarChar, 50).Value = entity.Estadio_Ciudad;
    command.Parameters.Add("@Estadio_Capacidad", SqlDbType.Int).Value = entity.Estadio_Capacidad;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Actualizar(Estadio entity)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = @"UPDATE [Estadio] SET
            [Estadio_Nombre] = @Estadio_Nombre,
            [Estadio_Ciudad] = @Estadio_Ciudad,
            [Estadio_Capacidad] = @Estadio_Capacidad
            WHERE [Estadio_ID] = @Estadio_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Estadio_ID", SqlDbType.Int).Value = entity.Estadio_ID;
    command.Parameters.Add("@Estadio_Nombre", SqlDbType.VarChar, 50).Value = entity.Estadio_Nombre;
    command.Parameters.Add("@Estadio_Ciudad", SqlDbType.VarChar, 50).Value = entity.Estadio_Ciudad;
    command.Parameters.Add("@Estadio_Capacidad", SqlDbType.Int).Value = entity.Estadio_Capacidad;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Eliminar(int estadio_ID)
    {
        using SqlConnection connection = _databaseConnection.GetConnection();
        string query = "DELETE FROM [Estadio] WHERE [Estadio_ID] = @Estadio_ID";
        using SqlCommand command = new(query, connection);
        command.Parameters.Add("@Estadio_ID", SqlDbType.Int).Value = estadio_ID;

        connection.Open();
        command.ExecuteNonQuery();
    }


    public void InsertarCompleto(Estadio estadio, IReadOnlyList<AreaRegistro> areas)
    {
        if (estadio == null) throw new ArgumentNullException(nameof(estadio));
        if (areas == null || areas.Count == 0) throw new ArgumentException("Debe agregar áreas.");
        using SqlConnection connection = _databaseConnection.GetConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            int SiguienteId(string tabla, string columna)
            {
                using SqlCommand cmd = new($"SELECT ISNULL(MAX([{columna}]), 0) + 1 FROM [{tabla}] WITH (TABLOCKX, HOLDLOCK)", connection, transaction);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            int estadioId = SiguienteId("Estadio", "Estadio_ID");
            int areaId = SiguienteId("Area", "Area_ID");
            int asientoId = SiguienteId("Asiento", "Asiento_ID");
            using (SqlCommand cmd = new("INSERT INTO Estadio (Estadio_ID, Estadio_Nombre, Estadio_Ciudad, Estadio_Capacidad) VALUES (@id,@nombre,@ciudad,@capacidad)", connection, transaction))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = estadioId;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = estadio.Estadio_Nombre;
                cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = estadio.Estadio_Ciudad;
                cmd.Parameters.Add("@capacidad", SqlDbType.Int).Value = estadio.Estadio_Capacidad;
                cmd.ExecuteNonQuery();
            }
            foreach (AreaRegistro area in areas)
            {
                using (SqlCommand cmd = new("INSERT INTO Area (Area_ID, Area_Nombre, Area_Capacidad, Estadio_ID) VALUES (@id,@nombre,@capacidad,@estadio)", connection, transaction))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = areaId;
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = area.Nombre;
                    cmd.Parameters.Add("@capacidad", SqlDbType.Int).Value = checked(area.Filas * area.Columnas);
                    cmd.Parameters.Add("@estadio", SqlDbType.Int).Value = estadioId;
                    cmd.ExecuteNonQuery();
                }
                using SqlCommand asiento = new("INSERT INTO Asiento (Asiento_ID, Asiento_Numero, Asiento_Fila, Area_ID) VALUES (@id,@numero,@fila,@area)", connection, transaction);
                var pId = asiento.Parameters.Add("@id", SqlDbType.Int);
                var pNumero = asiento.Parameters.Add("@numero", SqlDbType.Int);
                var pFila = asiento.Parameters.Add("@fila", SqlDbType.Int);
                asiento.Parameters.Add("@area", SqlDbType.Int).Value = areaId;
                for (int fila = 1; fila <= area.Filas; fila++)
                    for (int columna = 1; columna <= area.Columnas; columna++)
                    {
                        pId.Value = asientoId++;
                        pNumero.Value = columna;
                        pFila.Value = fila;
                        asiento.ExecuteNonQuery();
                    }
                areaId++;
            }
            transaction.Commit();
            estadio.Estadio_ID = estadioId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static Estadio Mapear(SqlDataReader reader)
    {
        return new Estadio
        {
            Estadio_ID = reader.GetInt32(reader.GetOrdinal("Estadio_ID")),
        Estadio_Nombre = reader.GetString(reader.GetOrdinal("Estadio_Nombre")),
        Estadio_Ciudad = reader.GetString(reader.GetOrdinal("Estadio_Ciudad")),
        Estadio_Capacidad = reader.GetInt32(reader.GetOrdinal("Estadio_Capacidad"))
        };
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Tabla(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Valor(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Ejecutar(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => new PIA_PA.Comun.Data.OperacionesMenusRepository(_databaseConnection).Transaccion(accion);
}
