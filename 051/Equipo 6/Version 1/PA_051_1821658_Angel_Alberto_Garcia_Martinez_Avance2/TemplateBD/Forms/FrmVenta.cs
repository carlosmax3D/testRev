using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Venta de boletos: cliente, partido, zona y cantidad; al pagar se guarda la venta con su factura
public partial class FrmVenta : Form
{
    private readonly IClienteService _clienteService;
    private readonly IPartidoService _partidoService;
    private readonly IVentaService _ventaService;
    private readonly IEstadioService _estadioService;
    private readonly IServiceProvider _serviceProvider;

    // Compra en captura
    private Venta _venta = new();

    // Estadio cuyo mapa se está mostrando (0 = ninguno)
    private int _idEstadioMapa;

    public FrmVenta(IClienteService clienteService, IPartidoService partidoService,
        IVentaService ventaService, IEstadioService estadioService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _clienteService = clienteService;
        _partidoService = partidoService;
        _ventaService = ventaService;
        _estadioService = estadioService;
        _serviceProvider = serviceProvider;
        dgvPartidos.AutoGenerateColumns = false;
        dgvCompra.AutoGenerateColumns = false;
        colPrecio.DefaultCellStyle.FormatProvider = Formato.Cultura;
        nudCantidad.Maximum = _ventaService.MaximoBoletos;
        lblCantidad.Text = $"Cantidad (máx. {_ventaService.MaximoBoletos})";
        MostrarCompra();
    }

    private void CargarPartidos()
    {
        try
        {
            dgvPartidos.DataSource = _partidoService.ObtenerPartidosDisponibles(txtBuscarPartido.Text);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }

        ActualizarPartidoSeleccionado();
    }

    // Al cambiar de partido se vacía la compra y se cargan las zonas con lugares libres y el mapa
    private void ActualizarPartidoSeleccionado()
    {
        Partido? partido = dgvPartidos.CurrentRow?.DataBoundItem as Partido;
        int idPartido = partido?.IdPartido ?? 0;
        if (idPartido == _venta.IdPartido)
        {
            return;
        }

        _venta.IdPartido = idPartido;
        _venta.Boletos.Clear();
        MostrarCompra();
        CargarZonas();
        MostrarMapa(partido);
    }

    // Mapa del estadio del partido, para que el cliente vea dónde queda cada zona
    private void MostrarMapa(Partido? partido)
    {
        int idEstadio = partido?.IdEstadio ?? 0;
        if (idEstadio == _idEstadioMapa)
        {
            return;
        }

        _idEstadioMapa = idEstadio;
        Image? anterior = picMapa.Image;
        picMapa.Image = null;
        anterior?.Dispose();
        btnAmpliarMapa.Enabled = false;

        if (partido is null)
        {
            lblMapaEstadio.Text = "Seleccione un partido para ver el mapa.";
            return;
        }

        lblMapaEstadio.Text = partido.EstadioCiudad;
        try
        {
            picMapa.Image = VisorMapa.CrearImagen(_estadioService.ObtenerMapa(idEstadio));
            btnAmpliarMapa.Enabled = true;
        }
        catch (Exception ex)
        {
            lblMapaEstadio.Text = $"{partido.EstadioCiudad}: {Errores.Mensaje(ex)}";
            // Permite reintentar la carga al cambiar de partido
            _idEstadioMapa = 0;
        }
    }

    private void AmpliarMapa(object? sender, EventArgs e)
    {
        if (picMapa.Image is not null)
        {
            VisorMapa.Mostrar(this, picMapa.Image, $"Mapa de {lblMapaEstadio.Text}");
        }
    }

    private void CargarZonas()
    {
        cboZona.DataSource = null;
        lblPrecio.Text = string.Empty;
        if (_venta.IdPartido == 0)
        {
            return;
        }

        try
        {
            cboZona.DisplayMember = nameof(Zona.NombreZona);
            cboZona.DataSource = _ventaService.ObtenerZonasDisponibles(_venta.IdPartido);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void MostrarCompra()
    {
        dgvCompra.DataSource = null;
        dgvCompra.DataSource = new List<Boleto>(_venta.Boletos);

        List<string> ubicaciones = [];
        foreach (Boleto boleto in _venta.Boletos)
        {
            ubicaciones.Add(boleto.Ubicacion);
        }

        txtAsientos.Text = string.Join(", ", ubicaciones);
        lblSubtotal.Text = Formato.Moneda(_venta.Subtotal);
        lblIva.Text = Formato.Moneda(_venta.Iva);
        lblTotal.Text = Formato.Moneda(_venta.Total);
    }

    private void BuscarCliente(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtBuscarCliente.Text))
        {
            Mensajes.Aviso("Escriba los apellidos o el correo del cliente.");
            return;
        }

        try
        {
            List<Cliente> clientes = _clienteService.ObtenerClientes(txtBuscarCliente.Text);
            cboClientes.DataSource = null;
            cboClientes.DisplayMember = nameof(Cliente.NombreParaMostrar);
            cboClientes.DataSource = clientes;
            if (clientes.Count == 0)
            {
                Mensajes.Aviso("No se encontró el cliente. Puede registrarlo con el botón \"Registrar cliente nuevo\".");
            }
        }
        catch (Exception ex)
        {
            Mensajes.Aviso($"No fue posible buscar al cliente.\n\n{Errores.Mensaje(ex)}");
        }
    }

