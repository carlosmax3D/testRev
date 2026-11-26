using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IClienteRepository
{
    List<Cliente> Consultar(string? busqueda, int idUsuario, bool incluirInactivos = false);

    int Insertar(Cliente cliente, int idUsuarioRegistro, bool corregirCodigoPostal);

    void Actualizar(Cliente cliente, int idUsuarioModifico, bool corregirCodigoPostal);

    // Baja lógica (Activo = 0)
    void Eliminar(int idCliente, int idUsuario);

    void Reactivar(int idCliente, int idUsuario);

    CodigoPostalInfo? ObtenerCodigoPostal(string codigoPostal, int idUsuario);
}
