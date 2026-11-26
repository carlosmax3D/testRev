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
    public partial class FormPartidos : Form
    {

        int idPartido = 0;

        public FormPartidos()
        {
            InitializeComponent();
        }

        private void FormPartidos_Load(object sender, EventArgs e)
        {
            CargarEstadios();
            CargarPartidos();

            cmbEstado.Items.Add("Disponible");
            cmbEstado.Items.Add("Agotado");
            cmbEstado.Items.Add("Cancelado");

            dgvPartidos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        void CargarEstadios()
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT IdEstadio, Nombre FROM Estadio", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbEstadio.DataSource = dt;
                cmbEstadio.DisplayMember = "Nombre";
                cmbEstadio.ValueMember = "IdEstadio";
            }
        }

        void CargarPartidos()
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT 
                P.IdPartido,
                E.Nombre AS Estadio,
                P.FechaHora,
                P.EquipoLocal,
                P.EquipoVisitante,
                P.Estado
              FROM Partido P
              INNER JOIN Estadio E
              ON P.IdEstadio = E.IdEstadio", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPartidos.DataSource = dt;
            }
        }

        void Limpiar()
        {
            txtLocal.Clear();
            txtVisitante.Clear();

            cmbEstado.SelectedIndex = -1;

            idPartido = 0;

            txtLocal.Focus();
        }

        //private void CargarEstadios()
        //{
        //    using (SqlConnection con = Conexion.conectar())
        //    {
        //        SqlDataAdapter da = new SqlDataAdapter("SELECT IdEstadio, Nombre FROM Estadio", con);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        cmbEstadio.DataSource = dt;
        //        cmbEstadio.DisplayMember = "Nombre";
        //        cmbEstadio.ValueMember = "IdEstadio";
        //    }
        //}

        //private void CargarPartidos()
        //{
        //    using (SqlConnection con = Conexion.conectar())
        //    {
        //        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Partido", con);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        dgvPartidos.DataSource = dt;
        //    }
        //}

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbEstadio.SelectedValue == null)
            {
                MessageBox.Show("Selecciona estadio");
                return;
            }

            if (txtLocal.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa equipo local");
                return;
            }

            if (txtVisitante.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa equipo visitante");
                return;
            }


            // VALIDACIÓN OBLIGATORIA: No registrar partidos en fechas en el pasado
            if (dtpFechaHora.Value < DateTime.Now)
            {
                MessageBox.Show("Error Lógico: La fecha y hora del partido no puede pertenecer al pasado.",
                                "Validación Temporal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (cmbEstado.Text == "")
            {
                MessageBox.Show("Selecciona estado");
                return;
            }

            using (SqlConnection con = Conexion.conectar())
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_InsertarPartido", con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@IdEstadio",
                    Convert.ToInt32(cmbEstadio.SelectedValue));

                cmd.Parameters.AddWithValue(
                    "@FechaHora",
                    dtpFechaHora.Value);

                cmd.Parameters.AddWithValue(
                    "@EquipoLocal",
                    txtLocal.Text);

                cmd.Parameters.AddWithValue(
                    "@EquipoVisitante",
                    txtVisitante.Text);

                cmd.Parameters.AddWithValue(
                    "@Estado",
                    cmbEstado.Text);

                con.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Partido guardado");

                CargarPartidos();
                Limpiar();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPartidos.CurrentRow == null)
            {
                MessageBox.Show("Selecciona partido");
                return;
            }

            int id = Convert.ToInt32(
                dgvPartidos.CurrentRow.Cells["IdPartido"].Value);

            DialogResult r = MessageBox.Show(
                "¿Eliminar partido?",
                "Confirmar",
                MessageBoxButtons.YesNo);

            if (r == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.conectar())
                {
                    SqlCommand cmd = new SqlCommand(
                        "sp_EliminarPartido", con);

                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@IdPartido", id);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Partido eliminado");

                    CargarPartidos();
                }
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        
    }
}
