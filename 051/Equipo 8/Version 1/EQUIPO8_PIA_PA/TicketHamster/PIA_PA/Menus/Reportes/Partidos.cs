using System;
using System.Windows.Forms;
using PIA_PA.Comun.Data;

namespace PIA_PA
{
 public partial class Partidos : UserControl
 {
  private readonly DatabaseConnection conexion;
  public Partidos(DatabaseConnection db)
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
   try { GeneradorReportes.Exportar("Reporte_Partidos", CrearTextoReporte()); }
   catch(Exception ex) { MessageBox.Show(ex.Message); }
  }
  private string CrearTextoReporte()
  {
    var (inicio,fin)=GeneradorReportes.Fechas(monthCalendar1);
    var tabla=GeneradorReportes.Consultar(conexion,@"SELECT p.Partido_ID, p.Partido_Fecha, p.Partido_Hora, p.Partido_Estado,
 el.Equipo_Nombre AS Local, ev.Equipo_Nombre AS Visitante, e.Estadio_Nombre,
 (SELECT COUNT(*) FROM Asiento a JOIN Area ar ON a.Area_ID=ar.Area_ID WHERE ar.Estadio_ID=p.Estadio_ID) AS Capacidad,
 (SELECT COUNT(*) FROM Ticket t WHERE t.Partido_ID=p.Partido_ID AND t.Ticket_Estado NOT IN ('Cancelado','Cancelada')) AS Ocupados
 FROM Partido p JOIN Equipos el ON el.Equipo_ID=p.Equipo_ID_Local
 JOIN Equipos ev ON ev.Equipo_ID=p.Equipo_ID_Visitante JOIN Estadio e ON e.Estadio_ID=p.Estadio_ID
 WHERE p.Partido_Fecha BETWEEN @inicio AND @fin ORDER BY p.Partido_Fecha,p.Partido_ID",("@inicio",inicio),("@fin",fin));
    string encabezado="";
    int capacidad=0,ocupados=0;
                foreach(System.Data.DataRow fila in tabla.Rows){capacidad+=Convert.ToInt32(fila["Capacidad"]);ocupados+=Convert.ToInt32(fila["Ocupados"]);}
                encabezado=$"Total de partidos: {tabla.Rows.Count}\nAsientos ocupados: {ocupados}\nDisponibles: {capacidad-ocupados}\n";
    return "REPORTE DE PARTIDOS\nPeriodo: "+inicio.ToString("yyyy-MM-dd")+" a "+fin.ToString("yyyy-MM-dd")+"\n"+encabezado+"\n"+GeneradorReportes.Formatear(tabla);
  }
 }
}
