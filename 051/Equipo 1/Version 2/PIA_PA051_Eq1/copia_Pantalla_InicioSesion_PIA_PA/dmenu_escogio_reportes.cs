
using System;
using System.Windows.Forms;

namespace Menu_p_Operativo
{
    public partial class eligio_opcion_reportes : Form
    {
        public eligio_opcion_reportes()
        {
            InitializeComponent();
        }

        private void atras_a_menu_oper_Click(object sender, EventArgs e)
        {
            // Cierra el submenú de reportes.
            // El menú operativo original volverá a mostrarse
            // mediante el evento FormClosed.
            this.Close();
        }

        private void opcion_reporte_ventas_Click(object sender, EventArgs e)
        {
            // Crea la ventana del reporte de ventas.
            using (eligio_reporte_ventas pantalla_reporte_ventas =
                new eligio_reporte_ventas())
            {
                // Oculta el submenú de reportes.
                this.Hide();

                // Abre el reporte de ventas.
                pantalla_reporte_ventas.ShowDialog(this);

                // Al cerrar el reporte, vuelve a mostrar
                // el submenú de reportes original.
                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                }
            }
        }

        private void opcion_rep_ocp_y_disp_Click(object sender, EventArgs e)
        {
            // Crea la ventana del reporte de ocupación y disponibilidad.
            using (eligio_reporte_ocup_y_disp pantalla_reporte_oc_disp =
                new eligio_reporte_ocup_y_disp())
            {
                // Oculta el submenú de reportes.
                this.Hide();

                // Abre el reporte de ocupación y disponibilidad.
                pantalla_reporte_oc_disp.ShowDialog(this);

                // Al cerrar el reporte, vuelve a mostrar
                // el submenú de reportes original.
                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                }
            }
        }

        private void eligio_opcion_reportes_Load(object sender, EventArgs e)
        {
        }
    }
}