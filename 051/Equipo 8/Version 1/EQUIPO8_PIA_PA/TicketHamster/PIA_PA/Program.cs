using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIA_PA.Comun.Data;
using PIA_PA.Entidades.Area;
using PIA_PA.Entidades.Asiento;
using PIA_PA.Entidades.Cancelacion;
using PIA_PA.Entidades.Cliente;
using PIA_PA.Entidades.Contrasena;
using PIA_PA.Entidades.Deporte;
using PIA_PA.Entidades.Direccion;
using PIA_PA.Entidades.Emisor;
using PIA_PA.Entidades.Equipos;
using PIA_PA.Entidades.Estadio;
using PIA_PA.Entidades.Pagador;
using PIA_PA.Entidades.Pago;
using PIA_PA.Entidades.Partido;
using PIA_PA.Entidades.Partido_Area_Precio;
using PIA_PA.Entidades.Telefono;
using PIA_PA.Entidades.Ticket;
using PIA_PA.Entidades.Usuario;
using PIA_PA.Entidades.Venta_Info;
using PIA_PA.Menus;


namespace PIA_PA
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("Comun/appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddTransient<DatabaseConnection>();

            services.AddTransient<IDireccionRepository, DireccionRepository>();
            services.AddTransient<IDireccionService, DireccionService>();
            services.AddTransient<ITelefonoRepository, TelefonoRepository>();
            services.AddTransient<ITelefonoService, TelefonoService>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IEstadioRepository, EstadioRepository>();
            services.AddTransient<IEstadioService, EstadioService>();
            services.AddTransient<IEquiposRepository, EquiposRepository>();
            services.AddTransient<IEquiposService, EquiposService>();
            services.AddTransient<IContrasenaRepository, ContrasenaRepository>();
            services.AddTransient<IContrasenaService, ContrasenaService>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<IDeporteRepository, DeporteRepository>();
            services.AddTransient<IDeporteService, DeporteService>();
            services.AddTransient<IPartidoRepository, PartidoRepository>();
            services.AddTransient<IPartidoService, PartidoService>();
            services.AddTransient<IPagoRepository, PagoRepository>();
            services.AddTransient<IPagoService, PagoService>();
            services.AddTransient<IPagadorRepository, PagadorRepository>();
            services.AddTransient<IPagadorService, PagadorService>();
            services.AddTransient<IEmisorRepository, EmisorRepository>();
            services.AddTransient<IEmisorService, EmisorService>();
            services.AddTransient<ConsultarVenta>();
            services.AddTransient<IVenta_InfoRepository, Venta_InfoRepository>();
            services.AddTransient<IVenta_InfoService, Venta_InfoService>();
            services.AddTransient<IAsientoRepository, AsientoRepository>();
            services.AddTransient<IAsientoService, AsientoService>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IPartido_Area_PrecioRepository, Partido_Area_PrecioRepository>();
            services.AddTransient<IPartido_Area_PrecioService, Partido_Area_PrecioService>();
            services.AddTransient<ICancelacionRepository, CancelacionRepository>();
            services.AddTransient<ConsultarPartidos>();
            services.AddTransient<AgregarEquipo>();
            services.AddTransient<AgregarPartido>();
            services.AddTransient<ModificarPartido>();
            services.AddTransient<PIA_PA.Menus.CancelarPartido>();
            services.AddTransient<ConsultarEstadio>();
            services.AddTransient<AgregarEstadio>();
            services.AddTransient<ConsultarEquipo>();
            services.AddTransient<ConsultarCliente>();
            services.AddTransient<ConsultarBoleto>();
            services.AddTransient<ConsultarEmpleado>();
            services.AddTransient<ICancelacionService, CancelacionService>();
            services.AddTransient<EliminarEquipo>();
            services.AddTransient<ModificarEquipo>();

            services.AddTransient<AgregarCliente>();
            services.AddTransient<ModificarCliente>();
            services.AddTransient<DesactivarCliente>();
            services.AddTransient<AgregarUsuario>();
            services.AddTransient<ModificarUsuario>();
            services.AddTransient<DesactivarUsuario>();
            services.AddTransient<AgregarEmpleado>();
            services.AddTransient<ModificarEmpleado>();
            services.AddTransient<DesactivarEmpleado>();
            services.AddTransient<ModificarEstadio>();
            services.AddTransient<EliminarEstadio>();
            services.AddTransient<Nueva>();
            services.AddTransient<AgregarVenta>();
            services.AddTransient<AgregarPrecioBoleto>();
            services.AddTransient<Historial>();
            services.AddTransient<Form1>();
            services.AddTransient<Form2>();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            Application.Run(serviceProvider.GetRequiredService<Form2>());
        }
    }
}
