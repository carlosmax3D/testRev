namespace PIA_PA.Entidades.Partido_Area_Precio;

public class Partido_Area_PrecioService : IPartido_Area_PrecioService
{
    private readonly IPartido_Area_PrecioRepository _repository;

    public Partido_Area_PrecioService(IPartido_Area_PrecioRepository repository)
    {
        _repository = repository;
    }

    public List<Partido_Area_Precio> ObtenerTodos() => _repository.ObtenerTodos();

    public Partido_Area_Precio? ObtenerPorId(int partido_ID, int area_ID)
    {
        if (partido_ID <= 0) throw new ArgumentException("La clave Partido_ID debe ser mayor que cero.");
    if (area_ID <= 0) throw new ArgumentException("La clave Area_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(partido_ID, area_ID);
    }

    public void Insertar(Partido_Area_Precio entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Partido_Area_Precio entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int partido_ID, int area_ID)
    {
        if (partido_ID <= 0) throw new ArgumentException("La clave Partido_ID debe ser mayor que cero.");
    if (area_ID <= 0) throw new ArgumentException("La clave Area_ID debe ser mayor que cero.");
        _repository.Eliminar(partido_ID, area_ID);
    }
}
