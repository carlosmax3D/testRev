using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IUsuarioRepository
{
    List<Usuario> GetAll();
    Usuario? GetById(int id);
    Usuario? Login(string correo, string contrasena);
    void Add(Usuario usuario);
    void Update(Usuario usuario);
    void Delete(int id);
}
