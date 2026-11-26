using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Configuration;


namespace VentaBoletosWF
{

    public class Conexion
    {
        public static SqlConnection conectar()
        {
            string cadena = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;
            return new SqlConnection(cadena);
        }
    }
}
