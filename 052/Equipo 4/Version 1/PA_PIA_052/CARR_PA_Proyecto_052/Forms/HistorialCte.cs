using CARR_PA_Proyecto_052.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public partial class HistorialCte : Form
    {
        private HistorialService historialService;
        public HistorialCte()
        {
            InitializeComponent();
            historialService = new HistorialService();
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            string textoId = cajatxt_ID.Text.Trim();
            string textoNombre = cajatxt_Nombre.Text.Trim();

            if (!string.IsNullOrEmpty(textoId))
            {
                if (int.TryParse(textoId, out int idCliente))
                {
                    var resultados = historialService.ObtenerHistorialPorId(idCliente);

                    if (resultados.Count > 0)
                    {
                        dgv_Historial.DataSource = resultados;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros de ventas para el ID ingresado.", "Sin Resultados", MessageBoxButtons.OK);
                        dgv_Historial.DataSource = null;
                    }
                }
                else
                {
                    MessageBox.Show("El ID de cliente debe ser un número entero válido.", "Atención", MessageBoxButtons.OK);
                }
            }
            else if (!string.IsNullOrEmpty(textoNombre))
            {
                using (var db = new Data.InventarioContext())
                {
                    var clientesEncontrados = db.Clientes
                        .FromSqlRaw("EXEC sp_HistorialCliente @Busqueda = {0}, @ClienteID = NULL", textoNombre)
                        .ToList();

                    dgv_Historial.DataSource = clientesEncontrados;
                }
            }
            else
            {
                MessageBox.Show("Por favor ingresa al menos un ID o un Nombre para consultar.", "Atención", MessageBoxButtons.OK);
            }
        }
    }
}
