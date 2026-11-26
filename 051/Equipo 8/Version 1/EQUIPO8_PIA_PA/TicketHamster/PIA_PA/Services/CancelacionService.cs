namespace PIA_PA.Entidades.Cancelacion;

public class CancelacionService : ICancelacionService
{
    private readonly ICancelacionRepository _repository;

    public CancelacionService(ICancelacionRepository repository)
    {
        _repository = repository;
    }

    public List<Cancelacion> ObtenerTodos() => _repository.ObtenerTodos();

    public Cancelacion? ObtenerPorId(int cancelacion_ID)
    {
        if (cancelacion_ID <= 0) throw new ArgumentException("La clave Cancelacion_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(cancelacion_ID);
    }

    public void Insertar(Cancelacion entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Cancelacion entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int cancelacion_ID)
    {
        if (cancelacion_ID <= 0) throw new ArgumentException("La clave Cancelacion_ID debe ser mayor que cero.");
        _repository.Eliminar(cancelacion_ID);
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => _repository.TablaOperacion(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => _repository.ValorOperacion(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => _repository.EjecutarOperacion(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => _repository.TransaccionOperacion(accion);
}
