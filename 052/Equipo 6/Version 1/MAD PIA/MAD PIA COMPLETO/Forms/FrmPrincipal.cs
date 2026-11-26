using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class inicioSesion : Form
{
    private readonly IServiceProvider _serviceProvider;

    public inicioSesion(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
    }

    private void inicioSesion_Load(object sender, EventArgs e)
    {
    }

    private void botonIni_Click(object sender, EventArgs e)
    {
        string correo = tBcorreoElectronico.Text.Trim();
        string contraseña = tBContraseña.Text;

        if (string.IsNullOrWhiteSpace(correo))
        {
            MessageBox.Show(
                "El correo electrónico está vacío. Por favor, ingréselo.",
                "Correo obligatorio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            tBcorreoElectronico.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(contraseña))
        {
            MessageBox.Show(
                "La contraseña está vacía. Por favor, ingrésela.",
                "Contraseña obligatoria",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            tBContraseña.Focus();
            return;
        }

        try
        {
            IUsuarioService usuarioService =
                _serviceProvider.GetRequiredService<IUsuarioService>();

            ResultadoInicioSesion resultado =
                usuarioService.IniciarSesion(correo, contraseña);

            if (!resultado.AccesoPermitido)
            {
                if (resultado.UsuarioInactivo)
                {
                    MessageBox.Show(
                        "Tu cuenta está inactiva. Contacta al administrador.",
                        "Cuenta inactiva",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "Correo o contraseña incorrectos. Verifica tus datos.",
                        "No se pudo iniciar sesión",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                return;
            }

            if (resultado.Usuario is null)
            {
                MessageBox.Show(
                    "No fue posible recuperar los datos del usuario.",
                    "Error de inicio de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            using FrmMenuPrincipal menu =new FrmMenuPrincipal(resultado.Usuario,_serviceProvider);

            tBContraseña.Clear();

            Hide();
            menu.ShowDialog();
            Show();

            tBContraseña.Clear();
            tBcorreoElectronico.Focus();
        }
        catch (SqlException)
        {
            MessageBox.Show(
                "No fue posible consultar la base de datos. " +
                "Verifica la conexión con SQL Server.",
                "Error de conexión",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (Exception)
        {
            MessageBox.Show(
                "Ocurrió un error al intentar iniciar sesión.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void botonSalir_Click(object sender, EventArgs e)
    {
        Close();
    }
}
