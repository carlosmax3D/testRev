using Microsoft.Extensions.DependencyInjection;

namespace TemplateCSharp.Forms;

public partial class FrmPrincipal1 : Form
{
    private readonly IServiceProvider _serviceProvider;

    public FrmPrincipal1(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
    }

    private void btnProductos_Click(object sender, EventArgs e)
    {
        using FrmProductos form =
            _serviceProvider.GetRequiredService<FrmProductos>();

        form.ShowDialog();
    }
}
