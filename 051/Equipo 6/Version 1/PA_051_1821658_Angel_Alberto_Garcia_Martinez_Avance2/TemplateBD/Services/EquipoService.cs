using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class EquipoService : IEquipoService
{
    private const string SeleccioneEquipo = "Seleccione un equipo de la lista.";

    private readonly IEquipoRepository _repository;
    private readonly ISesionUsuario _sesion;

    public EquipoService(IEquipoRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public List<Equipo> Consultar(string? busqueda)
    {
        if (busqueda != null && busqueda.Length > Longitudes.NombreEquipo)
        {
            throw new ArgumentException($"La búsqueda admite hasta {Longitudes.NombreEquipo} caracteres.");
        }

        return _repository.Consultar(busqueda, ObtenerAdministrador());
    }

    public List<Equipo> ObtenerActivos()
    {
        return Consultar(null).FindAll(equipo => equipo.Activo);
    }

    public void Registrar(string nombre)
    {
        ValidarNombre(nombre);
        _repository.Insertar(nombre.Trim(), ObtenerAdministrador());
    }

    public void Actualizar(Equipo equipo, string nombre)
    {
        ValidarActivo(equipo);
        ValidarNombre(nombre);
        _repository.Actualizar(equipo.IdEquipo, nombre.Trim(), ObtenerAdministrador());
    }

    public void Eliminar(Equipo equipo)
    {
        ValidarActivo(equipo);
        _repository.Eliminar(equipo.IdEquipo, ObtenerAdministrador());
    }

    public void Reactivar(Equipo equipo)
    {
        EstadoRegistro.ExigirInactivo(equipo, equipo?.IdEquipo ?? 0, SeleccioneEquipo, "El equipo ya está activo.");
        _repository.Reactivar(equipo!.IdEquipo, ObtenerAdministrador());
    }

    private static void ValidarActivo(Equipo? equipo)
    {
        EstadoRegistro.ExigirActivo(equipo, equipo?.IdEquipo ?? 0, SeleccioneEquipo,
            "El equipo está inactivo. Reactívelo para modificarlo o eliminarlo.");
    }

    private static void ValidarNombre(string nombre)
    {
        if (!Validador.TieneLetrasODigitos(nombre) || nombre.Trim().Length > Longitudes.NombreEquipo)
        {
            throw new ArgumentException($"Capture un nombre de equipo de 1 a {Longitudes.NombreEquipo} caracteres, sin emojis ni símbolos especiales.");
        }
    }

    private int ObtenerAdministrador()
    {
        if (_sesion.UsuarioActual == null || !_sesion.UsuarioActual.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un administrador puede gestionar equipos.");
        }

        return _sesion.UsuarioActual.IdUsuario;
    }
}
