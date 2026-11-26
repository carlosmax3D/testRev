using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VentaBoletosApp.Data;
using VentaBoletosApp.Data.Repositories;
using VentaBoletosApp.Forms;
using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Services;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-MX");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-MX");
CultureInfo.CurrentCulture = new CultureInfo("es-MX");

ApplicationConfiguration.Initialize();

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

ServiceCollection services = new();

services.AddSingleton(configuration);
services.AddTransient<DatabaseConnection>();

// Sesion (una sola para toda la aplicacion)
services.AddSingleton<ISesionActual, SesionActual>();

// Repositorios
services.AddTransient<ICatalogoRepository, CatalogoRepository>();
services.AddTransient<IUsuarioRepository, UsuarioRepository>();
services.AddTransient<IClienteRepository, ClienteRepository>();
services.AddTransient<IEstadioRepository, EstadioRepository>();
services.AddTransient<IZonaRepository, ZonaRepository>();
services.AddTransient<IAsientoRepository, AsientoRepository>();
services.AddTransient<IPartidoRepository, PartidoRepository>();
services.AddTransient<IVentaRepository, VentaRepository>();
services.AddTransient<IFacturaRepository, FacturaRepository>();
services.AddTransient<ICancelacionRepository, CancelacionRepository>();
services.AddTransient<IEmpresaRepository, EmpresaRepository>();
services.AddTransient<IReporteRepository, ReporteRepository>();

// Servicios
services.AddTransient<ICatalogoService, CatalogoService>();
services.AddTransient<IUsuarioService, UsuarioService>();
services.AddTransient<IClienteService, ClienteService>();
services.AddTransient<IEstadioService, EstadioService>();
services.AddTransient<IPartidoService, PartidoService>();
services.AddTransient<IVentaService, VentaService>();
services.AddTransient<ICancelacionService, CancelacionService>();
services.AddTransient<IFacturaPdfGenerator, FacturaPdfGenerator>();
services.AddTransient<IFacturaService, FacturaService>();
services.AddTransient<IReporteService, ReporteService>();

// Pantallas
services.AddTransient<FrmLogin>();
services.AddTransient<FrmPrincipal>();
services.AddTransient<FrmUsuarios>();
services.AddTransient<FrmClientes>();
services.AddTransient<FrmEstadios>();
services.AddTransient<FrmPartidos>();
services.AddTransient<FrmVentaBoletos>();
services.AddTransient<FrmCancelaciones>();
services.AddTransient<FrmFacturacion>();
services.AddTransient<FrmReporteVentas>();
services.AddTransient<FrmReporteOcupacion>();
services.AddTransient<FrmHistorialCliente>();

using ServiceProvider serviceProvider = services.BuildServiceProvider();

while (true)
{
    using (FrmLogin login = serviceProvider.GetRequiredService<FrmLogin>())
    {
        if (login.ShowDialog() != DialogResult.OK)
        {
            break;
        }
    }

    FrmPrincipal principal = serviceProvider.GetRequiredService<FrmPrincipal>();

    Application.Run(principal);

    if (!principal.CerrarSesionSolicitado)
    {
        break;
    }
}
