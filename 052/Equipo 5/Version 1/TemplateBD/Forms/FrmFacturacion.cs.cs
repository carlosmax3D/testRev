using System;
using System.Windows.Forms;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmFacturacion : Form
{
    private readonly FacturaRepository _facturaRepo;

    public FrmFacturacion(FacturaRepository facturaRepo)
    {
        InitializeComponent();
        _facturaRepo = facturaRepo;
    }

    private void FrmFacturacion_Load(object sender, EventArgs e)
    {
        CargarValoresPorDefectoEmisor();
        CargarFacturas();
    }

    private void CargarValoresPorDefectoEmisor()
    {
        txtNomEmisor.Text = "Estadios & Eventos S.A. de C.V.";
        txtRfcEmisor.Text = "EEV101010ABC";
        txtDomEmisor.Text = "Av. Constitución 100, Monterrey, NL";
    }

    private void CargarFacturas()
    {
        try
        {
            dgvFacturas.DataSource = _facturaRepo.ObtenerTodas();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al consultar historial de facturas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnGenerar_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtFolioVenta.Text))
            {
                MessageBox.Show("Ingrese el Folio de Venta a facturar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNomReceptor.Text) || string.IsNullOrWhiteSpace(txtRfcReceptor.Text) || string.IsNullOrWhiteSpace(txtDomReceptor.Text))
            {
                MessageBox.Show("Ingrese el nombre, RFC y domicilio del receptor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Factura nuevaFactura = new()
            {
                FolioVenta = txtFolioVenta.Text.Trim(),
                NomEmisor = txtNomEmisor.Text.Trim(),
                RfcEmisor = txtRfcEmisor.Text.Trim(),
                DomEmisor = txtDomEmisor.Text.Trim(),
                NomReceptor = txtNomReceptor.Text.Trim(),
                RfcReceptor = txtRfcReceptor.Text.Trim(),
                DomReceptor = txtDomReceptor.Text.Trim()
            };

            _facturaRepo.RegistrarFactura(nuevaFactura);

            MessageBox.Show("Factura registrada y generada correctamente.", "Facturación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnLimpiar_Click(sender, e);
            CargarFacturas();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al generar factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtFolioVenta.Clear();
        txtNomReceptor.Clear();
        txtRfcReceptor.Clear();
        txtDomReceptor.Clear();
        CargarValoresPorDefectoEmisor();
        txtFolioVenta.Focus();
    }
}