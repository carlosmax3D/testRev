using System.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IClienteService
{
    DataTable ObtenerClientes();

    DataTable BuscarClientes(string? criterio, string texto);

    Cliente? ObtenerCliente(int idCliente);

    int RegistrarCliente(Cliente cliente, int idUsuario);

    void ActualizarCliente(Cliente cliente, int idUsuario);

    void EliminarCliente(int idCliente, int idUsuario);

    DataTable ObtenerAuditoria(int idCliente);
}
