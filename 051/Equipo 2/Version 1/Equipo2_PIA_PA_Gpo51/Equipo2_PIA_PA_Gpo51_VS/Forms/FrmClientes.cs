using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;
using System.Text.RegularExpressions;

namespace TemplateCSharp.Forms;

public class FrmClientes : Form
{
    private readonly Ejecutor _ejecutor;
    private int _idClienteSeleccionado = 0;

    private TextBox txtNombre = new() { Left = 120, Top = 20, Width = 200 };
    private TextBox txtApellidos = new() { Left = 120, Top = 55, Width = 200 };
    private TextBox txtDomicilio = new() { Left = 120, Top = 90, Width = 200 };
    private TextBox txtRFC = new() { Left = 120, Top = 125, Width = 200, MaxLength = 13 };
    private TextBox txtEmail = new() { Left = 450, Top = 20, Width = 220 };
    private TextBox txtTelCasa = new() { Left = 450, Top = 55, Width = 220, MaxLength = 15 };
    private TextBox txtTelCel = new() { Left = 450, Top = 90, Width = 220, MaxLength = 15 };
    private DateTimePicker dtpFechaNac = new() { Left = 450, Top = 125, Width = 220, Format = DateTimePickerFormat.Custom, CustomFormat = "dd-MMM-yyyy" };

    private Button btnGuardar = new() { Text = "Registrar", Left = 20, Top = 170, Width = 105, Height = 32, BackColor = Color.LightGreen };
    private Button btnEditar = new() { Text = "Modificar", Left = 135, Top = 170, Width = 105, Height = 32, BackColor = Color.LightSkyBlue };
    private Button btnEliminar = new() { Text = "Eliminar", Left = 250, Top = 170, Width = 105, Height = 32, BackColor = Color.LightCoral };
    private Button btnLimpiar = new() { Text = "Limpiar", Left = 365, Top = 170, Width = 90, Height = 32 };

