using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;


namespace TemplateCSharp.Services
{
    public class EstadioService : IEstadioService
    {
        private readonly IEstadioRepository _repository;
        private readonly IZonaRepository _zonaRepository;

        public EstadioService(IEstadioRepository repository, IZonaRepository zonaRepository)
        {
            _repository = repository;
            _zonaRepository = zonaRepository;
        }

        public List<Estadio> ObtenerEstadios() => _repository.GetAll();

        public Estadio? ObtenerEstadio(int id) => _repository.GetById(id);

        public void CrearEstadio(Estadio estadio)
        {
            //Validación de permisos de usuario
            if (!Sesion.EsAdministrador)
                throw new UnauthorizedAccessException("Solo los administradores pueden registrar estadios.");

            ValidarEstadio(estadio);
            ValidarDuplicado(estadio);
            _repository.Add(estadio);
        }

        public void ActualizarEstadio(Estadio estadio)
        {
            //Validación de permisos de usuario
            if (!Sesion.EsAdministrador)
                throw new UnauthorizedAccessException("Solo los administradores pueden editar estadios.");

            ValidarEstadio(estadio);
            ValidarDuplicado(estadio);
            if (estadio.IdEstadio <= 0) throw new ArgumentException("El ID del estadio no es válido.");
            var zonas = _zonaRepository.GetByEstadio(estadio.IdEstadio);
            int sumaCupos = zonas.Sum(z => z.CupoDeLaZona);
            if (estadio.CapacidadTotal < sumaCupos)
                throw new ArgumentException($"La capacidad total ({estadio.CapacidadTotal}) no puede ser menor a la suma de los cupos de sus zonas ({sumaCupos}).");
            _repository.Update(estadio);
        }

        // Validaciones basadas en la FE
        private static void ValidarEstadio(Estadio estadio)
        {
            if (estadio is null) throw new ArgumentNullException(nameof(estadio));
            if (string.IsNullOrWhiteSpace(estadio.Nombre)) throw new ArgumentException("El nombre del estadio es obligatorio.");
            if (string.IsNullOrWhiteSpace(estadio.Ciudad)) throw new ArgumentException("La ciudad es obligatoria.");
            if (estadio.CapacidadTotal <= 0) throw new ArgumentException("La capacidad total debe ser mayor a cero.");
            if (string.IsNullOrWhiteSpace(estadio.MapaEstadio)) throw new ArgumentException("El mapa del estadio es obligatorio.");

            //Expresiones regulares
            string patronLetras = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$";

            if (!Regex.IsMatch(estadio.Nombre, patronLetras))
                throw new ArgumentException("El nombre del estadio solo puede contener letras y espacios.");

            if (!Regex.IsMatch(estadio.Ciudad, patronLetras))
                throw new ArgumentException("La ciudad solo puede contener letras y espacios.");
        }
        private void ValidarDuplicado(Estadio estadio)
        {
            if (_repository.ExisteEstadio(estadio.Nombre, estadio.IdEstadio) > 0)
                throw new ArgumentException($"El estadio con el nombre '{estadio.Nombre}' ya está registrado.");
        }

    }
}
