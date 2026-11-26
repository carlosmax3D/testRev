namespace PIA_PA
{
    using PIA_PA.Menus;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Windows.Forms;
    public partial class Form1 : Form
    {
        private IServiceProvider? _serviceProvider;

        // Constructor para el diseñador de Windows Forms.
        public Form1()
        {
            InitializeComponent();
            ToolStripLabel lblUsuario = new ToolStripLabel();
            lblUsuario.Text = SesionActual.Nombre + " (" + SesionActual.Tipo + ")";

            cuentaToolStripMenuItem.DropDownItems.Insert(0, lblUsuario);
        }

        public Form1(IServiceProvider serviceProvider) : this()
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            if (!SesionActual.Activa) throw new InvalidOperationException("Inicia sesión primero.");
            menuPrincipal.Items.Clear();
            string[] opciones = SesionActual.EsAdministrador
                ? new[] {"Inicio","Partidos","Estadios","Equipos","Boletos","Ventas","Cancelaciones","Clientes","Administradores","Operativos","Reportes"}
                : new[] {"Inicio","Clientes","Ventas","Partidos"};
            menuPrincipal.Items.AddRange(opciones);
            menuPrincipal.SelectedIndex = 0;
        }

        private T ObtenerModulo<T>() where T : notnull
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("Form1 debe abrirse mediante la inyección de dependencias.");

            if (!SesionActual.EsAdministrador)
            {
                Type t = typeof(T);
                bool permitido = t == typeof(ConsultarPartidos) || t == typeof(AgregarPartido) ||
                    t == typeof(ModificarPartido) || t == typeof(CancelarPartido) ||
                    t == typeof(ConsultarCliente) || t == typeof(AgregarCliente) ||
                    t == typeof(ModificarCliente) || t == typeof(DesactivarCliente) ||
                    t == typeof(ConsultarVenta) || t == typeof(AgregarVenta);
                if (!permitido) throw new UnauthorizedAccessException("Solo los administradores pueden acceder a este módulo.");
            }
            return _serviceProvider.GetRequiredService<T>();
        }

        private void MostrarModulo(UserControl modulo)
        {
            if (!SesionActual.Activa) throw new UnauthorizedAccessException("Inicia sesión primero.");
            if (!SesionActual.EsAdministrador && modulo is not (
                ConsultarPartidos or AgregarPartido or ModificarPartido or CancelarPartido or
                ConsultarCliente or AgregarCliente or ModificarCliente or DesactivarCliente or
                ConsultarVenta or AgregarVenta))
                throw new UnauthorizedAccessException("Acceso exclusivo de administradores.");
            panelContenido.Controls.Clear();

            modulo.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(modulo);
            modulo.Visible = true;
            modulo.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void OcultarMenusPrincipales()
        {
            gBCAMC.Visible = false;
            gBCAME.Visible = false;
            gBBoletos.Visible = false;
            gBVentas.Visible = false;
            gBCancelaciones.Visible = false;
            gBoxCAMD.Visible = false;
            gBReportes.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (menuPrincipal.SelectedItem == null)
            {
                return;
            }

            string selectedItem = menuPrincipal.SelectedItem?.ToString() ?? string.Empty;
            OcultarMenusPrincipales();
            switch (selectedItem)
            {
                case "Partidos":
                    gBCAMC.Visible = true;
                    break;

                case "Estadios":
                    gBCAME.Visible = true;
                    break;

                case "Equipos":
                    gBCAME.Visible = true;
                    break;

                case "Boletos":
                    gBBoletos.Visible = true;
                    break;

                case "Ventas":
                    gBVentas.Visible = true;
                    break;

                case "Cancelaciones":
                    gBCancelaciones.Visible = true;
                    break;

                case "Clientes":
                    gBoxCAMD.Visible = true;
                    break;

                case "Administradores":
                    gBoxCAMD.Visible = true;
                    break;

                case "Operativos":
                    gBoxCAMD.Visible = true;
                    break;

                case "Reportes":
                    gBReportes.Visible = true;
                    break;
            }

        }

        private void gBoxCatalogo_Enter(object sender, EventArgs e)
        {

        }
        private void gBCAMC_Enter(object sender, EventArgs e)
        {

        }

        // Menu Lateral Partidos
        private void cerrarSesiónToolStripMenuItem_Click_1(
            object sender, EventArgs e)
        {
            SesionActual.Cerrar();
            Form2 f = _serviceProvider.GetRequiredService<Form2>();
            f.Show();
            this.Hide();
        }

        private void rBConsultarCAMC_CheckedChanged(object sender, EventArgs e)
        {
            if (rBConsultarCAMC.Checked == true)
            {
                MostrarModulo(ObtenerModulo<ConsultarPartidos>());
            }
        }

        private void rBAgregarCAMC_CheckedChanged(object sender, EventArgs e)
        {
            if (rBAgregarCAMC.Checked == true)
            {
                MostrarModulo(ObtenerModulo<AgregarPartido>());
            }
        }

        private void rBModificarCAMC_CheckedChanged(object sender, EventArgs e)
        {
            if (rBModificarCAMC.Checked == true)
            {
                MostrarModulo(ObtenerModulo<ModificarPartido>());
            }
        }

        private void rBCancelarCAMC_CheckedChanged(object sender, EventArgs e)
        {
            if (rBCancelarCAMC.Checked == true)
            {
                MostrarModulo(ObtenerModulo<CancelarPartido>());
            }
        }

        // Menu Lateral Estadios - Equipos
        private void rBConsultarCAME_CheckedChanged(object sender, EventArgs e)
        {
            string selectedItem = menuPrincipal.SelectedItem?.ToString() ?? string.Empty;
            if (rBConsultarCAME.Checked == true)
            {
                if (selectedItem == "Estadios")
                {
                    MostrarModulo(ObtenerModulo<ConsultarEstadio>());
                }
                else if (selectedItem == "Equipos")
                {
                    MostrarModulo(ObtenerModulo<ConsultarEquipo>());
                }
            }
        }

        private void rBAgregarCAME_CheckedChanged(object sender, EventArgs e)
        {
            string selectedItem = menuPrincipal.SelectedItem?.ToString() ?? string.Empty;
            if (rBAgregarCAME.Checked == true)
            {
                if (selectedItem == "Estadios")
                {
                    MostrarModulo(ObtenerModulo<AgregarEstadio>());
                }
                else if (selectedItem == "Equipos")
                {
                    MostrarModulo(ObtenerModulo<AgregarEquipo>());
                }
            }
        }

        private void rBModificarCAME_CheckedChanged(object sender, EventArgs e)
        {
            string selectedItem = menuPrincipal.SelectedItem?.ToString() ?? string.Empty;
            if (rBModificarCAME.Checked == true)
            {
                if (selectedItem == "Estadios")
                {
                    MostrarModulo(ObtenerModulo<ModificarEstadio>());
                }
                else if (selectedItem == "Equipos")
                {
                    MostrarModulo(ObtenerModulo<ModificarEquipo>());
                }
            }
        }

        private void rBEliminarCAME_CheckedChanged(object sender, EventArgs e)
        {
            string selectedItem = menuPrincipal.SelectedItem?.ToString() ?? string.Empty;
            if (rBEliminarCAME.Checked == true)
            {
                if (selectedItem == "Estadios")
                {
                    MostrarModulo(ObtenerModulo<EliminarEstadio>());
                }
                else if (selectedItem == "Equipos")
                {
                    MostrarModulo(ObtenerModulo<EliminarEquipo>());
                }
            }
        }

        // Menu Lateral Ventas
        private void rBConsultarCAMD_CheckedChanged(object sender, EventArgs e)
        {
            string selectedItem = menuPrincipal.SelectedItem?.ToString() ?? string.Empty;
            if (rBConsultarCAMD.Checked == true)
            {
                if (selectedItem == "Clientes")
                {
                    MostrarModulo(ObtenerModulo<ConsultarCliente>());
                }
                else if (selectedItem == "Administradores")
                {
                    MostrarModulo(ObtenerModulo<ConsultarBoleto>());
                }
                else if (selectedItem == "Operativos")
                {
                    MostrarModulo(ObtenerModulo<ConsultarEmpleado>());
                }
            }
        }

        private void rBAgregarCAMD_CheckedChanged(object sender, EventArgs e)
        {
            string selectedItem = menuPrincipal.SelectedItem?.ToString() ?? string.Empty;
            if (rBAgregarCAMD.Checked == true)
            {
                if (selectedItem == "Clientes")
                {
                    MostrarModulo(ObtenerModulo<AgregarCliente>());
                }
                else if (selectedItem == "Administradores")
                {
                    MostrarModulo(ObtenerModulo<AgregarUsuario>());
                }
                else if (selectedItem == "Operativos")
                {
                    MostrarModulo(ObtenerModulo<AgregarEmpleado>());
                }
            }
        }

        private void rBModificarCAMD_CheckedChanged(object sender, EventArgs e)
        {
            string selectedItem = menuPrincipal.SelectedItem?.ToString() ?? string.Empty;
            if (rBModificarCAMD.Checked == true)
            {
                if (selectedItem == "Clientes")
                {
                    MostrarModulo(ObtenerModulo<ModificarCliente>());
                }
                else if (selectedItem == "Administradores")
                {
                    MostrarModulo(ObtenerModulo<ModificarUsuario>());
                }
                else if (selectedItem == "Operativos")
                {
                    MostrarModulo(ObtenerModulo<ModificarEmpleado>());
                }
            }
        }

        private void rBDesactivarCAMD_CheckedChanged(object sender, EventArgs e)
        {
            string selectedItem = menuPrincipal.SelectedItem?.ToString() ?? string.Empty;
            if (rBDesactivarCAMD.Checked == true)
            {
                if (selectedItem == "Clientes")
                {
                    MostrarModulo(ObtenerModulo<DesactivarCliente>());
                }
                else if (selectedItem == "Administradores")
                {
                    MostrarModulo(ObtenerModulo<DesactivarUsuario>());
                }
                else if (selectedItem == "Operativos")
                {
                    MostrarModulo(ObtenerModulo<DesactivarEmpleado>());
                }
            }
        }

        // Menu Lateral Cancelaciones

        private void rBCancelacionCancelaciones_CheckedChanged(object sender, EventArgs e)
        {
            if (rBCancelacionCancelaciones.Checked == true)
            {
                MostrarModulo(ObtenerModulo<Nueva>());
            }
        }

        private void rBHistorialCancelaciones_CheckedChanged(object sender, EventArgs e)
        {
            if (rBHistorialCancelaciones.Checked == true)
            {
                MostrarModulo(ObtenerModulo<Historial>());
            }
        }

        // Menu lateral Reportes

        private void rBVentasReportes_CheckedChanged(object sender, EventArgs e)
        {
            if (rBVentasReportes.Checked == true)
            {
                MostrarModulo(ActivatorUtilities.CreateInstance<Ventas>(_serviceProvider!));
            }
        }

        private void rBPartidosReportes_CheckedChanged(object sender, EventArgs e)
        {
            if (rBPartidosReportes.Checked == true)
            {
                MostrarModulo(ActivatorUtilities.CreateInstance<Partidos>(_serviceProvider!));
            }
        }

        private void rBCancelacionesReportes_CheckedChanged(object sender, EventArgs e)
        {
            if (rBCancelacionesReportes.Checked == true)
            {
                MostrarModulo(ActivatorUtilities.CreateInstance<Cancelaciones>(_serviceProvider!));
            }
        }

        private void rBIngresosReportes_CheckedChanged(object sender, EventArgs e)
        {
            if (rBClienteReportes.Checked == true)
            {
                MostrarModulo(ActivatorUtilities.CreateInstance<Cliente>(_serviceProvider!));
            }
        }

        private void rbOcupacionReportes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOcupacionReportes.Checked)
                MostrarModulo(ActivatorUtilities.CreateInstance<Ocupacion>(_serviceProvider!));
        }

        private void rBConsultarBoletos_CheckedChanged(object sender, EventArgs e)
        {
            MostrarModulo(ObtenerModulo<ConsultarBoleto>());
        }

        private void rBPreciosBoletos_CheckedChanged(object sender, EventArgs e)
        {
            MostrarModulo(ObtenerModulo<AgregarPrecioBoleto>());
        }

        private void rBConsultarVentas_CheckedChanged(object sender, EventArgs e)
        {
            MostrarModulo(ObtenerModulo<ConsultarVenta>());
        }

        private void rBVentaVentas_CheckedChanged(object sender, EventArgs e)
        {
            MostrarModulo(ObtenerModulo<AgregarVenta>());
        }
    }
}
