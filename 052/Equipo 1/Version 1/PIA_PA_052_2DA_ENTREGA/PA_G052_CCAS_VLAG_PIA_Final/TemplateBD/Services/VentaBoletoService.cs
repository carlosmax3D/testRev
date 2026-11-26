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
    public class VentaBoletoService : IVentaBoletoService
    {
        private readonly IVentaBoletoRepository _ventaRepository;
        private readonly IDetalleBoletoRepository _detalleRepository;

        public VentaBoletoService(IVentaBoletoRepository ventaRepository, IDetalleBoletoRepository detalleRepository)
        {
            _ventaRepository = ventaRepository;
            _detalleRepository = detalleRepository;
        }

        public List<VentaBoleto> ObtenerVentas() => _ventaRepository.GetAll();

        public VentaBoleto? ObtenerVenta(Guid id) => _ventaRepository.GetById(id);

        public List<DetalleBoleto> ObtenerDetalles(Guid idVenta) => _detalleRepository.GetByVenta(idVenta);

        public Guid RegistrarVenta(VentaBoleto venta, List<DetalleBoleto> detalles)
        {
            //Max 4 boletos, metodo de pago
            if (detalles == null || detalles.Count == 0)
                throw new ArgumentException("La venta debe incluir al menos un boleto.");

            if (detalles.Count > 4)
                throw new ArgumentException("No se pueden vender más de 4 boletos por transacción.");

            string[] metodosValidos = { "Efectivo", "Crédito", "Débito" };
            if (!metodosValidos.Contains(venta.MetodoPago))
                throw new ArgumentException("El método de pago debe ser Efectivo, Crédito o Débito.");

            //VALIDACION DE ASIENTOS OCUPADOS
            foreach (var detalle in detalles)
            {
                if (_detalleRepository.IsAsientoOcupado(detalle.IdAsiento, detalle.IdPartido))
                {
                    throw new ArgumentException($"El asiento con ID {detalle.IdAsiento} ya está ocupado para este partido.");
                }
            }


            //CALCULO DE SUBTOTAL, IVA Y TOTAL
            venta.Subtotal = detalles.Sum(d => d.PrecioUnitario);
            venta.Iva = venta.Subtotal * 0.16m; // IVA del 16%
            venta.Total = venta.Subtotal + venta.Iva;

            venta.FechaVenta = DateTime.Today;
            venta.HoraVenta = DateTime.Now.TimeOfDay;

            //guardar la venta y obtener el GUID generado
            Guid idVentaGenerado = _ventaRepository.Add(venta);

            //GUARDAR LOS DETALLEs
            foreach (var detalle in detalles)
            {
                detalle.IdVenta = idVentaGenerado;
                _detalleRepository.Add(detalle);
            }

            return idVentaGenerado;
        }
    }
}
