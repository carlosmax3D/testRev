using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmLogin : Form
{
    private readonly IServiceProvider _serviceProvider;

    public FrmLogin(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;

        Text = "Venta de boletos - Inicio de sesión";
        StartPosition = FormStartPosition.CenterScreen;
        AcceptButton = botonIni;           // Enter inicia sesión
        tBcorreoElectronico.MaxLength = 50;
        tBContraseña.MaxLength = 255;
    }

    private void inicioSesion_Load(object sender, EventArgs e)
    {
        tBcorreoElectronico.Focus();
    }

    private void botonIni_Click(object sender, EventArgs e)
    {
        string correo = tBcorreoElectronico.Text.Trim();
        string contrasena = tBContraseña.Text;

        if (string.IsNullOrWhiteSpace(correo))
        {
            UiHelper.Advertir("El correo electrónico está vacío. Por favor, ingrésalo.", "Correo obligatorio");
            tBcorreoElectronico.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(contrasena))
        {
            UiHelper.Advertir("La contraseña está vacía. Por favor, ingrésala.", "Contraseña obligatoria");
            tBContraseña.Focus();
            return;
        }

        try
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();

            ResultadoInicioSesion resultado = usuarioService.IniciarSesion(correo, contrasena);

            if (!resultado.AccesoPermitido || resultado.Usuario == null)
            {
                if (resultado.UsuarioInactivo)
                {
                    UiHelper.Advertir("Tu cuenta está inactiva. Contacta al administrador.", "Cuenta inactiva");
                }
                else
                {
                    UiHelper.Advertir("Correo o contraseña incorrectos. Verifica tus datos.", "No se pudo iniciar sesión");
                }

                tBContraseña.Clear();
                tBContraseña.Focus();
                return;
            }

            // Se guarda el usuario de la sesión para permisos y auditoría
            ISesionUsuario sesion = _serviceProvider.GetRequiredService<ISesionUsuario>();
            sesion.Usuario = resultado.Usuario;

            using FrmPrincipal menu = new FrmPrincipal(resultado.Usuario, _serviceProvider);

            tBContraseña.Clear();

            Hide();
            menu.ShowDialog();

            sesion.Usuario = null;

            Show();
            tBcorreoElectronico.Clear();
            tBContraseña.Clear();
            tBcorreoElectronico.Focus();
        }
        catch (Exception ex)
        {
            UiHelper.MostrarError(ex, "Error al iniciar sesión");
        }
    }

    private void botonSalir_Click(object sender, EventArgs e)
    {
        Close();
    }
}
