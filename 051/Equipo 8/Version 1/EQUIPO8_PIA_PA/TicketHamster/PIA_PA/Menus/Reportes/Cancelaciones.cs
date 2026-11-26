using System;
using System.Windows.Forms;
using PIA_PA.Comun.Data;

namespace PIA_PA
{
 public partial class Cancelaciones : UserControl
 {
  private readonly DatabaseConnection conexion;
  public Cancelaciones(DatabaseConnection db)
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
   try { GeneradorReportes.Exportar("Reporte_Cancelaciones", CrearTextoReporte()); }
   catch(Exception ex) { MessageBox.Show(ex.Message); }
  }
  private string CrearTextoReporte()
  {
    var (inicio,fin)=GeneradorReportes.Fechas(monthCalendar1);
    var tabla=GeneradorReportes.Consultar(conexion,@"SELECT c.Cancelacion_ID, c.Cancelacion_Fecha, c.Cancelacion_Motivo, c.Ticket_ID,
 t.Cliente_ID,t.Partido_ID,t.Asiento_ID,v.Venta_Info_ID
 FROM Cancelacion c JOIN Ticket t ON t.Ticket_ID=c.Ticket_ID
 JOIN Venta_Info v ON v.Venta_Info_ID=t.Venta_Info_ID
 WHERE c.Cancelacion_Fecha BETWEEN @inicio AND @fin
 ORDER BY c.Cancelacion_Fecha,c.Cancelacion_ID",("@inicio",inicio),("@fin",fin));
    string encabezado="";
    encabezado=$"Total de cancelaciones: {tabla.Rows.Count}\n";
    return "REPORTE DE CANCELACIONES\nPeriodo: "+inicio.ToString("yyyy-MM-dd")+" a "+fin.ToString("yyyy-MM-dd")+"\n"+encabezado+"\n"+GeneradorReportes.Formatear(tabla);
  }
 }
}
