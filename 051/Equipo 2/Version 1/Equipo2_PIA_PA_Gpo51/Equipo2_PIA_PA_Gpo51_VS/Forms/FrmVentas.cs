using System.Data;
using System.Drawing.Printing;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;

namespace TemplateCSharp.Forms;

public class FrmVentas : Form
{
    private readonly Ejecutor _ejecutor;

    // 1. Búsqueda, Alta Rápida y Datos Completos del Cliente
    private TextBox txtBuscarCliente = new() { Left = 20, Top = 35, Width = 210, PlaceholderText = "Apellido o correo..." };
    private Button btnBuscarCliente = new() { Text = "Buscar", Left = 238, Top = 33, Width = 70 };
    private Button btnNuevoCliente = new() { Text = "+ Nuevo Cliente", Left = 315, Top = 33, Width = 135, BackColor = Color.LightYellow };
    private DataGridView dgvClientes = new() { Left = 20, Top = 68, Width = 430, Height = 110, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private Label lblInfoCliente = new() { Left = 20, Top = 185, Width = 430, Height = 95, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.WhiteSmoke, Padding = new Padding(6), Text = "Seleccione un cliente de la lista para ver sus datos completos." };

    private int _idClienteSeleccionado = 0;
    private string _nombreClienteSel = "";
    private string _rfcClienteSel = "";
    private string _domicilioClienteSel = "";

    // 2. Búsqueda, Información Completa de Partido y Boletos
    private TextBox txtBuscarPartido = new() { Left = 480, Top = 35, Width = 270, PlaceholderText = "Buscar por equipo, estadio o fecha..." };
    private Button btnBuscarPartido = new() { Text = "Filtrar", Left = 760, Top = 33, Width = 80 };
    private ComboBox cmbPartidos = new() { Left = 480, Top = 68, Width = 360, DropDownStyle = ComboBoxStyle.DropDownList };
    private Label lblInfoPartido = new() { Left = 480, Top = 98, Width = 360, Height = 36, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.AliceBlue, Padding = new Padding(4), Text = "Información del partido..." };

    private ComboBox cmbZonas = new() { Left = 480, Top = 158, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
    private NumericUpDown numCantidad = new() { Left = 750, Top = 158, Width = 90, Minimum = 1, Maximum = 4, Value = 1 };

    // 3. Método de Pago y Datos de Tarjeta
    private ComboBox cmbMetodoPago = new() { Left = 480, Top = 210, Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
    private Panel pnlTarjeta = new() { Left = 480, Top = 242, Width = 360, Height = 95, BorderStyle = BorderStyle.FixedSingle, Visible = false };
    private TextBox txtTitular = new() { Left = 10, Top = 20, Width = 200, PlaceholderText = "Nombre del Titular" };
    private TextBox txtNumTarjeta = new() { Left = 220, Top = 20, Width = 125, MaxLength = 16, PlaceholderText = "16 dígitos" };
    private DateTimePicker dtpExp = new() { Left = 10, Top = 62, Width = 130, Format = DateTimePickerFormat.Custom, CustomFormat = "MM/yyyy" };
    private TextBox txtCCV = new() { Left = 160, Top = 62, Width = 60, MaxLength = 4, PlaceholderText = "CCV" };

    // 4. Totales y Procesamiento
    private Label lblSubtotal = new() { Left = 20, Top = 300, Width = 220, Text = "Subtotal: $0.00", Font = new Font("Segoe UI", 10, FontStyle.Bold) };
    private Label lblIVA = new() { Left = 20, Top = 325, Width = 220, Text = "IVA (16%): $0.00", Font = new Font("Segoe UI", 10) };
    private Label lblTotal = new() { Left = 20, Top = 350, Width = 250, Text = "Total: $0.00", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.DarkGreen };
    private Button btnProcesarVenta = new() { Text = "Confirmar Compra y Generar Factura PDF", Left = 20, Top = 395, Width = 280, Height = 42, BackColor = Color.LightGreen, Font = new Font("Segoe UI", 9, FontStyle.Bold) };

    // 5. Panel de Cancelación (Solo visible para Administrador)
    private GroupBox grpCancelacion = new() { Text = "Cancelación de Venta (Solo Administrador)", Left = 480, Top = 345, Width = 360, Height = 125 };
    private TextBox txtGuidCancelar = new() { Left = 15, Top = 38, Width = 330, PlaceholderText = "Pegue el Código GUID de compra..." };
    private TextBox txtMotivoCancelacion = new() { Left = 15, Top = 68, Width = 210, PlaceholderText = "Motivo de cancelación..." };
    private Button btnCancelarVenta = new() { Text = "Cancelar Venta", Left = 235, Top = 66, Width = 110, Height = 27, BackColor = Color.LightCoral };

    private DataTable dtPartidos = new();
    private DataTable dtZonas = new();

    public FrmVentas(Ejecutor ejecutor)
    {
        _ejecutor = ejecutor;
        ConfigurarInterfaz();
        this.Load += (s, e) => InicializarDatos();
    }

    private void ConfigurarInterfaz()
    {
        this.Text = "Gestión de Venta de Boletos y Facturación";
        this.Size = new Size(880, 520);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        cmbMetodoPago.Items.AddRange(new string[] { "Efectivo", "Crédito", "Débito" });
        cmbMetodoPago.SelectedIndex = 0;

        pnlTarjeta.Controls.AddRange(new Control[] {
            new Label() { Text = "Titular:", Left = 10, Top = 3, Width = 100 }, txtTitular,
            new Label() { Text = "No. Tarjeta:", Left = 220, Top = 3, Width = 100 }, txtNumTarjeta,
            new Label() { Text = "Expiración:", Left = 10, Top = 45, Width = 100 }, dtpExp,
            new Label() { Text = "CCV:", Left = 160, Top = 45, Width = 60 }, txtCCV
        });

        grpCancelacion.Controls.AddRange(new Control[] {
            new Label() { Text = "Código de Compra (GUID):", Left = 15, Top = 18, Width = 200 },
            txtGuidCancelar, txtMotivoCancelacion, btnCancelarVenta
        });

        Controls.AddRange(new Control[] {
            new Label() { Text = "1. Cliente (Apellido/Correo o Nuevo):", Left = 20, Top = 15, Width = 250, Font = new Font("Segoe UI", 9, FontStyle.Bold) },
            txtBuscarCliente, btnBuscarCliente, btnNuevoCliente, dgvClientes, lblInfoCliente,
            new Label() { Text = "2. Buscar y Seleccionar Partido Disponible:", Left = 480, Top = 15, Width = 300, Font = new Font("Segoe UI", 9, FontStyle.Bold) },
            txtBuscarPartido, btnBuscarPartido, cmbPartidos, lblInfoPartido,
            new Label() { Text = "Zona Disponible:", Left = 480, Top = 139, Width = 150 },
            new Label() { Text = "Boletos (Máx 4):", Left = 750, Top = 139, Width = 100 },
            cmbZonas, numCantidad,
            new Label() { Text = "3. Método de Pago:", Left = 480, Top = 190, Width = 150, Font = new Font("Segoe UI", 9, FontStyle.Bold) },
            cmbMetodoPago, pnlTarjeta,
            lblSubtotal, lblIVA, lblTotal, btnProcesarVenta, grpCancelacion
        });

        btnBuscarCliente.Click += (s, e) => BuscarClientes(txtBuscarCliente.Text.Trim());
        btnNuevoCliente.Click += (s, e) => { using FrmClientes fc = new(_ejecutor); fc.ShowDialog(); BuscarClientes(""); };
        dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;
        btnBuscarPartido.Click += (s, e) => CargarPartidos(txtBuscarPartido.Text.Trim());
        cmbPartidos.SelectedIndexChanged += CmbPartidos_SelectedIndexChanged;
        cmbZonas.SelectedIndexChanged += (s, e) => CalcularTotales();
        numCantidad.ValueChanged += (s, e) => CalcularTotales();
        cmbMetodoPago.SelectedIndexChanged += (s, e) => pnlTarjeta.Visible = cmbMetodoPago.Text != "Efectivo";
        btnProcesarVenta.Click += BtnProcesarVenta_Click;
        btnCancelarVenta.Click += BtnCancelarVenta_Click;
    }

    private void InicializarDatos()
    {
        grpCancelacion.Visible = SesionActual.Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase);
        BuscarClientes("");
        CargarPartidos("");
    }

    private void BuscarClientes(string filtro)
    {
        SqlParameter[] param = { new SqlParameter("@Filtro", filtro) };
        DataTable dt = _ejecutor.ConsultarSP("sp_BuscarCliente", param);
        dgvClientes.DataSource = dt;

        if (dgvClientes.Columns.Contains("ID_Cliente")) dgvClientes.Columns["ID_Cliente"].Visible = false;
        if (dgvClientes.Columns.Contains("TelefonoCasa")) dgvClientes.Columns["TelefonoCasa"].HeaderText = "Tel. Casa";
        if (dgvClientes.Columns.Contains("TelefonoCel")) dgvClientes.Columns["TelefonoCel"].HeaderText = "Celular";
        if (dgvClientes.Columns.Contains("FechaNacimiento")) dgvClientes.Columns["FechaNacimiento"].HeaderText = "Nacimiento";
        if (dgvClientes.Columns.Contains("FechaNacimiento"))
        {
            dgvClientes.Columns["FechaNacimiento"].HeaderText = "Nacimiento";
            dgvClientes.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd-MMM-yyyy";
        }
    }

    private void DgvClientes_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvClientes.CurrentRow == null) return;

        DataGridViewRow row = dgvClientes.CurrentRow;
        _idClienteSeleccionado = Convert.ToInt32(row.Cells["ID_Cliente"].Value);
        _nombreClienteSel = $"{row.Cells["Nombre"].Value} {row.Cells["Apellidos"].Value}";
        _rfcClienteSel = row.Cells["RFC"].Value?.ToString() ?? "";
        _domicilioClienteSel = row.Cells["Domicilio"].Value?.ToString() ?? "";

        lblInfoCliente.Text = $"CLIENTE SELECCIONADO:\n" +
                              $"Nombre: {_nombreClienteSel}  |  RFC: {_rfcClienteSel}\n" +
                              $"Correo: {row.Cells["Email"].Value}  |  Celular: {row.Cells["TelefonoCel"].Value}\n" +
                              $"Domicilio: {_domicilioClienteSel}  |  Nac: {row.Cells["FechaNacimiento"].Value}";
    }

