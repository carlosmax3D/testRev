using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Data;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Forms;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;

namespace TemplateCSharp;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(
                "appsettings.json",
                optional: false,
                reloadOnChange: false)
            .Build();

        ServiceCollection services = new();

        services.AddSingleton(configuration);
        services.AddTransient<DatabaseConnection>();
        services.AddTransient<Ejecutor>();

        services.AddTransient<IProductoRepository, ProductoRepository>();
        services.AddTransient<IProductoService, ProductoService>();

        services.AddTransient<FormLogin>();
        services.AddTransient<FrmPrincipal>();
        services.AddTransient<FrmProductos>();
        services.AddTransient<FrmClientes>();
        services.AddTransient<FrmVentas>();
        services.AddTransient<FrmReportes>();
        services.AddTransient<FrmUsuarios>();
        services.AddTransient<FrmCatalogos>();

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        FormLogin form = serviceProvider.GetRequiredService<FormLogin>();
        Application.Run(form);
    }
}






