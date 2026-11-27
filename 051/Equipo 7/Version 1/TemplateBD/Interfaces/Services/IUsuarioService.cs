using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IUsuarioService
{
    void InicializarCredencialesBase();
    Usuario IniciarSesion(string correo, string contrasena);
    List<Usuario> ObtenerUsuarios();
    void RegistrarUsuario(Usuario usuario);
    void ActualizarUsuario(Usuario usuario);
    void EliminarUsuario(int idUsuario);
}