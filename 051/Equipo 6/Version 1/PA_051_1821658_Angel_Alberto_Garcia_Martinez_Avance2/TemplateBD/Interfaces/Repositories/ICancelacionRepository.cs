using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface ICancelacionRepository
{
    ResumenVenta? ObtenerVenta(Guid folio, int idAdministrador);

    void Cancelar(Guid folio, string motivo, int idAdministrador);
}
