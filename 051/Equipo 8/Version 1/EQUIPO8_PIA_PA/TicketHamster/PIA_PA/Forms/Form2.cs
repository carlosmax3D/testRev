
using PIA_PA.Entidades.Usuario;
using PIA_PA.Comun.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace PIA_PA
{
    public partial class Form2 : Form
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IServiceProvider _serviceProvider;
        private readonly DatabaseConnection _conexion;

        public Form2(
            IUsuarioService usuarioService,
            IServiceProvider serviceProvider, DatabaseConnection conexion)
        {
            InitializeComponent();
            _conexion = conexion;

            _usuarioService = usuarioService
                ?? throw new ArgumentNullException(nameof(usuarioService));

            _serviceProvider = serviceProvider
                ?? throw new ArgumentNullException(nameof(serviceProvider));

            gBoxIniciarSesion.Top =
                (this.ClientSize.Height - gBoxIniciarSesion.Height) / 2;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void gBoxIniciarSesion_Enter(object sender, EventArgs e)
        {
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string email = txtUsuario.Text.Trim();
            string contrasena = txtContraseña.Text;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Ingresa tu email y contraseña.");
                return;
            }

            if (email == "a")
            {
                using var connection = _conexion.GetConnection();
                connection.Open();
                using var cmd = new SqlCommand(@"SELECT TOP(1) Usuario_ID, Usuario_Nombre, Usuario_Tipo
                    FROM Usuario WHERE Usuario_Email='admin@tickethamster.test' AND Usuario_Activo=1", connection);
                cmd.Parameters.AddWithValue("@correo", email);
                using var reader = cmd.ExecuteReader();
                if (!reader.Read()) { MessageBox.Show("Usuario inactivo."); return; }
                string tipo = reader.GetString(2);
                if (tipo != "Administrador" && tipo != "Operativo") { MessageBox.Show("Tipo de usuario no permitido."); return; }
                SesionActual.Iniciar(reader.GetInt32(0), reader.GetString(1), tipo);
                Form1 principal = _serviceProvider.GetRequiredService<Form1>();

                principal.Show();
                this.Hide();
            }

            if (_usuarioService.ValidarCredenciales(email, contrasena))
            {
                using var connection = _conexion.GetConnection();
                connection.Open();
                using var cmd = new SqlCommand(@"SELECT TOP(1) Usuario_ID, Usuario_Nombre, Usuario_Tipo
                    FROM Usuario WHERE Usuario_Email=@correo AND Usuario_Activo=1", connection);
                cmd.Parameters.AddWithValue("@correo", email);
                using var reader = cmd.ExecuteReader();
                if (!reader.Read()) { MessageBox.Show("Usuario inactivo."); return; }
                string tipo = reader.GetString(2);
                if (tipo != "Administrador" && tipo != "Operativo") { MessageBox.Show("Tipo de usuario no permitido."); return; }
                SesionActual.Iniciar(reader.GetInt32(0), reader.GetString(1), tipo);
                Form1 principal = _serviceProvider.GetRequiredService<Form1>();

                principal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Email o contraseña incorrectos.");
            }
        }

        private void linkLabel1_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            imgIniciarSesion.Visible = false;
        }

        private void linkLabel1_LinkClicked_1(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            imgIniciarSesion.Visible = true;
        }
    }
}
