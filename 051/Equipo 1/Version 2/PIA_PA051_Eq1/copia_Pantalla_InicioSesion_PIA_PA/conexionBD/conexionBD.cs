
using Microsoft.Data.SqlClient;
using System;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace Pantalla_InicioSesion_PIA_PA.conexionBD
{
        //USO DE HAS Y SALT
        public static class SeguridadContrasena
        {
            // Cantidad de iteraciones utilizadas para generar el hash.
            private const int Iteraciones = 600000;

            // Tamaño del salt en bytes.
            private const int TamanoSalt = 16;

            // Tamaño del hash en bytes.
            private const int TamanoHash = 32;

            // Genera un salt aleatorio y el hash de la contraseña.
            public static (byte[] Hash, byte[] Salt, int Iteraciones)
                GenerarHash(string contrasena)
            {
                if (string.IsNullOrWhiteSpace(contrasena))
                    throw new ArgumentException(
                        "La contraseña no puede estar vacía.");

                byte[] salt = RandomNumberGenerator.GetBytes(TamanoSalt);

                byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                    contrasena,
                    salt,
                    Iteraciones,
                    HashAlgorithmName.SHA256,
                    TamanoHash);

                return (hash, salt, Iteraciones);
            }

            // Comprueba si una contraseña coincide con el hash almacenado.
            public static bool VerificarContrasena(
                string contrasena,
                byte[] hashAlmacenado,
                byte[] salt,
                int iteraciones)
            {
                if (string.IsNullOrEmpty(contrasena) ||
                    hashAlmacenado == null ||
                    salt == null ||
                    iteraciones <= 0)
                {
                    return false;
                }

                byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
                    contrasena,
                    salt,
                    iteraciones,
                    HashAlgorithmName.SHA256,
                    hashAlmacenado.Length);

                return CryptographicOperations.FixedTimeEquals(
                    hashCalculado,
                    hashAlmacenado);
            }
        }
    

    // =========================================================
    // RESULTADO DE LA VENTA Y DEL PAGO
    // =========================================================

    public class ResultadoVentaPago
    {
        public int Folio { get; set; }
        public int IDPago { get; set; }
        public int Subtotal { get; set; }
        public int IVA { get; set; }
        public int TotalVenta { get; set; }
        public int TotalBoletos { get; set; }
        public int? Cambio { get; set; }
    }

    public static class ConexionBD
    {
        // Cadena de conexión con SQL Server.
        private static readonly string cadenaConexion =
            @"Data Source=XLMD007\SQLEXPRESS;Initial Catalog=PrimerAvance;Integrated Security=True;TrustServerCertificate=True;";

        // =========================================================
        // OBTENER CONEXIÓN
        // =========================================================

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        // =========================================================
        // CONSULTAR CLIENTES PARA EDICIÓN
        // =========================================================

        public static DataTable ObtenerClientesEdicion()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_ConsultarClientesEdicion", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // CONSULTAR HISTORIAL DE CLIENTES
        // =========================================================

        public static DataTable ObtenerHistorialClientes()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_ConsultarHistorialClientes", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // ACTUALIZAR CLIENTE
        // =========================================================

        // =========================================================
        // ACTUALIZAR CLIENTE
        // =========================================================

        public static int ActualizarCliente(
            string rfcOriginal,
            string primerNombre,
            string segundoNombre,
            string primerApellido,
            string segundoApellido,
            string correo,
            string telefonoCasa,
            string celular,
            DateTime fechaNacimiento,
            string calle,
            string colonia,
            string ciudad,
            string numeroVivienda,
            string municipio,
            int nominaUsuario)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_ActualizarCliente", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@RFCOriginal", SqlDbType.VarChar, 13)
                    .Value = rfcOriginal;

                comando.Parameters.Add("@PrimerNombre", SqlDbType.VarChar, 100)
                    .Value = primerNombre;

                comando.Parameters.Add("@SegundoNombre", SqlDbType.VarChar, 100)
                    .Value = string.IsNullOrWhiteSpace(segundoNombre)
                        ? (object)DBNull.Value : segundoNombre;

                comando.Parameters.Add("@PrimerApellido", SqlDbType.VarChar, 100)
                    .Value = primerApellido;

                comando.Parameters.Add("@SegundoApellido", SqlDbType.VarChar, 100)
                    .Value = segundoApellido;

                comando.Parameters.Add("@Correo", SqlDbType.VarChar, 150)
                    .Value = correo;

                comando.Parameters.Add("@TelCasa", SqlDbType.VarChar, 10)
                    .Value = string.IsNullOrWhiteSpace(telefonoCasa)
                        ? (object)DBNull.Value : telefonoCasa;

                comando.Parameters.Add("@Celular", SqlDbType.VarChar, 10)
                    .Value = celular;

                comando.Parameters.Add("@FechaNacimiento", SqlDbType.Date)
                    .Value = fechaNacimiento.Date;

                comando.Parameters.Add("@Calle", SqlDbType.VarChar, 150)
                    .Value = calle;

                comando.Parameters.Add("@Colonia", SqlDbType.VarChar, 100)
                    .Value = colonia;

                comando.Parameters.Add("@Ciudad", SqlDbType.VarChar, 100)
                    .Value = ciudad;

                comando.Parameters.Add("@NumeroVivienda", SqlDbType.VarChar, 10)
                    .Value = numeroVivienda;

                comando.Parameters.Add("@Municipio", SqlDbType.VarChar, 100)
                    .Value = municipio;

                // Enviar la nómina del usuario que realiza la modificación.
                comando.Parameters.Add("@NominaUsuario", SqlDbType.Int)
                    .Value = nominaUsuario;

                conexion.Open();

                object resultado = comando.ExecuteScalar();

                return resultado == null || resultado == DBNull.Value
                    ? 0
                    : Convert.ToInt32(resultado);
            }
        }

        // =========================================================
        // CONSULTAR REGISTROS RELACIONADOS CON UN CLIENTE
        // =========================================================

        public static DataTable ObtenerRelacionadosCliente(string rfc)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_ConsultarRelacionadosCliente", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@RFC", SqlDbType.VarChar, 13)
                    .Value = rfc;

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // ELIMINAR CLIENTE Y SUS REGISTROS RELACIONADOS
        // =========================================================

        // =========================================================
        // ELIMINAR CLIENTE Y SUS REGISTROS RELACIONADOS
        // =========================================================

        public static void EliminarClienteCompleto(
            string rfc,
            int nominaUsuario)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_EliminarClienteCompleto", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@RFC", SqlDbType.VarChar, 13)
                    .Value = rfc;

                // Enviar la nómina del usuario que realiza la eliminación.
                comando.Parameters.Add("@NominaUsuario", SqlDbType.Int)
                    .Value = nominaUsuario;

                conexion.Open();

                comando.ExecuteNonQuery();
            }
        }

        // =========================================================
        // CONSULTAR HISTORIAL DE USUARIOS
        // =========================================================

        // Obtiene el historial completo de usuarios desde la vista de SQL Server.
        public static DataTable ObtenerHistorialUsuarios()
        {
            // Crear una tabla para almacenar los resultados.
            DataTable tablaUsuarios = new DataTable();

            // Crear la conexión utilizando el método centralizado.
            using (SqlConnection conexion = ObtenerConexion())
            {
                // Crear el comando para consultar la vista.
                using (SqlCommand comando = new SqlCommand(
                    "SELECT * FROM dbo.vw_HistorialUsuarios", conexion))
                {
                    // Abrir la conexión.
                    conexion.Open();

                    // Crear el adaptador para recuperar los registros.
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        // Llenar la tabla con los resultados.
                        adaptador.Fill(tablaUsuarios);
                    }
                }
            }

            // Regresar los registros obtenidos.
            return tablaUsuarios;
        }

      
        // =========================================================
        // REGISTRAR USUARIO CON HASH + SALT
        // =========================================================

        // Registra un usuario y su tipo mediante el procedimiento almacenado.
        // La contraseña se transforma antes de enviarse a SQL Server.
        public static int RegistrarUsuario(
            string tipoUsuario,
            string primerNombre,
            string segundoNombre,
            string primerApellido,
            string segundoApellido,
            string correo,
            DateTime fechaNacimiento,
            string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contraseña es obligatoria.");

            if (contrasena.Length > 50)
                throw new ArgumentException(
                    "La contraseña no puede superar los 50 caracteres.");

            // Generar el hash y el salt de la contraseña.
            var credenciales = SeguridadContrasena.GenerarHash(contrasena);

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_RegistrarUsuario", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@TipoUsuario", SqlDbType.VarChar, 20)
                    .Value = tipoUsuario;

                comando.Parameters.Add("@PrimerNombre", SqlDbType.VarChar, 100)
                    .Value = primerNombre;

                comando.Parameters.Add("@SegundoNombre", SqlDbType.VarChar, 100)
                    .Value = string.IsNullOrWhiteSpace(segundoNombre)
                        ? (object)DBNull.Value
                        : segundoNombre;

                comando.Parameters.Add("@PrimerApellido", SqlDbType.VarChar, 100)
                    .Value = primerApellido;

                comando.Parameters.Add("@SegundoApellido", SqlDbType.VarChar, 100)
                    .Value = segundoApellido;

                comando.Parameters.Add("@Correo", SqlDbType.VarChar, 150)
                    .Value = correo;

                comando.Parameters.Add("@FechaNacimiento", SqlDbType.Date)
                    .Value = fechaNacimiento.Date;

                // Enviar el hash.
                comando.Parameters.Add(
                    "@HashContrasena", SqlDbType.VarBinary, 32)
                    .Value = credenciales.Hash;

                // Enviar el salt.
                comando.Parameters.Add(
                    "@Salt", SqlDbType.VarBinary, 16)
                    .Value = credenciales.Salt;

                // Enviar la cantidad de iteraciones.
                comando.Parameters.Add(
                    "@Iteraciones", SqlDbType.Int)
                    .Value = credenciales.Iteraciones;

                conexion.Open();

                object resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                    throw new Exception("No se recibió la nómina generada.");

                return Convert.ToInt32(resultado);
            }
        }

        // =========================================================
        // CONSULTAR VENTAS DE BOLETOS
        // =========================================================

        public static DataTable ObtenerVentasBoletos()
        {
            DataTable tablaVentas = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "SELECT * FROM dbo.vw_RegistroVentasBoletos",
                conexion))
            {
                conexion.Open();

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tablaVentas);
                }
            }

            return tablaVentas;
        }

        // =========================================================
        // CONSULTAR VENTA POR FOLIO
        // =========================================================

        public static DataTable ObtenerVentaPorFolio(int folio)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT Folio, Subtotal, IVA, Total_de_Venta,
                 Total_de_Boletos, RFC_Cliente, Nomina_Ope,
                 Nomina_Admin, RFC_Emisor
          FROM dbo.vw_EdicionVentasBoletos
          WHERE Folio = @Folio;",
                conexion))
            {
                comando.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;

                conexion.Open();

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // ACTUALIZAR VENTA DE BOLETOS
        // =========================================================

        public static int ActualizarVentaBoletos(
            int folio,
            int subtotal,
            int iva,
            int totalVenta,
            int totalBoletos,
            string rfcCliente,
            int nominaOpe,
            int nominaAdmin,
            string rfcEmisor)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE dbo.vw_EdicionVentasBoletos
          SET Subtotal = @Subtotal,
              IVA = @IVA,
              Total_de_Venta = @Total_de_Venta,
              Total_de_Boletos = @Total_de_Boletos,
              RFC_Cliente = @RFC_Cliente,
              Nomina_Ope = @Nomina_Ope,
              Nomina_Admin = @Nomina_Admin,
              RFC_Emisor = @RFC_Emisor
          WHERE Folio = @Folio;",
                conexion))
            {
                comando.Parameters.Add("@Subtotal", SqlDbType.Int).Value = subtotal;
                comando.Parameters.Add("@IVA", SqlDbType.Int).Value = iva;
                comando.Parameters.Add("@Total_de_Venta", SqlDbType.Int).Value = totalVenta;
                comando.Parameters.Add("@Total_de_Boletos", SqlDbType.Int).Value = totalBoletos;
                comando.Parameters.Add("@RFC_Cliente", SqlDbType.VarChar, 13).Value = rfcCliente;
                comando.Parameters.Add("@Nomina_Ope", SqlDbType.Int).Value = nominaOpe;
                comando.Parameters.Add("@Nomina_Admin", SqlDbType.Int).Value = nominaAdmin;

                comando.Parameters.Add("@RFC_Emisor", SqlDbType.VarChar, 13).Value =
                    string.IsNullOrWhiteSpace(rfcEmisor)
                        ? (object)DBNull.Value
                        : rfcEmisor;

                comando.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;

                conexion.Open();

                return comando.ExecuteNonQuery();
            }
        }

        // =========================================================
        // CONSULTAR FILAS DISPONIBLES
        // =========================================================

        // Consulta las filas que tienen asientos disponibles para el partido y zona.
        public static DataTable ObtenerFilasDisponibles(
            int numeroPartido,
            int idZona)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_ConsultarFilasDisponibles", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@NumeroPartido", SqlDbType.Int).Value = numeroPartido;

                comando.Parameters.Add(
                    "@IDZona", SqlDbType.Int).Value = idZona;

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // CONSULTAR ASIENTOS DISPONIBLES
        // =========================================================

        // Consulta los números de asiento disponibles de una fila.
        public static DataTable ObtenerAsientosDisponibles(
            int numeroPartido,
            int idZona,
            string fila)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_ConsultarAsientosPorFila", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@NumeroPartido", SqlDbType.Int).Value = numeroPartido;

                comando.Parameters.Add(
                    "@IDZona", SqlDbType.Int).Value = idZona;

                comando.Parameters.Add(
                    "@Fila", SqlDbType.VarChar, 10).Value = fila;

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // GUARDAR ASIENTOS SELECCIONADOS
        // =========================================================

        // Guarda temporalmente los asientos que seleccionó el usuario.
        public static int GuardarAsientosSeleccionados(
            int idSeleccion,
            int numeroPartido,
            int idZona,
            int cantidad,
            string fila,
            DataTable asientos)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_GuardarAsientosSeleccionados", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@IDSeleccion", SqlDbType.Int).Value = idSeleccion;

                comando.Parameters.Add(
                    "@NumeroPartido", SqlDbType.Int).Value = numeroPartido;

                comando.Parameters.Add(
                    "@IDZona", SqlDbType.Int).Value = idZona;

                comando.Parameters.Add(
                    "@Cantidad", SqlDbType.Int).Value = cantidad;

                comando.Parameters.Add(
                    "@Fila", SqlDbType.VarChar, 10).Value = fila;

                SqlParameter parametroAsientos =
                    comando.Parameters.Add("@Asientos", SqlDbType.Structured);

                parametroAsientos.TypeName =
                    "dbo.TipoAsientosSeleccionados";

                parametroAsientos.Value = asientos;

                conexion.Open();

                object resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                    return 0;

                return Convert.ToInt32(resultado);
            }
        }

        // =========================================================
        // CONSULTAR RESUMEN DE SELECCIÓN
        // =========================================================

        // Consulta los datos de la selección para mostrar la vista previa.
        public static DataTable ObtenerResumenSeleccion(int idSeleccion)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_ConsultarResumenSeleccion", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@IDSeleccion", SqlDbType.Int).Value = idSeleccion;

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // REGISTRAR CANCELACIÓN DE VENTA
        // =========================================================

        // Registra la cancelación de una venta mediante un procedimiento almacenado.
        public static int CancelarVenta(int folio, string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("El motivo de cancelación es obligatorio.");

            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "dbo.sp_CancelarVentaBoletos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;

                    comando.Parameters.Add("@Motivo", SqlDbType.VarChar, 250).Value =
                        motivo.Trim();

                    object resultado = comando.ExecuteScalar();

                    if (resultado == null || resultado == DBNull.Value)
                    {
                        throw new Exception(
                            "SQL Server no devolvió el código de cancelación.");
                    }

                    return Convert.ToInt32(resultado);
                }
            }
        }

        // =========================================================
        // CONFIRMAR VENTA Y REGISTRAR PAGO
        // =========================================================

        // Confirma la venta, registra el método de pago,
        // guarda los asientos y devuelve los datos de la operación.
        public static ResultadoVentaPago RegistrarVentaConPago(
            int idSeleccion,
            string rfcCliente,
            int nominaOperador,
            int nominaAdmin,
            string rfcEmisor,
            string tipoPago,
            int? pagoMeses,
            int? importeRecibido,
            int cantidadBilletes,
            int cantidadMonedas)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "dbo.sp_ConfirmarVentaConPago", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@IDSeleccion", SqlDbType.Int)
                        .Value = idSeleccion;

                    comando.Parameters.Add("@RFCCliente", SqlDbType.VarChar, 13)
                        .Value = rfcCliente;

                    comando.Parameters.Add("@NominaOperador", SqlDbType.Int)
                        .Value = nominaOperador;

                    comando.Parameters.Add("@NominaAdmin", SqlDbType.Int)
                        .Value = nominaAdmin;

                    comando.Parameters.Add("@RFCEmisor", SqlDbType.VarChar, 13)
                        .Value = string.IsNullOrWhiteSpace(rfcEmisor)
                            ? (object)DBNull.Value
                            : rfcEmisor;

                    comando.Parameters.Add("@TipoPago", SqlDbType.VarChar, 10)
                        .Value = tipoPago;

                    comando.Parameters.Add("@PagoMeses", SqlDbType.Int)
                        .Value = (object)pagoMeses ?? DBNull.Value;

                    comando.Parameters.Add("@ImporteRecibido", SqlDbType.Int)
                        .Value = (object)importeRecibido ?? DBNull.Value;

                    comando.Parameters.Add("@CantidadBilletes", SqlDbType.Int)
                        .Value = cantidadBilletes;

                    comando.Parameters.Add("@CantidadMonedas", SqlDbType.Int)
                        .Value = cantidadMonedas;

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read())
                        {
                            throw new Exception(
                                "SQL Server no devolvió los datos de la venta.");
                        }

                        return new ResultadoVentaPago
                        {
                            Folio = Convert.ToInt32(lector["Folio"]),

                            IDPago = Convert.ToInt32(lector["ID_Pago"]),

                            Subtotal = Convert.ToInt32(lector["Subtotal"]),

                            IVA = Convert.ToInt32(lector["IVA"]),

                            TotalVenta = Convert.ToInt32(
                                lector["Total_de_Venta"]),

                            TotalBoletos = Convert.ToInt32(
                                lector["Total_de_Boletos"]),

                            Cambio = lector["Cambio"] == DBNull.Value
                                ? (int?)null
                                : Convert.ToInt32(lector["Cambio"])
                        };
                    }
                }
            }
        }

        // =========================================================
        // CONSULTAR CLIENTE POR RFC
        // =========================================================

        public static DataTable ObtenerClientePorRFC(string rfc)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT
              C.*,
              C.Correoelectronico AS Correo_Electronico,
              COALESCE(C.Tel_casa, C.Celular) AS Telefono,
              C.Fecha_nacimiento AS Fecha_Nacimiento
          FROM dbo.CLIENTES C
          WHERE UPPER(LTRIM(RTRIM(C.RFC_Cliente))) = @RFC;",
                conexion))
            {
                comando.Parameters.Add("@RFC", SqlDbType.VarChar, 13).Value =
                    rfc.Trim().ToUpper();

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // CONSULTAR REPORTE DE OCUPACIÓN Y DISPONIBILIDAD
        // =========================================================

        public static DataTable ObtenerReporteOcupacionDisponibilidad()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT *
          FROM dbo.vw_ReporteOcupacionDisponibilidad
          ORDER BY Numero_partido, ID_Zona;",
                conexion))
            {
                conexion.Open();

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // REPORTE DE VENTAS
        // =========================================================

        public static DataTable ObtenerReporteVentas()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT *
                  FROM dbo.vw_ReporteVentas
                  ORDER BY Folio DESC;",
                conexion))
            {
                conexion.Open();

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // GUARDAR EVALUACIÓN GERENCIAL
        // =========================================================

        public static int GuardarEvaluacionGerencial(
            int nomina,
            int liderazgo,
            int comunicacion)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_GuardarEvaluacionGerencial", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@Nomina", SqlDbType.Int).Value = nomina;

                comando.Parameters.Add(
                    "@Liderazgo", SqlDbType.Int).Value = liderazgo;

                comando.Parameters.Add(
                    "@Comunicacion", SqlDbType.Int).Value = comunicacion;

                SqlParameter filasActualizadas = comando.Parameters.Add(
                    "@FilasActualizadas", SqlDbType.Int);

                filasActualizadas.Direction = ParameterDirection.Output;

                conexion.Open();
                comando.ExecuteNonQuery();

                return Convert.ToInt32(filasActualizadas.Value);
            }
        }

        // Comprueba si el cliente existe mediante su RFC.
        public static bool ExisteCliente(string rfc)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT COUNT(1)
          FROM dbo.CLIENTES
          WHERE UPPER(LTRIM(RTRIM(RFC_Cliente))) = @RFC;",
                conexion))
            {
                comando.Parameters.Add(
                    "@RFC", SqlDbType.VarChar, 13).Value = rfc;

                conexion.Open();

                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }

        // Obtiene los estadios registrados y sus datos actualizados.
        public static DataTable ObtenerEstadios()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT
              ID_Estadio,
              Nombre_Estadio,
              Ciudad,
              Capacidad_Total,
              Mapa
          FROM dbo.ESTADIO
          ORDER BY Nombre_Estadio ASC;",
                conexion))
            {
                conexion.Open();

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // Obtiene únicamente los partidos del estadio y la fecha seleccionados.
        public static DataTable ObtenerPartidosPorEstadioYFecha(
            int idEstadio,
            DateTime fecha)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT
              P.Numero_partido,
              P.Equipo_Local,
              P.Equipo_Visitante,
              E.Nombre_Estadio,
              E.Ciudad,
              P.Fecha,
              P.Hora,
              P.Estado
              FROM dbo.PARTIDOS AS P
              INNER JOIN dbo.ESTADIO AS E
                  ON E.ID_Estadio = P.ID_Estadio
              WHERE P.ID_Estadio = @IDEstadio
                AND P.Fecha = @Fecha
              ORDER BY P.Hora ASC;",
                conexion))
            {
                comando.Parameters.Add(
                    "@IDEstadio", SqlDbType.Int).Value = idEstadio;

                comando.Parameters.Add(
                    "@Fecha", SqlDbType.Date).Value = fecha.Date;

                conexion.Open();

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // CONSULTAR TICKET TEMPORAL
        // =========================================================

        public static DataTable ObtenerTicketTemporal(int idSeleccion)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand(
                    "SELECT * FROM vw_TicketTemporal WHERE ID_Seleccion = @IDSeleccion",
                    conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@IDSeleccion",
                        idSeleccion);

                    DataTable tabla = new DataTable();

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }

                    return tabla;
                }
            }
        }

        // =========================================================
        // CONSULTAR COMPROBANTE POR FOLIO
        // =========================================================

        public static DataTable ObtenerComprobantePorFolio(int folio)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT *
                  FROM dbo.fn_ObtenerComprobantePorFolio(@Folio);",
                conexion))
            {
                comando.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // CONSULTAR ZONAS POR PARTIDO
        // =========================================================

        public static DataTable ObtenerZonasPorPartido(int numeroPartido)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_ObtenerZonasPorPartido", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@NumeroPartido", SqlDbType.Int).Value = numeroPartido;

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // CONSULTAR MAPA DEL ESTADIO
        // =========================================================

        public static DataTable ObtenerMapaEstadio(int idEstadio)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_ObtenerMapaEstadio", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@IDEstadio", SqlDbType.Int).Value = idEstadio;

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =========================================================
        // ELIMINAR USUARIO DEL HISTORIAL (BAJA LÓGICA)
        // =========================================================

        // Se conserva este método para evitar romper llamadas existentes.
        // Ya no elimina físicamente al usuario; realiza una baja lógica.
        public static int EliminarUsuarioHistorial(int nomina)
        {
            return DarDeBajaUsuario(nomina);
        }

        // =========================================================
        // DAR DE BAJA A UN USUARIO
        // =========================================================

        // Cambia el estado del usuario sin eliminar su información.
        // Solo permite dar de baja a usuarios que todavía están activos.
        public static int DarDeBajaUsuario(int nomina)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE dbo.USUARIOS
                  SET Activo = 0,
                      Fecha_Baja = SYSDATETIME()
                  WHERE Nomina = @Nomina
                    AND Activo = 1;",
                conexion))
            {
                comando.Parameters.Add("@Nomina", SqlDbType.Int).Value = nomina;

                conexion.Open();

                return comando.ExecuteNonQuery();
            }
        }

        // =========================================================
        // ACTUALIZAR USUARIO DEL HISTORIAL
        // =========================================================

        // Actualiza únicamente usuarios activos.
        // El segundo nombre es opcional.
        public static int ActualizarUsuarioHistorial(
            int nomina,
            string primerNombre,
            string segundoNombre,
            string primerApellido,
            string segundoApellido,
            string correo,
            DateTime fechaNacimiento)
        {
            // Validar los campos obligatorios.
            if (string.IsNullOrWhiteSpace(primerNombre))
                throw new ArgumentException("El primer nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(primerApellido))
                throw new ArgumentException("El primer apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(segundoApellido))
                throw new ArgumentException("El segundo apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo electrónico es obligatorio.");

            // Validar que la fecha no sea futura.
            if (fechaNacimiento.Date > DateTime.Today)
                throw new ArgumentException(
                    "La fecha de nacimiento no puede ser futura.");

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE dbo.USUARIOS
                  SET Primer_Nombre = @PrimerNombre,
                      Segundo_Nombre = @SegundoNombre,
                      Primer_Apellido = @PrimerApellido,
                      Segundo_Apellido = @SegundoApellido,
                      Correo_electronico = @Correo,
                      Fecha_Nacimiento = @FechaNacimiento
                  WHERE Nomina = @Nomina
                    AND Activo = 1;",
                conexion))
            {
                comando.Parameters.Add("@Nomina", SqlDbType.Int)
                    .Value = nomina;

                comando.Parameters.Add("@PrimerNombre", SqlDbType.VarChar, 50)
                    .Value = primerNombre.Trim().ToUpperInvariant();

                comando.Parameters.Add("@SegundoNombre", SqlDbType.VarChar, 50)
                    .Value = string.IsNullOrWhiteSpace(segundoNombre)
                        ? (object)DBNull.Value
                        : segundoNombre.Trim().ToUpperInvariant();

                comando.Parameters.Add("@PrimerApellido", SqlDbType.VarChar, 50)
                    .Value = primerApellido.Trim().ToUpperInvariant();

                comando.Parameters.Add("@SegundoApellido", SqlDbType.VarChar, 50)
                    .Value = segundoApellido.Trim().ToUpperInvariant();

                comando.Parameters.Add("@Correo", SqlDbType.VarChar, 100)
                    .Value = correo.Trim();

                comando.Parameters.Add("@FechaNacimiento", SqlDbType.Date)
                    .Value = fechaNacimiento.Date;

                conexion.Open();

                return comando.ExecuteNonQuery();
            }
        }
        public static int GuardarConductaOperativo(
        int nomina,
        int productividad,
        int resistencia)
            {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "dbo.sp_GuardarConductaOperativo", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@NominaOpe", SqlDbType.Int).Value = nomina;
                comando.Parameters.Add("@Productividad", SqlDbType.Int).Value = productividad;
                comando.Parameters.Add("@Resistencia", SqlDbType.Int).Value = resistencia;

                conexion.Open();

                object resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                    return -1;

                return Convert.ToInt32(resultado);
            }
        }
    }
}
