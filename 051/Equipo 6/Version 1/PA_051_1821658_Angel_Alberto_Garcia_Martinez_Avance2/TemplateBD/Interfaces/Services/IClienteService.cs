using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IClienteService
{
    List<Cliente> ObtenerClientes(string? busqueda);

    // Incluye a los clientes dados de baja; solo administrador (historial de compras)
    List<Cliente> ObtenerClientesConInactivos(string? busqueda);

    void RegistrarCliente(Cliente cliente, bool corregirCodigoPostal = false);

    void ActualizarCliente(Cliente cliente, bool corregirCodigoPostal = false);

    void EliminarCliente(int idCliente);

    // Solo administrador
    void ReactivarCliente(Cliente cliente);

    CodigoPostalInfo? ObtenerCodigoPostal(string codigoPostal);
}
