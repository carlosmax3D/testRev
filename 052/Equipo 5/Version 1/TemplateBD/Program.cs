using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Data;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Forms;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp;

internal static class Program
{
    [STAThread]//esto permite el uso de portapapeles (dios tengo mucho sueño) y de arrastrar y pegar cosas
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

        var services = new ServiceCollection();
        services.AddSingleton(configuration);
        ConfigureServices(services);
        using var serviceProvider = services.BuildServiceProvider();
        var loginForm = serviceProvider.GetRequiredService<FrmLogin>();
        Application.Run(loginForm);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Infraestructura y estado
        services.AddSingleton<DatabaseConnection>();
        services.AddSingleton<SesionUsuario>();

        // Repositorios
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();

        // Servicios
        services.AddScoped<UsuarioService>();
        services.AddScoped<ClienteService>();

        // Formularios
        services.AddTransient<FrmLogin>();
        services.AddTransient<FrmPrincipal>();
        services.AddTransient<FrmUsuarios>();
        services.AddTransient<FrmClientes>();

        // Estadio
        services.AddScoped<EstadioRepository>();
        services.AddTransient<FrmEstadios>();

        // Partido
        services.AddScoped<PartidoRepository>();
        services.AddTransient<FrmPartidos>();

        // Boleto
        services.AddScoped<VentaRepository>();
        services.AddTransient<FrmVentas>();
        services.AddTransient<ClienteRepository>();

        // Cancelacion
        services.AddScoped<CancelacionRepository>();
        services.AddTransient<FrmCancelaciones>();

        // Facturacion
        services.AddScoped<FacturaRepository>();
        services.AddTransient<FrmFacturacion>();

        // Reportes
        services.AddScoped<ReporteRepository>();
        services.AddTransient<FrmReportes>();
    }
}