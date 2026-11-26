using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Data;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Forms;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;

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


// Nuevos repositorios
services.AddTransient<IUsuarioRepository, UsuarioRepository>();
services.AddTransient<IClienteRepository, ClienteRepository>();


// Nuevos servicios
services.AddTransient<IUsuarioService, UsuarioService>();
services.AddTransient<IClienteService, ClienteService>();

services.AddTransient<FrmPrincipal>();


// Registrar nuevos formularios
services.AddTransient<FrmLogin>();
services.AddTransient<FrmUsuarios>();
services.AddTransient<FrmClientes>();
services.AddTransient<FrmEstadios>();
services.AddTransient<FrmPartidos>();
services.AddTransient<FrmZonas>();
services.AddTransient<FrmAsientos>();
services.AddTransient<FrmVentaBoletos>();
services.AddTransient<FrmCancelaciones>();
services.AddTransient<FrmFacturacion>();
services.AddTransient<FrmReporteVentas>();
services.AddTransient<FrmReporteOcupacion>();
services.AddTransient<FrmHistorialCliente>();

using ServiceProvider serviceProvider =
    services.BuildServiceProvider();

// Cambiamos esta línea para que arranque el Login, no el Principal:
FrmLogin formLogin = serviceProvider.GetRequiredService<FrmLogin>();
Application.Run(formLogin);
