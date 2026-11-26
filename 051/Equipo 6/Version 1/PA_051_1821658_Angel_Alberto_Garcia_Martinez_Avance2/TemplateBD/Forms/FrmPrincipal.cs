using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmPrincipal : Form
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISesionUsuario _sesion;

    public bool CerrarSesionSolicitado { get; private set; }

    public FrmPrincipal(IServiceProvider serviceProvider, ISesionUsuario sesion)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _sesion = sesion;

        if (_sesion.UsuarioActual is null)
        {
            throw new InvalidOperationException("Debe iniciar sesión antes de abrir el menú.");
        }

        ConfigurarPermisos(_sesion.UsuarioActual);
    }

    // El operativo solo ve Clientes, Venta de boletos y Reporte de ventas
    private void ConfigurarPermisos(Usuario usuario)
    {
        bool admin = usuario.EsAdministrador;
        Text = $"Sistema de venta de boletos - {usuario.NombreCompleto} ({usuario.TipoUsuario})";
        lblBienvenida.Text = $"Bienvenido, {usuario.NombreCompleto}";
        btnUsuarios.Visible = admin;
        btnEstadios.Visible = admin;
        btnEquipos.Visible = admin;
        btnZonasAsientos.Visible = admin;
        btnPartidos.Visible = admin;
        btnCancelacion.Visible = admin;
        btnFactura.Visible = admin;
        btnReporteOcupacion.Visible = admin;
        btnHistorialCliente.Visible = admin;
        btnEmpresa.Visible = admin;
    }

    // Cada ventana se crea en su propio scope y se libera al cerrarla
    private void AbrirVentana<T>(bool soloAdministrador) where T : Form
    {
        if (_sesion.UsuarioActual is null)
        {
            Mensajes.Aviso("Inicie sesión.");
            return;
        }

        if (soloAdministrador && !_sesion.UsuarioActual.EsAdministrador)
        {
            Mensajes.Aviso("Esta opción requiere un administrador.");
            return;
        }

        using IServiceScope scope = _serviceProvider.CreateScope();
        T formulario = scope.ServiceProvider.GetRequiredService<T>();
        formulario.ShowDialog(this);
    }

    private void btnUsuarios_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmUsuarios>(soloAdministrador: true);
    }

    private void btnClientes_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmClientes>(soloAdministrador: false);
    }

    private void btnEstadios_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmEstadios>(soloAdministrador: true);
    }

    private void btnEquipos_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmEquipos>(soloAdministrador: true);
    }

    private void btnZonasAsientos_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmZonasAsientos>(soloAdministrador: true);
    }

    private void btnPartidos_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmPartidos>(soloAdministrador: true);
    }

    private void btnVenta_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmVenta>(soloAdministrador: false);
    }

    private void btnCancelacion_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmCancelacion>(soloAdministrador: true);
    }

    private void btnFactura_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmFactura>(soloAdministrador: true);
    }

    private void btnReporteVentas_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmReporteVentas>(soloAdministrador: false);
    }

    private void btnReporteOcupacion_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmReporteOcupacion>(soloAdministrador: true);
    }

    private void btnHistorialCliente_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmHistorialCliente>(soloAdministrador: true);
    }

    private void btnEmpresa_Click(object? sender, EventArgs e)
    {
        AbrirVentana<FrmEmpresa>(soloAdministrador: true);
    }

    private void btnCerrarSesion_Click(object? sender, EventArgs e)
    {
        if (!Mensajes.Confirmar("¿Desea cerrar la sesión?"))
        {
            return;
        }

        _sesion.UsuarioActual = null;
        CerrarSesionSolicitado = true;
        Close();
    }
}
