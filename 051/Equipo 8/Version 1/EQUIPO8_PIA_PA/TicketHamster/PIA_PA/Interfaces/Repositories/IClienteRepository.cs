using PIA_PA.Entidades.Cliente;

namespace PIA_PA.Entidades.Cliente;

public interface IClienteRepository
{
    List<Cliente> ObtenerTodos();
    Cliente? ObtenerPorId(int cliente_ID);
    void Insertar(Cliente entity);
    void Actualizar(Cliente entity);
    void Eliminar(int cliente_ID);

    System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros);
    object? ValorOperacion(int id, params (string,object?)[] parametros);
    int EjecutarOperacion(int id, params (string,object?)[] parametros);
    void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion);
}
