using CARR_PA_Proyecto_052.Data;
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
    public partial class Form10 : Form
    {
        private int IDCliente;
        public Form10(int ID)
        {
            InitializeComponent();
            IDCliente = ID;
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
    }
}
