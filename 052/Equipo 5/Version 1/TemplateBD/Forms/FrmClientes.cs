using System;
using System.Windows.Forms;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

public partial class FrmClientes : Form
{
    private readonly ClienteService _clienteService;
    private readonly SesionUsuario _sesion;

    public FrmClientes(ClienteService clienteService, SesionUsuario sesion)
    {
        InitializeComponent();
        _clienteService = clienteService;
        _sesion = sesion;
    }

    private void FrmClientes_Load(object sender, EventArgs e)
    {
        CargarClientes();
        LimpiarFormulario();
    }

    private void CargarClientes()
    {
        //try
        {
            dgvClientes.DataSource = _clienteService.ObtenerClientes();
        }
        /*
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar clientes frm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        */
    }

    private void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            string apellido = txtBuscarApellido.Text.Trim();
            if (string.IsNullOrEmpty(apellido))
            {
                CargarClientes();
                return;
            }

            dgvClientes.DataSource = _clienteService.BuscarClientesPorApellido(apellido);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error en búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnRestablecer_Click(object sender, EventArgs e)
    {
        txtBuscarApellido.Clear();
        CargarClientes();
    }

    private void btnNuevo_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Cliente cliente = new()
            {
                Nombre = txtNombre.Text.Trim(),
                Apellidos = txtApellidos.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Rfc = string.IsNullOrWhiteSpace(txtRfc.Text) ? null : txtRfc.Text.Trim(),
                CURP = txtCURP.Text.Trim(),
                IdUsuarioRegistro = _sesion.UsuarioActual?.IdUsuario
            };

            _clienteService.GuardarCliente(cliente, esNuevo: true);

            MessageBox.Show("Cliente registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            CargarClientes();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Seleccione un cliente de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cliente cliente = new()
            {
                IdCliente = id,
                Nombre = txtNombre.Text.Trim(),
                Apellidos = txtApellidos.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Rfc = string.IsNullOrWhiteSpace(txtRfc.Text) ? null : txtRfc.Text.Trim(),
                CURP = txtCURP.Text.Trim()
            };

            _clienteService.GuardarCliente(cliente, esNuevo: false);

            MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            CargarClientes();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Seleccione un cliente para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea eliminar a este cliente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _clienteService.BorrarCliente(id);

            MessageBox.Show("Cliente eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            CargarClientes();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];

        txtId.Text = fila.Cells["IdCliente"].Value?.ToString() ?? string.Empty;
        txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? string.Empty;
        txtApellidos.Text = fila.Cells["Apellidos"].Value?.ToString() ?? string.Empty;
        txtEmail.Text = fila.Cells["Email"].Value?.ToString() ?? string.Empty;
        txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString() ?? string.Empty;
        txtRfc.Text = fila.Cells["Rfc"].Value?.ToString() ?? string.Empty;
        txtCURP.Text = fila.Cells["CURP"].Value?.ToString() ?? string.Empty;
    }

    private void LimpiarFormulario()
    {
        txtId.Clear();
        txtNombre.Clear();
        txtApellidos.Clear();
        txtEmail.Clear();
        txtTelefono.Clear();
        txtRfc.Clear();
        txtCURP.Clear();
        txtNombre.Focus();

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }
}