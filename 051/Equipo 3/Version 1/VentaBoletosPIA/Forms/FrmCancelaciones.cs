using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Services;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

/// <summary>Cancelacion de compras por folio (solo administrador).</summary>
public partial class FrmCancelaciones : Form
{
    private readonly IVentaService _ventas;
    private readonly ICancelacionService _cancelaciones;

    private Venta? _venta;

    public FrmCancelaciones(IVentaService ventas, ICancelacionService cancelaciones)
    {
        _ventas = ventas;
        _cancelaciones = cancelaciones;

        InitializeComponent();
        Ui.Estilizar(dgvDetalle);
    }

    private void BtnBuscar_Click(object? sender, EventArgs e)
    {
        _venta = null;
        btnCancelar.Enabled = false;
        dgvDetalle.DataSource = null;
        lblInfo.Text = string.Empty;

        Venta? venta = null;

        if (!Ui.Ejecutar(this, () => venta = _ventas.BuscarPorFolio(txtFolio.Text)))
        {
            return;
        }

        if (venta is null)
        {
            Ui.Aviso(this, "No existe ninguna compra con ese folio.");
            return;
        }

        _venta = venta;
        dgvDetalle.DataSource = venta.Detalles;

        lblInfo.Text =
            $"Cliente: {venta.NombreCliente} ({venta.RFC})\n" +
            $"Partido: {venta.EquipoLocal} vs {venta.EquipoVisitante} - {Formato.FechaHora(venta.FechaPartido)} - {venta.NombreEstadio}\n" +
            $"Fecha de compra: {Formato.FechaHora(venta.FechaVenta)}   Método de pago: {venta.MetodoPago}\n" +
            $"Subtotal: {Formato.Moneda(venta.Subtotal)}   IVA: {Formato.Moneda(venta.IVA)}   Total: {Formato.Moneda(venta.Total)}\n" +
            $"Estado: {venta.EstadoVenta}";

        btnCancelar.Enabled = venta.EstaActiva;

        if (!venta.EstaActiva)
        {
            Ui.Aviso(this, "Esta compra ya se encuentra cancelada.");
        }
    }

    private void BtnCancelar_Click(object? sender, EventArgs e)
    {
        if (_venta is null)
        {
            return;
        }

        string folio = _venta.Folio.ToString();

        if (!Ui.Confirmar(this, "Se cancelará la compra, se liberarán sus asientos y la factura quedará cancelada.\n\n¿Desea continuar?"))
        {
            return;
        }

        if (Ui.Ejecutar(this, () => _cancelaciones.CancelarVenta(folio, txtMotivo.Text)))
        {
            Ui.Aviso(this, "La compra fue cancelada correctamente.");
            txtMotivo.Clear();
            BtnBuscar_Click(null, EventArgs.Empty);
        }
    }
}
