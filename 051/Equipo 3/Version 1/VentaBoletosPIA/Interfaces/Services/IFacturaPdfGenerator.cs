using VentaBoletosApp.Models.Dtos;

namespace VentaBoletosApp.Interfaces.Services;

public interface IFacturaPdfGenerator
{
    void Generar(FacturaCompleta factura, string rutaArchivo);
}