    private TextBox txtBuscar = new() { Left = 480, Top = 174, Width = 180, PlaceholderText = "Apellido o correo..." };
    private Button btnBuscar = new() { Text = "Buscar", Left = 670, Top = 172, Width = 90, Height = 30 };
    private DataGridView dgvClientes = new() { Left = 20, Top = 220, Width = 740, Height = 220, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

    public FrmClientes(Ejecutor ejecutor)
    {
        _ejecutor = ejecutor;
        ConfigurarInterfaz();
        this.Load += (s, e) => CargarClientes("");
    }

    private void ConfigurarInterfaz()
    {
        this.Text = "Gestión de Clientes - Venta de Boletos";
        this.Size = new Size(800, 500);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        Controls.AddRange(new Control[] {
            new Label() { Text = "Nombre:", Left = 20, Top = 23, Width = 90 }, txtNombre,
            new Label() { Text = "Apellidos:", Left = 20, Top = 58, Width = 90 }, txtApellidos,
            new Label() { Text = "Domicilio:", Left = 20, Top = 93, Width = 90 }, txtDomicilio,
            new Label() { Text = "RFC (12-13):", Left = 20, Top = 128, Width = 90 }, txtRFC,
            new Label() { Text = "Correo:", Left = 350, Top = 23, Width = 90 }, txtEmail,
            new Label() { Text = "Tel. Casa:", Left = 350, Top = 58, Width = 90 }, txtTelCasa,
            new Label() { Text = "Tel. Celular:", Left = 350, Top = 93, Width = 90 }, txtTelCel,
            new Label() { Text = "Nacimiento:", Left = 350, Top = 128, Width = 90 }, dtpFechaNac,
            btnGuardar, btnEditar, btnEliminar, btnLimpiar,
            txtBuscar, btnBuscar, dgvClientes
        });

        btnGuardar.Click += BtnGuardar_Click;
        btnEditar.Click += BtnEditar_Click;
        btnEliminar.Click += BtnEliminar_Click;
        btnLimpiar.Click += (s, e) => LimpiarCampos();
        btnBuscar.Click += (s, e) => CargarClientes(txtBuscar.Text.Trim());
        dgvClientes.CellClick += DgvClientes_CellClick;
    }

    private bool ValidarCampos()
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellidos.Text) ||
            string.IsNullOrWhiteSpace(txtDomicilio.Text) || string.IsNullOrWhiteSpace(txtRFC.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtTelCel.Text))
        {
            MessageBox.Show("Todos los campos obligatorios deben estar llenos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        string rfc = txtRFC.Text.Trim().ToUpper();
        if (!Regex.IsMatch(rfc, @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$"))
        {
            MessageBox.Show("El formato del RFC no es válido (Ej: ABCD900101XYZ).", "RFC Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        if (!ValidarCampos()) return;

        SqlParameter[] parametros = {
            new SqlParameter("@Nombre", txtNombre.Text.Trim()),
            new SqlParameter("@Apellidos", txtApellidos.Text.Trim()),
            new SqlParameter("@Domicilio", txtDomicilio.Text.Trim()),
            new SqlParameter("@RFC", txtRFC.Text.Trim().ToUpper()),
            new SqlParameter("@Email", txtEmail.Text.Trim()),
            new SqlParameter("@TelefonoCasa", txtTelCasa.Text.Trim()),
            new SqlParameter("@TelefonoCel", txtTelCel.Text.Trim()),
            new SqlParameter("@FechaNac", dtpFechaNac.Value.Date),
            new SqlParameter("@ID_UsuarioRegistro", SesionActual.IdUsuario == 0 ? 1 : SesionActual.IdUsuario)
        };

        if (_ejecutor.Ejecutar("sp_RegistrarCliente", parametros))
        {
            MessageBox.Show("Cliente registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
            CargarClientes("");
        }
    }

    private void BtnEditar_Click(object? sender, EventArgs e)
    {
        if (_idClienteSeleccionado == 0)
        {
            MessageBox.Show("Seleccione un cliente de la tabla para modificarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!ValidarCampos()) return;

        SqlParameter[] parametros = {
            new SqlParameter("@ID_Cliente", _idClienteSeleccionado),
            new SqlParameter("@Nombre", txtNombre.Text.Trim()),
            new SqlParameter("@Apellidos", txtApellidos.Text.Trim()),
            new SqlParameter("@Domicilio", txtDomicilio.Text.Trim()),
            new SqlParameter("@RFC", txtRFC.Text.Trim().ToUpper()),
            new SqlParameter("@Email", txtEmail.Text.Trim()),
            new SqlParameter("@TelefonoCasa", txtTelCasa.Text.Trim()),
            new SqlParameter("@TelefonoCel", txtTelCel.Text.Trim()),
            new SqlParameter("@FechaNac", dtpFechaNac.Value.Date),
            new SqlParameter("@ID_UsuarioModifica", SesionActual.IdUsuario == 0 ? 1 : SesionActual.IdUsuario)
        };

        if (_ejecutor.Ejecutar("sp_EditarCliente", parametros))
        {
            MessageBox.Show("Datos del cliente actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
            CargarClientes("");
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
        if (_idClienteSeleccionado == 0)
        {
            MessageBox.Show("Seleccione un cliente de la tabla para eliminarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            SqlParameter[] param = { new SqlParameter("@ID_Cliente", _idClienteSeleccionado) };
            if (_ejecutor.Ejecutar("sp_EliminarCliente", param))
            {
                MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarClientes("");
            }
        }
    }

    private void DgvClientes_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        DataGridViewRow row = dgvClientes.Rows[e.RowIndex];

        _idClienteSeleccionado = Convert.ToInt32(row.Cells["ID_Cliente"].Value);
        txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
        txtApellidos.Text = row.Cells["Apellidos"].Value?.ToString() ?? "";
        txtDomicilio.Text = row.Cells["Domicilio"].Value?.ToString() ?? "";
        txtRFC.Text = row.Cells["RFC"].Value?.ToString() ?? "";
        txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
        txtTelCasa.Text = row.Cells["TelefonoCasa"].Value?.ToString() ?? "";
        txtTelCel.Text = row.Cells["TelefonoCel"].Value?.ToString() ?? "";
        if (DateTime.TryParse(row.Cells["FechaNacimiento"].Value?.ToString(), out DateTime fn))
        {
            dtpFechaNac.Value = fn;
        }
    }

    private void LimpiarCampos()
    {
        _idClienteSeleccionado = 0;
        txtNombre.Clear();
        txtApellidos.Clear();
        txtDomicilio.Clear();
        txtRFC.Clear();
        txtEmail.Clear();
        txtTelCasa.Clear();
        txtTelCel.Clear();
        dtpFechaNac.Value = DateTime.Now.AddYears(-18);
    }

    private void CargarClientes(string filtro)
    {
        SqlParameter[] param = { new SqlParameter("@Filtro", filtro) };
        dgvClientes.DataSource = _ejecutor.ConsultarSP("sp_BuscarCliente", param);

        if (dgvClientes.Columns.Contains("ID_Cliente")) dgvClientes.Columns["ID_Cliente"].Visible = false;
        if (dgvClientes.Columns.Contains("TelefonoCasa")) dgvClientes.Columns["TelefonoCasa"].HeaderText = "Tel. Casa";
        if (dgvClientes.Columns.Contains("TelefonoCel")) dgvClientes.Columns["TelefonoCel"].HeaderText = "Tel. Celular";
        if (dgvClientes.Columns.Contains("FechaNacimiento")) dgvClientes.Columns["FechaNacimiento"].HeaderText = "Fecha Nacimiento";
    }
}
