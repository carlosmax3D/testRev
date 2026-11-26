using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using PIA_PA.Comun.Data;
using PIA_PA.Menus;
using PIA_PA.Entidades.Cancelacion;

namespace PIA_PA
{
 public partial class Nueva : UserControl
 {
  private readonly ICancelacionService _service;
  public Nueva(ICancelacionService service)
  {
   InitializeComponent(); _service = service;
   btnEnviar.Click+=(_,_)=>Guardar();Cargar();
  }
  private void Ejecutar(Action accion)
  {
   try { accion(); }
   catch(Exception ex) { MessageBox.Show(ex.Message, "TicketHamster"); }
  }
  private void Cargar()=>Ejecutar(()=>{var d=_service.TablaOperacion(147);cBTicket.DataSource=d;cBTicket.DisplayMember="Ticket_ID";cBTicket.ValueMember="Ticket_ID";});
 private void Guardar()=>Ejecutar(()=>{if(cBTicket.SelectedValue is not int id)throw new Exception("Selecciona un boleto.");if(string.IsNullOrWhiteSpace(txtMotivo.Text))throw new Exception("Escribe el motivo.");_service.TransaccionOperacion(tx=>{int n=Convert.ToInt32(tx.SQL(148,("@id",id)));if(n==0)throw new Exception("Boleto ya cancelado o inexistente.");int cancel=tx.Siguiente("Cancelacion","Cancelacion_ID");tx.SQL(149,("@id",cancel),("@motivo",txtMotivo.Text.Trim()),("@ticket",id));tx.SQL(150,("@id",id));});MessageBox.Show("Boleto cancelado.");Cargar();});
 }
}
