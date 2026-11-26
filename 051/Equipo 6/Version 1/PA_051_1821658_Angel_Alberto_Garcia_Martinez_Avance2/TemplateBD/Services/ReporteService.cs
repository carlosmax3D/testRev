using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

// Los reportes no se guardan en la base: se calculan con las ventas registradas
public class ReporteService : IReporteService
{
    private readonly IReporteRepository _repository;
    private readonly ISesionUsuario _sesion;

    public ReporteService(IReporteRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public ReporteVentas ObtenerReporteVentas(DateTime fechaInicial, DateTime fechaFinal, int? idEstadio, int? idPartido)
    {
        if (fechaInicial.Date > fechaFinal.Date)
        {
            throw new ArgumentException("La fecha inicial no puede ser posterior a la fecha final.");
        }

        return _repository.ConsultarVentas(fechaInicial, fechaFinal, idEstadio, idPartido, ObtenerUsuario());
    }

    public List<Estadio> ObtenerEstadiosParaFiltro()
    {
        return _repository.ConsultarEstadiosFiltro(ObtenerUsuario());
    }

    public List<Partido> ObtenerPartidosParaFiltro()
    {
        return _repository.ConsultarPartidosFiltro(ObtenerUsuario());
    }

    public ReporteOcupacion ObtenerOcupacion(int? idPartido, int? idZona, bool soloDisponibles)
    {
        if (idZona is not null && idPartido is null)
        {
            throw new ArgumentException("Para filtrar por zona, seleccione primero un partido.");
        }

        return _repository.ConsultarOcupacion(idPartido, idZona, soloDisponibles, ObtenerAdministrador());
    }

    public List<ResumenVenta> ObtenerHistorialCliente(int idCliente, bool ordenarPorPartido)
    {
        if (idCliente <= 0)
        {
            throw new ArgumentException("Seleccione un cliente de la lista.");
        }

        return _repository.ConsultarHistorialCliente(idCliente, ordenarPorPartido, ObtenerAdministrador());
    }

    private int ObtenerUsuario()
    {
        if (_sesion.UsuarioActual is null)
        {
            throw new UnauthorizedAccessException("Inicie sesión para consultar reportes.");
        }

        return _sesion.UsuarioActual.IdUsuario;
    }

    private int ObtenerAdministrador()
    {
        if (_sesion.UsuarioActual == null || !_sesion.UsuarioActual.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un administrador puede consultar reportes.");
        }

        return _sesion.UsuarioActual.IdUsuario;
    }
}
