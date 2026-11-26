using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Models.Dtos;
using VentaBoletosApp.Models.Lookups;

namespace VentaBoletosApp.Services;

public class VentaService : ServicioBase, IVentaService
{
    public const int MaximoBoletosPorTransaccion = 4;
    public const decimal TasaIva = 0.16m;

    private readonly IVentaRepository _ventas;
    private readonly IFacturaRepository _facturas;
    private readonly IClienteRepository _clientes;
    private readonly IPartidoRepository _partidos;
    private readonly IAsientoRepository _asientos;
    private readonly ICatalogoRepository _catalogos;

    public VentaService(
        IVentaRepository ventas,
        IFacturaRepository facturas,
        IClienteRepository clientes,
        IPartidoRepository partidos,
        IAsientoRepository asientos,
        ICatalogoRepository catalogos,
        ISesionActual sesion) : base(sesion)
    {
        _ventas = ventas;
        _facturas = facturas;
        _clientes = clientes;
        _partidos = partidos;
        _asientos = asientos;
        _catalogos = catalogos;
    }

    public ResumenCompra CalcularResumen(IEnumerable<decimal> preciosBoletos)
    {
        List<decimal> precios = preciosBoletos.ToList();

        return new ResumenCompra
        {
            CantidadBoletos = precios.Count,
            Subtotal = precios.Sum(),
            IVA = precios.Sum(p => Math.Round(p * TasaIva, 2, MidpointRounding.AwayFromZero))
        };
    }

    public List<Asiento> ObtenerAsientosDisponibles(int idZona, int idPartido)
    {
        ExigirSesion();

        if (idZona <= 0 || idPartido <= 0)
        {
            throw new ArgumentException("Seleccione un partido y una zona.");
        }

        return _asientos.GetDisponiblesPorZonaPartido(idZona, idPartido);
    }

    public ResultadoVenta RegistrarVenta(SolicitudVenta solicitud)
    {
        ExigirSesion();

        if (solicitud is null)
        {
            throw new ArgumentNullException(nameof(solicitud));
        }

        if (!Validador.RfcValido(solicitud.RfcCliente))
        {
            throw new ArgumentException("Seleccione al cliente que realiza la compra.");
        }

        Cliente cliente = _clientes.GetByRFC(solicitud.RfcCliente.Trim().ToUpperInvariant())
            ?? throw new ArgumentException("El cliente seleccionado no existe.");

        if (!cliente.Activo)
        {
            throw new InvalidOperationException("El cliente seleccionado está dado de baja.");
        }

        Partido partido = _partidos.GetById(solicitud.IdPartido)
            ?? throw new ArgumentException("Seleccione un partido.");

        bool disponible =
            string.Equals(partido.EstadoPartido, "Programado", StringComparison.OrdinalIgnoreCase)
            && partido.FechaHora >= DateTime.Now;

        if (!disponible)
        {
            throw new InvalidOperationException("El partido seleccionado ya no está disponible para la venta de boletos.");
        }

        List<int> ids = solicitud.IdsAsiento ?? [];

        if (ids.Count == 0)
        {
            throw new ArgumentException("Seleccione al menos un asiento.");
        }

        if (ids.Count > MaximoBoletosPorTransaccion)
        {
            throw new ArgumentException($"Solo se permiten {MaximoBoletosPorTransaccion} boletos por transacción.");
        }

        if (ids.Distinct().Count() != ids.Count)
        {
            throw new ArgumentException("No se puede seleccionar el mismo asiento más de una vez.");
        }

        CatalogoItem metodo = _catalogos.ObtenerMetodosPago().FirstOrDefault(m => m.Id == solicitud.IdMetodoPago)
            ?? throw new ArgumentException("Seleccione un método de pago.");

        bool conTarjeta =
            string.Equals(metodo.Nombre, "Credito", StringComparison.OrdinalIgnoreCase)
            || string.Equals(metodo.Nombre, "Debito", StringComparison.OrdinalIgnoreCase);

        if (conTarjeta)
        {
            string? error = Validador.ValidarTarjeta(solicitud.Tarjeta);

            if (error is not null)
            {
                throw new ArgumentException(error);
            }
        }

        (int idVenta, Guid folio) = _ventas.Registrar(metodo.Id, Sesion.IdUsuario, partido.IdPartido, cliente.RFC, ids);

        Venta venta = _ventas.GetByFolio(folio)
            ?? throw new InvalidOperationException("La venta se registró pero no pudo consultarse.");

        Factura? factura = _facturas.GetByVenta(idVenta);

        return new ResultadoVenta
        {
            IdVenta = idVenta,
            Folio = folio,
            FolioFactura = factura?.Folio,
            Resumen = new ResumenCompra
            {
                CantidadBoletos = ids.Count,
                Subtotal = venta.Subtotal,
                IVA = venta.IVA
            }
        };
    }

    public Venta? BuscarPorFolio(string folioTexto)
    {
        ExigirSesion();

        if (string.IsNullOrWhiteSpace(folioTexto))
        {
            throw new ArgumentException("Capture el folio de la compra.");
        }

        if (!Guid.TryParse(folioTexto.Trim(), out Guid folio))
        {
            throw new ArgumentException("El folio de compra no tiene un formato válido.");
        }

        Venta? venta = _ventas.GetByFolio(folio);

        if (venta is not null)
        {
            venta.Detalles = _ventas.GetDetallesPorVenta(venta.IdVenta);
        }

        return venta;
    }
}
