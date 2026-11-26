using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Ocupación y disponibilidad de boletos por partido y zona
    public class ReporteOcupacionForm : Form
    {
        readonly ReporteRepositorio reportes = new ReporteRepositorio();
        ComboBox cmbPartido, cmbZona;
        CheckBox chkDisponibles;
        DataGridView dgv;
        Label lblTotal;

        public ReporteOcupacionForm()
        {
            Ui.PrepararForm(this, "Reporte de ocupación", "Ocupación y disponibilidad de boletos", 1060, 600);

            GroupBox g = Ui.Grupo(this, "Filtros", 12, 70, 1036, 86);
            Ui.Etiqueta(g, "Partido", 12, 24);
            cmbPartido = Ui.Combo(g, 12, 44, 400);
            cmbPartido.SelectedIndexChanged += delegate { CargarZonas(); };
            Ui.Etiqueta(g, "Zona", 424, 24);
            cmbZona = Ui.Combo(g, 424, 44, 250);
            chkDisponibles = new CheckBox();
            chkDisponibles.Text = "Solo con boletos aún disponibles";
            chkDisponibles.AutoSize = true;
            chkDisponibles.Location = new Point(686, 47);
            g.Controls.Add(chkDisponibles);
            Ui.Boton(g, "Generar", 900, 40, 62, 30, Ui.Azul, delegate { Generar(); }).Width = 60;
            Ui.Boton(g, "Limpiar", 966, 40, 62, 30, Ui.Gris, btnLimpiar_Click).Width = 60;

            dgv = Ui.Grid(this, 12, 168, 1036, 340);
            lblTotal = Ui.Etiqueta(this, "Boletos vendidos en total: 0", 14, 520);
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotal.ForeColor = Ui.Navy;
            Ui.Boton(this, "Cerrar", 928, 520, 120, 36, Ui.Gris, delegate { Close(); });

            Load += delegate
            {
                try
                {
                    Tema.LlenarCombo(cmbPartido, new PartidoRepositorio().Combo());
                    CargarZonas();
                    Generar();
                }
                catch (Exception ex) { Tema.ErrorConexion(ex); }
            };
        }

        void CargarZonas()
        {
            try
            {
                int? p = cmbPartido.SelectedValue is int ? (int?)(int)cmbPartido.SelectedValue : null;
                Tema.LlenarCombo(cmbZona, new ZonaRepositorio().Combo(p));
            }
            catch (Exception ex) { Tema.Manejar(ex, "Reporte de ocupación"); }
        }

        void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbPartido.SelectedIndex = -1;
            chkDisponibles.Checked = false;
            CargarZonas();
            Generar();
        }

        void Generar()
        {
            int? partido = cmbPartido.SelectedValue is int ? (int?)(int)cmbPartido.SelectedValue : null;
            int? zona = cmbZona.SelectedValue is int ? (int?)(int)cmbZona.SelectedValue : null;
            try
            {
                DataSet ds = reportes.Ocupacion(partido, zona, chkDisponibles.Checked);
                dgv.DataSource = ds.Tables[0];
                Tema.FormatoGrid(dgv);
                string total = Convert.ToString(ds.Tables[1].Rows[0]["Boletos vendidos en total"]);
                lblTotal.Text = (partido != null ? "Boletos vendidos del partido: " : "Boletos vendidos en total: ") + total;
            }
            catch (Exception ex) { Tema.Manejar(ex, "Reporte de ocupación"); }
        }
    }
}
