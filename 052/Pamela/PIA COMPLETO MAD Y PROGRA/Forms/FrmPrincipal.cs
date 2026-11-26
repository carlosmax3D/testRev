using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms
{
    public partial class FrmPrincipal : Form
    {
        private readonly Usuario _usuario;
        private readonly IServiceProvider _serviceProvider;

        public FrmPrincipal(Usuario usuario, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _usuario = usuario;
            _serviceProvider = serviceProvider;

            Text = "Menú principal - " + _usuario.NombreCompleto + " (" + _usuario.TipoUsuario + ")";
            StartPosition = FormStartPosition.CenterScreen;

            ConfigurarPermisos();
        }

        /// <summary>
        /// Administradores: usuarios, clientes, catálogos, ventas, reportes, cancelaciones y facturación.
        /// Operativos: venta de boletos (con su factura), clientes y reporte de ventas.
        /// </summary>
        private void ConfigurarPermisos()
        {
            bool esAdministrador = string.Equals(_usuario.TipoUsuario, "Administrador", StringComparison.OrdinalIgnoreCase);

            botonClientes.Visible = true;
            botonVenta.Visible = true;
            botonFacturacion.Visible = true;
            botonReportVent.Visible = true;

            botonUsuarios.Visible = esAdministrador;
            botonEstadios.Visible = esAdministrador;
            botonPartidos.Visible = esAdministrador;
            botonCancelacion.Visible = esAdministrador;
            botonROD.Visible = esAdministrador;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
        }

        /// <summary>Abre una pantalla registrada en el contenedor de dependencias.</summary>
        private void Abrir<TFormulario>() where TFormulario : Form
        {
            try
            {
                using TFormulario formulario = _serviceProvider.GetRequiredService<TFormulario>();
                formulario.StartPosition = FormStartPosition.CenterParent;
                formulario.ShowDialog(this);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo abrir la pantalla");
            }
        }

        private void botonCerrarSesion_Click(object sender, EventArgs e)
        {
            if (UiHelper.Confirmar("¿Estás seguro de que quieres cerrar la sesión?", "Cerrar sesión"))
            {
                Close();
            }
        }

        private void botonUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                using FrmUsuarios formulario = new FrmUsuarios(
                    _serviceProvider.GetRequiredService<IUsuarioService>(),
                    _usuario);

                formulario.StartPosition = FormStartPosition.CenterParent;
                formulario.ShowDialog(this);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo abrir la pantalla");
            }
        }

        private void botonClientes_Click(object sender, EventArgs e)
        {
            Abrir<FrmClientes>();
        }

        private void botonEstadios_Click(object sender, EventArgs e)
        {
            Abrir<FrmEstadios>();
        }

        private void botonPartidos_Click(object sender, EventArgs e)
        {
            Abrir<FrmPartidos>();
        }

        private void botonVenta_Click(object sender, EventArgs e)
        {
            Abrir<FrmVentaBoletos>();
        }

        private void botonFacturacion_Click(object sender, EventArgs e)
        {
            Abrir<FrmFacturacion>();
        }

        private void botonCancelacion_Click(object sender, EventArgs e)
        {
            Abrir<FrmCancelaciones>();
        }

        private void botonReportVent_Click(object sender, EventArgs e)
        {
            Abrir<FrmReporteVentas>();
        }

        private void botonROD_Click(object sender, EventArgs e)
        {
            Abrir<FrmReporteOcupacion>();
        }
    }
}
