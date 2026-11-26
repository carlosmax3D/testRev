using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Forms.Cancelaciones;
using TemplateCSharp.Forms.Clientes;
using TemplateCSharp.Forms.Estadios;
using TemplateCSharp.Forms.Partidos;
using TemplateCSharp.Forms.Usuarios;
using TemplateCSharp.Forms.VentaBoletos;
using TemplateCSharp.Forms.Reportes;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms.Menu
{
    public partial class FrmMenu : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public FrmMenu(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }


        private void accesUsuarios_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmUsuarios>();
            form.ShowDialog();
        }

        private void accesClient_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmClientes>();
            form.ShowDialog();
        }

        private void accesEstadio_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmEstadios>();
            form.ShowDialog();
        }

        private void accespartido_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmPartidos>();
            form.ShowDialog();
        }


        private void FrmMenu_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text = $"Bienvenido de regreso, {Sesion.NombreUsuario}";

            if (!Sesion.EsAdministrador)
            {
                //El operativo no ve catálogos ni reportes administrativos
                accesUsuarios.Visible = false;
                accesEstadio.Visible = false;
                acceszona.Visible = false;
                accesAsientos.Visible = false;
                accespartido.Visible = false;
                accesEquipos.Visible = false;
                accescancel.Visible = false;
                reportocupacion.Visible = false;
                reporthistorial.Visible = false;
                reportventa.Visible = false;
            }
        }

        private void lblBienvenida_Click(object sender, EventArgs e)
        {

        }

        private void acceszona_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmZonas>();
            form.ShowDialog();
        }

        private void accesAsientos_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmAsientos>();
            form.ShowDialog();
        }

        private void accesEquipos_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmEquipos>();
            form.ShowDialog();
        }

        private void reportventa_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmReporteVentas>();
            form.ShowDialog();
        }

        private void accesventa_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmBuscarCliente>();
            form.ShowDialog();
        }

        private void reportocupacion_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmReporteOcupacion>();
            form.ShowDialog();
        }

        private void reporthistorial_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmHistorialCliente>();
            form.ShowDialog();
        }

        private void accescancel_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<FrmCancelacion>();
            form.ShowDialog();
        }

        private bool _regresandoAlLogin = false;


        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RegresarAlLogin();

        }




        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_regresandoAlLogin) return;

            var result = MessageBox.Show("¿Seguro que quieres regresar al login?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Cancela el cierre y se queda en el menú
                return;
            }

            e.Cancel = true; // Cancela el cierre inmediato para poder abrir el login
            RegresarAlLogin();
        }

        private void RegresarAlLogin()
        {
            _regresandoAlLogin = true;

            //Limpiar sesión
            Sesion.IdUsuario = 0;
            Sesion.NombreUsuario = string.Empty;
            Sesion.TipoUsuario = string.Empty;

            //Abrir login SIN using (para que no se cierre solo)
            var login = _serviceProvider.GetRequiredService<FrmLogin>();
            login.Show();

            //Cerrar el menú actual
            this.Close();
        }
    }
}
