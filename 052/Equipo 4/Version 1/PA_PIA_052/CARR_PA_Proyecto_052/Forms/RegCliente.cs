using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using CARR_PA_Proyecto_052.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public partial class RegCliente : Form
    {
        private void ValidarCamposRegistro()
        {
            bool camposCompletos = !string.IsNullOrWhiteSpace(cajatxtNom.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtDomi.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtRFC.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtCorreo.Text);

            btnConfirmar.Enabled = camposCompletos;
        }

        private string nomUsuario;
        public RegCliente(string nombreUsuario)
        {
            InitializeComponent();
            nomUsuario = nombreUsuario;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dtpFechaNac.MaxDate = DateTime.Now;

            btnConfirmar.Enabled = false;

            cajatxtNom.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtDomi.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtRFC.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtCorreo.TextChanged += (s, ev) => ValidarCamposRegistro();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!cajatxtCorreo.Text.EndsWith("@mail.com"))
            {
                MessageBox.Show("El correo debe terminar con @mail.com", "Correo Invalido");
                return;
            }

            if ((cajatxtRFC.Text.Length == 12 || cajatxtRFC.Text.Length == 13) && !cajatxtRFC.Text.Contains(" "))
            { }
            else 
            {
                MessageBox.Show("El RFC debe tener entre 12 y 13 caracteres y no debe contener espacios", "RFC Invalido");
                return;
            }
            

            string telefonoCasa = cajatxtTelCasa.Text.Trim();
            int cantidadDigitosCasa = telefonoCasa.Count(char.IsDigit);

            if (cantidadDigitosCasa > 15 || cantidadDigitosCasa == 0) 
            {
                MessageBox.Show("Los numeros de telefono deben tener maximo 15 caracteres", "Numero Telefono Casa Invalido");
                return;
            }


            string telefonoCel = cajatxtTelCel.Text.Trim();
            int cantidadDigitosCel = telefonoCel.Count(char.IsDigit);

            if (cantidadDigitosCel > 15 || cantidadDigitosCel == 0)
            {
                MessageBox.Show("Los numeros de telefono deben tener maximo 15 caracteres", "Numero Telefono Celular Invalido");
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Estan los datos correctos?", "Confirmacion", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes) 
            {
                try 
                {
                    RegistrarClientes RegCte = new RegistrarClientes();
                    RegCte.RegistrarCte(nomUsuario, cajatxtNom.Text, cajatxtDomi.Text, cajatxtRFC.Text, cajatxtCorreo.Text, dtpFechaNac.Value, cajatxtTelCasa.Text, cajatxtTelCel.Text);
                    
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
