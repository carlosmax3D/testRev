using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BoleteraPrototipo
{
    // Clase base de todos los repositorios. Aquí está lo común:
    // llamar procedimientos almacenados con la clase Conexion.
    public abstract class Repositorio
    {
        protected static DataTable Tabla(string sp, params SqlParameter[] ps)
        {
            return Conexion.Consultar(sp, ps);
        }

        protected static DataSet Varios(string sp, params SqlParameter[] ps)
        {
            return Conexion.ConsultarVarios(sp, ps);
        }

        protected static void Ejecutar(string sp, params SqlParameter[] ps)
        {
            Conexion.Ejecutar(sp, ps);
        }

        protected static SqlParameter P(string nombre, object valor)
        {
            return Conexion.P(nombre, valor);
        }
    }

    // ------------------------------------------------------------ Usuarios
    public class UsuarioRepositorio : Repositorio
    {
        // Regresa el usuario si el correo y la contraseña son correctos; si no, null
        public Usuario Login(string correo, string contrasenaTexto)
        {
            DataTable dt = Tabla("sp_Login", P("@correo", correo), P("@contrasena", Seguridad.Hash(contrasenaTexto)));
            if (dt.Rows.Count == 0) return null;
            DataRow r = dt.Rows[0];
            Usuario u = new Usuario();
            u.Id = Convert.ToInt32(r["id_usuario"]);
            u.Correo = Convert.ToString(r["correo_electronico"]);
            u.NombreCompleto = Convert.ToString(r["nombre_completo"]);
            u.Rol = Convert.ToString(r["rol"]);
            return u;
        }

        public DataTable Listar()
        {
            return Tabla("sp_Usuario_Listar");
        }

        public void Insertar(Usuario u, string registradoPor)
        {
            Ejecutar("sp_Usuario_Insertar",
                P("@correo", u.Correo.Trim()), P("@contrasena", Seguridad.Hash(u.Contrasena)),
                P("@nombre", u.NombreCompleto.Trim()), P("@nomina", u.Nomina.Trim()), P("@rol", u.Rol),
                P("@nacimiento", u.FechaNacimiento.Date), P("@registro", registradoPor));
        }

        public void Actualizar(Usuario u)
        {
            Ejecutar("sp_Usuario_Actualizar",
                P("@id_usuario", u.Id), P("@correo", u.Correo.Trim()), P("@nombre", u.NombreCompleto.Trim()),
                P("@nomina", u.Nomina.Trim()), P("@rol", u.Rol), P("@nacimiento", u.FechaNacimiento.Date));
        }

        public void CambiarContrasena(int idUsuario, string contrasenaTexto)
        {
            Ejecutar("sp_Usuario_CambiarContrasena", P("@id_usuario", idUsuario), P("@contrasena", Seguridad.Hash(contrasenaTexto)));
        }

        public void Eliminar(int idUsuario, int idUsuarioSesion)
        {
            Ejecutar("sp_Usuario_Eliminar", P("@id_usuario", idUsuario), P("@id_usuario_sesion", idUsuarioSesion));
        }
    }

    // ------------------------------------------------------------ Clientes
    public class ClienteRepositorio : Repositorio
    {
        public DataTable Listar() { return Tabla("sp_Cliente_Listar"); }

        public DataTable Buscar(string texto) { return Tabla("sp_Cliente_Buscar", P("@texto", texto ?? "")); }

        static string Vacio(string s) { return string.IsNullOrWhiteSpace(s) ? null : s.Trim(); }

        public void Insertar(Cliente c)
        {
            Ejecutar("sp_Cliente_Insertar",
                P("@nombre", c.NombreCompleto.Trim()), P("@domicilio", c.Domicilio.Trim()), P("@rfc", c.Rfc.Trim().ToUpper()),
                P("@correo", c.Correo.Trim()), P("@tel_casa", Vacio(c.TelCasa)), P("@tel_celular", c.TelCelular.Trim()),
                P("@nacimiento", c.FechaNacimiento.Date), P("@usuario", Sesion.Correo), P("@id_usuario", Sesion.IdUsuario));
        }

        public void Actualizar(Cliente c)
        {
            Ejecutar("sp_Cliente_Actualizar",
                P("@id_cliente", c.Id), P("@nombre", c.NombreCompleto.Trim()), P("@domicilio", c.Domicilio.Trim()),
                P("@rfc", c.Rfc.Trim().ToUpper()), P("@correo", c.Correo.Trim()), P("@tel_casa", Vacio(c.TelCasa)),
                P("@tel_celular", c.TelCelular.Trim()), P("@nacimiento", c.FechaNacimiento.Date), P("@usuario", Sesion.Correo));
        }

        public void Eliminar(int idCliente) { Ejecutar("sp_Cliente_Eliminar", P("@id_cliente", idCliente)); }

        // orden: "COMPRA" (fecha de compra, de la más lejana a la más reciente) o "PARTIDO" (pendientes primero)
        public DataTable Historial(int idCliente, string orden)
        {
            return Tabla("sp_Cliente_Historial", P("@id_cliente", idCliente), P("@orden", orden));
        }
    }

    // ------------------------------------------------------------ Estadios
    public class EstadioRepositorio : Repositorio
    {
        public DataTable Listar() { return Tabla("sp_Estadio_Listar"); }
        public DataTable Combo() { return Tabla("sp_Estadio_Combo"); }

        public byte[] ObtenerMapa(int idEstadio)
        {
            object o = Conexion.Escalar("sp_Estadio_ObtenerMapa", P("@id_estadio", idEstadio));
            return (o == null || o == DBNull.Value) ? null : (byte[])o;
        }

        public void Insertar(string nombre, string ciudad, int capacidad, byte[] mapa)
        {
            Ejecutar("sp_Estadio_Insertar", P("@nombre", nombre), P("@ciudad", ciudad), P("@capacidad", capacidad),
                Conexion.PBinario("@mapa", mapa), P("@usuario", Sesion.Correo));
        }

        public void Actualizar(int id, string nombre, string ciudad, int capacidad, bool cambiarMapa, byte[] mapa)
        {
            Ejecutar("sp_Estadio_Actualizar", P("@id_estadio", id), P("@nombre", nombre), P("@ciudad", ciudad),
                P("@capacidad", capacidad), P("@cambiar_mapa", cambiarMapa), Conexion.PBinario("@mapa", mapa));
        }

        public void Eliminar(int id) { Ejecutar("sp_Estadio_Eliminar", P("@id_estadio", id)); }
    }

    // ------------------------------------------------------------ Zonas
    public class ZonaRepositorio : Repositorio
    {
        public DataTable Listar() { return Tabla("sp_Zona_Listar"); }

        // todas las zonas (idPartido = null) o solo las del estadio de un partido
        public DataTable Combo(int? idPartido) { return Tabla("sp_Zona_Combo", P("@id_partido", idPartido)); }

        // zonas del partido que todavía tienen lugares
        public DataTable PorPartido(int idPartido) { return Tabla("sp_Zona_PorPartido", P("@id_partido", idPartido)); }

        public void Insertar(int idEstadio, string nombre, decimal precio, int cupo)
        {
            Ejecutar("sp_Zona_Insertar", P("@id_estadio", idEstadio), P("@nombre", nombre), P("@precio", precio),
                P("@cupo", cupo), P("@usuario", Sesion.Correo));
        }

        public void Actualizar(int id, int idEstadio, string nombre, decimal precio, int cupo)
        {
            Ejecutar("sp_Zona_Actualizar", P("@id_zona", id), P("@id_estadio", idEstadio), P("@nombre", nombre),
                P("@precio", precio), P("@cupo", cupo));
        }

        public void Eliminar(int id) { Ejecutar("sp_Zona_Eliminar", P("@id_zona", id)); }
    }

    // ------------------------------------------------------------ Partidos
    public class PartidoRepositorio : Repositorio
    {
        public DataTable Listar() { return Tabla("sp_Partido_Listar"); }
        public DataTable Combo() { return Tabla("sp_Partido_Combo"); }

        public DataTable Disponibles(DateTime? fecha, int? idEstadio, string equipo)
        {
            return Tabla("sp_Partido_Disponibles", P("@fecha", fecha), P("@id_estadio", idEstadio),
                P("@equipo", string.IsNullOrWhiteSpace(equipo) ? null : equipo.Trim()));
        }

        public void Insertar(int idEstadio, DateTime fechaHora, string local, string visitante, string estado)
        {
            Ejecutar("sp_Partido_Insertar", P("@id_estadio", idEstadio), P("@fecha_hora", fechaHora), P("@local", local),
                P("@visitante", visitante), P("@estado", estado), P("@usuario", Sesion.Correo));
        }

        public void Actualizar(int id, int idEstadio, DateTime fechaHora, string local, string visitante, string estado)
        {
            Ejecutar("sp_Partido_Actualizar", P("@id_partido", id), P("@id_estadio", idEstadio), P("@fecha_hora", fechaHora),
                P("@local", local), P("@visitante", visitante), P("@estado", estado));
        }

        public void Eliminar(int id) { Ejecutar("sp_Partido_Eliminar", P("@id_partido", id)); }

        public void Cancelar(int id, string motivo)
        {
            Ejecutar("sp_Partido_Cancelar", P("@id_partido", id), P("@id_usuario", Sesion.IdUsuario), P("@motivo", motivo));
        }
    }

    // ------------------------------------------------------------ Ventas
    public class ResultadoVenta
    {
        public int IdVenta;
        public Guid Folio;
    }

    public class VentaRepositorio : Repositorio
    {
        public bool AsientoLibre(int idPartido, int idZona, string fila, int numero)
        {
            object o = Conexion.Escalar("sp_Boleto_AsientoLibre", P("@id_partido", idPartido), P("@id_zona", idZona),
                P("@fila", fila), P("@numero", numero));
            return Convert.ToInt32(o) == 1;
        }

        // boletos: columnas id_zona (int), fila (string), numero_asiento (int)
        public ResultadoVenta Registrar(int idCliente, int idPartido, string metodoPago, DataTable boletos)
        {
            SqlParameter idVenta = Conexion.PSalida("@id_venta", SqlDbType.Int);
            SqlParameter folio = Conexion.PSalida("@folio", SqlDbType.UniqueIdentifier);
            Conexion.Ejecutar("sp_Venta_Registrar",
                P("@id_usuario", Sesion.IdUsuario), P("@id_cliente", idCliente), P("@id_partido", idPartido),
                P("@metodo_pago", metodoPago), Conexion.PTabla("@boletos", boletos, "dbo.TipoBoletoVenta"),
                idVenta, folio);
            ResultadoVenta r = new ResultadoVenta();
            r.IdVenta = Convert.ToInt32(idVenta.Value);
            r.Folio = (Guid)folio.Value;
            return r;
        }

        // lista de ventas; con folio filtra por (una parte del) folio
        public DataTable Listar(string folio)
        {
            return Tabla("sp_Venta_Listar", P("@folio", string.IsNullOrWhiteSpace(folio) ? null : folio.Trim()));
        }

        public void Cancelar(Guid folio, string motivo)
        {
            Ejecutar("sp_Venta_Cancelar", P("@folio", folio), P("@id_usuario", Sesion.IdUsuario), P("@motivo", motivo));
        }

        // Tabla 0 = encabezado de la factura, tabla 1 = detalle
        public DataSet Factura(int idVenta) { return Varios("sp_Factura_Obtener", P("@id_venta", idVenta)); }
    }

    // ------------------------------------------------------------ Reportes
    public class ReporteRepositorio : Repositorio
    {
        // Tabla 0 = detalle, 1 = totales, 2 = resumen por partido
        public DataSet Ventas(DateTime desde, DateTime hasta, int? idEstadio, int? idPartido)
        {
            return Varios("sp_Reporte_Ventas", P("@fecha_ini", desde.Date), P("@fecha_fin", hasta.Date),
                P("@id_estadio", idEstadio), P("@id_partido", idPartido));
        }

        // Tabla 0 = detalle, 1 = total de boletos vendidos
        public DataSet Ocupacion(int? idPartido, int? idZona, bool soloDisponibles)
        {
            return Varios("sp_Reporte_Ocupacion", P("@id_partido", idPartido), P("@id_zona", idZona),
                P("@solo_disponibles", soloDisponibles));
        }
    }
}
