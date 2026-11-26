
using System;
using System.Data;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    public partial class eligio_reporte_ocup_y_disp : Form
    {
        // Tabla que almacena los datos del reporte actual.
        private DataTable tablaOcupacion = new DataTable();

        public eligio_reporte_ocup_y_disp()
        {
            InitializeComponent();

            this.Load -= eligio_reporte_ocup_y_disp_Load;
            this.Load += eligio_reporte_ocup_y_disp_Load;
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void eligio_reporte_ocup_y_disp_Load(
            object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarOcupacionYDisponibilidad();
        }

        // Configura el DataGridView.
        private void ConfigurarGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.MultiSelect = false;

            dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.RowHeadersVisible = false;
        }

        // Carga exclusivamente la vista general de ocupación y disponibilidad.
        private void CargarOcupacionYDisponibilidad()
        {
            try
            {
                tablaOcupacion =
                    ConexionBD.ObtenerReporteOcupacionDisponibilidad();

                dataGridView1.DataSource = tablaOcupacion;

                // Información del partido.
                CambiarEncabezado(
                    "Numero_partido", "Número de partido");

                CambiarEncabezado(
                    "Partido", "Partido");

                // Información del estadio.
                CambiarEncabezado(
                    "ID_Estadio", "ID del estadio");

                CambiarEncabezado(
                    "Nombre_Estadio", "Estadio");

                CambiarEncabezado(
                    "Ciudad", "Ciudad");

                // Información de la zona.
                CambiarEncabezado(
                    "ID_Zona", "ID de zona");

                CambiarEncabezado(
                    "Zona", "Zona");

                CambiarEncabezado(
                    "Precio_Base", "Precio base");

                // Información de los asientos.
                CambiarEncabezado(
                    "Asientos_Totales", "Asientos totales");

                CambiarEncabezado(
                    "Asientos_Vendidos", "Asientos vendidos");

                CambiarEncabezado(
                    "Total_Boletos_Vendidos_Partido",
                    "Total de boletos vendidos por partido");

                CambiarEncabezado(
                    "Asientos_Disponibles", "Asientos disponibles");

                CambiarEncabezado(
                    "Porcentaje_Ocupacion", "Porcentaje de ocupación");

                // Información individual del asiento, si la vista la contiene.
                CambiarEncabezado(
                    "Fila_Asiento", "Fila");

                CambiarEncabezado(
                    "Num_Asiento", "Número de asiento");

                CambiarEncabezado(
                    "EstadoAsiento", "Estado del asiento");

                if (tablaOcupacion.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay estadios, partidos o zonas registrados para mostrar.",
                        "Reporte de ocupación y disponibilidad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar el reporte de ocupación " +
                    "y disponibilidad.\n\n" + ex.Message,
                    "Error al cargar el reporte",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Cambia el texto del encabezado de una columna.
        private void CambiarEncabezado(
            string nombreColumna, string encabezado)
        {
            if (dataGridView1.Columns.Contains(nombreColumna))
            {
                dataGridView1.Columns[nombreColumna].HeaderText =
                    encabezado;
            }
        }

        // Ordena los partidos por cantidad total de boletos vendidos,
        // de mayor a menor.
        private void ordenar_p_partido_oc_dis_Click(
            object sender, EventArgs e)
        {
            if (tablaOcupacion.Columns.Contains(
                "Total_Boletos_Vendidos_Partido"))
            {
                tablaOcupacion.DefaultView.Sort =
                    "[Total_Boletos_Vendidos_Partido] DESC, " +
                    "[Numero_partido] ASC";
            }
            else
            {
                MessageBox.Show(
                    "La vista no contiene la columna " +
                    "Total_Boletos_Vendidos_Partido.",
                    "Columna no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        // Ordena los asientos disponibles de mayor a menor.
        private void ordenar_p_asient_oc_dis_Click(
            object sender, EventArgs e)
        {
            if (tablaOcupacion.Columns.Contains("Asientos_Disponibles"))
            {
                tablaOcupacion.DefaultView.Sort =
                    "[Asientos_Disponibles] DESC";
            }
            else
            {
                MessageBox.Show(
                    "La vista no contiene la columna Asientos_Disponibles.",
                    "Columna no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        // Muestra la información de la misma vista general.
        // No carga una segunda vista.
        private void ordenar_p_part_zona_asient_Click(
            object sender, EventArgs e)
        {
            CargarOcupacionYDisponibilidad();
        }

        // Cierra el formulario y regresa al menú anterior.
        private void fin_d_ver_reporte_oc_disp_Click(
            object sender, EventArgs e)
        {
            this.Close();
        }

        // Evento de clic en una celda.
        private void dataGridView1_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            // Actualmente no realiza ninguna acción.
        }

        // EVENTOS DEL MENÚ STRIP.

        // Ordenar por cantidad total de boletos vendidos.
        private void partidoToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            if (tablaOcupacion.Columns.Contains(
                "Total_Boletos_Vendidos_Partido"))
            {
                tablaOcupacion.DefaultView.Sort =
                    "[Total_Boletos_Vendidos_Partido] DESC, " +
                    "[Numero_partido] ASC";
            }
            else
            {
                MessageBox.Show(
                    "No se encontró la columna de total de boletos vendidos por partido.",
                    "Columna no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void zonaToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            // Este elemento funciona como encabezado del submenú Zona.
        }

        // Ordenar zonas de A a Z.
        private void aZToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            if (tablaOcupacion.Columns.Contains("Zona"))
            {
                tablaOcupacion.DefaultView.Sort =
                    "[Zona] ASC";
            }
        }

        // Ordenar zonas de Z a A.
        private void zAToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            if (tablaOcupacion.Columns.Contains("Zona"))
            {
                tablaOcupacion.DefaultView.Sort =
                    "[Zona] DESC";
            }
        }

        // Ordenar asientos disponibles de mayor a menor.
        private void asientosDisponiblesToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            if (tablaOcupacion.Columns.Contains("Asientos_Disponibles"))
            {
                tablaOcupacion.DefaultView.Sort =
                    "[Asientos_Disponibles] DESC";
            }
            else
            {
                MessageBox.Show(
                    "No se encontró la columna Asientos_Disponibles.",
                    "Columna no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
