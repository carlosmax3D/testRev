using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Models.Dtos;
using VentaBoletosApp.Models.Lookups;
using VentaBoletosApp.Services;
using VentaBoletosApp.UI;

namespace VentaBoletosApp.Forms;

/// <summary>Renglon del carrito de compra (un boleto).</summary>
public class CarritoItem
{
    public int IdAsiento { get; set; }
    public int IdZona { get; set; }
    public string NombreZona { get; set; } = string.Empty;
    public string DescripcionAsiento { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}

public partial class FrmVentaBoletos : Form
{
    private readonly IServiceProvider _servicios;
    private readonly IClienteService _clientes;
    private readonly IPartidoService _partidos;
    private readonly IEstadioService _estadios;
    private readonly IVentaService _ventas;
    private readonly ICatalogoService _catalogos;
    private readonly IFacturaService _facturas;

    private readonly BindingList<CarritoItem> _carrito = [];

    private int _idPartidoActual;
    private bool _cargandoAsientos;

    public FrmVentaBoletos(
        IServiceProvider servicios,
        IClienteService clientes,
        IPartidoService partidos,
        IEstadioService estadios,
        IVentaService ventas,
        ICatalogoService catalogos,
        IFacturaService facturas)
    {
        _servicios = servicios;
        _clientes = clientes;
        _partidos = partidos;
        _estadios = estadios;
        _ventas = ventas;
        _catalogos = catalogos;
        _facturas = facturas;

        InitializeComponent();

        Ui.Estilizar(dgvPartidos);
        Ui.Estilizar(dgvZonas);
        Ui.Estilizar(dgvCarrito);

        dgvCarrito.DataSource = _carrito;
        dtpFecha.Checked = false;

        cmbCliente.Format += (_, e) =>
            e.Value = e.ListItem is Cliente c ? Descripciones.DeCliente(c) : string.Empty;

        txtBuscarCliente.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarClientes();
                e.SuppressKeyPress = true;
            }
        };

        lstAsientos.ItemCheck += LstAsientos_ItemCheck;

