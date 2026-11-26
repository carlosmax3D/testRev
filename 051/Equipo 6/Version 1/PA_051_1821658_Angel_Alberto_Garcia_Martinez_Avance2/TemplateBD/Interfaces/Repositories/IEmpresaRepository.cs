using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IEmpresaRepository
{
    Empresa? Obtener(int idAdministrador);

    void Actualizar(Empresa empresa, int idAdministrador);
}
