using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Reporte de ventas con filtros: rango de fechas, estadio y partido
    public class ReporteVentasForm : Form
    {
        readonly ReporteRepositorio reportes = new ReporteRepositorio();
        DateTimePicker dtpDesde, dtpHasta;
        ComboBox cmbEstadio, cmbPartido;
        DataGridView dgvDetalle, dgvResumen;
        Label lblIngreso, lblBoletos;

        public ReporteVentasForm()
        {
            Ui.PrepararForm(this, "Reporte de ventas", "Reporte de ventas", 1100, 690);

            GroupBox g = Ui.Grupo(this, "Filtros", 12, 70, 1076, 86);
            Ui.Etiqueta(g, "Desde", 12, 24);
            dtpDesde = Ui.Fecha(g, 12, 44, 120);
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            Ui.Etiqueta(g, "Hasta", 142, 24);
            dtpHasta = Ui.Fecha(g, 142, 44, 120);
            Ui.Etiqueta(g, "Estadio (opcional)", 274, 24);
            cmbEstadio = Ui.Combo(g, 274, 44, 220);
            Ui.Etiqueta(g, "Partido (opcional)", 506, 24);
            cmbPartido = Ui.Combo(g, 506, 44, 340);
            Ui.Boton(g, "Generar", 858, 40, 100, 30, Ui.Azul, btnGenerar_Click);
            Ui.Boton(g, "Limpiar", 966, 40, 96, 30, Ui.Gris, btnLimpiar_Click);

            Ui.Etiqueta(this, "Detalle de ventas (agrupado por estadio y partido)", 14, 164).Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDetalle = Ui.Grid(this, 12, 184, 1076, 250);

            Ui.Etiqueta(this, "Resumen por partido", 14, 442).Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvResumen = Ui.Grid(this, 12, 462, 700, 160);

            GroupBox t = Ui.Grupo(this, "Totales de lo mostrado", 724, 446, 364, 176);
            lblIngreso = Ui.Etiqueta(t, "Ingreso total: $0.00", 16, 40);
            lblIngreso.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblIngreso.ForeColor = Ui.Navy;
            lblBoletos = Ui.Etiqueta(t, "Boletos vendidos: 0", 16, 90);
            lblBoletos.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBoletos.ForeColor = Ui.Navy;

            Ui.Boton(this, "Cerrar", 968, 636, 120, 36, Ui.Gris, delegate { Close(); });

            Load += delegate
            {
                try
                {
                    Tema.LlenarCombo(cmbEstadio, new EstadioRepositorio().Combo());
                    Tema.LlenarCombo(cmbPartido, new PartidoRepositorio().Combo());
                    Generar();
                }
                catch (Exception ex) { Tema.ErrorConexion(ex); }
            };
        }

        void btnGenerar_Click(object sender, EventArgs e)
        {
            Generar();
        }

        void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
            cmbEstadio.SelectedIndex = -1;
            cmbPartido.SelectedIndex = -1;
            Generar();
        }

        void Generar()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                Tema.Aviso("La fecha inicial no puede ser mayor que la final.", "Reporte de ventas");
                return;
            }
            int? estadio = cmbEstadio.SelectedValue is int ? (int?)(int)cmbEstadio.SelectedValue : null;
            int? partido = cmbPartido.SelectedValue is int ? (int?)(int)cmbPartido.SelectedValue : null;
            try
            {
                DataSet ds = reportes.Ventas(dtpDesde.Value, dtpHasta.Value, estadio, partido);
                dgvDetalle.DataSource = ds.Tables[0];
                Tema.FormatoGrid(dgvDetalle);
                dgvResumen.DataSource = ds.Tables[2];
                Tema.FormatoGrid(dgvResumen);
                lblIngreso.Text = "Ingreso total: " + Formato.Dinero(Convert.ToDecimal(ds.Tables[1].Rows[0]["Ingreso total"]));
                lblBoletos.Text = "Boletos vendidos: " + Convert.ToString(ds.Tables[1].Rows[0]["Boletos vendidos"]);
                if (ds.Tables[0].Rows.Count == 0) Tema.Info("No hay ventas con esos filtros.", "Reporte de ventas");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Reporte de ventas"); }
        }
    }
}
