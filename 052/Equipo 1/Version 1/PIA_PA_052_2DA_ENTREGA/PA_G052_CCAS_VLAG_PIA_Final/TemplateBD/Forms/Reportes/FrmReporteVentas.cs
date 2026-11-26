using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TemplateCSharp.Forms.Reportes
{
    public partial class FrmReporteVentas : Form
    {
        private readonly IReporteService _reporteService;
        private readonly IEstadioService _estadioService;
        private readonly IPartidoService _partidoService;

        public FrmReporteVentas(IReporteService reporteService, IEstadioService estadioService, IPartidoService partidoService)
        {
            InitializeComponent();
            _reporteService = reporteService;
            _estadioService = estadioService;
            _partidoService = partidoService;
        }

        private void FrmReporteVentas_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Today.AddDays(-30);
            dtpFechaFinal.Value = DateTime.Today;
            CargarCombos();
        }

        private void CargarCombos()
        {
            try
            {
                //combo de Estadios
                var estadios = _estadioService.ObtenerEstadios();
                estadios.Insert(0, new Estadio { IdEstadio = 0, Nombre = "Todos" });
                cmbEstadios.DataSource = estadios;
                cmbEstadios.DisplayMember = "Nombre";
                cmbEstadios.ValueMember = "IdEstadio";

                //Partidos
                var partidos = _partidoService.ObtenerPartidos();
                //Creamos una lista anónima para el combo
                var listaPartidos = new List<dynamic> { new { IdPartido = 0, NombreMostrar = "Todos" } };
                foreach (var p in partidos)
                {
                    listaPartidos.Add(new { IdPartido = p.IdPartido, NombreMostrar = $"{p.NombreEquipoLocal} vs {p.NombreEquipoVisitante}" });
                }

                cmbPartidos.DataSource = listaPartidos;
                cmbPartidos.DisplayMember = "NombreMostrar"; // <- propiedad que creamos
                cmbPartidos.ValueMember = "IdPartido";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar filtros: " + ex.Message);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador)
            {
                MessageBox.Show("Solo los administradores pueden ver este reporte.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                int? idEstadio = (int?)cmbEstadios.SelectedValue == 0 ? null : (int?)cmbEstadios.SelectedValue;
                int? idPartido = (int?)cmbPartidos.SelectedValue == 0 ? null : (int?)cmbPartidos.SelectedValue;

                var ventas = _reporteService.ObtenerReporteVentas(dtpFechaInicio.Value.Date, dtpFechaFinal.Value.Date, idEstadio, idPartido);

                // CONFIGURAR COLUMNAS DE VENTAS
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Folio", DataPropertyName = "IdVenta", Width = 250 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha", DataPropertyName = "FechaVenta", Width = 100, DefaultCellStyle = { Format = "dd/MMM/yyyy" } });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estadio", DataPropertyName = "Estadio", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Partido", DataPropertyName = "Partido", Width = 200 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Zona", DataPropertyName = "Zona", Width = 100 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cantidad", DataPropertyName = "CantidadBoletos", Width = 80 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "Subtotal", Width = 100, DefaultCellStyle = { Format = "C2" } });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Método Pago", DataPropertyName = "MetodoPago", Width = 120 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "Total", Width = 100, DefaultCellStyle = { Format = "C2" } });

                dataGridView1.DataSource = ventas;

                // Totales
                decimal totalDinero = ventas.Sum(v => v.Total);
                int totalBoletos = ventas.Sum(v => v.CantidadBoletos);

                label7.Text = totalDinero.ToString("C2");
                label8.Text = totalBoletos.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnTopPartidos_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador) return;
            try
            {
                var top = _reporteService.ObtenerTopPartidos(10);
                ConfigurarGrid(top, "TopPartidos");
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnClientesFrecuentes_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador) return;
            try
            {
                var top = _reporteService.ObtenerClientesFrecuentes(10);
                ConfigurarGrid(top, "ClientesFrecuentes");
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnVentasPorZona_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador) return;
            try
            {
                var lista = _reporteService.ObtenerVentasPorZona();
                ConfigurarGrid(lista, "VentasPorZona");
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        //metodo auxiliar para configurar columnas dinámicamente
        private void ConfigurarGrid(object dataSource, string tipo)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            if (tipo == "TopPartidos")
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Partido", DataPropertyName = "Partido", Width = 200 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estadio", DataPropertyName = "Estadio", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha", DataPropertyName = "FechaPartido", Width = 100, DefaultCellStyle = { Format = "dd/MMM/yyyy" } });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Boletos", DataPropertyName = "BoletosVendidos", Width = 80 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ingreso", DataPropertyName = "IngresoTotal", Width = 100, DefaultCellStyle = { Format = "C2" } });
            }
            else if (tipo == "ClientesFrecuentes")
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cliente", DataPropertyName = "Cliente", Width = 200 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Correo", DataPropertyName = "Correo", Width = 200 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Compras", DataPropertyName = "TotalCompras", Width = 80 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total Gastado", DataPropertyName = "TotalGastado", Width = 120, DefaultCellStyle = { Format = "C2" } });
            }
            else if (tipo == "VentasPorZona")
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Zona", DataPropertyName = "Zona", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estadio", DataPropertyName = "Estadio", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio Base", DataPropertyName = "PrecioBase", Width = 100, DefaultCellStyle = { Format = "C2" } });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vendidos", DataPropertyName = "BoletosVendidos", Width = 80 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ingreso", DataPropertyName = "IngresoTotal", Width = 100, DefaultCellStyle = { Format = "C2" } });
            }

            dataGridView1.DataSource = dataSource;
        }


        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e) { }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar. Genere el reporte primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Archivo de Excel (*.xls)|*.xls", 
                FileName = $"Reporte_Ventas_{DateTime.Now:yyyyMMdd}.xls",
                Title = "Exportar Reporte de Ventas"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
                        sw.WriteLine("<head>");
                        sw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                        sw.WriteLine("<style>");
                        sw.WriteLine("table { border-collapse: collapse; font-family: Arial, sans-serif; }");
                        sw.WriteLine("th { background-color: #4CAF50; color: white; padding: 8px; border: 1px solid #ddd; text-align: left; }");
                        sw.WriteLine("td { padding: 8px; border: 1px solid #ddd; }");
                        sw.WriteLine("</style>");
                        sw.WriteLine("</head>");
                        sw.WriteLine("<body>");
                        sw.WriteLine("<table>");

                        // Leer los encabezados reales del grid actual
                        sw.WriteLine("<tr>");
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            sw.WriteLine($"<th>{col.HeaderText}</th>");
                        }
                        sw.WriteLine("</tr>");

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                sw.WriteLine("<tr>");
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    sw.WriteLine($"<td>{cell.FormattedValue}</td>");
                                }
                                sw.WriteLine("</tr>");
                            }
                        }

                        sw.WriteLine("</table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                    }

                    MessageBox.Show("Reporte exportado con éxito.\n\nSi Excel te avisa que 'el formato del archivo no coincide con la extensión', dale clic en 'Sí' para abrirlo.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el archivo automáticamente
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
