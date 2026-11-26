using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateCSharp.Forms;

public partial class FrmLogin : FormBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IServiceProvider _serviceProvider;

    public FrmLogin(IUsuarioService usuarioService, IServiceProvider serviceProvider)
        : base("Login - Venta de Boletos", 400, 290)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
        _serviceProvider = serviceProvider;
    }

    private void btnIngresar_Click(object sender, EventArgs e)
    {
        try
        {
            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Text;

            Usuario? usuario = _usuarioService.IniciarSesion(correo, contrasena);

            if (usuario == null)
            {
                MessageBox.Show("Credenciales incorrectas o el usuario no existe.", 
                    "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // GUARDAMOS LA SESIÓN AQUÍ
            SesionActual.IdUsuario = usuario.IdUsuario;
            SesionActual.NombreUsuario = usuario.Nombre;
            SesionActual.RolUsuario = usuario.TipoUsuario;

            // Si el login es correcto, abrimos el menú principal
            MessageBox.Show($"Bienvenido {usuario.Nombre} ({usuario.ObtenerRol()})", 
                "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide(); // Ocultamos el login

            // Obtenemos el menú principal usando DI
            FrmPrincipal menu = _serviceProvider.GetRequiredService<FrmPrincipal>();
            menu.ShowDialog();

            this.Close(); // Cerramos el login cuando se cierre el menú
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnSalir_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
