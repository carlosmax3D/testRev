using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class ActAdmin : Form
    {
        private string UsuarioSesion;
        public ActAdmin(string nombre)
        {
            InitializeComponent();
            UsuarioSesion = nombre;
        }

        private void CargarDatosAdmin()
        {
            using (var db = new InventarioContext())
            {
                dgvAdmin.DataSource = db.Usuarios
                    .Where(U => U.Rol == "Administrativo")
                    .Select(U => new
                    {
                        U.IDUsr,
                        U.Nombre,
                        U.Correo,
                        U.NumNomina,
                        U.UsrReg,
                        U.FechaHoraReg,
                        U.FechaNac,
                        U.IDUsr_ModificadoPor,
                        U.FechaHora_Modificacion
                    })
                    .ToList();
            }
        }

        private void ActAdmin_Load(object sender, EventArgs e)
        {
            CargarDatosAdmin();
        }

        private int IdUsuario;

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAdmin.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvAdmin.SelectedRows[0];

                IdUsuario = Convert.ToInt32(fila.Cells["IDUsr"].Value);
            }

            AAModificar ventanaActAnMod = new AAModificar(IdUsuario,UsuarioSesion);
            this.Hide();
            ventanaActAnMod.ShowDialog();
            this.Show();
        }
    }
}
