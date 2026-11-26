namespace PIA_PA.Entidades.Cliente;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public List<Cliente> ObtenerTodos() => _repository.ObtenerTodos();

    public Cliente? ObtenerPorId(int cliente_ID)
    {
        if (cliente_ID <= 0) throw new ArgumentException("La clave Cliente_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(cliente_ID);
    }

    public void Insertar(Cliente entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Cliente entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int cliente_ID)
    {
        if (cliente_ID <= 0) throw new ArgumentException("La clave Cliente_ID debe ser mayor que cero.");
        _repository.Eliminar(cliente_ID);
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => _repository.TablaOperacion(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => _repository.ValorOperacion(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => _repository.EjecutarOperacion(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => _repository.TransaccionOperacion(accion);
}
