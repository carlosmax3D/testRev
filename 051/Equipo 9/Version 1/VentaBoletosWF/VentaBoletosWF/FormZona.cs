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
    public partial class FormZona : Form
    {
        int idZona = 0;


        public FormZona()
        {
            InitializeComponent();
        }

        private void FormZona_Load(object sender, EventArgs e)
        {
            CargarEstadios();
            CargarZonas();

            dgvZonas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)
                && e.KeyChar != '.'
                && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txtCupo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)
                && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }





        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbEstadio.SelectedValue == null)
            {
                MessageBox.Show("Selecciona estadio");
                return;
            }

            if (txtNombreZona.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa nombre zona");
                return;
            }

            if (txtPrecio.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa precio");
                return;
            }

            if (txtCupo.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa cupo");
                return;
            }

            using (SqlConnection con = Conexion.conectar())
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_InsertarZona", con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@IdEstadio",
                    Convert.ToInt32(cmbEstadio.SelectedValue));

                cmd.Parameters.AddWithValue(
                    "@NombreZona",
                    txtNombreZona.Text);

                cmd.Parameters.AddWithValue(
                    "@PrecioBase",
                    Convert.ToDecimal(txtPrecio.Text));

                cmd.Parameters.AddWithValue(
                    "@Cupo",
                    Convert.ToInt32(txtCupo.Text));

                con.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Zona guardada");

                CargarZonas();
                Limpiar();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvZonas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona zona");
                return;
            }

            int id = Convert.ToInt32(
                dgvZonas.CurrentRow.Cells["IdZona"].Value);

            DialogResult r = MessageBox.Show(
                "¿Eliminar zona?",
                "Confirmar",
                MessageBoxButtons.YesNo);

            if (r == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.conectar())
                {
                    SqlCommand cmd = new SqlCommand(
                        "sp_EliminarZona", con);

                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@IdZona", id);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Zona eliminada");

                    CargarZonas();
                }
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
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



        void CargarZonas()
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT 
                Z.IdZona,
                E.Nombre AS Estadio,
                Z.NombreZona,
                Z.PrecioBase,
                Z.Cupo
              FROM Zona Z
              INNER JOIN Estadio E
              ON Z.IdEstadio = E.IdEstadio", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvZonas.DataSource = dt;
            }
        }



        void Limpiar()
        {
            txtNombreZona.Clear();
            txtPrecio.Clear();
            txtCupo.Clear();

            idZona = 0;
            
            txtNombreZona.Focus();
        }

    }
}
