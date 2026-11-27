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

public partial class FrmCancelaciones : Form
{
    private readonly IFacturaService _facturaService;

    public FrmCancelaciones(IFacturaService facturaService)
    {
        InitializeComponent();
        _facturaService = facturaService;
    }

    private void FrmCancelaciones_Load(object sender, EventArgs e)
    {
        CargarCompras();
    }

    private void CargarCompras()
    {
        try
        {
            dgvCompras.DataSource = null;
            dgvCompras.DataSource = _facturaService.ListarCompras();

            if (dgvCompras.Columns["Total"] != null)
                dgvCompras.Columns["Total"].DefaultCellStyle.Format = "$#,##0.00";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar compras: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvCompras_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        if (dgvCompras.Rows[e.RowIndex].DataBoundItem is CompraResumen item)
        {
            txtFolioCompra.Text = item.FolioCompra;
            CargarDetallePrevio(item.FolioCompra);
        }
    }

    private void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarDetallePrevio(txtFolioCompra.Text.Trim());
    }

    private void CargarDetallePrevio(string folio)
    {
        try
        {
            FacturaCompleta f = _facturaService.ConsultarFactura(folio);
            lblResumenCompra.Text =
                $"Cliente: {f.ReceptorNombre} | Partido: {f.Partido} | " +
                $"Total: {f.Total:$#,##0.00} | Estado: {f.EstadoCompra}";
        }
        catch (Exception ex)
        {
            lblResumenCompra.Text = "Sin compra seleccionada.";
            MessageBox.Show(ex.Message, "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnCancelarCompra_Click(object sender, EventArgs e)
    {
        try
        {
            string folio = txtFolioCompra.Text.Trim();
            string motivo = txtMotivo.Text.Trim();

            if (string.IsNullOrWhiteSpace(folio) || string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Ingrese el folio de compra y el motivo de cancelación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de cancelar esta compra? Se liberarán los boletos y se cancelará la factura vinculada.",
                "Confirmar Cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes) return;

            _facturaService.CancelarVenta(folio, motivo);

            MessageBox.Show(
                "Compra cancelada correctamente. Los asientos fueron liberados y la factura quedó invalidada.",
                "Cancelación Exitosa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtFolioCompra.Clear();
            txtMotivo.Clear();
            lblResumenCompra.Text = "Sin compra seleccionada.";
            CargarCompras();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error al cancelar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}