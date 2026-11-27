using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;



namespace TemplateCSharp.Forms;

public partial class FrmClientes : Form
{
    private readonly IClienteService _clienteService;
    private int _idClienteSeleccionado = 0;

    public FrmClientes(IClienteService clienteService)
    {
        InitializeComponent();
        _clienteService = clienteService;
    }

    private void FrmClientes_Load(object sender, EventArgs e)
    {
        CargarClientes();
    }

    private void CargarClientes(string? filtro = null)
    {
        try
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = _clienteService.ObtenerClientes(filtro);

            // Ocultamos columnas internas que no necesita ver el usuario
            if (dgvClientes.Columns["UsuarioAuditoria"] != null)
                dgvClientes.Columns["UsuarioAuditoria"].Visible = false;

            if (dgvClientes.Columns["NombreCompleto"] != null)
                dgvClientes.Columns["NombreCompleto"].Visible = false;

            // Formato de fecha exigido en rúbrica (dd-MMM-yyyy)
            if (dgvClientes.Columns["FechaNacimiento"] != null)
                dgvClientes.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd-MMM-yyyy";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnRegistrar_Click(object sender, EventArgs e)
    {
        try
        {
            Cliente nuevoCliente = ObtenerDatosFormulario();
            _clienteService.RegistrarCliente(nuevoCliente);

            MessageBox.Show("Cliente registrado correctamente en la base de datos.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            CargarClientes();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Cliente clienteEditado = ObtenerDatosFormulario();
            clienteEditado.IdCliente = _idClienteSeleccionado;

            _clienteService.ActualizarCliente(clienteEditado);

            MessageBox.Show("Datos del cliente actualizados correctamente.", "Modificación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (_idClienteSeleccionado <= 0)
            {
                MessageBox.Show("Selecciona un cliente de la tabla para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este cliente?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                _clienteService.EliminarCliente(_idClienteSeleccionado);
                MessageBox.Show("Cliente eliminado correctamente.", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                CargarClientes();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarClientes(txt_buscar.Text.Trim());
    }

    // Conecta este evento en el rayito (CellClick) de dgvClientes para llenar las cajas al dar clic
    private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        if (dgvClientes.Rows[e.RowIndex].DataBoundItem is Cliente cliente)
        {
            _idClienteSeleccionado = cliente.IdCliente;
            txt_Nombre.Text = cliente.Nombre;
            txt_ApellidoP.Text = cliente.ApellidoPaterno;
            txt_ApellidoM.Text = cliente.ApellidoMaterno;
            txt_Domicilio.Text = cliente.Domicilio;
            txt_RFC.Text = cliente.RFC;
            txt_Correo.Text = cliente.Correo;
            txt_telefono_casa.Text = cliente.TelefonoCasa;
            txt_telefono_cel.Text = cliente.TelefonoCelular;
            dtpFechaNacimiento.Value = cliente.FechaNacimiento;
        }
    }

    private Cliente ObtenerDatosFormulario()
    {
        return new Cliente
        {
            Nombre = txt_Nombre.Text.Trim(),
            ApellidoPaterno = txt_ApellidoP.Text.Trim(),
            ApellidoMaterno = txt_ApellidoM.Text.Trim(),
            Domicilio = txt_Domicilio.Text.Trim(),
            RFC = txt_RFC.Text.Trim(),
            Correo = txt_Correo.Text.Trim(),
            TelefonoCasa = txt_telefono_casa.Text.Trim(),
            TelefonoCelular = txt_telefono_cel.Text.Trim(),
            FechaNacimiento = dtpFechaNacimiento.Value.Date
        };
    }

    private void LimpiarFormulario()
    {
        _idClienteSeleccionado = 0;
        txt_Nombre.Clear();
        txt_ApellidoP.Clear();
        txt_ApellidoM.Clear();
        txt_Domicilio.Clear();
        txt_RFC.Clear();
        txt_Correo.Clear();
        txt_telefono_casa.Clear();
        txt_telefono_cel.Clear();
        dtpFechaNacimiento.Value = DateTime.Today.AddYears(-18);
        txt_Nombre.Focus();
    }

    private void label3_Click(object sender, EventArgs e) { }
    private void label8_Click(object sender, EventArgs e) { }
}