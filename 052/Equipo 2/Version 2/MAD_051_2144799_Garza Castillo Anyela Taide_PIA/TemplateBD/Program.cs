using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TemplateCSharp.Data;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Forms;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;
namespace TemplateCSharp
{
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
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<FrmLogin>();
            services.AddTransient<FrmPrincipal>();
            using ServiceProvider serviceProvider =
                services.BuildServiceProvider();
            FrmLogin form =
                serviceProvider.GetRequiredService<FrmLogin>();
            Application.Run(form);
        }
    }
}