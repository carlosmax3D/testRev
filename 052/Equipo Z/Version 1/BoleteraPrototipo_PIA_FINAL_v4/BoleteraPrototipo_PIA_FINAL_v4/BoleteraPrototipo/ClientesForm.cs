using System;
using System.Data;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    public partial class ClientesForm : Form
    {
        readonly ClienteRepositorio clientes = new ClienteRepositorio();
        int? idActual = null; // null = registro nuevo

        public ClientesForm()
        {
            InitializeComponent();
        }

        private void ClientesForm_Load(object sender, EventArgs e)
        {
            dtpNacimiento.MaxDate = DateTime.Today;
            txtRfc.CharacterCasing = CharacterCasing.Upper;
            txtTelCasa.MaxLength = 10;
            txtTelCelular.MaxLength = 10;
            try { CargarGrid(); }
            catch (Exception ex) { Tema.ErrorConexion(ex); }
        }

        void CargarGrid()
        {
            dgvDatos.DataSource = clientes.Listar();
            Tema.FormatoGrid(dgvDatos);
        }

        // Al dar clic a una fila se cargan sus datos para editarlos
        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvDatos.Rows[e.RowIndex];
            idActual = Convert.ToInt32(fila.Cells["ID"].Value);
            txtNombre.Text = Convert.ToString(fila.Cells["Nombre"].Value);
            txtDomicilio.Text = Convert.ToString(fila.Cells["Domicilio"].Value);
            txtRfc.Text = Convert.ToString(fila.Cells["RFC"].Value);
            txtCorreo.Text = Convert.ToString(fila.Cells["Correo"].Value);
            txtTelCasa.Text = Convert.ToString(fila.Cells["Tel. casa"].Value);
            txtTelCelular.Text = Convert.ToString(fila.Cells["Tel. celular"].Value);
            DateTime nac = Convert.ToDateTime(fila.Cells["Fecha de nacimiento"].Value);
            dtpNacimiento.Value = nac < dtpNacimiento.MinDate ? dtpNacimiento.MinDate : nac;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cliente c = new Cliente();
            c.NombreCompleto = txtNombre.Text;
            c.Domicilio = txtDomicilio.Text;
            c.Rfc = txtRfc.Text;
            c.Correo = txtCorreo.Text;
            c.TelCasa = txtTelCasa.Text;
            c.TelCelular = txtTelCelular.Text;
            c.FechaNacimiento = dtpNacimiento.Value.Date;
            if (Tema.HayErrores(c.Validar(), "Clientes")) return;

            try
            {
                bool nuevo = idActual == null;
                if (nuevo) clientes.Insertar(c);
                else { c.Id = idActual.Value; clientes.Actualizar(c); }
                Limpiar();
                CargarGrid();
                Tema.Info(nuevo ? "Cliente registrado correctamente." : "Cliente actualizado correctamente.", "Clientes");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Clientes"); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idActual == null) { Tema.Info("Primero elige un cliente de la tabla.", "Clientes"); return; }
            if (!Tema.ConfirmarEliminar("Clientes")) return;
            try
            {
                clientes.Eliminar(idActual.Value);
                Limpiar();
                CargarGrid();
                Tema.Info("Cliente eliminado.", "Clientes");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Clientes"); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            idActual = null;
            txtNombre.Clear();
            txtDomicilio.Clear();
            txtRfc.Clear();
            txtCorreo.Clear();
            txtTelCasa.Clear();
            txtTelCelular.Clear();
            dtpNacimiento.Value = DateTime.Today;
            dgvDatos.ClearSelection();
        }
    }
}
