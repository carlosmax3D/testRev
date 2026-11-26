using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Services;

public class EstadioService : ServicioBase, IEstadioService
{
    private const int CapacidadMaxima = 200_000;

    private readonly IEstadioRepository _estadios;
    private readonly IZonaRepository _zonas;

    public EstadioService(
        IEstadioRepository estadios,
        IZonaRepository zonas,
        ISesionActual sesion) : base(sesion)
    {
        _estadios = estadios;
        _zonas = zonas;
    }

    public List<Estadio> ObtenerEstadios()
    {
        ExigirSesion();

        return _estadios.GetAll();
    }

    public int CrearEstadio(Estadio estadio)
    {
        ExigirAdministrador();

        if (estadio is null)
        {
            throw new ArgumentNullException(nameof(estadio));
        }

        ValidarTexto(estadio.Nombre, "Nombre del estadio", 100);
        ValidarTexto(estadio.Ciudad, "Ciudad", 100);

        if (estadio.Capacidad <= 0 || estadio.Capacidad > CapacidadMaxima)
        {
            throw new ArgumentException($"La capacidad total debe estar entre 1 y {CapacidadMaxima:N0}.");
        }

        if (string.IsNullOrWhiteSpace(estadio.MapaEstadio))
        {
            throw new ArgumentException("Seleccione la imagen del mapa del estadio.");
        }

        if (estadio.MapaEstadio.Trim().Length > 300)
        {
            throw new ArgumentException("La ruta del mapa del estadio es demasiado larga (máximo 300 caracteres).");
        }

        estadio.Nombre = estadio.Nombre.Trim();
        estadio.Ciudad = estadio.Ciudad.Trim();
        estadio.MapaEstadio = estadio.MapaEstadio.Trim();

        if (_estadios.ExisteNombre(estadio.Nombre))
        {
            throw new ArgumentException("Ya existe un estadio registrado con ese nombre.");
        }

        estadio.IdUsuarioRegistro = Sesion.IdUsuario;

        return _estadios.Add(estadio);
    }

    public List<Zona> ObtenerZonas(int idEstadio)
    {
        ExigirSesion();

        if (idEstadio <= 0)
        {
            throw new ArgumentException("Seleccione un estadio.");
        }

        return _zonas.GetByEstadio(idEstadio);
    }

    public int CrearZona(Zona zona)
    {
        ExigirAdministrador();

        if (zona is null)
        {
            throw new ArgumentNullException(nameof(zona));
        }

        ValidarTexto(zona.NombreZona, "Nombre de la zona", 50);

        if (zona.Precio <= 0 || zona.Precio > 99_999_999m)
        {
            throw new ArgumentException("El precio base debe ser mayor que cero.");
        }

        if (zona.Cupo <= 0)
        {
            throw new ArgumentException("El cupo de la zona debe ser mayor que cero.");
        }

        Estadio estadio = _estadios.GetById(zona.IdEstadio)
            ?? throw new ArgumentException("Seleccione un estadio válido.");

        zona.NombreZona = zona.NombreZona.Trim();
        zona.Precio = Math.Round(zona.Precio, 2);

        if (_zonas.ExisteNombreEnEstadio(zona.NombreZona, zona.IdEstadio))
        {
            throw new ArgumentException("Ya existe una zona con ese nombre en el estadio seleccionado.");
        }

        int cupoRegistrado = _zonas.GetByEstadio(zona.IdEstadio).Sum(z => z.Cupo);

        if (cupoRegistrado + zona.Cupo > estadio.Capacidad)
        {
            int disponible = estadio.Capacidad - cupoRegistrado;

            throw new ArgumentException(
                $"El cupo excede la capacidad del estadio. Solo quedan {Math.Max(disponible, 0):N0} lugares por asignar a zonas.");
        }

        zona.IdUsuarioRegistro = Sesion.IdUsuario;

        return _zonas.Add(zona);
    }
}
