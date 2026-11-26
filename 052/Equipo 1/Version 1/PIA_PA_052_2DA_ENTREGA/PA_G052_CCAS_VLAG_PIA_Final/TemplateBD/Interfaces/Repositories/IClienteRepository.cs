using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IClienteRepository
{
    List<Cliente> GetAll();
    Cliente? GetById(int id);
    List<Cliente> BuscarPorApellidos(string apellido_pa, string apellido_ma);
    void Add(Cliente cliente);
    void Update(Cliente cliente);
    void Delete(int id);
    int ExisteRFC(string rfc, int idExcluir = 0);
    int ExisteCorreo(string correo, int idExcluir = 0);
}