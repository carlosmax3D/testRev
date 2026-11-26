using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using System.Text.RegularExpressions;


namespace TemplateCSharp.Services
{
    public class PartidoService : IPartidoService
    {
        private readonly IPartidoRepository _repository;

        public PartidoService(IPartidoRepository repository)
        {
            _repository = repository;
        }

        public List<Partido> ObtenerPartidos() => _repository.GetAll();

        public Partido? ObtenerPartido(int id) => _repository.GetById(id);

        public List<Partido> BuscarPartidos(DateTime? fecha, int? idEstadio, string? nombreEquipo) => _repository.GetByFiltros(fecha, idEstadio, nombreEquipo);

        public void CrearPartido(Partido partido)
        {
            //CANDADO DE ADMIN
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden registrar partidos.");

            ValidarPartido(partido);
            _repository.Add(partido);
        }

        public void ActualizarPartido(Partido partido)
        {
            //CANDADO DE ADMIN
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden editar partidos.");

            ValidarPartido(partido);
            if (partido.IdPartido <= 0) throw new ArgumentException("El ID del partido no es válido.");
            _repository.Update(partido);
        }

        private static void ValidarPartido(Partido partido)
        {
            if (partido is null) throw new ArgumentNullException(nameof(partido));
            if (partido.IdEstadio <= 0) throw new ArgumentException("Debe seleccionar un estadio.");
            if (partido.IdEquipoLocal <= 0) throw new ArgumentException("Debe seleccionar el equipo local.");
            if (partido.IdEquipoVisitante <= 0) throw new ArgumentException("Debe seleccionar el equipo visitante.");

            //Un equipo no puede jugar contra sí mismo
            if (partido.IdEquipoLocal == partido.IdEquipoVisitante)
                throw new ArgumentException("El equipo local y el visitante no pueden ser el mismo.");

            //no registrar partidos en fechas en el pasado
            if (partido.FechaPartido.Date < DateTime.Today)
                throw new ArgumentException("No se pueden registrar partidos en fechas pasadas.");

            string[] estadosValidos = { "Programado", "Terminado", "Cancelado" };
            if (!estadosValidos.Contains(partido.Estado))
                throw new ArgumentException("El estado del partido debe ser 'Programado', 'Terminado' o 'Cancelado'.");
        }

        public void ActualizarPartidosTerminados()
        {
            _repository.ActualizarPartidosTerminados();
        }

    }
}
