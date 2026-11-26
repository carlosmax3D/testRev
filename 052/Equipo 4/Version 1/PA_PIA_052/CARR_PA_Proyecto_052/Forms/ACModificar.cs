using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Repositorios;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class ACModificar : Form
    {
        private int IDCliente;
        private string nombreUsuarioInSesion;
        public ACModificar(int ID, string nombre)
        {
            InitializeComponent();
            IDCliente = ID;
            nombreUsuarioInSesion = nombre;
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            using (var db = new InventarioContext())
            {
                var cliente = db.Clientes.FirstOrDefault(c => c.IDCts == IDCliente);
                var telCel = db.TelefonosClientes
                    .Where(c => c.Tipo == "Celular")
                    .FirstOrDefault(c => c.IDCts == IDCliente);
                var telCasa = db.TelefonosClientes
                    .Where(c => c.Tipo == "Casa")
                    .FirstOrDefault(c => c.IDCts == IDCliente);

                if (cliente != null)
                {
                    cajatxtNom.Text = cliente.Nombre;
                    cajatxtDomi.Text = cliente.Domicilio;
                    cajatxtCorreo.Text = cliente.Correo;
                    if (cliente.FechaNac.HasValue)
                    {
                        dtpFechaNac.Value = cliente.FechaNac.Value;
                    }
                    else
                    {
                        dtpFechaNac.Value = DateTime.Now;
                    }
                    cajatxtRFC.Text = cliente.RFC;
                    if (!string.IsNullOrWhiteSpace(telCel?.Numero))
                    {
                        cajatxtTelCel.Text = telCel.Numero;
                    }

                    if (!string.IsNullOrWhiteSpace(telCasa?.Numero))
                    {
                        cajatxtTelCasa.Text = telCasa.Numero;
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
                    RegCte.ActualizarCte(IDCliente, cajatxtNom.Text, cajatxtDomi.Text, cajatxtRFC.Text, cajatxtCorreo.Text, dtpFechaNac.Value, cajatxtTelCel.Text, cajatxtTelCasa.Text, nombreUsuarioInSesion);

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
