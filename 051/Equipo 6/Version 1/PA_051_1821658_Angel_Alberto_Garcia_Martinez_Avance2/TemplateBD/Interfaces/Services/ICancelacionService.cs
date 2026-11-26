using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface ICancelacionService
{
    int LongitudMaximaMotivo { get; }

    ResumenVenta? BuscarVenta(string folio);

    void CancelarVenta(Guid folio, string motivo);
}
