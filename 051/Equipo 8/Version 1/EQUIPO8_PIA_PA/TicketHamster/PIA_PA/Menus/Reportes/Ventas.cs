using System;
using System.Windows.Forms;
using PIA_PA.Comun.Data;

namespace PIA_PA
{
 public partial class Ventas : UserControl
 {
  private readonly DatabaseConnection conexion;
  public Ventas(DatabaseConnection db)
  {
   InitializeComponent(); conexion=db;
   monthCalendar1.MaxSelectionCount=30;
   btnGenerarTXT.Click+=(_,_)=>Generar(); btnGuardarTXT.Click+=(_,_)=>Guardar(); ConfigurarVistaPrevia();
  }
  private void ConfigurarVistaPrevia()
  {
   txtReporte.Multiline = true;
   txtReporte.ReadOnly = true;
   txtReporte.ScrollBars = ScrollBars.Both;
   txtReporte.WordWrap = false;
  }
  private void Generar()
  {
   try { GeneradorReportes.MostrarVistaPrevia(txtReporte, CrearTextoReporte()); }
   catch(Exception ex) { txtReporte.Clear(); MessageBox.Show(ex.Message); }
  }
  private void Guardar()
  {
   try { GeneradorReportes.Exportar("Reporte_Ventas", CrearTextoReporte()); }
   catch(Exception ex) { MessageBox.Show(ex.Message); }
  }
  private string CrearTextoReporte()
  {
    var (inicio,fin)=GeneradorReportes.Fechas(monthCalendar1);
    var tabla=GeneradorReportes.Consultar(conexion,@"SELECT v.Venta_Info_ID AS Venta, v.Venta_Info_Fecha AS Fecha, v.Usuario_ID AS Vendedor,
 v.Venta_Info_Subtotal AS Subtotal, v.Venta_Info_IVA AS IVA, v.Venta_Info_Total AS Total,
 p.Pago_Metodo AS MetodoPago, v.Pagador_RFC AS RFC, COUNT(t.Ticket_ID) AS Boletos
 FROM Venta_Info v JOIN Pago p ON p.Pago_ID=v.Pago_ID
 LEFT JOIN Ticket t ON t.Venta_Info_ID=v.Venta_Info_ID
 WHERE v.Venta_Info_Fecha BETWEEN @inicio AND @fin
 GROUP BY v.Venta_Info_ID,v.Venta_Info_Fecha,v.Usuario_ID,v.Venta_Info_Subtotal,v.Venta_Info_IVA,v.Venta_Info_Total,p.Pago_Metodo,v.Pagador_RFC
 ORDER BY v.Venta_Info_Fecha,v.Venta_Info_ID",("@inicio",inicio),("@fin",fin));
    string encabezado="";
    double total=0; foreach(System.Data.DataRow fila in tabla.Rows)total+=Convert.ToDouble(fila["Total"]);
                encabezado=$"Total de ventas: {tabla.Rows.Count}\nTotal de ingresos: {total:C2}\n";
    return "REPORTE DE VENTAS\nPeriodo: "+inicio.ToString("yyyy-MM-dd")+" a "+fin.ToString("yyyy-MM-dd")+"\n"+encabezado+"\n"+GeneradorReportes.Formatear(tabla);
  }
 }
}
