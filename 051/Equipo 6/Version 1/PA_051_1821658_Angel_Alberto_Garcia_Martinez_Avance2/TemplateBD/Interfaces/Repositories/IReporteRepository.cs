using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IReporteRepository
{
    ReporteVentas ConsultarVentas(DateTime fechaInicial, DateTime fechaFinal, int? idEstadio, int? idPartido, int idUsuario);

    List<Estadio> ConsultarEstadiosFiltro(int idUsuario);

    List<Partido> ConsultarPartidosFiltro(int idUsuario);

    ReporteOcupacion ConsultarOcupacion(int? idPartido, int? idZona, bool soloDisponibles, int idAdministrador);

    List<ResumenVenta> ConsultarHistorialCliente(int idCliente, bool ordenarPorPartido, int idAdministrador);
}
