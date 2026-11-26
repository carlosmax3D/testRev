using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Services;

public class CancelacionService : ServicioBase, ICancelacionService
{
    private readonly ICancelacionRepository _cancelaciones;
    private readonly IVentaService _ventas;

    public CancelacionService(
        ICancelacionRepository cancelaciones,
        IVentaService ventas,
        ISesionActual sesion) : base(sesion)
    {
        _cancelaciones = cancelaciones;
        _ventas = ventas;
    }

    public void CancelarVenta(string folioTexto, string motivo)
    {
        ExigirAdministrador();

        if (string.IsNullOrWhiteSpace(motivo) || motivo.Trim().Length < 5)
        {
            throw new ArgumentException("Capture el motivo de la cancelación (mínimo 5 caracteres).");
        }

        if (motivo.Trim().Length > 255)
        {
            throw new ArgumentException("El motivo no puede superar los 255 caracteres.");
        }

        Venta venta = _ventas.BuscarPorFolio(folioTexto)
            ?? throw new ArgumentException("No existe ninguna compra con ese folio.");

        if (!venta.EstaActiva)
        {
            throw new InvalidOperationException("La compra ya se encuentra cancelada.");
        }

        _cancelaciones.Add(motivo.Trim(), Sesion.IdUsuario, venta.IdVenta);
    }
}
