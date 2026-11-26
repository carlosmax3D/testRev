using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using PIA_PA.Comun.Data;
using PIA_PA.Menus;
using PIA_PA.Entidades.Cliente;

namespace PIA_PA
{
 public partial class ModificarCliente : UserControl
 {
  private readonly IClienteService _service;
  public ModificarCliente(IClienteService service)
  {
   InitializeComponent(); _service = service;
   btnSeleccionar.Click+=(_,_)=>Buscar();btnModificar.Click+=(_,_)=>Guardar();
  }
  private void Ejecutar(Action accion)
  {
   try { accion(); }
   catch(Exception ex) { MessageBox.Show(ex.Message, "TicketHamster"); }
  }
  private void Buscar()=>Ejecutar(()=>{if(!int.TryParse(txtID1.Text,out int id))throw new Exception("ID inválido.");var d=_service.TablaOperacion(105,("@id",id));
   if(d.Rows.Count==0)throw new Exception("Cliente no encontrado");var x=d.Rows[0];
   txtID2.Text=id.ToString();txtRFC.Text=x["Cliente_RFC"].ToString();txtCorreo.Text=x["Cliente_Email"].ToString();
   txtNombre1.Text=x["Cliente_Nombre"].ToString();txtNombre2.Text=x["Cliente_Segundo_Nombre"].ToString();txtNombre3.Text=x["Cliente_Apellido_Paterno"].ToString();txtNombre4.Text=x["Cliente_Apellido_Materno"].ToString();
   txtBoxCalle.Text=x["Direccion_Calle"].ToString();txtBoxNumero.Text=x["Direccion_Numero"].ToString();txtBoxColonia.Text=x["Direccion_Colonia"].ToString();
   txtBoxTelefono.Text=x["Telefono_Fijo"].ToString();txtBoxCelular.Text=x["Telefono_Celular"].ToString();dtpFecha.Value=Convert.ToDateTime(x["Cliente_Fecha_Nacimiento"]);});
 private void Guardar()=>Ejecutar(()=>{if(string.IsNullOrWhiteSpace(txtRFC.Text)||string.IsNullOrWhiteSpace(txtCorreo.Text)||string.IsNullOrWhiteSpace(txtNombre1.Text)||string.IsNullOrWhiteSpace(txtNombre3.Text)||string.IsNullOrWhiteSpace(txtBoxCalle.Text))throw new Exception("Completa RFC, correo, nombre, apellido y calle.");
   if(!int.TryParse(txtBoxNumero.Text,out int numero))throw new Exception("Número de dirección inválido.");
   int? fijo=int.TryParse(txtBoxTelefono.Text,out int f)?f:null;int? celular=int.TryParse(txtBoxCelular.Text,out int cel)?cel:null;
   if(!int.TryParse(txtID2.Text,out int id))throw new Exception("Primero selecciona un cliente.");
   _service.TransaccionOperacion(tx=>{
    var di=tx.SQL(106,("@id",id));
    var tel=tx.SQL(107,("@id",id));
    if(di==null||tel==null)throw new Exception("Cliente no encontrado.");
    tx.SQL(108,("@r",txtRFC.Text.Trim()),("@m",txtCorreo.Text.Trim()),("@f",dtpFecha.Value.Date),("@n1",txtNombre1.Text.Trim()),("@n2",txtNombre2.Text.Trim()),("@a1",txtNombre3.Text.Trim()),("@a2",txtNombre4.Text.Trim()),("@id",id));
    tx.SQL(109,("@calle",txtBoxCalle.Text.Trim()),("@num",numero),("@col",txtBoxColonia.Text.Trim()),("@id",di));
    tx.SQL(110,("@f",fijo),("@cel",celular),("@id",tel));
   });MessageBox.Show("Cliente modificado.");});
 }
}
