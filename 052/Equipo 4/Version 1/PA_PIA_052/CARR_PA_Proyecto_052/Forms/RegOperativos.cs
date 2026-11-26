using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using CARR_PA_Proyecto_052.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public partial class RegOperativos : Form
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
        public RegOperativos(string nombreUsuario)
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
            if (!cajatxtEmail.Text.EndsWith("@mail.com"))
            {
                MessageBox.Show("El correo debe terminar con @mail.com");
                return;
            }


            if (!Regex.IsMatch(cajatxtCont.Text, @"[%$#]"))
            {
                MessageBox.Show("La contraseña debe contener por lo menos un caracter especial (%, $, #)");
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Estan los datos correctos?", "Confirmacion", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    RegistrarOperativo RegOp = new RegistrarOperativo();
                    RegOp.RegistrarOp(nomUsuario, cajatxtUsuNombre.Text, cajatxtEmail.Text, cajatxtCont.Text, cajatxtNumNomina.Text, dtpFechaNac.Value);

                    MessageBox.Show("Registracion Exitosa", "Éxito", MessageBoxButtons.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al guardar en la base de datos:\n" + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK);

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
