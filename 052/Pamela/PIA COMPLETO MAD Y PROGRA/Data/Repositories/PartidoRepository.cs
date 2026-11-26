using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

/// <summary>Acceso a datos de partidos mediante SP_GestionPartidos.</summary>
public class PartidoRepository : RepositorioBase, IPartidoRepository
{
    private const string SpGestion = "SP_GestionPartidos";

    public PartidoRepository(DatabaseConnection database) : base(database)
    {
    }

    public DataTable Consultar()
    {
        return EjecutarTabla(SpGestion, ParametroTexto("@p_accion", "consultar", 25));
    }

    public DataTable Buscar(string texto)
    {
        return EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "buscar", 25),
            ParametroTexto("@p_texto", texto, 100));
    }

    public Partido? ObtenerPorId(int idPartido)
    {
        DataTable tabla = EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "consultar_id", 25),
            Parametro("@p_idPartido", SqlDbType.Int, idPartido));

        if (tabla.Rows.Count == 0)
        {
            return null;
        }

        DataRow fila = tabla.Rows[0];

        return new Partido
        {
            IdPartido = DatosHelper.Entero(fila, "IdPartido"),
            IdEstadio = DatosHelper.Entero(fila, "IdEstadio"),
            FechaHora = DatosHelper.Fecha(fila, "FechaHora"),
            EquipoLocal = DatosHelper.Texto(fila, "EquipoLocal"),
            EquipoVisitante = DatosHelper.Texto(fila, "EquipoVisitante"),
            Estado = DatosHelper.Texto(fila, "Estado")
        };
    }

    public int Agregar(Partido partido, int idUsuario)
    {
        object? idNuevo = EjecutarEscalar(SpGestion, CrearParametros("agregar", partido, idUsuario));

        return Convert.ToInt32(idNuevo);
    }

    public void Modificar(Partido partido, int idUsuario)
    {
        // Si el estado cambia a Cancelado, el trigger TR_Partido_CancelarVentas
        // cancela las ventas y las facturas del partido.
        EjecutarAccion(SpGestion, CrearParametros("modificar", partido, idUsuario));
    }

    public void Eliminar(int idPartido, int idUsuario)
    {
        EjecutarAccion(
            SpGestion,
            ParametroTexto("@p_accion", "eliminar", 25),
            Parametro("@p_idPartido", SqlDbType.Int, idPartido),
            Parametro("@p_idUsuario", SqlDbType.Int, idUsuario));
    }

    public DataTable ConsultarDisponibles(string? criterio, string? texto, DateTime? fecha)
    {
        return EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "consultar_disponibles", 25),
            ParametroTexto("@p_criterio", criterio, 20),
            ParametroTexto("@p_texto", texto, 100),
            Parametro("@p_fecha", SqlDbType.Date, fecha.HasValue ? (object)fecha.Value.Date : null));
    }

    public DataTable ConsultarZonas(int idPartido, bool soloDisponibles)
    {
        return EjecutarTabla(
            "SP_ZonasDisponiblesPartido",
            Parametro("@p_idPartido", SqlDbType.Int, idPartido),
            Parametro("@p_soloDisponibles", SqlDbType.Bit, soloDisponibles));
    }

    private static SqlParameter[] CrearParametros(string accion, Partido partido, int idUsuario)
    {
        return new SqlParameter[]
        {
            ParametroTexto("@p_accion", accion, 25),
            Parametro("@p_idPartido", SqlDbType.Int, partido.IdPartido > 0 ? (object)partido.IdPartido : null),
            Parametro("@p_idEstadio", SqlDbType.Int, partido.IdEstadio),
            Parametro("@p_fechaHora", SqlDbType.DateTime2, partido.FechaHora),
            ParametroTexto("@p_equipoLocal", partido.EquipoLocal, 100),
            ParametroTexto("@p_equipoVisitante", partido.EquipoVisitante, 100),
            ParametroTexto("@p_estado", partido.Estado, 20),
            Parametro("@p_idUsuario", SqlDbType.Int, idUsuario)
        };
    }
}
