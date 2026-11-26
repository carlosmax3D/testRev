using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TemplateCSharp.Forms.Reportes
{
    public partial class FrmReporteOcupacion : Form
    {
        private readonly IReporteService _reporteService;
        private readonly IPartidoService _partidoService;
        private readonly IZonaService _zonaService;

        public FrmReporteOcupacion(IReporteService reporteService, IPartidoService partidoService, IZonaService zonaService)
        {
            InitializeComponent();
            _reporteService = reporteService;
            _partidoService = partidoService;
            _zonaService = zonaService;
        }

        private void FrmReporteOcupacion_Load(object sender, EventArgs e)
        {
            try
            {
                // Partidos
                var partidos = _partidoService.ObtenerPartidos();
                // Creamos una lista anónima para el combo pq no podemos usar enfrentamiento
                var listaPartidos = new List<dynamic> { new { IdPartido = 0, NombreMostrar = "Todos" } };
                foreach (var p in partidos)
                {
                    listaPartidos.Add(new { IdPartido = p.IdPartido, NombreMostrar = $"{p.NombreEquipoLocal} vs {p.NombreEquipoVisitante}" });
                }

                cmbPartidos.DataSource = listaPartidos;
                cmbPartidos.DisplayMember = "NombreMostrar"; 
                cmbPartidos.ValueMember = "IdPartido";

                // Zonas
                var zonas = _zonaService.ObtenerZonas();
                zonas.Insert(0, new Zona { IdZona = 0, NombreZona = "Todas" });
                cmbZona.DataSource = zonas;
                cmbZona.DisplayMember = "NombreZona";
                cmbZona.ValueMember = "IdZona";

                cmbDisponible.SelectedIndex = 0; // "Todas" por defecto
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar catálogos: " + ex.Message);
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
                int? idPartido = (int?)cmbPartidos.SelectedValue == 0 ? null : (int?)cmbPartidos.SelectedValue;
                int? idZona = (int?)cmbZona.SelectedValue == 0 ? null : (int?)cmbZona.SelectedValue;

                // Manejo del ComboBox de Disponibilidad
                bool? soloDisponibles = null;
                if (cmbDisponible.SelectedIndex == 1) soloDisponibles = true;  // Con disponibilidad
                if (cmbDisponible.SelectedIndex == 2) soloDisponibles = false; // Sin disponibilidad

                var reporte = _reporteService.ObtenerReporteOcupacion(idPartido, idZona, soloDisponibles);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = reporte;

                // Mapeo
                dataGridView1.Columns[0].DataPropertyName = "Partido";
                dataGridView1.Columns[1].DataPropertyName = "Zona";
                dataGridView1.Columns[2].DataPropertyName = "PrecioBase";
                dataGridView1.Columns[3].DataPropertyName = "AsientosTotales";
                dataGridView1.Columns[4].DataPropertyName = "AsientosVendidos";
                dataGridView1.Columns[5].DataPropertyName = "AsientosDisponibles";
                dataGridView1.Columns[6].DataPropertyName = "PorcentajeOcupacion";

                // Formatos
                dataGridView1.Columns[2].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns[6].DefaultCellStyle.Format = "N2";

                // Resumen
                if (reporte.Count > 0)
                {
                    lbTotalAsientos.Text = reporte.Sum(r => r.AsientosTotales).ToString();
                    lbTotalVendidos.Text = reporte.Sum(r => r.AsientosVendidos).ToString();
                    lbTotalDisponibles.Text = reporte.Sum(r => r.AsientosDisponibles).ToString();

                    // Promedio de ocupación
                    decimal promedioOcupacion = reporte.Average(r => r.PorcentajeOcupacion);
                    lbOcupacionGeneral.Text = promedioOcupacion.ToString("N2") + " %";
                }
                else
                {
                    lbTotalAsientos.Text = lbTotalVendidos.Text = lbTotalDisponibles.Text = "0";
                    lbOcupacionGeneral.Text = "0.00 %";
                    MessageBox.Show("No hay datos con esos filtros.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}