using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TemplateCSharp.Forms.Clientes;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms.VentaBoletos
{
    public partial class FrmBuscarCliente : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IClienteService _clienteService;

        private Cliente? _clienteSeleccionado = null;

        
        public FrmBuscarCliente(IServiceProvider serviceProvider, IClienteService clienteService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _clienteService = clienteService;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show("Ingrese un apellido para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                var resultados = _clienteService.BuscarPorApellidos(txtBuscar.Text.Trim(), string.Empty);

                if (resultados.Count == 0)
                {
                    MessageBox.Show("No se encontraron clientes con ese apellido.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvClientes.DataSource = null;
                    return;
                }

                
                dgvClientes.AutoGenerateColumns = false;
                dgvClientes.DataSource = resultados;

                
                if (dgvClientes.Columns.Count >= 3)
                {
                    dgvClientes.Columns[0].DataPropertyName = "Nombre";
                    dgvClientes.Columns[1].DataPropertyName = "ApellidoPa";
                    dgvClientes.Columns[2].DataPropertyName = "Correo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cuando el usuario hace clic en un cliente de la tabla
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _clienteSeleccionado = (Cliente)dgvClientes.Rows[e.RowIndex].DataBoundItem;

                if (_clienteSeleccionado != null)
                {
                    lblClienteSeleccionado.Text = $"{_clienteSeleccionado.Nombre} {_clienteSeleccionado.ApellidoPa} {_clienteSeleccionado.ApellidoMa}";
                    lblCorreo.Text = _clienteSeleccionado.Correo;
                }
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            
            using var form = _serviceProvider.GetRequiredService<FrmClientes>();
            form.ShowDialog();

            // Si el usuario registro a alguien, podríamos limpiar la búsqueda para que lo busque
            txtBuscar.Clear();
            dgvClientes.DataSource = null;
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            // Validamos que realmente haya seleccionado a alguien
            if (_clienteSeleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente de la tabla antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guardamos el cliente en el contexto global
            VentaContext.ClienteSeleccionado = _clienteSeleccionado;

            // Navegación al siguiente paso (Selección de Partido)
            this.Hide();
            using var form = _serviceProvider.GetRequiredService<FrmSeleccionPartido>();
            form.ShowDialog();

            // Si en la siguiente ventana cancelan la compra, volvemos a mostrar esta
            if (form.DialogResult == DialogResult.Cancel)
                this.Show();
            else
                this.Close();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Limpiamos el contexto porque se canceló la venta
            VentaContext.Limpiar();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Eventos vacíos, no mover, no tocar, dejalos existir
        private void txtBuscar_TextChanged(object sender, EventArgs e) { }
        private void lblClienteSeleccionado_Click(object sender, EventArgs e) { }
        private void lblCorreo_Click(object sender, EventArgs e) { }
    }
}