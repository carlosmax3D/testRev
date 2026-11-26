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
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("es-MX");

        IConfiguration configuration;
        try
        {
            configuration = Configuracion.Cargar();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Revise App.config: archivo válido, servidor y DefaultConnection.\n\n{ex.Message}", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        ServiceCollection services = new();

        services.AddSingleton(configuration);

        services.AddTransient<DatabaseConnection>();

        // Usuario que inició sesión (una sola instancia para toda la aplicación)
        services.AddSingleton<ISesionUsuario, SesionUsuario>();

        // Repositorios (acceso a datos por procedimientos almacenados)
        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<IClienteRepository, ClienteRepository>();
        services.AddTransient<IEstadioRepository, EstadioRepository>();
        services.AddTransient<IPartidoRepository, PartidoRepository>();
        services.AddTransient<IVentaRepository, VentaRepository>();
        services.AddTransient<IReporteRepository, ReporteRepository>();

        // Servicios (reglas de negocio y validaciones)
        services.AddTransient<IUsuarioService, UsuarioService>();
        services.AddTransient<IClienteService, ClienteService>();
        services.AddTransient<IEstadioService, EstadioService>();
        services.AddTransient<IPartidoService, PartidoService>();
        services.AddTransient<IVentaService, VentaService>();
        services.AddTransient<IReporteService, ReporteService>();

        // Formularios
        services.AddTransient<FrmLogin>();
        services.AddTransient<FrmClientes>();
        services.AddTransient<FrmEstadios>();
        services.AddTransient<FrmPartidos>();
        services.AddTransient<FrmVentaBoletos>();
        services.AddTransient<FrmFacturacion>();
        services.AddTransient<FrmCancelaciones>();
        services.AddTransient<FrmReporteVentas>();
        services.AddTransient<FrmReporteOcupacion>();

        using ServiceProvider serviceProvider =
            services.BuildServiceProvider();

        // Inicio de sesión -> menú principal. Al cerrar sesión se regresa al inicio de sesión.
        FrmLogin login = serviceProvider.GetRequiredService<FrmLogin>();

        Application.Run(login);
    }
}
