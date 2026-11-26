using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IClienteRepository
{
    List<Cliente> GetAll();

    Cliente? GetByRFC(string rfc);

    List<Cliente> BuscarPorApellidoOCorreo(string texto);

    void Add(Cliente cliente);

    void Update(Cliente cliente);

    void DeleteLogico(string rfc, int idUsuarioModificacion);

    bool ExisteRfcOCorreo(string rfc, string correo, string? rfcExcluir = null);
}
