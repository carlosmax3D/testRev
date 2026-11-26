using TemplateCSharp.Data;

namespace TemplateCSharp.Forms;

public partial class FrmPrincipal : Form
{
    private readonly FrmClientes _frmClientes;
    private readonly FrmVentas _frmVentas;
    private readonly FrmReportes _frmReportes;
    private readonly FrmUsuarios _frmUsuarios;
    private readonly FrmCatalogos _frmCatalogos;

    public FrmPrincipal(FrmClientes frmClientes, FrmVentas frmVentas, FrmReportes frmReportes, FrmUsuarios frmUsuarios, FrmCatalogos frmCatalogos)
    {
        InitializeComponent();
        _frmClientes = frmClientes;
        _frmVentas = frmVentas;
        _frmReportes = frmReportes;
        _frmUsuarios = frmUsuarios;
        _frmCatalogos = frmCatalogos;
    }

    public void ConfigurarPermisos()
    {
        this.Text = $"Sistema de Venta de Boletos - Usuario: {SesionActual.NombreCompleto} ({SesionActual.Rol})";
        bool esAdmin = SesionActual.Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase);

        btnUsuarios.Visible = esAdmin;
        btnCatalogos.Visible = esAdmin;
        btnCancelaciones.Visible = esAdmin;
    }

    private void btnClientes_Click(object sender, EventArgs e) => _frmClientes.ShowDialog();
    private void btnVentas_Click(object sender, EventArgs e) => _frmVentas.ShowDialog();
    private void btnReportes_Click(object sender, EventArgs e) => _frmReportes.ShowDialog();
    private void btnUsuarios_Click(object sender, EventArgs e) => _frmUsuarios.ShowDialog();
    private void btnCatalogos_Click(object sender, EventArgs e) => _frmCatalogos.ShowDialog();
    private void btnCancelaciones_Click(object sender, EventArgs e) => _frmVentas.ShowDialog();
    private void btnClientes_Click_1(object sender, EventArgs e) => _frmClientes.ShowDialog();
    private void btnVentas_Click_1(object sender, EventArgs e) => _frmVentas.ShowDialog();
    private void button1_Click(object sender, EventArgs e) => _frmReportes.ShowDialog();
    private void btnProductos_Click(object sender, EventArgs e) => _frmReportes.ShowDialog();

    private void btnUsuarios_Click_1(object sender, EventArgs e)
    {
        _frmUsuarios.ShowDialog();
    }

    private void btnCatalogos_Click_1(object sender, EventArgs e)
    {
        _frmCatalogos.ShowDialog();
    }

    private void btnCancelaciones_Click_1(object sender, EventArgs e)
    {
        _frmVentas.ShowDialog();
    }

}
