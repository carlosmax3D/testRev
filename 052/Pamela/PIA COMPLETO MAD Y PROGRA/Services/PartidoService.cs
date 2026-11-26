using System.Data;
using System.Globalization;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

/// <summary>Reglas de negocio de los partidos.</summary>
public class PartidoService : IPartidoService
{
    private static readonly string[] EstadosValidos = { "Programado", "Terminado", "Cancelado" };

    private static readonly string[] FormatosFecha =
    {
        "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy", "yyyy-MM-dd", "dd/MMM/yyyy", "dd-MMM-yyyy"
    };

    private readonly IPartidoRepository _repository;

    public PartidoService(IPartidoRepository repository)
    {
        _repository = repository;
    }

    public DataTable ObtenerPartidos()
    {
        return _repository.Consultar();
    }

    public DataTable BuscarPartidos(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException("Escribe el equipo o el estadio del partido que quieres buscar.");
        }

        return _repository.Buscar(texto.Trim());
    }

    public Partido? ObtenerPartido(int idPartido)
    {
        return _repository.ObtenerPorId(idPartido);
    }

    public int RegistrarPartido(Partido partido, int idUsuario)
    {
        Validar(partido);

        // Regla de negocio: no registrar partidos en fechas en el pasado
        if (partido.FechaHora <= DateTime.Now)
        {
            throw new ArgumentException("No se pueden registrar partidos en una fecha y hora que ya pasaron.");
        }

        partido.Estado = "Programado";

        return _repository.Agregar(partido, idUsuario);
    }

    public void ActualizarPartido(Partido partido, int idUsuario)
    {
        Validar(partido);

        if (partido.IdPartido <= 0)
        {
            throw new ArgumentException("El partido que quieres modificar no es válido.");
        }

        if (Array.IndexOf(EstadosValidos, partido.Estado) < 0)
        {
            throw new ArgumentException("Selecciona el estado del partido: Programado, Terminado o Cancelado.");
        }

        _repository.Modificar(partido, idUsuario);
    }

    public void EliminarPartido(int idPartido, int idUsuario)
    {
        if (idPartido <= 0)
        {
            throw new ArgumentException("Selecciona el partido que quieres eliminar.");
        }

        _repository.Eliminar(idPartido, idUsuario);
    }

    public DataTable ObtenerPartidosDisponibles(string? criterio, string? texto)
    {
        // Sin criterio: todos los partidos disponibles para la venta
        if (string.IsNullOrWhiteSpace(criterio) || string.IsNullOrWhiteSpace(texto))
        {
            return _repository.ConsultarDisponibles(null, null, null);
        }

        string valor = texto.Trim();

        if (criterio.StartsWith("Fecha", StringComparison.OrdinalIgnoreCase))
        {
            if (!DateTime.TryParseExact(
                    valor,
                    FormatosFecha,
                    new CultureInfo("es-MX"),
                    DateTimeStyles.None,
                    out DateTime fecha))
            {
                throw new ArgumentException("Escribe la fecha con el formato día/mes/año, por ejemplo 25/12/2026.");
            }

            return _repository.ConsultarDisponibles("fecha", null, fecha);
        }

        if (criterio.StartsWith("Estadio", StringComparison.OrdinalIgnoreCase))
        {
            return _repository.ConsultarDisponibles("estadio", valor, null);
        }

        return _repository.ConsultarDisponibles("equipo", valor, null);
    }

    public DataTable ObtenerZonasDePartido(int idPartido, bool soloDisponibles)
    {
        return _repository.ConsultarZonas(idPartido, soloDisponibles);
    }

    private static void Validar(Partido partido)
    {
        if (partido == null)
        {
            throw new ArgumentNullException(nameof(partido));
        }

        partido.EquipoLocal = partido.EquipoLocal.Trim();
        partido.EquipoVisitante = partido.EquipoVisitante.Trim();

        if (partido.IdEstadio <= 0)
        {
            throw new ArgumentException("Selecciona el estadio donde se jugará el partido.");
        }

        if (partido.EquipoLocal.Length == 0 || partido.EquipoLocal.Length > 100)
        {
            throw new ArgumentException("El equipo local es obligatorio (máximo 100 caracteres).");
        }

        if (partido.EquipoVisitante.Length == 0 || partido.EquipoVisitante.Length > 100)
        {
            throw new ArgumentException("El equipo visitante es obligatorio (máximo 100 caracteres).");
        }

        if (string.Equals(partido.EquipoLocal, partido.EquipoVisitante, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("El equipo local y el visitante deben ser diferentes.");
        }
    }
}
