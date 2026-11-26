using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;


namespace TemplateCSharp.Interfaces.Services
{
    public interface IUsuarioService
    {
        Usuario? Autenticar(string correo, string contrasena);

        List<Usuario> Consultar();
        List<Usuario> BuscarPorApellidos(string apellidos);
        (bool ok, string mensaje) Insertar(Usuario u);
        (bool ok, string mensaje) Actualizar(Usuario u);
        (bool ok, string mensaje) Eliminar(int id);
    }
}
