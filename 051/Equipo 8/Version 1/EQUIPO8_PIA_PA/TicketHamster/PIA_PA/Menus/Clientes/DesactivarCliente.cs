using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using PIA_PA.Comun.Data;
using PIA_PA.Menus;
using PIA_PA.Entidades.Cliente;

namespace PIA_PA.Menus
{
 public partial class DesactivarCliente : UserControl
 {
  private readonly IClienteService _service;
  public DesactivarCliente(IClienteService service)
  {
   InitializeComponent(); _service = service;
   btnSeleccionar.Click+=(_,_)=>Buscar();btnEliminar.Click+=(_,_)=>Desactivar();
  }
  private void Ejecutar(Action accion)
  {
   try { accion(); }
   catch(Exception ex) { MessageBox.Show(ex.Message, "TicketHamster"); }
  }
  private void Buscar()=>Ejecutar(()=>{if(!int.TryParse(txtID1.Text,out int id))throw new Exception("ID inválido.");var d=_service.TablaOperacion(111,("@id",id));if(d.Rows.Count==0)throw new Exception("Cliente no encontrado.");txtRFC.Text=d.Rows[0][0].ToString();txtCorreo.Text=d.Rows[0][1].ToString();txtID2.Text=id.ToString();});
 private void Desactivar()=>Ejecutar(()=>{if(!int.TryParse(txtID2.Text,out int id))throw new Exception("Selecciona un registro.");if(MessageBox.Show("¿Desactivar el registro?","Confirmar",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;int n=_service.EjecutarOperacion(112,("@id",id));if(n==0)throw new Exception("Registro no encontrado.");MessageBox.Show("Registro desactivado.");});
 }
}
