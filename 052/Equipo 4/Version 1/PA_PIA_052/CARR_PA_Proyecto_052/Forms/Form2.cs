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
    public partial class Form2 : Form
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
        public Form2(string nombreUsuario)
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
                    var nuevoCliente = new Cliente
                    {
                        Nombre = cajatxtNom.Text,
                        Domicilio = cajatxtDomi.Text,
                        RFC = cajatxtRFC.Text,
                        Correo = cajatxtCorreo.Text,
                        FechaNac = dtpFechaNac.Value,
                        IDUsr = idUsuarioActual
                    };
                    db.Clientes.Add(nuevoCliente);
                    db.SaveChanges();

                    if (!string.IsNullOrWhiteSpace(cajatxtTelCasa.Text)) 
                    {
                        var telCasa = new TelefonosClientes
                        {
                            IDCts = nuevoCliente.IDCts,
                            Tipo = "Casa",
                            Numero = cajatxtTelCasa.Text
                        };
                        db.TelefonosClientes.Add(telCasa);
                        db.SaveChanges();
                    }
                    if (!string.IsNullOrWhiteSpace(cajatxtTelCel.Text))
                    {
                        var telCelular = new TelefonosClientes
                        {
                            IDCts = nuevoCliente.IDCts,
                            Tipo = "Celular",
                            Numero = cajatxtTelCel.Text
                        };
                        db.TelefonosClientes.Add(telCelular);
                        db.SaveChanges();
                    }
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
