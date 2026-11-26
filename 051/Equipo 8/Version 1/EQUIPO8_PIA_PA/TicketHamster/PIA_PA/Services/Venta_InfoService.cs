namespace PIA_PA.Entidades.Venta_Info;

public class Venta_InfoService : IVenta_InfoService
{
    private readonly IVenta_InfoRepository _repository;

    public Venta_InfoService(IVenta_InfoRepository repository)
    {
        _repository = repository;
    }

    public List<Venta_Info> ObtenerTodos() => _repository.ObtenerTodos();

    public Venta_Info? ObtenerPorId(int venta_Info_ID)
    {
        if (venta_Info_ID <= 0) throw new ArgumentException("La clave Venta_Info_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(venta_Info_ID);
    }

    public void Insertar(Venta_Info entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Venta_Info entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int venta_Info_ID)
    {
        if (venta_Info_ID <= 0) throw new ArgumentException("La clave Venta_Info_ID debe ser mayor que cero.");
        _repository.Eliminar(venta_Info_ID);
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => _repository.TablaOperacion(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => _repository.ValorOperacion(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => _repository.EjecutarOperacion(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => _repository.TransaccionOperacion(accion);
}
