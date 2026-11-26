using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

public partial class FrmClientes : Form
{
    private readonly IClienteService _clientes;

    private bool _editando;

    public FrmClientes(IClienteService clientes)
    {
        _clientes = clientes;

        InitializeComponent();
        Ui.Estilizar(dgvClientes);

        dtpNacimiento.MaxDate = DateTime.Today;
        dtpNacimiento.Value = DateTime.Today.AddYears(-25);

        txtBuscar.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                Recargar();
                e.SuppressKeyPress = true;
            }
        };

        Load += (_, _) => Recargar();
    }

    private void BtnBuscar_Click(object? sender, EventArgs e) => Recargar();

    private void BtnNuevo_Click(object? sender, EventArgs e) => Limpiar();

    private void Recargar()
    {
        Ui.Ejecutar(this, () => dgvClientes.DataSource = _clientes.BuscarClientes(txtBuscar.Text.Trim(), chkActivos.Checked));
        Limpiar();
    }

    private void Limpiar()
    {
        _editando = false;
        txtRfc.Enabled = true;
        txtRfc.Clear();
        txtNombre.Clear();
        txtDomicilio.Clear();
        txtCorreo.Clear();
        txtTelCasa.Clear();
        txtTelCelular.Clear();
        dtpNacimiento.Value = DateTime.Today.AddYears(-25);
        dgvClientes.ClearSelection();
        txtRfc.Focus();
    }

    private void DgvClientes_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvClientes.CurrentRow?.DataBoundItem is not Cliente c || !dgvClientes.CurrentRow.Selected)
        {
            return;
        }

        _editando = true;
        txtRfc.Text = c.RFC;
        txtRfc.Enabled = false;
        txtNombre.Text = c.NombreCompleto;
        txtDomicilio.Text = c.DomicilioCompleto;
        txtCorreo.Text = c.Correo;
        txtTelCasa.Text = c.TelefonoCasa ?? string.Empty;
        txtTelCelular.Text = c.TelefonoCelular;
        dtpNacimiento.Value = c.FechaNacimiento < dtpNacimiento.MinDate ? dtpNacimiento.MinDate : c.FechaNacimiento;
    }

    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        Cliente cliente = new()
        {
            RFC = txtRfc.Text.Trim(),
            NombreCompleto = txtNombre.Text,
            DomicilioCompleto = txtDomicilio.Text,
            Correo = txtCorreo.Text,
            TelefonoCasa = string.IsNullOrWhiteSpace(txtTelCasa.Text) ? null : txtTelCasa.Text.Trim(),
            TelefonoCelular = txtTelCelular.Text.Trim(),
            FechaNacimiento = dtpNacimiento.Value.Date
        };

        bool editando = _editando;

        bool ok = Ui.Ejecutar(this, () =>
        {
            if (editando)
            {
                _clientes.ActualizarCliente(cliente);
            }
            else
            {
                _clientes.CrearCliente(cliente);
            }
        });

        if (ok)
        {
            Ui.Aviso(this, editando ? "Cliente actualizado correctamente." : "Cliente registrado correctamente.");
            Recargar();
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
        if (!_editando)
        {
            Ui.Aviso(this, "Seleccione un cliente de la lista.");
            return;
        }

        string rfc = txtRfc.Text.Trim();

        if (!Ui.Confirmar(this, $"¿Desea dar de baja al cliente {rfc}? (baja lógica)"))
        {
            return;
        }

        if (Ui.Ejecutar(this, () => _clientes.EliminarCliente(rfc)))
        {
            Ui.Aviso(this, "Cliente dado de baja.");
            Recargar();
        }
    }
}
