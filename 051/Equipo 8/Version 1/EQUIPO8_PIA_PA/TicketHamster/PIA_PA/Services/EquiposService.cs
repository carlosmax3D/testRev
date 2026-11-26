using System.Data;

namespace PIA_PA.Entidades.Equipos;

public class EquiposService : IEquiposService
{
    private readonly IEquiposRepository _repository;

    public DataTable ObtenerTablaConsulta() => _repository.ObtenerTablaConsulta();

    public EquiposService(IEquiposRepository repository)
    {
        _repository = repository;
    }

    public List<Equipos> ObtenerTodos() => _repository.ObtenerTodos();

    public Equipos? ObtenerPorId(int equipo_ID)
    {
        if (equipo_ID <= 0) throw new ArgumentException("La clave Equipo_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(equipo_ID);
    }

    public void Insertar(Equipos entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Equipos entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int equipo_ID)
    {
        if (equipo_ID <= 0) throw new ArgumentException("La clave Equipo_ID debe ser mayor que cero.");
        _repository.Eliminar(equipo_ID);
    }
}
