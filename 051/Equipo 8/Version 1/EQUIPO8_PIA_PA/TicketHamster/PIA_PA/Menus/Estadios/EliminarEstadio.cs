using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using PIA_PA.Comun.Data;
using PIA_PA.Menus;
using PIA_PA.Entidades.Estadio;

namespace PIA_PA.Menus
{
 public partial class EliminarEstadio : UserControl
 {
  private readonly IEstadioService _service;
  public EliminarEstadio(IEstadioService service)
  {
   InitializeComponent(); _service = service;
   btnSeleccionar.Click+=(_,_)=>Buscar();btnEliminar2.Click+=(_,_)=>EliminarEstadioRegistro();btnEliminar3.Click+=(_,_)=>EliminarArea();cBArea.SelectedIndexChanged+=(_,_)=>VerArea();
  }
  private void Ejecutar(Action accion)
  {
   try { accion(); }
   catch(Exception ex) { MessageBox.Show(ex.Message, "TicketHamster"); }
  }
  private void Buscar()=>Ejecutar(()=>{if(!int.TryParse(txtID1.Text,out int id))throw new Exception("ID inválido.");var d=_service.TablaOperacion(121,("@id",id));if(d.Rows.Count==0)throw new Exception("Estadio no encontrado.");txtID2.Text=id.ToString();txtNombre8.Text=d.Rows[0]["Estadio_Nombre"].ToString();txtCiudad.Text=d.Rows[0]["Estadio_Ciudad"].ToString();txtCapacidad.Text=d.Rows[0]["Estadio_Capacidad"].ToString();CargarAreas(id);});private void CargarAreas(int id){var d=_service.TablaOperacion(122,("@id",id));cBArea.DataSource=d;cBArea.DisplayMember="Area_Nombre";cBArea.ValueMember="Area_ID";txtArea.Text=d.Rows.Count.ToString();}
 private int AreaId()=>cBArea.SelectedValue is int id?id:throw new Exception("Selecciona un área.");
 private void VerArea()=>Ejecutar(()=>{if(cBArea.SelectedValue is not int id)return;var d=_service.TablaOperacion(123,("@id",id));if(d.Rows.Count==0)return;txtNombre5.Text=d.Rows[0][0].ToString();var asientos=_service.TablaOperacion(124,("@id",id));txtFila.Text=asientos.Rows.Count==0?"0":asientos.AsEnumerable().Max(x=>Convert.ToInt32(x[0])).ToString();txtColumna.Text=asientos.Rows.Count==0?"0":asientos.AsEnumerable().Max(x=>Convert.ToInt32(x[1])).ToString();});private void EliminarArea()=>Ejecutar(()=>{int id=AreaId();if(MessageBox.Show("¿Eliminar área y sus asientos?","Confirmar",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;_service.TransaccionOperacion(tx=>{tx.SQL(128,("@id",id));tx.SQL(129,("@id",id));});CargarAreas(int.Parse(txtID2.Text));MessageBox.Show("Área eliminada.");});
 private void EliminarEstadioRegistro()=>Ejecutar(()=>{if(!int.TryParse(txtID2.Text,out int id))throw new Exception("Selecciona un estadio.");if(MessageBox.Show("¿Eliminar estadio y todas sus áreas?","Confirmar",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;_service.TransaccionOperacion(tx=>{tx.SQL(130,("@id",id));tx.SQL(131,("@id",id));tx.SQL(132,("@id",id));});MessageBox.Show("Estadio eliminado.");});
 }
}
