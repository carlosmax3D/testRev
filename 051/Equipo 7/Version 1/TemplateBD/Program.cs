using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp;
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

        services.AddTransient<IProductoRepository, ProductoRepository>();

        services.AddTransient<IProductoService, ProductoService>();

        services.AddTransient<FrmPrincipal>();
        services.AddTransient<FrmProductos>();
        services.AddTransient<FrmIniciarSesion>();
        services.AddTransient<FrmClientes>();
        services.AddTransient<FrmEstadio>();
        services.AddTransient<FrmEquipo>();
        services.AddTransient<FrmZona>();
        services.AddTransient<FrmPartido>();
        services.AddTransient<FrmCompra>();
        services.AddTransient<FrmFacturacion>();
        services.AddTransient<FrmCancelaciones>();
        services.AddTransient<FrmReportes>();
        services.AddTransient<FrmUsuarios>();


        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<IUsuarioService, UsuarioService>();
        services.AddTransient<IClienteRepository, ClienteRepository>();
        services.AddTransient<IClienteService, ClienteService>();
        services.AddTransient<IEstadioRepository, EstadioRepository>();
        services.AddTransient<IEstadioService, EstadioService>();
        services.AddTransient<IEquipoRepository, EquipoRepository>();
        services.AddTransient<IEquipoService, EquipoService>();
        services.AddTransient<IZonaRepository, ZonaRepository>();
        services.AddTransient<IZonaService, ZonaService>();
        services.AddTransient<IPartidoRepository, PartidoRepository>();
        services.AddTransient<IPartidoService, PartidoService>();
        services.AddTransient<IVentaRepository, VentaRepository>();
        services.AddTransient<IVentaService, VentaService>();
        services.AddTransient<IFacturaRepository, FacturaRepository>();
        services.AddTransient<IFacturaService, FacturaService>();
        services.AddTransient<IReporteRepository, ReporteRepository>();
        services.AddTransient<IReporteService, ReporteService>();

        using ServiceProvider serviceProvider = services.BuildServiceProvider();


        serviceProvider.GetRequiredService<IUsuarioService>().InicializarCredencialesBase();

        FrmIniciarSesion form = serviceProvider.GetRequiredService<FrmIniciarSesion>();
        Application.Run(form);
    }
}