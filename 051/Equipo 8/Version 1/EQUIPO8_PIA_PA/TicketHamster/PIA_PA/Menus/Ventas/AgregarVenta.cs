using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Text;
using PIA_PA.Comun.Data;
using PIA_PA.Menus;
using PIA_PA.Entidades.Venta_Info;

namespace PIA_PA
{
 public partial class AgregarVenta : UserControl
 {
  private readonly IVenta_InfoService _service;
  private readonly List<(int asiento,double precio)> seleccionados=new();
  private bool cargando;
  public AgregarVenta(IVenta_InfoService service)
  {
   InitializeComponent();_service=service;
   txtBoxAsientos.ReadOnly=true;
   cBoxPartido.SelectedIndexChanged+=(_,_)=>CambiarPartido();
   cBoxAsientoArea.SelectedIndexChanged+=(_,_)=>CambiarArea();
   cBoxAsientoFila.SelectedIndexChanged+=(_,_)=>CambiarFila();
   btnAgregarAsiento.Click+=(_,_)=>AgregarAsientoSeleccionado();
   radioBtnTarjeta.CheckedChanged+=(_,_)=>CambiarPago();
   chkBoxFacturar.CheckedChanged+=(_,_)=>CambiarFactura();
   radioBtnEfectivo.Checked=true;
   CambiarFactura();CambiarPago();
   try {Cargar();}catch(Exception ex){MessageBox.Show(ex.Message);}
  }
  private void Cargar()
  {
   cargando=true;
   var clientes=_service.TablaOperacion(133);
   comboBox1.DataSource=clientes;comboBox1.DisplayMember="Cliente_ID";comboBox1.ValueMember="Cliente_ID";
   var partidos=_service.TablaOperacion(134);
   cBoxPartido.DataSource=partidos;cBoxPartido.DisplayMember="Partido_ID";cBoxPartido.ValueMember="Partido_ID";
   cargando=false;CambiarPartido();
  }
  private int Id(ComboBox cb)=>cb.SelectedValue is int n?n:throw new Exception("Selecciona un valor.");
  private void CambiarPartido()
  {
   if(cargando)return;
   try {seleccionados.Clear();MostrarTotal();int partido=Id(cBoxPartido);
    var d=_service.TablaOperacion(135,("@id",partido));
    cargando=true;cBoxAsientoArea.DataSource=d;cBoxAsientoArea.DisplayMember="Area_Nombre";cBoxAsientoArea.ValueMember="Area_ID";cargando=false;CambiarArea();
   }catch(Exception ex){MessageBox.Show(ex.Message);}
  }
  private void CambiarArea()
  {
   if(cargando)return;
   try {int area=Id(cBoxAsientoArea);int partido=Id(cBoxPartido);
    var d=_service.TablaOperacion(136,("@area",area),("@partido",partido));
    cargando=true;cBoxAsientoFila.DataSource=d;cBoxAsientoFila.DisplayMember="Asiento_Fila";cBoxAsientoFila.ValueMember="Asiento_Fila";cargando=false;CambiarFila();
   }catch(Exception ex){MessageBox.Show(ex.Message);}
  }
  private void CambiarFila()
  {
   if(cargando)return;
   try {int area=Id(cBoxAsientoArea),partido=Id(cBoxPartido),fila=Id(cBoxAsientoFila);
    var d=_service.TablaOperacion(137,("@area",area),("@fila",fila),("@partido",partido));
    cBoxAsientoColumna.DataSource=d;cBoxAsientoColumna.DisplayMember="Asiento_Numero";cBoxAsientoColumna.ValueMember="Asiento_Numero";
   }catch(Exception ex){MessageBox.Show(ex.Message);}
  }
  private void AgregarAsientoSeleccionado()
  {
   try {
    int partido=Id(cBoxPartido),area=Id(cBoxAsientoArea),fila=Id(cBoxAsientoFila),numero=Id(cBoxAsientoColumna);
    var id=_service.ValorOperacion(138,("@a",area),("@f",fila),("@n",numero));
    if(id==null)throw new Exception("Asiento inexistente.");int asiento=Convert.ToInt32(id);
    if(seleccionados.Any(x=>x.asiento==asiento))throw new Exception("Ya agregaste ese asiento.");
    var precio=_service.ValorOperacion(139,("@p",partido),("@a",area));
    if(precio==null)throw new Exception("Primero registra el precio de esta área para este partido.");
    seleccionados.Add((asiento,Convert.ToDouble(precio)));MostrarTotal();
    if(MessageBox.Show("Asiento agregado. ¿Finalizar y guardar la venta ahora?\nSelecciona No para agregar más asientos.","Venta",MessageBoxButtons.YesNo)==DialogResult.Yes)GuardarVenta();
   }catch(Exception ex){MessageBox.Show(ex.Message);}
  }
  private void MostrarTotal()
  {
   double subtotal=seleccionados.Sum(x=>x.precio);double iva=Math.Round(subtotal*.16,2);double total=subtotal+iva;
   txtBoxAsientos.Text=string.Join(", ",seleccionados.Select(x=>x.asiento));
   txtSubtotal.Text=subtotal.ToString("C2");txtIVA.Text=iva.ToString("C2");txtTotal.Text=total.ToString("C2");
  }
  private void CambiarPago()
  {
   bool tarjeta=radioBtnTarjeta.Checked;
   txtBoxTarjeta.Enabled=tarjeta;txtBoxCVV.Enabled=tarjeta;txtBoxFechaExpiracion.Enabled=tarjeta;
  }
  private void CambiarFactura()
  {
   bool factura=chkBoxFacturar.Checked;
   txtBoxRFC.Enabled=factura;txtBoxNombreLegal.Enabled=factura;textBox1.Enabled=factura;txtDireccionLegal.Enabled=factura;
  }
  private void GuardarVenta()
  {
   if(seleccionados.Count==0)throw new Exception("Agrega un asiento.");
   int partido=Id(cBoxPartido),cliente=Id(comboBox1);
   if(!radioBtnTarjeta.Checked&&!radioBtnEfectivo.Checked)throw new Exception("Selecciona método de pago.");
   if(radioBtnTarjeta.Checked&&(string.IsNullOrWhiteSpace(txtBoxTarjeta.Text)||string.IsNullOrWhiteSpace(txtBoxCVV.Text)||string.IsNullOrWhiteSpace(txtBoxFechaExpiracion.Text)))throw new Exception("Completa los datos de tarjeta.");
   // Datos de tarjeta y CVV nunca se guardan en la base de datos.
   double subtotal=seleccionados.Sum(x=>x.precio),iva=Math.Round(subtotal*.16,2),total=subtotal+iva;
   int ventaGuardada=0;
   string rfcFactura=txtBoxRFC.Text.Trim();
   string nombreFactura=txtBoxNombreLegal.Text.Trim();
   string regimenFactura=textBox1.Text.Trim();
   string direccionFactura=txtDireccionLegal.Text.Trim();
   if(chkBoxFacturar.Checked && (rfcFactura.Length<12 || rfcFactura.Length>13 ||
       nombreFactura.Length==0 || regimenFactura.Length==0 || direccionFactura.Length==0))
       throw new Exception("Completa RFC válido, nombre legal, régimen fiscal y dirección.");
   _service.TransaccionOperacion(tx=>{
    int usuario=SesionActual.UsuarioId;
    if(usuario<=0)throw new Exception("Debes iniciar sesión para vender.");
    int emisor=Convert.ToInt32(tx.SQL(140)??throw new Exception("Primero registra un emisor."));
    string rfc;
    if(chkBoxFacturar.Checked){rfc=txtBoxRFC.Text.Trim();if(rfc.Length==0)throw new Exception("Escribe RFC de facturación.");}
    else {rfc=Convert.ToString(tx.SQL(141))??throw new Exception("Primero registra un pagador.");}
    var existe=tx.SQL(142,("@r",rfc));
    if(existe==null)throw new Exception("El RFC del pagador debe estar registrado antes de facturar.");
    foreach(var item in seleccionados){
     int ocupado=Convert.ToInt32(tx.SQL(143,("@p",partido),("@a",item.asiento)));
     if(ocupado>0)throw new Exception("Uno de los asientos ya está vendido.");
    }
    int pago=tx.Siguiente("Pago","Pago_ID");
    tx.SQL(144,("@id",pago),("@metodo",radioBtnTarjeta.Checked?"Tarjeta":"Efectivo"),("@total",total));
    int venta=tx.Siguiente("Venta_Info","Venta_Info_ID");
    ventaGuardada=venta;
    tx.SQL(145,("@id",venta),("@subtotal",subtotal),("@iva",iva),("@total",total),("@usuario",usuario),("@pago",pago),("@rfc",rfc),("@emisor",emisor));
    int ticket=tx.Siguiente("Ticket","Ticket_ID");
    foreach(var item in seleccionados){tx.SQL(146,("@id",ticket++),("@cliente",cliente),("@venta",venta),("@partido",partido),("@asiento",item.asiento));}
   });
   if(chkBoxFacturar.Checked)
   {
    string contenido=$"COMPROBANTE DE VENTA - TicketHamster\n"+
       $"Documento informativo: NO es un CFDI timbrado.\n"+
       $"Venta: {ventaGuardada}\nFecha: {DateTime.Now:yyyy-MM-dd}\n"+
       $"Cliente ID: {cliente}\nPartido ID: {partido}\n"+
       $"RFC: {rfcFactura}\nNombre legal: {nombreFactura}\n"+
       $"Régimen fiscal: {regimenFactura}\nDirección: {direccionFactura}\n"+
       $"Método: {(radioBtnTarjeta.Checked ? "Tarjeta" : "Efectivo")}\n"+
       $"Subtotal: {subtotal:C2}\nIVA: {iva:C2}\nTotal: {total:C2}\n"+
       "Boletos (asientos ID): "+string.Join(", ",seleccionados.Select(x=>x.asiento));
    GeneradorReportes.Exportar("Comprobante_Venta_"+ventaGuardada,contenido);
   }
   seleccionados.Clear();MostrarTotal();CambiarArea();MessageBox.Show("Venta registrada correctamente.");
  }
  private void label2_Click(object sender,EventArgs e){}
  private void comboBox2_SelectedIndexChanged(object sender,EventArgs e){}
 }
}
