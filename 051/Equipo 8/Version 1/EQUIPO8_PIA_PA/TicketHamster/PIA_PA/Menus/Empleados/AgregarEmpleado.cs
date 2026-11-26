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
 public partial class AgregarEmpleado : UserControl
 {
  private readonly IUsuarioService _service;
  public AgregarEmpleado(IUsuarioService service)
  {
   InitializeComponent(); _service = service;
   btnGuardar.Click+=(_,_)=>Guardar();
  }
  private void Ejecutar(Action accion)
  {
   try { accion(); }
   catch(Exception ex) { MessageBox.Show(ex.Message, "TicketHamster"); }
  }
  private void Guardar()=>Ejecutar(()=>{if(string.IsNullOrWhiteSpace(txtNombre1.Text)||string.IsNullOrWhiteSpace(txtNombre3.Text)||string.IsNullOrWhiteSpace(txtCorreo.Text)||string.IsNullOrWhiteSpace(txtContrasena.Text))throw new Exception("Completa nombre, apellido, correo y contraseña.");
 _service.TransaccionOperacion(tx=>{int id=tx.Siguiente("Usuario","Usuario_ID");int pass=tx.Siguiente("Contrasena","Contrasena_ID");
 tx.SQL(113,("@id",id),("@n1",txtNombre1.Text.Trim()),("@n2",txtNombre2.Text.Trim()),("@a1",txtNombre3.Text.Trim()),("@a2",txtNombre4.Text.Trim()),("@tipo","Operativo"),("@email",txtCorreo.Text.Trim()),("@pass",pass));
 tx.SQL(114,("@id",pass),("@clave",txtContrasena.Text),("@usuario",id));
 });MessageBox.Show("Empleado agregado.");});
 }
}
