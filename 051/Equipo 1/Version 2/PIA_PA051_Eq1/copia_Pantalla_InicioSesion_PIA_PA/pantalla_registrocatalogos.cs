using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Pantalla_RegistroPartido_PIA_PA;
using Pantalla_RegistroEstadio_PIA_PA;
using Pantalla_RegistroZona_PIA_PA;
using Pantalla_RegistroAsientos_PIA_PA;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_RegistroCatalogos_PIA_PA
{
    // Ventana encargada del registro del catálogo.
    public partial class pantalla_registrocatalogos : Form
    {
        // Almacena temporalmente los datos del estadio,
        // sus zonas, asientos y partidos.
        private RegistroCatalogoTemporal registroActual =
            new RegistroCatalogoTemporal();

        // Nómina del administrador que realiza el registro.
        private int NominaAdmin;

        // Constructor que recibe la nómina del administrador.
        public pantalla_registrocatalogos(int nominaAdmin)
        {
            InitializeComponent();

            NominaAdmin = nominaAdmin;
        }

        // Abrir el formulario de estadio.
        private void boton1_estadio_Click(object sender, EventArgs e)
        {
            if (registroActual.TieneEstadio)
            {
                MessageBox.Show(
                    "Ya capturaste los datos del estadio.",
                    "Estadio capturado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using (pantalla_registroestadio estadio =
                   new pantalla_registroestadio(registroActual))
            {
                estadio.ShowDialog(this);
            }
        }

        // Abrir el formulario de partidos.
        private void boton2_partido_Click(object sender, EventArgs e)
        {
            using (pantalla_registroPartido partido =
                   new pantalla_registroPartido(registroActual))
            {
                partido.ShowDialog(this);
            }
        }

        // Abrir el formulario de zonas.
        private void boton3_zona_Click(object sender, EventArgs e)
        {
            if (!registroActual.TieneEstadio)
            {
                MessageBox.Show(
                    "Primero captura los datos del estadio.",
                    "Falta el estadio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            using (pantalla_registroZona zona =
                   new pantalla_registroZona(registroActual))
            {
                zona.ShowDialog(this);
            }
        }

        // Regresar al menú anterior.
        private void boton4_regresarregistrocatalogo_Click(
            object sender, EventArgs e)
        {
            this.Close();
        }

        // Evento de carga.
        private void pantalla_registrocatalogos_Load(
            object sender, EventArgs e)
        {
        }

        // Botón para guardar el catálogo.

        private void boton5_guardaregistroCatalogos_Click(object sender, EventArgs e)
        {
            if (NominaAdmin < 0)
            {
                MessageBox.Show(
                    "La nómina del administrador no es válida.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Validar los partidos solamente si se registraron.
            if (!ValidarPartidosTemporales())
            {
                return;
            }

            // Si se registró un estadio nuevo, se guarda el catálogo
            // con sus zonas y asientos, aunque no existan partidos.
            if (registroActual.TieneEstadio)
            {
                if (registroActual.Zonas == null ||
                    registroActual.Zonas.Count == 0)
                {
                    MessageBox.Show(
                        "Debe registrar al menos una zona para el estadio.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Los partidos de un estadio nuevo se asignan
                // automáticamente al estadio que se está registrando.
                if (registroActual.Partidos != null &&
                    registroActual.Partidos.Any(p => p.IDEstadioSeleccionado.HasValue))
                {
                    MessageBox.Show(
                        "Los partidos de un estadio nuevo no deben tener otro estadio seleccionado.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Se mantienen todas las validaciones de zonas,
                // filas, asientos y capacidad del estadio.
                if (!ValidarCapacidadEstadio())
                {
                    return;
                }

                GuardarCatalogoCompleto();
                return;
            }

            // Si no se registró un estadio nuevo, se requiere
            // registrar partidos asociados a estadios existentes.
            if (registroActual.Partidos == null ||
                registroActual.Partidos.Count == 0)
            {
                MessageBox.Show(
                    "Debe registrar un estadio nuevo o agregar partidos de estadios existentes.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Cada partido debe tener un estadio existente seleccionado.
            if (registroActual.Partidos.Any(p => !p.IDEstadioSeleccionado.HasValue))
            {
                MessageBox.Show(
                    "Todos los partidos deben tener un estadio seleccionado.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            GuardarCatalogoPartidosExistentes();
        }


        // Validar las zonas, filas y asientos.
        private bool ValidarCapacidadEstadio()
        {
            if (registroActual.Capacidad <= 0)
            {
                MessageBox.Show(
                    "La capacidad del estadio debe ser mayor que cero.");

                return false;
            }

            int sumaCuposZonas = 0;

            // Evitar nombres de zonas repetidos.
            var nombresZonas = registroActual.Zonas
                .Select(z => z.Nombre?.Trim() ?? "")
                .ToList();

            if (nombresZonas.Distinct(
                StringComparer.OrdinalIgnoreCase).Count() != nombresZonas.Count)
            {
                MessageBox.Show(
                    "No puedes registrar dos zonas con el mismo nombre.");

                return false;
            }

            foreach (ZonaTemporal zona in registroActual.Zonas)
            {
                if (string.IsNullOrWhiteSpace(zona.Nombre))
                {
                    MessageBox.Show("Existe una zona sin nombre.");
                    return false;
                }

                if (zona.Precio < 0 ||
                    zona.Precio != Math.Truncate(zona.Precio))
                {
                    MessageBox.Show(
                        $"El precio de la zona '{zona.Nombre}' debe ser entero.");

                    return false;
                }

                if (zona.Precio > int.MaxValue)
                {
                    MessageBox.Show(
                        $"El precio de la zona '{zona.Nombre}' es demasiado grande.");

                    return false;
                }

                if (zona.Cupo <= 0)
                {
                    MessageBox.Show(
                        $"El cupo de la zona '{zona.Nombre}' debe ser positivo.");

                    return false;
                }

                if (zona.Filas == null || zona.Filas.Count == 0)
                {
                    MessageBox.Show(
                        $"La zona '{zona.Nombre}' no tiene filas registradas.");

                    return false;
                }

                var filasRegistradas =
                    new System.Collections.Generic.List<string>();

                foreach (FilaTemporal fila in zona.Filas)
                {
                    string nombreFila =
                        fila.Nombre?.Trim().ToUpperInvariant();

                    if (string.IsNullOrWhiteSpace(nombreFila) ||
                        !System.Text.RegularExpressions.Regex.IsMatch(
                            nombreFila, @"^[A-Z0-9]{1,10}$"))
                    {
                        MessageBox.Show(
                            $"La fila '{fila.Nombre}' tiene un identificador incorrecto.");

                        return false;
                    }

                    if (fila.CantidadAsientos <= 0)
                    {
                        MessageBox.Show(
                            $"La fila {nombreFila} debe tener asientos.");

                        return false;
                    }

                    if (filasRegistradas.Contains(
                        nombreFila,
                        StringComparer.OrdinalIgnoreCase))
                    {
                        MessageBox.Show(
                            $"La fila {nombreFila} está repetida en la zona '{zona.Nombre}'.");

                        return false;
                    }

                    filasRegistradas.Add(nombreFila);
                }

                int totalAsientosZona =
                    zona.Filas.Sum(f => f.CantidadAsientos);

                if (totalAsientosZona != zona.Cupo)
                {
                    MessageBox.Show(
                        $"La zona '{zona.Nombre}' está incompleta.\n\n" +
                        $"Cupo: {zona.Cupo}\n" +
                        $"Asientos registrados: {totalAsientosZona}\n" +
                        $"Faltantes: {Math.Max(0, zona.Cupo - totalAsientosZona)}",
                        "Zona incompleta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                sumaCuposZonas += zona.Cupo;
            }

            if (sumaCuposZonas != registroActual.Capacidad)
            {
                MessageBox.Show(
                    "La suma de los cupos de las zonas no coincide con " +
                    "la capacidad total del estadio.\n\n" +
                    $"Capacidad del estadio: {registroActual.Capacidad}\n" +
                    $"Suma de cupos: {sumaCuposZonas}",
                    "Capacidad incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        // Validar los partidos antes de enviarlos.
        private bool ValidarPartidosTemporales()
        {
            if (registroActual.Partidos == null)
                return true;

            foreach (PartidoTemporal partido in registroActual.Partidos)
            {
                if (partido == null)
                {
                    MessageBox.Show(
                        "Existe un partido vacío en el registro temporal.");

                    return false;
                }

                if (string.IsNullOrWhiteSpace(partido.EquipoLocal) ||
                    string.IsNullOrWhiteSpace(partido.EquipoVisitante) ||
                    string.IsNullOrWhiteSpace(partido.Estado))
                {
                    MessageBox.Show(
                        "Existe un partido con campos obligatorios vacíos.");

                    return false;
                }

                if (partido.EquipoLocal.Trim().Length > 50 ||
                    partido.EquipoVisitante.Trim().Length > 50 ||
                    partido.Estado.Trim().Length > 50)
                {
                    MessageBox.Show(
                        "Los nombres de los equipos y el estado no pueden " +
                        "superar los 50 caracteres.");

                    return false;
                }

                if (string.Equals(
                    partido.EquipoLocal.Trim(),
                    partido.EquipoVisitante.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "Un partido no puede tener el mismo equipo local " +
                        "y visitante.");

                    return false;
                }

                if (partido.Hora < TimeSpan.Zero ||
                    partido.Hora >= TimeSpan.FromDays(1))
                {
                    MessageBox.Show(
                        "Existe un partido con una hora incorrecta.");

                    return false;
                }
            }

            return true;
        }

        // Construye la tabla de datos de las zonas.
        // Se utiliza para enviar la información al procedimiento almacenado.
        private DataTable CrearTablaZonas()
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("Nombre_Zona", typeof(string));
            tabla.Columns.Add("Precio_Base", typeof(int));
            tabla.Columns.Add("Cupo", typeof(int));

            foreach (ZonaTemporal zona in registroActual.Zonas)
            {
                tabla.Rows.Add(
                    zona.Nombre.Trim().ToUpperInvariant(),
                    Convert.ToInt32(zona.Precio),
                    zona.Cupo
                );
            }

            return tabla;
        }

        // Construye la tabla de filas y cantidades de asientos.
        private DataTable CrearTablaFilas()
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("Nombre_Zona", typeof(string));
            tabla.Columns.Add("Nombre_Fila", typeof(string));
            tabla.Columns.Add("Cantidad_Asientos", typeof(int));

            foreach (ZonaTemporal zona in registroActual.Zonas)
            {
                foreach (FilaTemporal fila in zona.Filas)
                {
                    tabla.Rows.Add(
                        zona.Nombre.Trim().ToUpperInvariant(),
                        fila.Nombre.Trim().ToUpperInvariant(),
                        fila.CantidadAsientos
                    );
                }
            }

            return tabla;
        }

        // Construye la tabla de partidos para un estadio nuevo.
        private DataTable CrearTablaPartidos()
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("Equipo_Local", typeof(string));
            tabla.Columns.Add("Estado", typeof(string));
            tabla.Columns.Add("Equipo_Visitante", typeof(string));
            tabla.Columns.Add("Fecha", typeof(DateTime));
            tabla.Columns.Add("Hora", typeof(TimeSpan));

            if (registroActual.Partidos != null)
            {
                foreach (PartidoTemporal partido in registroActual.Partidos)
                {
                    tabla.Rows.Add(
                        partido.EquipoLocal.Trim().ToUpperInvariant(),
                        partido.Estado.Trim().ToUpperInvariant(),
                        partido.EquipoVisitante.Trim().ToUpperInvariant(),
                        partido.Fecha.Date,
                        partido.Hora
                    );
                }
            }

            return tabla;
        }

        // Construye la tabla de partidos asociados a estadios existentes.
        private DataTable CrearTablaPartidosExistentes()
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("Equipo_Local", typeof(string));
            tabla.Columns.Add("Estado", typeof(string));
            tabla.Columns.Add("Equipo_Visitante", typeof(string));
            tabla.Columns.Add("Fecha", typeof(DateTime));
            tabla.Columns.Add("Hora", typeof(TimeSpan));
            tabla.Columns.Add("ID_Estadio", typeof(int));

            foreach (PartidoTemporal partido in registroActual.Partidos)
            {
                tabla.Rows.Add(
                    partido.EquipoLocal.Trim().ToUpperInvariant(),
                    partido.Estado.Trim().ToUpperInvariant(),
                    partido.EquipoVisitante.Trim().ToUpperInvariant(),
                    partido.Fecha.Date,
                    partido.Hora,
                    partido.IDEstadioSeleccionado.Value
                );
            }

            return tabla;
        }

        // Guardar el catálogo completo mediante el procedimiento almacenado.
        // Este método no contiene consultas ni instrucciones SQL.
        private void GuardarCatalogoCompleto()
        {
            try
            {
                // Obtener la conexión centralizada del proyecto.
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    // Preparar las tablas que se enviarán a SQL Server.
                    DataTable tablaZonas = CrearTablaZonas();
                    DataTable tablaFilas = CrearTablaFilas();
                    DataTable tablaPartidos = CrearTablaPartidos();

                    // Preparar la llamada al procedimiento almacenado.
                    using (SqlCommand comando = new SqlCommand(
                        "dbo.sp_RegistrarCatalogoCompleto",
                        conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.CommandTimeout = 120;

                        // Datos generales del estadio y del administrador.
                        comando.Parameters.Add(
                            "@NominaAdmin",
                            SqlDbType.Int).Value = NominaAdmin;

                        comando.Parameters.Add(
                            "@NombreEstadio",
                            SqlDbType.VarChar,
                            100).Value =
                            (object)registroActual.NombreEstadio ?? DBNull.Value;

                        comando.Parameters.Add(
                            "@Ciudad",
                            SqlDbType.VarChar,
                            100).Value =
                            (object)registroActual.Ciudad ?? DBNull.Value;

                        comando.Parameters.Add(
                            "@Capacidad",
                            SqlDbType.Int).Value =
                            registroActual.Capacidad;

                        comando.Parameters.Add(
                            "@Mapa",
                            SqlDbType.VarChar,
                            255).Value =
                            (object)registroActual.RutaMapa ?? DBNull.Value;

                        // Enviar las zonas.
                        SqlParameter parametroZonas =
                            comando.Parameters.Add(
                                "@Zonas",
                                SqlDbType.Structured);

                        parametroZonas.TypeName = "dbo.TipoZonaRegistro";
                        parametroZonas.Value = tablaZonas;

                        // Enviar las filas.
                        SqlParameter parametroFilas =
                            comando.Parameters.Add(
                                "@Filas",
                                SqlDbType.Structured);

                        parametroFilas.TypeName = "dbo.TipoFilaRegistro";
                        parametroFilas.Value = tablaFilas;

                        // Enviar los partidos.
                        SqlParameter parametroPartidos =
                            comando.Parameters.Add(
                                "@Partidos",
                                SqlDbType.Structured);

                        parametroPartidos.TypeName = "dbo.TipoPartidoRegistro";
                        parametroPartidos.Value = tablaPartidos;

                        // Ejecutar el procedimiento almacenado.
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                int idEstadio =
                                    Convert.ToInt32(lector["ID_Estadio"]);

                                int idCatalogo =
                                    Convert.ToInt32(lector["ID_Catalogo"]);

                                int totalPartidos =
                                    registroActual.Partidos?.Count ?? 0;

                                MessageBox.Show(
                                    "El catálogo se registró correctamente.\n\n" +
                                    "ID del estadio: " + idEstadio + "\n" +
                                    "ID del catálogo: " + idCatalogo + "\n" +
                                    "Estadio: " + registroActual.NombreEstadio + "\n" +
                                    "Zonas: " + registroActual.Zonas.Count + "\n" +
                                    "Partidos: " + totalPartidos + "\n\n" +
                                    "También se registraron todos los asientos.",
                                    "Registro completado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                throw new Exception(
                                    "El procedimiento no devolvió los identificadores del registro.");
                            }
                        }
                    }
                }

                // Limpiar los datos temporales después del registro.
                registroActual = new RegistroCatalogoTemporal();

                // Cerrar el formulario.
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "SQL Server no pudo completar el registro.\n\n" +
                    ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar el catálogo completo.\n\n" +
                    ex.Message,
                    "Error al guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Guardar un catálogo que contiene partidos de estadios existentes.
        // No registra estadios, zonas ni asientos nuevos.
        private void GuardarCatalogoPartidosExistentes()
        {
            try
            {
                // Obtener la conexión centralizada del proyecto.
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    // Preparar los partidos con sus respectivos estadios.
                    DataTable tablaPartidos =
                        CrearTablaPartidosExistentes();

                    // Preparar la llamada al procedimiento almacenado.
                    using (SqlCommand comando = new SqlCommand(
                        "dbo.sp_RegistrarCatalogoPartidos",
                        conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.CommandTimeout = 120;

                        // Enviar la nómina del administrador.
                        comando.Parameters.Add(
                            "@NominaAdmin",
                            SqlDbType.Int).Value = NominaAdmin;

                        // Enviar los partidos y los estadios seleccionados.
                        SqlParameter parametroPartidos =
                            comando.Parameters.Add(
                                "@Partidos",
                                SqlDbType.Structured);

                        parametroPartidos.TypeName =
                            "dbo.TipoPartidoRegistroConEstadio";

                        parametroPartidos.Value = tablaPartidos;

                        // Ejecutar el procedimiento almacenado.
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                int idCatalogo =
                                    Convert.ToInt32(lector["ID_Catalogo"]);

                                int primerNumeroPartido =
                                    Convert.ToInt32(
                                        lector["Primer_Numero_Partido"]);

                                int totalPartidos =
                                    registroActual.Partidos.Count;

                                MessageBox.Show(
                                    "El catálogo de partidos se registró correctamente.\n\n" +
                                    "ID del catálogo: " + idCatalogo + "\n" +
                                    "Partidos registrados: " + totalPartidos + "\n" +
                                    "Primer número de partido: " +
                                    primerNumeroPartido + "\n\n" +
                                    "Los partidos quedaron relacionados con " +
                                    "los estadios seleccionados.\n\n" +
                                    "No se crearon estadios, zonas ni asientos nuevos.",
                                    "Registro completado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                throw new Exception(
                                    "El procedimiento no devolvió el ID del catálogo.");
                            }
                        }
                    }
                }

                // Limpiar los datos temporales después del registro.
                registroActual = new RegistroCatalogoTemporal();

                // Cerrar el formulario.
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "SQL Server no pudo registrar los partidos.\n\n" +
                    ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar el catálogo de partidos.\n\n" +
                    ex.Message,
                    "Error al guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}