        Load += (_, _) => CargaInicial();
    }

    // ---------- Eventos del diseñador ----------

    private void BtnBuscarCliente_Click(object? sender, EventArgs e) => BuscarClientes();

    private void BtnBuscarPartido_Click(object? sender, EventArgs e) => BuscarPartidos();

    private void DgvPartidos_SelectionChanged(object? sender, EventArgs e) => PartidoSeleccionado();

    private void DgvZonas_SelectionChanged(object? sender, EventArgs e) => CargarAsientos();

    private void CmbMetodo_SelectedIndexChanged(object? sender, EventArgs e) => ActualizarCamposTarjeta();

    private void BtnVaciar_Click(object? sender, EventArgs e)
    {
        _carrito.Clear();
        ActualizarResumen();
        CargarAsientos();
    }

    private void BtnLimpiar_Click(object? sender, EventArgs e) => ReiniciarVenta(true);

    // ============================================================ Datos

    private void CargaInicial()
    {
        Ui.Ejecutar(this, () =>
        {
            List<Estadio> estadios = [new Estadio { IdEstadio = 0, Nombre = "Todos los estadios" }];
            estadios.AddRange(_estadios.ObtenerEstadios());
            Ui.LlenarCombo(cmbEstadio, estadios, nameof(Estadio.Nombre), nameof(Estadio.IdEstadio));

            Ui.LlenarCombo(cmbMetodo, _catalogos.ObtenerMetodosPago(), nameof(CatalogoItem.Nombre), nameof(CatalogoItem.Id));
        });

        BuscarClientes(false);
        BuscarPartidos(false);
        ActualizarCamposTarjeta();
    }

    private void BuscarClientes(bool avisarSiVacio = true)
    {
        List<Cliente> resultado = [];

        if (!Ui.Ejecutar(this, () => resultado = _clientes.BuscarClientes(txtBuscarCliente.Text.Trim(), true)))
        {
            return;
        }

        cmbCliente.DataSource = null;
        cmbCliente.DataSource = resultado;
        cmbCliente.SelectedIndex = resultado.Count > 0 ? 0 : -1;

        if (resultado.Count == 0 && avisarSiVacio)
        {
            Ui.Aviso(this, "No se encontraron clientes. Puede registrarlo con el botón \"Nuevo cliente\".");
        }
    }

    private void BtnNuevoCliente_Click(object? sender, EventArgs e)
    {
        using FrmClientes form = _servicios.GetRequiredService<FrmClientes>();
        form.ShowDialog(this);
        BuscarClientes(false);
    }

    private int? EstadioFiltro =>
        cmbEstadio.SelectedValue is int id && id > 0 ? id : null;

    private void BuscarPartidos(bool avisarSiVacio = true)
    {
        DateTime? fecha = dtpFecha.Checked ? dtpFecha.Value.Date : null;
        List<Partido> lista = [];

        if (!Ui.Ejecutar(this, () => lista = _partidos.BuscarDisponiblesParaVenta(fecha, EstadioFiltro, txtEquipo.Text)))
        {
            return;
        }

        dgvPartidos.DataSource = lista;
        dgvPartidos.ClearSelection();
        _idPartidoActual = 0;
        dgvZonas.DataSource = null;
        lstAsientos.Items.Clear();
        _carrito.Clear();
        ActualizarResumen();

        if (lista.Count == 0 && avisarSiVacio)
        {
            Ui.Aviso(this, "No hay partidos disponibles con esos criterios.");
        }
    }

    private Partido? PartidoActual =>
        dgvPartidos.CurrentRow is { Selected: true } fila ? fila.DataBoundItem as Partido : null;

    private void PartidoSeleccionado()
    {
        Partido? partido = PartidoActual;

        if (partido is null || partido.IdPartido == _idPartidoActual)
        {
            return;
        }

        _idPartidoActual = partido.IdPartido;
        _carrito.Clear();
        ActualizarResumen();
        lstAsientos.Items.Clear();

        List<DisponibilidadZona> zonas = [];

        if (Ui.Ejecutar(this, () => zonas = _partidos.ObtenerDisponibilidad(partido.IdPartido)))
        {
            dgvZonas.DataSource = zonas;
            dgvZonas.ClearSelection();
        }
    }

    private DisponibilidadZona? ZonaActual =>
        dgvZonas.CurrentRow is { Selected: true } fila ? fila.DataBoundItem as DisponibilidadZona : null;

    private void CargarAsientos()
    {
        lstAsientos.Items.Clear();

        DisponibilidadZona? zona = ZonaActual;

        if (zona is null || _idPartidoActual == 0)
        {
            return;
        }

        List<Asiento> asientos = [];

        if (!Ui.Ejecutar(this, () => asientos = _ventas.ObtenerAsientosDisponibles(zona.IdZona, _idPartidoActual)))
        {
            return;
        }

        HashSet<int> enCarrito = _carrito.Select(c => c.IdAsiento).ToHashSet();

        _cargandoAsientos = true;
        lstAsientos.BeginUpdate();

        foreach (Asiento a in asientos.Where(a => !enCarrito.Contains(a.IdAsiento)))
        {
            lstAsientos.Items.Add(a);
        }

        lstAsientos.EndUpdate();
        _cargandoAsientos = false;
    }

    private void LstAsientos_ItemCheck(object? sender, ItemCheckEventArgs e)
    {
        if (_cargandoAsientos || e.NewValue != CheckState.Checked)
        {
            return;
        }

        if (_carrito.Count + lstAsientos.CheckedItems.Count + 1 > VentaService.MaximoBoletosPorTransaccion)
        {
            e.NewValue = e.CurrentValue;
            Ui.Aviso(this, $"Solo se permiten {VentaService.MaximoBoletosPorTransaccion} boletos por transacción.");
        }
    }

    private void BtnAgregar_Click(object? sender, EventArgs e)
    {
        DisponibilidadZona? zona = ZonaActual;

        if (zona is null)
        {
            Ui.Aviso(this, "Seleccione un partido y una zona.");
            return;
        }

        List<Asiento> marcados = lstAsientos.CheckedItems.Cast<Asiento>().ToList();

        if (marcados.Count == 0)
        {
            Ui.Aviso(this, "Marque al menos un asiento.");
            return;
        }

        foreach (Asiento a in marcados)
        {
            _carrito.Add(new CarritoItem
            {
                IdAsiento = a.IdAsiento,
                IdZona = zona.IdZona,
                NombreZona = zona.NombreZona,
                DescripcionAsiento = a.ToString(),
                Precio = zona.Precio
            });
        }

        ActualizarResumen();
        CargarAsientos();
    }

    private void BtnQuitar_Click(object? sender, EventArgs e)
    {
        if (dgvCarrito.CurrentRow?.DataBoundItem is CarritoItem item)
        {
            _carrito.Remove(item);
            ActualizarResumen();
            CargarAsientos();
        }
    }

    private void ActualizarResumen()
    {
        ResumenCompra resumen = _ventas.CalcularResumen(_carrito.Select(c => c.Precio));

        lblCantidad.Text = $"Boletos: {resumen.CantidadBoletos}";
        lblSubtotal.Text = $"Subtotal: {Formato.Moneda(resumen.Subtotal)}";
        lblIva.Text = $"IVA (16%): {Formato.Moneda(resumen.IVA)}";
        lblTotal.Text = $"Total: {Formato.Moneda(resumen.Total)}";
    }

    private bool MetodoRequiereTarjeta
    {
        get
        {
            string nombre = (cmbMetodo.SelectedItem as CatalogoItem)?.Nombre ?? string.Empty;

            return nombre.Equals("Credito", StringComparison.OrdinalIgnoreCase)
                   || nombre.Equals("Debito", StringComparison.OrdinalIgnoreCase);
        }
    }

    private void ActualizarCamposTarjeta()
    {
        bool activo = MetodoRequiereTarjeta;

        txtTitular.Enabled = activo;
        txtNumero.Enabled = activo;
        txtExpira.Enabled = activo;
        txtCcv.Enabled = activo;

        if (!activo)
        {
            LimpiarTarjeta();
        }
    }

    private void LimpiarTarjeta()
    {
        txtTitular.Clear();
        txtNumero.Clear();
        txtExpira.Clear();
        txtCcv.Clear();
    }

    // ============================================================ Compra

    private void BtnConfirmar_Click(object? sender, EventArgs e)
    {
        if (cmbCliente.SelectedItem is not Cliente cliente)
        {
            Ui.Aviso(this, "Seleccione al cliente que realiza la compra.");
            return;
        }

        if (_idPartidoActual == 0)
        {
            Ui.Aviso(this, "Seleccione un partido.");
            return;
        }

        if (_carrito.Count == 0)
        {
            Ui.Aviso(this, "Agregue al menos un boleto al carrito.");
            return;
        }

        if (cmbMetodo.SelectedItem is not CatalogoItem metodo)
        {
            Ui.Aviso(this, "Seleccione un método de pago.");
            return;
        }

        SolicitudVenta solicitud = new()
        {
            RfcCliente = cliente.RFC,
            IdPartido = _idPartidoActual,
            IdMetodoPago = metodo.Id,
            IdsAsiento = _carrito.Select(c => c.IdAsiento).ToList(),
            Tarjeta = MetodoRequiereTarjeta
                ? new DatosTarjeta
                {
                    Titular = txtTitular.Text,
                    Numero = txtNumero.Text,
                    Expiracion = txtExpira.Text,
                    CCV = txtCcv.Text
                }
                : null
        };

        ResumenCompra previo = _ventas.CalcularResumen(_carrito.Select(c => c.Precio));

        if (!Ui.Confirmar(this,
                $"Cliente: {cliente.NombreCompleto}\nBoletos: {previo.CantidadBoletos}\nTotal a pagar: {Formato.Moneda(previo.Total)}\n\n¿Confirmar la compra?"))
        {
            return;
        }

        ResultadoVenta? resultado = null;

        if (!Ui.Ejecutar(this, () => resultado = _ventas.RegistrarVenta(solicitud)))
        {
            // Los asientos pudieron ocuparse mientras tanto: se refrescan.
            CargarAsientos();
            return;
        }

        if (resultado is null)
        {
            return;
        }

        string mensaje =
            "¡Compra realizada con éxito!\n\n" +
            $"Folio de compra: {resultado.Folio}\n" +
            $"Folio de factura: {resultado.FolioFactura}\n" +
            $"Total: {Formato.Moneda(resultado.Resumen.Total)}\n\n" +
            "¿Desea guardar la factura en PDF?";

        bool quierePdf = Ui.Confirmar(this, mensaje, "Compra registrada");

        if (quierePdf)
        {
            ExportarFactura(resultado.Folio.ToString());
        }

        ReiniciarVenta(false);
    }

    private void ExportarFactura(string folio)
    {
        Ui.Ejecutar(this, () =>
        {
            FacturaCompleta? factura = _facturas.ObtenerPorFolioVenta(folio);

            if (factura is null)
            {
                throw new InvalidOperationException("No se encontró la factura de la compra.");
            }

            using SaveFileDialog dialogo = new()
            {
                Title = "Guardar factura",
                Filter = "Archivo PDF|*.pdf",
                FileName = $"Factura_{factura.Factura.Folio}.pdf"
            };

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                _facturas.ExportarPdf(factura, dialogo.FileName);
                Ui.Aviso(this, "Factura guardada correctamente.");
            }
        });
    }

    private void ReiniciarVenta(bool tambienCliente)
    {
        _carrito.Clear();
        ActualizarResumen();
        LimpiarTarjeta();
        lstAsientos.Items.Clear();

        // Se vuelve a consultar la disponibilidad del partido para reflejar lo vendido.
        int idPartido = _idPartidoActual;
        _idPartidoActual = 0;

        if (!tambienCliente && idPartido > 0)
        {
            _idPartidoActual = idPartido;
            Ui.Ejecutar(this, () => dgvZonas.DataSource = _partidos.ObtenerDisponibilidad(idPartido));
            dgvZonas.ClearSelection();
        }
        else
        {
            dgvZonas.DataSource = null;
            dgvPartidos.ClearSelection();
            txtBuscarCliente.Clear();
            BuscarClientes(false);
        }
    }
}
