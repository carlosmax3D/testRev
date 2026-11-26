using CARR_PA_Proyecto_052.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class ActOperativo : Form
    {
        private string UsuarioSesion;
        public ActOperativo(string nombre)
        {
            InitializeComponent();
            UsuarioSesion = nombre;
        }

        private void CargarDatosOperativo()
        {
            using (var db = new InventarioContext())
            {
                dgvOp.DataSource = db.Usuarios
                    .Where(U => U.Rol == "Operativo")
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

        private void Form13_Load(object sender, EventArgs e)
        {
            CargarDatosOperativo();
        }

        private int IdUsuario;

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvOp.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvOp.SelectedRows[0];

                IdUsuario = Convert.ToInt32(fila.Cells["IDUsr"].Value);
            }
            AOModificar ventanaModificarOp = new AOModificar(IdUsuario, UsuarioSesion);
            this.Hide();
            ventanaModificarOp.ShowDialog();
            this.Show();
        }
    }
}
