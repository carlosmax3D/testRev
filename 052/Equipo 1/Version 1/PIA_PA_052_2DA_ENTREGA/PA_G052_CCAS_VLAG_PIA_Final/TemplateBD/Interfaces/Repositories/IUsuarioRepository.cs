using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;


namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        //No hay sp_UsuarioAutenticar porque
        //El flujo ahora es: FrmLogin-> UsuarioService.Autenticar (usa GetByCorreo y verifica hash).
        Usuario? GetByCorreo(string correo);
        List<Usuario> GetAll();
        List<Usuario> BuscarPorApellidos(string apellidos);
        void Add(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(int id);
        int ExisteCorreo(string correo, int idExcluir = 0);
        int ExisteNomina(string nomina, int idExcluir = 0);
        List<string> ObtenerContrasenasRecientes(int idUsuario);
    }
}
