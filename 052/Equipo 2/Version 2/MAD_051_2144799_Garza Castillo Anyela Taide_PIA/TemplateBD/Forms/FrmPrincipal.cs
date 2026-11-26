using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace TemplateCSharp.Forms
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnSalirPrincipal_Click(object sender, EventArgs e)
        {
            this.Close();
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

            // Crear el formulario hijo
            FrmUsuarios formulario = new FrmUsuarios();

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
