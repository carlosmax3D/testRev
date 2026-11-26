using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using VentaBoletosWF;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VentaBoletosWF
{
    public partial class FormRegistroUsuario : Form
    {
        public FormRegistroUsuario()
        {
            InitializeComponent();
        }

        private void FormRegistroUsuario_Load(object sender, EventArgs e)
        {
            cmbTipo.Items.Add("Administrador");
            cmbTipo.Items.Add("Operativo");
        }

        // VALIDACIONES
        public bool CamposVacios(params string[] campos)
        {
            foreach (string campo in campos)
            {
                if (string.IsNullOrWhiteSpace(campo))
                    return true;
            }
            return false;
        }

        public bool ValidarCorreo(string correo)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, patron);
        }

        public bool ValidarContrasena(string pass)
        {
            string patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(pass, patron);
        }

        

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (CamposVacios(txtNombre.Text, txtCorreo.Text, txtPassword.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios");
                return;
            }

            if (!ValidarCorreo(txtCorreo.Text))
            {
                MessageBox.Show("Correo inválido");
                return;
            }

            if (!ValidarContrasena(txtPassword.Text))
            {
                MessageBox.Show("Contraseña débil (mínimo 8 caracteres, mayúscula, minúscula y símbolo)");
                return;
            }


            if (CorreoExiste(txtCorreo.Text))
            {
                MessageBox.Show("Este correo ya está registrado");
                return;
            }




            using (SqlConnection con = Conexion.conectar())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Contrasena", txtPassword.Text); // Mandas texto plano, SQL se encarga de encriptarlo automáticamente
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@NumeroNomina", string.IsNullOrWhiteSpace(txtNumeroNomina.Text) ? (object)DBNull.Value : txtNumeroNomina.Text);
                cmd.Parameters.AddWithValue("@FechaNacimiento", dtpFechaNacimiento.Value);
                cmd.Parameters.AddWithValue("@Tipo", cmbTipo.Text);

                con.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Usuario registrado correctamente");

                FormLogin login = new FormLogin();
                login.Show();
                this.Hide();
            }


        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Hide();
        }



        public bool CorreoExiste(string correo)
        {
            using (SqlConnection con = Conexion.conectar())
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarCorreo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Correo", correo);

                con.Open();
                int existe = Convert.ToInt32(cmd.ExecuteScalar());

                return existe == 1;
            }
        }


    }

}




