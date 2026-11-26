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
    .AddJsonFile("appsettings.json",optional: false,reloadOnChange: false).Build();

ServiceCollection services = new();

services.AddSingleton(configuration);

services.AddTransient<DatabaseConnection>();

services.AddTransient<IProductoRepository, ProductoRepository>();

services.AddTransient<IUsuarioRepository, UsuarioRepository>();

services.AddTransient<IProductoService, ProductoService>();

services.AddTransient<IUsuarioService, UsuarioService>();

services.AddTransient<inicioSesion>();
services.AddTransient<FrmProductos>();
services.AddTransient<FrmUsuarios>();
services.AddTransient<FrmClientes>();
services.AddTransient<FrmEstadios>();
services.AddTransient<FrmPartidos>();
services.AddTransient<FrmVentaBoletos>();
services.AddTransient<FrmFacturación>();
services.AddTransient<FrmCancelaciones>();
services.AddTransient<FrmReporteVentas>();
services.AddTransient<FrmReporteOcupacion>();

using ServiceProvider serviceProvider =services.BuildServiceProvider();

inicioSesion form =serviceProvider.GetRequiredService<inicioSesion>();

Application.Run(form);