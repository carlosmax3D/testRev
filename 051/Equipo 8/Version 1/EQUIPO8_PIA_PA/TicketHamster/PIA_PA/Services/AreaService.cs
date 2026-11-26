namespace PIA_PA.Entidades.Area;

public class AreaService : IAreaService
{
    private readonly IAreaRepository _repository;

    public AreaService(IAreaRepository repository)
    {
        _repository = repository;
    }

    public List<Area> ObtenerTodos() => _repository.ObtenerTodos();

    public Area? ObtenerPorId(int area_ID)
    {
        if (area_ID <= 0) throw new ArgumentException("La clave Area_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(area_ID);
    }

    public void Insertar(Area entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Area entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int area_ID)
    {
        if (area_ID <= 0) throw new ArgumentException("La clave Area_ID debe ser mayor que cero.");
        _repository.Eliminar(area_ID);
    }
}
