using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IClienteService
{
    List<Cliente> ObtenerClientes();
    Cliente? ObtenerCliente(int id);
    void CrearCliente(Cliente cliente);
    void ActualizarCliente(Cliente cliente);
    void EliminarCliente(int id);
}
