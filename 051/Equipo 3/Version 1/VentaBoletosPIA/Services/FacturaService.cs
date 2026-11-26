using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Models.Dtos;

namespace VentaBoletosApp.Services;

public class FacturaService : ServicioBase, IFacturaService
{
    private readonly IVentaService _ventas;
    private readonly IFacturaRepository _facturas;
    private readonly IClienteRepository _clientes;
    private readonly IEmpresaRepository _empresa;
    private readonly IFacturaPdfGenerator _pdf;

    public FacturaService(
        IVentaService ventas,
        IFacturaRepository facturas,
        IClienteRepository clientes,
        IEmpresaRepository empresa,
        IFacturaPdfGenerator pdf,
        ISesionActual sesion) : base(sesion)
    {
        _ventas = ventas;
        _facturas = facturas;
        _clientes = clientes;
        _empresa = empresa;
        _pdf = pdf;
    }

    public FacturaCompleta? ObtenerPorFolioVenta(string folioTexto)
    {
        ExigirSesion();

        Venta? venta = _ventas.BuscarPorFolio(folioTexto);

        if (venta is null)
        {
            return null;
        }

        Factura? factura = _facturas.GetByVenta(venta.IdVenta);

        if (factura is null)
        {
            return null;
        }

        Cliente receptor = _clientes.GetByRFC(venta.RFC)
            ?? throw new InvalidOperationException("No se encontró el cliente de la factura.");

        Empresa emisor = _empresa.Obtener()
            ?? throw new InvalidOperationException("No hay datos del emisor configurados en la base de datos.");

        return new FacturaCompleta
        {
            Emisor = emisor,
            Receptor = receptor,
            Factura = factura,
            Venta = venta,
            Detalles = venta.Detalles
        };
    }

    public void ExportarPdf(FacturaCompleta factura, string rutaArchivo)
    {
        ExigirSesion();

        if (factura is null)
        {
            throw new ArgumentNullException(nameof(factura));
        }

        if (string.IsNullOrWhiteSpace(rutaArchivo))
        {
            throw new ArgumentException("Indique la ruta donde se guardará el PDF.");
        }

        _pdf.Generar(factura, rutaArchivo);
    }
}
