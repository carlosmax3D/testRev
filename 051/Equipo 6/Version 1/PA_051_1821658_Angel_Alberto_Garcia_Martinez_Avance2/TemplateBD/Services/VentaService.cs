using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class VentaService : IVentaService
{
    private const int MaximoBoletosPorVenta = 4;

    private readonly IVentaRepository _repository;
    private readonly ISesionUsuario _sesion;

    public VentaService(IVentaRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public int MaximoBoletos => MaximoBoletosPorVenta;

    public decimal ObtenerTasaIva()
    {
        return _repository.ObtenerTasaIva();
    }

    public List<Zona> ObtenerZonasDisponibles(int idPartido)
    {
        if (idPartido <= 0)
        {
            throw new ArgumentException("Seleccione el partido.");
        }

        return _repository.ConsultarZonasDisponibles(idPartido, Actor());
    }

    // Asigna los primeros asientos libres de la zona. Se vuelven a pedir también los que
    // ya estaban en la compra para esa zona, así la lista nunca repite un asiento.
    public void AgregarBoletos(Venta venta, Zona zona, int cantidad)
    {
        if (venta.IdPartido <= 0)
        {
            throw new ArgumentException("Seleccione el partido.");
        }

        if (cantidad < 1)
        {
            throw new ArgumentException("La cantidad de boletos debe ser al menos 1.");
        }

        if (venta.Boletos.Count + cantidad > MaximoBoletosPorVenta)
        {
            throw new ArgumentException($"Solo se pueden comprar {MaximoBoletosPorVenta} boletos por compra.");
        }

        int yaEnLaZona = venta.ContarBoletosDeZona(zona.IdZona);
        List<Boleto> asignados = _repository.ConsultarAsientosDisponibles(
            venta.IdPartido, zona.IdZona, yaEnLaZona + cantidad, Actor());

        venta.Boletos.RemoveAll(boleto => boleto.IdZona == zona.IdZona);
        venta.Boletos.AddRange(asignados);
    }

    public Guid RegistrarVenta(Venta venta)
    {
        if (venta.IdCliente <= 0)
        {
            throw new ArgumentException("Primero busque y seleccione un cliente.");
        }

        if (venta.IdPartido <= 0)
        {
            throw new ArgumentException("Seleccione el partido.");
        }

        if (venta.Boletos.Count == 0 || venta.Boletos.Count > MaximoBoletosPorVenta)
        {
            throw new ArgumentException($"La compra debe tener de 1 a {MaximoBoletosPorVenta} boletos.");
        }

        if (Validador.Vacio(venta.MetodoPago))
        {
            throw new ArgumentException("Seleccione el método de pago.");
        }

        List<int> asientos = [];
        foreach (Boleto boleto in venta.Boletos)
        {
            if (asientos.Contains(boleto.IdAsiento))
            {
                throw new ArgumentException("La compra tiene un asiento repetido.");
            }

            asientos.Add(boleto.IdAsiento);
        }

        return _repository.Registrar(venta, Actor());
    }

    private int Actor()
    {
        if (_sesion.UsuarioActual is null)
        {
            throw new UnauthorizedAccessException("Inicie sesión para vender boletos.");
        }

        return _sesion.UsuarioActual.IdUsuario;
    }
}