    private void CargarPartidos(string filtro)
    {
        SqlParameter[] param = { new SqlParameter("@Filtro", filtro) };
        dtPartidos = _ejecutor.ConsultarSP("sp_ConsultarPartidosDisponibles", param);
        cmbPartidos.Items.Clear();

        foreach (DataRow r in dtPartidos.Rows)
        {
            cmbPartidos.Items.Add($"{r["Partido"]} | {r["Estadio"]} ({r["Fecha"]} {r["Hora"]})");
        }

        if (cmbPartidos.Items.Count > 0)
        {
            cmbPartidos.SelectedIndex = 0;
        }
        else
        {
            cmbZonas.Items.Clear();
            lblInfoPartido.Text = "No hay partidos disponibles con ese filtro.";
            CalcularTotales();
        }
    }
    private void CmbPartidos_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbPartidos.SelectedIndex < 0) return;
        DataRow rPart = dtPartidos.Rows[cmbPartidos.SelectedIndex];
        int idEstadio = Convert.ToInt32(rPart["ID_Estadio"]);
        SqlParameter[] param = { new SqlParameter("@ID_Estadio", idEstadio) };
        dtZonas = _ejecutor.ConsultarSP("sp_ObtenerZonasPorEstadio", param);
        cmbZonas.Items.Clear();
        foreach (DataRow r in dtZonas.Rows)
        {
            decimal precio = Convert.ToDecimal(r["PrecioBase"]);
            cmbZonas.Items.Add($"{r["NombreZona"]} - {precio:C2} (Disp: {r["CupoZona"]})");
        }
        lblInfoPartido.Text = $"Estadio: {rPart["Estadio"]} | Fecha: {rPart["Fecha"]} a las {rPart["Hora"]} hrs | Zonas disp: {dtZonas.Rows.Count}";
        if (cmbZonas.Items.Count > 0) cmbZonas.SelectedIndex = 0;
        CalcularTotales();
    }
    private decimal ObtenerPrecioSeleccionado()
    {
        if (cmbZonas.SelectedIndex < 0 || dtZonas.Rows.Count == 0) return 0m;
        return Convert.ToDecimal(dtZonas.Rows[cmbZonas.SelectedIndex]["PrecioBase"]);
    }
    private void CalcularTotales()
    {
        decimal precio = ObtenerPrecioSeleccionado();
        decimal subtotal = precio * numCantidad.Value;
        decimal iva = subtotal * 0.16m;
        decimal total = subtotal + iva;
        lblSubtotal.Text = $"Subtotal: {subtotal:C2}";
        lblIVA.Text = $"IVA (16%): {iva:C2}";
        lblTotal.Text = $"Total: {total:C2}";
    }
    private void BtnProcesarVenta_Click(object? sender, EventArgs e)
    {
        if (_idClienteSeleccionado == 0 || cmbPartidos.SelectedIndex < 0 || cmbZonas.SelectedIndex < 0)
        {
            MessageBox.Show("Seleccione un cliente, un partido y una zona válida.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int cupoDisponible = Convert.ToInt32(dtZonas.Rows[cmbZonas.SelectedIndex]["CupoZona"]);
        if (numCantidad.Value > cupoDisponible)
        {
            MessageBox.Show($"Solo quedan {cupoDisponible} boletos disponibles en esta zona.", "Cupo Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cmbMetodoPago.Text != "Efectivo")
        {
            bool tarjetaEsNumero = txtNumTarjeta.Text.Length == 16 && txtNumTarjeta.Text.All(char.IsDigit);
            bool ccvEsNumero = (txtCCV.Text.Length == 3 || txtCCV.Text.Length == 4) && txtCCV.Text.All(char.IsDigit);
            DateTime fechaExpiracion = new DateTime(dtpExp.Value.Year, dtpExp.Value.Month, 1).AddMonths(1).AddDays(-1);

            if (string.IsNullOrWhiteSpace(txtTitular.Text) || !tarjetaEsNumero || !ccvEsNumero)
            {
                MessageBox.Show("Ingrese un titular válido, 16 dígitos numéricos de tarjeta y 3 o 4 dígitos de CCV.", "Validación de Tarjeta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (fechaExpiracion < DateTime.Today)
            {
                MessageBox.Show("La tarjeta ingresada está vencida.", "Tarjeta Vencida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        decimal precio = ObtenerPrecioSeleccionado();
        decimal subtotal = precio * numCantidad.Value;
        decimal iva = subtotal * 0.16m;
        decimal total = subtotal + iva;
        int idPartido = Convert.ToInt32(dtPartidos.Rows[cmbPartidos.SelectedIndex]["ID_Partido"]);
        int idZona = Convert.ToInt32(dtZonas.Rows[cmbZonas.SelectedIndex]["ID_Zona"]);
        string nombreZona = dtZonas.Rows[cmbZonas.SelectedIndex]["NombreZona"].ToString() ?? "";
        SqlParameter[] paramVenta = {
new SqlParameter("@ID_Usuario", SesionActual.IdUsuario == 0 ? 1 : SesionActual.IdUsuario),
new SqlParameter("@Subtotal", subtotal),
new SqlParameter("@IVA", iva),
new SqlParameter("@Total", total),
new SqlParameter("@MetodoPago", cmbMetodoPago.Text),
new SqlParameter("@NombreTitular", cmbMetodoPago.Text == "Efectivo" ? DBNull.Value : txtTitular.Text.Trim()),
new SqlParameter("@NumTarjeta", cmbMetodoPago.Text == "Efectivo" ? DBNull.Value : txtNumTarjeta.Text.Trim()),
new SqlParameter("@FechaExp", cmbMetodoPago.Text == "Efectivo" ? DBNull.Value : dtpExp.Value.Date),
new SqlParameter("@CCV", cmbMetodoPago.Text == "Efectivo" ? DBNull.Value : txtCCV.Text.Trim())
};
        DataTable dtResVenta = _ejecutor.ConsultarSP("sp_RegistrarVenta", paramVenta);
        if (dtResVenta.Rows.Count > 0)
        {
            int folioNum = Convert.ToInt32(dtResVenta.Rows[0]["FolioGenerado"]);
            string folioGuid = dtResVenta.Rows[0]["FolioGUID"].ToString() ?? "";
            SqlParameter[] paramBoleto = {
new SqlParameter("@Folio", folioNum),
new SqlParameter("@ID_Cliente", _idClienteSeleccionado),
new SqlParameter("@ID_Partido", idPartido),
new SqlParameter("@ID_Zona", idZona),
new SqlParameter("@Cantidad", (int)numCantidad.Value)
};
            DataTable dtBoleto = _ejecutor.ConsultarSP("sp_RegistrarBoleto", paramBoleto);
            if (dtBoleto.Rows.Count > 0)
            {
                string asiento = dtBoleto.Rows[0]["AsientoAsignado"].ToString() ?? "A-1";
                string textoFactura =
                $"FACTURA ELECTRÓNICA - VENTA DE BOLETOS\n" +
                $"------------------------------------------------------------\n" +
                $"EMISOR: Boletos Monterrey S.A. de C.V. | RFC: BMO100101XYZ\n" +
                $"Domicilio: Av. Lincoln 1234, Monterrey, NL\n" +
                $"------------------------------------------------------------\n" +
                $"Folio Factura: FAC-{folioNum:D6}\n" +
                $"Código Compra (GUID): {folioGuid}\n" +
                $"Fecha Emisión: {DateTime.Now:dd-MMM-yyyy HH:mm}\n" +
                $"Método de Pago: {cmbMetodoPago.Text}\n" +
                $"------------------------------------------------------------\n" +
                $"RECEPTOR: {_nombreClienteSel}\n" +
                $"RFC: {_rfcClienteSel} | Domicilio: {_domicilioClienteSel}\n" +
                $"------------------------------------------------------------\n" +
                $"DETALLE:\n" +
                $"Partido: {cmbPartidos.Text}\n" +
                $"Zona: {nombreZona} | Asiento inicial: {asiento} | Cantidad: {numCantidad.Value}\n" +
                $"Precio Unitario: {precio:C2}\n" +
                $"Subtotal: {subtotal:C2}\n" +
                $"IVA (16%): {iva:C2}\n" +
                $"TOTAL PAGADO: {total:C2}\n" +
                $"------------------------------------------------------------";
                ExportarFacturaPDF(textoFactura, folioNum);
                MessageBox.Show($"¡Compra registrada con éxito!\nCódigo GUID: {folioGuid}\n\nLa factura en PDF se guardó en tu Escritorio.", "Venta Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTitular.Clear(); txtNumTarjeta.Clear(); txtCCV.Clear();
                CmbPartidos_SelectedIndexChanged(null, EventArgs.Empty);
            }
        }
    }
    private void ExportarFacturaPDF(string contenido, int folio)
    {
        string rutaPdf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Factura_{folio}.pdf");
        using PrintDocument pd = new();
        pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
        pd.PrinterSettings.PrintToFile = true;
        pd.PrinterSettings.PrintFileName = rutaPdf;
        pd.PrintPage += (s, ev) =>
        {
            ev.Graphics?.DrawString(contenido, new Font("Consolas", 10), Brushes.Black, new RectangleF(40, 40, 750, 1000));
        };
        pd.Print();
    }
    private void BtnCancelarVenta_Click(object? sender, EventArgs e)
    {
        if (!Guid.TryParse(txtGuidCancelar.Text.Trim(), out Guid codigoGuid) || string.IsNullOrWhiteSpace(txtMotivoCancelacion.Text))
        {
            MessageBox.Show("Ingrese un Código GUID válido y el motivo de la cancelación.", "Datos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (MessageBox.Show("¿Está seguro de cancelar esta venta? Se liberarán los asientos y se anulará la factura.", "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            SqlParameter[] param = {
new SqlParameter("@CodigoCompra", codigoGuid),
new SqlParameter("@ID_Usuario", SesionActual.IdUsuario == 0 ? 1 : SesionActual.IdUsuario),
new SqlParameter("@Motivo", txtMotivoCancelacion.Text.Trim())
};
            if (_ejecutor.Ejecutar("sp_CancelarVentaPorGUID", param))
            {
                MessageBox.Show("Venta cancelada, factura anulada y boletos liberados.", "Cancelación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGuidCancelar.Clear();
                txtMotivoCancelacion.Clear();
                CmbPartidos_SelectedIndexChanged(null, EventArgs.Empty);
            }
        }
    }
}