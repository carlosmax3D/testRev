using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;


namespace TemplateCSharp.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _repository;

        public ReporteService(IReporteRepository repository)
        {
            _repository = repository;
        }

        public List<ReporteVenta> ObtenerReporteVentas(DateTime fechaInicio, DateTime fechaFin, int? idEstadio, int? idPartido)
        {
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden generar reportes.");

            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha fin.");

            return _repository.ObtenerReporteVentas(fechaInicio, fechaFin, idEstadio, idPartido);
        }

        public List<ReporteOcupacion> ObtenerReporteOcupacion(int? idPartido, int? idZona, bool? soloDisponibles)
        {
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden generar reportes.");

            return _repository.ObtenerReporteOcupacion(idPartido, idZona, soloDisponibles);
        }

        public List<HistorialCompra> ObtenerHistorialPorCliente(int idCliente, string orden)
        {
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden generar reportes.");

            if (idCliente <= 0)
                throw new ArgumentException("El ID del cliente no es válido.");

            if (string.IsNullOrWhiteSpace(orden))
                orden = "fecha_compra";

            return _repository.ObtenerHistorialPorCliente(idCliente, orden);
        }

        public List<ReporteTopPartido> ObtenerTopPartidos(int cantidad)
        {
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden generar reportes.");
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            return _repository.ObtenerTopPartidos(cantidad);
        }

        public List<ReporteClienteFrecuente> ObtenerClientesFrecuentes(int cantidad)
        {
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden generar reportes.");
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            return _repository.ObtenerClientesFrecuentes(cantidad);
        }

        public List<ReporteVentasPorZona> ObtenerVentasPorZona()
        {
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden generar reportes.");
            return _repository.ObtenerVentasPorZona();
        }

        public ResumenPartido? ObtenerResumenPartido(int idPartido)
        {
            if (idPartido <= 0)
                throw new ArgumentException("El ID del partido no es válido.");
            return _repository.ObtenerResumenPartido(idPartido);
        }

    }
}
