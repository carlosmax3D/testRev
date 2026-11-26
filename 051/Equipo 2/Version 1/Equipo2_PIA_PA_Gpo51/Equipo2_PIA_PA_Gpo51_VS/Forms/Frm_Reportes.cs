using System.Data;
using System.Drawing.Printing;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;

namespace TemplateCSharp.Forms;

public class FrmReportes : Form
{
    private readonly Ejecutor _ejecutor;
    private TabControl tabReportes = new() { Dock = DockStyle.Fill };
    private TabPage tabVentas = new("1. Reporte de Ventas");
    private TabPage tabOcupacion = new("2. Ocupación y Disponibilidad");
    private TabPage tabHistorial = new("3. Historial del Cliente");

    private DateTimePicker dtpInicio = new() { Left = 95, Top = 15, Width = 120, Format = DateTimePickerFormat.Custom, CustomFormat = "dd-MMM-yyyy" };
    private DateTimePicker dtpFin = new() { Left = 285, Top = 15, Width = 120, Format = DateTimePickerFormat.Custom, CustomFormat = "dd-MMM-yyyy" };
    private ComboBox cmbEstadioRep = new() { Left = 480, Top = 15, Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
    private ComboBox cmbPartidoRep = new() { Left = 95, Top = 50, Width = 310, DropDownStyle = ComboBoxStyle.DropDownList };
    private Button btnFiltrarVentas = new() { Text = "Consultar Ventas", Left = 480, Top = 48, Width = 160, Height = 28, BackColor = Color.LightSkyBlue };
    private DataGridView dgvVentas = new() { Left = 15, Top = 90, Width = 830, Height = 280, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private Label lblTotalesVentas = new() { Left = 15, Top = 382, Width = 630, Height = 25, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkGreen, Text = "Total Boletos: 0 | Ingreso Total: $0.00" };
    private Button btnReimprimirPDF = new() { Text = "Exportar Factura PDF", Left = 665, Top = 378, Width = 180, Height = 30, BackColor = Color.LightGreen, Font = new Font("Segoe UI", 9, FontStyle.Bold) };

    private ComboBox cmbPartidoOcup = new() { Left = 80, Top = 15, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
    private TextBox txtZonaOcup = new() { Left = 440, Top = 15, Width = 130, PlaceholderText = "Nombre zona..." };
    private CheckBox chkSoloDisp = new() { Text = "Solo disponibles", Left = 585, Top = 17, Width = 120 };
    private Button btnFiltrarOcup = new() { Text = "Consultar", Left = 715, Top = 13, Width = 120, Height = 28, BackColor = Color.LightSkyBlue };
    private DataGridView dgvOcupacion = new() { Left = 15, Top = 55, Width = 830, Height = 315, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private Label lblTotalVendidosPartido = new() { Left = 15, Top = 380, Width = 830, Height = 25, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkBlue, Text = "Total boletos vendidos en el filtro: 0" };

    private TextBox txtBuscarApellido = new() { Left = 130, Top = 15, Width = 180, PlaceholderText = "Apellidos del cliente..." };
    private Button btnBuscarCliHist = new() { Text = "Buscar Cliente", Left = 320, Top = 13, Width = 110, Height = 27 };
    private ComboBox cmbOrdenHist = new() { Left = 560, Top = 15, Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
    private DataGridView dgvClientesHist = new() { Left = 15, Top = 50, Width = 830, Height = 90, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private Label lblDatosCliHist = new() { Left = 15, Top = 148, Width = 830, Height = 40, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.WhiteSmoke, Padding = new Padding(4), Text = "Seleccione un cliente para ver sus datos y compras." };
    private DataGridView dgvHistorial = new() { Left = 15, Top = 195, Width = 830, Height = 210, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

    private DataTable dtEstadios = new();
    private DataTable dtPartidos = new();

    public FrmReportes(Ejecutor ejecutor)
    {
        _ejecutor = ejecutor;
        ConfigurarInterfaz();
        this.Load += (s, e) => InicializarReportes();
    }

    private void ConfigurarInterfaz()
    {
        this.Text = "Módulo de Reportes del Sistema";
        this.Size = new Size(880, 490);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        tabVentas.Controls.AddRange(new Control[] {
            new Label() { Text = "Fecha Inicio:", Left = 15, Top = 18, Width = 75 }, dtpInicio,
            new Label() { Text = "Fecha Fin:", Left = 220, Top = 18, Width = 65 }, dtpFin,
            new Label() { Text = "Estadio:", Left = 420, Top = 18, Width = 55 }, cmbEstadioRep,
            new Label() { Text = "Partido:", Left = 15, Top = 53, Width = 75 }, cmbPartidoRep,
            btnFiltrarVentas, dgvVentas, lblTotalesVentas, btnReimprimirPDF
        });

        tabOcupacion.Controls.AddRange(new Control[] {
            new Label() { Text = "Partido:", Left = 15, Top = 18, Width = 60 }, cmbPartidoOcup,
            new Label() { Text = "Zona:", Left = 395, Top = 18, Width = 40 }, txtZonaOcup,
            chkSoloDisp, btnFiltrarOcup, dgvOcupacion, lblTotalVendidosPartido
        });

        cmbOrdenHist.Items.AddRange(new string[] { "Fecha de Compra (Antiguo -> Reciente)", "Fecha de Partido (Pendientes -> Realizados)" });
        cmbOrdenHist.SelectedIndex = 0;

        tabHistorial.Controls.AddRange(new Control[] {
            new Label() { Text = "Buscar Apellidos:", Left = 15, Top = 18, Width = 110 }, txtBuscarApellido, btnBuscarCliHist,
            new Label() { Text = "Ordenar por:", Left = 475, Top = 18, Width = 80 }, cmbOrdenHist,
            dgvClientesHist, lblDatosCliHist, dgvHistorial
        });

        tabReportes.TabPages.AddRange(new TabPage[] { tabVentas, tabOcupacion, tabHistorial });
        Controls.Add(tabReportes);

        btnFiltrarVentas.Click += (s, e) => ConsultarVentas();
        btnReimprimirPDF.Click += BtnReimprimirPDF_Click;
        btnFiltrarOcup.Click += (s, e) => ConsultarOcupacion();
        btnBuscarCliHist.Click += (s, e) => BuscarClientesHist();
        dgvClientesHist.SelectionChanged += (s, e) => CargarHistorialClienteSeleccionado();
        cmbOrdenHist.SelectedIndexChanged += (s, e) => CargarHistorialClienteSeleccionado();
    }

    private void InicializarReportes()
    {
        bool esAdmin = SesionActual.Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase);
        if (!esAdmin)
        {
            tabReportes.TabPages.Remove(tabOcupacion);
            tabReportes.TabPages.Remove(tabHistorial);
        }

        dtpInicio.Value = DateTime.Now.AddMonths(-1);
        dtpFin.Value = DateTime.Now.AddDays(1);

        CargarFiltrosDinamicos();
        ConsultarVentas();
        if (esAdmin)
        {
            ConsultarOcupacion();
            BuscarClientesHist();
        }
    }

    private void CargarFiltrosDinamicos()
    {
        dtPartidos = _ejecutor.ConsultarSP("sp_ConsultarPartidosDisponibles");
        cmbPartidoRep.Items.Clear();
        cmbPartidoOcup.Items.Clear();
        cmbPartidoRep.Items.Add("-- Todos los Partidos --");
        cmbPartidoOcup.Items.Add("-- Todos los Partidos --");

        foreach (DataRow r in dtPartidos.Rows)
        {
            string texto = $"{r["Partido"]} ({r["Fecha"]})";
            cmbPartidoRep.Items.Add(texto);
            cmbPartidoOcup.Items.Add(texto);
        }
        cmbPartidoRep.SelectedIndex = 0;
        cmbPartidoOcup.SelectedIndex = 0;

        dtEstadios = _ejecutor.ConsultarSP("sp_ConsultarEstadios");
        cmbEstadioRep.Items.Clear();
        cmbEstadioRep.Items.Add("-- Todos --");
        foreach (DataRow r in dtEstadios.Rows)
        {
            cmbEstadioRep.Items.Add(r["Nombre"].ToString() ?? "");
        }
        cmbEstadioRep.SelectedIndex = 0;
    }

    private void ConsultarVentas()
    {
        if (dtpInicio.Value.Date > dtpFin.Value.Date)
        {
            MessageBox.Show("La fecha de inicio no puede ser mayor a la fecha fin.", "Rango de Fechas Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        int idEstadio = cmbEstadioRep.SelectedIndex > 0 ? Convert.ToInt32(dtEstadios.Rows[cmbEstadioRep.SelectedIndex - 1]["ID_Estadio"]) : 0;
        int idPartido = cmbPartidoRep.SelectedIndex > 0 ? Convert.ToInt32(dtPartidos.Rows[cmbPartidoRep.SelectedIndex - 1]["ID_Partido"]) : 0;

        SqlParameter[] param = {
            new SqlParameter("@FechaInicio", dtpInicio.Value.Date),
            new SqlParameter("@FechaFin", dtpFin.Value.Date),
            new SqlParameter("@ID_Estadio", idEstadio),
            new SqlParameter("@ID_Partido", idPartido)
        };

        DataTable dt = _ejecutor.ConsultarSP("sp_ReporteVentas", param);
        dgvVentas.DataSource = dt;

        if (dgvVentas.Columns.Contains("Subtotal")) dgvVentas.Columns["Subtotal"].Visible = false;
        if (dgvVentas.Columns.Contains("Total")) dgvVentas.Columns["Total"].Visible = false;
        if (dgvVentas.Columns.Contains("FolioVenta")) dgvVentas.Columns["FolioVenta"].HeaderText = "Folio";
        if (dgvVentas.Columns.Contains("FolioGUID")) dgvVentas.Columns["FolioGUID"].HeaderText = "Código GUID";
        if (dgvVentas.Columns.Contains("FechaHoraVenta")) dgvVentas.Columns["FechaHoraVenta"].HeaderText = "Fecha y Hora";
        if (dgvVentas.Columns.Contains("FechaPartido")) dgvVentas.Columns["FechaPartido"].HeaderText = "Fecha Partido";
        if (dgvVentas.Columns.Contains("BoletosVendidos")) dgvVentas.Columns["BoletosVendidos"].HeaderText = "Boletos";
        if (dgvVentas.Columns.Contains("SubtotalFmt")) dgvVentas.Columns["SubtotalFmt"].HeaderText = "Subtotal";
        if (dgvVentas.Columns.Contains("TotalFmt")) dgvVentas.Columns["TotalFmt"].HeaderText = "Total";
        if (dgvVentas.Columns.Contains("MetodoPago")) dgvVentas.Columns["MetodoPago"].HeaderText = "Método Pago";

        int totalBoletos = 0;
        decimal ingresoTotal = 0m;
        foreach (DataRow r in dt.Rows)
        {
            totalBoletos += Convert.ToInt32(r["BoletosVendidos"]);
            ingresoTotal += Convert.ToDecimal(r["Total"]);
        }
        lblTotalesVentas.Text = $"Total Boletos: {totalBoletos}   |   Ingreso Total: {ingresoTotal:C2}";
    }

    private void BtnReimprimirPDF_Click(object? sender, EventArgs e)
    {
        if (dgvVentas.CurrentRow == null)
        {
            MessageBox.Show("Seleccione una venta de la tabla para exportar su factura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int folioVenta = Convert.ToInt32(dgvVentas.CurrentRow.Cells["FolioVenta"].Value);
        SqlParameter[] param = { new SqlParameter("@FolioVenta", folioVenta) };
        DataTable dtFac = _ejecutor.ConsultarSP("sp_ObtenerDatosFactura", param);
        if (dtFac.Rows.Count > 0)
        {
            DataRow r = dtFac.Rows[0];
            int folioFac = Convert.ToInt32(r["ID_FolioFactura"]);
            string textoFactura =
            $"FACTURA ELECTRÓNICA - VENTA DE BOLETOS ({r["EstadoFactura"]})\n" +
            $"------------------------------------------------------------\n" +
            $"EMISOR: {r["EmisorNombre"]} | RFC: {r["EmisorRFC"]}\n" +
            $"Domicilio: {r["EmisorDomicilio"]}\n" +
            $"------------------------------------------------------------\n" +
            $"Folio Factura: FAC-{folioFac:D6}\n" +
            $"Código Compra (GUID): {r["FolioGUID"]}\n" +
            $"Fecha Emisión: {r["FechaEmision"]}\n" +
            $"Método de Pago: {r["MetodoPago"]}\n" +
            $"------------------------------------------------------------\n" +
            $"RECEPTOR: {r["ReceptorNombre"]}\n" +
            $"RFC: {r["ReceptorRFC"]} | Domicilio: {r["ReceptorDomicilio"]}\n" +
            $"------------------------------------------------------------\n" +
            $"DETALLE:\n" +
            $"Partido: {r["Partido"]} ({r["FechaPartido"]})\n" +
            $"Estadio: {r["Estadio"]} | Zona: {r["Zona"]}\n" +
            $"Asiento inicial: {r["Asiento"]} | Cantidad: {r["Cantidad"]}\n" +
            $"Precio Unitario: {Convert.ToDecimal(r["PrecioUnitario"]):C2}\n" +
            $"Subtotal: {Convert.ToDecimal(r["Subtotal"]):C2}\n" +
            $"IVA (16%): {Convert.ToDecimal(r["IVA"]):C2}\n" +
            $"TOTAL PAGADO: {Convert.ToDecimal(r["Total"]):C2}\n" +
            $"------------------------------------------------------------";
            string rutaPdf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Factura_Reimpresa_{folioFac}.pdf");
            using PrintDocument pd = new();
            pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            pd.PrinterSettings.PrintToFile = true;
            pd.PrinterSettings.PrintFileName = rutaPdf;
            pd.PrintPage += (s, ev) =>
            {
                ev.Graphics?.DrawString(textoFactura, new Font("Consolas", 10), Brushes.Black, new RectangleF(40, 40, 750, 1000));
            };
            pd.Print();
            MessageBox.Show($"Factura exportada en PDF en tu Escritorio:\nFactura_Reimpresa_{folioFac}.pdf", "PDF Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    private void ConsultarOcupacion()
    {
        int idPartido = cmbPartidoOcup.SelectedIndex > 0 ? Convert.ToInt32(dtPartidos.Rows[cmbPartidoOcup.SelectedIndex - 1]["ID_Partido"]) : 0;
        SqlParameter[] param = {
new SqlParameter("@ID_Partido", idPartido),
new SqlParameter("@NombreZona", txtZonaOcup.Text.Trim()),
new SqlParameter("@SoloDisponibles", chkSoloDisp.Checked ? 1 : 0)
};
        DataTable dt = _ejecutor.ConsultarSP("sp_ReporteOcupacion", param);
        dgvOcupacion.DataSource = dt;
        if (dgvOcupacion.Columns.Contains("PrecioBase"))
        {
            dgvOcupacion.Columns["PrecioBase"].HeaderText = "Precio Base";
            dgvOcupacion.Columns["PrecioBase"].DefaultCellStyle.Format = "C2";
        }
        if (dgvOcupacion.Columns.Contains("AsientosTotales")) dgvOcupacion.Columns["AsientosTotales"].HeaderText = "Asientos Totales";
        if (dgvOcupacion.Columns.Contains("AsientosVendidos")) dgvOcupacion.Columns["AsientosVendidos"].HeaderText = "Vendidos";
        if (dgvOcupacion.Columns.Contains("AsientosDisponibles")) dgvOcupacion.Columns["AsientosDisponibles"].HeaderText = "Disponibles";
        if (dgvOcupacion.Columns.Contains("PorcentajeOcupacion")) dgvOcupacion.Columns["PorcentajeOcupacion"].HeaderText = "% Ocupación";
        if (dgvOcupacion.Columns.Contains("ID_Partido")) dgvOcupacion.Columns["ID_Partido"].Visible = false;
        int totalVendidos = 0;
        foreach (DataRow r in dt.Rows)
        {
            totalVendidos += Convert.ToInt32(r["AsientosVendidos"]);
        }
        lblTotalVendidosPartido.Text = $"Cantidad de boletos vendidos en total: {totalVendidos}";
    }
    private void BuscarClientesHist()
    {
        SqlParameter[] param = { new SqlParameter("@Filtro", txtBuscarApellido.Text.Trim()) };
        dgvClientesHist.DataSource = _ejecutor.ConsultarSP("sp_BuscarCliente", param);

        if (dgvClientesHist.Columns.Contains("ID_Cliente")) dgvClientesHist.Columns["ID_Cliente"].Visible = false;
        if (dgvClientesHist.Columns.Contains("TelefonoCasa")) dgvClientesHist.Columns["TelefonoCasa"].HeaderText = "Tel. Casa";
        if (dgvClientesHist.Columns.Contains("TelefonoCel")) dgvClientesHist.Columns["TelefonoCel"].HeaderText = "Celular";
        if (dgvClientesHist.Columns.Contains("FechaNacimiento"))
        {
            dgvClientesHist.Columns["FechaNacimiento"].HeaderText = "Nacimiento";
            dgvClientesHist.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd-MMM-yyyy";
        }
    }

    private void CargarHistorialClienteSeleccionado()
    {
        if (dgvClientesHist.CurrentRow == null) return;
        DataGridViewRow row = dgvClientesHist.CurrentRow;
        int idCliente = Convert.ToInt32(row.Cells["ID_Cliente"].Value);
        lblDatosCliHist.Text = $"Cliente: {row.Cells["Nombre"].Value} {row.Cells["Apellidos"].Value} | RFC: {row.Cells["RFC"].Value} | Correo: {row.Cells["Email"].Value} | Tel: {row.Cells["TelefonoCel"].Value}";
        string orden = cmbOrdenHist.SelectedIndex == 0 ? "FechaCompra" : "FechaPartido";
        SqlParameter[] param = {
new SqlParameter("@ID_Cliente", idCliente),
new SqlParameter("@OrdenarPor", orden)
};
        dgvHistorial.DataSource = _ejecutor.ConsultarSP("sp_ReporteHistorialCliente", param);
        if (dgvHistorial.Columns.Contains("CodigoCompra")) dgvHistorial.Columns["CodigoCompra"].HeaderText = "Código Compra (GUID)";
        if (dgvHistorial.Columns.Contains("FechaCompra")) dgvHistorial.Columns["FechaCompra"].HeaderText = "Fecha Compra";
        if (dgvHistorial.Columns.Contains("FechaPartido")) dgvHistorial.Columns["FechaPartido"].HeaderText = "Fecha Partido";
        if (dgvHistorial.Columns.Contains("Boletos")) dgvHistorial.Columns["Boletos"].HeaderText = "Boletos";
        if (dgvHistorial.Columns.Contains("Subtotal")) dgvHistorial.Columns["Subtotal"].HeaderText = "Subtotal";
        if (dgvHistorial.Columns.Contains("Total")) dgvHistorial.Columns["Total"].HeaderText = "Total";
        if (dgvHistorial.Columns.Contains("MetodoPago")) dgvHistorial.Columns["MetodoPago"].HeaderText = "Método Pago";
        if (dgvHistorial.Columns.Contains("Factura")) dgvHistorial.Columns["Factura"].HeaderText = "Factura";

    }
}