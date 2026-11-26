using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;
using System.Text.RegularExpressions;

namespace TemplateCSharp.Services
{
    public class AsientoService : IAsientoService
    {
        private readonly IAsientoRepository _repository;
        private readonly IZonaService _zonaService;

        public AsientoService(IAsientoRepository repository, IZonaService zonaService)
        {
            _repository = repository;
            _zonaService = zonaService;
        }

        public List<Asiento> ObtenerAsientos() => _repository.GetAll();

        public Asiento? ObtenerAsiento(int id) => _repository.GetById(id);

        public List<Asiento> ObtenerAsientosPorZona(int idZona) => _repository.GetByZona(idZona);

        public void CrearAsiento(Asiento asiento)
        {
            //CANDADO DE ADMIN
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden registrar asientos.");

            ValidarAsiento(asiento);
            ValidarCapacidadZona(asiento);
            ValidarDuplicado(asiento);
            _repository.Add(asiento);
        }

        public void ActualizarAsiento(Asiento asiento)
        {
            //CANDADO DE ADMIN
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden editar asientos.");

            ValidarAsiento(asiento);
            if (asiento.IdAsiento <= 0) throw new ArgumentException("El ID del asiento no es válido.");
            ValidarCapacidadZona(asiento);
            ValidarDuplicado(asiento);
            _repository.Update(asiento);
        }

        private static void ValidarAsiento(Asiento asiento)
        {
            if (asiento is null) throw new ArgumentNullException(nameof(asiento));
            if (asiento.IdZona <= 0) throw new ArgumentException("Debe seleccionar una zona.");
            if (string.IsNullOrWhiteSpace(asiento.Fila)) throw new ArgumentException("La fila es obligatoria.");
            if (asiento.NumeroAsiento <= 0) throw new ArgumentException("El número de asiento debe ser mayor a cero.");

            //La fila puede ser una letra o combinación de letra y número, ej. A, B, A1, B2
            string patronFila = @"^[a-zA-Z0-9]+$";
            if (!Regex.IsMatch(asiento.Fila, patronFila))
                throw new ArgumentException("La fila solo puede contener letras y números (sin espacios ni caracteres especiales).");
        }

        private void ValidarCapacidadZona(Asiento asiento)
        {
            var zona = _zonaService.ObtenerZona(asiento.IdZona);
            if (zona == null) throw new ArgumentException("La zona no existe.");

            var asientosDeZona = _repository.GetByZona(asiento.IdZona);
            int ocupados = 0;
            foreach (var a in asientosDeZona)
            {
                if (a.IdAsiento != asiento.IdAsiento)
                    ocupados++;
            }
            if (ocupados + 1 > zona.CupoDeLaZona)
                throw new ArgumentException($"La zona ya tiene {ocupados} asientos registrados y su cupo es {zona.CupoDeLaZona}.");
        }

        private void ValidarDuplicado(Asiento asiento)
        {
            int existe = _repository.ExisteAsiento(asiento.IdZona, asiento.Fila, asiento.NumeroAsiento, asiento.IdAsiento);
            if (existe > 0)
                throw new ArgumentException($"Ya existe un asiento {asiento.Fila}-{asiento.NumeroAsiento} en esta zona.");
        }


    }
}
