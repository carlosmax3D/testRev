namespace PIA_PA.Entidades.Telefono;

public class TelefonoService : ITelefonoService
{
    private readonly ITelefonoRepository _repository;

    public TelefonoService(ITelefonoRepository repository)
    {
        _repository = repository;
    }

    public List<Telefono> ObtenerTodos() => _repository.ObtenerTodos();

    public Telefono? ObtenerPorId(int telefono_ID)
    {
        if (telefono_ID <= 0) throw new ArgumentException("La clave Telefono_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(telefono_ID);
    }

    public void Insertar(Telefono entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Telefono entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int telefono_ID)
    {
        if (telefono_ID <= 0) throw new ArgumentException("La clave Telefono_ID debe ser mayor que cero.");
        _repository.Eliminar(telefono_ID);
    }
}
