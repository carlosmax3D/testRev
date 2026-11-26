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
    public partial class ConsOperativos : Form
    {
        public ConsOperativos()
        {
            InitializeComponent();
        }

        private void CargarDatosOperativo()
        {
            using (var db = new InventarioContext())
            {
                dgvOperativos.DataSource = db.Usuarios
                    .Where(U => U.Rol == "Operativo")
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

        private void Form11_Load(object sender, EventArgs e)
        {
            CargarDatosOperativo();
        }
    }
}
