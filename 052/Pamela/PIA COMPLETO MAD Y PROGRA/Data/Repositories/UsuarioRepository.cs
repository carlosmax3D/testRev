using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

/// <summary>
/// Acceso a datos de usuarios. Todas las operaciones se hacen con los
/// procedimientos almacenados SP_AutenticarUsuario y SP_GestionUsuarios.
/// </summary>
public class UsuarioRepository : RepositorioBase, IUsuarioRepository
{
    private const string SpGestion = "SP_GestionUsuarios";

    public UsuarioRepository(DatabaseConnection database) : base(database)
    {
    }

    public ResultadoInicioSesion ValidarAcceso(string correo, string contrasena)
    {
        DataTable tabla = EjecutarTabla(
            "SP_AutenticarUsuario",
            ParametroTexto("@p_correo", correo.Trim(), 50),
            ParametroTexto("@p_contrasena", contrasena, 255));

        // Sin renglones: el correo no existe o la contraseña no coincide
        if (tabla.Rows.Count == 0)
        {
            return new ResultadoInicioSesion
            {
                AccesoPermitido = false,
                UsuarioInactivo = false,
                Usuario = null
            };
        }

        Usuario usuario = CrearUsuario(tabla.Rows[0]);
        bool activo = usuario.EstadoUsuario == "Activo";

        return new ResultadoInicioSesion
        {
            AccesoPermitido = activo,
            UsuarioInactivo = !activo,
            Usuario = usuario
        };
    }

