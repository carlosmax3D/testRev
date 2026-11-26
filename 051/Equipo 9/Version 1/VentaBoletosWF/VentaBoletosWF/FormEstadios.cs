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
    public partial class FormEstadios : Form
    {
        int idEstadio = 0;

        public FormEstadios()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // VALIDACIONES
            if (txtNombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa nombre");
                return;
            }

            if (txtCiudad.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa ciudad");
                return;
            }

            if (txtCapacidad.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa capacidad");
                return;
            }

            using (SqlConnection con = Conexion.conectar())
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_InsertarEstadio", con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Nombre", txtNombre.Text);

                cmd.Parameters.AddWithValue(
                    "@Ciudad", txtCiudad.Text);

                cmd.Parameters.AddWithValue(
                    "@Capacidad",
                    Convert.ToInt32(txtCapacidad.Text));

                cmd.Parameters.AddWithValue(
                    "@Mapa", txtMapa.Text);

                cmd.Parameters.AddWithValue(
                    "@IdUsuarioRegistro", 1);

                con.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "Estadio guardado correctamente");

                CargarEstadios();
                Limpiar();
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEstadios.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un estadio");
                return;
            }

            int id = Convert.ToInt32(
                dgvEstadios.CurrentRow.Cells["IdEstadio"].Value);

            DialogResult r = MessageBox.Show(
                "¿Eliminar estadio?",
                "Confirmar",
                MessageBoxButtons.YesNo);

            if (r == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.conectar())
                {
                    SqlCommand cmd = new SqlCommand(
                        "sp_EliminarEstadio", con);

                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@IdEstadio", id);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Eliminado");

                    CargarEstadios();
                }
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //private void CargarEstadios()
        //{
        //    using (SqlConnection con = Conexion.conectar())
        //    {
        //        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Estadio", con);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        dgvEstadios.DataSource = dt;
        //    }
        //}
        void CargarEstadios()
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM Estadio", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvEstadios.DataSource = dt;
            }
        }

        void Limpiar()
        {
            txtNombre.Clear();
            txtCiudad.Clear();
            txtCapacidad.Clear();
            txtMapa.Clear();

            idEstadio = 0;

            txtNombre.Focus();
        }



        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtCiudad.Clear();
            txtCapacidad.Clear();
        }

        private void txtCapacidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)
                && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void FormEstadios_Load(object sender, EventArgs e)
        {
            CargarEstadios();

            dgvEstadios.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
