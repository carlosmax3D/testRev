using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class FacturaService : IFacturaService
{
    private readonly IFacturaRepository _repository;
    private readonly ISesionUsuario _sesion;

    public FacturaService(IFacturaRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public Factura? ObtenerFactura(string folioVenta)
    {
        if (!Validador.FolioValido(folioVenta, out Guid folio))
        {
            throw new ArgumentException("El folio no es válido. Revíselo e intente de nuevo.");
        }

        if (_sesion.UsuarioActual is null)
        {
            throw new UnauthorizedAccessException("Inicie sesión para consultar facturas.");
        }

        return _repository.ObtenerPorFolioVenta(folio, _sesion.UsuarioActual.IdUsuario);
    }

    public void ExportarPdf(Factura factura, string ruta)
    {
        DocumentoPdf pdf = new();

        pdf.Escribir($"Factura {factura.FolioFactura}", 16, negrita: true);
        pdf.Escribir($"Fecha y hora de emisión: {Formato.FechaHora(factura.FechaHoraEmision)}");
        pdf.Escribir($"Folio de la compra: {factura.FolioVenta}");
        pdf.Escribir($"Método de pago: {factura.MetodoPago}    Estado: {factura.Estado}");
        pdf.Espacio(10);

        pdf.Escribir("Emisor", negrita: true);
        pdf.Escribir($"{factura.EmisorRazonSocial}    RFC: {factura.EmisorRFC}");
        pdf.Escribir(factura.EmisorDomicilio);
        pdf.Espacio(10);

        pdf.Escribir("Receptor", negrita: true);
        pdf.Escribir($"{factura.ReceptorNombre}    RFC: {factura.ReceptorRFC}");
        pdf.Escribir(factura.ReceptorDomicilio);
        pdf.Espacio(10);

        pdf.Escribir("Detalle", negrita: true);
        foreach (DetalleFactura renglon in factura.Detalle)
        {
            pdf.Escribir($"{renglon.Partido} - {renglon.Estadio}");
            pdf.Escribir($"    Zona {renglon.Zona}, asientos {renglon.Asientos}    Cantidad: {renglon.Cantidad}    " +
                $"Precio unitario: {Formato.Moneda(renglon.PrecioUnitario)}    Importe: {Formato.Moneda(renglon.Importe)}");
        }

        pdf.Espacio(10);
        pdf.Escribir($"Subtotal: {Formato.Moneda(factura.Subtotal)}");
        pdf.Escribir($"IVA ({factura.TasaIva * 100:0.##}%): {Formato.Moneda(factura.Iva)}");
        pdf.Escribir($"Total: {Formato.Moneda(factura.Total)}", negrita: true);

        pdf.Guardar(ruta);
    }
}
