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
    public partial class FormCancelacion : Form
    {
        

        public FormCancelacion()
        {
            InitializeComponent();
        }



        

        private void btnBuscar_Click(
    object sender,
    EventArgs e)
        {
            if (txtIdVenta.Text == "")
            {
                MessageBox.Show(
                    "Ingresa ID Venta");

                return;
            }

            using (SqlConnection con =
                Conexion.conectar())
            {
                SqlCommand cmd =
                    new SqlCommand(
                    @"
            SELECT
                V.IdVenta,
                C.NombreCompleto,
                V.Total,
                V.Estado
            FROM Venta V

            JOIN Cliente C
            ON V.IdCliente = C.IdCliente

            WHERE V.IdVenta = @IdVenta
            ", con);

                cmd.Parameters.AddWithValue(
                    "@IdVenta",
                    txtIdVenta.Text);

                con.Open();

                SqlDataReader dr =
                    cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblCliente.Text =
                        dr["NombreCompleto"]
                        .ToString();

                    lblTotal.Text =
                        dr["Total"]
                        .ToString();

                    lblEstado.Text =
                        dr["Estado"]
                        .ToString();
                }
                else
                {
                    MessageBox.Show(
                        "Venta no encontrada");
                }
            }
        }

        


        private void btnCancelarVenta_Click(
            object sender,
            EventArgs e)
        {

            // REQUERIMIENTO COMPLETO: Solo el administrador puede realizar cancelaciones
            // Debes validar contra la variable global de tu usuario autenticado
            //if (FormMenu.RolUsuarioLogueado != "Administrador")
            //{
            //    MessageBox.Show("Acceso denegado. Solo los usuarios administradores pueden revocar ventas.");
            //    return;
            //}
            // ... resto de tu código de cancelación




            if (txtIdVenta.Text == "")
            {
                MessageBox.Show(
                    "Ingresa ID Venta");

                return;
            }

            
            if (txtMotivo.Text == "")
            {
                MessageBox.Show(
                    "Ingresa motivo");

                return;
            }

            
            if (lblEstado.Text == "Cancelada")
            {
                MessageBox.Show(
                    "La venta ya está cancelada");

                return;
            }

            using (SqlConnection con =
                Conexion.conectar())
            {
                con.Open();

                SqlCommand cmd =
                    new SqlCommand(
                    "sp_CancelarVenta",
                    con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@IdVenta",
                    txtIdVenta.Text);

                cmd.Parameters.AddWithValue(
                    "@Motivo",
                    txtMotivo.Text);

                cmd.Parameters.AddWithValue("@IdUsuario", 1);
 
                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "Venta cancelada correctamente");

                Limpiar();
            } 		

        }

       

        void Limpiar()
        {
            txtIdVenta.Clear();
            txtMotivo.Clear();

            lblCliente.Text = "";
            lblTotal.Text = "";
            lblEstado.Text = "";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
