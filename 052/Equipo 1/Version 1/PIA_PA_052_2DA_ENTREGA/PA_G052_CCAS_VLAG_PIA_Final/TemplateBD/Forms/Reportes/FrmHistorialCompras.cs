using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms.Reportes
{
    public partial class FrmHistorialCompras : Form
    {
        private readonly IReporteService _reporteService;

        public int IdCliente { get; set; }

        public FrmHistorialCompras(IReporteService reporteService)
        {
            InitializeComponent();
            _reporteService = reporteService;
        }

        private void FrmHistorialCompras_Load(object sender, EventArgs e)
        {
            try
            {
                
                var historial = _reporteService.ObtenerHistorialPorCliente(IdCliente, "fecha_compra");

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = historial;

                //tabla
                dataGridView1.Columns[0].DataPropertyName = "IdVenta";
                dataGridView1.Columns[1].DataPropertyName = "FechaVenta";
                dataGridView1.Columns[2].DataPropertyName = "Partido";
                dataGridView1.Columns[3].DataPropertyName = "Estadio";
                dataGridView1.Columns[4].DataPropertyName = "Zona";
                dataGridView1.Columns[5].DataPropertyName = "FechaPartido";
                dataGridView1.Columns[6].DataPropertyName = "CantidadBoletos";
                dataGridView1.Columns[7].DataPropertyName = "Subtotal";
                dataGridView1.Columns[8].DataPropertyName = "Total";
                dataGridView1.Columns[9].DataPropertyName = "MetodoPago";
                dataGridView1.Columns[10].DataPropertyName = "FolioFactura";
                dataGridView1.Columns[11].DataPropertyName = "EstadoFactura";

                
                dataGridView1.Columns[1].DefaultCellStyle.Format = "dd/MMM/yyyy";
                dataGridView1.Columns[5].DefaultCellStyle.Format = "dd/MMM/yyyy";
                dataGridView1.Columns[7].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns[8].DefaultCellStyle.Format = "C2";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}