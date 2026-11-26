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

namespace VentaBoletosWF
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (CamposVacios(txtCorreo.Text, txtPassword.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios");
                return;
            }

            if (!ValidarCorreo(txtCorreo.Text))
            {
                MessageBox.Show("Correo no válido");
                return;
            }

            try
            {
                using (SqlConnection con = Conexion.conectar())
                {
                    SqlCommand cmd = new SqlCommand("sp_Login", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Contrasena", txtPassword.Text); // Enviamos texto plano, el nuevo SP lo encriptará en SQL

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string tipo = dr["Tipo"].ToString();
                            string nombre = dr["NombreCompleto"].ToString();

                            MessageBox.Show("Bienvenido " + nombre + " (" + tipo + ")");

                            FormMenu menu = new FormMenu(tipo);
                            menu.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar conectar: " + ex.Message);
            }
        }





        private void btnRegistro_Click(object sender, EventArgs e)
        {
            FormRegistroUsuario reg = new FormRegistroUsuario();
            reg.Show();
            this.Hide();
        }


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

    }
}