    public List<Usuario> ObtenerUsuarios()
    {
        DataTable tabla = EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "consultar", 20));

        return CrearLista(tabla);
    }

    public List<Usuario> BuscarUsuarios(string criterio, string texto)
    {
        // El criterio llega con el texto del ComboBox y se traduce a la clave del procedimiento
        string clave;

        if (criterio.StartsWith("Correo", StringComparison.OrdinalIgnoreCase))
        {
            clave = "correo";
        }
        else if (criterio.StartsWith("Nombre", StringComparison.OrdinalIgnoreCase))
        {
            clave = "nombre";
        }
        else if (criterio.StartsWith("Numero", StringComparison.OrdinalIgnoreCase)
            || criterio.StartsWith("Número", StringComparison.OrdinalIgnoreCase))
        {
            clave = "nomina";
        }
        else
        {
            throw new ArgumentException("El criterio de búsqueda no es válido.");
        }

        DataTable tabla = EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "buscar", 20),
            ParametroTexto("@p_criterio", clave, 20),
            ParametroTexto("@p_texto", texto.Trim(), 100));

        return CrearLista(tabla);
    }

    public Usuario ObtenerUsuarioPorId(int idUsuario)
    {
        DataTable tabla = EjecutarTabla(
            SpGestion,
            ParametroTexto("@p_accion", "consultar_id", 20),
            Parametro("@p_idUsuario", SqlDbType.Int, idUsuario));

        if (tabla.Rows.Count == 0)
        {
            throw new InvalidOperationException("No se encontró el usuario solicitado.");
        }

        return CrearUsuario(tabla.Rows[0]);
    }

    public bool ExisteNumeroNomina(string numeroNomina)
    {
        object? total = EjecutarEscalar(
            SpGestion,
            ParametroTexto("@p_accion", "existe_nomina", 20),
            ParametroTexto("@p_numeroNomina", numeroNomina.Trim(), 20));

        return Convert.ToInt32(total) > 0;
    }

    public bool ExisteCorreo(string correo)
    {
        object? total = EjecutarEscalar(
            SpGestion,
            ParametroTexto("@p_accion", "existe_correo", 20),
            ParametroTexto("@p_correo", correo.Trim(), 50));

        return Convert.ToInt32(total) > 0;
    }

    public int RegistrarUsuario(Usuario usuario, string claveAdmin, string cajaAsignada)
    {
        object? idNuevo = EjecutarEscalar(SpGestion, CrearParametros("agregar", usuario, claveAdmin, cajaAsignada));

        return Convert.ToInt32(idNuevo);
    }

    public void ActualizarUsuario(Usuario usuario, string claveAdmin, string cajaAsignada)
    {
        // Si la contraseña va vacía, el procedimiento conserva la actual.
        // Si cambia, el procedimiento valida que no sea igual a las últimas dos
        // y el trigger TR_Usuario_HistorialContrasena guarda la anterior.
        EjecutarAccion(SpGestion, CrearParametros("modificar", usuario, claveAdmin, cajaAsignada));
    }

    public void EliminarUsuario(int idUsuario)
    {
        // Baja lógica: el usuario queda Inactivo y se conserva su historial
        EjecutarAccion(
            SpGestion,
            ParametroTexto("@p_accion", "eliminar", 20),
            Parametro("@p_idUsuario", SqlDbType.Int, idUsuario));
    }

    private static SqlParameter[] CrearParametros(string accion, Usuario usuario, string claveAdmin, string cajaAsignada)
    {
        return new SqlParameter[]
        {
            ParametroTexto("@p_accion", accion, 20),
            Parametro("@p_idUsuario", SqlDbType.Int, usuario.IdUsuario > 0 ? (object)usuario.IdUsuario : null),
            ParametroTexto("@p_tipoUsuario", usuario.TipoUsuario, 20),
            ParametroTexto("@p_estadoUsuario", string.IsNullOrWhiteSpace(usuario.EstadoUsuario) ? "Activo" : usuario.EstadoUsuario, 20),
            Parametro("@p_idAdministradorReg", SqlDbType.Int, usuario.IdAdministradorReg),
            ParametroTexto("@p_correo", usuario.Correo, 50),
            ParametroTexto("@p_contrasena", string.IsNullOrEmpty(usuario.Contrasena) ? null : usuario.Contrasena, 255),
            ParametroTexto("@p_nombreCompleto", usuario.NombreCompleto, 150),
            ParametroTexto("@p_numeroNomina", usuario.NumeroNomina, 20),
            Parametro("@p_fechaNacimiento", SqlDbType.Date, usuario.FechaNacimiento.Date),
            ParametroTexto("@p_claveAdmin", claveAdmin, 50),
            ParametroTexto("@p_cajaOperativo", cajaAsignada, 50)
        };
    }

    private static List<Usuario> CrearLista(DataTable tabla)
    {
        List<Usuario> usuarios = new List<Usuario>();

        foreach (DataRow fila in tabla.Rows)
        {
            usuarios.Add(CrearUsuario(fila));
        }

        return usuarios;
    }

    private static Usuario CrearUsuario(DataRow fila)
    {
        DataColumnCollection columnas = fila.Table.Columns;

        Usuario usuario = new Usuario
        {
            IdUsuario = DatosHelper.Entero(fila, "IdUsuario"),
            Correo = DatosHelper.Texto(fila, "Correo"),
            NombreCompleto = DatosHelper.Texto(fila, "NombreCompleto"),
            TipoUsuario = DatosHelper.Texto(fila, "TipoUsuario"),
            EstadoUsuario = DatosHelper.Texto(fila, "EstadoUsuario"),
            NumeroNomina = DatosHelper.Texto(fila, "NumeroNomina")
        };

        // El inicio de sesión devuelve menos columnas que las consultas de gestión
        if (columnas.Contains("FechaNacimiento"))
        {
            usuario.FechaNacimiento = DatosHelper.Fecha(fila, "FechaNacimiento");
            usuario.FechaHoraRegistro = DatosHelper.Fecha(fila, "FechaHoraRegistro");
            usuario.IdAdministradorReg = fila.IsNull("IdAdministradorReg")
                ? (int?)null
                : DatosHelper.Entero(fila, "IdAdministradorReg");
            usuario.ClaveAdmin = DatosHelper.Texto(fila, "ClaveAdmin");
            usuario.CajaAsignada = DatosHelper.Texto(fila, "CajaAsignada");
        }

        return usuario;
    }
}
