
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Pantalla_InicioSesion_PIA_PA.conexionBD
{
    public class Vistas
    {
        // Busca los clientes que tienen el primer apellido ingresado.
        public DataTable BuscarClientes(string apellido)
        {
            DataTable resultados = new DataTable();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"
                    SELECT DISTINCT
                        C.RFC_Cliente,
                        LTRIM(RTRIM(CONCAT(
                            C.Primer_Nombre, ' ',
                            ISNULL(C.Segundo_Nombre + ' ', ''),
                            C.Primer_Apellido, ' ',
                            ISNULL(C.Segundo_Apellido, '')
                        ))) AS Nombre_Completo
                    FROM dbo.CLIENTES AS C
                    WHERE C.Primer_Apellido = @Apellido
                    ORDER BY Nombre_Completo;";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Apellido", SqlDbType.VarChar, 100)
                        .Value = apellido;

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(resultados);
                    }
                }
            }

            return resultados;
        }

        // Obtiene el historial de compras de un cliente mediante la vista.
        public DataTable ObtenerHistorialComprasCliente(string rfcCliente)
        {
            DataTable resultados = new DataTable();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"
                    SELECT R.*
                    FROM dbo.vw_BusquedaClientes AS R
                    INNER JOIN dbo.VENTADEBOLETOS AS VB
                        ON VB.Folio = R.Codigo_Compra
                    WHERE VB.RFC_Cliente = @RFC
                    ORDER BY R.Fecha_Compra DESC;";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@RFC", SqlDbType.VarChar, 13)
                        .Value = rfcCliente;

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(resultados);
                    }
                }
            }

            return resultados;
        }

        // Verifica si existe un folio de venta.
        public bool ExisteFolioVenta(int folio)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"
                    SELECT COUNT(*)
                    FROM dbo.VENTADEBOLETOS
                    WHERE Folio = @Folio;";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;

                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    int cantidad = Convert.ToInt32(comando.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }

        // Obtiene los catálogos registrados mediante la vista.
        public DataTable ObtenerCatalogosRegistrados()
        {
            DataTable resultados = new DataTable();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"
                    SELECT
                        ID_Catalogo,
                        Nomina,
                        Numero_partido,
                        ID_Estadio,
                        Nombre_Estadio,
                        Ciudad,
                        Capacidad_Total,
                        Equipo_Local,
                        Estado,
                        Equipo_Visitante,
                        Fecha,
                        Hora
                    FROM dbo.vw_CatalogosRegistrados
                    ORDER BY ID_Catalogo, Numero_partido;";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(resultados);
                    }
                }
            }

            return resultados;
        }

        // Obtiene la información completa de una factura mediante su folio de venta.
        public static DataTable ObtenerComprobanteFactura(int folio)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand(@"
            SELECT *
            FROM dbo.fn_ObtenerComprobantePorFolio(@Folio)
            ORDER BY Numero_partido, Fila_Asiento, Num_Asiento;", conexion))
                {
                    comando.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }

            return tabla;
        }
        // Verifica si una venta tiene una factura registrada.
        public bool ExisteFacturaVenta(int folio)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"
            SELECT COUNT(*)
            FROM dbo.FACTURACIÓN
            WHERE Folio_Venta = @Folio;";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;

                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    int cantidad = Convert.ToInt32(comando.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }
    }
}
