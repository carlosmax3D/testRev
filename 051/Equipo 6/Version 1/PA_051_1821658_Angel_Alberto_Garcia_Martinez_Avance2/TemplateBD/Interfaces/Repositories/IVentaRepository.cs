using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IVentaRepository
{
    List<Zona> ConsultarZonasDisponibles(int idPartido, int idUsuario);

    List<Boleto> ConsultarAsientosDisponibles(int idPartido, int idZona, int cantidad, int idUsuario);

    decimal ObtenerTasaIva();

    Guid Registrar(Venta venta, int idUsuario);
}
