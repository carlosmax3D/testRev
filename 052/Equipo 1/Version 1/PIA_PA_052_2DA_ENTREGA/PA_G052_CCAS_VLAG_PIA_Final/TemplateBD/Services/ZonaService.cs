using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;


namespace TemplateCSharp.Services
{
    public class ZonaService : IZonaService
    {
        private readonly IZonaRepository _repository;
        private readonly IEstadioService _estadioService;

        public ZonaService(IZonaRepository repository, IEstadioService estadioService)
        {
            _repository = repository;
            _estadioService = estadioService;
        }

        public List<Zona> ObtenerZonas() => _repository.GetAll();
        public Zona? ObtenerZona(int id) => _repository.GetById(id);
        public List<Zona> ObtenerZonasPorEstadio(int idEstadio) => _repository.GetByEstadio(idEstadio);

        public void CrearZona(Zona zona)
        {
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden registrar zonas.");
            ValidarZona(zona);
            ValidarDuplicado(zona);
            ValidarCapacidadEstadio(zona);
            _repository.Add(zona);
        }

        public void ActualizarZona(Zona zona)
        {
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden editar zonas.");

            ValidarZona(zona);
            if (zona.IdZona <= 0)
                throw new ArgumentException("El ID de la zona no es válido.");

            ValidarCapacidadEstadio(zona);
            ValidarDuplicado(zona);
            _repository.Update(zona);
        }

        private void ValidarCapacidadEstadio(Zona zona)
        {
            var estadio = _estadioService.ObtenerEstadio(zona.IdEstadio);
            if (estadio == null)
                throw new ArgumentException("El estadio seleccionado no existe.");

            //Sumar cupos de OTRAS zonas del mismo estadio (excluyendo la que se edita)
            var zonasDelEstadio = _repository.GetByEstadio(zona.IdEstadio);
            int cupoOcupado = 0;
            foreach (var z in zonasDelEstadio)
            {
                if (z.IdZona != zona.IdZona)
                    cupoOcupado += z.CupoDeLaZona;
            }

            int cupoNuevoTotal = cupoOcupado + zona.CupoDeLaZona;
            if (cupoNuevoTotal > estadio.CapacidadTotal)
            {
                int disponible = estadio.CapacidadTotal - cupoOcupado;
                throw new ArgumentException(
                    $"El cupo total de las zonas ({cupoNuevoTotal}) excede la capacidad del estadio " +
                    $"({estadio.CapacidadTotal}). Cupo disponible: {disponible}.");
            }
        }

        private static void ValidarZona(Zona zona)
        {
            if (zona is null) throw new ArgumentNullException(nameof(zona));
            if (zona.IdEstadio <= 0) throw new ArgumentException("Debe seleccionar un estadio.");
            if (string.IsNullOrWhiteSpace(zona.NombreZona)) throw new ArgumentException("El nombre de la zona es obligatorio.");
            if (zona.PrecioBase <= 0) throw new ArgumentException("El precio base debe ser mayor a cero.");
            if (zona.CupoDeLaZona <= 0) throw new ArgumentException("El cupo de la zona debe ser mayor a cero.");
            string patronNombre = @"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]+$";
            if (!Regex.IsMatch(zona.NombreZona, patronNombre))
                throw new ArgumentException("El nombre de la zona solo puede contener letras, números y espacios.");
        }

        private void ValidarDuplicado(Zona zona)
        {
            if (_repository.ExisteZona(zona.IdEstadio, zona.NombreZona, zona.IdZona) > 0)
                throw new ArgumentException($"Ya existe una zona con el nombre '{zona.NombreZona}' en este estadio.");
        }

    }
}
