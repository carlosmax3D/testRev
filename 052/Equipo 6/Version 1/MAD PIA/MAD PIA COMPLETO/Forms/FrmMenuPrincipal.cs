using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms
{
    public partial class FrmMenuPrincipal : Form
    {
        private readonly Usuario _usuario;
        private readonly IServiceProvider _serviceProvider;
        public FrmMenuPrincipal(Usuario usuario,IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _usuario = usuario;
            _serviceProvider = serviceProvider;

            ConfigurarPermisos();
        }

        private void ConfigurarPermisos()
        {
            bool esAdministrador = string.Equals(_usuario.TipoUsuario,"Administrador",StringComparison.OrdinalIgnoreCase);

            botonClientes.Visible = true;
            botonVenta.Visible = true;
            botonFacturacion.Visible = true;

            botonEstadios.Visible = esAdministrador;
            botonPartidos.Visible = esAdministrador;
            botonUsuarios.Visible = esAdministrador;
            botonCancelacion.Visible = esAdministrador;
            botonReportVent.Visible = esAdministrador;
            botonROD.Visible = esAdministrador;
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void botonCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Estas segura que quiere cerrar la sesion?",
                "Cerrar Sesion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if( respuesta == DialogResult.Yes)
            {
                Close();
            }
        }
        private void botonUsuarios_Click(object sender, EventArgs e)
        {
            FrmUsuarios formulario = new FrmUsuarios(_serviceProvider.GetRequiredService<IUsuarioService>(),_usuario);

            formulario.ShowDialog();
            formulario.Dispose();
        }
        private void botonClientes_Click(object sender, EventArgs e)
        {
            FrmClientes formulario = _serviceProvider.GetRequiredService<FrmClientes>();

            formulario.ShowDialog();
            formulario.Dispose();
        }
        private void botonEstadios_Click(object sender, EventArgs e)
        {
            FrmEstadios formulario = _serviceProvider.GetRequiredService<FrmEstadios>();

            formulario.ShowDialog();
            formulario.Dispose();
        }
        private void botonPartidos_Click(object sender, EventArgs e)
        {
            FrmPartidos formulario = _serviceProvider.GetRequiredService<FrmPartidos>();

            formulario.ShowDialog();
            formulario.Dispose();
        }
        private void botonVenta_Click(object sender, EventArgs e)
        {
            FrmVentaBoletos formulario =_serviceProvider.GetRequiredService<FrmVentaBoletos>();

            formulario.ShowDialog();
            formulario.Dispose();
        }
        private void botonFacturacion_Click(object sender, EventArgs e)
        {
            FrmFacturación formulario =_serviceProvider.GetRequiredService<FrmFacturación>();

            formulario.ShowDialog();
            formulario.Dispose();
        }
        private void botonCancelacion_Click(object sender, EventArgs e)
        {
            FrmCancelaciones formulario =_serviceProvider.GetRequiredService<FrmCancelaciones>();

            formulario.ShowDialog();
            formulario.Dispose();
        }
        private void botonReportVent_Click(object sender, EventArgs e)
        {
            FrmReporteVentas formulario =_serviceProvider.GetRequiredService<FrmReporteVentas>();

            formulario.ShowDialog();
            formulario.Dispose();
        }
        private void botonROD_Click(object sender, EventArgs e)
        {
            FrmReporteOcupacion formulario =
                _serviceProvider.GetRequiredService<FrmReporteOcupacion>();

            formulario.ShowDialog();
            formulario.Dispose();
        }
    }
}
