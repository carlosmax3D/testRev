using VentaBoletosApp.Models.Dtos;

namespace VentaBoletosApp.Interfaces.Services;

public interface IFacturaService
{
    /// <summary>Obtiene la factura completa de la venta con ese folio. Null si no existe.</summary>
    FacturaCompleta? ObtenerPorFolioVenta(string folioTexto);

    void ExportarPdf(FacturaCompleta factura, string rutaArchivo);
}
