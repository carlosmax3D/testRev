using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Services;

public interface IClienteService
{
    List<Cliente> ObtenerClientes();

    Cliente? ObtenerCliente(string rfc);

    /// <summary>Busca por apellidos/nombre o correo electronico.</summary>
    List<Cliente> BuscarClientes(string texto, bool soloActivos);

    void CrearCliente(Cliente cliente);

    void ActualizarCliente(Cliente cliente);

    void EliminarCliente(string rfc);
}
