using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IVentaService
{
    int MaximoBoletos { get; }

    decimal ObtenerTasaIva();

    List<Zona> ObtenerZonasDisponibles(int idPartido);

    void AgregarBoletos(Venta venta, Zona zona, int cantidad);

    Guid RegistrarVenta(Venta venta);
}
