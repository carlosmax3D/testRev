using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

public partial class FrmClientes : Form
{
    private readonly IClienteService _clienteService;
    private readonly bool _esAdministrador;
    private bool _ciudadDelCatalogo;
    private int _idSeleccionado;
    private Cliente? _seleccionado;

    public FrmClientes(IClienteService clienteService, ISesionUsuario sesion)
    {
        InitializeComponent();
        _clienteService = clienteService;
        _esAdministrador = sesion.UsuarioActual?.EsAdministrador ?? false;
        dtpNacimiento.MaxDate = DateTime.Today;
        dgvClientes.AutoGenerateColumns = false;

        // Solo un administrador consulta y reactiva clientes dados de baja
        chkInactivos.Visible = _esAdministrador;
        btnReactivar.Visible = _esAdministrador;
    }

    private void CargarClientes()
    {
        try
        {
            dgvClientes.DataSource = chkInactivos.Checked
                ? _clienteService.ObtenerClientesConInactivos(txtBuscar.Text)
                : _clienteService.ObtenerClientes(txtBuscar.Text);
            dgvClientes.ClearSelection();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible cargar los clientes.\n\n{Errores.Mensaje(ex)}");
        }
    }

    // Si el código postal ya existe se llenan la ciudad y el estado; solo un administrador puede cambiarlos
    private void txtCodigoPostal_TextChanged(object? sender, EventArgs e)
    {
        try
        {
            CodigoPostalInfo? info = _clienteService.ObtenerCodigoPostal(txtCodigoPostal.Text.Trim());
            if (info is not null)
            {
                txtCiudad.Text = info.Ciudad;
                txtEstado.Text = info.EntidadFederativa;
                txtCiudad.ReadOnly = !_esAdministrador;
                txtEstado.ReadOnly = !_esAdministrador;
                _ciudadDelCatalogo = true;
            }
            else
            {
                if (_ciudadDelCatalogo)
                {
                    txtCiudad.Clear();
                    txtEstado.Clear();
                    _ciudadDelCatalogo = false;
                }

                txtCiudad.ReadOnly = false;
                txtEstado.ReadOnly = false;
            }
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible consultar el código postal.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private Cliente LeerFormulario()
    {
        return new Cliente
        {
            IdCliente = _idSeleccionado,
            Nombre = txtNombre.Text,
            ApellidoPaterno = txtPaterno.Text,
            ApellidoMaterno = txtMaterno.Text,
            Calle = txtCalle.Text,
            NumeroExterior = txtNumExterior.Text,
            Colonia = txtColonia.Text,
            CodigoPostal = txtCodigoPostal.Text,
            Ciudad = txtCiudad.Text,
            EntidadFederativa = txtEstado.Text,
            RFC = txtRfc.Text,
            Correo = txtCorreo.Text,
            TelefonoCasa = txtTelCasa.Text,
            TelefonoCelular = txtTelCelular.Text,
            FechaNacimiento = dtpNacimiento.Value.Date
        };
    }

    // Si un administrador cambió la ciudad o el estado de un código postal ya registrado, pregunta si se
    // corrige el catálogo. Devuelve false si el usuario cancela.
    private bool ConfirmarCodigoPostal(Cliente cliente, out bool corregir)
    {
        corregir = false;
        if (!_esAdministrador)
        {
            return true;
        }

        CodigoPostalInfo? registrado = _clienteService.ObtenerCodigoPostal(cliente.CodigoPostal.Trim());
        if (registrado is null ||
            (string.Equals(registrado.Ciudad, cliente.Ciudad.Trim(), StringComparison.OrdinalIgnoreCase) &&
             string.Equals(registrado.EntidadFederativa, cliente.EntidadFederativa.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        string mensaje = $"El código postal {registrado.CodigoPostal} está registrado como {registrado.Ciudad}, {registrado.EntidadFederativa}.\n\n" +
                         $"¿Corregirlo a {cliente.Ciudad.Trim()}, {cliente.EntidadFederativa.Trim()}? El cambio aplica a todos los clientes con ese código postal " +
                         "(a cada uno se le registra como modificación suya) y solo se guarda si el cliente también se guarda.";
        if (!Mensajes.Confirmar(mensaje))
        {
            return false;
        }

        corregir = true;
        return true;
    }

    private void btnRegistrar_Click(object? sender, EventArgs e)
    {
        try
        {
            Cliente cliente = LeerFormulario();
            if (!Mensajes.Confirmar("¿Registrar este cliente?"))
            {
                return;
            }

            if (!ConfirmarCodigoPostal(cliente, out bool corregirCp))
            {
                return;
            }

            _clienteService.RegistrarCliente(cliente, corregirCp);
            Mensajes.Informacion("Cliente registrado correctamente.");
            LimpiarFormulario();
            CargarClientes();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible registrar el cliente.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private void btnActualizar_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_idSeleccionado <= 0)
            {
                Mensajes.Aviso("Seleccione un cliente de la lista.");
                return;
            }

            if (_seleccionado is { Activo: false })
            {
                Mensajes.Aviso("El cliente está dado de baja. Reactívelo para modificarlo.");
                return;
            }

            Cliente cliente = LeerFormulario();
            if (!Mensajes.Confirmar("¿Guardar los cambios del cliente seleccionado?"))
            {
                return;
            }

            if (!ConfirmarCodigoPostal(cliente, out bool corregirCp))
            {
                return;
            }

            _clienteService.ActualizarCliente(cliente, corregirCp);
            Mensajes.Informacion("Cliente actualizado correctamente.");
            LimpiarFormulario();
            CargarClientes();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible actualizar el cliente.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private void btnEliminar_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_idSeleccionado <= 0)
            {
                Mensajes.Aviso("Seleccione un cliente de la lista.");
                return;
            }

            if (!Mensajes.Confirmar("¿Está seguro de dar de baja al cliente seleccionado?"))
            {
                return;
            }

            if (_seleccionado is { Activo: false })
            {
                Mensajes.Aviso("El cliente ya está dado de baja.");
                return;
            }

            _clienteService.EliminarCliente(_idSeleccionado);
            Mensajes.Informacion("Cliente dado de baja; se conservan sus ventas e historial.");
            LimpiarFormulario();
            CargarClientes();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible dar de baja al cliente.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private void dgvClientes_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvClientes.Rows[e.RowIndex].DataBoundItem is not Cliente cliente)
        {
            return;
        }

        _idSeleccionado = cliente.IdCliente;
        _seleccionado = cliente;
        txtNombre.Text = cliente.Nombre;
        txtPaterno.Text = cliente.ApellidoPaterno;
        txtMaterno.Text = cliente.ApellidoMaterno;
        txtCalle.Text = cliente.Calle;
        txtNumExterior.Text = cliente.NumeroExterior;
        txtColonia.Text = cliente.Colonia;
        txtCodigoPostal.Text = cliente.CodigoPostal;
        txtCiudad.Text = cliente.Ciudad;
        txtEstado.Text = cliente.EntidadFederativa;
        txtRfc.Text = cliente.RFC;
        txtCorreo.Text = cliente.Correo;
        txtTelCasa.Text = cliente.TelefonoCasa;
        txtTelCelular.Text = cliente.TelefonoCelular;
        dtpNacimiento.Value = cliente.FechaNacimiento > dtpNacimiento.MaxDate ? dtpNacimiento.MaxDate : cliente.FechaNacimiento;
        lblAuditoria.Text = $"{cliente.DescripcionRegistro}{Environment.NewLine}{cliente.DescripcionModificacion}";
    }

    private void btnReactivar_Click(object? sender, EventArgs e)
    {
        if (_seleccionado is null)
        {
            Mensajes.Aviso("Marque \"Mostrar dados de baja\" y seleccione un cliente dado de baja.");
            return;
        }

        try
        {
            if (!Mensajes.Confirmar($"¿Reactivar al cliente {_seleccionado.NombreParaMostrar}?"))
            {
                return;
            }

            _clienteService.ReactivarCliente(_seleccionado);
            Mensajes.Informacion("Cliente reactivado correctamente.");
            LimpiarFormulario();
            CargarClientes();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible reactivar al cliente.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private void chkInactivos_CheckedChanged(object? sender, EventArgs e)
    {
        LimpiarFormulario();
        CargarClientes();
    }

    private void LimpiarFormulario()
    {
        _idSeleccionado = 0;
        _seleccionado = null;
        txtNombre.Clear();
        txtPaterno.Clear();
        txtMaterno.Clear();
        txtCalle.Clear();
        txtNumExterior.Clear();
        txtColonia.Clear();
        txtCodigoPostal.Clear();
        txtCiudad.Clear();
        txtEstado.Clear();
        txtRfc.Clear();
        txtCorreo.Clear();
        txtTelCasa.Clear();
        txtTelCelular.Clear();
        txtCiudad.ReadOnly = false;
        txtEstado.ReadOnly = false;
        _ciudadDelCatalogo = false;
        dtpNacimiento.Value = new DateTime(1990, 1, 1);
        lblAuditoria.Text = string.Empty;
        dgvClientes.ClearSelection();
        txtNombre.Focus();
    }

    private void btnBuscar_Click(object? sender, EventArgs e)
    {
        // Al cambiar la lista se olvida el cliente que estaba seleccionado
        LimpiarFormulario();
        CargarClientes();
    }

    private void btnLimpiar_Click(object? sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void FrmClientes_Load(object? sender, EventArgs e)
    {
        CargarClientes();
    }
}
