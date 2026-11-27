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

public partial class FrmReportes : Form
{
    private readonly IReporteService _reporteService;
    private readonly IEstadioService _estadioService;
    private readonly IPartidoService _partidoService;
    private readonly IClienteService _clienteService;
    private int _idClienteHistorial = 0;

    public FrmReportes(
        IReporteService reporteService,
        IEstadioService estadioService,
        IPartidoService partidoService,
        IClienteService clienteService)
    {
        InitializeComponent();
        _reporteService = reporteService;
        _estadioService = estadioService;
        _partidoService = partidoService;
        _clienteService = clienteService;
    }

    private void FrmReportes_Load(object sender, EventArgs e)
    {
        dtpInicio.Value = DateTime.Today.AddMonths(-1);
        dtpFin.Value = DateTime.Today;

        CargarFiltrosCatalogos();

        cmbOrdenHistorial.Items.Clear();
        cmbOrdenHistorial.Items.AddRange([
            "Fecha de compra (más lejana a más reciente)",
            "Fecha de partido (pendientes a ya realizados)"
        ]);
        cmbOrdenHistorial.SelectedIndex = 0;

        CargarReporteVentas();
        CargarReporteOcupacion();
    }

    private void CargarFiltrosCatalogos()
    {
        // Estadios con opción "Todos"
        List<Estadio> estadios = [new Estadio { IdEstadio = 0, Nombre = "-- Todos los estadios --" }];
        estadios.AddRange(_estadioService.ObtenerEstadios());
        cmbEstadioVentas.DataSource = estadios;
        cmbEstadioVentas.DisplayMember = "Nombre";
        cmbEstadioVentas.ValueMember = "IdEstadio";

        // Partidos con opción "Todos" (dos listas separadas para las dos pestañas)
        List<Partido> partidos1 = [new Partido { IdPartido = 0, Encuentro = "-- Todos los partidos --" }];
        partidos1.AddRange(_partidoService.ObtenerPartidos());
        cmbPartidoVentas.DataSource = partidos1;
        cmbPartidoVentas.DisplayMember = "Encuentro";
        cmbPartidoVentas.ValueMember = "IdPartido";

        List<Partido> partidos2 = [new Partido { IdPartido = 0, Encuentro = "-- Todos los partidos --" }];
        partidos2.AddRange(_partidoService.ObtenerPartidos());
        cmbPartidoOcupacion.DataSource = partidos2;
        cmbPartidoOcupacion.DisplayMember = "Encuentro";
        cmbPartidoOcupacion.ValueMember = "IdPartido";
    }

    // ==================== PESTAÑA 1: REPORTE DE VENTAS ====================
    private void btnFiltrarVentas_Click(object sender, EventArgs e)
    {
        CargarReporteVentas();
    }

