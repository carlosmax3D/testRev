namespace PIA_PA.Entidades.Asiento;

public class AsientoService : IAsientoService
{
    private readonly IAsientoRepository _repository;

    public AsientoService(IAsientoRepository repository)
    {
        _repository = repository;
    }

    public List<Asiento> ObtenerTodos() => _repository.ObtenerTodos();

    public Asiento? ObtenerPorId(int asiento_ID)
    {
        if (asiento_ID <= 0) throw new ArgumentException("La clave Asiento_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(asiento_ID);
    }

    public void Insertar(Asiento entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Asiento entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int asiento_ID)
    {
        if (asiento_ID <= 0) throw new ArgumentException("La clave Asiento_ID debe ser mayor que cero.");
        _repository.Eliminar(asiento_ID);
    }
}
