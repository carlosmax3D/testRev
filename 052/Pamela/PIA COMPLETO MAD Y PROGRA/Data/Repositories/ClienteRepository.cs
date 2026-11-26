using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

/// <summary>Acceso a datos de clientes mediante SP_GestionClientes.</summary>
public class ClienteRepository : RepositorioBase, IClienteRepository
{
    private const string SpGestion = "SP_GestionClientes";

    public ClienteRepository(DatabaseConnection database) : base(database)
    {
    }

    public DataTable Consultar()
    {
        return EjecutarTabla(SpGestion, ParametroTexto("@p_accion", "consultar", 20));
    }

    public DataTable Buscar(string? criterio, string texto)
    {
        return EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "buscar", 20),
            ParametroTexto("@p_criterio", criterio, 20),
            ParametroTexto("@p_texto", texto, 100));
    }

    public Cliente? ObtenerPorId(int idCliente)
    {
        DataTable tabla = EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "consultar_id", 20),
            Parametro("@p_idCliente", SqlDbType.Int, idCliente));

        if (tabla.Rows.Count == 0)
        {
            return null;
        }

        DataRow fila = tabla.Rows[0];

        return new Cliente
        {
            IdCliente = DatosHelper.Entero(fila, "IdCliente"),
            Nombre = DatosHelper.Texto(fila, "Nombre"),
            Apellido = DatosHelper.Texto(fila, "Apellido"),
            DomicilioCompleto = DatosHelper.Texto(fila, "DomicilioCompleto"),
            RFC = DatosHelper.Texto(fila, "RFC"),
            Correo = DatosHelper.Texto(fila, "Correo"),
            FechaNacimiento = DatosHelper.Fecha(fila, "FechaNacimiento"),
            TelefonoCasa = DatosHelper.Texto(fila, "TelefonoCasa"),
            TelefonoCelular = DatosHelper.Texto(fila, "TelefonoCelular")
        };
    }

    public int Agregar(Cliente cliente, int idUsuario)
    {
        object? idNuevo = EjecutarEscalar(SpGestion, CrearParametros("agregar", cliente, idUsuario));

        return Convert.ToInt32(idNuevo);
    }

    public void Modificar(Cliente cliente, int idUsuario)
    {
        // El trigger TR_Auditoria_Cliente registra el cambio con usuario, fecha y hora
        EjecutarAccion(SpGestion, CrearParametros("modificar", cliente, idUsuario));
    }

    public void Eliminar(int idCliente, int idUsuario)
    {
        EjecutarAccion(
            SpGestion,
            ParametroTexto("@p_accion", "eliminar", 20),
            Parametro("@p_idCliente", SqlDbType.Int, idCliente),
            Parametro("@p_idUsuario", SqlDbType.Int, idUsuario));
    }

    public DataTable ConsultarAuditoria(int idCliente)
    {
        return EjecutarTabla(
            "SP_ConsultarAuditoriaCliente",
            Parametro("@p_idCliente", SqlDbType.Int, idCliente));
    }

    private static SqlParameter[] CrearParametros(string accion, Cliente cliente, int idUsuario)
    {
        return new SqlParameter[]
        {
            ParametroTexto("@p_accion", accion, 20),
            Parametro("@p_idCliente", SqlDbType.Int, cliente.IdCliente > 0 ? (object)cliente.IdCliente : null),
            Parametro("@p_idUsuario", SqlDbType.Int, idUsuario),
            ParametroTexto("@p_nombre", cliente.Nombre, 80),
            ParametroTexto("@p_apellido", cliente.Apellido, 100),
            ParametroTexto("@p_domicilioCompleto", cliente.DomicilioCompleto, 250),
            ParametroTexto("@p_rfc", cliente.RFC, 13),
            ParametroTexto("@p_correo", cliente.Correo, 100),
            Parametro("@p_fechaNacimiento", SqlDbType.Date, cliente.FechaNacimiento.Date),
            ParametroTexto("@p_telCasa", cliente.TelefonoCasa, 15),
            ParametroTexto("@p_telCelular", cliente.TelefonoCelular, 15)
        };
    }
}
