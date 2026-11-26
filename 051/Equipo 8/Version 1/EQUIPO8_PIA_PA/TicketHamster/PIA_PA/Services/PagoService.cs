namespace PIA_PA.Entidades.Pago;

public class PagoService : IPagoService
{
    private readonly IPagoRepository _repository;

    public PagoService(IPagoRepository repository)
    {
        _repository = repository;
    }

    public List<Pago> ObtenerTodos() => _repository.ObtenerTodos();

    public Pago? ObtenerPorId(int pago_ID)
    {
        if (pago_ID <= 0) throw new ArgumentException("La clave Pago_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(pago_ID);
    }

    public void Insertar(Pago entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Pago entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int pago_ID)
    {
        if (pago_ID <= 0) throw new ArgumentException("La clave Pago_ID debe ser mayor que cero.");
        _repository.Eliminar(pago_ID);
    }
}
