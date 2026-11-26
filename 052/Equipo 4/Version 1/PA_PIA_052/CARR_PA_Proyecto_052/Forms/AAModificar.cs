using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class AAModificar : Form
    {
        private int IDUsuario;
        private string nombreUsuarioInSesion;
        public AAModificar(int ID, string nombre)
        {
            InitializeComponent();
            IDUsuario = ID;
            nombreUsuarioInSesion = nombre;
        }

        private void AAModificar_Load(object sender, EventArgs e)
        {
            using (var db = new InventarioContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.IDUsr == IDUsuario);

                if (usuario != null)
                {
                    cajatxtUsuNombre.Text = usuario.Nombre;
                    cajatxtEmail.Text = usuario.Correo;
                    cajatxtNumNomina.Text = usuario.NumNomina;
                    if (usuario.FechaNac.HasValue)
                    {
                        dtpFechaNac.Value = usuario.FechaNac.Value;
                    }
                    else
                    {
                        dtpFechaNac.Value = DateTime.Now;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!cajatxtEmail.Text.EndsWith("@mail.com"))
            {
                MessageBox.Show("El correo debe terminar con @mail.com");
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Estan los datos correctos?", "Confirmacion", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    RegistrarAdmin RegAd = new RegistrarAdmin();
                    RegAd.ActualizarAn(IDUsuario, cajatxtUsuNombre.Text, cajatxtEmail.Text, cajatxtCont.Text, cajatxtNumNomina.Text, dtpFechaNac.Value,nombreUsuarioInSesion);

                    MessageBox.Show("Actualizacion Exitosa", "Éxito", MessageBoxButtons.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al guardar en la base de datos:\n" + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK);

                }
            }
        }
    }
}
