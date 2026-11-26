using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms
{
    public partial class FrmPrincipal : Form
    {
        //Variables de la clase
        private readonly Usuario usuarioActual;
        private readonly IPermisoService _permisoService;
        private readonly ITipoUsuarioService _tipoUsuarioService;

        // Constructor
        public FrmPrincipal(
            Usuario objUsuario,
            IPermisoService permisoService,
            ITipoUsuarioService tipoUsuarioService)
        {
            InitializeComponent();

            usuarioActual = objUsuario;
            _permisoService = permisoService;
            _tipoUsuarioService = tipoUsuarioService;
        }

        // Evento Load del formulario
        private async void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // Mostrar el nombre del usuario
            lblNUsuario.Text = usuarioActual.Nombre;
            
            // Cargar los permisos del usuario 
            await CargarPermisosAsync();
        }

        // Método para cargar los permisos
        private async Task CargarPermisosAsync()
        {
            var permisos = await _permisoService
                .ObtenerPermisosPorTipoAsync(usuarioActual.Id_tipo);

            HashSet<string> permisosUsuario = permisos
                .Select(p => p.NombreMenu)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            btnUsuarios.Visible = permisosUsuario.Contains("btnUsuarios");
            btnClientes.Visible = permisosUsuario.Contains("btnClientes");
            btnCatalogo.Visible = permisosUsuario.Contains("btnCatalogo");
            btnEquipos.Visible = permisosUsuario.Contains("btnEquipos");
            btnPartidos.Visible = permisosUsuario.Contains("btnPartidos");
            btnVentas.Visible = permisosUsuario.Contains("btnVentas");
            btnBoletos.Visible = permisosUsuario.Contains("btnBoletos");
            btnCancelaciones.Visible = permisosUsuario.Contains("btnCancelaciones");
            btnFacturacion.Visible = permisosUsuario.Contains("btnFacturacion");
            btnReportes.Visible = permisosUsuario.Contains("btnReportes");
        }

        private void btnSalirPrincipal_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Cerrar sesión?",
                "Confirmar cierre de sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void abrirFormulario(IconMenuItem menu, Form formulario)
        {

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            // Limpiar el contenido anterior del panel
            panelContenedor.Controls.Clear();

            // Crear el formulario hijo pasando el servicio
            FrmUsuarios formulario = new FrmUsuarios(_tipoUsuarioService, usuarioActual);

            // Configurarlo para que pueda incrustarse
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            // Agregarlo al panel
            panelContenedor.Controls.Add(formulario);

            // Mostrarlo
            formulario.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();

            FrmClientes formulario = new FrmClientes();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void btnCatalogo_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();

            FrmCatalogo formulario = new FrmCatalogo();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void btnEquipos_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();

            FrmEquipos formulario = new FrmEquipos();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void btnPartidos_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();

            FrmPartidos formulario = new FrmPartidos();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();

            FrmVentas formulario = new FrmVentas();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void btnBoletos_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();

            FrmBoletos formulario = new FrmBoletos();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void btnCancelaciones_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();

            FrmCancelaciones formulario = new FrmCancelaciones();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();

            FrmFacturacion formulario = new FrmFacturacion();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();

            FrmReportes formulario = new FrmReportes();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formulario);

            formulario.Show();
        }

    }
}
