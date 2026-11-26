using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Historial de compras de un cliente (se busca por apellidos)
    public class HistorialClienteForm : Form
    {
        readonly ClienteRepositorio clientes = new ClienteRepositorio();
        TextBox txtApellidos;
        DataGridView dgvClientes, dgvHistorial;
        Label lblDatos;
        RadioButton rbCompra, rbPartido;
        int idCliente = 0;

        public HistorialClienteForm()
        {
            Ui.PrepararForm(this, "Historial del cliente", "Historial del cliente", 1100, 680);

            GroupBox g = Ui.Grupo(this, "1. Buscar cliente por apellidos", 12, 70, 1076, 170);
            Ui.Etiqueta(g, "Apellidos", 12, 22);
            txtApellidos = Ui.Texto(g, 12, 42, 300, 60);
            txtApellidos.KeyDown += delegate (object s, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Buscar(); }
            };
            Ui.Boton(g, "Buscar", 322, 40, 100, 27, Ui.Azul, delegate { Buscar(); });
            dgvClientes = Ui.Grid(g, 12, 76, 1050, 86);
            dgvClientes.CellClick += dgvClientes_CellClick;

            lblDatos = Ui.Etiqueta(this, "Elige un cliente de la lista para ver su historial.", 14, 250);
            lblDatos.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDatos.ForeColor = Ui.Navy;

            GroupBox h = Ui.Grupo(this, "2. Historial de compras", 12, 296, 1076, 340);
            rbCompra = new RadioButton();
            rbCompra.Text = "Ordenar por fecha de compra (de la más lejana a la más reciente)";
            rbCompra.AutoSize = true;
            rbCompra.Checked = true;
            rbCompra.Location = new Point(12, 24);
            h.Controls.Add(rbCompra);
            rbPartido = new RadioButton();
            rbPartido.Text = "Ordenar por fecha de partido (pendientes primero)";
            rbPartido.AutoSize = true;
            rbPartido.Location = new Point(500, 24);
            h.Controls.Add(rbPartido);
            rbCompra.CheckedChanged += delegate { if (rbCompra.Checked) CargarHistorial(); };
            rbPartido.CheckedChanged += delegate { if (rbPartido.Checked) CargarHistorial(); };
            dgvHistorial = Ui.Grid(h, 12, 54, 1050, 276);

            Ui.Boton(this, "Cerrar", 968, 642, 120, 30, Ui.Gris, delegate { Close(); });

            Load += delegate
            {
                if (!Sesion.EsAdmin) { Tema.Aviso("Solo un administrador puede consultar el historial.", "Historial"); Close(); }
            };
        }

        void Buscar()
        {
            if (txtApellidos.Text.Trim() == "") { Tema.Aviso("Escribe los apellidos del cliente.", "Historial"); txtApellidos.Focus(); return; }
            try
            {
                DataTable dt = clientes.Buscar(txtApellidos.Text);
                dgvClientes.DataSource = dt;
                Tema.FormatoGrid(dgvClientes);
                idCliente = 0;
                dgvHistorial.DataSource = null;
                lblDatos.Text = dt.Rows.Count == 0 ? "No se encontraron clientes con esos apellidos."
                    : "Se encontraron " + dt.Rows.Count + " cliente(s). Elige el correcto.";
            }
            catch (Exception ex) { Tema.Manejar(ex, "Historial"); }
        }

        void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow f = dgvClientes.Rows[e.RowIndex];
            idCliente = Convert.ToInt32(f.Cells["ID"].Value);
            lblDatos.Text = Convert.ToString(f.Cells["Nombre"].Value) + "   |   RFC: " + Convert.ToString(f.Cells["RFC"].Value)
                + "   |   " + Convert.ToString(f.Cells["Correo"].Value) + "   |   Cel: " + Convert.ToString(f.Cells["Tel. celular"].Value)
                + "   |   Nacimiento: " + Formato.Fecha(Convert.ToDateTime(f.Cells["Fecha de nacimiento"].Value));
            CargarHistorial();
        }

        void CargarHistorial()
        {
            if (idCliente == 0) return;
            try
            {
                dgvHistorial.DataSource = clientes.Historial(idCliente, rbCompra.Checked ? "COMPRA" : "PARTIDO");
                Tema.FormatoGrid(dgvHistorial);
            }
            catch (Exception ex) { Tema.Manejar(ex, "Historial"); }
        }
    }
}
