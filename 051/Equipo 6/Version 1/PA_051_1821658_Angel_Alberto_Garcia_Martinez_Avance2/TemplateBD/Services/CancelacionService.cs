using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class CancelacionService : ICancelacionService
{
    private readonly ICancelacionRepository _repository;
    private readonly ISesionUsuario _sesion;

    public CancelacionService(ICancelacionRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public int LongitudMaximaMotivo => Longitudes.MotivoCancelacion;

    public ResumenVenta? BuscarVenta(string folio)
    {
        if (!Validador.FolioValido(folio, out Guid codigo))
        {
            throw new ArgumentException("El folio no es válido. Revíselo e intente de nuevo.");
        }

        return _repository.ObtenerVenta(codigo, ObtenerAdministrador());
    }

    public void CancelarVenta(Guid folio, string motivo)
    {
        if (Validador.Vacio(motivo))
        {
            throw new ArgumentException("Debe escribir el motivo de la cancelación.");
        }

        if (motivo.Trim().Length > Longitudes.MotivoCancelacion)
        {
            throw new ArgumentException($"El motivo no puede superar los {Longitudes.MotivoCancelacion} caracteres.");
        }

        if (!Validador.CaracteresValidos(motivo))
        {
            throw new ArgumentException("El motivo tiene caracteres que no se pueden guardar (emojis o símbolos especiales).");
        }

        _repository.Cancelar(folio, motivo.Trim(), ObtenerAdministrador());
    }

    private int ObtenerAdministrador()
    {
        if (_sesion.UsuarioActual == null || !_sesion.UsuarioActual.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un administrador puede cancelar ventas.");
        }

        return _sesion.UsuarioActual.IdUsuario;
    }
}