    private void CargarReporteVentas()
    {
        try
        {
            int idEstadio = cmbEstadioVentas.SelectedValue is int est ? est : 0;
            int idPartido = cmbPartidoVentas.SelectedValue is int par ? par : 0;

            List<ReporteVentaItem> ventas = _reporteService.ConsultarVentas(
                dtpInicio.Value.Date, dtpFin.Value.Date, idEstadio, idPartido);

            dgvVentas.DataSource = null;
            dgvVentas.DataSource = ventas;

            if (dgvVentas.Columns["Subtotal"] != null)
                dgvVentas.Columns["Subtotal"].DefaultCellStyle.Format = "$#,##0.00";

            if (dgvVentas.Columns["Total"] != null)
                dgvVentas.Columns["Total"].DefaultCellStyle.Format = "$#,##0.00";

            decimal ingresoTotal = ventas.Sum(v => v.Total);
            int boletosTotales = ventas.Sum(v => v.CantidadBoletos);

            lblIngresoTotal.Text = $"Ingreso Total: {ingresoTotal:$#,##0.00}";
            lblTotalBoletosVentas.Text = $"Total Boletos Vendidos: {boletosTotales:N0}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Reporte de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    // ==================== PESTAÑA 2: OCUPACIÓN Y DISPONIBILIDAD ====================
    private void btnFiltrarOcupacion_Click(object sender, EventArgs e)
    {
        CargarReporteOcupacion();
    }

    private void CargarReporteOcupacion()
    {
        try
        {
            int idPartido = cmbPartidoOcupacion.SelectedValue is int par ? par : 0;
            string zona = txtZonaOcupacion.Text.Trim();
            bool soloDisp = chkSoloDisponibles.Checked;

            List<ReporteOcupacionItem> ocupacion = _reporteService.ConsultarOcupacion(idPartido, zona, soloDisp);

            dgvOcupacion.DataSource = null;
            dgvOcupacion.DataSource = ocupacion;

            if (dgvOcupacion.Columns["IdPartido"] != null)
                dgvOcupacion.Columns["IdPartido"].Visible = false;

            if (dgvOcupacion.Columns["PrecioBase"] != null)
                dgvOcupacion.Columns["PrecioBase"].DefaultCellStyle.Format = "$#,##0.00";

            if (dgvOcupacion.Columns["PorcentajeOcupacion"] != null)
                dgvOcupacion.Columns["PorcentajeOcupacion"].DefaultCellStyle.Format = "0.00'%'";

            int vendidosTotal = ocupacion.Sum(o => o.AsientosVendidos);
            lblTotalVendidosPartido.Text = idPartido > 0
                ? $"Boletos vendidos en este partido: {vendidosTotal:N0}"
                : $"Boletos vendidos (general): {vendidosTotal:N0}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Reporte de Ocupación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    // ==================== PESTAÑA 3: HISTORIAL DEL CLIENTE ====================
    private void btnBuscarClienteHist_Click(object sender, EventArgs e)
    {
        try
        {
            string apellidos = txtBuscarApellidos.Text.Trim();
            if (string.IsNullOrWhiteSpace(apellidos))
            {
                MessageBox.Show("Ingrese al menos un apellido para buscar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Cliente> coincidencias = _clienteService.ObtenerClientes(apellidos);
            dgvClientesHist.DataSource = null;
            dgvClientesHist.DataSource = coincidencias;

            if (dgvClientesHist.Columns["UsuarioAuditoria"] != null)
                dgvClientesHist.Columns["UsuarioAuditoria"].Visible = false;

            if (dgvClientesHist.Columns["FechaNacimiento"] != null)
                dgvClientesHist.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd-MMM-yyyy";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Búsqueda de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void dgvClientesHist_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        if (dgvClientesHist.Rows[e.RowIndex].DataBoundItem is Cliente cliente)
        {
            _idClienteHistorial = cliente.IdCliente;
            lblDatosCliente.Text =
                $"Cliente: {cliente.NombreCompleto} | RFC: {cliente.RFC} | " +
                $"Correo: {cliente.Correo} | Tel: {cliente.TelefonoCelular} | " +
                $"Nacimiento: {cliente.FechaNacimiento:dd-MMM-yyyy}";

            CargarHistorialCliente();
        }
    }

    private void cmbOrdenHistorial_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_idClienteHistorial > 0)
            CargarHistorialCliente();
    }

    private void CargarHistorialCliente()
    {
        try
        {
            int tipoOrden = cmbOrdenHistorial.SelectedIndex < 0 ? 0 : cmbOrdenHistorial.SelectedIndex;
            List<HistorialClienteItem> historial = _reporteService.ConsultarHistorialCliente(_idClienteHistorial, tipoOrden);

            dgvHistorialCliente.DataSource = null;
            dgvHistorialCliente.DataSource = historial;

            if (dgvHistorialCliente.Columns["Subtotal"] != null)
                dgvHistorialCliente.Columns["Subtotal"].DefaultCellStyle.Format = "$#,##0.00";

            if (dgvHistorialCliente.Columns["Total"] != null)
                dgvHistorialCliente.Columns["Total"].DefaultCellStyle.Format = "$#,##0.00";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Historial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
