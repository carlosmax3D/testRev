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
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _repository;

        public FacturaService(IFacturaRepository repository)
        {
            _repository = repository;
        }

        public Factura? ObtenerFacturaPorVenta(Guid idVenta) => _repository.GetByVenta(idVenta);

        public void GenerarFactura(Factura factura)
        {
            //La factura la puede generar el operativo (no candado de admin)
            if (factura is null) throw new ArgumentNullException(nameof(factura));
            if (factura.IdVenta == Guid.Empty) throw new ArgumentException("La venta es obligatoria.");
            if (string.IsNullOrWhiteSpace(factura.FolioFactura)) throw new ArgumentException("El folio es obligatorio.");

            //Verificar que no exista ya una factura para esa venta
            var existente = _repository.GetByVenta(factura.IdVenta);
            if (existente != null) throw new ArgumentException("Esta venta ya tiene una factura generada.");

            factura.FechaEmision = DateTime.Today;
            factura.HoraEmision = DateTime.Now.TimeOfDay;
            factura.EstadoFactura = "Vigente";

            _repository.Add(factura);
        }

        public void CancelarFactura(int idFactura)
        {
            //solo el administrador puede cancelar facturas
            if (Sesion.TipoUsuario != "Administrador")
                throw new UnauthorizedAccessException("Solo los administradores pueden cancelar facturas.");

            _repository.UpdateEstado(idFactura, "Cancelada");
        }
    }
}
