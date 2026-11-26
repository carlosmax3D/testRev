using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IEmpresaService
{
    Empresa? ObtenerEmpresa();

    void ActualizarEmpresa(Empresa empresa);
}
