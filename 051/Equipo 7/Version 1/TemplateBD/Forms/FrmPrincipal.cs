using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Models;
using TemplateCSharp.Services;
using TemplateCSharp.Forms;


namespace TemplateCSharp.Forms;

public partial class FrmPrincipal : Form
{
    private readonly IServiceProvider _serviceProvider;

    public FrmPrincipal(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    private void FrmPrincipal_Load(object sender, EventArgs e)
    {



        if (this.Tag is Usuario usuarioLogueado)
        {
            this.Text = $"Sistema de Venta de Boletos - {usuarioLogueado.TipoUsuario}: {usuarioLogueado.NombreCompleto}";

            // El Operativo solo puede administrar clientes y vender boletos
            if (usuarioLogueado.TipoUsuario == "Operativo")
            {
                btnUsuarios.Visible = false;
                btnEstadio.Visible = false;
                btnZona.Visible = false;
                btnPartido.Visible = false;
                btnEquipo.Visible = false;
                btnFactura.Visible = false;
                btnCancelaciones.Visible = false;
                btnDetalleVenta.Visible = false;
            }
        }
    }

    private void btnUsuarios_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmUsuarios>();
        frm.ShowDialog();
    }

    private void btnClientes_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmClientes>();
        frm.ShowDialog();
    }

    private void btnEstadio_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmEstadio>();
        frm.ShowDialog();
    }

    private void btnZona_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmZona>();
        frm.ShowDialog();
    }

    private void btnEquipo_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmEquipo>();
        frm.ShowDialog();
    }

    private void btnPartido_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmPartido>();
        frm.ShowDialog();
    }

    private void btnCompra_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmCompra>();
        frm.ShowDialog();
    }

    private void btnFacturacion_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmFacturacion>();
        frm.ShowDialog();
    }

    private void btnCancelaciones_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmCancelaciones>();
        frm.ShowDialog();
    }

    // Adaptado para abrir la ventana con los 3 Reportes
    private void btnDetalleVenta_Click(object sender, EventArgs e)
    {
        var frm = _serviceProvider.GetRequiredService<FrmReportes>();
        frm.ShowDialog();
    }

    private void btnSalida_Click(object sender, EventArgs e)
    {
        this.Close();
    }
    private void btnAsiento_Click(object sender, EventArgs e) 
    { 
    }
    private void btnPago_Click(object sender, EventArgs e) 
    { 
    }
}