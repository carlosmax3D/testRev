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

namespace VentaBoletosWF
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            CargarKPIs();

            CargarTopPartidos();

            CargarUltimasVentas();

            CargarOcupacion();

            dgvTopPartidos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvVentas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvOcupacion.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }













        void CargarKPIs()
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                con.Open();

                

                SqlCommand cmd1 = new SqlCommand(
                    "SELECT COUNT(*) FROM Venta",
                    con);

                lblVentas.Text =
                    cmd1.ExecuteScalar().ToString();

                

                SqlCommand cmd2 = new SqlCommand(
                    @"SELECT ISNULL(SUM(Total),0)
              FROM Venta
              WHERE Estado != 'Cancelada'",
                    con);

                lblIngresos.Text =
                    Convert.ToDecimal(
                    cmd2.ExecuteScalar())
                    .ToString("C");

                

                SqlCommand cmd3 = new SqlCommand(
                    @"SELECT ISNULL(SUM(Cantidad),0)
              FROM DetalleVenta",
                    con);

                lblBoletos.Text =
                    cmd3.ExecuteScalar().ToString();

                

                SqlCommand cmd4 = new SqlCommand(
                    @"SELECT COUNT(*)
              FROM Cancelacion",
                    con);

                lblCancelaciones.Text =
                    cmd4.ExecuteScalar().ToString();
            }
        }


        void CargarTopPartidos()
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    "SELECT * FROM vw_TopPartidos",
                    con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvTopPartidos.DataSource = dt;
            }
        }

        void CargarUltimasVentas()
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    @"
            SELECT TOP 10
                IdVenta,
                FechaVenta,
                Total,
                MetodoPago
            FROM Venta
            ORDER BY FechaVenta DESC
            ", con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvVentas.DataSource = dt;
            }
        }


        void CargarOcupacion()
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    "SELECT * FROM vw_DisponibilidadBoletos",
                    con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvOcupacion.DataSource = dt;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
