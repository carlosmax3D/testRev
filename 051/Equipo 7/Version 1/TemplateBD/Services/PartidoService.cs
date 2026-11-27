using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;



namespace TemplateCSharp.Services;

public class PartidoService : IPartidoService
{
    private readonly IPartidoRepository _repository;

    public PartidoService(IPartidoRepository repository)
    {
        _repository = repository;
    }

    public List<Partido> ObtenerPartidos() => _repository.GetAll();

    public void RegistrarPartido(Partido partido)
    {
        if (partido.IdEstadio <= 0)
            throw new ArgumentException("Seleccione un estadio para el encuentro.");

        if (partido.IdEquipoLocal <= 0 || partido.IdEquipoVisitante <= 0)
            throw new ArgumentException("Seleccione tanto el equipo local como el visitante.");

        if (partido.IdEquipoLocal == partido.IdEquipoVisitante)
            throw new ArgumentException("El equipo local y el equipo visitante no pueden ser el mismo.");

        if (partido.Fecha.Date < DateTime.Today)
            throw new ArgumentException("No se pueden registrar partidos en fechas pasadas.");

        partido.UsuarioAuditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.Add(partido);
    }
}