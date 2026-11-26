using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IUsuarioRepository
{
    List<Usuario> GetAll();

    Usuario? GetById(int id);

    Usuario? GetByCorreo(string correo);

    int Add(Usuario usuario, string contrasenaHash);

    void Update(Usuario usuario);

    void ChangePassword(int idUsuario, string contrasenaHash, int idUsuarioCambio);

    void DeleteLogico(int id, int idUsuarioModificacion);

    bool ExisteCorreoONomina(string correo, string numNomina, int idExcluir = 0);
}
