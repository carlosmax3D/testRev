using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VentaBoletosWF
{
    public partial class FormReportes : Form
    {
        public FormReportes()
        {
            InitializeComponent();
        }

       

        private void FormReportes_Load(
            object sender,
            EventArgs e)
        {
            CargarReportes();

            dgvReportes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        


        void CargarReportes()
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    @"SELECT *
                      FROM vw_ReporteVentas",
                    con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvReportes.DataSource = dt;

                CalcularResumen(dt);
            }
        }

        

        void CalcularResumen(DataTable dt)
        {
            decimal totalVentas = 0;
            int boletos = 0;

            foreach (DataRow row in dt.Rows)
            {
                totalVentas +=
                    Convert.ToDecimal(
                        row["Total"]);

                boletos +=
                    Convert.ToInt32(
                        row["Boletos"]);
            }

            lblTotalVentas.Text =
                "$ " + totalVentas.ToString("0.00");

            lblBoletos.Text =
                boletos.ToString();
        }

        

        private void btnBuscar_Click(
            object sender,
            EventArgs e)
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    @"
                    SELECT *
                    FROM vw_ReporteVentas
                    WHERE FechaVenta
                    BETWEEN @Inicio
                    AND @Fin
                    ",
                    con);

                da.SelectCommand.Parameters.AddWithValue(
                    "@Inicio",
                    dtpInicio.Value.Date);

                da.SelectCommand.Parameters.AddWithValue(
                    "@Fin",
                    dtpFin.Value.Date);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvReportes.DataSource = dt;

                CalcularResumen(dt);
            }
        }

        

        private void btnMostrarTodo_Click(
            object sender,
            EventArgs e)
        {
            CargarReportes();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}