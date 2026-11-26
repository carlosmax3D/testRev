using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

public partial class FrmLogin : Form
{
    private const int MaximoIntentos = 3;
    private const int SegundosBloqueo = 30;
    private const string MensajeIncorrecto = "Usuario o contraseña incorrectos. Intente de nuevo.";

    private readonly IUsuarioService _usuarioService;
    private readonly ISesionUsuario _sesion;
    private readonly IServiceProvider _serviceProvider;
    private int _intentosFallidos;
    private DateTime _bloqueadoHasta = DateTime.MinValue;

    public FrmLogin(IUsuarioService usuarioService, ISesionUsuario sesion, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
        _sesion = sesion;
        _serviceProvider = serviceProvider;
    }

    // El botón del primer administrador solo aparece cuando la base no tiene usuarios
    private void ActualizarOpcionAdministrador()
    {
        btnPrimerAdministrador.Visible = false;
        try
        {
            btnPrimerAdministrador.Visible = _usuarioService.RequiereAdministradorInicial();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void CrearPrimerAdministrador(object? sender, EventArgs e)
    {
        try
        {
            if (!_usuarioService.RequiereAdministradorInicial())
            {
                btnPrimerAdministrador.Visible = false;
                Mensajes.Informacion("La base ya tiene usuarios. Inicie sesión.");
                return;
            }

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                FrmAdministradorInicial inicial = scope.ServiceProvider.GetRequiredService<FrmAdministradorInicial>();
                inicial.ShowDialog(this);
            }

            ActualizarOpcionAdministrador();
            txtCorreo.Focus();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Ingresar(object? sender, EventArgs e)
    {
        if (DateTime.Now < _bloqueadoHasta)
        {
            int segundos = (int)Math.Ceiling((_bloqueadoHasta - DateTime.Now).TotalSeconds);
            Mensajes.Aviso($"Demasiados intentos. Espere {segundos} segundos e intente de nuevo.");
            return;
        }

        try
        {
            Usuario? usuario = _usuarioService.IniciarSesion(txtCorreo.Text, txtClave.Text);
            if (usuario is null)
            {
                _intentosFallidos++;
                if (_intentosFallidos >= MaximoIntentos)
                {
                    _intentosFallidos = 0;
                    _bloqueadoHasta = DateTime.Now.AddSeconds(SegundosBloqueo);
                    Mensajes.Aviso($"Usuario o contraseña incorrectos. Espere {SegundosBloqueo} segundos para volver a intentar.");
                }
                else
                {
                    Mensajes.Aviso(MensajeIncorrecto);
                }

                txtClave.Clear();
                txtClave.Focus();
                return;
            }

            _intentosFallidos = 0;
            _sesion.UsuarioActual = usuario;
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void chkMostrar_CheckedChanged(object? sender, EventArgs e)
    {
        txtClave.UseSystemPasswordChar = !chkMostrar.Checked;
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void FrmLogin_Shown(object? sender, EventArgs e)
    {
        ActualizarOpcionAdministrador();
    }
}
