using System.Data;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

/// <summary>Reglas de negocio de estadios y zonas.</summary>
public class EstadioService : IEstadioService
{
    /// <summary>Tamaño máximo de la imagen del mapa: 5 MB.</summary>
    public const int TamanoMaximoMapa = 5 * 1024 * 1024;

    private readonly IEstadioRepository _repository;

    public EstadioService(IEstadioRepository repository)
    {
        _repository = repository;
    }

    public DataTable ObtenerEstadios()
    {
        return _repository.Consultar();
    }

    public DataTable BuscarEstadios(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException("Escribe el nombre o la ciudad del estadio que quieres buscar.");
        }

        return _repository.Buscar(texto.Trim());
    }

    public Estadio? ObtenerEstadio(int idEstadio)
    {
        return _repository.ObtenerPorId(idEstadio);
    }

    public List<Zona> ObtenerZonas(int idEstadio)
    {
        return _repository.ConsultarZonas(idEstadio);
    }

    public void ValidarZona(Zona zona)
    {
        if (zona == null)
        {
            throw new ArgumentNullException(nameof(zona));
        }

        zona.NombreZona = zona.NombreZona.Trim();

        if (zona.NombreZona.Length == 0)
        {
            throw new ArgumentException("El nombre de la zona es obligatorio.");
        }

        if (zona.NombreZona.Length > 100)
        {
            throw new ArgumentException("El nombre de la zona no puede superar los 100 caracteres.");
        }

        if (zona.Asientos <= 0)
        {
            throw new ArgumentException("El cupo de la zona debe ser un número entero mayor a cero.");
        }

        if (zona.PrecioBase <= 0 || zona.PrecioBase > 99999999.99m)
        {
            throw new ArgumentException("El precio base debe ser mayor a cero.");
        }
    }

    public void ValidarEstadio(Estadio estadio, List<Zona> zonasActuales)
    {
        if (estadio == null)
        {
            throw new ArgumentNullException(nameof(estadio));
        }

        estadio.Nombre = estadio.Nombre.Trim();
        estadio.Ciudad = estadio.Ciudad.Trim();

        if (estadio.Nombre.Length == 0 || estadio.Nombre.Length > 150)
        {
            throw new ArgumentException("El nombre del estadio es obligatorio (máximo 150 caracteres).");
        }

        if (estadio.Ciudad.Length == 0 || estadio.Ciudad.Length > 100)
        {
            throw new ArgumentException("La ciudad del estadio es obligatoria (máximo 100 caracteres).");
        }

        if (estadio.CapacidadTotal <= 0)
        {
            throw new ArgumentException("La capacidad total debe ser un número entero mayor a cero.");
        }

        if (estadio.MapaEstadio == null || estadio.MapaEstadio.Length == 0)
        {
            throw new ArgumentException("Selecciona la imagen del mapa del estadio con el botón Examinar.");
        }

        if (estadio.MapaEstadio.Length > TamanoMaximoMapa)
        {
            throw new ArgumentException("La imagen del mapa no puede pesar más de 5 MB.");
        }

        if (zonasActuales.Count == 0)
        {
            throw new ArgumentException("Agrega al menos una zona al estadio.");
        }

        int sumaCupos = 0;

        foreach (Zona zona in zonasActuales)
        {
            ValidarZona(zona);
            sumaCupos += zona.Asientos;
        }

        if (sumaCupos > estadio.CapacidadTotal)
        {
            throw new ArgumentException(
                "La suma de los cupos de las zonas (" + sumaCupos.ToString("N0") +
                ") rebasa la capacidad total del estadio (" + estadio.CapacidadTotal.ToString("N0") + ").");
        }

    }

    public int GuardarEstadio(Estadio estadio, List<Zona> zonasActuales, List<Zona> zonasNuevas, List<int> zonasEliminadas, int idUsuario)
    {
        ValidarEstadio(estadio, zonasActuales);

        return _repository.Guardar(estadio, zonasNuevas, zonasEliminadas, idUsuario);
    }

    public void EliminarEstadio(int idEstadio)
    {
        if (idEstadio <= 0)
        {
            throw new ArgumentException("Selecciona el estadio que quieres eliminar.");
        }

        _repository.Eliminar(idEstadio);
    }
}
