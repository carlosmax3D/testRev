using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmClientes : FormBase
{
    private readonly IClienteService _clienteService;
    private int _idClienteSeleccionado = 0;

    public FrmClientes(IClienteService clienteService)
        : base("Gestión de Clientes", 820, 560)
    {
        InitializeComponent();
        _clienteService = clienteService;
        ConfigurarBotones();
    }

    private void ConfigurarBotones()
    {
        btnEditar.Click += btnEditar_Click;
        btnEliminar.Click += btnEliminar_Click;
        btnNuevo.Click += (s, e) => LimpiarFormulario();
        dgvClientes.CellClick += dgvClientes_CellClick;
    }

    private void FrmClientes_Load(object sender, EventArgs e) => CargarClientes();

    private void CargarClientes()
    {
        try { dgvClientes.DataSource = _clienteService.ObtenerClientes(); }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Cliente c = new()
            {
                Nombre = txtNombre.Text.Trim(), ApellidoPaterno = txtPaterno.Text.Trim(), ApellidoMaterno = txtMaterno.Text.Trim(),
                Calle = txtCalle.Text.Trim(), Numero = txtNumero.Text.Trim(), Colonia = txtColonia.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(), Estado = txtEstado.Text.Trim(), CP = txtCP.Text.Trim(),
                RFC = txtRFC.Text.Trim(), CorreoElectronico = txtCorreo.Text.Trim(),
                TelefonoCasa = txtTelCasa.Text.Trim(), TelefonoCelular = txtTelCelular.Text.Trim(),
                FechaNacimiento = dtpFechaNac.Value, UsuarioRegistro = SesionActual.IdUsuario
            };
            _clienteService.CrearCliente(c);
            MessageBox.Show("Cliente guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario(); CargarClientes();
        }
        catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
        {
            MessageBox.Show("El RFC o el correo ya están registrados en otro cliente. Intenta con otros.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void btnEditar_Click(object sender, EventArgs e)
    {
        if (_idClienteSeleccionado == 0) { MessageBox.Show("Selecciona un cliente."); return; }
        try
        {
            Cliente c = new()
            {
                IdCliente = _idClienteSeleccionado,
                Nombre = txtNombre.Text.Trim(), ApellidoPaterno = txtPaterno.Text.Trim(), ApellidoMaterno = txtMaterno.Text.Trim(),
                Calle = txtCalle.Text.Trim(), Numero = txtNumero.Text.Trim(), Colonia = txtColonia.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(), Estado = txtEstado.Text.Trim(), CP = txtCP.Text.Trim(),
                RFC = txtRFC.Text.Trim(), CorreoElectronico = txtCorreo.Text.Trim(),
                TelefonoCasa = txtTelCasa.Text.Trim(), TelefonoCelular = txtTelCelular.Text.Trim(),
                FechaNacimiento = dtpFechaNac.Value
            };
            _clienteService.ActualizarCliente(c);
            MessageBox.Show("Cliente actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario(); CargarClientes();
        }
        catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
        {
            MessageBox.Show("El RFC o el correo ya pertenecen a otro cliente.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        if (_idClienteSeleccionado == 0) { MessageBox.Show("Selecciona un cliente."); return; }
        if (MessageBox.Show("¿Eliminar cliente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            try
            {
                _clienteService.EliminarCliente(_idClienteSeleccionado);
                MessageBox.Show("Cliente eliminado.");
                LimpiarFormulario(); CargarClientes();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("No se puede eliminar a este cliente porque tiene ventas o facturas asociadas en el sistema.", "Error de Integridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }

    private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (dgvClientes.Rows[e.RowIndex].DataBoundItem is Cliente c)
        {
            _idClienteSeleccionado = c.IdCliente;
            txtNombre.Text = c.Nombre; txtPaterno.Text = c.ApellidoPaterno; txtMaterno.Text = c.ApellidoMaterno;
            txtCalle.Text = c.Calle; txtNumero.Text = c.Numero; txtColonia.Text = c.Colonia;
            txtCiudad.Text = c.Ciudad; txtEstado.Text = c.Estado; txtCP.Text = c.CP;
            txtRFC.Text = c.RFC; txtCorreo.Text = c.CorreoElectronico;
            txtTelCasa.Text = c.TelefonoCasa; txtTelCelular.Text = c.TelefonoCelular;
            dtpFechaNac.Value = c.FechaNacimiento;
        }
    }

    private void LimpiarFormulario()
    {
        _idClienteSeleccionado = 0;
        txtNombre.Clear(); txtPaterno.Clear(); txtMaterno.Clear(); txtCalle.Clear();
        txtNumero.Clear(); txtColonia.Clear(); txtCiudad.Clear(); txtEstado.Clear();
        txtCP.Clear(); txtRFC.Clear(); txtCorreo.Clear(); txtTelCasa.Clear(); txtTelCelular.Clear();
        txtNombre.Focus();
    }
}
