using VentaBoletosApp.Models;

namespace VentaBoletosApp.Interfaces.Repositories;

public interface IEmpresaRepository
{
    Empresa? Obtener();
}
