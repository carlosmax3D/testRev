using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IFacturaRepository
{
    Factura? ObtenerPorFolioVenta(Guid folio, int idUsuario);
}
