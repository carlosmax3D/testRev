using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms.Clientes
{
    public partial class FrmClientes : Form
    {
        private readonly IClienteService _clienteService;
        private readonly IServiceProvider _serviceProvider;
        private int _idSeleccionado = 0;

        public FrmClientes(IClienteService clienteService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _clienteService = clienteService;
            _serviceProvider = serviceProvider;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApellidoP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApellidoM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRFC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCalle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtColonia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefonoCasa_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefonoCelular_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpFechaNacimiento_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new()
                {
                    Nombre = txtNombre.Text.Trim(),
                    ApellidoPa = txtApellidoP.Text.Trim(),
                    ApellidoMa = txtApellidoM.Text.Trim(),
                    RFC = txtRFC.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Calle = txtCalle.Text.Trim(),
                    Numero = txtNumero.Text.Trim(),
                    Colonia = txtColonia.Text.Trim(),
                    CP = txtCP.Text.Trim(),
                    TelefonoCasa = txtTelefonoCasa.Text.Trim(),
                    TelefonoCelular = txtTelefonoCelular.Text.Trim(),
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    UsuarioCambio = Sesion.IdUsuario
                };

                if (_idSeleccionado == 0)
                {
                    _clienteService.CrearCliente(cliente);
                    MessageBox.Show("Cliente registrado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cliente.IdCliente = _idSeleccionado;
                    _clienteService.ActualizarCliente(cliente);
                    MessageBox.Show("Cliente actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Limpiar();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0 || dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un cliente de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Llenamos los TextBox con los datos del cliente seleccionado
            var cliente = (Cliente)dgvClientes.CurrentRow.DataBoundItem;

            txtNombre.Text = cliente.Nombre;
            txtApellidoP.Text = cliente.ApellidoPa;
            txtApellidoM.Text = cliente.ApellidoMa;
            txtRFC.Text = cliente.RFC;
            txtCorreo.Text = cliente.Correo;
            txtCalle.Text = cliente.Calle;
            txtNumero.Text = cliente.Numero;
            txtColonia.Text = cliente.Colonia;
            txtCP.Text = cliente.CP;
            txtTelefonoCasa.Text = cliente.TelefonoCasa;
            txtTelefonoCelular.Text = cliente.TelefonoCelular;
            dtpFechaNacimiento.Value = cliente.FechaNacimiento;

            MessageBox.Show("Modifique los campos y presione Guardar para actualizar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            txtBuscar.Clear();   //limpia el buscador
            CargarClientes();    //vuelve a mostrar TODOS los clientes en el grid
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarClientes();
                return;
            }

            try
            {
                dgvClientes.DataSource = _clienteService.BuscarPorApellidos(txtBuscar.Text.Trim(), string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvClientes.CurrentRow == null) return;

            //Solo guardamos el ID de la fila seleccionada, NO llenamos nada.
            var cliente = (Cliente)dgvClientes.CurrentRow.DataBoundItem;
            if (cliente != null)
            {
                _idSeleccionado = cliente.IdCliente;
            }
        }

        private void Limpiar()
        {
            _idSeleccionado = 0;
            txtNombre.Clear();
            txtApellidoP.Clear();
            txtApellidoM.Clear();
            txtRFC.Clear();
            txtCorreo.Clear();
            txtCalle.Clear();
            txtNumero.Clear();
            txtColonia.Clear();
            txtCP.Clear();
            txtTelefonoCasa.Clear();
            txtTelefonoCelular.Clear();
            txtBuscar.Clear();
            txtNombre.Focus();
            dtpFechaNacimiento.Value = DateTime.Today;
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            dgvClientes.AutoGenerateColumns = false;
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                dgvClientes.DataSource = _clienteService.ObtenerClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                _clienteService.EliminarCliente(_idSeleccionado);
                MessageBox.Show("Cliente eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarClientes();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 547)
            {
                //El error 547 en SQL Server significa "Violación de restriccion de FK"
                MessageBox.Show("No se puede eliminar este cliente porque ya tiene ventas de boletos registradas en el sistema. Para mantener la integridad de los datos, primero debe cancelar sus compras o conservar el registro histórico.",
                                "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
