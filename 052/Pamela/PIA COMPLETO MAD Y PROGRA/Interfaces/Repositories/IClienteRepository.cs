using System.Data;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IClienteRepository
{
    DataTable Consultar();

    DataTable Buscar(string? criterio, string texto);

    Cliente? ObtenerPorId(int idCliente);

    int Agregar(Cliente cliente, int idUsuario);

    void Modificar(Cliente cliente, int idUsuario);

    void Eliminar(int idCliente, int idUsuario);

    DataTable ConsultarAuditoria(int idCliente);
}
