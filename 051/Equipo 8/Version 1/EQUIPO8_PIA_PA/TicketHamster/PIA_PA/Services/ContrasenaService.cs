namespace PIA_PA.Entidades.Contrasena;

public class ContrasenaService : IContrasenaService
{
    private readonly IContrasenaRepository _repository;

    public ContrasenaService(IContrasenaRepository repository)
    {
        _repository = repository;
    }

    public List<Contrasena> ObtenerTodos() => _repository.ObtenerTodos();

    public Contrasena? ObtenerPorId(int contrasena_ID)
    {
        if (contrasena_ID <= 0) throw new ArgumentException("La clave Contrasena_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(contrasena_ID);
    }

    public void Insertar(Contrasena entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Contrasena entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int contrasena_ID)
    {
        if (contrasena_ID <= 0) throw new ArgumentException("La clave Contrasena_ID debe ser mayor que cero.");
        _repository.Eliminar(contrasena_ID);
    }
}
