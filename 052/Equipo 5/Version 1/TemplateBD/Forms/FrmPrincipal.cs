using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmPrincipal : Form
{
    private readonly IServiceProvider _serviceProvider;
    private readonly SesionUsuario _sesion;

    public FrmPrincipal(IServiceProvider serviceProvider, SesionUsuario sesion)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _sesion = sesion;
    }

    private void FrmPrincipal_Load(object sender, EventArgs e)
    {
        ValidarYAplicarPermisos();
    }

    private void ValidarYAplicarPermisos()
    {
        // Determinamos si es Admin revisando SesionUsuario o el texto del Rol
        bool esAdmin = _sesion.EsAdmin ||
                      (_sesion.UsuarioActual == null) || // Si es el usuario por defecto de pruebas
                      string.Equals(_sesion.UsuarioActual?.Rol?.Trim(), "Admin", StringComparison.OrdinalIgnoreCase) ||
                      string.Equals(_sesion.UsuarioActual?.Rol?.Trim(), "Administrador", StringComparison.OrdinalIgnoreCase);

        if (_sesion.UsuarioActual != null)
        {
            lblInfoUsuario.Text = $"Conectado como: {_sesion.UsuarioActual.NombreCompleto} | Rol: {_sesion.UsuarioActual.Rol}";
        }
        else
        {
            lblInfoUsuario.Text = "Conectado como: Administrador del Sistema | Rol: Admin";
        }

        // Todos los usuarios (Operativo y Admin) tienen acceso a los módulos de trabajo diario
        btnClientes.Enabled = true;
        btnEstadios.Enabled = true;
        btnPartidos.Enabled = true;
        btnVentas.Enabled = true;
        btnFacturacion.Enabled = true;

        // Módulos administrativos: habilitados SOLO si esAdmin es true
        btnUsuarios.Enabled = esAdmin;
        btnCancelaciones.Enabled = esAdmin;
        btnReportes.Enabled = esAdmin;
    }

    private bool ValidarAccesoAdmin()
    {
        bool esAdmin = _sesion.EsAdmin ||
                      (_sesion.UsuarioActual == null) ||
                      string.Equals(_sesion.UsuarioActual?.Rol?.Trim(), "Admin", StringComparison.OrdinalIgnoreCase) ||
                      string.Equals(_sesion.UsuarioActual?.Rol?.Trim(), "Administrador", StringComparison.OrdinalIgnoreCase);

        if (!esAdmin)
        {
            MessageBox.Show("Acceso denegado: Este módulo requiere permisos de Administrador.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    private void btnUsuarios_Click(object sender, EventArgs e)
    {
        if (!ValidarAccesoAdmin()) return;
        using FrmUsuarios form = _serviceProvider.GetRequiredService<FrmUsuarios>();
        form.ShowDialog();
    }

    private void btnClientes_Click(object sender, EventArgs e)
    {
        using FrmClientes form = _serviceProvider.GetRequiredService<FrmClientes>();
        form.ShowDialog();
    }

    private void btnEstadios_Click(object sender, EventArgs e)
    {
        using FrmEstadios form = _serviceProvider.GetRequiredService<FrmEstadios>();
        form.ShowDialog();
    }

    private void btnPartidos_Click(object sender, EventArgs e)
    {
        using FrmPartidos form = _serviceProvider.GetRequiredService<FrmPartidos>();
        form.ShowDialog();
    }

    private void btnVentas_Click(object sender, EventArgs e)
    {
        using FrmVentas form = _serviceProvider.GetRequiredService<FrmVentas>();
        form.ShowDialog();
    }

    private void btnCancelaciones_Click(object sender, EventArgs e)
    {
        if (!ValidarAccesoAdmin()) return;
        using FrmCancelaciones form = _serviceProvider.GetRequiredService<FrmCancelaciones>();
        form.ShowDialog();
    }

    private void btnFacturacion_Click(object sender, EventArgs e)
    {
        using FrmFacturacion form = _serviceProvider.GetRequiredService<FrmFacturacion>();
        form.ShowDialog();
    }

    private void btnReportes_Click(object sender, EventArgs e)
    {
        if (!ValidarAccesoAdmin()) return;
        using FrmReportes form = _serviceProvider.GetRequiredService<FrmReportes>();
        form.ShowDialog();
    }

    private void btnCerrarSesion_Click(object sender, EventArgs e)
    {
        _sesion.UsuarioActual = null;
        this.Close();
    }

    private void MostrarAvisoModulo(string nombreModulo)
    {
        MessageBox.Show($"Módulo de {nombreModulo} listo para integración de pantalla.", "Navegación", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}