using System.Data;
using Microsoft.Data.SqlClient;

namespace TemplateCSharp.Data;

public class Ejecutor
{
    private readonly DatabaseConnection _dbConnection;

    public Ejecutor(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // Método 1: Para consultar datos 
    public DataTable ConsultarSP(string nombreSP, SqlParameter[]? parametros = null)
    {
        DataTable tabla = new DataTable();
        using (SqlConnection conn = _dbConnection.CreateConnection())
        {
            try
            {
                using SqlCommand cmd = new SqlCommand(nombreSP, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    cmd.Parameters.AddRange(parametros);
                }

                using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(tabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en consulta: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        return tabla;
    }

    // Método 2: Para guardar o modificar datos 
    public bool Ejecutar(string nombreSP, SqlParameter[] parametros)
    {
        using (SqlConnection conn = _dbConnection.CreateConnection())
        {
            try
            {
                using SqlCommand cmd = new SqlCommand(nombreSP, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametros);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
    }
}

