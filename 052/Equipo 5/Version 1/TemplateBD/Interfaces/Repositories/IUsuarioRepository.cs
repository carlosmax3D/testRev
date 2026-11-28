using System.Collections.Generic;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Usuario? ValidarLogin(string email, string contrasena);
    List<Usuario> ObtenerTodos();
    void Insertar(Usuario usuario);
    void Modificar(Usuario usuario);
    void Eliminar(int idUsuario);
}