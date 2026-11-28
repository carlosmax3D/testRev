using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

public partial class FrmLogin : Form
{
    private readonly UsuarioService _usuarioService;
    private readonly SesionUsuario _sesion;
    private readonly IServiceProvider _serviceProvider;

    public FrmLogin(UsuarioService usuarioService, SesionUsuario sesion, IServiceProvider serviceProvider)
    {
        InitializeComponent();


        _usuarioService = usuarioService;
        _sesion = sesion;
        _serviceProvider = serviceProvider;
    }

    private void btnIngresar_Click(object sender, EventArgs e)
    {
        try
        {
            string email = txtEmail.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            var usuario = _usuarioService.Autenticar(email, contrasena);

            if (usuario == null)
            {
                MessageBox.Show("Credenciales inválidas. Verifique el correo o contraseña.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _sesion.UsuarioActual = usuario;

            MessageBox.Show($"Bienvenido(a), {usuario.NombreCompleto}\nRol: {usuario.Rol}", "Acceso Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

            FrmPrincipal menu = _serviceProvider.GetRequiredService<FrmPrincipal>();
            this.Hide();
            menu.ShowDialog();
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error al autenticar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnSalir_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

}