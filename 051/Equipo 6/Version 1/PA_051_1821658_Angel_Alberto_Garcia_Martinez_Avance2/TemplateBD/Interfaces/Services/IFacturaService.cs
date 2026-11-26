using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IFacturaService
{
    Factura? ObtenerFactura(string folioVenta);

    void ExportarPdf(Factura factura, string ruta);
}
