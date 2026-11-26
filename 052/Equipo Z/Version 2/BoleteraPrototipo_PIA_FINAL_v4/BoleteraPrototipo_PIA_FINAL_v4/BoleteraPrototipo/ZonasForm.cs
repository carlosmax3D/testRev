using System;
using System.Data;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    public partial class ZonasForm : Form
    {
        readonly ZonaRepositorio zonas = new ZonaRepositorio();
        int? idActual = null; // null = registro nuevo

        public ZonasForm()
        {
            InitializeComponent();
        }

        private void ZonasForm_Load(object sender, EventArgs e)
        {
            try
            {
                Tema.LlenarCombo(cmbEstadio, new EstadioRepositorio().Combo());
                CargarGrid();
            }
            catch (Exception ex) { Tema.ErrorConexion(ex); }
        }

        void CargarGrid()
        {
            dgvDatos.DataSource = zonas.Listar();
            Tema.FormatoGrid(dgvDatos);
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvDatos.Rows[e.RowIndex];
            idActual = Convert.ToInt32(fila.Cells["ID"].Value);
            cmbEstadio.SelectedValue = Convert.ToInt32(fila.Cells["id_estadio"].Value);
            txtNombreZona.Text = Convert.ToString(fila.Cells["Zona"].Value);
            nudPrecio.Value = Convert.ToDecimal(fila.Cells["Precio base"].Value);
            nudCupo.Value = Convert.ToDecimal(fila.Cells["Cupo"].Value);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreZona.Text.Trim();
            if (!(cmbEstadio.SelectedValue is int)) { Tema.Info("Elige: Estadio", "Falta un dato"); cmbEstadio.Focus(); return; }
            if (nombre == "") { Tema.Info("Escribe: Nombre de zona", "Falta un dato"); txtNombreZona.Focus(); return; }
            if (nudCupo.Value <= 0) { Tema.Info("El cupo de la zona debe ser mayor a 0.", "Falta un dato"); nudCupo.Focus(); return; }
            int idEstadio = (int)cmbEstadio.SelectedValue;
            try
            {
                bool nuevo = idActual == null;
                if (nuevo) zonas.Insertar(idEstadio, nombre, nudPrecio.Value, (int)nudCupo.Value);
                else zonas.Actualizar(idActual.Value, idEstadio, nombre, nudPrecio.Value, (int)nudCupo.Value);
                Limpiar();
                CargarGrid();
                Tema.Info(nuevo ? "Zona registrada correctamente." : "Zona actualizada correctamente.", "Zonas");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Zonas"); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idActual == null) { Tema.Info("Primero elige una zona de la tabla.", "Zonas"); return; }
            if (!Tema.ConfirmarEliminar("Zonas")) return;
            try
            {
                zonas.Eliminar(idActual.Value);
                Limpiar();
                CargarGrid();
                Tema.Info("Zona eliminada.", "Zonas");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Zonas"); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            idActual = null;
            cmbEstadio.SelectedIndex = -1;
            txtNombreZona.Clear();
            nudPrecio.Value = 0;
            nudCupo.Value = 0;
            dgvDatos.ClearSelection();
        }
    }
}
