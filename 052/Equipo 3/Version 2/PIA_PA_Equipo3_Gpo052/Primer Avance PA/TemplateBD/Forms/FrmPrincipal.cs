using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmPrincipal : FormBase
{
    private readonly IServiceProvider _serviceProvider;

    public FrmPrincipal(IServiceProvider serviceProvider)
        : base("Sistema de Venta de Boletos", 480, 540)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        AplicarPermisos();
    }

    /// <summary>
    /// Administrador: usuarios, clientes, catálogos, ventas, reportes, cancelaciones y facturación.
    /// Operativo: solo venta de boletos y administración de clientes (el resto se oculta).
    /// </summary>
    private void AplicarPermisos()
    {
        if (SesionActual.RolUsuario == "Administrador") return;

        Button[] soloAdministrador =
        {
            btnUsuarios, btnEstadios, btnZonas, btnAsientos, btnPartidos,
            btnCancelaciones, btnFacturacion,
            btnReporteVentas, btnReporteOcupacion, btnHistorialCliente
        };

        foreach (Button boton in soloAdministrador)
            boton.Visible = false;

        // Acomoda los botones que quedan en una sola columna centrada, sin huecos
        Button[] visibles = { btnClientes, btnVentas, btnSalir };
        int paso = btnClientes.Top - btnUsuarios.Top;
        int inicioY = btnUsuarios.Top;
        int x = (ClientSize.Width - btnClientes.Width) / 2;

        for (int i = 0; i < visibles.Length; i++)
            visibles[i].Location = new Point(x, inicioY + i * paso);

        int separacion = paso - btnClientes.Height;
        ClientSize = new Size(ClientSize.Width, visibles[^1].Bottom + separacion * 3);
    }

    // Botón para Usuarios
    private void btnUsuarios_Click(object sender, EventArgs e)
    {
        using FrmUsuarios form = _serviceProvider.GetRequiredService<FrmUsuarios>();
        form.ShowDialog();
    }

    // Botón para Clientes
    private void btnClientes_Click(object sender, EventArgs e)
    {
        using FrmClientes form = _serviceProvider.GetRequiredService<FrmClientes>();
        form.ShowDialog();
    }

    // Botón para Estadios
    private void btnEstadios_Click(object sender, EventArgs e)
    {
        using FrmEstadios form = _serviceProvider.GetRequiredService<FrmEstadios>();
        form.ShowDialog();
    }

    // Botón para Zonas
    private void btnZonas_Click(object sender, EventArgs e)
    {
        using FrmZonas form = _serviceProvider.GetRequiredService<FrmZonas>();
        form.ShowDialog();
    }

    // Botón para Asientos
    private void btnAsientos_Click(object sender, EventArgs e)
    {
        using FrmAsientos form = _serviceProvider.GetRequiredService<FrmAsientos>();
        form.ShowDialog();
    }

    // Botón para Partidos
    private void btnPartidos_Click(object sender, EventArgs e)
    {
        using FrmPartidos form = _serviceProvider.GetRequiredService<FrmPartidos>();
        form.ShowDialog();
    }

    // Botón para Venta de Boletos
    private void btnVentas_Click(object sender, EventArgs e)
    {
        using FrmVentaBoletos form = _serviceProvider.GetRequiredService<FrmVentaBoletos>();
        form.ShowDialog();
    }

    // Botón para Cancelaciones
    private void btnCancelaciones_Click(object sender, EventArgs e)
    {
        using FrmCancelaciones form = _serviceProvider.GetRequiredService<FrmCancelaciones>();
        form.ShowDialog();
    }

    // Botón para Facturación
    private void btnFacturacion_Click(object sender, EventArgs e)
    {
        using FrmFacturacion form = _serviceProvider.GetRequiredService<FrmFacturacion>();
        form.ShowDialog();
    }

    // Botón para Reporte de Ventas
    private void btnReporteVentas_Click(object sender, EventArgs e)
    {
        using FrmReporteVentas form = _serviceProvider.GetRequiredService<FrmReporteVentas>();
        form.ShowDialog();
    }

    // Botón para Reporte de Ocupación
    private void btnReporteOcupacion_Click(object sender, EventArgs e)
    {
        using FrmReporteOcupacion form = _serviceProvider.GetRequiredService<FrmReporteOcupacion>();
        form.ShowDialog();
    }

    // Botón para Historial de Cliente
    private void btnHistorialCliente_Click(object sender, EventArgs e)
    {
        using FrmHistorialCliente form = _serviceProvider.GetRequiredService<FrmHistorialCliente>();
        form.ShowDialog();
    }

    private void btnSalir_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
