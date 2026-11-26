using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using VentaBoletosWF.Clases;

namespace VentaBoletosWF
{
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
        }


        public bool CamposVacios(params string[] campos)
        {
            foreach (string campo in campos)
                if (string.IsNullOrWhiteSpace(campo))
                    return true;
            return false;
        }

        public bool ValidarCorreo(string correo)
        {
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public bool ValidarRFC(string rfc)
        {
            return Regex.IsMatch(rfc, @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$");
        }

        private void CargarClientes()
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Cliente", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvClientes.DataSource = dt;
            }
        }





        private void btnGuardar_Click(object sender, EventArgs e)
        {

            Cliente cliente =
new Cliente();

            if (CamposVacios(txtNombre.Text, txtCorreo.Text))
            {
                MessageBox.Show("Nombre y correo son obligatorios");
                return;
            }

            ///

            if (!ValidarCorreo2(txtCorreo.Text))
            {
                MessageBox.Show(
                    "Correo inválido");

                return;
            }

            ///


            if (!ValidarCorreo(txtCorreo.Text))
            {
                MessageBox.Show("Correo inválido");
                return;
            }
            ///

            if (txtDomicilio.Text.Trim() == "")
            {
                MessageBox.Show(
                    "El domicilio es obligatorio");

                txtDomicilio.Focus();

                return;
            }

            if (txtDomicilio.Text.Length < 5)
            {
                MessageBox.Show(
                    "Domicilio demasiado corto");

                return;
            }

            if (txtDomicilio.Text.Length > 200)
            {
                MessageBox.Show(
                    "Domicilio demasiado largo");

                return;
            }


            ///

            if (!string.IsNullOrEmpty(txtRFC.Text) && !ValidarRFC(txtRFC.Text))
            {
                MessageBox.Show("RFC inválido");
                return;
            }

            ///

            if (txtTelCasa.Text.Length != 10)
            {
                MessageBox.Show(
                    "El teléfono de casa debe tener 10 dígitos");

                return;
            }

            if (txtTelCel.Text.Length != 10)
            {
                MessageBox.Show(
                    "El teléfono celular debe tener 10 dígitos");

                return;
            }


            if (txtTelCasa.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Ingresa teléfono de casa");

                return;
            }

            if (txtTelCel.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Ingresa teléfono celular");

                return;
            }


            if (txtTelCasa.Text == txtTelCel.Text)
            {
                MessageBox.Show(
                    "Los teléfonos no deben ser iguales");

                return;
            }




            ///

            cliente.NombreCompleto = txtNombre.Text;

            cliente.Correo =
            txtCorreo.Text;

            cliente.Domicilio =
            txtDomicilio.Text;

            cliente.RFC =
            txtRFC.Text;

            cliente.TelefonoCelular =
            txtTelCel.Text;

            cliente.TelefonoCasa =
            txtTelCasa.Text;

            cliente.FechaNacimiento =
            dtpFecha.Value;

          
          
            ///




            using (SqlConnection con = Conexion.conectar())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", cliente.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                cmd.Parameters.AddWithValue("@Domicilio", cliente.Domicilio);
                cmd.Parameters.AddWithValue("@RFC", cliente.RFC);
                cmd.Parameters.AddWithValue("@TelefonoCasa", cliente.TelefonoCasa);
                cmd.Parameters.AddWithValue("@TelefonoCelular", cliente.TelefonoCelular);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);

                con.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cliente guardado correctamente");
                CargarClientes();
                Limpiar();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlCommand cmd = new SqlCommand("sp_BuscarCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Busqueda", txtNombre.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvClientes.DataSource = dt;
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



        private void FormClientes_Load(object sender, EventArgs e)
        {
            CargarClientes();

            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void Limpiar()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtDomicilio.Clear();
            txtRFC.Clear();
            txtTelCasa.Clear();
            txtTelCel.Clear();
        }

        private void txtTelCasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
        ///

        bool ValidarCorreo2(string correo)
        {
            try
            {
                var addr =
                new System.Net.Mail.MailAddress(correo);

                return addr.Address == correo;
            }
            catch
            {
                return false;
            }
        }

        private void txtDomicilio_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;

            if (
                !char.IsLetterOrDigit(c)
                && !char.IsWhiteSpace(c)
                && c != '#'
                && c != '.'
                && c != ','
                && c != '-'
                && c != '/'
                && !char.IsControl(c)
               )
            {
                e.Handled = true;
            }
        }

        private void txtTelCel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
