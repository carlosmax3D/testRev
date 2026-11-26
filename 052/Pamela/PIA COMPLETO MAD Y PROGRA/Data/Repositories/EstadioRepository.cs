using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

/// <summary>Acceso a datos de estadios y zonas mediante SP_GestionEstadios.</summary>
public class EstadioRepository : RepositorioBase, IEstadioRepository
{
    private const string SpGestion = "SP_GestionEstadios";

    public EstadioRepository(DatabaseConnection database) : base(database)
    {
    }

    public DataTable Consultar()
    {
        return EjecutarTabla(SpGestion, ParametroTexto("@p_accion", "consultar", 20));
    }

    public DataTable Buscar(string texto)
    {
        return EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "buscar", 20),
            ParametroTexto("@p_nombre", texto, 150));
    }

    public Estadio? ObtenerPorId(int idEstadio)
    {
        DataTable tabla = EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "consultar_id", 20),
            Parametro("@p_idEstadio", SqlDbType.Int, idEstadio));

        if (tabla.Rows.Count == 0)
        {
            return null;
        }

        DataRow fila = tabla.Rows[0];

        return new Estadio
        {
            IdEstadio = DatosHelper.Entero(fila, "IdEstadio"),
            Nombre = DatosHelper.Texto(fila, "Nombre"),
            Ciudad = DatosHelper.Texto(fila, "Ciudad"),
            CapacidadTotal = DatosHelper.Entero(fila, "CapacidadTotal"),
            MapaEstadio = DatosHelper.Bytes(fila, "MapaEstadio")
        };
    }

    public List<Zona> ConsultarZonas(int idEstadio)
    {
        DataTable tabla = EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "consultar_zonas", 20),
            Parametro("@p_idEstadio", SqlDbType.Int, idEstadio));

        List<Zona> zonas = new List<Zona>();

        foreach (DataRow fila in tabla.Rows)
        {
            zonas.Add(new Zona
            {
                IdZona = DatosHelper.Entero(fila, "IdZona"),
                IdEstadio = DatosHelper.Entero(fila, "IdEstadio"),
                NombreZona = DatosHelper.Texto(fila, "NombreZona"),
                Asientos = DatosHelper.Entero(fila, "Asientos"),
                PrecioBase = DatosHelper.Numero(fila, "PrecioBase")
            });
        }

        return zonas;
    }

    /// <summary>
    /// Guarda el estadio junto con sus zonas en una sola transacción:
    /// o se guarda todo, o no se guarda nada.
    /// </summary>
    public int Guardar(Estadio estadio, List<Zona> zonasNuevas, List<int> zonasEliminadas, int idUsuario)
    {
        using SqlConnection connection = CrearConexion();

        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            int idEstadio = estadio.IdEstadio;

            if (idEstadio == 0)
            {
                object? idNuevo = EjecutarEscalarEnTransaccion(
                    connection,
                    transaction,
                    SpGestion,
                    ParametroTexto("@p_accion", "agregar_estadio", 20),
                    Parametro("@p_idUsuarioRegistro", SqlDbType.Int, idUsuario),
                    ParametroTexto("@p_nombre", estadio.Nombre, 150),
                    ParametroTexto("@p_ciudad", estadio.Ciudad, 100),
                    Parametro("@p_capacidadTotal", SqlDbType.Int, estadio.CapacidadTotal),
                    ParametroBinario("@p_mapaEstadio", estadio.MapaEstadio));

                idEstadio = Convert.ToInt32(idNuevo);
            }
            else
            {
                // Primero se quitan las zonas eliminadas para que la validación
                // de capacidad se haga con las zonas que realmente quedan.
                foreach (int idZona in zonasEliminadas)
                {
                    EjecutarAccionEnTransaccion(
                        connection,
                        transaction,
                        SpGestion,
                        ParametroTexto("@p_accion", "eliminar_zona", 20),
                        Parametro("@p_idZona", SqlDbType.Int, idZona));
                }

                EjecutarAccionEnTransaccion(
                    connection,
                    transaction,
                    SpGestion,
                    ParametroTexto("@p_accion", "modificar_estadio", 20),
                    Parametro("@p_idEstadio", SqlDbType.Int, idEstadio),
                    ParametroTexto("@p_nombre", estadio.Nombre, 150),
                    ParametroTexto("@p_ciudad", estadio.Ciudad, 100),
                    Parametro("@p_capacidadTotal", SqlDbType.Int, estadio.CapacidadTotal),
                    ParametroBinario("@p_mapaEstadio", estadio.MapaEstadio));
            }

            foreach (Zona zona in zonasNuevas)
            {
                EjecutarAccionEnTransaccion(
                    connection,
                    transaction,
                    SpGestion,
                    ParametroTexto("@p_accion", "agregar_zona", 20),
                    Parametro("@p_idEstadio", SqlDbType.Int, idEstadio),
                    ParametroTexto("@p_nombreZona", zona.NombreZona, 100),
                    Parametro("@p_asientosZona", SqlDbType.Int, zona.Asientos),
                    ParametroDecimal("@p_precioBaseZona", zona.PrecioBase));
            }

            transaction.Commit();

            return idEstadio;
        }
        catch
        {
            try
            {
                transaction.Rollback();
            }
            catch
            {
                // La transacción ya fue deshecha por SQL Server
            }

            throw;
        }
    }

    public void Eliminar(int idEstadio)
    {
        EjecutarAccion(
            SpGestion,
            ParametroTexto("@p_accion", "eliminar_estadio", 20),
            Parametro("@p_idEstadio", SqlDbType.Int, idEstadio));
    }
}
