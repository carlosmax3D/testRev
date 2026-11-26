using System;
using System.Data;
using System.Windows.Forms;
using PIA_PA.Comun.Data;
using PIA_PA.Menus;
using PIA_PA.Entidades.Cancelacion;
namespace PIA_PA
{
 public partial class Historial:UserControl
 {
  private readonly ICancelacionService _service;private DataTable historial=new();
  public Historial(ICancelacionService service){InitializeComponent();_service=service;btnActualizar.Click+=(_,_)=>Cargar();btnBuscar.Click+=(_,_)=>Buscar();Cargar();}
  private void Cargar(){try{historial=_service.TablaOperacion(151);gridCatalogo.DataSource=null;gridCatalogo.Columns.Clear();gridCatalogo.AutoGenerateColumns=true;gridCatalogo.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.None;gridCatalogo.ScrollBars=ScrollBars.Both;gridCatalogo.ReadOnly=true;gridCatalogo.DataSource=historial;foreach(DataGridViewColumn columna in gridCatalogo.Columns)columna.Width=140;if(gridCatalogo.Columns.Contains("Motivo"))gridCatalogo.Columns["Motivo"].Width=250;foreach(DataGridViewColumn columna in gridCatalogo.Columns)columna.MinimumWidth=columna.Width;ConsultaGridHelper.AjustarColumnas(gridCatalogo);cBoxConsultarPartidos.Items.Clear();foreach(DataColumn col in historial.Columns)cBoxConsultarPartidos.Items.Add(col.ColumnName);if(cBoxConsultarPartidos.Items.Count>0)cBoxConsultarPartidos.SelectedIndex=0;}catch(Exception ex){MessageBox.Show(ex.Message);}}
  private void Buscar(){if(cBoxConsultarPartidos.SelectedItem==null)return;string col=cBoxConsultarPartidos.SelectedItem.ToString()!;string value=txtBoxConsultarPartidos.Text.Replace("'","''").Replace("[","[[]").Replace("%","[%]");historial.DefaultView.RowFilter=$"CONVERT([{col}], 'System.String') LIKE '%{value}%'";}
  private void btnConsultarPartidos_Click(object sender,EventArgs e)=>Buscar();
 }
}
