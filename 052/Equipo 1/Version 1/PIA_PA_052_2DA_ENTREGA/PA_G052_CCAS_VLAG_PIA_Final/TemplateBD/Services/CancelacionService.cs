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
    public class CancelacionService : ICancelacionService
    {
        private readonly ICancelacionRepository _cancelacionRepository;
        private readonly IVentaBoletoRepository _ventaRepository;
        private readonly IDetalleBoletoRepository _detalleRepository;
        private readonly IFacturaRepository _facturaRepository;

        public CancelacionService(
            ICancelacionRepository cancelacionRepository,
            IVentaBoletoRepository ventaRepository,
            IDetalleBoletoRepository detalleRepository,
            IFacturaRepository facturaRepository)
        {
            _cancelacionRepository = cancelacionRepository;
            _ventaRepository = ventaRepository;
            _detalleRepository = detalleRepository;
            _facturaRepository = facturaRepository;
        }

        public Cancelacion? ObtenerCancelacionPorVenta(Guid idVenta) => _cancelacionRepository.GetByVenta(idVenta);

        public void CancelarVenta(Guid idVenta, string motivo)
        {
            //CANDADO DE ADMIN
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden cancelar ventas.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("El motivo de cancelación es obligatorio.");

            var venta = _ventaRepository.GetById(idVenta);
            if (venta == null) throw new ArgumentException("La venta no existe.");

            //verificar que no esté ya cancelada
            var cancelacionExistente = _cancelacionRepository.GetByVenta(idVenta);
            if (cancelacionExistente != null) throw new ArgumentException("Esta venta ya está cancelada.");

            //Registrar la cancelacion
            var cancelacion = new Cancelacion
            {
                IdVenta = idVenta,
                IdUsuarioAdmin = Sesion.IdUsuario,
                Motivo = motivo,
                FechaCancelacion = DateTime.Today,
                HoraCancelacion = DateTime.Now.TimeOfDay
            };
            _cancelacionRepository.Add(cancelacion);

            //liberar los boletos, eliminar los detalles de la venta
            var detalles = _detalleRepository.GetByVenta(idVenta);
            foreach (var detalle in detalles)
            {
                _detalleRepository.Delete(detalle.IdDetalle);
            }

            //Cancelar la factura si existe
            var factura = _facturaRepository.GetByVenta(idVenta);
            if (factura != null)
            {
                _facturaRepository.UpdateEstado(factura.IdFactura, "Cancelada");
            }
        }
    }
}
