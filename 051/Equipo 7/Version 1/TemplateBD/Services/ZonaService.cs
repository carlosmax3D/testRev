using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class ZonaService : IZonaService
{
    private readonly IZonaRepository _repository;

    public ZonaService(IZonaRepository repository)
    {
        _repository = repository;
    }

    public List<Zona> ObtenerZonas() => _repository.GetAll();

    public void RegistrarZona(Zona zona)
    {
        if (zona.IdEstadio <= 0)
            throw new ArgumentException("Seleccione un estadio válido.");

        if (string.IsNullOrWhiteSpace(zona.Nombre))
            throw new ArgumentException("El nombre de la zona es obligatorio.");

        if (zona.PrecioBase <= 0)
            throw new ArgumentException("El precio base debe ser mayor a cero.");

        if (zona.Capacidad <= 0)
            throw new ArgumentException("El cupo de la zona debe ser mayor a cero.");

        zona.UsuarioAuditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.Add(zona);
    }
}
