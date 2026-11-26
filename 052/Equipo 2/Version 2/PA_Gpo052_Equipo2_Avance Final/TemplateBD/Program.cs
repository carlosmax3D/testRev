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


//services.AddTransient<IProductoRepository, ProductoRepository>();

//services.AddTransient<IProductoService, ProductoService>();


// Repositorios
services.AddTransient<IUsuarioRepository, UsuarioRepository>();
services.AddTransient<IPermisoRepository, PermisoRepository>();
services.AddTransient<ITipoUsuarioRepository, TipoUsuarioRepository>();

// Servicios
services.AddTransient<IUsuarioService, UsuarioService>();
services.AddTransient<IPermisoService, PermisoService>();
services.AddTransient<ITipoUsuarioService, TipoUsuarioService>();


// Formularios
//services.AddTransient<FrmPrincipal>();
services.AddTransient<FrmLogin>();
services.AddTransient<FrmPrincipal>();
services.AddTransient<FrmUsuarios>();
//services.AddTransient<FrmProductos>();

using ServiceProvider serviceProvider =
    services.BuildServiceProvider();

//FrmPrincipal form =
// serviceProvider.GetRequiredService<FrmPrincipal>();

FrmLogin form =
    serviceProvider.GetRequiredService<FrmLogin>();

Application.Run(form);
