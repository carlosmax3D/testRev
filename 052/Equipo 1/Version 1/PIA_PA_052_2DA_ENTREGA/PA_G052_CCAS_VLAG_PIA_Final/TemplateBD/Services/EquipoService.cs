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
    public class EquipoService : IEquipoService
    {
        private readonly IEquipoRepository _repository;

        public EquipoService(IEquipoRepository repository)
        {
            _repository = repository;
        }

        public List<Equipo> ObtenerEquipos() => _repository.GetAll();

        public Equipo? ObtenerEquipo(int id) => _repository.GetById(id);

        public void CrearEquipo(Equipo equipo)
        {
            //CANDADO DE ADMIN
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden registrar equipos.");

            ValidarEquipo(equipo);
            ValidarDuplicado(equipo);
            _repository.Add(equipo);
        }

        public void ActualizarEquipo(Equipo equipo)
        {
            //CANDADO DE ADMIN
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden editar equipos.");

            ValidarEquipo(equipo);
            if (equipo.IdEquipo <= 0) throw new ArgumentException("El ID del equipo no es válido.");

            ValidarDuplicado(equipo);
            _repository.Update(equipo);
        }

        private static void ValidarEquipo(Equipo equipo)
        {
            if (equipo is null) throw new ArgumentNullException(nameof(equipo));
            if (string.IsNullOrWhiteSpace(equipo.NombreEquipo)) throw new ArgumentException("El nombre del equipo es obligatorio.");

            //Solo letras, números y espacios para nombres como "Tigres UANL", "América", "C.F. Monterrey")
            string patronNombre = @"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\.]+$";
            if (!Regex.IsMatch(equipo.NombreEquipo, patronNombre))
                throw new ArgumentException("El nombre del equipo solo puede contener letras, números, puntos y espacios.");

            if (equipo.NombreEquipo.Length > 100)
                throw new ArgumentException("El nombre del equipo no puede superar los 100 caracteres.");
        }

        private void ValidarDuplicado(Equipo equipo)
        {
            if (_repository.ExisteEquipo(equipo.NombreEquipo, equipo.IdEquipo) > 0)
                throw new ArgumentException("El nombre del equipo ya está registrado. Intente con otro.");
        }

    }
}
