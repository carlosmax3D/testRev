using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Menú principal: los botones que se ven dependen del rol
    public class MenuForm : Form
    {
        public bool CerrarSesion = false;
        FlowLayoutPanel panel;

        public MenuForm()
        {
            Ui.PrepararForm(this, "Menú principal", "Sistema de venta de boletos", 760, 470);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label usuario = new Label();
            usuario.AutoSize = true;
            usuario.Text = "Usuario: " + Sesion.Nombre + "   |   Rol: " + Sesion.Rol;
            usuario.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            usuario.ForeColor = Ui.Navy;
            usuario.Location = new Point(24, 76);
            Controls.Add(usuario);

            panel = new FlowLayoutPanel();
            panel.SetBounds(16, 108, 728, 290);
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.WrapContents = true;
            Controls.Add(panel);

            // (texto, solo administrador, pantalla que abre)
            Agregar("Venta de boletos", false, delegate { new VentaForm().Show(); });
            Agregar("Clientes", false, delegate { new ClientesForm().Show(); });
            Agregar("Historial del cliente", true, delegate { new HistorialClienteForm().Show(); });
            Agregar("Cancelación de ventas", true, delegate { new CancelacionForm().Show(); });
            Agregar("Facturación", true, delegate { new FacturaForm().Show(); });
            Agregar("Reporte de ventas", true, delegate { new ReporteVentasForm().Show(); });
            Agregar("Reporte de ocupación", true, delegate { new ReporteOcupacionForm().Show(); });
            Agregar("Estadios", true, delegate { new EstadiosForm().Show(); });
            Agregar("Zonas", true, delegate { new ZonasForm().Show(); });
            Agregar("Partidos", true, delegate { new PartidosForm().Show(); });
            Agregar("Usuarios", true, delegate { new UsuarioForm().Show(); });

            Ui.Boton(this, "Cerrar sesión", 560, 412, 184, 40, Ui.Gris, delegate { CerrarSesion = true; Close(); });
        }

        void Agregar(string texto, bool soloAdmin, Action accion)
        {
            if (soloAdmin && !Sesion.EsAdmin) return;
            Button b = Ui.Boton(panel, texto, 0, 0, 350, 44, soloAdmin ? Ui.Azul : Ui.Verde, null);
            b.Margin = new Padding(8);
            b.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            b.Click += delegate { accion(); };
        }
    }
}
