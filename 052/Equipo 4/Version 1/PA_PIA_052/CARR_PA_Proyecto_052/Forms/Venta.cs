using CARR_PA_Proyecto_052.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class Venta : Form
    {
        private string nombreUsuarioInSesion;
        private string rolUsuario;
        public Venta(string nombre, string rol)
        {
            InitializeComponent();
            nombreUsuarioInSesion = nombre;
            rolUsuario = rol;
        }

        private void Venta_Load(object sender, EventArgs e)
        {
            txtOp.Text = "Operativo: " + nombreUsuarioInSesion;

            if (rolUsuario == "Operativo")
            {
                panel1.BackColor = Color.Crimson;
            }
        }

        private int idCliente;

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (var db = new InventarioContext())
            {
                VSeleccionarCte ventanaSelectCte = new VSeleccionarCte();
                this.Hide();
                if (ventanaSelectCte.ShowDialog() == DialogResult.OK)
                {
                    idCliente = ventanaSelectCte.idSeleccionado;
                }
                this.Show();


                var checarNombre = db.Clientes
                    .Where(c => c.IDCts == idCliente)
                    .Select(c => c.Nombre)
                    .FirstOrDefault();

                txtCte.Text = "Cliente: " + checarNombre;
            }

        }
        private int idEstadio;

        private void btnSelectEst_Click(object sender, EventArgs e)
        {
            using (var db = new InventarioContext())
            {
                VSeleccionarEstadios ventanaSelectEst = new VSeleccionarEstadios();
                this.Hide();
                if (ventanaSelectEst.ShowDialog() == DialogResult.OK)
                {
                    idEstadio = ventanaSelectEst.idSeleccionado;
                }
                this.Show();


                var checarNombre = db.Estadios
                    .Where(e => e.IDEst == idEstadio)
                    .Select(e => e.Nombre)
                    .FirstOrDefault();

                txtestadio.Text = "Estadio: " + checarNombre;
            }
        }

        private int idZona;

        private void btnSelectZn_Click(object sender, EventArgs e)
        {
            using (var db = new InventarioContext()) 
            {
                VSeleccionarZona ventanaSelectZn = new VSeleccionarZona(idEstadio);
                this.Hide();
                if (ventanaSelectZn.ShowDialog() == DialogResult.OK)
                {
                    idZona = ventanaSelectZn.idSeleccionado;
                }
                this.Show();

                var checarNombre = db.Zonas
                    .Where(z => z.IDZona == idZona)
                    .Select(z => z.NomZona)
                    .FirstOrDefault();

                txtZona.Text = "Zona: " + checarNombre;

            }
        }
    }
}
