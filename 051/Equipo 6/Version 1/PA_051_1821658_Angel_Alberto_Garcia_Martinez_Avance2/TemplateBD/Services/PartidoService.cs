using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class PartidoService : IPartidoService
{
    // El filtro de partidos se aplica en memoria; no llega a la base
    private const int MaximoBusqueda = 60;

    private readonly IPartidoRepository _repository;
    private readonly ISesionUsuario _sesion;

    public PartidoService(IPartidoRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public int LongitudMaximaMotivo => Longitudes.MotivoCancelacionPartido;

    public List<Partido> ObtenerPartidos(string? busqueda)
    {
        return Filtrar(_repository.Consultar(ObtenerAdministrador()), busqueda);
    }

    public void RegistrarPartido(Partido partido)
    {
        ValidarPartido(partido);
        partido.IdPartido = _repository.Insertar(partido, ObtenerAdministrador());
    }

    public void ActualizarPartido(Partido partido)
    {
        if (partido.IdPartido <= 0)
        {
            throw new ArgumentException("Seleccione un partido de la lista.");
        }

        if (!partido.Activo)
        {
            throw new ArgumentException("El partido está eliminado. Reactívelo para modificarlo.");
        }

        if (partido.Estado != EstadosPartido.Programado)
        {
            throw new ArgumentException("Solo se puede modificar un partido programado que aún no se juega.");
        }

        ValidarPartido(partido);
        _repository.Actualizar(partido, ObtenerAdministrador());
    }

    public void EliminarPartido(Partido partido)
    {
        if (partido.IdPartido <= 0)
        {
            throw new ArgumentException("Seleccione un partido de la lista.");
        }

        if (!partido.Activo)
        {
            throw new ArgumentException("El partido ya está eliminado.");
        }

        if (partido.Estado == EstadosPartido.Cancelado)
        {
            throw new ArgumentException("Un partido cancelado no se elimina: se conserva como historial.");
        }

        if (partido.VentasVigentes > 0)
        {
            throw new ArgumentException("No se puede eliminar: el partido tiene ventas registradas. Puede cancelarlo.");
        }

        _repository.Eliminar(partido.IdPartido, ObtenerAdministrador());
    }

    public void ReactivarPartido(Partido partido)
    {
        if (partido.IdPartido <= 0)
        {
            throw new ArgumentException("Seleccione un partido de la lista.");
        }

        if (partido.Activo)
        {
            throw new ArgumentException("El partido ya está activo.");
        }

        if (partido.Estado != EstadosPartido.Programado)
        {
            throw new ArgumentException("Solo se puede reactivar un partido programado cuya fecha aún no pasa. Registre uno nuevo.");
        }

        _repository.Reactivar(partido.IdPartido, ObtenerAdministrador());
    }

    // Revisa estadio, equipos y fecha; quita los segundos de la hora
    private static void ValidarPartido(Partido partido)
    {
        if (partido.IdEstadio <= 0)
        {
            throw new ArgumentException("Seleccione el estadio.");
        }

        if (partido.IdEquipoLocal <= 0 || partido.IdEquipoVisitante <= 0)
        {
            throw new ArgumentException("Registre al menos dos equipos en el catálogo y selecciónelos.");
        }

        if (partido.IdEquipoLocal == partido.IdEquipoVisitante)
        {
            throw new ArgumentException("El equipo local y el visitante deben ser distintos.");
        }

        // La base guarda la hora sin segundos
        DateTime fecha = partido.FechaHora;
        partido.FechaHora = new DateTime(fecha.Year, fecha.Month, fecha.Day, fecha.Hour, fecha.Minute, 0);

        if (partido.FechaHora <= DateTime.Now)
        {
            throw new ArgumentException("La fecha y hora del partido deben ser posteriores a este momento.");
        }
    }

    public int CancelarPartido(Partido partido, string motivo)
    {
        if (partido.IdPartido <= 0)
        {
            throw new ArgumentException("Seleccione un partido de la lista.");
        }

        if (!partido.Activo)
        {
            throw new ArgumentException("El partido está eliminado; no se puede cancelar.");
        }

        if (partido.Estado != EstadosPartido.Programado)
        {
            throw new ArgumentException("Solo se puede cancelar un partido programado que aún no se juega.");
        }

        if (Validador.Vacio(motivo))
        {
            throw new ArgumentException("Debe escribir el motivo de la cancelación del partido.");
        }

        if (motivo.Trim().Length > Longitudes.MotivoCancelacionPartido)
        {
            throw new ArgumentException($"El motivo no puede superar los {Longitudes.MotivoCancelacionPartido} caracteres.");
        }

        if (!Validador.CaracteresValidos(motivo))
        {
            throw new ArgumentException("El motivo tiene caracteres que no se pueden guardar (emojis o símbolos especiales).");
        }

        return _repository.Cancelar(partido.IdPartido, motivo.Trim(), ObtenerAdministrador());
    }

    // Partidos programados, futuros y con asientos libres
    public List<Partido> ObtenerPartidosDisponibles(string? busqueda)
    {
        if (_sesion.UsuarioActual is null)
        {
            throw new UnauthorizedAccessException("Inicie sesión para vender boletos.");
        }

        return Filtrar(_repository.ConsultarDisponibles(_sesion.UsuarioActual.IdUsuario), busqueda);
    }

    private static List<Partido> Filtrar(List<Partido> partidos, string? busqueda)
    {
        if (busqueda != null && busqueda.Length > MaximoBusqueda)
        {
            throw new ArgumentException($"La búsqueda admite hasta {MaximoBusqueda} caracteres.");
        }

        List<Partido> encontrados = [];
        foreach (Partido partido in partidos)
        {
            if (partido.Coincide(busqueda))
            {
                encontrados.Add(partido);
            }
        }

        return encontrados;
    }

    private int ObtenerAdministrador()
    {
        if (_sesion.UsuarioActual == null || !_sesion.UsuarioActual.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un administrador puede gestionar partidos.");
        }

        return _sesion.UsuarioActual.IdUsuario;
    }
}
