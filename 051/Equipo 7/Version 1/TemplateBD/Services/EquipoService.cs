using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;


namespace TemplateCSharp.Services;

public class EquipoService : IEquipoService
{
    private readonly IEquipoRepository _repository;

    public EquipoService(IEquipoRepository repository)
    {
        _repository = repository;
    }

    public List<Equipo> ObtenerEquipos() => _repository.GetAll();

    public void RegistrarEquipo(Equipo equipo)
    {
        if (string.IsNullOrWhiteSpace(equipo.Nombre))
            throw new ArgumentException("El nombre del equipo es obligatorio.");

        if (string.IsNullOrWhiteSpace(equipo.Ciudad))
            throw new ArgumentException("La ciudad del equipo es obligatoria.");

        equipo.UsuarioAuditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.Add(equipo);
    }
}
