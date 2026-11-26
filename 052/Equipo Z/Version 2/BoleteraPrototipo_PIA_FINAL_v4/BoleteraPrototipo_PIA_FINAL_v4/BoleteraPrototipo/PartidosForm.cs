using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    public partial class PartidosForm : Form
    {
        readonly PartidoRepositorio partidos = new PartidoRepositorio();
        int? idActual = null; // null = registro nuevo
        Button btnCancelarPartido;

        public PartidosForm()
        {
            InitializeComponent();

            // botón para cancelar un partido (cancela también sus ventas)
            grpDatos.Height += 50;
            btnCancelarPartido = new Button();
            btnCancelarPartido.Text = "Cancelar partido";
            btnCancelarPartido.SetBounds(16, 288, 320, 32);
            btnCancelarPartido.BackColor = Ui.Naranja;
            btnCancelarPartido.ForeColor = Color.White;
            btnCancelarPartido.FlatStyle = FlatStyle.Flat;
            btnCancelarPartido.FlatAppearance.BorderSize = 0;
            btnCancelarPartido.UseVisualStyleBackColor = false;
            btnCancelarPartido.Click += btnCancelarPartido_Click;
            grpDatos.Controls.Add(btnCancelarPartido);
        }

        private void PartidosForm_Load(object sender, EventArgs e)
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
            dgvDatos.DataSource = partidos.Listar();
            Tema.FormatoGrid(dgvDatos);
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvDatos.Rows[e.RowIndex];
            idActual = Convert.ToInt32(fila.Cells["ID"].Value);
            cmbEstadio.SelectedValue = Convert.ToInt32(fila.Cells["id_estadio"].Value);
            DateTime fh = Convert.ToDateTime(fila.Cells["Fecha y hora"].Value);
            dtpFechaHora.Value = fh < dtpFechaHora.MinDate ? dtpFechaHora.MinDate : fh;
            txtLocal.Text = Convert.ToString(fila.Cells["Local"].Value);
            txtVisitante.Text = Convert.ToString(fila.Cells["Visitante"].Value);
            cmbEstado.SelectedItem = Convert.ToString(fila.Cells["Estado"].Value);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string local = txtLocal.Text.Trim();
            string visitante = txtVisitante.Text.Trim();
            if (!(cmbEstadio.SelectedValue is int)) { Tema.Info("Elige: Estadio", "Falta un dato"); cmbEstadio.Focus(); return; }
            if (local == "") { Tema.Info("Escribe: Equipo local", "Falta un dato"); txtLocal.Focus(); return; }
            if (visitante == "") { Tema.Info("Escribe: Equipo visitante", "Falta un dato"); txtVisitante.Focus(); return; }
            if (local.ToLower() == visitante.ToLower()) { Tema.Info("El equipo local y el visitante no pueden ser el mismo.", "Falta un dato"); txtVisitante.Focus(); return; }
            if (cmbEstado.SelectedItem == null) { Tema.Info("Elige: Estado", "Falta un dato"); cmbEstado.Focus(); return; }
            if (idActual == null && dtpFechaHora.Value < DateTime.Now)
            {
                Tema.Aviso("No se puede registrar un partido en una fecha pasada.", "Partidos");
                dtpFechaHora.Focus();
                return;
            }
            int idEstadio = (int)cmbEstadio.SelectedValue;
            string estado = cmbEstado.SelectedItem.ToString();
            try
            {
                bool nuevo = idActual == null;
                if (nuevo) partidos.Insertar(idEstadio, dtpFechaHora.Value, local, visitante, estado);
                else partidos.Actualizar(idActual.Value, idEstadio, dtpFechaHora.Value, local, visitante, estado);
                Limpiar();
                CargarGrid();
                Tema.Info(nuevo ? "Partido registrado correctamente." : "Partido actualizado correctamente.", "Partidos");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Partidos"); }
        }

        private void btnCancelarPartido_Click(object sender, EventArgs e)
        {
            if (idActual == null) { Tema.Info("Primero elige un partido de la tabla.", "Partidos"); return; }
            string motivo = PedirMotivo();
            if (motivo == null) return;
            if (!Tema.Pregunta("Se cancelará el partido y TODAS sus ventas (se liberan los boletos y se cancelan las facturas).\n¿Continuar?", "Cancelar partido")) return;
            try
            {
                partidos.Cancelar(idActual.Value, motivo);
                Limpiar();
                CargarGrid();
                Tema.Info("Partido cancelado correctamente.", "Partidos");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Partidos"); }
        }

        // ventanita para escribir el motivo (regresa null si se cancela o queda vacío)
        string PedirMotivo()
        {
            using (Form f = new Form())
            {
                f.Text = "Motivo de la cancelación";
                f.StartPosition = FormStartPosition.CenterParent;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MaximizeBox = false; f.MinimizeBox = false;
                f.ClientSize = new Size(380, 130);
                Label l = new Label(); l.Text = "Escribe el motivo de la cancelación del partido:"; l.AutoSize = true; l.Location = new Point(12, 12);
                TextBox t = new TextBox(); t.SetBounds(12, 36, 356, 23); t.MaxLength = 150;
                Button ok = new Button(); ok.Text = "Aceptar"; ok.SetBounds(196, 84, 80, 30); ok.DialogResult = DialogResult.OK;
                Button no = new Button(); no.Text = "Cancelar"; no.SetBounds(288, 84, 80, 30); no.DialogResult = DialogResult.Cancel;
                f.Controls.Add(l); f.Controls.Add(t); f.Controls.Add(ok); f.Controls.Add(no);
                f.AcceptButton = ok; f.CancelButton = no;
                if (f.ShowDialog(this) != DialogResult.OK) return null;
                if (t.Text.Trim() == "") { Tema.Aviso("Escribe el motivo de la cancelación.", "Partidos"); return null; }
                return t.Text.Trim();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idActual == null) { Tema.Info("Primero elige un partido de la tabla.", "Partidos"); return; }
            if (!Tema.ConfirmarEliminar("Partidos")) return;
            try
            {
                partidos.Eliminar(idActual.Value);
                Limpiar();
                CargarGrid();
                Tema.Info("Partido eliminado.", "Partidos");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Partidos"); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            idActual = null;
            cmbEstadio.SelectedIndex = -1;
            dtpFechaHora.Value = DateTime.Now;
            txtLocal.Clear();
            txtVisitante.Clear();
            cmbEstado.SelectedIndex = -1;
            dgvDatos.ClearSelection();
        }
    }
}
