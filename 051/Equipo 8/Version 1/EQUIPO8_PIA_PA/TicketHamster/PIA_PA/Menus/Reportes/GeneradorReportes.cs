using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PIA_PA.Comun.Data;
using Microsoft.Data.SqlClient;

namespace PIA_PA;

public static class GeneradorReportes
{
    public static void Exportar(string nombre, string contenido)
    {
        using var dialogo = new SaveFileDialog { Filter="Archivo de texto (*.txt)|*.txt", FileName=nombre+"_"+DateTime.Now.ToString("yyyyMMdd_HHmm")+".txt" };
        if (dialogo.ShowDialog()!=DialogResult.OK) return;
        File.WriteAllText(dialogo.FileName, contenido, Encoding.UTF8);
        MessageBox.Show("Reporte guardado correctamente.");
    }
    public static void MostrarVistaPrevia(TextBox caja, string contenido)
    {
        // Windows Forms espera saltos CRLF para mostrar cada linea correctamente.
        caja.Text = contenido.Replace("\r\n", "\n").Replace("\r", "\n")
                            .Replace("\n", Environment.NewLine);
    }

    public static DataTable Consultar(DatabaseConnection db, string sql, params (string nombre, object valor)[] parametros)
    {
        using var conexion=db.GetConnection();
        using var comando=new SqlCommand(sql,conexion);
        foreach(var p in parametros) comando.Parameters.AddWithValue(p.nombre,p.valor);
        using var adaptador=new SqlDataAdapter(comando);
        var tabla=new DataTable(); adaptador.Fill(tabla); return tabla;
    }
    public static string Formatear(DataTable tabla)
    {
        var sb=new StringBuilder();
        foreach(DataRow fila in tabla.Rows)
        {
            foreach(DataColumn col in tabla.Columns)
                sb.Append(col.ColumnName).Append(": ").Append(fila[col]).Append(" | ");
            sb.AppendLine();
        }
        if(tabla.Rows.Count==0)sb.AppendLine("Sin registros en el periodo seleccionado.");
        return sb.ToString();
    }
    public static (DateTime inicio,DateTime fin) Fechas(MonthCalendar calendario)
    {
        var inicio=calendario.SelectionStart.Date;
        var fin=calendario.SelectionEnd.Date;
        if((fin-inicio).TotalDays>29)throw new Exception("Selecciona máximo 30 días incluyendo ambos extremos.");
        return (inicio,fin);
    }
}
