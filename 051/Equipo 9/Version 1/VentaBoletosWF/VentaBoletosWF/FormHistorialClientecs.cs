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
    public partial class FormHistorialClientes : Form
    {
        public FormHistorialClientes()
        {
            InitializeComponent();
        }

        private void FormHistorialCliente_Load(object sender, EventArgs e)
        {
            dgvClientes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvHistorial.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            cmbFiltro.Items.Add("Fecha Compra");
            cmbFiltro.Items.Add("Fecha Partido");



            dgvClientes.ReadOnly = true;

            dgvHistorial.ReadOnly = true;

            dgvClientes.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

            dgvHistorial.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (txtBuscar.Text == "")
            {
                MessageBox.Show(
                    "Escribe apellido");

                return;
            }


            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    @"
            SELECT
                IdCliente,
                NombreCompleto,
                Correo
            FROM Cliente
            WHERE NombreCompleto
            LIKE @Buscar
            ", con);

                da.SelectCommand.Parameters.AddWithValue(
                    "@Buscar",
                    "%" + txtBuscar.Text + "%");

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvClientes.DataSource = dt;
            }
        }





        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int idCliente =
                Convert.ToInt32(
                dgvClientes.Rows[e.RowIndex]
                .Cells["IdCliente"].Value);

            CargarHistorial(idCliente);
        }

        private void dgvClientes_CellClick(object sender, EventArgs e)
        {

        }







        void CargarHistorial(int idCliente)
        {
            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    @"
            SELECT *

            FROM vw_HistorialCliente

            WHERE IdCliente = @IdCliente
            ", con);

                da.SelectCommand.Parameters.AddWithValue(
                    "@IdCliente",
                    idCliente);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvHistorial.DataSource = dt;
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecciona cliente");

                return;
            }

            

            int idCliente =
                Convert.ToInt32(
                dgvClientes.CurrentRow
                .Cells["IdCliente"].Value);

            string orden = "";

            if (cmbFiltro.Text ==
                "Fecha Compra")
            {
                orden =
                " ORDER BY FechaVenta ASC";
            }

            else if (cmbFiltro.Text ==
                "Fecha Partido")
            {
                orden =
                " ORDER BY FechaPartido ASC";
            }

            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlDataAdapter da =
                    new SqlDataAdapter(
                    @"
            SELECT *
            FROM vw_HistorialCliente

            WHERE IdCliente = @IdCliente
            "
                    + orden,
                    con);

                da.SelectCommand.Parameters.AddWithValue(
                    "@IdCliente",
                    idCliente);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                dgvHistorial.DataSource = dt;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
