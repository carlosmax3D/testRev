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
    public partial class ConsAdmins : Form
    {
        public ConsAdmins()
        {
            InitializeComponent();
        }

        private void CargarDatosAdmins()
        {
            using (var db = new InventarioContext())
            {
                dgvAdmins.DataSource = db.Usuarios
                    .Where(U => U.Rol == "Administrativo")
                    .Select(U => new
                    {
                        U.IDUsr,
                        U.Nombre,
                        U.Correo,
                        U.NumNomina,
                        U.UsrReg,
                        U.FechaHoraReg,
                        U.FechaNac
                    })
                    .ToList();
            }
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            CargarDatosAdmins();
        }
    }
}