    private void RegistrarClienteNuevo(object? sender, EventArgs e)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            FrmClientes formulario = scope.ServiceProvider.GetRequiredService<FrmClientes>();
            formulario.ShowDialog(this);
        }

        // Volver a buscar para que aparezca el cliente nuevo
        if (!string.IsNullOrWhiteSpace(txtBuscarCliente.Text))
        {
            BuscarCliente(sender, e);
        }
    }

    private void AgregarBoletos(object? sender, EventArgs e)
    {
        if (cboClientes.SelectedItem is not Cliente)
        {
            Mensajes.Aviso("Primero busque y seleccione un cliente.");
            return;
        }

        if (cboZona.SelectedItem is not Zona zona)
        {
            Mensajes.Aviso("Seleccione el partido y la zona.");
            return;
        }

        try
        {
            _ventaService.AgregarBoletos(_venta, zona, (int)nudCantidad.Value);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
            // La disponibilidad pudo cambiar por otra venta
            CargarZonas();
        }

        MostrarCompra();
    }

    private void ContinuarPago(object? sender, EventArgs e)
    {
        if (cboClientes.SelectedItem is not Cliente cliente)
        {
            Mensajes.Aviso("Primero busque y seleccione un cliente.");
            return;
        }

        if (_venta.Boletos.Count == 0)
        {
            Mensajes.Aviso("Agregue al menos un boleto a la compra.");
            return;
        }

        // Se vuelve a consultar la tasa de IVA para cobrar el mismo total que se registra
        decimal tasaMostrada = _venta.TasaIva;
        if (!CargarTasaIva())
        {
            return;
        }

        if (_venta.TasaIva != tasaMostrada)
        {
            MostrarCompra();
            Mensajes.Aviso("Se actualizó la tasa de IVA. Revise el total y vuelva a presionar el botón de pago.");
            return;
        }

        using (FrmPago pago = new(_venta.Total))
        {
            if (pago.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            _venta.MetodoPago = pago.Metodo;
        }

        _venta.IdCliente = cliente.IdCliente;

        Guid folio;
        try
        {
            folio = _ventaService.RegistrarVenta(_venta);
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
            CargarZonas();
            return;
        }

        string copiado = Portapapeles.CopiarFolio(folio) ? "\n\nEl folio se copió al portapapeles." : string.Empty;
        Mensajes.Informacion($"Venta registrada.\n\nFolio de compra: {folio}{copiado}");
        MostrarFactura(folio);
        NuevaVenta();
    }

    private void MostrarFactura(Guid folio)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        FrmFactura factura = scope.ServiceProvider.GetRequiredService<FrmFactura>();
        factura.CargarFactura(folio.ToString());
        factura.ShowDialog(this);
    }

    // Deja la pantalla lista para otra venta, con la disponibilidad actualizada
    private void NuevaVenta()
    {
        _venta = new Venta { TasaIva = _venta.TasaIva };
        cboClientes.DataSource = null;
        txtBuscarCliente.Clear();
        MostrarCompra();
        CargarZonas();
        CargarPartidos();
    }

    // Devuelve falso si no se pudo obtener la tasa; en ese caso no se permite cobrar
    private bool CargarTasaIva()
    {
        try
        {
            _venta.TasaIva = _ventaService.ObtenerTasaIva();
            lblIvaTexto.Text = $"IVA ({_venta.TasaIva * 100:0.##}%):";
            return true;
        }
        catch (Exception ex)
        {
            lblIvaTexto.Text = "IVA (no disponible):";
            Mensajes.Aviso($"No se pudo obtener la tasa de IVA; no se puede cobrar hasta obtenerla.\n\n{Errores.Mensaje(ex)}");
            return false;
        }
    }

    private void FrmVenta_Load(object? sender, EventArgs e)
    {
        CargarTasaIva();
        CargarPartidos();
    }

    private void btnBuscarPartido_Click(object? sender, EventArgs e)
    {
        CargarPartidos();
    }

    private void cboZona_SelectedIndexChanged(object? sender, EventArgs e)
    {
        lblPrecio.Text = cboZona.SelectedItem is Zona zona
            ? $"{Formato.Moneda(zona.PrecioBase)} c/u ({zona.Disponibles} libres)"
            : string.Empty;
    }

    private void btnCancelar_Click(object? sender, EventArgs e)
    {
        _venta.Boletos.Clear();
        MostrarCompra();
    }

    private void dgvPartidos_SelectionChanged(object? sender, EventArgs e)
    {
        ActualizarPartidoSeleccionado();
    }

    // El PictureBox no libera la imagen al cerrarse
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        Image? imagen = picMapa.Image;
        picMapa.Image = null;
        imagen?.Dispose();
        base.OnFormClosed(e);
    }
}
