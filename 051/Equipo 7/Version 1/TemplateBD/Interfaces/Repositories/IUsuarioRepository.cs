using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Usuario? ObtenerPorCorreo(string correo);
    List<Usuario> GetAll();
    void Add(Usuario usuario);
    void Update(Usuario usuario);
    void Delete(int idUsuario, string usuarioAuditoria);
}