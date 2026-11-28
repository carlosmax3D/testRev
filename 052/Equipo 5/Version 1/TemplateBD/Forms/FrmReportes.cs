using System;
using System.Data;
using System.Windows.Forms;
using TemplateCSharp.Data.Repositories;

namespace TemplateCSharp.Forms;

public partial class FrmReportes : Form
{
    private readonly ReporteRepository _reporteRepo;

    public FrmReportes(ReporteRepository reporteRepo)
    {
        InitializeComponent();
        _reporteRepo = reporteRepo;
    }

    private void FrmReportes_Load(object sender, EventArgs e)
    {
        dtpFechaInicio.Value = DateTime.Today.AddDays(-30);
        dtpFechaFin.Value = DateTime.Today;
        CargarReporteVentas(null, null);
    }

    private void CargarReporteVentas(DateTime? inicio, DateTime? fin)
    {
        try
        {
            DataTable dt = _reporteRepo.ObtenerReporteVentas(inicio, fin);
            dgvReportes.DataSource = dt;

            // Calcular suma total de los resultados mostrados
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["precio_unitario"] != DBNull.Value)
                    total += Convert.ToDecimal(row["precio_unitario"]);
            }
            lblTotalIngresos.Text = $"Total Recaudado: {total:C2}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al consultar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnFiltrar_Click(object sender, EventArgs e)
    {
        CargarReporteVentas(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date);
    }

    private void btnMostrarTodo_Click(object sender, EventArgs e)
    {
        CargarReporteVentas(null, null);
    }

    private void btnResumenPartidos_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = _reporteRepo.ObtenerResumenPartidos();
            dgvReportes.DataSource = dt;

            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["total_recaudado"] != DBNull.Value)
                    total += Convert.ToDecimal(row["total_recaudado"]);
            }
            lblTotalIngresos.Text = $"Total Recaudado Global: {total:C2}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al consultar resumen: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}