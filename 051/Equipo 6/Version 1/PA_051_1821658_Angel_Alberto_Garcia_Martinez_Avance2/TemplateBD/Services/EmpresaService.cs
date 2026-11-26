using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _repository;
    private readonly ISesionUsuario _sesion;

    public EmpresaService(IEmpresaRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public Empresa? ObtenerEmpresa()
    {
        return _repository.Obtener(ObtenerAdministrador());
    }

    public void ActualizarEmpresa(Empresa empresa)
    {
        Validar(empresa);
        _repository.Actualizar(empresa, ObtenerAdministrador());
    }

    internal static void Validar(Empresa empresa)
    {
        if (!Validador.TieneLetrasODigitos(empresa.RazonSocial) || empresa.RazonSocial.Trim().Length > Longitudes.RazonSocial)
        {
            throw new ArgumentException($"Capture la razón social de la empresa (máximo {Longitudes.RazonSocial} caracteres, sin emojis ni símbolos especiales).");
        }

        if (!Validador.RfcFiscalValido(empresa.RFC))
        {
            throw new ArgumentException("El RFC de la empresa debe tener 12 caracteres (persona moral) o 13 (persona física).");
        }

        if (!Validador.TieneLetrasODigitos(empresa.Domicilio) || empresa.Domicilio.Trim().Length > Longitudes.DomicilioFiscal)
        {
            throw new ArgumentException($"Capture el domicilio fiscal de la empresa (máximo {Longitudes.DomicilioFiscal} caracteres, sin emojis ni símbolos especiales).");
        }

        empresa.RazonSocial = empresa.RazonSocial.Trim();
        empresa.RFC = empresa.RFC.Trim().ToUpperInvariant();
        empresa.Domicilio = empresa.Domicilio.Trim();
    }

    private int ObtenerAdministrador()
    {
        if (_sesion.UsuarioActual is null || !_sesion.UsuarioActual.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un administrador puede consultar o cambiar los datos de la empresa.");
        }

        return _sesion.UsuarioActual.IdUsuario;
    }
}
