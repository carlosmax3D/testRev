namespace PIA_PA.Entidades.Direccion;

public class DireccionService : IDireccionService
{
    private readonly IDireccionRepository _repository;

    public DireccionService(IDireccionRepository repository)
    {
        _repository = repository;
    }

    public List<Direccion> ObtenerTodos() => _repository.ObtenerTodos();

    public Direccion? ObtenerPorId(int direccion_ID)
    {
        if (direccion_ID <= 0) throw new ArgumentException("La clave Direccion_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(direccion_ID);
    }

    public void Insertar(Direccion entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Direccion entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int direccion_ID)
    {
        if (direccion_ID <= 0) throw new ArgumentException("La clave Direccion_ID debe ser mayor que cero.");
        _repository.Eliminar(direccion_ID);
    }
}
