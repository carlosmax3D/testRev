using Microsoft.Extensions.DependencyInjection;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

public partial class FrmPrincipal : Form
{
    private readonly IServiceProvider _servicios;
    private readonly ISesionActual _sesion;

    public bool CerrarSesionSolicitado { get; private set; }

    public FrmPrincipal(IServiceProvider servicios, ISesionActual sesion)
    {
        _servicios = servicios;
        _sesion = sesion;

        InitializeComponent();

        lblBienvenida.Text = $"Bienvenido, {_sesion.NombreUsuario}";
        lblRol.Text = $"Perfil: {_sesion.TipoUsuario}";
        lblAyuda.Text = _sesion.EsAdministrador
            ? "Seleccione una opción del menú para administrar el sistema."
            : "Seleccione una opción del menú para registrar clientes y vender boletos.";

        // Solo el administrador ve estas opciones.
        bool admin = _sesion.EsAdministrador;

        foreach (Button b in new[]
                 { btnUsuarios, btnEstadios, btnPartidos, btnCancelaciones, btnFacturacion, btnRepVentas, btnRepOcupacion, btnHistorial })
        {
            b.Visible = admin;
        }
    }

    private void Abrir<T>() where T : Form
    {
        using T form = _servicios.GetRequiredService<T>();
        form.ShowDialog(this);
    }

    private void BtnClientes_Click(object? sender, EventArgs e) => Abrir<FrmClientes>();
    private void BtnVenta_Click(object? sender, EventArgs e) => Abrir<FrmVentaBoletos>();
    private void BtnUsuarios_Click(object? sender, EventArgs e) => Abrir<FrmUsuarios>();
    private void BtnEstadios_Click(object? sender, EventArgs e) => Abrir<FrmEstadios>();
    private void BtnPartidos_Click(object? sender, EventArgs e) => Abrir<FrmPartidos>();
    private void BtnCancelaciones_Click(object? sender, EventArgs e) => Abrir<FrmCancelaciones>();
    private void BtnFacturacion_Click(object? sender, EventArgs e) => Abrir<FrmFacturacion>();
    private void BtnRepVentas_Click(object? sender, EventArgs e) => Abrir<FrmReporteVentas>();
    private void BtnRepOcupacion_Click(object? sender, EventArgs e) => Abrir<FrmReporteOcupacion>();
    private void BtnHistorial_Click(object? sender, EventArgs e) => Abrir<FrmHistorialCliente>();

    private void BtnCerrarSesion_Click(object? sender, EventArgs e)
    {
        if (!Ui.Confirmar(this, "¿Desea cerrar la sesión?"))
        {
            return;
        }

        _sesion.CerrarSesion();
        CerrarSesionSolicitado = true;
        Close();
    }
}
