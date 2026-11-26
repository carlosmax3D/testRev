namespace PIA_PA.Entidades.Deporte;

public class DeporteService : IDeporteService
{
    private readonly IDeporteRepository _repository;

    public DeporteService(IDeporteRepository repository)
    {
        _repository = repository;
    }

    public List<Deporte> ObtenerTodos() => _repository.ObtenerTodos();

    public Deporte? ObtenerPorId(int deporte_ID)
    {
        if (deporte_ID <= 0) throw new ArgumentException("La clave Deporte_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(deporte_ID);
    }

    public void Insertar(Deporte entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Deporte entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int deporte_ID)
    {
        if (deporte_ID <= 0) throw new ArgumentException("La clave Deporte_ID debe ser mayor que cero.");
        _repository.Eliminar(deporte_ID);
    }
}
