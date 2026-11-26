using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class TipoUsuarioService : ITipoUsuarioService
{
    private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

    public TipoUsuarioService(
        ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    public Task<List<TipoUsuario>> ObtenerTodosAsync()
    {
        return _tipoUsuarioRepository.ObtenerTodosAsync();
    }
}