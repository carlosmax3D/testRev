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

        // Repositorios
        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<IClienteRepository, ClienteRepository>();
        services.AddTransient<IEquipoRepository, EquipoRepository>();
        services.AddTransient<IEstadioRepository, EstadioRepository>();
        services.AddTransient<IPartidoRepository, PartidoRepository>();
        services.AddTransient<IVentaRepository, VentaRepository>();
        services.AddTransient<IFacturaRepository, FacturaRepository>();
        services.AddTransient<ICancelacionRepository, CancelacionRepository>();
        services.AddTransient<IReporteRepository, ReporteRepository>();
        services.AddTransient<IEmpresaRepository, EmpresaRepository>();

        // Servicios
        services.AddTransient<IUsuarioService, UsuarioService>();
        services.AddTransient<IClienteService, ClienteService>();
        services.AddTransient<IEquipoService, EquipoService>();
        services.AddTransient<IEstadioService, EstadioService>();
        services.AddTransient<IPartidoService, PartidoService>();
        services.AddTransient<IVentaService, VentaService>();
        services.AddTransient<IFacturaService, FacturaService>();
        services.AddTransient<ICancelacionService, CancelacionService>();
        services.AddTransient<IReporteService, ReporteService>();
        services.AddTransient<IEmpresaService, EmpresaService>();
        services.AddSingleton<ISesionUsuario, SesionUsuario>();

        // Formularios
        services.AddTransient<FrmPrincipal>();
        services.AddTransient<FrmLogin>();
        services.AddTransient<FrmUsuarios>();
        services.AddTransient<FrmAdministradorInicial>();
        services.AddTransient<FrmEquipos>();
        services.AddTransient<FrmZonasAsientos>();
        services.AddTransient<FrmClientes>();
        services.AddTransient<FrmEstadios>();
        services.AddTransient<FrmPartidos>();
        services.AddTransient<FrmVenta>();
        services.AddTransient<FrmCancelacion>();
        services.AddTransient<FrmFactura>();
        services.AddTransient<FrmReporteVentas>();
        services.AddTransient<FrmReporteOcupacion>();
        services.AddTransient<FrmHistorialCliente>();
        services.AddTransient<FrmEmpresa>();

        using ServiceProvider serviceProvider =
            services.BuildServiceProvider();

        ISesionUsuario sesion = serviceProvider.GetRequiredService<ISesionUsuario>();

        // Login -> menú. Al cerrar sesión se regresa al login; al cerrar el menú termina la app.
        while (true)
        {
            sesion.UsuarioActual = null;
            using (IServiceScope scopeLogin = serviceProvider.CreateScope())
            {
                FrmLogin login = scopeLogin.ServiceProvider.GetRequiredService<FrmLogin>();
                if (login.ShowDialog() != DialogResult.OK || sesion.UsuarioActual is null)
                {
                    break;
                }
            }

            bool volverAlLogin;
            using (IServiceScope scopeMenu = serviceProvider.CreateScope())
            {
                FrmPrincipal principal = scopeMenu.ServiceProvider.GetRequiredService<FrmPrincipal>();
                Application.Run(principal);
                volverAlLogin = principal.CerrarSesionSolicitado;
            }

            sesion.UsuarioActual = null;
            if (!volverAlLogin)
            {
                break;
            }
        }
    }
}
