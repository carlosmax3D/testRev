using Pantalla_BusquedaCompraCliente_PIA_PA;
using Pantalla_InicioSesion_PIA_PA.conexionBD;
using System;
using System.Data;
using System.Windows.Forms;

namespace Pantalla_InicioSesion_PIA_PA
{
    public partial class historialCompraCliente : Form
    {
        // Instancia de la clase que consulta las vistas.
        private readonly Vistas vistas = new Vistas();

        // RFC del cliente cuyo historial se consultará.
        private readonly string rfcCliente;

        public historialCompraCliente(string rfc)
        {
            InitializeComponent();

            rfcCliente = rfc;

            // Carga el historial al abrir la ventana.
            Load += historialCompraCliente_Load;
        }

        // Evento de carga del formulario.
        private void historialCompraCliente_Load(object sender, EventArgs e)
        {
            CargarHistorialCompras();
        }

        // Consulta y muestra el historial de compras del cliente.
        private void CargarHistorialCompras()
        {
            try
            {
                DataTable historial =
                    vistas.ObtenerHistorialComprasCliente(rfcCliente);

                dataGridView_historialCompClie.DataSource = historial;

                // Configuración de la tabla.
                dataGridView_historialCompClie.ReadOnly = true;
                dataGridView_historialCompClie.AllowUserToAddRows = false;
                dataGridView_historialCompClie.AllowUserToDeleteRows = false;
                dataGridView_historialCompClie.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;
                dataGridView_historialCompClie.MultiSelect = false;
                dataGridView_historialCompClie.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.DisplayedCells;

                if (historial.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "El cliente seleccionado no tiene compras registradas.",
                        "Sin historial de compras",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al consultar el historial de compras.\n\n"
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dataGridView_historialCompClie_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        // Ordena las compras de la más reciente a la más antigua.
        private void atrasToolStripMenuItem_Click(
            object sender, EventArgs e)
        {

            this.Close();
        }

        // Ordena las compras por fecha del partido.
        private void fechaDeCompraToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            if (dataGridView_historialCompClie.DataSource is DataTable tabla)
            {
                tabla.DefaultView.Sort = "Fecha_Compra DESC";
            }
        }

        private void fechaPartidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_historialCompClie.DataSource is DataTable tabla)
            {
                tabla.DefaultView.Sort = "Fecha_Partido ASC";
            }
        }
    }
}