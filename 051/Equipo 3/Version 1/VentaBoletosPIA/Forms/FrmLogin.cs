using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

public partial class FrmLogin : Form
{
    private readonly IUsuarioService _usuarios;
    private readonly ISesionActual _sesion;

    public FrmLogin(IUsuarioService usuarios, ISesionActual sesion)
    {
        _usuarios = usuarios;
        _sesion = sesion;

        InitializeComponent();

        AcceptButton = btnIngresar;
        CancelButton = btnSalir;
    }

    private void BtnSalir_Click(object? sender, EventArgs e) => DialogResult = DialogResult.Cancel;

    private void BtnIngresar_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrEmpty(txtContrasena.Text))
        {
            Ui.Aviso(this, "Capture su correo y su contraseña.");
            return;
        }

        Usuario? usuario = null;

        if (!Ui.Ejecutar(this, () => usuario = _usuarios.ValidarCredenciales(txtCorreo.Text.Trim(), txtContrasena.Text)))
        {
            return;
        }

        if (usuario is null)
        {
            Ui.Aviso(this, "Correo o contraseña incorrectos, o el usuario está dado de baja.", "Acceso denegado");
            txtContrasena.Clear();
            txtContrasena.Focus();
            return;
        }

        _sesion.IniciarSesion(usuario);
        DialogResult = DialogResult.OK;
    }
}
