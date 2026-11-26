
using System;
using System.Data;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    public partial class eligio_reporte_ventas : Form
    {
        private DataTable tablaVentas;

        public eligio_reporte_ventas()
        {
            InitializeComponent();

            ConfigurarGrid();
            ConfigurarEventosOrdenamiento();

            // Evitar que el evento Load se registre dos veces.
            this.Load -= eligio_reporte_ventas_Load;
            this.Load += eligio_reporte_ventas_Load;
        }

        // =========================================================
        // CONFIGURAR GRID
        // =========================================================

        private void ConfigurarGrid()
        {
            vista_reporteVentas.AutoGenerateColumns = true;
            vista_reporteVentas.ReadOnly = true;
            vista_reporteVentas.AllowUserToAddRows = false;
            vista_reporteVentas.AllowUserToDeleteRows = false;
            vista_reporteVentas.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            vista_reporteVentas.MultiSelect = false;
            vista_reporteVentas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
        }

        // =========================================================
        // CARGAR REPORTE AL ABRIR EL FORMULARIO
        // =========================================================

        private void eligio_reporte_ventas_Load(object sender, EventArgs e)
        {
            CargarVentas();
        }

        // =========================================================
        // CONSULTAR REPORTE DE VENTAS
        // =========================================================

        private void CargarVentas()
        {
            try
            {
                tablaVentas = ConexionBD.ObtenerReporteVentas();

                vista_reporteVentas.DataSource = tablaVentas.DefaultView;

                ConfigurarColumnasReporte();

                // =========================================================
                // CALCULAR INGRESO TOTAL DE VENTAS
                // =========================================================

                decimal ingresoTotal = 0;

                if (tablaVentas.Columns.Contains("Total"))
                {
                    foreach (DataRow fila in tablaVentas.Rows)
                    {
                        if (fila["Total"] != DBNull.Value)
                        {
                            ingresoTotal += Convert.ToDecimal(fila["Total"]);
                        }
                    }
                }

                // Mostrar el ingreso total en su label correspondiente.
                label1_ingresoTotVenta.Text =
                    "Ingreso total de ventas: " +
                    ingresoTotal.ToString("C2");

                // =========================================================
                // CALCULAR CANTIDAD TOTAL DE BOLETOS VENDIDOS
                // =========================================================

                int cantidadTotalBoletos = 0;

                if (tablaVentas.Columns.Contains("Cantidad_Boletos_Vendidos"))
                {
                    foreach (DataRow fila in tablaVentas.Rows)
                    {
                        if (fila["Cantidad_Boletos_Vendidos"] != DBNull.Value)
                        {
                            cantidadTotalBoletos +=
                                Convert.ToInt32(fila["Cantidad_Boletos_Vendidos"]);
                        }
                    }
                }

                // Mostrar la cantidad total de boletos vendidos en su label correspondiente.
                label2_cantTotBV.Text =
                    "Cantidad total de boletos vendidos: " +
                    cantidadTotalBoletos.ToString();

                // =========================================================
                // ORDENAR VENTAS POR FECHA
                // =========================================================

                if (tablaVentas.Rows.Count > 0)
                {
                    AplicarOrden(
                        "Fecha_Hora_Venta DESC, Folio DESC");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el reporte de ventas:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CONFIGURAR COLUMNAS VISIBLES DEL REPORTE
        // =========================================================

        private void ConfigurarColumnasReporte()
        {
            if (vista_reporteVentas.Columns.Contains("Folio"))
                vista_reporteVentas.Columns["Folio"].HeaderText =
                    "Folio de venta";

            if (vista_reporteVentas.Columns.Contains("Nomina_Ope"))
                vista_reporteVentas.Columns["Nomina_Ope"].HeaderText =
                    "Nómina del operativo";

            if (vista_reporteVentas.Columns.Contains("Fecha_Hora_Venta"))
            {
                vista_reporteVentas.Columns["Fecha_Hora_Venta"].HeaderText =
                    "Fecha y hora de venta";

                vista_reporteVentas.Columns["Fecha_Hora_Venta"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }

            if (vista_reporteVentas.Columns.Contains("Estadio"))
                vista_reporteVentas.Columns["Estadio"].HeaderText =
                    "Estadio";

            if (vista_reporteVentas.Columns.Contains("Partido"))
                vista_reporteVentas.Columns["Partido"].HeaderText =
                    "Partido";

            if (vista_reporteVentas.Columns.Contains("Fecha_Partido"))
            {
                vista_reporteVentas.Columns["Fecha_Partido"].HeaderText =
                    "Fecha del partido";

                vista_reporteVentas.Columns["Fecha_Partido"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (vista_reporteVentas.Columns.Contains("Zona"))
                vista_reporteVentas.Columns["Zona"].HeaderText =
                    "Zona";

            if (vista_reporteVentas.Columns.Contains(
                "Cantidad_Boletos_Vendidos"))
            {
                vista_reporteVentas.Columns[
                    "Cantidad_Boletos_Vendidos"].HeaderText =
                    "Cantidad de boletos vendidos";
            }

            if (vista_reporteVentas.Columns.Contains("Subtotal"))
                vista_reporteVentas.Columns["Subtotal"].HeaderText =
                    "Subtotal";

            if (vista_reporteVentas.Columns.Contains("Total"))
                vista_reporteVentas.Columns["Total"].HeaderText =
                    "Total";

            if (vista_reporteVentas.Columns.Contains("Metodo_Pago"))
                vista_reporteVentas.Columns["Metodo_Pago"].HeaderText =
                    "Método de pago";

            // Ocultar columnas auxiliares utilizadas para ordenar.
            if (vista_reporteVentas.Columns.Contains("Equipo_Local"))
                vista_reporteVentas.Columns["Equipo_Local"].Visible = false;

            if (vista_reporteVentas.Columns.Contains("Equipo_Visitante"))
                vista_reporteVentas.Columns["Equipo_Visitante"].Visible = false;
        }

        // =========================================================
        // CONFIGURAR EVENTOS DE ORDENAMIENTO
        // =========================================================

        private void ConfigurarEventosOrdenamiento()
        {
            // Fecha de venta.
            ordenar_p_fecha_antigua.CheckedChanged -=
                organiza_p_fecha_venta_Click;

            ordenar_p_fecha_antigua.CheckedChanged +=
                organiza_p_fecha_venta_Click;

            ordenar_p_fecha_recientes.CheckedChanged -=
                organiza_p_fecha_venta_Click;

            ordenar_p_fecha_recientes.CheckedChanged +=
                organiza_p_fecha_venta_Click;

            // Estadio.
            ordenar_Z_A_estadio.CheckedChanged -=
                ordenar_Z_A_estadio_CheckedChanged;

            ordenar_Z_A_estadio.CheckedChanged +=
                ordenar_Z_A_estadio_CheckedChanged;

            ordenar_A_Z_estadio.CheckedChanged -=
                ordenar_Z_A_estadio_CheckedChanged;

            ordenar_A_Z_estadio.CheckedChanged +=
                ordenar_Z_A_estadio_CheckedChanged;

            // Equipo visitante.
            ordenar_eq_visitante_partido.CheckedChanged -=
                ordenar_eq_visitante_partido_CheckedChanged;

            ordenar_eq_visitante_partido.CheckedChanged +=
                ordenar_eq_visitante_partido_CheckedChanged;

            // Equipo local.
            ordenar_eq_local_partido.CheckedChanged -=
                ordenar_eq_local_partido_CheckedChanged;

            ordenar_eq_local_partido.CheckedChanged +=
                ordenar_eq_local_partido_CheckedChanged;
        }

        // =========================================================
        // ORDENAR POR FECHA DE VENTA
        // =========================================================

        private void organiza_p_fecha_venta_Click(
            object sender, EventArgs e)
        {
            if (ordenar_p_fecha_antigua.Checked)
            {
                AplicarOrden(
                    "Fecha_Hora_Venta ASC, Folio ASC");
            }
            else if (ordenar_p_fecha_recientes.Checked)
            {
                AplicarOrden(
                    "Fecha_Hora_Venta DESC, Folio DESC");
            }
        }

        // =========================================================
        // ORDENAR POR ESTADIO
        // =========================================================

        private void ordenar_Z_A_estadio_CheckedChanged(
            object sender, EventArgs e)
        {
            if (ordenar_Z_A_estadio.Checked)
            {
                AplicarOrden("Estadio DESC");
            }
            else if (ordenar_A_Z_estadio.Checked)
            {
                AplicarOrden("Estadio ASC");
            }
        }

        // =========================================================
        // ORDENAR POR EQUIPO VISITANTE
        // =========================================================

        private void ordenar_eq_visitante_partido_CheckedChanged(
            object sender, EventArgs e)
        {
            if (ordenar_eq_visitante_partido.Checked)
            {
                AplicarOrden("Equipo_Visitante ASC");
            }
        }

        // =========================================================
        // ORDENAR POR EQUIPO LOCAL
        // =========================================================

        private void ordenar_eq_local_partido_CheckedChanged(
            object sender, EventArgs e)
        {
            if (ordenar_eq_local_partido.Checked)
            {
                AplicarOrden("Equipo_Local ASC");
            }
        }

        // =========================================================
        // APLICAR ORDENAMIENTO
        // =========================================================

        private void AplicarOrden(string orden)
        {
            if (tablaVentas == null || tablaVentas.Rows.Count == 0)
                return;

            try
            {
                tablaVentas.DefaultView.Sort = orden;

                vista_reporteVentas.DataSource =
                    tablaVentas.DefaultView;

                ConfigurarColumnasReporte();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo aplicar el ordenamiento.\n\n" +
                    ex.Message,
                    "Error de ordenamiento",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // EVENTO DEL GRID
        // =========================================================

        private void vista_reporteVentas_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            // Se conserva el evento del formulario.
        }

        // =========================================================
        // REGRESAR AL MENÚ
        // =========================================================

        private void ok_regresa_menu_reporte_Click(
            object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_ingresoTotVenta_Click(object sender, EventArgs e)
        {

        }

        private void label2_cantTotBV_Click(object sender, EventArgs e)
        {

        }
    }
}
