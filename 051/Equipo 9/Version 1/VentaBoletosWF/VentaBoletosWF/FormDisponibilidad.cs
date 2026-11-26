using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Data.SqlClient;


namespace VentaBoletosWF
{
    public partial class FormDisponibilidad : Form
    {
        public FormDisponibilidad()
        {
            InitializeComponent();
        }

       

        private void FormDisponibilidad_Load(
            object sender,
            EventArgs e)
        {
            CargarPartidos();
            CargarZonas();
            MostrarDisponibilidad();

            dgvDisponibilidad.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

     

        void CargarPartidos()
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    @"
                    SELECT
                        IdPartido,
                        EquipoLocal + ' vs ' +
                        EquipoVisitante AS Partido
                    FROM Partido
                    ", con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                cmbPartido.DataSource = dt;
                cmbPartido.DisplayMember =
                    "Partido";

                cmbPartido.ValueMember =
                    "IdPartido";
            }
        }

        

        void CargarZonas()
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    @"
                    SELECT
                        IdZona,
                        NombreZona
                    FROM Zona
                    ", con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                cmbZona.DataSource = dt;
                cmbZona.DisplayMember =
                    "NombreZona";

                cmbZona.ValueMember =
                    "IdZona";
            }
        }

        


        void MostrarDisponibilidad()
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    @"SELECT *
                      FROM vw_DisponibilidadBoletos",
                    con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvDisponibilidad.DataSource =
                    dt;
            }
        }

        


        private void btnBuscar_Click(
            object sender,
            EventArgs e)
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                string sql =
                @"
                SELECT *
                FROM vw_DisponibilidadBoletos
                WHERE IdPartido = @IdPartido
                AND IdZona = @IdZona
                ";

                if (chkDisponibles.Checked)
                {
                    sql +=
                    " AND AsientosDisponibles > 0";
                }

                SqlDataAdapter da =
                    new SqlDataAdapter(
                    sql, con);

                da.SelectCommand.Parameters.AddWithValue(
                    "@IdPartido",
                    cmbPartido.SelectedValue);

                da.SelectCommand.Parameters.AddWithValue(
                    "@IdZona",
                    cmbZona.SelectedValue);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvDisponibilidad.DataSource =
                    dt;

                CalcularVendidos(dt);
            }
        }

        


        void CalcularVendidos(DataTable dt)
        {
            int vendidos = 0;

            foreach (DataRow row in dt.Rows)
            {
                vendidos +=
                    Convert.ToInt32(
                    row["AsientosVendidos"]);
            }

            lblVendidos.Text =
                vendidos.ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
