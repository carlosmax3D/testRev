namespace PIA_PA.Entidades.Emisor;

public class EmisorService : IEmisorService
{
    private readonly IEmisorRepository _repository;

    public EmisorService(IEmisorRepository repository)
    {
        _repository = repository;
    }

    public List<Emisor> ObtenerTodos() => _repository.ObtenerTodos();

    public Emisor? ObtenerPorId(int emisor_ID)
    {
        if (emisor_ID <= 0) throw new ArgumentException("La clave Emisor_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(emisor_ID);
    }

    public void Insertar(Emisor entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Emisor entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int emisor_ID)
    {
        if (emisor_ID <= 0) throw new ArgumentException("La clave Emisor_ID debe ser mayor que cero.");
        _repository.Eliminar(emisor_ID);
    }
}
