using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using PIA_PA.Comun.Data;
using PIA_PA.Menus;
using PIA_PA.Entidades.Usuario;

namespace PIA_PA
{
 public partial class ModificarUsuario : UserControl
 {
  private readonly IUsuarioService _service;
  public ModificarUsuario(IUsuarioService service)
  {
   InitializeComponent(); _service = service;
   btnSeleccionar.Click+=(_,_)=>Buscar();btnModificar.Click+=(_,_)=>Guardar();
  }
  private void Ejecutar(Action accion)
  {
   try { accion(); }
   catch(Exception ex) { MessageBox.Show(ex.Message, "TicketHamster"); }
  }
  private void Buscar()=>Ejecutar(()=>{if(!int.TryParse(txtID1.Text,out int id))throw new Exception("ID inválido.");var d=_service.TablaOperacion(115,("@id",id));if(d.Rows.Count==0)throw new Exception("No encontrado.");var x=d.Rows[0];txtID2.Text=id.ToString();txtNombre1.Text=x["Usuario_Nombre"].ToString();txtNombre2.Text=x["Usuario_Nombre_2"].ToString();txtNombre3.Text=x["Usuario_Apellido"].ToString();txtNombre4.Text=x["Usuario_Apellido_2"].ToString();txtCorreo.Text=x["Usuario_Email"].ToString();});private void Guardar()=>Ejecutar(()=>{if(!int.TryParse(txtID2.Text,out int id))throw new Exception("Selecciona un usuario.");int n=_service.EjecutarOperacion(116,("@n1",txtNombre1.Text.Trim()),("@n2",txtNombre2.Text.Trim()),("@a1",txtNombre3.Text.Trim()),("@a2",txtNombre4.Text.Trim()),("@mail",txtCorreo.Text.Trim()),("@id",id));if(n==0)throw new Exception("Registro no encontrado.");MessageBox.Show("Usuario modificado.");});
 }
}
