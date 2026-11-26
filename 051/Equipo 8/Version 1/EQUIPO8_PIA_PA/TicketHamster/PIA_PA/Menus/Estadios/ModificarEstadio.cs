using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using PIA_PA.Comun.Data;
using PIA_PA.Menus;
using PIA_PA.Entidades.Estadio;

namespace PIA_PA
{
 public partial class ModificarEstadio : UserControl
 {
  private readonly IEstadioService _service;
  public ModificarEstadio(IEstadioService service)
  {
   InitializeComponent(); _service = service;
   btnSeleccionar.Click+=(_,_)=>Buscar();btnModificar2.Click+=(_,_)=>GuardarEstadio();btnModificar3.Click+=(_,_)=>GuardarArea();cBArea.SelectedIndexChanged+=(_,_)=>VerArea();
  }
  private void Ejecutar(Action accion)
  {
   try { accion(); }
   catch(Exception ex) { MessageBox.Show(ex.Message, "TicketHamster"); }
  }
  private void Buscar()=>Ejecutar(()=>{if(!int.TryParse(txtID1.Text,out int id))throw new Exception("ID inválido.");var d=_service.TablaOperacion(121,("@id",id));if(d.Rows.Count==0)throw new Exception("Estadio no encontrado.");txtID2.Text=id.ToString();txtNombre8.Text=d.Rows[0]["Estadio_Nombre"].ToString();txtCiudad.Text=d.Rows[0]["Estadio_Ciudad"].ToString();txtCapacidad.Text=d.Rows[0]["Estadio_Capacidad"].ToString();CargarAreas(id);});private void CargarAreas(int id){var d=_service.TablaOperacion(122,("@id",id));cBArea.DataSource=d;cBArea.DisplayMember="Area_Nombre";cBArea.ValueMember="Area_ID";txtArea.Text=d.Rows.Count.ToString();}
 private int AreaId()=>cBArea.SelectedValue is int id?id:throw new Exception("Selecciona un área.");
 private void VerArea()=>Ejecutar(()=>{if(cBArea.SelectedValue is not int id)return;var d=_service.TablaOperacion(123,("@id",id));if(d.Rows.Count==0)return;txtNombre5.Text=d.Rows[0][0].ToString();var asientos=_service.TablaOperacion(124,("@id",id));txtFila.Text=asientos.Rows.Count==0?"0":asientos.AsEnumerable().Max(x=>Convert.ToInt32(x[0])).ToString();txtColumna.Text=asientos.Rows.Count==0?"0":asientos.AsEnumerable().Max(x=>Convert.ToInt32(x[1])).ToString();});private void GuardarEstadio()=>Ejecutar(()=>{if(!int.TryParse(txtID2.Text,out int id)||!int.TryParse(txtCapacidad.Text,out int capacidad))throw new Exception("Selecciona un estadio y escribe una capacidad válida.");_service.EjecutarOperacion(125,("@n",txtNombre8.Text.Trim()),("@c",txtCiudad.Text.Trim()),("@cap",capacidad),("@id",id));MessageBox.Show("Estadio modificado.");});
 private void GuardarArea()=>Ejecutar(()=>{int id=AreaId();if(!int.TryParse(txtFila.Text,out int filas)||!int.TryParse(txtColumna.Text,out int columnas)||filas<1||columnas<1)throw new Exception("Filas y columnas inválidas.");int capacidad=checked(filas*columnas);_service.TransaccionOperacion(tx=>{int actuales=Convert.ToInt32(tx.SQL(126,("@id",id)));if(actuales!=capacidad)throw new Exception("No se puede cambiar la distribución de asientos existentes desde este formulario. Mantén filas y columnas actuales.");tx.SQL(127,("@n",txtNombre5.Text.Trim()),("@cap",capacidad),("@id",id));});MessageBox.Show("Área modificada.");});
 }
}
