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
 public partial class AgregarCliente : UserControl
 {
  private readonly IClienteService _service;
  public AgregarCliente(IClienteService service)
  {
   InitializeComponent(); _service = service;
   btnGuardar.Click+=(_,_)=>Guardar();
  }
  private void Ejecutar(Action accion)
  {
   try { accion(); }
   catch(Exception ex) { MessageBox.Show(ex.Message, "TicketHamster"); }
  }
  private void Guardar() => Ejecutar(()=>{if(string.IsNullOrWhiteSpace(txtRFC.Text)||string.IsNullOrWhiteSpace(txtCorreo.Text)||string.IsNullOrWhiteSpace(txtNombre1.Text)||string.IsNullOrWhiteSpace(txtNombre3.Text)||string.IsNullOrWhiteSpace(txtBoxCalle.Text))throw new Exception("Completa RFC, correo, nombre, apellido y calle.");
   if(!int.TryParse(txtBoxNumero.Text,out int numero))throw new Exception("Número de dirección inválido.");
   int? fijo=int.TryParse(txtBoxTelefono.Text,out int f)?f:null;int? celular=int.TryParse(txtBoxCelular.Text,out int cel)?cel:null;
   _service.TransaccionOperacion(tx=>{
    int di=tx.Siguiente("Direccion","Direccion_ID");
    int tel=tx.Siguiente("Telefono","Telefono_ID");
    int id=tx.Siguiente("Cliente","Cliente_ID");
    tx.SQL(101,("@id",di),("@n",numero),("@calle",txtBoxCalle.Text.Trim()),("@col",txtBoxColonia.Text.Trim()));
    tx.SQL(102,("@id",tel),("@f",fijo),("@cel",celular));
    tx.SQL(103,("@id",id),("@rfc",txtRFC.Text.Trim()),("@mail",txtCorreo.Text.Trim()),("@fecha",dtpFecha.Value.Date),("@n1",txtNombre1.Text.Trim()),("@n2",txtNombre2.Text.Trim()),("@a1",txtNombre3.Text.Trim()),("@a2",txtNombre4.Text.Trim()),("@di",di),("@tel",tel));
    if(!string.IsNullOrWhiteSpace(txtContrasena.Text)){int pass=tx.Siguiente("Contrasena","Contrasena_ID");tx.SQL(104,("@id",pass),("@pass",txtContrasena.Text),("@cliente",id));}
   });
   MessageBox.Show("Cliente agregado.");});
 }
}
