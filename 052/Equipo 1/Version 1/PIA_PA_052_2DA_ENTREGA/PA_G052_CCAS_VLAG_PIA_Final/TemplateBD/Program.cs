using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Data;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Forms;
using TemplateCSharp.Forms.Cancelaciones;
using TemplateCSharp.Forms.Clientes;
using TemplateCSharp.Forms.Estadios;
using TemplateCSharp.Forms.Facturacion;
using TemplateCSharp.Forms.Menu;
using TemplateCSharp.Forms.Partidos;
using TemplateCSharp.Forms.Reportes;
using TemplateCSharp.Forms.Usuarios;
using TemplateCSharp.Forms.VentaBoletos;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;


namespace TemplateCSharp
{
    internal static class Program
    {
        [STAThread] // tuvimos que agregar eso por que sino no deja exportar correctamente
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            ServiceCollection services = new();
            services.AddSingleton(configuration);
            services.AddTransient<DatabaseConnection>();


            //Registro de Cliente
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<FrmClientes>();
            services.AddTransient<FrmClienteGuardado>();
            services.AddTransient<FrmEliminarCliente>();


            //Registro de Usuario
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<FrmUsuarios>();
            services.AddTransient<FrmEliminarUsuario>();
            services.AddTransient<FrmUsuarioCreado>();


            //Cancelaciones
            services.AddTransient<FrmCancelacion>();
            services.AddTransient<FrmCheckCancelacion>();
            services.AddTransient<FrmConfirmCancelacion>();
            services.AddTransient<ICancelacionRepository, CancelacionRepository>();
            services.AddTransient<ICancelacionService, CancelacionService>();
            services.AddTransient<FrmDetalleCancelacion>();

            //Asientos
            services.AddTransient<FrmAsientos>();
            services.AddTransient<IAsientoRepository, AsientoRepository>();
            services.AddTransient<IAsientoService, AsientoService>();
            services.AddTransient<FrmAsientosRegistrados>();
            services.AddTransient<FrmConfirmacionAsiento>();

            //Estadios
            services.AddTransient<FrmEstadios>();
            services.AddTransient<FrmConfirmacionEstadio>();
            services.AddTransient<IEstadioRepository, EstadioRepository>();
            services.AddTransient<IEstadioService, EstadioService>();

            //Zonas
            services.AddTransient<FrmZonas>();
            services.AddTransient<FrmConfirmacionZona>();
            services.AddTransient<IZonaRepository, ZonaRepository>();
            services.AddTransient<IZonaService, ZonaService>();

            //Emisor 
            services.AddTransient<IEmisorRepository, EmisorRepository>();
            services.AddTransient<IEmisorService, EmisorService>();

            //Facturas
            services.AddTransient<FrmFactura>();
            services.AddTransient<FrmMensajeFactura>();
            services.AddTransient<IFacturaRepository, FacturaRepository>();
            services.AddTransient<IFacturaService, FacturaService>();

            //Menu
            services.AddTransient<FrmMenu>();

            //Equipos
            services.AddTransient<FrmConfirmacionPartido>();
            services.AddTransient<FrmEquipos>();
            services.AddTransient<IEquipoRepository, EquipoRepository>();
            services.AddTransient<IEquipoService, EquipoService>();

            //Partidos
            services.AddTransient<FrmPartidos>();
            services.AddTransient<IPartidoRepository, PartidoRepository>();
            services.AddTransient<IPartidoService, PartidoService>();

            //Reportes
            services.AddTransient<IReporteRepository, ReporteRepository>();
            services.AddTransient<IReporteService, ReporteService>();
            services.AddTransient<FrmHistorialCliente>();
            services.AddTransient<FrmHistorialCompras>();
            services.AddTransient<FrmReporteOcupacion>();
            services.AddTransient<FrmReporteVentas>();

            //Venta Boletos
            services.AddTransient<IVentaBoletoRepository, VentaBoletoRepository>();
            services.AddTransient<IDetalleBoletoRepository, DetalleBoletoRepository>();
            services.AddTransient<IVentaBoletoService, VentaBoletoService>();

            services.AddTransient<FrmBuscarCliente>();
            services.AddTransient<FrmCompletadaVenta>();
            services.AddTransient<FrmConfirmaVenta>();
            services.AddTransient<FrmSeleccionBoletos>();
            services.AddTransient<FrmSeleccionPago>();
            services.AddTransient<FrmSeleccionPartido>();
            services.AddTransient<PagoTarjeta>();

            //Registro de Tarjeta (para validacion de pagos con tarjeta)
            services.AddTransient<ITarjetaService, TarjetaService>();
            services.AddTransient<PagoTarjeta>();


            services.AddTransient<FrmLogin>();




            using ServiceProvider serviceProvider =
                services.BuildServiceProvider();

            //FrmPrincipal form =
            //    serviceProvider.GetRequiredService<FrmPrincipal>();

            //Para que arranque con login:
            FrmLogin login = serviceProvider.GetRequiredService<FrmLogin>();
            Application.Run(login);
        }
    }
}