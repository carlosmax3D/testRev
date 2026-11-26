using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public partial class Form4 : Form
    {
        private void ValidarCamposRegistro()
        {
            bool camposCompletos = !string.IsNullOrWhiteSpace(cajatxtUsuNombre.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtEmail.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtCont.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtNumNomina.Text);

            btnConfirmar.Enabled = camposCompletos;
        }

        private string nomUsuario;
        public Form4(string nombreUsuario)
        {
            InitializeComponent();
            nomUsuario = nombreUsuario;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dtpFechaNac.MaxDate = DateTime.Now;

            btnConfirmar.Enabled = false;

            cajatxtUsuNombre.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtEmail.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtCont.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtNumNomina.TextChanged += (s, ev) => ValidarCamposRegistro();
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estan los datos correctos?", "Confirmacion", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                int idUsuarioActual = 0;

                using (var db = new InventarioContext())
                {
                    var checarUsuario = db.Usuarios
                        .Where(p => p.Nombre == nomUsuario)
                        .FirstOrDefault();
                    if (checarUsuario != null)
                    {
                        idUsuarioActual = checarUsuario.IDUsr;
                    }
                    var nuevoUsuario = new Usuario
                    {
                        Rol = "Operativo",
                        Nombre = cajatxtUsuNombre.Text,
                        Correo = cajatxtEmail.Text,
                        Contrasena = LogicaUsuario.PasswordHelper.generarHash(cajatxtCont.Text),
                        NumNomina = cajatxtNumNomina.Text,
                        FechaNac = dtpFechaNac.Value,
                        FechaHoraReg = DateTime.Now,
                        UsrReg = idUsuarioActual
                    };
                    db.Usuarios.Add(nuevoUsuario);
                    db.SaveChanges();
                }
                MessageBox.Show("Registracion Exitosa", "Éxito", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
